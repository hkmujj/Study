using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Motor.HMI.CRH1A.Traction
{
    public class MCM1 : MCM
    {
        public MCM1(int x, int y, int weight, int height)
            : base(x, y, weight, height)
        {
            Content = "1";
            Type = EquipType.Mcm1;
        }
    }

    public class MCM2 : MCM
    {
        public MCM2(int x, int y, int weight, int height)
            : base(x, y, weight, height)
        {
            Content = "2";
            Type = EquipType.Mcm2;
        }
    }

    public class MCM: TractionEquipMemtBase
    {

        public MCM(int x, int y, int weight, int height)
            : base(new Rectangle(x, y, weight, height))
        {
            
        }

        /// <summary>
        /// 内容,
        /// </summary>
        protected string Content { set; get; }

        public override void OnDraw(Graphics g)
        {
            SolidBrush contentBrush = WhiteBrush;

            switch (Status)
            {
                case EquipStatus.Active: //激活状态
                    g.FillRectangle(GreenBrush, OutLineOutLineRectangle);
                    contentBrush = WhiteBrush;
                    //g.DrawString(Content, strfont, white_Brush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 5);
                    //g.DrawString("1", strfont, white_Brush, OutLineOutLineRectangle.X + 15, OutLineOutLineRectangle.Y + 23);
                    break;
                case EquipStatus.Well://完好状态
                    g.DrawRectangle(GreenPen, OutLineOutLineRectangle);
                    contentBrush = GreenBrush;
                    //g.DrawString("MCM", strfont, green_Brush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 5);
                    //g.DrawString("1", strfont, green_Brush, OutLineOutLineRectangle.X + 15, OutLineOutLineRectangle.Y + 23);
                    break;
                case EquipStatus.Fault://出现故障
                    g.DrawRectangle(RedPen, OutLineOutLineRectangle);
                    contentBrush = RedBrush;
                    //g.DrawString("MCM", strfont, red_Brush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 5);
                    //g.DrawString("1", strfont, red_Brush, OutLineOutLineRectangle.X + 15, OutLineOutLineRectangle.Y + 23);
                    break;
                case EquipStatus.CutOut:
                    g.DrawRectangle(BluePen, OutLineOutLineRectangle);
                    contentBrush = BlueBrush;
                    //g.DrawString("MCM", strfont, blue_Brush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 5);
                    //g.DrawString("1", strfont, blue_Brush, OutLineOutLineRectangle.X + 15, OutLineOutLineRectangle.Y + 23);
                    break;
                case EquipStatus.Unknow://未知状态
                    g.DrawRectangle(GrayPen, OutLineOutLineRectangle);
                    contentBrush = GrayBrush;
                    //g.DrawString("MCM", strfont, gray_Brush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 5);
                    //g.DrawString("1", strfont, gray_Brush, OutLineOutLineRectangle.X + 15, OutLineOutLineRectangle.Y + 23);
                    break;
            }

            g.DrawString("MCM", Strfont, contentBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 5);
            g.DrawString(Content, Strfont, contentBrush, OutLineOutLineRectangle.X + 15, OutLineOutLineRectangle.Y + 23);

            base.OnDraw(g);
        }

    }
}
