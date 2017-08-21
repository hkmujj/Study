using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using Engine.TCMS.HXD3D.Config;
using Engine.TCMS.HXD3D.Title;
using Engine.TCMS.HXD3D.底层共用;
using Excel.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MsgReceiveMod;

namespace Engine.TCMS.HXD3D.Fault
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FaultReceive : HXD3BaseClass
    {
        private IReadOnlyDictionary<int, FaultItemConfig> m_FaultInfoDictionary;

        private static List<int> m_HappedErrorList;

        public static HXD3DMsgHandler<FaultMsgHXD3D> MsgInf { get; private set; }

        public override string GetInfo()
        {
            return "故障接收模块";
        }

        protected override  void Initalize()
        {
            InitData();

            SetValueWhenDebug();
        }

        private void InitData()
        {
            m_FaultInfoDictionary =
                ExcelParser.Parser<FaultItemConfig>(AppPaths.ConfigDirectory)
                    .ToDictionary(k => k.LogicIndex, k => k)
                    .AsReadOnlyDictionary();

            m_HappedErrorList = new List<int>();

            MsgInf = new HXD3DMsgHandler<FaultMsgHXD3D>(SortCriteriaOfMsg.Level);
        }

        private void SetValueWhenDebug()
        {
            foreach (var kvp in m_FaultInfoDictionary.Take(5))
            {
                append_postCmd(CmdType.SetInBoolValue, kvp.Key, 1, 0);
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if ((nParaA == 1) && (nParaB == 100))
            {
                MsgInf.ClearAllList();
            }
        }

        private void UpdateFault()
        {
            foreach (var kvp in m_FaultInfoDictionary)
            {
                var it = kvp.Value;

                if (BoolList[kvp.Key] && !m_HappedErrorList.Contains(kvp.Key))
                {
                    m_HappedErrorList.Add(kvp.Key);
                    var msg = new FaultMsgHXD3D
                    {
                        MsgID = it.LogicIndex.ToString(),
                        SubSysName = it.SubsysName,
                        TheMsgLevel = (MsgLevels) it.MsgLevel,
                        MsgContent = it.Content,
                        FaultSolutionStr = it.Solution,
                        MsgSendLogicID = it.SendIndex,
                        MsgShowType = (MsgShowType) it.ShowType,
                        TrainID = it.TrainId,
                        MsgReceiveTime = DateTime.Now
                    };

                    MsgInf.AddNewMsg(msg);

                    if (msg.TheMsgLevel == MsgLevels.Level3)
                    {
                        TopTitleScreen.Countdown.CounterStart();
                    }
                }
                else if (!(!m_HappedErrorList.Contains(kvp.Key) || BoolList[kvp.Key]))
                {
                    m_HappedErrorList.Remove(kvp.Key);
                    MsgInf.MsgOver(it.LogicIndex.ToString());
                }
            }

        }

        public override void paint(Graphics dcGs)
        {
            UpdateFault();
        }

    }
}