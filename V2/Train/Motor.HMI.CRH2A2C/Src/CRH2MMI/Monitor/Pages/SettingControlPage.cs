using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;
using CRH2MMI.Common;
using CRH2MMI.Common.Util;


namespace CRH2MMI.Monitor.Pages
{
    class SettingControlPage : CommonInnerControlBase
    {
        private List<CommonInnerControlBase> m_ConstInfos;

        private MonitorSet m_MonitorSet;

        private List<CRH2Button> m_SetBtns;

        public EventHandler<ChangedToPageEventArgs> ChangePage;

        /// <summary>
        /// 上次修改的时间
        /// </summary>
        private DateTime LastSetTime
        {
            set
            {
                m_LastSetTime = value;
                m_LastSetTimeText.Text = string.Format("{0}        {1}              {2}", value.Year, value.Month, value.Day);
            }

            get { return m_LastSetTime; }
        }

        private GDIRectText m_LastSetTimeText;

        private string m_DateString;
        private static Point m_DateStringLocation;
        private string m_TimeString;
        private static Point m_TimeStringLocation;

        private static List<Point> m_WheelLocations;

        private const string Date = "年\t        月                日";
        private static readonly Point DateLocation;
        private const string Time = "时\t        分                秒";
        private static readonly Point TimeLocation;
        private const string Wheel = "T1\t\tmm\r\n\r\nT2\t\tmm";
        private string m_Wheel;
        private static Point m_WheelLocation;

        private const string SetTimeInfo = " 年        月        日行车时间表更改";

        private static readonly List<Rectangle> m_Rectangles;

        private static readonly List<Rectangle> m_BtnRectangles;


        static SettingControlPage()
        {
            var point1 = new Point(1, 200);
            var size1 = new Size(500, 300);
            var size2 = new Size(285, 300);
            m_BtnRectangles = new List<Rectangle>();

            var btnSize = new Size(100, 100);
            for (int i = 0; i < 2; i++)
            {
                m_BtnRectangles.Add(new Rectangle(point1.X + 10, point1.Y + 50 + i * btnSize.Height, btnSize.Width, btnSize.Height));
            }

            m_Rectangles = new List<Rectangle>()
            {
                new Rectangle(point1.X + 2, point1.Y, size1.Width, size1.Height),
                new Rectangle(point1.X + size1.Width + 5, point1.Y, size2.Width, size2.Height),
            };
            m_WheelLocation = new Point(m_Rectangles[1].Left + 3, m_Rectangles[1].Top + 100);
            m_BtnRectangles.ForEach(
                e => m_Rectangles.Add(new Rectangle(e.Right + 3, e.Top, size1.Width - e.Width - 10 - 5, e.Height)));
            DateLocation = new Point(m_BtnRectangles[0].Right + 3 + 100, m_BtnRectangles[0].Top + 30);
            m_DateStringLocation = new Point(m_BtnRectangles[0].Right + 3 + 2 + 60, m_BtnRectangles[0].Top + 30);
            TimeLocation = new Point(m_BtnRectangles[1].Right + 3 + 100, m_BtnRectangles[1].Top + 30);
            m_TimeStringLocation = new Point(m_BtnRectangles[1].Right + 3 + 2 + 60, m_BtnRectangles[1].Top + 30);
            m_WheelLocations = new List<Point>();
            for (int i = 0; i < 2; i++)
            {
                m_WheelLocations.Add(new Point(m_WheelLocation.X + 80, m_WheelLocation.Y + i * 55));
            }
        }

        public SettingControlPage(MonitorSet monitorSet)
        {
            m_MonitorSet = monitorSet;
            m_SrcObj = monitorSet;
            m_ConstInfos = new List<CommonInnerControlBase>();
            m_MonitorSet.MonitorDateTimeChanged += MonitorDateTimeChanged;
        }

        private void MonitorDateTimeChanged(object sender, EventArgs eventArgs)
        {
            var value = m_MonitorSet.MonitorDateTime;
            m_DateString = string.Format("{0}            {1}                     {2}", value.Year, value.Month, value.Day);
            m_TimeString = string.Format("{0}            {1}                     {2}", value.Hour, value.Minute, value.Second);
        }

        /// <summary>
        /// 选择项目的
        /// </summary>
        //private List<CRH2Button> m_SelectProjectBtns;

        private DateTime m_LastSetTime;

        private readonly CRH2BaseClass m_SrcObj;

