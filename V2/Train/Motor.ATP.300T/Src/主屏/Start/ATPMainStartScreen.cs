using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.ATP._300T.Config;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.共用;

namespace Motor.ATP._300T.主屏.Start
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ATPMainStartScreen : ATPBaseClass, IBtnEventListener
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Rectangle m_BkImageRectangle = new Rectangle(0, 0, 800, 600);

        private string m_Info;

        // ReSharper disable once InconsistentNaming
        private static readonly Rectangle m_InfoRectangle = new Rectangle(68, 450, 300, 30);

        private readonly List<string> m_InfoList = new List<string>()
        {
            "DMI CopyRight CRSC",
            "ATP正在启动，请等待",
            "ATP与主机通讯故障"
        };

        private readonly Font m_InFont = new Font(SystemFonts.DefaultFont.FontFamily, 12, FontStyle.Bold);

        private DateTime m_SwitchInTime;

        private readonly List<TimeSpan> m_TimePeriod = new List<TimeSpan>()
        {
            new TimeSpan(0, 0, 0, 42),
            new TimeSpan(0, 0, 1, 43)
        };

        private ICourseService m_CourseService;

        public override bool Initalize()
        {
            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);

            if (nParaA == ParaADefine.SwitchFromOther)
            {
                m_SwitchInTime = CurrentTime;
                Reset();
            }
        }

        public override bool mouseDown(Point point)
        {
            StartComplate();

            return true;
        }

        public override void paint(Graphics g)
        {
            if (!BoolList[GetInBoolIndex(InBoolKeys.Inb黑屏)] || m_CourseService.CurrentCourseState == CourseState.Stoped)
            {
                append_postCmd(CmdType.ChangePage, (int) ViewConfig.Black, 0, 0);
            }

            g.DrawImage(ComImages.底层框架_启动, m_BkImageRectangle);

            var dim = CurrentTime - m_SwitchInTime;
            if (BoolList[212])
            {
                m_Info = m_InfoList[2];
            }
            if (!BoolList[GetInBoolIndex(InBoolKeys.InbATP自检不通过)])
            {
                if (dim > m_TimePeriod[0])
                {
                    m_Info = m_InfoList[1];
                }
                if (dim > m_TimePeriod[1] ||
                    ATPButtonScreen.BtnUpState != null && !ATPButtonScreen.BtnUpState.IsPressResponsed &&
                    ATPButtonScreen.BtnUpState.BtnId == 17)
                {
                    StartComplate();
                }
            }
            g.DrawString(string.Format("{0:00}:{1:00}:{2:00} {3}", dim.Hours, dim.Minutes, dim.Seconds, m_Info),
                m_InFont,
                Brushes.White,
                m_InfoRectangle);
        }

        private void StartComplate()
        {
            append_postCmd(CmdType.ChangePage, (int) ViewConfig.WriteForDriver, 0, 0);
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub开机检测中), 0, 0);
        }

        private void Reset()
        {
            m_Info = m_InfoList[0];
        }

        public bool ResponseBtnEvent(BtnInfo btnInfo)
        {
            return false;
        }
    }
}
