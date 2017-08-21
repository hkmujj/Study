using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A
{
    public class KaiGuan//蓄电池开关
    {
        public Rectangle Rect;
        public bool Status = true;
        public Point[] Points = new Point[5];
        public Pen GreenPen = new Pen(Color.FromArgb(0, 128, 0), 2);
        public Pen LPen = new Pen(Color.FromArgb(170, 216, 167), 4);
        public Pen SPen = new Pen(Color.FromArgb(135, 210, 130), 2);
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public KaiGuan(int x, int y, int weight, int height)
        {
            Rect.X = x;
            Rect.Y = y;
            Rect.Width = weight;
            Rect.Height = height;
            Points[0] = new Point(Rect.X + Rect.Width / 2, Rect.Y);
            Points[1] = new Point(Rect.X + Rect.Width / 2, Rect.Y + 15);
            Points[2] = new Point(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height);
            Points[3] = new Point(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height - 8);
            Points[4] = new Point(Rect.X + Rect.Width / 2 + 10, Rect.Y + 15);
        }
        public void OnDraw(Graphics g)
        {
            if (Status)//Status为TRue时显示为开状态
            {

                g.DrawLine(GreenPen, Points[0], Points[1]);
                g.DrawLine(GreenPen, Points[2], Points[3]);
                g.DrawLine(GreenPen, Points[3], Points[4]);
                g.DrawRectangle(GreenPen, Rect);
            }
            else
            {
                g.FillRectangle(Brush, Rect);
                g.DrawLine(SPen, Points[0], Points[1]);
                g.DrawLine(LPen, Points[1], Points[3]);
                g.DrawLine(SPen, Points[2], Points[3]);
            }

        }
    }

    public class RongDuanQi//蓄电池熔断器
    {
        public Rectangle Rect;
        public Rectangle SmallRect;
        public bool Status = false;
        public Point[] Points = new Point[2];
        public Pen LgreenPen = new Pen(Color.FromArgb(0, 128, 0), 2);
        public Pen SgreenPen = new Pen(Color.FromArgb(0, 128, 0), 1);
        public Pen LredPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        public Pen SredPen = new Pen(Color.FromArgb(255, 0, 0), 1);
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public RongDuanQi(int x, int y, int weight, int height)
        {
            Rect.X = x;
            Rect.Y = y;
            Rect.Width = weight;
            Rect.Height = height;
            Points[0] = new Point(Rect.X + Rect.Width / 2, Rect.Y);
            Points[1] = new Point(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height);

            SmallRect.X = x + 15;
            SmallRect.Y = y + 10;
            SmallRect.Width = weight - 30;
            SmallRect.Height = height - 20;


        }
        public void OnDraw(Graphics g)
        {
            if (!Status)//Status为false时显示为状态
            {

                g.DrawLine(LgreenPen, Points[0], Points[1]);
                g.DrawRectangle(LgreenPen, Rect);
                g.DrawRectangle(SgreenPen, SmallRect);
            }
            else
            {

                g.DrawLine(LredPen, Points[0], Points[1]);//Status为true时显示熔断或故障状态
                g.DrawRectangle(LredPen, Rect);
                g.DrawRectangle(SredPen, SmallRect);
            }

        }
    }

    public class BusRongDuan//总线熔断器
    {
        public Rectangle Rect;
        public Rectangle SmallRect;
        public bool Status = false;
        public Point[] Points = new Point[2];
        public Pen LgreenPen = new Pen(Color.FromArgb(0, 128, 0), 2);
        public Pen SgreenPen = new Pen(Color.FromArgb(0, 128, 0), 1);
        public Pen LredPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        public Pen SredPen = new Pen(Color.FromArgb(255, 0, 0), 1);
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public BusRongDuan(int x, int y, int weight, int height)
        {
            Rect.X = x;
            Rect.Y = y;
            Rect.Width = weight;
            Rect.Height = height;
            Points[0] = new Point(Rect.X, Rect.Y + Rect.Height / 2);
            Points[1] = new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height / 2);

            SmallRect.X = x + 10;
            SmallRect.Y = y + 15;
            SmallRect.Width = weight - 20;
            SmallRect.Height = height - 30;


        }
        public void OnDraw(Graphics g)
        {
            if (!Status)//Status为false时显示为状态
            {

                g.DrawLine(LgreenPen, Points[0], Points[1]);
                g.DrawRectangle(LgreenPen, Rect);
                g.DrawRectangle(SgreenPen, SmallRect);
            }
            else
            {

                g.DrawLine(LredPen, Points[0], Points[1]);//Status为true时显示熔断或故障状态
                g.DrawRectangle(LredPen, Rect);
                g.DrawRectangle(SredPen, SmallRect);
            }

        }
    }

    public class JieChuQi//接触器
    {
        public Rectangle Rect;
        public bool Status = true;
        public Point[] Points = new Point[7];
        public Pen GreenPen = new Pen(Color.FromArgb(0, 128, 0), 2);
        public Pen LPen = new Pen(Color.FromArgb(170, 216, 167), 4);
        public Pen SPen = new Pen(Color.FromArgb(135, 210, 130), 2);
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public JieChuQi(int x, int y, int weight, int height)
        {
            Rect.X = x;
            Rect.Y = y;
            Rect.Width = weight;
            Rect.Height = height;
            Points[0] = new Point(Rect.X + Rect.Width / 2, Rect.Y);
            Points[1] = new Point(Rect.X + Rect.Width / 2, Rect.Y + 15);
            Points[2] = new Point(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height);
            Points[3] = new Point(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height - 8);
            Points[4] = new Point(Rect.X + Rect.Width / 2 + 10, Rect.Y + 15);
            Points[5] = new Point(Rect.X + Rect.Width / 2, Rect.Y + 8);
            Points[6] = new Point(Rect.X + Rect.Width / 2 + 5, Rect.Y + 11);

        }
        public void OnDraw(Graphics g)
        {
            if (Status)//Status为TRue时显示为开状态
            {

                g.DrawLine(GreenPen, Points[0], Points[1]);
                g.DrawLine(GreenPen, Points[2], Points[3]);
                g.DrawLine(GreenPen, Points[3], Points[4]);
                g.DrawLine(GreenPen, Points[1], Points[6]);
                g.DrawLine(GreenPen, Points[5], Points[6]);
                g.DrawRectangle(GreenPen, Rect);


            }
            else
            {
                g.FillRectangle(Brush, Rect);
                g.DrawLine(SPen, Points[0], Points[1]);
                g.DrawLine(SPen, Points[2], Points[6]);
            }

        }
    }
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_BatterySupply : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("电池供电");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 170, 800, 100);
        public Rectangle[] Rect = new Rectangle[5];//各节车厢边框
        public Rectangle[] NoRect = new Rectangle[3];//显示编号
        KaiGuan[] m_Kaiguan = new KaiGuan[5];//蓄电池开关
        RongDuanQi[] m_Rongduanqi = new RongDuanQi[5];//蓄电池熔断器
        BusRongDuan[] m_BusRongduan = new BusRongDuan[2];//总线熔断器
        JieChuQi[] m_Jiechuqi = new JieChuQi[5];//接触器
        public Rectangle[] ChongdianjiRect = new Rectangle[5];//充电机位置
        public GDIRectText[] AText = new GDIRectText[5];//显示电流文本框
        public GDIRectText[] VText = new GDIRectText[5];//显示电压文本框

        public GDIRectText[] GText = new GDIRectText[6];//显示网侧电压和电流

        public Rectangle[] ImageRect = new Rectangle[5];
        public Rectangle[] strRect = new Rectangle[6];

        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));//绿色画刷
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//红色画刷
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));//蓝色画刷
        public SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));//黑色画刷 
        public SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(121, 121, 121));//黑色画刷 
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));//白色画刷
        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 2);
        public Pen LinePen = new Pen(Color.FromArgb(121, 121, 121), 2);//线条画笔
        public Pen GreenPen = new Pen(Color.FromArgb(0, 128, 0), 2);//绿色画笔
        public Pen RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);//红色画笔

        public Font Strfont = new Font("Arial", 11);
        public Image Image;
        public bool[] Valueb = new bool[32];
        public float[] Valuef = new float[10];

        public Point[] C0 = new Point[12];
        public Point[] C1 = new Point[12];
        public Point[] C2 = new Point[12];
        public Point[] C3 = new Point[12];
        public Point[] C4 = new Point[12];


        public override string GetInfo()
        {
            return "蓄电池系统状态";
        }


        public override bool Initialize()
        {
            //3
            InitData();

            if (UIObj.ParaList.Count >= 1)
            {

                Image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[0]);


            }

            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            ReFreshData();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 32)
            {
                for (int i = 0; i < 32; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
            if (UIObj.InFloatList.Count >= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }

        }

        public void ReFreshData()
        {
            for (int i = 0; i < 5; i++)
            {
                m_Kaiguan[i].Status = !Valueb[i];//蓄电池开关状态更新
                m_Rongduanqi[i].Status = Valueb[i + 5];

                //////////////////////////////////////////////
                //-ycl-
                //取整
                ////////////////////////////////////////////
                AText[i].SetText(Convert.ToInt32(Valuef[i]).ToString());
                VText[i].SetText(Convert.ToInt32(Valuef[i + 5]).ToString());//电压电流更新
                if (i < 2)
                {
                    m_BusRongduan[i].Status = Valueb[i + 10];//总线熔断器状态更新
                }
                m_Jiechuqi[i].Status = Valueb[i + 12];
            }

        }
        public void InitData()
        {


            #region ::::::::::::::::::::各 节 车 边 框 位 置 ::::::::::::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {
                Rect[i] = new Rectangle(Recposition.X + i * 155 + 10, Recposition.Y, 150, 230);
            }
            #endregion

            #region :::::::::::::::::::蓄 电 池 开 关 状 态 显 示 位 置::::::::::::::::::::::::::::::::

            for (int i = 0; i < 5; i++)
            {

                m_Kaiguan[i] = new KaiGuan(Rect[i].X + 7, Rect[i].Y + 80, 40, 40);

            }

            #endregion

            #region ::::::::::::::::::::蓄 电 池 充 电 机  位 置::::::::::::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {

                ChongdianjiRect[i] = new Rectangle(Rect[i].X + 70, Rect[i].Y + 5, 40, 40);

            }

            #endregion

            #region :::::::::::::::::::::::::蓄 电 池 熔 断 器 位 置:::::::::::::::;;;;;:::::::
            for (int i = 0; i < 5; i++)
            {

                m_Rongduanqi[i] = new RongDuanQi(Rect[i].X + 7, Rect[i].Y + 130, 40, 40);


            }

            m_BusRongduan[0] = new BusRongDuan(Rect[1].X + Rect[1].Width - 45, Rect[1].Y + Rect[1].Height - 40, 40, 40);
            m_BusRongduan[1] = new BusRongDuan(Rect[3].X + Rect[3].Width - 45, Rect[3].Y + Rect[3].Height - 40, 40, 40);
            #endregion

            #region:::::::::::::::::::::::::接 触 器 位 置::::::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {
                m_Jiechuqi[i] = new JieChuQi(Rect[i].X + 70, Rect[i].Y + 100, 40, 40);
            }
            #endregion

            #region::::::::::::::::::::::::电 流 电 压 显 示 位 置:::::::::::::::::::::::::::::::
            for (int i = 0; i < 5; i++)
            {
                AText[i] = new GDIRectText();
                AText[i].SetBkColor(192, 192, 192);
                AText[i].SetDrawFrm(true);
                AText[i].SetTextColor(0, 128, 0);
                AText[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                AText[i].SetLinePen(121, 121, 121, 1);
                AText[i].SetTextRect(Rect[i].X + 7, Rect[i].Y + 50, 35, 20);

                VText[i] = new GDIRectText();
                VText[i].SetBkColor(192, 192, 192);
                VText[i].SetDrawFrm(true);
                VText[i].SetTextColor(0, 128, 0);
                VText[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                VText[i].SetLinePen(121, 121, 121, 1);
                VText[i].SetTextRect(Rect[i].X + 95, Rect[i].Y + 60, 40, 20);

            }

            #endregion

            #region :::::::::::::::::::::::::各 线 段 点 坐 标 设 置:::::::::::::::;;;;;:::::::



            C0[0] = new Point(Rect[0].X + 90, Rect[0].Y + 45);
            C0[1] = new Point(Rect[0].X + 90, Rect[0].Y + 100);
            C0[2] = new Point(Rect[0].X + 85, Rect[0].Y + 140);
            C0[3] = new Point(Rect[0].X + 85, Rect[0].Y + 182);
            C0[4] = new Point(Rect[0].X + 90, Rect[0].Y + 75);
            C0[5] = new Point(Rect[0].X + 27, Rect[0].Y + 75);
            C0[6] = new Point(Rect[0].X + 27, Rect[0].Y + 80);
            C0[7] = new Point(Rect[0].X + 27, Rect[0].Y + 120);
            C0[8] = new Point(Rect[0].X + 27, Rect[0].Y + 130);
            C0[9] = new Point(Rect[0].X + 27, Rect[0].Y + 170);
            C0[10] = new Point(Rect[0].X + 27, Rect[0].Y + 182);


            C1[0] = new Point(Rect[1].X + 90, Rect[1].Y + 45);
            C1[1] = new Point(Rect[1].X + 90, Rect[1].Y + 100);
            C1[2] = new Point(Rect[1].X + 85, Rect[1].Y + 140);
            C1[3] = new Point(Rect[1].X + 85, Rect[1].Y + 182);
            C1[4] = new Point(Rect[1].X + 90, Rect[1].Y + 75);
            C1[5] = new Point(Rect[1].X + 27, Rect[1].Y + 75);
            C1[6] = new Point(Rect[1].X + 27, Rect[1].Y + 80);
            C1[7] = new Point(Rect[1].X + 27, Rect[1].Y + 120);
            C1[8] = new Point(Rect[1].X + 27, Rect[1].Y + 130);
            C1[9] = new Point(Rect[1].X + 27, Rect[1].Y + 170);
            C1[10] = new Point(Rect[1].X + 27, Rect[1].Y + 182);


            C2[0] = new Point(Rect[2].X + 90, Rect[2].Y + 45);
            C2[1] = new Point(Rect[2].X + 90, Rect[2].Y + 100);
            C2[2] = new Point(Rect[2].X + 85, Rect[2].Y + 140);
            C2[3] = new Point(Rect[2].X + 85, Rect[2].Y + 182);
            C2[4] = new Point(Rect[2].X + 90, Rect[2].Y + 75);
            C2[5] = new Point(Rect[2].X + 27, Rect[2].Y + 75);
            C2[6] = new Point(Rect[2].X + 27, Rect[2].Y + 80);
            C2[7] = new Point(Rect[2].X + 27, Rect[2].Y + 120);
            C2[8] = new Point(Rect[2].X + 27, Rect[2].Y + 130);
            C2[9] = new Point(Rect[2].X + 27, Rect[2].Y + 170);
            C2[10] = new Point(Rect[2].X + 27, Rect[2].Y + 182);


            C3[0] = new Point(Rect[3].X + 90, Rect[3].Y + 45);
            C3[1] = new Point(Rect[3].X + 90, Rect[3].Y + 100);
            C3[2] = new Point(Rect[3].X + 85, Rect[3].Y + 140);
            C3[3] = new Point(Rect[3].X + 85, Rect[3].Y + 182);
            C3[4] = new Point(Rect[3].X + 90, Rect[3].Y + 75);
            C3[5] = new Point(Rect[3].X + 27, Rect[3].Y + 75);
            C3[6] = new Point(Rect[3].X + 27, Rect[3].Y + 80);
            C3[7] = new Point(Rect[3].X + 27, Rect[3].Y + 120);
            C3[8] = new Point(Rect[3].X + 27, Rect[3].Y + 130);
            C3[9] = new Point(Rect[3].X + 27, Rect[3].Y + 170);
            C3[10] = new Point(Rect[3].X + 27, Rect[3].Y + 182);


            C4[0] = new Point(Rect[4].X + 90, Rect[4].Y + 45);
            C4[1] = new Point(Rect[4].X + 90, Rect[4].Y + 100);
            C4[2] = new Point(Rect[4].X + 85, Rect[4].Y + 140);
            C4[3] = new Point(Rect[4].X + 85, Rect[4].Y + 182);
            C4[4] = new Point(Rect[4].X + 90, Rect[4].Y + 75);
            C4[5] = new Point(Rect[4].X + 27, Rect[4].Y + 75);
            C4[6] = new Point(Rect[4].X + 27, Rect[4].Y + 80);
            C4[7] = new Point(Rect[4].X + 27, Rect[4].Y + 120);
            C4[8] = new Point(Rect[4].X + 27, Rect[4].Y + 130);
            C4[9] = new Point(Rect[4].X + 27, Rect[4].Y + 170);
            C4[10] = new Point(Rect[4].X + 27, Rect[4].Y + 182);



            #endregion

            #region:::::::::::::::::::::::::::底 部 图 标 显 示:::::::::::::::::

            for (int i = 0; i < 5; i++)
            {
                ImageRect[i] = new Rectangle(Rect[i].X + 1, Rect[i].Y + 178, Rect[i].Width - 3, 35);
            }
            #endregion
        }
        public void DrawOn(Graphics g)
        {

            //绘制菜单标题
            Title.OnDraw(g);
            //底部图形

            for (int i = 0; i < 5; i++)
            {
                g.DrawImage(Image, ImageRect[i]);
            }
            //各 节 车 边 框 绘 制

            for (int i = 0; i < 5; i++)
            {
                g.DrawRectangle(LinePen, Rect[i]);
            }
            //电池开关  蓄 电 池 熔 断 器 状态显示
            for (int i = 0; i < 5; i++)
            {
                m_Kaiguan[i].OnDraw(g);//蓄电池开关绘制
                m_Jiechuqi[i].OnDraw(g);//接触器绘制
                m_Rongduanqi[i].OnDraw(g);//熔断器绘制
                if (i < 2)
                {
                    m_BusRongduan[i].OnDraw(g);//总线熔断器 
                }

            }
            //蓄电池充电机绘制
            {
                for (int i = 0; i < 5; i++)
                {
                    if (!Valueb[17 + i] && (!Valueb[22 + i]))
                    {
                        g.DrawRectangle(GreenPen, ChongdianjiRect[i]);
                        g.DrawString("BCM", Strfont, GreenBrush, ChongdianjiRect[i].X, ChongdianjiRect[i].Y + 12);
                    }
                    else if (!Valueb[17 + i] && (Valueb[22 + i]))
                    {
                        g.FillRectangle(GreenBrush, ChongdianjiRect[i]);
                        g.DrawString("BCM", Strfont, WhiteBrush, ChongdianjiRect[i].X, ChongdianjiRect[i].Y + 12);
                    }
                    else if (Valueb[17 + i])
                    {
                        g.DrawRectangle(RedPen, ChongdianjiRect[i]);
                        g.DrawString("BCM", Strfont, RedBrush, ChongdianjiRect[i].X, ChongdianjiRect[i].Y + 12);
                    }
                    else
                    {
                        g.DrawRectangle(LinePen, ChongdianjiRect[i]);
                        g.DrawString("BCM", Strfont, GrayBrush, ChongdianjiRect[i].X, ChongdianjiRect[i].Y + 12);
                    }
                    if (Valueb[i + 27])
                    {
                        g.DrawRectangle(LinePen, ChongdianjiRect[i]);
                        g.DrawString("BCM", Strfont, GrayBrush, ChongdianjiRect[i].X, ChongdianjiRect[i].Y + 12);
                    }
                }
            }

            //电流电压的显示
            for (int i = 0; i < 5; i++)
            {
                AText[i].OnDraw(g);
                VText[i].OnDraw(g);
                g.DrawString("A", Strfont, BlackBrush, AText[i].OutLineRectangle.X + 40, AText[i].OutLineRectangle.Y);
                g.DrawString("V", Strfont, BlackBrush, VText[i].OutLineRectangle.X + 40, VText[i].OutLineRectangle.Y);
            }

            //绘制线条
            g.DrawLine(LinePen, C0[0], C0[1]);
            g.DrawLine(LinePen, C0[2], C0[3]);
            g.DrawLine(LinePen, C0[4], C0[5]);
            g.DrawLine(LinePen, C0[5], C0[6]);
            g.DrawLine(LinePen, C0[7], C0[8]);
            g.DrawLine(LinePen, C0[9], C0[10]);

            g.DrawLine(LinePen, C1[0], C1[1]);
            g.DrawLine(LinePen, C1[2], C1[3]);
            g.DrawLine(LinePen, C1[4], C1[5]);
            g.DrawLine(LinePen, C1[5], C1[6]);
            g.DrawLine(LinePen, C1[7], C1[8]);
            g.DrawLine(LinePen, C1[9], C1[10]);

            g.DrawLine(LinePen, C2[0], C2[1]);
            g.DrawLine(LinePen, C2[2], C2[3]);
            g.DrawLine(LinePen, C2[4], C2[5]);
            g.DrawLine(LinePen, C2[5], C2[6]);
            g.DrawLine(LinePen, C2[7], C2[8]);
            g.DrawLine(LinePen, C2[9], C2[10]);

            g.DrawLine(LinePen, C3[0], C3[1]);
            g.DrawLine(LinePen, C3[2], C3[3]);
            g.DrawLine(LinePen, C3[4], C3[5]);
            g.DrawLine(LinePen, C3[5], C3[6]);
            g.DrawLine(LinePen, C3[7], C3[8]);
            g.DrawLine(LinePen, C3[9], C3[10]);

            g.DrawLine(LinePen, C4[0], C4[1]);
            g.DrawLine(LinePen, C4[2], C4[3]);
            g.DrawLine(LinePen, C4[4], C4[5]);
            g.DrawLine(LinePen, C4[5], C4[6]);
            g.DrawLine(LinePen, C4[7], C4[8]);
            g.DrawLine(LinePen, C4[9], C4[10]);


        }




    }
}