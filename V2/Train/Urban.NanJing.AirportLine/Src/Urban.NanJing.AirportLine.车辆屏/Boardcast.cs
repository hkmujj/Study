using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public class BoardCast : baseClass
    {
        private Image[] m_ImageArray;

        private BoardCastRect m_StartStation;
        private BoardCastRect m_Destination;
        private BoardCastRect m_Model;

        private Button m_Manual1;
        private Button m_HalfAuto1;

        private Button m_AUTO2;
        private Button m_HalfAuto2;

        private Button m_Manual3;
        private Button m_AUTO3;


        private Button m_Up;
        private Button m_Down;

        private Button m_Cancel;
        private Button m_Cross;



        private Button m_Start;
        private Button m_Destion;

        private Button m_Go;
        private Button m_NextStation;

        private Button m_Exit;
        private Button m_Validation;

        private RectText m_DirectionRect;


        private List<string> m_StationStringList = new List<string>();
        private List<float> m_StationStringListNum = new List<float>();

        private SkipStation m_SkipStation1;

        private SkipStation m_SkipStation2;



        private Rectangle m_SkipRect;
        private RectText m_SkipStringRect;

        private bool m_IsCrossSelect = false;

        private bool m_IsManual = false;
        private bool m_IsAuto = true;
        private bool m_IsHalfAuto = false;
        private List<Button> m_ButtonList = new List<Button>();

        private bool m_IsUpGreen = false;
        private bool m_IsDownGreen = false;
        public static Direction m_Direction;
        /// <summary>
        /// 上下行
        /// </summary>
        public enum Direction
        {
            Up,
            Down,
        }
        /// <summary>
        /// 广播模式枚举
        /// </summary>
        public enum Model
        {
            Auto,
            HalfAuto,
            Manual
        }
        /// <summary>
        /// 发送模式数据
        /// Add date 2015/06/03
        /// Add user tandw
        /// </summary>
        /// <param name="model">模式</param>
        private void SendValus(Model model)
        {
            //发送模式Bool值
            Title.m_Model = model;
            switch (model)
            {
                case Model.Auto:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    break;
                case Model.HalfAuto:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    break;
                case Model.Manual:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    break;
            }
        }

        private void InitialButton()
        {
            m_Manual1 = new Button(new Rectangle(440, 180, 100, 60), m_ImageArray[17], m_ImageArray[0], 0);
            m_Manual1.MouseUpEvent += ((i) =>
            {
                SendValus(Model.Manual);
                m_Model.m_Value.Text = "手动";
                m_IsManual = true;
                m_IsAuto = false;
                m_IsHalfAuto = false;
                m_ButtonList.Clear();
                m_ButtonList.Add(m_AUTO2);
                m_ButtonList.Add(m_HalfAuto2);
            });

            m_HalfAuto1 = new Button(new Rectangle(550, 180, 100, 60), m_ImageArray[17], m_ImageArray[15], 0);
            m_HalfAuto1.MouseUpEvent += ((i) =>
            {
                SendValus(Model.HalfAuto);
                m_Model.m_Value.Text = "半自动";
                m_IsManual = false;
                m_IsAuto = false;
                m_IsHalfAuto = true;
                m_ButtonList.Clear();
                m_ButtonList.Add(m_Manual3);
                m_ButtonList.Add(m_AUTO3);
            });

            m_AUTO2 = new Button(new Rectangle(440, 180, 100, 60), m_ImageArray[17], m_ImageArray[1], 0);
            m_AUTO2.MouseUpEvent += ((i) =>
            {
                SendValus(Model.Auto);
                m_Model.m_Value.Text = "自动";
                m_IsManual = false;
                m_IsAuto = true;
                m_IsHalfAuto = false;

                m_ButtonList.Clear();
                m_ButtonList.Add(m_Manual1);
                m_ButtonList.Add(m_HalfAuto1);
            });

            m_HalfAuto2 = new Button(new Rectangle(550, 180, 100, 60), m_ImageArray[17], m_ImageArray[15], 0);
            m_HalfAuto2.MouseUpEvent += ((i) =>
            {
                SendValus(Model.HalfAuto);
                m_Model.m_Value.Text = "半自动";
                m_IsManual = false;
                m_IsAuto = false;
                m_IsHalfAuto = true;
                m_ButtonList.Clear();
                m_ButtonList.Add(m_Manual3);
                m_ButtonList.Add(m_AUTO3);
            });

            m_Manual3 = new Button(new Rectangle(440, 180, 100, 60), m_ImageArray[17], m_ImageArray[0], 0);
            m_Manual3.MouseUpEvent += ((i) =>
            {
                SendValus(Model.Manual);
                m_Model.m_Value.Text = "手动";
                m_IsManual = true;
                m_IsAuto = false;
                m_IsHalfAuto = false;
                m_ButtonList.Clear();
                m_ButtonList.Add(m_AUTO2);
                m_ButtonList.Add(m_HalfAuto2);
            });

            m_AUTO3 = new Button(new Rectangle(550, 180, 100, 60), m_ImageArray[17], m_ImageArray[1], 0);
            m_AUTO3.MouseUpEvent += ((i) =>
            {
                SendValus(Model.Auto);
                m_Model.m_Value.Text = "自动";
                m_IsManual = false;
                m_IsAuto = true;
                m_IsHalfAuto = false;

                m_ButtonList.Clear();
                m_ButtonList.Add(m_Manual1);
                m_ButtonList.Add(m_HalfAuto1);
            });

            m_ButtonList.Clear();
            m_ButtonList.Add(m_Manual1);
            m_ButtonList.Add(m_HalfAuto1);
            SendValus(Model.Auto);
            m_Up = new Button(new Rectangle(25, 300, 80, 40), m_ImageArray[13], m_ImageArray[2], 0);
            m_Up.MouseUpEvent += ((i) =>
            {
                m_IsUpGreen = true;
                m_IsDownGreen = false;
                m_DirectionRect.Text = "→" + m_StationStringList[m_StationStringList.Count - 1].ToString();
                m_SkipStation1.StationStringList = m_StationStringList;
                m_Direction = Direction.Up;
            });

            m_Down = new Button(new Rectangle(110, 300, 80, 40), null, m_ImageArray[3], 0);
            m_Down.MouseUpEvent += ((i) =>
            {
                m_IsUpGreen = false;
                m_IsDownGreen = true;
                m_DirectionRect.Text = "→" + m_StationStringList[0].ToString();

                m_SkipStation1.StationStringList = OrderByList(m_StationStringList);
                m_Direction = Direction.Down;
            });

            m_Cancel = new Button(new Rectangle(25, 380, 120, 50), m_ImageArray[17], m_ImageArray[4], 0);
            m_Cancel.MouseUpEvent += i =>
            {
                m_IsCrossSelect = false;
                m_StartStation.m_Value.Text = string.Empty;
                m_Destination.m_Value.Text = string.Empty;
            };
            m_Cross = new Button(new Rectangle(25, 480, 120, 50), m_ImageArray[17], m_ImageArray[5], 0);
            m_Cross.MouseUpEvent += ((i) =>
            {
                m_IsCrossSelect = true;
            });


            m_Start = new Button(new Rectangle(580, 400, 100, 50), m_ImageArray[17], m_ImageArray[8], 0);
            m_Start.MouseUpEvent += ((i) =>
            {
                foreach (var v in m_SkipStation1.m_StationList.Where(v => v.IsSelect))
                {
                    m_StartStation.m_Value.Text = v.Describe;
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[1], 0, m_StationStringListNum[m_StationStringList.IndexOf(v.Describe)]);
                    return;
                }
            });

            m_Destion = new Button(new Rectangle(580, 460, 100, 50), m_ImageArray[17], m_ImageArray[9], 0);
            m_Destion.MouseUpEvent += ((i) =>
            {
                foreach (var v in m_SkipStation1.m_StationList.Where(v => v.IsSelect))
                {
                    m_Destination.m_Value.Text = v.Describe;
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, m_StationStringListNum[m_StationStringList.IndexOf(v.Describe)]);
                    return;
                }
            });

            m_Go = new Button(new Rectangle(430, 505, 100, 50), m_ImageArray[17], m_ImageArray[10], 0);
            m_Go.MouseUpEvent += (i =>
            {
                if (m_IsHalfAuto)//出发
                {

                }

            });
            m_NextStation = new Button(new Rectangle(430, 505, 100, 50), m_ImageArray[17], m_ImageArray[16], 0);
            m_NextStation.MouseUpEvent += (i =>
            {
                if (!m_IsManual)
                {
                    return;
                }
                foreach (var v in m_SkipStation1.m_StationList.Where(v => v.IsSelect))
                {
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, m_StationStringListNum[m_StationStringList.IndexOf(v.Describe)]);
                    return;
                }
            });
            m_DirectionRect = new RectText(new Rectangle(195, 300, 180, 40), "", 12, 0, Color.Black, Color.White, Color.Black, 1, true, null, true);

            m_Exit = new Button(new Rectangle(510, 310, 100, 70), m_ImageArray[17], m_ImageArray[11], 0);
            m_Exit.MouseUpEvent += ((i) =>
            {
                m_IsCrossSelect = false;
            });
            m_Validation = new Button(new Rectangle(450, 420, 120, 60), m_ImageArray[17], m_ImageArray[12], 0);
            m_Validation.MouseUpEvent += ((i) =>
            {
                append_postCmd(CmdType.SetFloatValue, 13, 0, m_SkipStation2.m_StartSkipIndex);
                append_postCmd(CmdType.SetFloatValue, 14, 0, m_SkipStation2.m_EndSkipIndex);
            });

        }

        private List<string> OrderByList(IList<string> lst)
        {
            List<string> tmp = new List<string>();
            for (int i = lst.Count - 1; i >= 0; i--)
            {
                tmp.Add(lst[i]);
            }
            return tmp;
        }

        private void InitialStation()
        {
            if (m_SkipStation1.m_StationList.Count == 0)
            {
                for (int i = 0; i < 7 && i < m_StationStringList.Count; i++)
                {
                    m_SkipStation1.m_StationList.Add(new Station(new Rectangle(380, 280 + 32 * i, 190, 32), m_SkipStation1.StationStringList[i]));
                }

                m_DirectionRect.Text = "→" + m_StationStringList[m_StationStringList.Count - 1].ToString();
            }

            if (m_SkipStation2.m_StationList.Count == 0)
            {
                for (int i = 0; i < 7 && i < m_StationStringList.Count; i++)
                {
                    m_SkipStation2.m_StationList.Add(new Station(new Rectangle(240, 280 + 32 * i, 190, 32), m_StationStringList[i]));
                }
            }

        }

        private void PaintString(Graphics g)
        {
            if (!m_IsAuto)
            {
                g.DrawString("取消所选车站", Common.m_14Font, Common.m_BlueBrush, new Point(25, 350));
                if (m_IsHalfAuto)
                {
                    g.DrawString("越战功能", Common.m_14Font, Common.m_BlueBrush, new Point(25, 440));
                }
            }
            g.DrawString("车站选择                           页", Common.m_14Font, Common.m_BlueBrush, new Point(380, 250));
        }


        private void InitSkipStation()
        {


            m_SkipStation1 = new SkipStation(m_StationStringList);
            m_SkipStation1.m_Page = new RectText(new Rectangle(640, 250, 50, 30), "", 14, 1, Color.White, Color.Black, Color.White, 1, false);

            m_SkipStation1.m_UpDirection = new Button(new Rectangle(580, 280, 50, 50), null, m_ImageArray[6], 0);
            m_SkipStation1.m_DownDirection = new Button(new Rectangle(580, 340, 50, 50), null, m_ImageArray[7], 0);


            m_SkipStation1.m_UpDirection.MouseUpEvent += ((i) =>
            {
                m_SkipStation1.CurrentPage = m_SkipStation1.CurrentPage - 1 >= 1 ? m_SkipStation1.CurrentPage - 1 : 1;
            });




            m_SkipStation1.m_DownDirection.MouseUpEvent += ((i) =>
            {
                m_SkipStation1.CurrentPage = m_SkipStation1.CurrentPage + 1 <= m_StationStringList.Count / 7 + 1 ? m_SkipStation1.CurrentPage + 1 : m_StationStringList.Count / 7;
            });



            m_SkipStation2 = new SkipStation(m_StationStringList);
            m_SkipStation2.m_Page = new RectText(new Rectangle(540, 250, 50, 30), "", 14, 1, Color.White, Color.Black, Color.White, 1, false);
            m_SkipStation2.m_UpDirection = new Button(new Rectangle(440, 280, 50, 50), null, m_ImageArray[6], 0);
            m_SkipStation2.m_DownDirection = new Button(new Rectangle(440, 340, 50, 50), null, m_ImageArray[7], 0);

            m_SkipStation2.m_UpDirection.MouseUpEvent += ((i) =>
            {
                m_SkipStation2.CurrentPage = m_SkipStation2.CurrentPage - 1 >= 1 ? m_SkipStation2.CurrentPage - 1 : 1;
            });



            m_SkipStation2.m_DownDirection.MouseUpEvent += ((i) =>
            {
                m_SkipStation2.CurrentPage = m_SkipStation2.CurrentPage + 1 <= m_StationStringList.Count / 7 + 1 ? m_SkipStation2.CurrentPage + 1 : m_StationStringList.Count / 7;
            });
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadStationInfo();

            m_ImageArray = new Image[UIObj.ParaList.Count];
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }

            m_StartStation = new BoardCastRect(new RectText(new Rectangle(25, 175, 160, 35), "起始站", 14, 0, Color.Blue, Common.m_BackGroundColor, Color.Blue, 1, true, null, true), new RectText(new Rectangle(185, 175, 195, 35), "", 14, 1, Color.Black, Color.White, Color.Blue, 1, true, null, true));
            m_Destination = new BoardCastRect(new RectText(new Rectangle(25, 215, 160, 35), "目的地", 14, 0, Color.Blue, Common.m_BackGroundColor, Color.Blue, 1, true, null, true), new RectText(new Rectangle(185, 215, 195, 35), "", 14, 1, Color.Black, Color.White, Color.Blue, 1, true, null, true));
            m_Model = new BoardCastRect(new RectText(new Rectangle(390, 135, 140, 35), "模式", 14, 0, Color.Blue, Common.m_BackGroundColor, Color.Blue, 1, true, null, true), new RectText(new Rectangle(530, 135, 155, 35), "自动", 14, 1, Color.Black, Color.White, Color.Blue, 1, true, null, true));


            InitialButton();

            InitSkipStation();

            return true;
        }

        private void LoadStationInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "车站信息.txt");
            var all = File.ReadLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] strStation = cStr.Split(new[] { '_' });
                if (strStation.Length == 2)
                {
                    m_StationStringList.Add(strStation[0].Trim());
                    m_StationStringListNum.Add(float.Parse(strStation[1].Trim()));
                }
            }
        }

        public override string GetInfo()
        {
            return "广播";
        }

        private void PaintButton(Graphics g)
        {

            m_Up.OnDraw(g);
            m_Down.OnDraw(g);

            if (m_IsUpGreen)
            {
                g.DrawImage(m_ImageArray[13], new Rectangle(25, 300, 80, 40));
            }

            if (m_IsDownGreen)
            {
                g.DrawImage(m_ImageArray[14], new Rectangle(110, 300, 80, 40));
            }
            m_Start.OnDraw(g);
            m_Destion.OnDraw(g);
            if (m_IsManual)
            {
                m_NextStation.OnDraw(g);
                m_Cancel.OnDraw(g);
            }
            if (m_IsHalfAuto)
            {
                m_Cancel.OnDraw(g);
                m_Cross.OnDraw(g);
                m_Go.OnDraw(g);
            }
        }



        public override void paint(Graphics g)
        {
            InitialStation();

            foreach (var v in m_ButtonList)
            {
                v.OnDraw(g);
            }

            m_StartStation.OnDraw(g);
            m_Destination.OnDraw(g);
            m_Model.OnDraw(g);



            if (!m_IsAuto)
            {
                m_DirectionRect.OnDraw(g);

                m_SkipStation1.OnDraw(g);

                PaintButton(g);

                PaintString(g);


                if (m_IsCrossSelect)
                {
                    m_SkipRect = new Rectangle(230, 220, 400, 290);
                    g.FillRectangle(Common.m_BackgroundBrush, m_SkipRect);
                    g.DrawRectangle(new Pen(Color.Gray, 4), m_SkipRect);

                    m_SkipStation2.OnDraw(g);
                    m_Exit.OnDraw(g);
                    m_Validation.OnDraw(g);
                    m_SkipStringRect = new RectText(new Rectangle(310, 230, 160, 40), "越站", 14, 1, Color.Blue, Common.m_BackGroundColor, Color.Black, 1, true, null, true);
                    m_SkipStringRect.OnDraw(g);
                    g.DrawString("Page", Common.m_14Font, Common.m_BlueBrush, new Point(490, 250));
                }
            }

        }
        /// <summary>
        /// 重写按钮按下事件
        /// </summary>
        /// <param name="point">按下的坐标点</param>
        /// <returns></returns>
        public override bool mouseDown(Point point)
        {
            foreach (Button t in m_ButtonList)
            {
                t.MouseDown(point);
            }
            if (!m_IsAuto)
            {
                m_Start.MouseDown(point);
                m_Destion.MouseDown(point);

                m_Up.MouseDown(point);
                m_Down.MouseDown(point);
                if (m_IsHalfAuto)
                {
                    m_Go.MouseDown(point);
                    m_Cross.MouseDown(point);
                }
                else
                {
                    m_NextStation.MouseDown(point);

                }
                if (m_IsCrossSelect)
                {
                    m_Validation.MouseDown(point);
                }
                m_Exit.MouseDown(point);

                m_Cancel.MouseDown(point);
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {


            for (int i = 0; i < m_ButtonList.Count; i++)
            {
                m_ButtonList[i].MouseUp(point);
            }

            if (!m_IsAuto)
            {
                m_Start.MouseUp(point);
                m_Destion.MouseUp(point);

                m_Up.MouseUp(point);
                m_Down.MouseUp(point);
                if (m_IsHalfAuto)
                {
                    m_Go.MouseUp(point);
                    m_Cross.MouseUp(point);
                }
                else
                {
                    m_NextStation.MouseUp(point);

                }
                m_Exit.MouseUp(point);
                m_Cancel.MouseUp(point);

                if (m_IsCrossSelect)
                {

                    m_Validation.MouseUp(point);

                    m_SkipStation2.MouseUp(point);
                }
                else
                {
                    m_SkipStation1.MouseUp(point);
                }
            }

            return true;
        }
    }


    class BoardCastRect
    {
        public RectText m_Title;
        public RectText m_Value;

        public BoardCastRect(RectText title, RectText value)
        {
            m_Title = title;
            m_Value = value;
        }

        public void OnDraw(Graphics g)
        {
            m_Title.OnDraw(g);
            m_Value.OnDraw(g);
        }
    }

    class Station
    {
        public Station(Rectangle rect, string describe)
        {
            m_Rect = rect;
            m_Describe = describe;

            m_BackgroundBrush = Common.m_BlackBrush;
            m_PenBrush = Common.m_WhiteBrush;
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(m_BackgroundBrush, m_Rect);
            g.DrawString(m_Describe, Common.m_14Font, m_PenBrush, m_Rect);
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


    class SkipStation
    {
        public int CurrentPage
        {
            set
            {

                try
                {
                    m_CurrentPage = value;
                    for (int i = (m_CurrentPage - 1) * 7; i < (m_CurrentPage) * 7; i++)
                    {
                        m_StationList[i - (m_CurrentPage - 1) * 7].Describe = "";
                    }
                    for (int i = (m_CurrentPage - 1) * 7; i < (m_CurrentPage) * 7 && i < StationStringList.Count; i++)
                    {
                        m_StationList[i - (m_CurrentPage - 1) * 7].Describe = StationStringList[i];
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            get
            {
                return m_CurrentPage;
            }
        }
        private int m_CurrentPage = 1;

        public List<Station> m_StationList = new List<Station>();

        public RectText m_Page;

        public List<string> StationStringList
        {
            get { return m_StationStringList; }
            set
            {
                m_StationStringList = value;
                CurrentPage = 1;
            }
        }

        public Button m_UpDirection;
        public Button m_DownDirection;


        public int m_StartSkipIndex = 0;
        public int m_EndSkipIndex = 0;
        private List<string> m_StationStringList;

        public SkipStation(List<string> stationStringList)
        {
            StationStringList = new List<string>();
            StationStringList = stationStringList;
        }

        public void OnDraw(Graphics g)
        {
            m_Page.Text = m_CurrentPage.ToString() + "/" + (StationStringList.Count / 7 + 1).ToString();
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
                if (v.IsSelect)
                {
                    if (m_StartSkipIndex == 0)
                        m_StartSkipIndex = m_StationList.IndexOf(v);
                    if (m_StationList.IndexOf(v) > m_StartSkipIndex)
                    {
                        m_EndSkipIndex = m_StationList.IndexOf(v);
                    }
                }
            }
        }

    }
}
