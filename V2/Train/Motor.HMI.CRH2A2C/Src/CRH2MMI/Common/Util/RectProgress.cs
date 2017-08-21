using System.Drawing;

namespace CRH2MMI.Common.Util
{
    /// <summary>
    /// 矩形框 样式设置
    /// </summary>
    public class RectProgress
    {
        public RectangleF RectPosition;
        public SolidBrush PRBrush;
        public float X;
        public float Y;
        
        public void SetRect( float height)
        {
            RectPosition.X = X;
            RectPosition.Y = Y - height;
            RectPosition.Height = height;
 
        }

        public void SetRectProgress(float height, float x, float y, float width,int color)
        {
            X = x;
            Y = y;
            RectPosition.Width = width;
            SetRect(height);

            if (color == 0)
            {
                PRBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
            }
            else if (color == 1)
            {
                PRBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
            }
            else if (color == 2)
            {
                PRBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
            }
        }


        public virtual void OnDraw(Graphics e)
        {

            if (PRBrush != null)
            {
                e.FillRectangle(PRBrush, RectPosition.X, RectPosition.Y ,
                    RectPosition.Width, RectPosition.Height);

            }

        }
    }
}