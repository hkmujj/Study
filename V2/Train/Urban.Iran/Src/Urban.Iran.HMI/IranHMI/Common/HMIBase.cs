using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;

namespace Urban.Iran.HMI.Common
{
    /// <summary>
    /// ATP 基类
    /// </summary>
    public class HMIBase : baseClass
    {
        private Image[] m_Images;

        public Image[] Images
        {
            get { return m_Images ?? (m_Images = UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToArray()); }
        }

        public sealed override bool init(ref int nErrorObjectIndex)
        {
            try
            {
                if (Initalize())
                {
                    return true;
                }
                LogMgr.Error(string.Format("initalize {0} fail.", GetType()));
                return false;
            }
            catch (Exception e)
            {
                LogMgr.Fatal(string.Format("Initalize {0} fail, occuse some exception, {1}", GetType(), e));
                return false;
            }
        }

        public sealed override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex)nParaB;
            }

            SetNavigateValue((NavigateType) nParaA, (IranViewIndex)nParaB, (IranViewIndex)nParaC);
        }

        protected virtual void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {

        }

        public virtual void ChangedPage(IranViewIndex iranView)
        {
            append_postCmd(CmdType.ChangePage, (int)iranView, 0, 0);
        }

        protected void UpdateUiobject(CommunicationIndexType type, IEnumerable<string> names)
        {
            switch (type)
            {
                case CommunicationIndexType.InBool:
                    UIObj.InBoolList.AddRange(names.Select(s => GlobleParam.Instance.CommunicationIndexFacade.FindIndex(CommunicationIndexType.InBool, s)));
                    break;
                case CommunicationIndexType.InFloat:
                    UIObj.InFloatList.AddRange(names.Select(s => GlobleParam.Instance.CommunicationIndexFacade.FindIndex(CommunicationIndexType.InFloat, s)));
                    break;
                case CommunicationIndexType.OutBool:
                    UIObj.OutBoolList.AddRange(names.Select(s => GlobleParam.Instance.CommunicationIndexFacade.FindIndex(CommunicationIndexType.OutBool, s)));
                    break;
                case CommunicationIndexType.OutFloat:
                    UIObj.OutFloatList.AddRange(names.Select(s => GlobleParam.Instance.CommunicationIndexFacade.FindIndex(CommunicationIndexType.OutFloat, s)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        protected virtual bool Initalize()
        {
            return true;
        }

    }
}
