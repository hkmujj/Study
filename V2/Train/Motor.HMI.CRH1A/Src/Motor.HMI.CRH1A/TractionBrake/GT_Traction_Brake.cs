using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.TractionBrake;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_Traction_Brake:CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("牵引/制动");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 170, 791, 280);
        public Rectangle[] Rect = new Rectangle[8];//添加8个背景框

        public SolidBrush BackBrush=new SolidBrush(Color.FromArgb(119,136,152));
        public SolidBrush BlackBrush=new SolidBrush(Color.FromArgb(0,0,0));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));//绿色画刷
        public SolidBrush BlueBrush=new SolidBrush(Color.FromArgb(0,255,255));//蓝色画刷
        public Pen Pen1 = new Pen(Color.FromArgb(166,174, 182),2);
        public Pen Pen2 = new Pen(Color.FromArgb(22, 25, 29), 2);
        public Pen Pen3= new Pen(Color.FromArgb(195, 198,201), 2);
        public Font Strfont=new Font("Arial",10);
        public CRH1AButton GButton;
        public float[] Valuef = new float[25];
        public Rectangle[] N1 = new Rectangle[8];//表示牵引力
        public Rectangle[] N2 = new Rectangle[8];//再生（电）制动力
        public Rectangle[] N3 = new Rectangle[8];//机械制动力

        /// <summary>
        /// 加速度 
        /// </summary>
        private AccelerationCtrol m_AccelerationCtrol;

        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "牵引/制动";
        }


        public override bool Initialize()
        {
            //3
            InitDate();

            return true;
        }

        public void GetValue()
        {
            if (UIObj.InFloatList.Count >= 24)
            {
                for (int i = 0; i < 24; i++)
                {
                    if (FloatList[UIObj.InFloatList[i]] > 100)
                    {
                        Valuef[i] = 100;
                    }
                    else if (FloatList[UIObj.InFloatList[i]] < 0)
                    {
                        Valuef[i] = 0;
                    }
                    else
                    {
                        Valuef[i] = FloatList[UIObj.InFloatList[i]];
                    }

                }
            }

            m_AccelerationCtrol.Acceleration = FloatList[UIObj.InFloatList[TractionBrakeInFloatAdpt.AccIdx]];

            for (int i = 0; i < 8;i++ )
            {
                N1[i] = new Rectangle(Rect[i].X + 13,
                                        Rect[i].Y + 100 - Convert.ToInt32(Valuef[i]),
                                        16,
                                        Convert.ToInt32(Valuef[i]));

                N2[i] = new Rectangle(Rect[i].X + 13,
                                        Rect[i].Y + 100,
                                        16,
                                        Convert.ToInt32(Valuef[i+8]));

                N3[i] = new Rectangle(Rect[i].X + 13,
                                        Rect[i].Y + 100 + Convert.ToInt32(Valuef[i + 8]),
                                        16,
                                        Convert.ToInt32(Valuef[i + 16]));
            }
           
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void InitDate()
        {
            GButton = new CRH1AButton();
            GButton.SetButtonColor(192, 192, 192);
            GButton.SetButtonRect(Recposition.X + 610, Recposition.Y + 120, 120, 60);
            GButton.SetButtonText("切除电制动");

            m_AccelerationCtrol = new AccelerationCtrol(new Point(Recposition.X + 630, Recposition.Y + 20));

            //背景框初始化
            for (int i = 0; i < 8;i++ )
            {
                Rect[i] = new Rectangle(Recposition.X + 42 * i+22, Recposition.Y, 40, 200);
            }
        }

        public void DrawOn(Graphics e)
        {
            //菜单标题栏的绘制
            Title.OnDraw(e);
            GButton.OnDraw(e);

            m_AccelerationCtrol.OnDraw(e);

            //绘制背景框
            for (int i = 0; i < 8;i++ )
            {
                e.FillRectangle(BackBrush, Rect[i]);
                e.DrawLine(Pen1,Rect[i].X+5,Rect[i].Y+40,Rect[i].X+35,Rect[i].Y+40);
                e.DrawLine(Pen2, Rect[i].X + 5, Rect[i].Y + 100, Rect[i].X + 35, Rect[i].Y + 100);
                e.DrawLine(Pen3, Rect[i].X + 5, Rect[i].Y + 160, Rect[i].X + 35, Rect[i].Y + 160);
            }
            e.DrawString("100", Strfont, BlackBrush, Rect[7].X + 42, Rect[7].Y - 5);
            e.DrawString("60", Strfont, BlackBrush, Rect[7].X + 42, Rect[7].Y +32);
            e.DrawString("0%", Strfont, BlackBrush, Rect[7].X + 42, Rect[7].Y+90);
            e.DrawString("-60", Strfont, BlackBrush, Rect[7].X + 42, Rect[7].Y + 150);
            e.DrawString("-100", Strfont, BlackBrush, Rect[7].X + 42, Rect[7].Y + 190);

            e.FillRectangle(GreenBrush, Recposition.X + 70, Recposition.Y + 210, 40, 20);
            e.DrawString("%电力牵引/制动", Strfont, BlackBrush, Recposition.X + 115, Recposition.Y + 210);

            e.FillRectangle(BlueBrush, Recposition.X + 70, Recposition.Y + 245, 40, 20);
            e.DrawString("%机械制动", Strfont, BlackBrush, Recposition.X + 115, Recposition.Y + 245);
            for (int i = 0; i < 8;i++ )
            {
                e.FillRectangle(GreenBrush, N1[i]);
                e.FillRectangle(GreenBrush, N2[i]);
                e.FillRectangle(BlueBrush, N3[i]);
            }
        }
        #endregion#
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

        public void OnButtonDown(int x,int y)
        {
            if (GButton.Contains(x,y))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                GButton.OnButtonDown();
            }
        }


        public void OnButtonUp(int x, int y)
        {
            if (GButton.Contains(x, y))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                GButton.OnButtonUp();
            }
        }
   
    }
    

}
