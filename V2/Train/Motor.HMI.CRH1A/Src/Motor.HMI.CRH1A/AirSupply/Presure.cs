using System.Drawing;

namespace Motor.HMI.CRH1A
{
    public class Presure   //ѹ��������
    {
        public Rectangle Rect;
      
        public Pen LbluePen = new Pen(Color.FromArgb(0, 255, 255), 3);//����ɫ���� 
        public Pen SbluePen = new Pen(Color.FromArgb(0, 255, 255), 2);//ϸ��ɫ���� 
        public Pen LgrayPen = new Pen(Color.FromArgb(135, 135, 135),3);//�ֻ�ɫ���� 
        public Pen SgrayPen = new Pen(Color.FromArgb(135, 135, 135),2);//ϸ��ɫ���� 
        public Pen WhitePen = new Pen(Color.FromArgb(235,235, 235),2);//��ɫ���� 
        public SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(135, 135,135));//��ɫ��ˢ
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));//��ɫ��ˢ
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(234, 235, 236));//��ɫ��ˢ
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 255, 255));//��ɫ��ˢ
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//��ɫ��ˢ
        public Font Font = new Font("Arial", 11);
        public enum Status
        {
            Well,
            Fault,
            CutOut,
            Unknow
        };

        public Status S;//����ѹ����������״̬ ���� �ر� δ֪
        public Point[] Point = new Point[2];
        public Presure(int x, int y, int weight, int height)
        {
            Rect = new Rectangle(x, y, weight, height);
            Point[0] = new Point(Rect.X , Rect.Y+Rect.Height);
            Point[1] = new Point(Rect.X +Rect.Width, Rect.Y);
            

        }
        public void OnDraw(Graphics g)
        {
            switch (S)
            {
                case Status.Well:
                    g.FillRectangle(GreenBrush, Rect);
                    g.DrawLine(WhitePen, Point[0], Point[1]);
                    g.DrawString("I", Font,WhiteBrush, Rect.X + 8, Rect.Y + 8);
                    g.DrawString("P", Font, WhiteBrush, Rect.X + 22, Rect.Y + 22);
                    break;
                case Status.Fault:
                    g.FillRectangle(RedBrush, Rect);
                    g.DrawLine(WhitePen, Point[0], Point[1]);
                    g.DrawString("I", Font, WhiteBrush, Rect.X + 8, Rect.Y + 8);
                    g.DrawString("P", Font, WhiteBrush, Rect.X + 22, Rect.Y + 22);
                    break;
                case Status.CutOut:
                    g.DrawLine(SbluePen, Point[0], Point[1]);
                    g.DrawRectangle(LbluePen, Rect);
                    g.DrawString("I", Font, BlueBrush, Rect.X + 8, Rect.Y + 8);
                    g.DrawString("P", Font, BlueBrush, Rect.X + 22, Rect.Y + 22);
                    break;
                case Status.Unknow:
                    g.DrawLine(SgrayPen, Point[0], Point[1]);
                    g.DrawRectangle(SgrayPen, Rect);
                    g.DrawString("I", Font,GrayBrush, Rect.X + 8, Rect.Y + 8);
                    g.DrawString("P", Font, GrayBrush, Rect.X + 22, Rect.Y + 22);
                    break;
            }
        }
    }
}