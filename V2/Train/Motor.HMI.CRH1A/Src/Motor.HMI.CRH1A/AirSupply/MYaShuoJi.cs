using System.Drawing;

namespace Motor.HMI.CRH1A
{
    public class MYaShuoJi  //Ö÷Ñ¹Ëõ»ú
    {
        public Rectangle Rect;
        public Pen LgreenPen=new Pen(Color.FromArgb(0,128,0),3);//´ÖÂÌÉ«»­±Ê 
        public Pen SgreenPen=new Pen(Color.FromArgb(0,128,0),2);//Ï¸ÂÌÉ«»­±Ê 
        public Pen LbluePen = new Pen(Color.FromArgb(0, 255, 255), 3);//´ÖÀ¶É«»­±Ê 
        public Pen SbluePen = new Pen(Color.FromArgb(0, 255, 255), 2);//Ï¸À¶É«»­±Ê 
        public Pen LgrayPen = new Pen(Color.FromArgb(135, 135, 135),3);//´Ö»ÒÉ«»­±Ê 
        public Pen SgrayPen = new Pen(Color.FromArgb(135, 135, 135),2);//Ï¸»ÒÉ«»­±Ê 
        public Pen WhitePen = new Pen(Color.FromArgb(235,235, 235),2);//°×É«»­±Ê 
        public SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(135, 135,135));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(234, 235, 236));
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 255, 255));
        public Font Font = new Font("Arial", 10);
        public enum Status
        {
            Working,
            Closed,
            CutOut,
            Unknow
        };

        public Status S=Status.Working;//Ö÷Ñ¹Ëõ»úµÄËÄÖÖ×´Ì¬ ¹¤×÷ ¹Ø±Õ ÇÐ¶Ï Î´Öª
        public Point[] Point = new Point[6];
        public MYaShuoJi(int x, int y, int weight, int height)
        {
            Rect = new Rectangle(x, y, weight, height);
            Point[0] = new Point(Rect.X + 8, Rect.Y + 8);
            Point[1] = new Point(Rect.X + 33, Rect.Y + 16);
            Point[2] = new Point(Rect.X + 8, Rect.Y + 32);
            Point[3] = new Point(Rect.X + 33, Rect.Y + 24);
            Point[4] = new Point(Rect.X + 35, Rect.Y + 20);
            Point[5] = new Point(Rect.X + 40, Rect.Y + 20);

        }
        public void OnDraw(Graphics g)
        {
            switch (S)
            {
                case Status.Working:
                    g.FillRectangle(GreenBrush, Rect);
                    g.DrawEllipse(WhitePen, Rect.X + 5, Rect.Y + 5, Rect.Width - 10, Rect.Height - 10);
                    g.DrawLine(WhitePen, Point[0], Point[1]);
                    g.DrawLine(WhitePen, Point[2], Point[3]);
                    g.DrawLine(WhitePen, Point[4], Point[5]);
                    g.DrawString("M", Font,WhiteBrush, Rect.X + 8, Rect.Y + 12);
                    break;
                case Status.Closed:
                    g.DrawEllipse(SgreenPen, Rect.X + 5, Rect.Y + 5, Rect.Width - 10, Rect.Height - 10);
                    g.DrawLine(SgreenPen, Point[0], Point[1]);
                    g.DrawLine(SgreenPen, Point[2], Point[3]);
                    g.DrawLine(SgreenPen, Point[4], Point[5]);
                    g.DrawRectangle(LgreenPen, Rect);
                    g.DrawString("M", Font, GreenBrush, Rect.X + 8, Rect.Y + 12);
                    break;
                case Status.CutOut:
                    g.DrawEllipse(SbluePen, Rect.X + 5, Rect.Y + 5, Rect.Width - 10, Rect.Height - 10);
                    g.DrawLine(SbluePen, Point[0], Point[1]);
                    g.DrawLine(SbluePen, Point[2], Point[3]);
                    g.DrawLine(SbluePen, Point[4], Point[5]);
                    g.DrawRectangle(LbluePen, Rect);
                    g.DrawString("M", Font,BlueBrush, Rect.X + 8, Rect.Y + 12);
                    break;
                case Status.Unknow:
                    g.DrawEllipse(LgrayPen, Rect.X + 5, Rect.Y + 5, Rect.Width - 10, Rect.Height - 10);
                    g.DrawLine(SgrayPen, Point[0], Point[1]);
                    g.DrawLine(SgrayPen, Point[2], Point[3]);
                    g.DrawLine(LgrayPen, Point[4], Point[5]);
                    g.DrawRectangle(SgrayPen, Rect);
                    g.DrawString("M", Font, GrayBrush, Rect.X + 8, Rect.Y + 12);
                    break;
            }
        }
    }
}