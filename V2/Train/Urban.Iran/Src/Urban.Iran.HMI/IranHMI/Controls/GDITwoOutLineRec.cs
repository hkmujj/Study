using System.Drawing;
using CommonUtil.Controls;

namespace Urban.Iran.HMI.Controls
{
    public class GDITwoOutLineRec : GDIRectText
    {

        public GDITwoOutLineRec(int length)
        {
            Length = length;
            FillOutRectangle = false;
            OutLineChanged += (sender, args) =>
            {
                OutRectangleTwoLine = Rectangle.Inflate(OutLineRectangle, -Length, -Length);
            };
        }
        /// <summary>
        /// 是否填充外边框 ture 是,false 否
        /// </summary>
        public bool FillOutRectangle { get; set; }

        public int Length { get; private set; }


        public Rectangle OutRectangleTwoLine { get; private set; }

        public override void OnPaint(Graphics g)
        {
            Refresh();
            OnDraw(g);
        }

        public override void OnDraw(Graphics g)
        {
            if (Visible)
            {
                if (BackColorVisible)
                {

                    g.FillRectangle(new SolidBrush(BkColor), FillOutRectangle ? OutLineRectangle : OutRectangleTwoLine);
                }
                g.DrawRectangle(OutLinePen, OutRectangleTwoLine);
                g.DrawRectangle(OutLinePen, OutLineRectangle);
                g.DrawString(Text, DrawFont, new SolidBrush(TextColor), OutRectangleTwoLine, TextFormat);
            }
        
        }

    }
}
