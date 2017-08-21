using System.Drawing;
using System.IO;
using System.Windows.Forms.VisualStyles;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BlackScreen : CRH1BaseClass
    {
        //2
        private bool m_LastRestartState;
        private bool[] m_BValue;

        public override string GetInfo()
        {
            return "黑屏视图";
        }


        //6
        public override bool Initialize()
        {
            InitData();

            GlobalInfo.Instance.Init(Path.Combine(RecPath, "..\\config"));
            if (GlobalInfo.Instance.Crh1AConfig.DebugConfig.AutoLightUpScreen)
            {
                OnPost(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);
            }

            m_LastRestartState = BoolList[UIObj.InBoolList[0]];

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        protected override void NavigateInCurrent(ViewConfig current)
        {
            if (!m_LastRestartState)
            {
                m_LastRestartState = m_BValue[1];

                if (m_BValue[1] && m_BValue[0])
                {
                    GT_GalobalFaultManage.Instance.CanResponseFaultA = false;
                    GT_GalobalFaultManage.Instance.CanResponsePopupFault = false;
                    OnPost(CmdType.ChangePage, (int)ViewConfig.Start, 0, 0);
                    return;
                }
            }

            m_LastRestartState = m_BValue[1];

            if (current == ViewConfig.BlackScreen)
            {
                //电钥匙闭合
                if (m_BValue[0])
                {
                    GT_GalobalFaultManage.Instance.CanResponseFaultA = false;
                    OnPost(CmdType.ChangePage, (int)ViewConfig.Login, 0, 0);
                }
            }
            else
            {
                if (!m_BValue[0])
                {
                    GT_GalobalFaultManage.Instance.CanResponseFaultA = false;
                    OnPost(CmdType.ChangePage, (int)ViewConfig.BlackScreen, 0, 0);
                }
            }
        }


        private void Draw(Graphics g)
        {
                
        }

        private void GetValue()
        {
            for (int i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
            }
        }

        private void InitData()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];
        }
    }
}