using System;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Alarm.FaultPopupView
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class UserDefineFaultView : CRH1BaseClass
    {
        private ExceptionData m_CurrentFault;

        private Image[] m_Images;

        private readonly Rectangle m_FullScreen = new Rectangle(0, 0, 800, 600);

        public static UserDefineFaultView Instance { private set; get; }

        public Action<UserDefineFaultView, Graphics> DrawContent;

        public ExceptionData CurrentFault
        {
            set
            {
                m_CurrentFault = value;
                RefreshState(value);
            }
            get { return m_CurrentFault; }
        }

        protected override void NavigateFrom(ViewConfig fromOhter)
        {
            RefreshState(this.CurrentFault);
        }

        private void RefreshState(ExceptionData value)
        {
            if (value == null)
            {
                return;
            }
            switch (value.ExType)
            {
                case FaultType.UserDefinedSystemHalted :
                    DrawContent = null;
                    CanResponseUser = false;
                    break;
                case FaultType.UserDefinedSystemSleep :
                    DrawContent = (view, graphics) => graphics.DrawImage(view.m_Images[1], m_FullScreen);
                    CanResponseUser = false;
                    break;
                default :
                    DrawContent = null;
                    LogMgr.Error(string.Format("You send an fault to UserDefineFaultView, but the type is {0}", value.ExType));
                    break;
            }
        }

        public override bool Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            m_Images = UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToArray();

            GlobalEvent.Instance.RestartCompleted += (sender, args) =>
            {
                CanResponseUser = true;
                DrawContent = null;
                this.CurrentFault = null;
            };

            return true;
        }

        public override void paint(Graphics g)
        {
            if (DrawContent != null)
            {
                DrawContent(this, g);
            }

        }
    }
}