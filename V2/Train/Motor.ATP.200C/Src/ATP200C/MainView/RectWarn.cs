using System.Drawing;

namespace ATP200C.MainView
{
    /// <summary>
    /// 制动预警图标
    /// </summary>
    public class RectWarn
    {
        public SolidBrush BKBrush;
        public RectangleF RectPosition;

        public void SetBKColor(int r, int g, int b)
        {
            BKBrush = new SolidBrush(Color.FromArgb(r, g, b));
        }

        public void SetRect(float width)
        {
            RectPosition.X = 32 - width/2f;
            RectPosition.Y = 32 - width / 2f;
            RectPosition.Height = width;
            RectPosition.Width = width;
        }

        public void OnDraw(Graphics e)
        {
            if (BKBrush != null)
            {
                e.FillRectangle(BKBrush, RectPosition.X, RectPosition.Y,
                    RectPosition.Width, RectPosition.Height);
            }
        }
    }
}