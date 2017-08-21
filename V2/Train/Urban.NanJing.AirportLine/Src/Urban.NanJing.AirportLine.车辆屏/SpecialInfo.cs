using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class SpecialInfo : baseClass
    {
        private Image[] m_ImageArray;
        private SpecialInfoView m_SpecialInfoView;
        private List<string> m_StringList;


        private Button m_ReportOnce;
        private Button m_ReportCircle;
        private RectText m_Fangda;

        private bool m_IsFangda;

        private RectText m_FangDaView;
        private Button m_OkButton;


        public override string GetInfo()
        {
            return "特殊信息";
        }

        private List<string> GetSpecialInfo()
        {
            string filePath = Path.Combine(RecPath, @"..\config\特殊信息.txt");
            List<string> lst = File.ReadAllLines(filePath, Encoding.Default).Skip(1).Where(source => source.Trim().Length > 0).ToList();

            return lst;
        }
        public override bool init(ref int nErrorObjectIndex)
        {

            m_ImageArray = new Image[UIObj.ParaList.Count];
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }

            m_StringList = GetSpecialInfo();
            m_SpecialInfoView = new SpecialInfoView(m_StringList)
                               {
                                   m_Page =
                                       new RectText(new Rectangle(580, 145, 50, 30),
                                       "",
                                       14,
                                       1,
                                       Color.White,
                                       Color.Black,
                                       Color.White,
                                       1,
                                       false),
                                   m_UpDirection = new Button(new Rectangle(520, 190, 50, 50), null, m_ImageArray[0]),
                                   m_DownDirection = new Button(new Rectangle(520, 250, 50, 50), null, m_ImageArray[1])
                               };

            m_SpecialInfoView.m_UpDirection.MouseUpEvent += (i =>
            {
                m_SpecialInfoView.CurrentPage = m_SpecialInfoView.CurrentPage - 1 >= 1 ? m_SpecialInfoView.CurrentPage - 1 : 1;
            });

            m_SpecialInfoView.m_DownDirection.MouseUpEvent += (i =>
            {
                m_SpecialInfoView.CurrentPage = m_SpecialInfoView.CurrentPage + 1 <= m_StringList.Count / 7 + 1 ? m_SpecialInfoView.CurrentPage + 1 : m_StringList.Count / 7 + 1;
            });


            m_ReportOnce = new Button(new Rectangle(100, 505, 80, 50), null, m_ImageArray[2]);
            m_ReportCircle = new Button(new Rectangle(240, 505, 80, 50), null, m_ImageArray[3]);
            m_Fangda = new RectText(new Rectangle(360, 505, 80, 50), "", 14, 1, Color.Black, Common.m_BackGroundColor, Color.White, 1, false, m_ImageArray[4], true);
            m_Fangda.MouseDownEvent += (item =>
            {
                m_Fangda.ImagePicture = m_ImageArray[5];
                m_IsFangda = true;
                int index = (from v in m_SpecialInfoView.m_StationList where v.IsSelect select m_SpecialInfoView.m_StationList.IndexOf(v)).FirstOrDefault();
                if (m_SpecialInfoView.m_StationList.Count==0)
                {
                    m_IsFangda = false;
                    m_Fangda.ImagePicture = m_ImageArray[4];
                    return;
                }
                m_FangDaView.Text = m_StringList[(m_SpecialInfoView.CurrentPage - 1) * 7 + index];
            });

            m_FangDaView = new RectText(new Rectangle(80, 190, 400, 260), "", 14, 0, Color.Black, Color.White, Color.Black, 1, true, null, true) { AlignmentTextFormat = 0, LineAlignmentTextFormat = 0 };
            m_OkButton = new Button(new Rectangle(240, 390, 80, 50), null, m_ImageArray[6]);
            m_OkButton.MouseUpEvent += (item =>
            {
                m_IsFangda = false;
                m_Fangda.ImagePicture = m_ImageArray[4];
            });
            return true;
        }



        public override void paint(Graphics g)
        {
            g.DrawString("特殊信息                                                                             页", Common.m_14Font, Common.m_BlueBrush, new Point(60, 150));

            g.DrawString("特殊信息", Common.m_14Font, Common.m_BlueBrush, new Point(60, 390));

            m_SpecialInfoView.OnDraw(g);

            g.FillRectangle(Common.m_BlackBrush, new Rectangle(60, 420, 450, 80));


            m_ReportOnce.OnDraw(g);
            m_ReportCircle.OnDraw(g);
            m_Fangda.OnDraw(g);




            if (m_IsFangda)
            {
                m_FangDaView.OnDraw(g);
                m_OkButton.OnDraw(g);
            }
        }

        public override bool mouseUp(Point point)
        {
            if (!m_IsFangda)
            {

                m_Fangda.OnMouseUp(point);
                m_SpecialInfoView.MouseUp(point);
            }
            else
            {
                m_OkButton.MouseUp(point);
            }

            return true;
        }
    }

    class SpecialInfoDescribe
    {
        public SpecialInfoDescribe(Rectangle rect, string describe)
        {
            m_Rect = rect;
            m_Describe = describe;

            m_BackgroundBrush = Common.m_BlackBrush;
            m_PenBrush = Common.m_WhiteBrush;
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(m_BackgroundBrush, m_Rect);
            g.DrawString(m_Describe, Common.m_14Font, m_PenBrush, m_Rect, new StringFormat { FormatFlags = StringFormatFlags.NoWrap });

        }

        public void OnMouseUp(Point point)
        {
            if (m_Rect.Contains(point))
            {
                m_BackgroundBrush = Common.m_WhiteBrush;
                m_PenBrush = Common.m_BlackBrush;
                m_IsSelect = true;
            }
        }

        public void Reset()
        {
            m_BackgroundBrush = Common.m_BlackBrush;
            m_PenBrush = Common.m_WhiteBrush;
            m_IsSelect = false;
        }

        public bool IsSelect
        {
            get
            {
                return m_IsSelect;
            }
        }
        private bool m_IsSelect;

        private Rectangle m_Rect;

        public string Describe
        {
            get
            {
                return m_Describe;
            }
            set
            {
                m_Describe = value;
            }
        }
        private string m_Describe;

        private Brush m_BackgroundBrush;
        private Brush m_PenBrush;
    }





    class SpecialInfoView
    {
        public int CurrentPage
        {
            set
            {
                if (m_CurrentPage != value)
                {
                    m_CurrentPage = value;

                    //for (int i = (_currentPage - 1) * 7; i < (_currentPage) * 7; i++)
                    //{
                    //    StationList[i - (_currentPage - 1) * 7].Describe = "";
                    //}

                    m_StationList.Clear();

                    for (int i = (m_CurrentPage - 1) * 7; i < (m_CurrentPage) * 7 && i < m_StationStringList.Count; i++)
                    {
                        m_StationList.Add(new SpecialInfoDescribe(new Rectangle(60, 180 + 30 * (i - (m_CurrentPage - 1) * 7), 450, 30), m_StationStringList[i]));
                        m_StationList[i - (m_CurrentPage - 1) * 7].Describe = m_StationStringList[i];
                    }
                }
            }
            get
            {
                return m_CurrentPage;
            }
        }
        private int m_CurrentPage = 1;

        public List<SpecialInfoDescribe> m_StationList = new List<SpecialInfoDescribe>();

        public RectText m_Page;

        private readonly List<string> m_StationStringList;

        public Button m_UpDirection;
        public Button m_DownDirection;




        public SpecialInfoView(List<string> stationStringList)
        {
            m_StationStringList = stationStringList;

            for (int i = 0; i < 7 && i < m_StationStringList.Count; i++)
            {
                m_StationList.Add(new SpecialInfoDescribe(new Rectangle(60, 180 + 30 * i, 450, 30), m_StationStringList[i]));
            }

        }

        public void OnDraw(Graphics g)
        {

            g.FillRectangle(Common.m_BlackBrush, new Rectangle(60, 180, 450, 210));

            m_Page.Text = m_CurrentPage + "/" + ((m_StationStringList.Count) / 7 + 1);
            m_Page.OnDraw(g);

            m_UpDirection.OnDraw(g);
            m_DownDirection.OnDraw(g);

            foreach (var v in m_StationList)
            {
                v.OnDraw(g);
            }


        }

        public void MouseUp(Point p)
        {

            m_UpDirection.MouseUp(p);
            m_DownDirection.MouseUp(p);

            foreach (var v in m_StationList)
            {
                v.Reset();
                v.OnMouseUp(p);
            }
        }

    }
}
