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
    class GT_Front : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("前端");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 100, 800, 100);


        public Rectangle[] Rect = new Rectangle[2];//左右车的位置
        public bool[] Valueb = new bool[8];
        public Image[] Image = new Image[10];
        private Font m_Font = new Font("Arial", 12, FontStyle.Bold);
        private SolidBrush m_StringBrush = new SolidBrush(Color.FromArgb(75, 75, 75));

        private bool[] m_IsDriverRoom = new bool[2];

        public override string GetInfo()
        {
            return "前端";
        }

        public override bool Initialize()
        {
            //3
            InitData();


            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

            for (int index = 0; index < 2; index++)
            {
                m_IsDriverRoom[index] = BoolList[UIObj.InBoolList[index + 8]];
            }

        }
        public void InitData()
        {
            if (UIObj.ParaList.Count >= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    Image[i] = System.Drawing.Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                }
            }

            Rect[0] = new Rectangle(Recposition.X + 100, Recposition.Y + 150, 200, 90);
            Rect[1] = new Rectangle(Recposition.X + 290, Recposition.Y + 150, 200, 90);

        }
        public void DrawOn(Graphics g)
        {
            //绘制菜单标题
            Title.OnDraw(g);
            #region 车钩状态

            if (Valueb[6])
            {
                g.DrawImage(Image[1], Rect[0]);//故障状态优先级设置为最高
            }
            else if (Valueb[0])//车钩处于工作状态
            {
                g.DrawImage(Image[0], Rect[0]);
            }
            else if (Valueb[2])//车钩处于关闭状态
            {
                g.DrawImage(Image[4], Rect[0]);
            }
            else if (Valueb[4])//车钩处于伸长并锁定状态
            {
                g.DrawImage(Image[3], Rect[0]);
            }
            else                      //当以上所有状态均未出现时表示处于状态不明的状态
            {
                g.DrawImage(Image[2], Rect[0]);
            }

            if (Valueb[7])
            {
                g.DrawImage(Image[6], Rect[1]);//故障状态优先级设置为最高
            }
            else if (Valueb[1])//车钩处于工作状态
            {
                g.DrawImage(Image[5], Rect[1]);
            }
            else if (Valueb[3])//车钩处于关闭状态
            {
                g.DrawImage(Image[9], Rect[1]);
            }
            else if (Valueb[5])//车钩处于伸长并锁定状态
            {
                g.DrawImage(Image[8], Rect[1]);
            }
            else                      //当以上所有状态均未出现时表示处于状态不明的状态
            {
                g.DrawImage(Image[7], Rect[1]);
            }
            #endregion

            g.DrawString("001", m_Font, m_StringBrush, Rect[0].X + 35, Rect[0].Y + 20);

            if (m_IsDriverRoom[0])
            {
                g.FillEllipse(Brushes.Black, new Rectangle(Rect[0].X + 64, Rect[0].Y + 35, 8, 8));
            }
            else if (m_IsDriverRoom[1])
            {
                g.FillEllipse(Brushes.Black, new Rectangle(Rect[1].X + 125, Rect[1].Y + 35, 8, 8));
            }
        }

    }
}