        public override void Init()
        {
            InitTitle();

            m_Wheel = string.Format("{0}\t\t\r\n\r\n{1}\t\t", m_MonitorSet.WheelDiameters[0], m_MonitorSet.WheelDiameters[1]);

            var second = m_Rectangles[1];
            var txtSize = new Size(300, 20);
            m_LastSetTimeText = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(second.X - 30, second.Y - 70, txtSize.Width, txtSize.Height),
                TextColor = Color.Yellow,
                BkColor = Color.Black,
            };
            LastSetTime = m_SrcObj.CurrentTime;

            InitBtn();
        }

        private void InitBtn()
        {
            m_SetBtns = new List<CRH2Button>()
            {
                new CRH2Button()
                {
                    OutLineRectangle = m_BtnRectangles[0],
                    Text = " 日    期 ",
                    TextColor = Color.White,
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent =
                        (sender, args) =>
                            HandleUtil.OnHandle(ChangePage, sender,
                                new ChangedToPageEventArgs(ChangedToPageEventArgs.ChangeTo.Date)),
                },
                new CRH2Button()
                {
                    OutLineRectangle = m_BtnRectangles[1],
                    Text = " 时    间 ",
                    TextColor = Color.White,
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent =
                        (sender, args) =>
                            HandleUtil.OnHandle(ChangePage, sender,
                                new ChangedToPageEventArgs(ChangedToPageEventArgs.ChangeTo.Time)),
                }
            };
        }

        private void InitTitle()
        {
            var fisrt = m_Rectangles[0];
            var second = m_Rectangles[1];
            var txtSize = new Size(150, 20);
            var strFormat = new StringFormat() { Alignment = StringAlignment.Center };
            var font = CRH2Resource.Font14;
            m_ConstInfos = new List<CommonInnerControlBase>()
            {
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(fisrt.X + 70, fisrt.Y - 10, txtSize.Width, txtSize.Height),
                    TextColor = Color.White,
                    BkColor = Color.Black,
                    TextFormat = strFormat,
                    DrawFont = font,
                    Text = "请选择项目。",
                },

                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(second.X + 10, second.Y - 10, txtSize.Width, txtSize.Height),
                    TextColor = Color.White,
                    BkColor = Color.Black,
                    TextFormat = strFormat,
                    DrawFont = font,
                    Text = "车轮径设定值",
                }
            };
        }


        public void RefreshDateTimeString()
        {
            m_DateString = string.Format("{0}\t{1}\t{2}", m_MonitorSet.MonitorDateTime.Year,
                m_MonitorSet.MonitorDateTime.Month, m_MonitorSet.MonitorDateTime.Day);
            m_TimeString = string.Format("{0}\t{1}\t{2}", m_MonitorSet.MonitorDateTime.Hour,
                m_MonitorSet.MonitorDateTime.Minute, m_MonitorSet.MonitorDateTime.Second);
        }

        public override void OnDraw(Graphics g)
        {
            m_Rectangles.ForEach(e => g.DrawRectangle(CRH2Resource.WhitePen, e));

            m_LastSetTimeText.OnDraw(g);
            g.DrawString(SetTimeInfo, CRH2Resource.Font12, CRH2Resource.WhiteBrush, new PointF(m_LastSetTimeText.Location.X + 35, m_LastSetTimeText.Location.Y));

            m_ConstInfos.ForEach(e => e.OnDraw(g));

            g.DrawString(Date, CRH2Resource.Font13, CRH2Resource.WhiteBrush, DateLocation);
            g.DrawString(m_DateString, CRH2Resource.Font13, CRH2Resource.YellowBrush, m_DateStringLocation);
            g.DrawString(Time, CRH2Resource.Font13, CRH2Resource.WhiteBrush, TimeLocation);
            g.DrawString(m_TimeString, CRH2Resource.Font13, CRH2Resource.YellowBrush, m_TimeStringLocation);
            g.DrawString(Wheel, CRH2Resource.Font16, CRH2Resource.WhiteBrush, m_WheelLocation);

            for (int i = 0; i < m_MonitorSet.WheelDiameters.Count; i++)
            {
                g.DrawString(m_MonitorSet.WheelDiameters[i].ToString(), CRH2Resource.Font13, CRH2Resource.YellowBrush, m_WheelLocations[i]);
            }

            //m_BtnRectangles.ForEach(e => g.DrawRectangle(CRH2Resource.WhitePen, e));
            m_SetBtns.ForEach(e => e.OnDraw(g));

        }

        public override bool OnMouseDown(Point point)
        {
            return m_SetBtns.Any(a => a.OnMouseDown(point));
        }
    }
}
