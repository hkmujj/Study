using System.Drawing;

namespace Urban.Iran.HMI.Common
{
    public static class GdiCommon
    {
        public static readonly Font Txt8Font = new Font("Arial", 8);
        public static readonly Font Txt9Font = new Font("Arial", 9);
        public static readonly Font Txt10Font = new Font("Arial", 10);//, FontStyle.Bold)

        public static readonly Font Txt12Font = new Font("Arial", 12, FontStyle.Bold);
        public static readonly Font Txt14Font = new Font("Arial", 14);//, FontStyle.Bold);
        public static readonly Font Txt15Font = new Font("Arial", 15);//, FontStyle.Bold);
        public static readonly Font Txt18Font = new Font("Arial", 18);//, FontStyle.Bold);
        public static readonly Font Txt20Font = new Font("Arial", 20);//, FontStyle.Bold);
        public static readonly Font Txt22Font = new Font("Arial", 22);//, FontStyle.Bold);
        public static readonly Font Txt25Font = new Font("Arial", 25);//, FontStyle.Bold);
        public static readonly Font Txt28Font = new Font("Arial", 28);//, FontStyle.Bold);


        public static readonly Font Txt12FontBold = new Font("Arial", 12, FontStyle.Bold);
        public static readonly Font Txt14FontBold = new Font("Arial", 14, FontStyle.Bold);
        public static readonly Font Txt15FontBold = new Font("Arial", 15, FontStyle.Bold);
        public static readonly Font Txt18FontBold = new Font("Arial", 18, FontStyle.Bold);
        public static readonly Font Txt22FontBold = new Font("Arial", 22, FontStyle.Bold);
        public static readonly Font Txt25FontBold = new Font("Arial", 25, FontStyle.Bold);
        public static readonly Font Txt28FontBold = new Font("Arial", 28, FontStyle.Bold);

        public static readonly Pen DarkBluePen = new Pen(Color.FromArgb(3, 17, 34));                 //背景
        public static readonly Pen DarkGreyPen = new Pen(Color.FromArgb(40, 40, 40));                //按钮
        public static readonly Pen RedPen = new Pen(Color.FromArgb(191, 0, 2));                      //错误
        public static readonly Pen YellowPen = new Pen(Color.FromArgb(223, 223, 0));                 //警告
        public static readonly Pen GreyBluePen = new Pen(Color.FromArgb(81, 91, 109));               //通常操作
        public static readonly Pen OceanBluePen = new Pen(Color.FromArgb(90, 90, 150));              //开、激活、成功、标题
        public static readonly Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150));           //文本
        public static readonly Pen GrayWhitePen = new Pen(Color.FromArgb(198, 196, 194));
        public static readonly Pen OrangePen = new Pen(Color.FromArgb(232, 146, 0));
        public static readonly Pen WhitePen = new Pen(Color.White);
        public static readonly Pen BlackPen = new Pen(Color.Black);


        public static readonly SolidBrush DarkBlueBrush = new SolidBrush(Color.FromArgb(3, 17, 34));         //背景   
        public static readonly SolidBrush DarkGreyBrush = new SolidBrush(Color.FromArgb(40, 40, 40));        //按钮
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));              //错误
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(223, 223, 0));         //警告
        public static readonly SolidBrush GreyBrush = new SolidBrush(Color.FromArgb(81, 91, 109));           //通常操作
        public static readonly SolidBrush OceanBlueBrush = new SolidBrush(Color.FromArgb(90, 90, 180));      //开、激活、成功、标题
        public static readonly SolidBrush MediumGreyBrush = new SolidBrush(Color.FromArgb(150, 150, 150));   //文本
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.White);
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.Black);
        public static readonly SolidBrush GridHeadBrush = new SolidBrush(Color.FromArgb(40, 40, 40));
        public static readonly SolidBrush StartViewBk = new SolidBrush(Color.FromArgb(240, 228, 216));
        public static readonly SolidBrush StartViewGray = new SolidBrush(Color.FromArgb(137, 140, 159));
        public static readonly SolidBrush LightBlueBrush = new SolidBrush(Color.FromArgb(119, 165, 227)); 
        public static readonly SolidBrush GrayWhiteBrush=new SolidBrush(Color.FromArgb(198, 196, 194));
        /// <summary>
        /// 事件消息的标题
        /// </summary>
        public static readonly SolidBrush EventTitleBrush = new SolidBrush(Color.FromArgb(57, 33, 29));

        private static Bitmap m_BtnBkBitmap;
        public static Bitmap BtnBkBitmap
        {
            get { return m_BtnBkBitmap ?? (m_BtnBkBitmap = new Bitmap(GlobleView.Instance.RecPath + "\\frame/btnBkNormal.jpg")); }
        }

        public static Bitmap BtnRightArrawImage
        {
            get { return m_BtnRightArrawImage ?? (m_BtnRightArrawImage = new Bitmap(GlobleView.Instance.RecPath + "\\frame/ButtonRightArrow.png")); }
        }

        public static Bitmap BtnLeftArrawImage
        {
            get { return m_BtnLeftArrawImage ?? (m_BtnLeftArrawImage = new Bitmap(GlobleView.Instance.RecPath + "\\frame/ButtonLeftArrow.png")); }
        }

        public static StringFormat m_StrFormat = new StringFormat();

        public static readonly StringFormat CenterFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public static readonly StringFormat LeftFormat = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        public static readonly StringFormat RightFormat = new StringFormat()
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Center
        };
        private static Bitmap m_BtnLeftArrawImage;
        private static Bitmap m_BtnRightArrawImage;
    }
}