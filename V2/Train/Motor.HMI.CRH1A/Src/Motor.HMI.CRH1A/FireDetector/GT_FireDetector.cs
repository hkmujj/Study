using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.FireDetector;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_FireDetector : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("火灾探测");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 170, 800, 100);
        public Rectangle[] TrainRect = new Rectangle[8];//表示各车厢的边框
        public Rectangle[] NoRect = new Rectangle[8];//车编号显示位置  
        public Rectangle Rect;//页脚区域

        public Pen TrainPen = new Pen(Color.FromArgb(100, 100, 100), 2);
        public Pen LinePen = new Pen(Color.FromArgb(131, 131, 131), 2);
        public Pen RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        public Pen GreenPen = new Pen(Color.FromArgb(0, 126, 0), 2);

        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 126, 0));

        public Font Strfont = new Font("Arial", 11, FontStyle.Bold);
        public bool[] Valueb = new bool[96];
        public Image Image;
        public CRH1AButton GButton;
        private FireModelState m_FireModelState;

        private Color m_FireModelBtnColor = Color.FromArgb(192, 192, 192);

        public Rectangle[] SmallRect = new Rectangle[48];//每节车厢的各个火灾探测器的状态
        public Point[] C1 = new Point[7];
        public Point[] C2 = new Point[6];
        public Point[] C3 = new Point[6];
        public Point[] C4 = new Point[6];

        public Point[] C5 = new Point[8];
        public Point[] C6 = new Point[6];
        public Point[] C7 = new Point[6];
        public Point[] C8 = new Point[8];

        public override string GetInfo()
        {
            return "火灾探测系统";
        }


        public override bool Initialize()
        {
            //3
            InitData();
            if (UIObj.ParaList.Count > 0)
            {
                Image = Image.FromFile(RecPath + "//" + UIObj.ParaList[0]);
            }

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
            if (UIObj.InBoolList.Count >= 96)
            {
                for (int i = 0; i < 96; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

        }

        public void ReFreshData()
        {




        }
        public void InitData()
        {

            #region::::::::::::;;;;;;;;各 车 边 框 位 置 初 始 化;;;;;;;;;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 4; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + 50 + 185 * i, Recposition.Y - 20, 175, 75);

            }
            for (int i = 0; i < 4; i++)
            {
                TrainRect[i + 4] = new Rectangle(Recposition.X + 10 + 185 * i, Recposition.Y + 120, 175, 75);
            }





            #endregion


            #region :::::::::::::::::::车编号显示的位置设置;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 4; i++)
            {
                NoRect[i] = new Rectangle(Recposition.X + 120 + 185 * i, Recposition.Y - 40, 175, 75);

            }
            for (int i = 0; i < 4; i++)
            {
                NoRect[i + 4] = new Rectangle(Recposition.X + 70 + 185 * i, Recposition.Y + 90, 175, 75);
            }


            #endregion






            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::

            Rect = new Rectangle(Recposition.X, Recposition.Y + 210, 790, 70);
            GButton = new CRH1AButton();
            GButton.SetButtonRect(Rect.X + 370, Rect.Y + 10, 90, 50);
            GButton.SetButtonColor(m_FireModelBtnColor.R, m_FireModelBtnColor.G, m_FireModelBtnColor.B);
            GButton.SetButtonText("火灾模式");
            m_FireModelState = new FireModelState(GButton, idx => OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], idx, 0));

            #endregion

            #region ::::::::::::::::::::::::::::网格线条线条;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            C1[0] = new Point(Recposition.X + 90, Recposition.Y - 20);
            C1[1] = new Point(Recposition.X + 90, Recposition.Y + 55);
            C1[2] = new Point(Recposition.X + 130, Recposition.Y - 20);
            C1[3] = new Point(Recposition.X + 130, Recposition.Y + 55);
            C1[4] = new Point(Recposition.X + 130, Recposition.Y + 15);
            C1[5] = new Point(Recposition.X + 160, Recposition.Y + 15);
            C1[6] = new Point(Recposition.X + 160, Recposition.Y + 55);

            //2
            C2[0] = new Point(Recposition.X + 300, Recposition.Y - 20);
            C2[1] = new Point(Recposition.X + 300, Recposition.Y + 55);
            C2[2] = new Point(Recposition.X + 330, Recposition.Y + 15);
            C2[3] = new Point(Recposition.X + 360, Recposition.Y + 15);
            C2[4] = new Point(Recposition.X + 330, Recposition.Y - 20);
            C2[5] = new Point(Recposition.X + 330, Recposition.Y + 55);

            //3
            C3[0] = new Point(Recposition.X + 470, Recposition.Y - 20);
            C3[1] = new Point(Recposition.X + 470, Recposition.Y + 55);
            C3[2] = new Point(Recposition.X + 530, Recposition.Y - 20);
            C3[3] = new Point(Recposition.X + 530, Recposition.Y + 55);
            C3[4] = new Point(Recposition.X + 473, Recposition.Y + 20);
            C3[5] = new Point(Recposition.X + 495, Recposition.Y + 20);

            //4
            C4[0] = new Point(Recposition.X + 680, Recposition.Y - 20);
            C4[1] = new Point(Recposition.X + 680, Recposition.Y + 55);
            C4[2] = new Point(Recposition.X + 710, Recposition.Y + 15);
            C4[3] = new Point(Recposition.X + 735, Recposition.Y + 15);
            C4[4] = new Point(Recposition.X + 710, Recposition.Y - 20);
            C4[5] = new Point(Recposition.X + 710, Recposition.Y + 55);

            //5
            C5[0] = new Point(Recposition.X + 60, Recposition.Y + 120);
            C5[1] = new Point(Recposition.X + 60, Recposition.Y + 195);
            C5[2] = new Point(Recposition.X + 90, Recposition.Y + 120);
            C5[3] = new Point(Recposition.X + 90, Recposition.Y + 195);
            C5[4] = new Point(Recposition.X + 60, Recposition.Y + 158);
            C5[5] = new Point(Recposition.X + 90, Recposition.Y + 158);
            C5[6] = new Point(Recposition.X + 120, Recposition.Y + 120);
            C5[7] = new Point(Recposition.X + 120, Recposition.Y + 195);

            //6
            C6[0] = new Point(Recposition.X + 260, Recposition.Y + 120);
            C6[1] = new Point(Recposition.X + 260, Recposition.Y + 195);
            C6[2] = new Point(Recposition.X + 290, Recposition.Y + 155);
            C6[3] = new Point(Recposition.X + 320, Recposition.Y + 155);
            C6[4] = new Point(Recposition.X + 290, Recposition.Y + 120);
            C6[5] = new Point(Recposition.X + 290, Recposition.Y + 195);

            //7
            C7[0] = new Point(Recposition.X + 430, Recposition.Y + 120);
            C7[1] = new Point(Recposition.X + 430, Recposition.Y + 195);
            C7[2] = new Point(Recposition.X + 490, Recposition.Y + 120);
            C7[3] = new Point(Recposition.X + 490, Recposition.Y + 195);
            C7[4] = new Point(Recposition.X + 433, Recposition.Y + 160);
            C7[5] = new Point(Recposition.X + 455, Recposition.Y + 160);

            //8
            C8[0] = new Point(Recposition.X + 630, Recposition.Y + 120);
            C8[1] = new Point(Recposition.X + 630, Recposition.Y + 195);
            C8[2] = new Point(Recposition.X + 660, Recposition.Y + 120);
            C8[3] = new Point(Recposition.X + 660, Recposition.Y + 195);
            C8[4] = new Point(Recposition.X + 630, Recposition.Y + 160);
            C8[5] = new Point(Recposition.X + 660, Recposition.Y + 160);
            C8[6] = new Point(Recposition.X + 700, Recposition.Y + 120);
            C8[7] = new Point(Recposition.X + 700, Recposition.Y + 195);

            #endregion


            #region :::::::::::::::::::探 测 器 位 置 设 置:::::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    SmallRect[i] = new Rectangle(Recposition.X + 25, Recposition.Y + 5, 18, 15);
                }
                else if (i == 3)
                {
                    SmallRect[i] = new Rectangle(Recposition.X + 45 + i * 30, Recposition.Y - 10, 18, 15);
                }
                else if (i == 1 || i == 2)
                {
                    SmallRect[i] = new Rectangle(Recposition.X + 35 + i * 30, Recposition.Y + 5, 18, 15);
                }
                else
                {
                    SmallRect[i] = new Rectangle(Recposition.X + 45 + i * 30, Recposition.Y + 5, 18, 15);
                }
            }
            // 2
            SmallRect[6] = new Rectangle(Recposition.X + 260, Recposition.Y + 15, 18, 15);
            SmallRect[7] = new Rectangle(Recposition.X + 305, Recposition.Y + 15, 18, 15);
            SmallRect[8] = new Rectangle(Recposition.X + 335, Recposition.Y - 12, 18, 15);
            SmallRect[9] = new Rectangle(Recposition.X + 335, Recposition.Y + 25, 18, 15);
            SmallRect[10] = new Rectangle(Recposition.X + 365, Recposition.Y + 15, 18, 15);
            SmallRect[11] = new Rectangle(Recposition.X + 390, Recposition.Y + 15, 18, 15);

            //3
            SmallRect[12] = new Rectangle(Recposition.X + 423, Recposition.Y + 15, 18, 15);
            SmallRect[13] = new Rectangle(Recposition.X + 448, Recposition.Y + 15, 18, 15);
            SmallRect[14] = new Rectangle(Recposition.X + 475, Recposition.Y - 5, 18, 15);
            SmallRect[15] = new Rectangle(Recposition.X + 475, Recposition.Y + 28, 18, 15);
            SmallRect[16] = new Rectangle(Recposition.X + 500, Recposition.Y + 15, 18, 15);
            SmallRect[17] = new Rectangle(Recposition.X + 550, Recposition.Y + 15, 18, 15);

            // 4           
            SmallRect[18] = new Rectangle(Recposition.X + 640, Recposition.Y + 15, 18, 15);
            SmallRect[19] = new Rectangle(Recposition.X + 685, Recposition.Y + 15, 18, 15);
            SmallRect[20] = new Rectangle(Recposition.X + 715, Recposition.Y - 12, 18, 15);
            SmallRect[21] = new Rectangle(Recposition.X + 715, Recposition.Y + 25, 18, 15);
            SmallRect[22] = new Rectangle(Recposition.X + 736, Recposition.Y + 15, 18, 15);
            SmallRect[23] = new Rectangle(Recposition.X + 760, Recposition.Y + 15, 18, 15);

            //5          
            SmallRect[24] = new Rectangle(Recposition.X + 25, Recposition.Y + 150, 18, 15);
            SmallRect[25] = new Rectangle(Recposition.X + 65, Recposition.Y + 165, 18, 15);
            SmallRect[26] = new Rectangle(Recposition.X + 95, Recposition.Y + 150, 18, 15);
            SmallRect[27] = new Rectangle(Recposition.X + 135, Recposition.Y + 150, 18, 15);
            SmallRect[28] = new Rectangle(Recposition.X + 160, Recposition.Y + 135, 18, 15);
            SmallRect[29] = new Rectangle(Recposition.X + 160, Recposition.Y + 160, 18, 15);

            //6
            SmallRect[30] = new Rectangle(Recposition.X + 220, Recposition.Y + 155, 18, 15);
            SmallRect[31] = new Rectangle(Recposition.X + 265, Recposition.Y + 155, 18, 15);
            SmallRect[32] = new Rectangle(Recposition.X + 295, Recposition.Y + 128, 18, 15);
            SmallRect[33] = new Rectangle(Recposition.X + 295, Recposition.Y + 165, 18, 15);
            SmallRect[34] = new Rectangle(Recposition.X + 323, Recposition.Y + 155, 18, 15);
            SmallRect[35] = new Rectangle(Recposition.X + 347, Recposition.Y + 155, 18, 15);

            //7
            SmallRect[36] = new Rectangle(Recposition.X + 383, Recposition.Y + 165, 18, 15);
            SmallRect[37] = new Rectangle(Recposition.X + 408, Recposition.Y + 165, 18, 15);
            SmallRect[38] = new Rectangle(Recposition.X + 435, Recposition.Y + 135, 18, 15);
            SmallRect[39] = new Rectangle(Recposition.X + 435, Recposition.Y + 168, 18, 15);
            SmallRect[40] = new Rectangle(Recposition.X + 460, Recposition.Y + 155, 18, 15);
            SmallRect[41] = new Rectangle(Recposition.X + 510, Recposition.Y + 155, 18, 15);

            //8
            SmallRect[42] = new Rectangle(Recposition.X + 584, Recposition.Y + 155, 18, 15);
            SmallRect[43] = new Rectangle(Recposition.X + 607, Recposition.Y + 155, 18, 15);
            SmallRect[44] = new Rectangle(Recposition.X + 635, Recposition.Y + 168, 18, 15);
            SmallRect[45] = new Rectangle(Recposition.X + 670, Recposition.Y + 155, 18, 15);
            SmallRect[46] = new Rectangle(Recposition.X + 710, Recposition.Y + 155, 18, 15);
            SmallRect[47] = new Rectangle(Recposition.X + 745, Recposition.Y + 155, 18, 15);



            #endregion

        }
        public void DrawOn(Graphics g)
        {

            //绘制菜单标题
            Title.OnDraw(g);

            //车辆绘制
            for (int i = 0; i < 8; i++)
            {
                g.DrawRectangle(TrainPen, TrainRect[i]);
            }
            g.DrawArc(TrainPen, TrainRect[0].X - 40, TrainRect[0].Y, 70, TrainRect[0].Height, 85, 189);
            g.DrawArc(TrainPen, TrainRect[7].X + TrainRect[7].Width - 40, TrainRect[7].Y, 70, TrainRect[0].Height, 270, 180);


            //页脚区域绘制
            g.DrawImage(Image, Rect);
            GButton.OnDraw(g);

            //绘制编号
            for (int i = 0; i < 8; i++)
            {
                if (i < 7)
                {
                    g.DrawString("0" + (i + 1).ToString(), new Font("Arial", 12), new SolidBrush(Color.White), NoRect[i]);
                }
                else
                {
                    g.DrawString("00", new Font("Arial", 12), new SolidBrush(Color.White), NoRect[i]);
                }
            }

            //每辆车火灾探测器状态显示区域
            for (int i = 0; i < 48; i++)
            {
                if (Valueb[i])
                {
                    g.FillRectangle(RedBrush, SmallRect[i]);
                    if ((i + 1) % 6 == 0)
                    {
                        g.DrawString("6", new Font("Arial", 9), WhiteBrush, SmallRect[i]);
                    }
                    else
                    {
                        g.DrawString(((i + 1) % 6).ToString(), new Font("Arial", 9), WhiteBrush, SmallRect[i]);
                    }

                }
                else if (Valueb[i + 48] && (!Valueb[i]))
                {
                    g.DrawRectangle(RedPen, SmallRect[i]);
                    if ((i + 1) % 6 == 0)
                    {
                        g.DrawString("6", new Font("Arial", 9), RedBrush, SmallRect[i]);
                    }
                    else
                    {
                        g.DrawString(((i + 1) % 6).ToString(), new Font("Arial", 9), RedBrush, SmallRect[i]);
                    }

                }
                else
                {
                    g.DrawRectangle(GreenPen, SmallRect[i]);
                    if ((i + 1) % 6 == 0)
                    {
                        g.DrawString("6", new Font("Arial", 9), GreenBrush, SmallRect[i]);
                    }
                    else
                    {
                        g.DrawString(((i + 1) % 6).ToString(), new Font("Arial", 9), GreenBrush, SmallRect[i]);
                    }
                }

            }
            //绘制网格
            //1
            g.DrawLine(TrainPen, C1[0], C1[1]);
            g.DrawLine(TrainPen, C1[2], C1[3]);
            g.DrawLine(TrainPen, C1[4], C1[5]);
            g.DrawLine(TrainPen, C1[5], C1[6]);

            //2
            g.DrawLine(TrainPen, C2[0], C2[1]);
            g.DrawLine(TrainPen, C2[2], C2[3]);
            g.DrawLine(TrainPen, C2[4], C2[5]);

            //3
            g.DrawLine(TrainPen, C3[0], C3[1]);
            g.DrawLine(TrainPen, C3[2], C3[3]);
            g.DrawLine(TrainPen, C3[4], C3[5]);

            //4
            g.DrawLine(TrainPen, C4[0], C4[1]);
            g.DrawLine(TrainPen, C4[2], C4[3]);
            g.DrawLine(TrainPen, C4[4], C4[5]);

            //5
            g.DrawLine(TrainPen, C5[0], C5[1]);
            g.DrawLine(TrainPen, C5[2], C5[3]);
            g.DrawLine(TrainPen, C5[4], C5[5]);
            g.DrawLine(TrainPen, C5[6], C5[7]);

            //6
            g.DrawLine(TrainPen, C6[0], C6[1]);
            g.DrawLine(TrainPen, C6[2], C6[3]);
            g.DrawLine(TrainPen, C6[4], C6[5]);

            //7
            g.DrawLine(TrainPen, C7[0], C7[1]);
            g.DrawLine(TrainPen, C7[2], C7[3]);
            g.DrawLine(TrainPen, C7[4], C7[5]);

            //8
            g.DrawLine(TrainPen, C8[0], C8[1]);
            g.DrawLine(TrainPen, C8[2], C8[3]);
            g.DrawLine(TrainPen, C8[4], C8[5]);
            g.DrawLine(TrainPen, C8[6], C8[7]);


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
            if (GButton.Contains(x, y))
            {
                GButton.OnButtonDown();
                //OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
            }
        }

        public void OnButtonUp(int x, int y)
        {
            if (GButton.Contains(x, y))
            {
                GButton.OnButtonUp();
                m_FireModelState.ChangeState();
            }
        }

    }
}

