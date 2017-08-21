using System.Drawing;
using Urban.Iran.DMI.Controls;
using Urban.Iran.DMI.Index.IndexKeys;

namespace Urban.Iran.DMI
{
    public class GdiCommon
    {
        public static readonly Font Txt10Font = new Font("Arial", 10);//, FontStyle.Bold)     (254, 84, 84)   (254, 254, 84) (254, 128, 0) (168, 168, 168)
        public static readonly Font Txt12Font = new Font("Arial", 12, FontStyle.Bold);
        public static readonly Font Txt14Font = new Font("Arial", 14);//, FontStyle.Bold);
        public static readonly Font Txt15Font = new Font("Arial", 15);//, FontStyle.Bold);
        public static readonly Font Txt18Font = new Font("Arial", 18);//, FontStyle.Bold);
        public static readonly Font Txt22Font = new Font("Arial", 22);//, FontStyle.Bold);
        public static readonly Font Txt25Font = new Font("Arial", 25);//, FontStyle.Bold);
        public static readonly Font Txt28Font = new Font("Arial", 28);//, FontStyle.Bold);

        public static readonly Pen DarkBluePen = new Pen(Color.FromArgb(3, 17, 34));                 //背景
        public static readonly Pen DarkGreyPen = new Pen(Color.FromArgb(40, 40, 40));                //按钮
        public static readonly Pen RedPen = new Pen(Color.FromArgb(191, 0, 2));                      //错误
        public static readonly Pen YellowPen = new Pen(Color.FromArgb(223, 223, 0));                 //警告
        public static readonly Pen GreyBluePen = new Pen(Color.FromArgb(81, 91, 109));               //通常操作
        public static readonly Pen OceanBluePen = new Pen(Color.FromArgb(90, 90, 150));              //开、激活、成功、标题
        public static readonly Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150));           //文本
        public static readonly Pen GrayWhitePen = new Pen(Color.FromArgb(198, 196, 194));

        public static readonly Pen WhitePen = new Pen(Color.White);
        public static readonly Pen OrangePen = new Pen(Color.FromArgb(254, 128, 0), 8);
        public static readonly Pen PinkPen = new Pen(Color.FromArgb(254, 84, 84), 8);
        public static readonly Pen LightYellowPen = new Pen(Color.FromArgb(254, 254, 84), 8);
        public static readonly Pen GreenPen = new Pen(Color.Green, 8);


        public static readonly SolidBrush DarkBlueBrush = new SolidBrush(Color.FromArgb(3, 17, 34));         //背景   
        public static readonly SolidBrush DarkGreyBrush = new SolidBrush(Color.FromArgb(40, 40, 40));        //按钮
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));              //错误
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(223, 223, 0));         //警告
        public static readonly SolidBrush GreyBrush = new SolidBrush(Color.FromArgb(81, 91, 109));           //通常操作
        public static readonly SolidBrush OceanBlueBrush = new SolidBrush(Color.FromArgb(90, 90, 150));      //开、激活、成功、标题
        public static readonly SolidBrush MediumGreyBrush = new SolidBrush(Color.FromArgb(150, 150, 150));   //文本
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.White);
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.Black);
        public static readonly SolidBrush GirdHeadBrush = new SolidBrush(Color.FromArgb(40, 40, 40));
        public static readonly SolidBrush PinkBrush = new SolidBrush(Color.FromArgb(254, 84, 84));
        public static readonly SolidBrush BkgBrush = new SolidBrush(Color.FromArgb(22, 22, 22));


        public static readonly StringFormat CenterFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        public static readonly StringFormat LeftFormat = new StringFormat();
    }

    public delegate void OnMouseDownEx(FjButtonEx btnSender, Point pt);
    public delegate void OnLostFoucse(FjButtonEx btnSender, Point pt);
}
