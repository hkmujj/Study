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
    public class Info : baseClass
    {
        private Image[] m_ImageArray;
        private InfoRect m_Language;
        private InfoRect m_MileCount;
        private InfoRect m_Time;
        private InfoRect m_Tongji;
        private DateSetting m_Date;

        public override string GetInfo()
        {
            return "信息页面";
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

            m_Language = new InfoRect(new Rectangle(30 + Common.m_InitialPosX, 130 + Common.m_InitialPosY, 230, 170), "语言切换");
            m_MileCount = new InfoRect(new Rectangle(30 + Common.m_InitialPosX, 315 + Common.m_InitialPosY, 230, 60), "已行驶公里数");
            m_Time = new InfoRect(new Rectangle(30 + Common.m_InitialPosX, 390 + Common.m_InitialPosY, 230, 60), "操作时间");
            m_Tongji = new InfoRect(new Rectangle(30 + Common.m_InitialPosX, 465 + Common.m_InitialPosY, 230, 60), "能耗统计");

            m_Date = new DateSetting(m_ImageArray);
            return true;
        }

        private void GetValue()
        {
            m_MileCount.RectTextInfo.Text = FloatList[UIObj.InFloatList[0]].ToString() + " km";
            m_Time.RectTextInfo.Text = FloatList[UIObj.InFloatList[1]].ToString() + " min";

            if (BasicClass.m_StartTime != null)
            {
                TimeSpan passTime = DateTime.Now - BasicClass.m_StartTime;
                m_Time.RectTextInfo.Text = passTime.Hours + " h " + passTime.Minutes + " min";
            }
            m_Tongji.RectTextInfo.Text = FloatList[UIObj.InFloatList[2]].ToString() + " Kwh";

            if (BoolList[UIObj.InBoolList[0]])
            {
                m_Language.RectTextInfo.ImagePicture = m_ImageArray[0];
            }
            else
            {
                m_Language.RectTextInfo.ImagePicture = m_ImageArray[0];
            }
        }

        public override void paint(Graphics g)
        {
            GetValue();
            m_Language.OnDraw(g);
            m_MileCount.OnDraw(g);
            m_Time.OnDraw(g);
            m_Tongji.OnDraw(g);

            m_Date.OnDraw(g);
        }

        public override bool mouseDown(Point point)
        {
            m_Date.OnMouseDown(point);
            return true;
        }

        public override bool mouseUp(Point point)
        {
            m_Date.OnMouseUp(point);
            return true;
        }
    }

    public class InfoRect
    {
        public InfoRect(Rectangle rect, string describeInfo)
        {
            m_Rect = rect;
            m_DescribeString = describeInfo;

            m_Label = new RectText(new Rectangle(rect.X + 4, rect.Y + 4, rect.Width - 30, 20), describeInfo, 14, 0, Color.Blue, Common.m_BackGroundColor, Color.Blue, 1, false, null, true);
            m_RectTextInfo = new RectText(new Rectangle(rect.X + 4, rect.Y + 28, rect.Width - 30, rect.Height - 34), "", 14, 2, Color.Black, Color.White, Color.Black, 1, true);
        }

        private string m_DescribeString;

        private Rectangle m_Rect;

        private RectText m_Label;

        public RectText RectTextInfo
        {
            get
            {
                return m_RectTextInfo;
            }
        }
        private RectText m_RectTextInfo;

        public void OnDraw(Graphics g)
        {
            g.DrawRectangle(Common.m_BluePen, m_Rect);
            m_Label.OnDraw(g);
            m_RectTextInfo.OnDraw(g);


        }
    }

    class DateSetting
    {
        private List<RectText> m_RectList = new List<RectText>();

        private Rectangle m_Rect;

        private Button m_Left;
        private Button m_Right;

        private List<Button> m_ButtonList = new List<Button>();

        private Button m_InputButton;
        public DateSetting(Image[] imageArray)
        {
            m_Rect = new Rectangle(285, 130, 395, 370);
            m_Left = new Button(new Rectangle(m_Rect.X + 260, m_Rect.Y + 22, 40, 40), null, imageArray[2], -1);
            m_Left.MouseUpEvent += OnLeftButtonUpHandler;
            m_Right = new Button(new Rectangle(m_Rect.X + 320, m_Rect.Y + 22, 40, 40), null, imageArray[3], 1);
            m_Right.MouseUpEvent += OnRightButtonUpHandler;

            m_InputButton = new Button(new Rectangle(m_Rect.X + 90, m_Rect.Y + 300, 150, 70), null, imageArray[imageArray.Length - 1], 1);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (3 * i + j < 10)
                    {
                        Button temp = new Button(new Rectangle(m_Rect.X + 10 + 80 * j, m_Rect.Y + 60 + 80 * i, 70, 70), null, imageArray[4 + 3 * i + j], (i * 3 + j + 1) % 10);
                        temp.MouseUpEvent += OnNumberButtonUpHandler;
                        m_ButtonList.Add(temp);
                    }
                }
            }

            for (int i = 0; i < 14; i++)
            {
                if (i == 8)
                    m_RectList.Add(new RectText(new Rectangle(m_Rect.X + 10 + 220 / 14 * i, m_Rect.Y + 30, 220 / 14, 25), ":", 12, 1, Color.Black, Color.White, Color.Black, 1, false, null, true));
                else if (i == 11)
                    m_RectList.Add(new RectText(new Rectangle(m_Rect.X + 10 + 220 / 14 * i, m_Rect.Y + 30, 220 / 14, 25), "/", 12, 1, Color.Black, Color.White, Color.Black, 1, false, null, true));
                else
                    m_RectList.Add(new RectText(new Rectangle(m_Rect.X + 10 + 220 / 14 * i, m_Rect.Y + 30, 220 / 14, 25), "", 12, 1, Color.Black, Color.White, Color.Black, 1, false, null, true));
            }
        }

        public void OnMouseDown(Point p)
        {
            m_Left.MouseDown(p);
            m_Right.MouseDown(p);
            m_InputButton.MouseDown(p);

            foreach (var v in m_ButtonList)
            {
                v.MouseDown(p);
            }
        }

        public void OnMouseUp(Point p)
        {
            m_Left.MouseUp(p);
            m_Right.MouseUp(p);
            m_InputButton.MouseUp(p);

            foreach (var v in m_ButtonList)
            {
                v.MouseUp(p);
            }
        }



        private int m_SelectIndex = 0;
        private int SelectIndex
        {
            set
            {
                if (value <= 13 && value >= 0)
                {
                    if (value == 8 || value == 11)
                    {
                        m_RectList[m_SelectIndex].BackgroundColor = Color.White;
                        m_SelectIndex = m_SelectIndex > value ? value - 1 : value + 1;
                        m_RectList[m_SelectIndex].BackgroundColor = Color.Yellow;
                    }
                    else
                    {
                        m_RectList[m_SelectIndex].BackgroundColor = Color.White;
                        m_SelectIndex = value;
                        m_RectList[m_SelectIndex].BackgroundColor = Color.Yellow;
                    }
                }
            }
            get
            {
                return m_SelectIndex;
            }
        }
        private void OnLeftButtonUpHandler(int message)
        {
            SelectIndex = SelectIndex - 1;
        }

        private void OnRightButtonUpHandler(int message)
        {
            SelectIndex = SelectIndex + 1;
        }

        private void OnNumberButtonUpHandler(int message)
        {
            m_RectList[SelectIndex].Text = message.ToString();
        }

        public void OnDraw(Graphics g)
        {
            g.DrawRectangle(Common.m_BluePen, m_Rect);
            g.DrawString("改变日期和时间", Common.m_12Font, Common.m_BlueBrush, new PointF(m_Rect.X + 4, m_Rect.Y + 4));
            g.FillRectangle(Common.m_WhiteBrush, new Rectangle(m_Rect.X + 10, m_Rect.Y + 30, 220, 25));

            m_Left.OnDraw(g);
            m_Right.OnDraw(g);
            m_InputButton.OnDraw(g);
            foreach (var v in m_ButtonList)
            {
                v.OnDraw(g);
            }

            foreach (var v in m_RectList)
            {
                v.OnDraw(g);
            }
        }

    }




}
