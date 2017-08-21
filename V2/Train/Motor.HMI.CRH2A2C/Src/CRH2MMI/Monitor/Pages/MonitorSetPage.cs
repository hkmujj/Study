using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;

namespace CRH2MMI.Monitor.Pages
{
    abstract class MonitorSetPageBase : CommonInnerControlBase
    {
        private SettingKeyBoard m_SettingKeyBoard;

        private readonly MonitorSet m_MonitorSet;

        public EventHandler ChangePageBack;

        private static readonly List<Rectangle> m_ConstInfoRectangles;

        private static readonly List<Rectangle> m_ValueRectangles;

        public DateTime ShowTime
        {
            set
            {
                m_ShowTime = value;
                UpdateShowTime();
            }
            get { return m_ShowTime; }
        }

        /// <summary>
        /// 返回
        /// </summary>
        public CRH2Button m_BackBtn;

        static MonitorSetPageBase()
        {
            int inter = 100;
            const int width = 20;
            m_ConstInfoRectangles = new List<Rectangle>()
            {
                new Rectangle(10, 180, 100, 20),
                new Rectangle(10 + inter,   250, width, width),
                new Rectangle(10 + 2*inter, 250, width, width),
                new Rectangle(10 + 3*inter, 250, width, width),
            };

            var size = new Size(78, 50);
            var point = new Point(22, 220);
            m_ValueRectangles = new List<Rectangle>()
            {
                new Rectangle(point.X, point.Y, size.Width, size.Height),
                new Rectangle(point.X+ 8 + inter, point.Y, size.Width, size.Height),
                new Rectangle(point.X+ 8 + 2*inter, point.Y, size.Width, size.Height),
            };
        }

        protected MonitorSetPageBase(MonitorSet monitorSet)
        {
            m_MonitorSet = monitorSet;
            m_MonitorSet.MonitorDateTimeChanged += MonitorDateTimeChanged;
            m_SettingKeyBoard = new SettingKeyBoard();
        }

        private void MonitorDateTimeChanged(object sender, EventArgs eventArgs)
        {
            ShowTime = m_MonitorSet.MonitorDateTime;
        }

        protected List<string> m_ConstInfos;

        /// <summary>
        /// 固定信息
        /// </summary>
        private List<GDIRectText> m_ConstTexts;

        /// <summary>
        /// 设定的值
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected List<GDIRectText> m_SetValues;

        private GDIRectText m_SelectedValueTxt;

        private DateTime m_ShowTime;

        public override void Init()
        {
            InitBackBtn();

            InitKeyBoard();

            InitValueTxt();

            InitConstTxt();
        }

        public void Reset()
        {
            ShowTime = m_MonitorSet.MonitorDateTime;
            ReselectValueTxt(m_SetValues.First());
        }

        private void InitConstTxt()
        {
            m_ConstTexts = new List<GDIRectText>();
            for (int i = 0; i < 4; i++)
            {
                m_ConstTexts.Add(new GDIRectText()
                {
                    OutLineRectangle = m_ConstInfoRectangles[i],
                    Text = m_ConstInfos[i],
                });
            }
            m_ConstTexts[0].TextColor = CRH2Resource.WWBrush.Color;
        }

        private void InitValueTxt()
        {
            m_SetValues = new List<GDIRectText>();

            const int interval = 2;
            var txtSize = new Size(m_ValueRectangles[0].Width / 2 - 3 * interval, m_ValueRectangles[0].Height - 3 * interval);
            var strformt = new StringFormat() { LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft };
            foreach (var valueRectangle in m_ValueRectangles)
            {
                m_SetValues.Add(new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(valueRectangle.X + interval, valueRectangle.Y + interval, txtSize.Width, txtSize.Height),
                    TextFormat = strformt,
                    TextColor = Color.Yellow,
                });
                m_SetValues.Add(new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(valueRectangle.X + 2 * interval + txtSize.Width, valueRectangle.Y + interval, txtSize.Width, txtSize.Height),
                    TextFormat = strformt,
                    TextColor = Color.Yellow,
                });
            }

