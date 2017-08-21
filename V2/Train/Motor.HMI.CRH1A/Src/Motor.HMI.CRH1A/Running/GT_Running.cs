using System;
using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Running.DialStrategy;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A.Running
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_Running : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("运行");//菜单标题
        public Rectangle Recposition = new Rectangle(3, 185, 790, 260);
        public Rectangle Clockrect;
        public PointF Circle;//圆心位置
        public PointF[] Points = new PointF[60];
        public float Radius = 110.0F;//时钟半径
        public int Hour = 0;
        public int Minu = 0;
        public int Second = 0;
        public PointF[] PointH = new PointF[4];//时针位置
        public PointF[] PointM = new PointF[4];//分针位置
        public SolidBrush RecBrush = new SolidBrush(Color.FromArgb(119, 135, 155));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(70, 130, 180));
        public SolidBrush BlackBrush = new SolidBrush(Color.Black);
        public Pen Recpen = new Pen(Color.FromArgb(185, 199, 211), 3);
        public Pen BlackPen1 = new Pen(Color.Black, 2);
        public Pen BlackPen2 = new Pen(Color.Black, 3);
        public Font NoFont = new Font("Arial", 10);

        public SolidBrush ClockBrush = new SolidBrush(Color.FromArgb(192, 192, 192));
        public Pen Cdarkpen = new Pen(Color.FromArgb(92, 92, 92), 2);
        public Pen Clinepen = new Pen(Color.FromArgb(54, 56, 59), 2);
        public GDIRectText[] GText = new GDIRectText[2];//分别显示动力和温度

        public CRH1AButton[] GButton = new CRH1AButton[4];//底部按钮
        public float[] Valuef = new float[2];
        public Image Image;

        /// <summary>
        /// 制动等级
        /// </summary>
        private bool[] m_IsBrakeLevel = new bool[7];

        /// <summary>
        /// 表盘绘制策略
        /// </summary>
        private IDialStrategy m_DialStrategy;

        private IFloatValueExpression m_ValueExpression;

        public override string GetInfo()
        {
            return "运行状态";
        }

        public override bool Initialize()
        {
            //3
            m_ValueExpression = new TowBrakeValueExpression();

            m_DialStrategy = new NormalDialStrategy()
                             {
                                 DrawDarkAction = (g, i) => g.FillRectangle(WhiteBrush, Points[i].X, Points[i].Y, 6, 6),
                                 DrawLightAction = (g, i) => g.FillRectangle(BlueBrush, Points[i].X, Points[i].Y, 6, 6),
                             };

            if (UIObj.ParaList.Count > 0)
                // for(int i=0;i<3;i++)
                // {
                Image = Image.FromFile(RecPath + "//" + UIObj.ParaList[0]);
            // }
            InitData();
            return true;

        }

        public override void paint(Graphics g)
        {
            GetValue();
            ReFreshData();
            DrawOn(g);
        }

        public void GetValue()
        {
            if (UIObj.InFloatList.Count >= 2)
            {
                for (int i = 0; i < 2; i++)
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
            }

            DateTime date = CurrenTime;
            Hour = Convert.ToInt32(date.Hour);
            Minu = Convert.ToInt32(date.Minute);
            Second = Convert.ToInt32(date.Second);
            //时针位置
            PointH[0] = new PointF(Circle.X, Circle.Y);
            PointH[1] = new PointF(Circle.X + (float)((Radius - 85) * Math.Sin(Hour * 0.5235 - 0.35 + Minu * 0.00873)), Circle.Y - (float)((Radius - 85) * Math.Cos(Hour * 0.5235 - 0.35 + Minu * 0.00873)));
            PointH[2] = new PointF(Circle.X + (float)((Radius - 40) * Math.Sin(Hour * 0.5235 + Minu * 0.00873)), Circle.Y - (float)((Radius - 40) * Math.Cos(Hour * 0.5235 + Minu * 0.00873)));
            PointH[3] = new PointF(Circle.X + (float)((Radius - 85) * Math.Sin(Hour * 0.5235 + 0.35 + Minu * 0.00873)), Circle.Y - (float)((Radius - 85) * Math.Cos(Hour * 0.5235 + 0.35 + Minu * 0.00873)));
            //分针位置
            PointM[0] = new PointF(Circle.X, Circle.Y);
            PointM[1] = new PointF(Circle.X + (float)((Radius - 90) * Math.Sin(6 * Minu * 0.0175 - 0.25 + Second * 0.001745)), Circle.Y - (float)((Radius - 90) * Math.Cos(6 * Minu * 0.0175 - 0.25 + Second * 0.001745)));
            PointM[2] = new PointF(Circle.X + (float)((Radius - 15) * Math.Sin(6 * Minu * 0.0175 + Second * 0.001745)), Circle.Y - (float)((Radius - 15) * Math.Cos(6 * Minu * 0.0175 + Second * 0.001745)));
            PointM[3] = new PointF(Circle.X + (float)((Radius - 90) * Math.Sin(6 * Minu * 0.0175 + 0.25 + Second * 0.001745)), Circle.Y - (float)((Radius - 90) * Math.Cos(6 * Minu * 0.0175 + 0.25 + Second * 0.001745)));

            //获取制动等级
            for (int index = 0; index < 7; index++)
            {
                m_IsBrakeLevel[index] = BoolList[UIObj.InBoolList[index]];
            }
        }
        public void InitData()
        {
            //设置钟的显示位置
            Clockrect = new Rectangle(Recposition.X + 250, Recposition.Y + 10, 280, 240);
            Circle = new PointF(Clockrect.X + 130, Clockrect.Y + 120);
            for (int i = 0; i < 60; i++)
            {

                Points[i] = new PointF(Circle.X - 2 + (float)(Radius * Math.Sin(6 * i * 0.0175)), Circle.Y - (float)(Radius * Math.Cos(6 * i * 0.0175)));
            }

            //两边信息显示部分
            GText[0] = new GDIRectText();
            GText[0].SetBkColor(192, 192, 192);
            GText[0].SetTextColor(140, 140, 140);
            GText[0].SetTextStyle(14, FormatStyle.Center, true, "Arial");
            GText[0].SetTextRect(Recposition.X + 90, Recposition.Y + 125, 75, 35);


            GText[1] = new GDIRectText();
            GText[1].SetBkColor(192, 192, 192);
            GText[1].SetTextColor(140, 140, 140);
            GText[1].SetTextStyle(14, FormatStyle.Center, true, "Arial");
            GText[1].SetTextRect(Recposition.X + 620, Recposition.Y + 125, 75, 35);

            //底部按钮初始化
            for (int i = 0; i < 4; i++)
            {
                GButton[i] = new CRH1AButton();
                GButton[i].SetButtonColor(192, 192, 192);
                if (i == 2)
                {
                    GButton[i].SetButtonRect(Recposition.X + 627, Recposition.Y + 326, 80, 50);
                }
                else if (i == 3)
                {
                    GButton[i].SetButtonRect(Recposition.X + 710, Recposition.Y + 326, 80, 50);
                }
                else
                {
                    GButton[i].SetButtonRect(Recposition.X + 170 * i + 120, Recposition.Y + 326, 80, 50);
                }

            }
            GButton[0].SetButtonText("夜间运行");
            GButton[1].SetButtonText("列车状态");
            GButton[2].SetButtonText("主菜单");
            GButton[3].SetButtonText("车站");

        }
        public void ReFreshData()
        {
            //if (valuef[1] < 1000)
            //{
            //    G_Text[0].SetText(valuef[1].ToString());
            //}
            //else
            //{
            //    string str = Convert.ToInt32(valuef[1]).ToString();
            //    char[] strChar = str.ToCharArray();
            //    string textStr = string.Empty;
            //    for (int index = 1; index < strChar.Length; index++)
            //    {
            //        textStr += strChar[index].ToString();
            //    }
            //    G_Text[0].SetText(textStr);
            //}

            GText[1].SetText(Valuef[0].ToString());

            GText[0].SetText(m_ValueExpression.Interprete(Valuef[1]));

        }
        public void DrawOn(Graphics g)
        {
            //绘制菜单标题
            Title.OnDraw(g);
            //绘制背景框

            g.DrawRectangle(Recpen, Recposition);
            g.FillRectangle(RecBrush, Recposition);
            //绘制钟框
            g.FillRectangle(ClockBrush, Clockrect);
            g.DrawLine(Cdarkpen, Clockrect.X, Clockrect.Y, Clockrect.X + Clockrect.Width, Clockrect.Y);
            g.DrawLine(Cdarkpen, Clockrect.X, Clockrect.Y, Clockrect.X, Clockrect.Y + Clockrect.Height);
            g.DrawLine(Clinepen, Clockrect.X + Clockrect.Width, Clockrect.Y, Clockrect.X + Clockrect.Width, Clockrect.Y + Clockrect.Height);
            g.DrawLine(Clinepen, Clockrect.X, Clockrect.Y + Clockrect.Height, Clockrect.X + Clockrect.Width, Clockrect.Y + Clockrect.Height);
            g.DrawString("牵引/制动指令", new Font("Arial", 13), new SolidBrush(Color.FromArgb(255, 255, 255)), Recposition.X + 80, Recposition.Y + 100);
            GText[0].OnDraw(g);
            g.DrawString("外部空气温度", new Font("Arial", 13), new SolidBrush(Color.FromArgb(255, 255, 255)), Recposition.X + 600, Recposition.Y + 100);
            GText[1].OnDraw(g);
            //绘制底部按钮
            for (int i = 0; i < 4; i++)
            {
                GButton[i].OnDraw(g);
            }
            //绘制时钟
            m_DialStrategy.Display(g, Second);
            for (int i = 0; i < 12; i++)
            {
                if (i < 8 && i != 0 && i != 6)
                {
                    g.DrawLine(BlackPen1, Circle.X + (float)((Radius - 5) * Math.Sin(i * 0.5235)), Circle.Y - (float)((Radius - 5) * Math.Cos(i * 0.5235)), Circle.X +
                        (float)((Radius - 15) * Math.Sin(i * 0.5235)), Circle.Y - (float)((Radius - 15) * Math.Cos(i * 0.5235)));
                }
                else if (i >= 8)
                {
                    g.DrawLine(BlackPen1, Circle.X + (float)((Radius - 10) * Math.Sin(i * 0.5235)), Circle.Y - (float)((Radius - 10) * Math.Cos(i * 0.5235)), Circle.X +
                      (float)((Radius - 20) * Math.Sin(i * 0.5235)), Circle.Y - (float)((Radius - 20) * Math.Cos(i * 0.5235)));
                }
                else
                {
                    g.DrawLine(BlackPen2, Circle.X + (float)((Radius - 10) * Math.Sin(i * 0.5235)), Circle.Y - (float)((Radius - 10) * Math.Cos(i * 0.5235)), Circle.X +
                         (float)((Radius - 20) * Math.Sin(i * 0.5235)), Circle.Y - (float)((Radius - 20) * Math.Cos(i * 0.5235)));
                }
            }
            g.DrawString("1 2", NoFont, BlackBrush, Circle.X - 10, Circle.Y - Radius + 20);
            g.DrawString("3", NoFont, BlackBrush, Circle.X + (float)((Radius - 28) * Math.Sin(3 * 0.5235)), Circle.Y - 7 - (float)((Radius - 10) * Math.Cos(3 * 0.5235)));
            g.DrawString("6", NoFont, BlackBrush, Circle.X - 6, Circle.Y + Radius - 35);
            g.DrawString("9", NoFont, BlackBrush, Circle.X - 3 + (float)((Radius - 28) * Math.Sin(9 * 0.5235)), Circle.Y - 7 - (float)((Radius - 10) * Math.Cos(9 * 0.5235)));

            //绘制时针
            g.FillPolygon(BlackBrush, PointH);
            g.DrawLines(BlackPen1, PointH);

            g.FillPolygon(BlackBrush, PointM);
            g.DrawLines(BlackPen1, PointM);
        }
        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }
        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point.X, point.Y);
            return true;
        }

        public void OnButtonDown(int x, int y)
        {
            //  按 钮 响 应 事 件
            for (int i = 0; i < 4; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    GButton[i].OnButtonDown();
                }
            }
        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    switch (i)
                    {
                        case 0:
                            OnPost(CmdType.ChangePage, 33, 0, 0);
                            break;
                        case 1:
                            OnPost(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 2:
                            OnPost(CmdType.ChangePage, 1, 0, 0);
                            break;
                        case 3:
                            OnPost(CmdType.ChangePage, 5, 0, 0);
                            break;
                    }
                    GButton[i].OnButtonUp();

                }
            }

        }
    }
}
         
    
