using System;
using System.Drawing;

namespace SS4B_TMS.Common
{
    public class SS4BButton
    {
        public event Action MouseDown;

        public event Action MouseUp;

        public SS4BButton()
        {
            TextFont = new Font("宋体", 12);
        }

        public string Text { get; set; }
        public Font TextFont { get; set; }
        public Image BackIamge { get; set; }
        public Image MoseDownImage { get; set; }
        public Image MoseUpImage { get; set; }
        public Brush TextBrush { get; set; }
        public StringFormat TextFormat { get; set; }
        public Rectangle OutRec { get; set; }

        public bool OnMouseDown(Point point)
        {
            if (OutRec.Contains(point))
            {
                OnMouseDown();
                BackIamge = MoseDownImage;
                return true;
            }
            return false;
        }

        public void OnMouseUp(Point point)
        {
            if (OutRec.Contains(point))
            {
                OnMouseUp();
                BackIamge = MoseUpImage;
            }
        }

        protected virtual void OnMouseDown()
        {
            MouseDown?.Invoke();
        }

        protected virtual void OnMouseUp()
        {
            MouseUp?.Invoke();
        }

        public void OnDraw(Graphics g)
        {
            g.DrawImage(BackIamge, OutRec);
            g.DrawString(Text, TextFont, TextBrush, OutRec, TextFormat);
        }
    }
}