            ReselectValueTxt(m_SetValues[0]);
        }

        private void ReselectValueTxt(GDIRectText setValue)
        {
            if (m_SelectedValueTxt != null)
            {
                m_SelectedValueTxt.TextColor = Color.Yellow;
                m_SelectedValueTxt.BkColor = Color.Black;
            }

            m_SelectedValueTxt = setValue;
            m_SelectedValueTxt.TextColor = Color.Black;
            m_SelectedValueTxt.BkColor = Color.Yellow;
        }

        protected abstract void UpdateShowTime();

        protected abstract DateTime ParserDateTime();


        private void InitKeyBoard()
        {
            m_SettingKeyBoard = new SettingKeyBoard()
            {
                OutLineRectangle = new Rectangle(new Point(450, 180), new Size(250, 350)),
                NumberPressed = OnKeyboardNumberPressed,
                ControlPressed = OnKeyboardControlPressed
            };
        }

        private void OnKeyboardControlPressed(object sender, KeyBoardControlPressedEventArgs keyBoardControlPressedEventArgs)
        {
            switch (keyBoardControlPressedEventArgs.Type)
            {
                case KeyBoardControlType.Delete:
                    if (m_SelectedValueTxt != m_SetValues.First())
                    {
                        ReselectValueTxt(m_SetValues[m_SetValues.IndexOf(m_SelectedValueTxt) - 1]);
                    }
                    break;
                case KeyBoardControlType.Set:
                    m_MonitorSet.MonitorDateTime = ParserDateTime();
                    ReselectValueTxt(m_SetValues.First());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnKeyboardNumberPressed(object sender, KeyBoardNumberPressedEventArgs keyBoardNumberPressedEventArgs)
        {
            m_SelectedValueTxt.Text = keyBoardNumberPressedEventArgs.PressedNumber.ToString();
            if (m_SelectedValueTxt != m_SetValues.Last())
            {
                ReselectValueTxt(m_SetValues[m_SetValues.IndexOf(m_SelectedValueTxt) + 1]);
            }
        }

        private void InitBackBtn()
        {
            var point = new Point(680, 120);
            var btnSize = new Size(100, 40);
            m_BackBtn = new CRH2Button()
            {
                OutLineRectangle = new Rectangle(point, btnSize),
                Text = " 返   回 ",
                TextColor = Color.White,
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent = ChangePageBack
            };
        }

        public override bool OnMouseDown(Point point)
        {
            return m_BackBtn.OnMouseDown(point) || m_SettingKeyBoard.OnMouseDown(point);
        }

        public override void OnDraw(Graphics g)
        {
            m_ValueRectangles.ForEach(e => g.DrawRectangle(CRH2Resource.WhitePen, e));

            m_ConstTexts.ForEach(e => e.OnDraw(g));

            m_BackBtn.OnDraw(g);

            m_SettingKeyBoard.OnDraw(g);

            m_SetValues.ForEach(e => e.OnDraw(g));
        }
    }

    class MonitorSetDatePage : MonitorSetPageBase
    {
        public MonitorSetDatePage(MonitorSet monitorSet)
            : base(monitorSet)
        {
            m_ConstInfos = new List<string>() { "请设定日期。", "年", "月", "日" };
        }

        protected override void UpdateShowTime()
        {
            var year = ShowTime.Year % 100;
            m_SetValues[0].Text = (year / 10).ToString();
            m_SetValues[1].Text = (year % 10).ToString();

            m_SetValues[2].Text = (ShowTime.Month / 10).ToString();
            m_SetValues[3].Text = (ShowTime.Month % 10).ToString();

            m_SetValues[4].Text = (ShowTime.Day / 10).ToString();
            m_SetValues[5].Text = (ShowTime.Day % 10).ToString();
        }

        protected override DateTime ParserDateTime()
        {
            var str = string.Format("20{0}{1}-{2}{3}-{4}{5}", m_SetValues[0].Text, m_SetValues[1].Text,
                m_SetValues[2].Text, m_SetValues[3].Text, m_SetValues[4].Text, m_SetValues[5].Text);
            DateTime date;
            return DateTime.TryParse(str, out date) ? new DateTime(date.Year, date.Month, date.Day, ShowTime.Hour, ShowTime.Minute, ShowTime.Second) : ShowTime;
        }
    }

    class MonitorSetTimePage : MonitorSetPageBase
    {
        public MonitorSetTimePage(MonitorSet monitorSet)
            : base(monitorSet)
        {
            m_ConstInfos = new List<string>() { "请设定时间。", "时", "分", "秒" };
        }

        protected override void UpdateShowTime()
        {
            m_SetValues[0].Text = (ShowTime.Hour / 10).ToString();
            m_SetValues[1].Text = (ShowTime.Hour % 10).ToString();

            m_SetValues[2].Text = (ShowTime.Minute / 10).ToString();
            m_SetValues[3].Text = (ShowTime.Minute % 10).ToString();

            m_SetValues[4].Text = (ShowTime.Second / 10).ToString();
            m_SetValues[5].Text = (ShowTime.Second % 10).ToString();
        }

        protected override DateTime ParserDateTime()
        {
            var str = string.Format("{0}{1}:{2}{3}:{4}{5}", m_SetValues[0].Text, m_SetValues[1].Text,
                m_SetValues[2].Text, m_SetValues[3].Text, m_SetValues[4].Text, m_SetValues[5].Text);
            DateTime date;
            return DateTime.TryParse(str, out date) ? new DateTime(ShowTime.Year, ShowTime.Month, ShowTime.Day, date.Hour, date.Minute, date.Second) : ShowTime;

        }
    }
}
