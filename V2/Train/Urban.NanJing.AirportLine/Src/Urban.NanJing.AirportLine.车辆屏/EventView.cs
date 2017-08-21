using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    class EventView : baseClass
    {
        private Page m_Page;
        private EventUnion m_EventUnion;
        private Image[] m_ImageArray;

        private Button m_UpButton;
        private Button m_DownButton;

        private Button m_OperateInstruct;
        private Button m_AllAffirm;

        private Button m_BackButton;
        private Button m_Affirm;

        public static Dictionary<int, FaultEvent> m_FaultEventDictionary = new Dictionary<int, FaultEvent>();
        public static List<FaultEvent> m_CurrentEventList = new List<FaultEvent>();
        public static int m_FaultLevel = 0;

        private bool m_IsDisplayHelp = false;
        private string m_FaultMessage = "当前故障清单";

        public override string GetInfo()
        {
            return "列车故障信息";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFaultInfo();

            m_Page = new Page(new Rectangle(580, 310, 60, 25));
            m_EventUnion = new EventUnion(new Rectangle(80, 340, 460, 210), m_CurrentEventList);
            m_EventUnion.MaxPageChangedEvent += (int page) => { m_Page.Total = page; };

            m_ImageArray = new Image[UIObj.ParaList.Count];

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }

            m_UpButton = new Button(new Rectangle(540, 340, 50, 50), null, m_ImageArray[0]);
            m_UpButton.MouseUpEvent += OnUpButtonHandler;
            m_DownButton = new Button(new Rectangle(540, 395, 50, 50), null, m_ImageArray[1]);
            m_DownButton.MouseUpEvent += OnDownButtonHandler;

            m_OperateInstruct = new Button(new Rectangle(595, 340, 100, 100), null, m_ImageArray[2]);
            m_OperateInstruct.MouseUpEvent += (message) => { m_FaultMessage = "操作指导"; m_IsDisplayHelp = true; m_EventUnion.IsDisplayEventInfo = true; };
            m_AllAffirm = new Button(new Rectangle(565, 450, 100, 50), null, m_ImageArray[3]);
            m_AllAffirm.MouseUpEvent += s => m_CurrentEventList.ForEach(e =>
            {
                e.Affirm = true;
            });
            m_BackButton = new Button(new Rectangle(595, 340, 100, 100), null, m_ImageArray[5]);
            m_BackButton.MouseUpEvent += (message) => { m_FaultMessage = "当前故障清单"; m_IsDisplayHelp = false; m_EventUnion.IsDisplayEventInfo = false; };
            m_Affirm = new Button(new Rectangle(545, 450, 150, 100), null, m_ImageArray[4]);
            m_Affirm.MouseUpEvent += (s) =>
            {
                if (m_CurrentEventList.Count==0)
                {
                    return;
                }
                m_CurrentEventList[m_EventUnion.CurrentIndex].Affirm = true;
            };
            return true;
        }

        private void LoadFaultInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "故障信息.txt");
            var all = File.ReadLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] stringArray = cStr.Split(new char[] { ';' });
                FaultEvent f = new FaultEvent(Int32.Parse(stringArray[0].Trim()), stringArray[1].Trim(), stringArray[2].Trim(), Int32.Parse(stringArray[3].Trim()), stringArray[4].Trim(), stringArray[5].Trim());
                m_FaultEventDictionary.Add(Int32.Parse(stringArray[0].Trim()), f);
            }
        }


        private void OnUpButtonHandler(int message)
        {
            m_EventUnion.CurrentIndex -= 1;

            m_Page.CurrentPage = m_EventUnion.CurrentPage;
        }

        private void OnDownButtonHandler(int message)
        {
            m_EventUnion.CurrentIndex += 1;

            m_Page.CurrentPage = m_EventUnion.CurrentPage;
        }

        public override bool mouseDown(Point point)
        {
            m_UpButton.MouseDown(point);
            m_DownButton.MouseDown(point);

            if (!m_IsDisplayHelp)
            {
                m_OperateInstruct.MouseDown(point);
                m_AllAffirm.MouseDown(point);
            }
            else
            {
                m_BackButton.MouseDown(point);
                m_Affirm.MouseDown(point);
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            m_UpButton.MouseUp(point);
            m_DownButton.MouseUp(point);

            if (!m_IsDisplayHelp)
            {
                m_OperateInstruct.MouseUp(point);
                m_AllAffirm.MouseUp(point);
            }
            else
            {
                m_BackButton.MouseUp(point);
                m_Affirm.MouseUp(point);
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawString(m_FaultMessage, Common.m_16Font, Common.m_BlueBrush, new Point(80, 310));
            g.DrawString("页", Common.m_16Font, Common.m_BlueBrush, new Point(540, 310));

            m_Page.OnDraw(g);
            m_EventUnion.OnDraw(g);

            m_UpButton.OnDraw(g);
            m_DownButton.OnDraw(g);

            if (!m_IsDisplayHelp)
            {
                m_OperateInstruct.OnDraw(g);
                m_AllAffirm.OnDraw(g);
            }
            else
            {
                m_BackButton.OnDraw(g);
                m_Affirm.OnDraw(g);
            }
        }
    }

    class Page
    {
        public int Total
        {
            get
            {
                return m_Total;
            }
            set
            {
                m_Total = value;
            }
        }
        private int m_Total = 1;

        public int CurrentPage
        {
            set
            {
                m_CurrentPage = value;
            }
            get
            {
                return m_CurrentPage;
            }
        }
        private int m_CurrentPage = 1;

        private Rectangle m_Rect;

        public Page(Rectangle rect)
        {
            m_Rect = rect;
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common.m_BlackBrush, m_Rect);

            g.DrawString(m_CurrentPage.ToString(), Common.m_16Font, Common.m_WhiteBrush, new Point(m_Rect.X, m_Rect.Y));
            g.DrawString("/", Common.m_16Font, Common.m_WhiteBrush, new Point(m_Rect.X + 20, m_Rect.Y));
            g.DrawString(m_Total.ToString(), Common.m_16Font, Common.m_WhiteBrush, new Point(m_Rect.X + 40, m_Rect.Y));
        }
    }


    class EventUnion
    {
        public event Action<int> MaxPageChangedEvent;

        private readonly List<EventDisplay> m_EventDisplayList = new List<EventDisplay>();

        private readonly EventDisplay m_CurrentEventDisplay;
        private readonly Rectangle m_CurrentEventRect;

        private readonly Rectangle m_Rect;
        private readonly List<FaultEvent> m_FaultEventList;
        public EventUnion(Rectangle rect, List<FaultEvent> faultList)
        {
            m_Rect = rect;
            m_FaultEventList = faultList;
            for (int i = 0; i < 6; i++)
            {
                m_EventDisplayList.Add(new EventDisplay(new Rectangle(m_Rect.X, m_Rect.Y + m_Rect.Height / 6 * i, m_Rect.Width, m_Rect.Height / 6), null, -1, null, null));
            }
            m_EventDisplayList[0].IsSelect = true;
            m_CurrentEventDisplay = new EventDisplay(new Rectangle(m_Rect.X, m_Rect.Y, m_Rect.Width, m_Rect.Height / 6), null, -1, null, null);
            m_CurrentEventRect = new Rectangle(m_Rect.X, m_Rect.Y + m_Rect.Height / 6, m_Rect.Width, m_Rect.Height / 6);
        }

        private FaultEvent m_CurrentEvent;

        public bool IsDisplayEventInfo
        {
            get
            {
                return m_IsDisplayEventInfo;
            }
            set
            {
                m_IsDisplayEventInfo = value;
                if (!m_IsDisplayEventInfo)
                {
                    return;
                }
                if ((m_CurrentPage - 1) * 6 + m_CurrentIndex >= m_FaultEventList.Count)
                {
                    return;
                }
                m_CurrentEvent = m_FaultEventList[(m_CurrentPage - 1) * 6 + m_CurrentIndex];
                m_CurrentEventDisplay.LogicNumber = m_CurrentEvent.Number;
                m_CurrentEventDisplay.Display = m_CurrentEvent.Display;
                m_CurrentEventDisplay.Train = m_CurrentEvent.Train;
                m_CurrentEventDisplay.Image = BasicClass.m_EventImageArray[(int)m_FaultEventList[(m_CurrentPage - 1) * 6 + m_CurrentIndex].EventFaultLevel - 1];
            }
        }
        private bool m_IsDisplayEventInfo;

        public int CurrentPage
        {
            get
            {
                return m_CurrentPage;
            }
        }
        private int m_CurrentPage = 1;
        public int CurrentIndex
        {
            get
            {
                return m_CurrentIndex;
            }
            set
            {
                m_EventDisplayList[m_CurrentIndex].IsSelect = false;
                m_CurrentIndex = value;

                if (m_CurrentPage == m_MaxPage)
                {
                    m_CurrentIndex = m_CurrentIndex >= m_FaultEventList.Count - (m_MaxPage - 1) * 6 - 1 ? m_FaultEventList.Count - (m_MaxPage - 1) * 6 - 1 : m_CurrentIndex;
                }
                else
                    if (m_CurrentIndex >= 6)
                    {
                        m_CurrentIndex = 0;
                        m_CurrentPage = m_CurrentPage + 1 >= m_MaxPage ? m_MaxPage : m_CurrentPage + 1;
                    }

                if (m_CurrentPage == 1)
                {
                    m_CurrentIndex = m_CurrentIndex <= 0 ? 0 : m_CurrentIndex;
                }
                else
                    if (m_CurrentIndex <= -1)
                    {
                        m_CurrentIndex = 5;
                        m_CurrentPage = m_CurrentPage - 1 <= 1 ? 1 : m_CurrentPage - 1;
                    }
                m_EventDisplayList[m_CurrentIndex].IsSelect = true;
            }
        }
        private int m_CurrentIndex;

        public int MaxPage
        {
            get
            {
                return m_MaxPage;
            }
            private set
            {
                m_MaxPage = value;
                if (MaxPageChangedEvent != null)
                    MaxPageChangedEvent(m_MaxPage);
            }
        }
        private int m_MaxPage = 1;

        private void UpdateEvent()
        {
            for (int i = 0; i < 6; i++)
            {
                m_EventDisplayList[i].Clear();
                if ((m_CurrentPage - 1) * 6 + i >= m_FaultEventList.Count)
                {
                    continue;
                }
                m_EventDisplayList[i].LogicNumber = m_FaultEventList[(m_CurrentPage - 1) * 6 + i].Number;
                m_EventDisplayList[i].Display = m_FaultEventList[(m_CurrentPage - 1) * 6 + i].Display;
                m_EventDisplayList[i].Train = m_FaultEventList[(m_CurrentPage - 1) * 6 + i].Train;
                m_EventDisplayList[i].Image = BasicClass.m_EventImageArray[(int)m_FaultEventList[(m_CurrentPage - 1) * 6 + i].EventFaultLevel - 1];
            }
            MaxPage = m_FaultEventList.Count / 6 + 1;
        }







        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common.m_BlackBrush, m_Rect);

            UpdateEvent();

            if (!m_IsDisplayEventInfo)
            {
                foreach (var v in m_EventDisplayList)
                {
                    v.OnDraw(g);
                }
            }
            else
            {
                if (m_CurrentEvent == null || m_CurrentEventDisplay == null || m_FaultEventList.Count == 0)
                {
                    return;
                }
                m_CurrentEventDisplay.IsSelect = true;
                m_CurrentEventDisplay.OnDraw(g);
                g.DrawString(m_CurrentEvent.HelpDisplay, Common.m_16Font, Common.m_WhiteBrush, m_CurrentEventRect, new StringFormat() { Alignment = StringAlignment.Near });
            }
        }
    }

    class EventDisplay
    {
        public Image Image
        {
            get
            {
                return m_Image;
            }
            set
            {
                m_Image = value;
            }
        }
        private Image m_Image;
        private Rectangle m_Rect;
        public int LogicNumber
        {
            get
            {
                return m_LogicNumber;
            }
            set
            {
                m_LogicNumber = value;
            }
        }
        private int m_LogicNumber;

        public string Display
        {
            get
            {
                return m_Display;
            }
            set
            {
                m_Display = value;
            }
        }
        private string m_Display;

        public string Train
        {
            get
            {
                return m_Train;
            }
            set
            {
                m_Train = value;
            }
        }
        private string m_Train;

        private Brush m_Brush;


        public bool IsSelect
        {
            get
            {
                return m_IsSelect;
            }
            set
            {
                m_IsSelect = value;
            }
        }
        private bool m_IsSelect;


        public EventDisplay(Rectangle rect, Image image, int logicNumber, string display, string train)
        {
            m_Image = image;
            m_Rect = rect;
            m_LogicNumber = logicNumber;
            m_Display = display;
            m_Train = train;
        }


        public void Clear()
        {
            m_Image = null;
            m_LogicNumber = -1;
            m_Display = null;
            m_Train = null;
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common.m_BlackBrush, m_Rect);

            if (m_Image != null)
            {
                g.DrawImage(m_Image, new Rectangle(m_Rect.X, m_Rect.Y, 30, 30));
            }

            m_Brush = m_IsSelect ? Common.m_BlackBrush : Common.m_WhiteBrush;

            if (m_LogicNumber != -1)
            {
                if (m_IsSelect)
                    g.FillRectangle(Common.m_WhiteBrush, new Rectangle(m_Rect.X + 35, m_Rect.Y, m_Rect.Width - 35, m_Rect.Height));
                g.DrawString(m_LogicNumber.ToString(), Common.m_16Font, m_Brush, new Point(m_Rect.X + 35, m_Rect.Y));

            }
            if (m_Display != null)
                g.DrawString(m_Display, Common.m_16Font, m_Brush, new Point(m_Rect.X + 70, m_Rect.Y));
            if (m_Train != null)
                g.DrawString(m_Train, Common.m_16Font, m_Brush, new Point(500, m_Rect.Y));
        }
    }


    public enum EventLevel
    {
        无 = 0,
        轻微,
        中级,
        严重
    }

    class FaultEvent
    {
        public int LogicNumber { get; private set; }

        public string Level { get; private set; }


        public string Train { get; private set; }

        public int Number { get; private set; }

        public string Display { get; private set; }

        public string HelpDisplay { get; private set; }

        public bool Affirm { get; set; }

        public EventLevel EventFaultLevel
        {
            get
            {
                return (EventLevel)Enum.Parse(typeof(EventLevel), Level);
            }
        }

        public FaultEvent(int logiNumber, string level, string train, int number, string display, string helpDisplay)
        {
            LogicNumber = logiNumber;
            Level = level;
            Train = train;
            Number = number;
            Display = display;
            HelpDisplay = helpDisplay;
            Affirm = false;
        }
    }
}
