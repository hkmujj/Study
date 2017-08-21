using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_MMI
{

    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_Starting : baseClass
    {

        private IList<Image> m_Image;
        private bool DrawOne;
        private bool DrawSecond;
        private bool DrawThird;
        private bool DrawFourth;
        private DateTime StarTime;
        private const double Secend = 0.2;
        private const double Third = 3;
        private const double Fourth = 5;
        private RectangleF Rectangle;
        private static Brush m_ForColor = new SolidBrush(Color.FromArgb(0, 89, 188));
        private static Brush m_BackColor = new SolidBrush(Color.FromArgb(63, 209, 255));
        private Rectangle recOne;
        private Rectangle recTwo;
        public override bool init(ref int nErrorObjectIndex)
        {
            Rectangle = new RectangleF(0, 0, 800, 600);
            m_Image = new List<Image>();
            recOne = new Rectangle(197, 394, 400, 16);
            recTwo = new Rectangle(197, 394, 400, 16);
            foreach (var str in UIObj.ParaList)
            {
                m_Image.Add(Image.FromFile(Path.Combine(RecPath, str)));
            }
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                StarTime = this.NowTime();
                Index = 0;
                IsPlus = true;
                DrawOne = true;
                DrawSecond = false;
                DrawThird = false;
                DrawFourth = false;
            }
            else
            {
                var now = this.NowTime();
                if ((now - StarTime).Ticks / 10000000 >= Secend)
                {
                    DrawSecond = true;
                }
                if ((now - StarTime).Seconds >= Third)
                {
                    DrawThird = true;
                }
                if ((now - StarTime).Seconds >= Fourth)
                {
                    DrawFourth = true;
                }
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        public override string GetInfo()
        {
            return "MMI_Starting";
        }

        public override void paint(Graphics g)
        {
            if (DrawFourth)
            {
                append_postCmd(CmdType.ChangePage, 4, 0, 0);
            }
            else if (DrawThird)
            {
                g.DrawImage(m_Image[0], Rectangle);
                g.FillRectangle(m_BackColor, recOne);
                g.FillRectangle(m_ForColor, Calculate());
            }
            else if (DrawSecond)
            {
                g.FillRectangle(Brushes.Black, Rectangle);
            }
            else if (DrawOne)
            {
                DrawOne = false;
                g.DrawImage(m_Image[1], Rectangle);
            }

            base.paint(g);
        }

        private Rectangle Calculate()
        {
            Rectangle rectangle = new Rectangle();
            if (IsPlus)
            {
                Index += 10;
                rectangle = new Rectangle(recOne.X, recOne.Y, Index * recOne.Width / 100,
                  recOne.Height);
            }
            if (!IsPlus)
            {
                Index -= 10;
                rectangle = new Rectangle(recOne.X + (int)((1d - (double)Index / 100d) * recOne.Width), recOne.Y, (int)(((double)Index / 100d) * recOne.Width),
                 recOne.Height);
            }
            if (Index >= 100)
            {
                IsPlus = false;
            }
            else if (Index <= 0)
            {
                IsPlus = true;
            }

            return rectangle;
        }

        private bool IsPlus = true;
        private int Index = 0;
    }
}