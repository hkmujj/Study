using System.Drawing;
using System.Drawing.Drawing2D;
using Motor.HMI.CRH1A.Common;

namespace Motor.HMI.CRH1A.Train
{
    public class TrainBase : CRH1BaseClass
    {
        public Rectangle Recposition = new Rectangle(20, 20, 100, 100);
        public Pen WhitePen1 = new Pen(Color.FromArgb(255, 255, 255));//白色画笔
        public Pen WhitePen2 = new Pen(Color.FromArgb(240, 240, 240));//淡白色画笔
        public Pen BluePen = new Pen(Color.FromArgb(48, 128, 168), 3);
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));//白色画刷
        public SolidBrush Blue1Brush = new SolidBrush(Color.FromArgb(48, 128, 168));//较深蓝色画刷 用于绘制车身
        public SolidBrush Blue2Brush = new SolidBrush(Color.FromArgb(56, 144, 192));//较浅蓝色画刷 用于绘制各节车厢
        public SolidBrush Blackbrush = new SolidBrush(Color.FromArgb(0, 0, 0));//黑色画刷
        public Font Font = new Font("Arial", 11);
        public GraphicsPath Path;//整车
        public GraphicsPath Path01;//车厢1
        public GraphicsPath Path00;//车厢8           
        public Rectangle[] TrainRect = new Rectangle[6];//中间六节车厢 
        public Point[] Point1 = new Point[9];
        public Point[] Point2 = new Point[7];
    }
}