using System;
using System.Collections.Generic;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.TrainLineNo;
using CRH2MMI.WorkState;

namespace CRH2MMI.Fault
{
    class FaultCreator
    {
        private readonly FaultConfig m_FaultConfig;
        private readonly CRH2BaseClass m_ViewClass;
        private readonly List<int> m_AllFaultIndexes;

        private List<int> m_CurrentFaultLogicIndexs;

        private FaultMgr.ICanDeleteFaultGetter m_CanDeleteFaultGetter;

        /// <summary>
        /// 更新同一个故障的间隔
        /// </summary>
        private readonly TimeSpan m_UpdateInterval;

        public FaultCreator(CRH2BaseClass viewClass, FaultConfig faultConfig)
        {
            m_FaultConfig = faultConfig;
            m_ViewClass = viewClass;
            m_AllFaultIndexes = FaultMgr.Instance.FaultNameInfoDic.Keys.ToList();
            if (GlobalInfo.CurrentCRH2Config.FaultConfig.UpdateIntervale != -1)
            {
                m_UpdateInterval = TimeSpan.FromMilliseconds(GlobalInfo.CurrentCRH2Config.FaultConfig.UpdateIntervale);
            }
            else
            {
                m_UpdateInterval = TimeSpan.MaxValue;
            }

            m_CanDeleteFaultGetter = FaultMgr.Instance.GetterFacotry.CreateGetter(FaultGetterType.NotDel) as FaultMgr.ICanDeleteFaultGetter;
        }

        private ICommunicationPortFacade ICommunicationPortFacade
        {
            get
            {
                return GlobalResource.Instance.ICommunicationPortFacade;
            }
        }

        public bool CheckHasFault()
        {
            foreach (var fault in FaultMgr.Instance)
            {
                if (!m_ViewClass.BoolList[fault.LogicIndex])
                {
                    fault.Activie = false;
                    fault.HasDeleted= true;
                    m_CanDeleteFaultGetter.DeleteFaultItem(fault);
                }
            }

            m_CurrentFaultLogicIndexs = m_AllFaultIndexes.FindAll(f => m_ViewClass.BoolList[f]);

            return m_CurrentFaultLogicIndexs.Any();
        }

        private float GetInFloatValue(string name)
        {
            return m_ViewClass.GetInFloatValue(name);
        }

        public int GetInFloatValueAsInt(string name)
        {
            return (int) GetInFloatValue(name);
        }

        public void UpdateFaultMgr()
        {
            m_CurrentFaultLogicIndexs.ForEach(e =>
            {
                if (FaultMgr.Instance.FaultNameInfoDic.ContainsKey(e))
                {
                    var nameInfo = FaultMgr.Instance.FaultNameInfoDic[e];

                    var histor = FaultMgr.Instance.LastOrDefault(f => f.LogicIndex == e);

                    if (Validate(histor))
                    {
                        var tmp = new FaultItemInfo(m_ViewClass.CurrentTime)
                        {
                            LogicIndex = e,
                            Id = nameInfo.FaultNo,
                            TrainNo = TNSET.TrainLine,
                            Name = nameInfo.FaultName,
                            CarNumbers = nameInfo.FaultCarNos,
                            Speed = GetInFloatValueAsInt(m_FaultConfig.Speed).ToString(),
                            Distance = GetInFloatValueAsInt(m_FaultConfig.Distance).ToString("#,#0.0"),
                            Level = WorkStateResource.Instance.GetLevel(),
                            Brake = WorkStateResource.Instance.GetBrakeType(),
                            ProessInfoImageFile = nameInfo.FaultProcessImageFile,
                            ProtectedMachine = nameInfo.ProtectedMachine,
                            ProessInfoImage = GlobalResource.Instance.GetOrLoadImage(nameInfo.FaultProcessImageFile),
                        };
                        FaultMgr.Instance.Add(tmp);
                    }
                }
            });

        }

        private bool Validate(FaultItemInfo histor)
        {
            if (histor == null)
            {
                return true;
            }
            if (m_UpdateInterval == TimeSpan.MaxValue)
            {
                return histor.HasDeleted;
            }
            // 暂时每一分钟更新相同的故障
            return ( m_ViewClass.CurrentTime - histor.OccurTime ) > m_UpdateInterval || histor.HasDeleted;
        }
    }
}
