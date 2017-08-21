using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.ATP._300D.Common;

namespace Motor.ATP._300D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Background2D : ATPBase
    {
        int FC_X = FrameButton2D.FrameChange_X;
        int FC_Y = FrameButton2D.FrameChange_Y;

        Timer m_FlashTimer;

        private ICourseService m_CourseService;

        /// <summary>
        /// 时间是否需要闪烁
        /// </summary>
        public static bool TimeNeedFlash { private set; get; }

        private bool[] m_BValue;

        static DateTime m_Currenttime = DateTime.Now;

        //时间日期
        public static String StrtimeY { get { return m_Currenttime.ToString("yy.MM.dd"); } }
        public static String StrtimeH { get { return TimeNeedFlash ? StrtimeH1 : m_Currenttime.ToString("HH mm ss"); } }
        public static String StrtimeH1 { get { return m_Currenttime.ToString("HH:mm:ss"); } }


        /// <summary>
        /// 被按下的按键的索引  , -1 代表没有任何button被按下
        /// </summary>
        public static ButtonType PressedButtonIndex { private set; get; }


        /// <summary>
        /// 被按下弹起的按键的索引  , -1 代表没有任何button被按下
        /// </summary>
        public static ButtonType PoppedButtonIndex { private set; get; }

        /// <summary>
        /// 按键个数
        /// </summary>
        private readonly int m_ButtonCount;

        public static bool bfirstshuju = false;
        public static bool bfirstcheci = false;
        public static bool bfirstsijihao = false;

        readonly SolidBrush m_BackgroundBrush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));

        private Blackscreen2D m_Blackscreen;

        static Background2D()
        {
            TimeNeedFlash = false;
        }

        public Background2D()
        {
            PressedButtonIndex = ButtonType.None;
            m_ButtonCount = Enum.GetValues(typeof (ButtonType)).Cast<int>().Max();

        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            m_Blackscreen.setRunValue(nParaA, nParaB, nParaC);

            if (nParaA == 2)
            {
                //append_postCmd(CmdType.SetFloatValue, 213, 0, nParaB);
            }
        }

        public override void paint(Graphics g)
        {
            m_Blackscreen.paint(g);

            GetValue();
            OnDraw(g);
        }

        public void SetImgflash(bool needFlash, int dur)
        {
            if (needFlash)
            {
                m_FlashTimer = new Timer(o => TimeNeedFlash = !TimeNeedFlash);
                m_FlashTimer.Change(dur, dur);

            }
            else
            {
                if (m_FlashTimer != null)
                {
                    m_FlashTimer.Dispose();
                }
            }
        }

        private void OnButtonClick()
        {

            PressedButtonIndex = ButtonType.None;
            PoppedButtonIndex = ButtonType.None;

            for (var i = 0; i <= m_ButtonCount; i++)
            {
                //if (BoolList[320 + i] && !BoolOldList[320 + i])
                if (BoolList[UIObj.InBoolList[4] + i] && !BoolOldList[UIObj.InBoolList[4] + i])
                {
                    PressedButtonIndex = (ButtonType)i;
                    break;
                }

                if (!BoolList[UIObj.InBoolList[4] + i] && BoolOldList[UIObj.InBoolList[4] + i])
                {
                    PoppedButtonIndex = (ButtonType)i;
                    break;
                }
            }
        }

        public void GetValue()
        {
            m_Currenttime = DateTime.Now;

            OnButtonClick();

            m_BValue = UIObj.InBoolList.Select(s => BoolList[s]).ToArray();
            //if (bValue[0])
            if (m_BValue[2])
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
            }

            if (!BoolOldList[UIObj.InBoolList[0]] && m_BValue[0])
            {
                for (var k = 1; k < 6; k++)
                {
                    append_postCmd(CmdType.SetBoolValue, 4894 + k, 0, 0);
                }
                append_postCmd(CmdType.SetBoolValue, 4893, 1, 0);

                bfirstshuju = false;
                StrDrivData2D.breal = false;
                DMIMainMenu2D.Popuptxt = " ";


                bfirstcheci = false;
                bfirstsijihao = false;
            }
            //if (BoolList[270])
            if (m_BValue[3])
            {
                append_postCmd(CmdType.ChangePage, 45, 0, 0);
            }
        }

        public override bool Initalize()
        {
            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();
            m_Blackscreen = new Blackscreen2D(this)
            {
                IsBlackScreen = obj => !BoolList[UIObj.InBoolList[5]] || m_CourseService.CurrentCourseState == CourseState.Stoped
            };
            m_Blackscreen.Initalize();
            append_postCmd(CmdType.SetInBoolValue, 263, 1, 0);
            SetImgflash(true, 500);

            return true;
        }

        public override string GetInfo()
        {
            return "课程开始/结束";
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(m_BackgroundBrush, 0, 0, 800, 600);
        }

        public static void ChangePressedButtonType(ButtonType type)
        {
            PressedButtonIndex = type;
        }

        public static void ChangePoppedButtonType(ButtonType type)
        {
            PoppedButtonIndex = type;
        }
    }
}
