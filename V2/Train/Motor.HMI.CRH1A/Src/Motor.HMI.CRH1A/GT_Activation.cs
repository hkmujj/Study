using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_Activation : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("启动状态");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 133, 800, 100);
        public Rectangle[] TrainRect = new Rectangle[8];//车的位置
        public int Weight = 90;//车的宽度
        public int Height = 70;//车的高度

        //页脚区域
        Rectangle m_Rect;

        public Rectangle[] HighVoltageRect = new Rectangle[2];//受电弓显示位置
        public Rectangle[] BatteryRect = new Rectangle[5];//电池显示位置
        public Rectangle[] BianRect = new Rectangle[5];//变流器显示位置
        public Rectangle[] LcbRect = new Rectangle[5];//主断路器显示位置
        public Font Strfont = new Font("Arial", 10);
        public SolidBrush StrBrush = new SolidBrush(Color.FromArgb(135, 135, 135));
        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 2);
        public Image[] Image = new Image[14];
        public bool[] Valueb = new bool[24];

        public override string GetInfo()
        {
            return "启动状态";
        }

        public override bool Initialize()
        {
            //3
            InitData();


            //////////////////加 载 图 片
            if (UIObj.ParaList.Count >= 14)
                for (int i = 0; i < 14; i++)
                {
                    Image[i] = System.Drawing.Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
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
            if (UIObj.InBoolList.Count >= 24)
            {
                for (int i = 0; i < 24; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

        }

        public void ReFreshData()
        {
            for (int i = 0; i < 9; i++)
            {


            }
        }
        public void InitData()
        {

            #region::::::::::::;;;;;;;;各 车 位 置 初 始 化;;;;;;;;;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 8; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + i * (Weight + 5) + 25, Recposition.Y + 40, Weight, Height);
            }


            #endregion


            #region::::::::::::::::::::受 电 弓 的 位 置 设 定:::::::::::::::::::::::::::::
            HighVoltageRect[0] = new Rectangle(TrainRect[1].X + 10, TrainRect[1].Y - 10, 25, 20);
            HighVoltageRect[1] = new Rectangle(TrainRect[6].X + 10, TrainRect[1].Y - 10, 25, 20);
            #endregion

            #region :::::::::::::::::::蓄 电 池 位 置 设 置:::::::::::::::::
            BatteryRect[0] = new Rectangle(TrainRect[0].X + 20, TrainRect[0].Y + 20, 25, 20);
            BatteryRect[1] = new Rectangle(TrainRect[2].X + 15, TrainRect[2].Y + 20, 25, 20);
            BatteryRect[2] = new Rectangle(TrainRect[3].X + 15, TrainRect[3].Y + 20, 25, 20);
            BatteryRect[3] = new Rectangle(TrainRect[5].X + 15, TrainRect[5].Y + 20, 25, 20);
            BatteryRect[4] = new Rectangle(TrainRect[7].X + 15, TrainRect[7].Y + 20, 25, 20);
            #endregion

            #region :::::::::::::::::::变 流 器 位 置 设 置:::::::::::::::::
            BianRect[0] = new Rectangle(TrainRect[0].X + 60, TrainRect[0].Y + 20, 25, 20);
            BianRect[1] = new Rectangle(TrainRect[2].X + 55, TrainRect[2].Y + 20, 25, 20);
            BianRect[2] = new Rectangle(TrainRect[3].X + 55, TrainRect[3].Y + 20, 25, 20);
            BianRect[3] = new Rectangle(TrainRect[5].X + 55, TrainRect[5].Y + 20, 25, 20);
            BianRect[4] = new Rectangle(TrainRect[7].X + 55, TrainRect[7].Y + 20, 25, 20);
            #endregion

            #region ::::::::::::::::::断 路 器 位 置 设 置:::::::::::::::::
            LcbRect[0] = new Rectangle(TrainRect[1].X + 10, TrainRect[0].Y + 20, 25, 20);
            LcbRect[1] = new Rectangle(TrainRect[1].X + 45, TrainRect[2].Y, 25, 20);
            LcbRect[2] = new Rectangle(TrainRect[4].X + 35, TrainRect[3].Y + 20, 25, 20);
            LcbRect[3] = new Rectangle(TrainRect[6].X + 10, TrainRect[5].Y + 20, 25, 20);
            LcbRect[4] = new Rectangle(TrainRect[6].X + 45, TrainRect[7].Y, 25, 20);
            #endregion

            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 240, 790, 80);

            #endregion
        }
        public void DrawOn(Graphics g)
        {
            //菜单标题栏绘制
            Title.OnDraw(g);
            //页脚区域绘制
            g.DrawImage(Image[10], m_Rect);



            //绘制列车 及其编号 
            g.DrawImage(Image[11], TrainRect[0]);
            g.DrawString("01", Strfont, StrBrush, TrainRect[0].X + 60, TrainRect[0].Y - 10);
            for (int i = 1; i < 7; i++)
            {
                g.DrawImage(Image[12], TrainRect[i]);
                g.DrawString("0" + (i + 1).ToString(), Strfont, StrBrush, TrainRect[i].X + 60, TrainRect[i].Y - 12);
            }
            g.DrawImage(Image[13], TrainRect[7]);
            g.DrawString("00", Strfont, StrBrush, TrainRect[7].X + 50, TrainRect[7].Y - 12);

            //绘制受电弓
            for (int i = 0; i < 2; i++)
            {
                if (Valueb[i])
                {
                    g.DrawImage(Image[0], HighVoltageRect[i]);//受电弓上
                }
                else
                {
                    g.DrawImage(Image[1], HighVoltageRect[i]);//受电弓下
                }
                if (!Valueb[i] && Valueb[i + 2])
                {
                    g.DrawImage(Image[2], HighVoltageRect[i]);
                }
            }

            //绘制蓄电池
            for (int i = 0; i < 5; i++)
            {
                if (Valueb[i + 4])
                {
                    g.DrawImage(Image[3], BatteryRect[i]);
                }
                else
                {
                    g.DrawImage(Image[4], BatteryRect[i]);
                }
            }

            //绘制变流器
            for (int i = 0; i < 5; i++)
            {
                if (Valueb[i + 9])
                {
                    g.DrawImage(Image[5], BianRect[i]);
                }
                else
                {
                    g.DrawImage(Image[6], BianRect[i]);
                }
            }
            //绘制断路器
            for (int i = 0; i < 5; i++)
            {
                if (Valueb[i + 14])
                {
                    g.DrawImage(Image[7], LcbRect[i]);
                }
                else
                {
                    g.DrawImage(Image[8], LcbRect[i]);
                }
                if (!Valueb[i + 14] && Valueb[i + 19])
                {
                    g.DrawImage(Image[9], LcbRect[i]);
                }
            }
        }




    }
}
