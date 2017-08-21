using System.Drawing;

namespace Engine.HMI.HXD1C.TPX21A
{
    public class HxCommon
    {
        public static readonly Rectangle Recposition = new Rectangle(0, 0, 800, 600);
        //公共信息显示区
        public static readonly Rectangle RectPublic = new Rectangle(Recposition.X, Recposition.Y, 800, 27);
        //故障信息显示区
        public static readonly Rectangle RectDefault = new Rectangle(Recposition.X, Recposition.Y + 500, 800, 40);

        #region ;;;;;;;显示标题文本框 制动信息区 信息提示区 故障信息区 故障等级;;;;;;;;;;;;;

        public static readonly HxRectText HTitle = new HxRectText();
        public static readonly HxRectText[] HDefault = new HxRectText[4]; //故障区四个文本框 依次为制动信息区 信息提示区 故障信息区 故障等级

        #endregion

        //底部导航文本框
        public static readonly HxRectText[] ButtonText = new HxRectText[10];
        public static readonly Rectangle ButtonInfo = new Rectangle(Recposition.X + 3, Recposition.Y + 547, 81, 50); //按键提示区

        #region ;;;;;;构造函数初始化部分;;;;;;;;;;;

        public HxCommon()
        {
            HTitle.SetBkColor(0, 0, 0);
            HTitle.SetTextColor(255, 255, 255);
            HTitle.SetTextStyle(13, FormatStyle.Center, true, "宋体");
            HTitle.SetTextRect(Recposition.X + 130, Recposition.Y + 3, 135, 24);
            // H_Title.isdrawrectfrm = true;

            //故障区文本框初始化
            for (int i = 0; i < 4; i++)
            {
                HDefault[i] = new HxRectText();
                HDefault[i].SetBkColor(0, 0, 0);
                HDefault[i].SetTextColor(0, 0, 0);
                HDefault[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                if (i < 3)
                {
                    HDefault[i].SetTextRect(Recposition.X + i*242 + 4, RectDefault.Y - 7, 239, 50);
                }
                else
                {
                    HDefault[i].SetTextRect(Recposition.X + 730, RectDefault.Y - 7, 60, 50);
                }
                HDefault[i].SetLinePen(84, 84, 84, 2);
                HDefault[i].m_Isdrawrectfrm = true;

            }



            //底部导航文本框初始化
            for (int i = 0; i < 10; i++)
            {
                ButtonText[i] = new HxRectText();
                ButtonText[i].SetTextColor(255, 255, 255);
                ButtonText[i].SetTextStyle(14, FormatStyle.Center, true, "宋体");
                ButtonText[i].SetTextRect(Recposition.X + 90 + 71*i, Recposition.Y + 547, 63, 50);
                ButtonText[i].SetLinePen(84, 84, 84, 2);
                ButtonText[i].SetDrawFrm(true);
            }
        }


        #endregion

        #region ;;;;;;;可 重 用 的 画 笔;;;;;;;;;

        public static readonly Pen WhitePen1 = new Pen(Color.FromArgb(255, 255, 255), 1);
        public static readonly Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
        public static readonly Pen LinePen1 = new Pen(Color.FromArgb(84, 84, 84), 1);
        public static readonly Pen LinePen2 = new Pen(Color.FromArgb(84, 84, 84), 2);
        public static readonly Pen LightWhitePen1 = new Pen(Color.FromArgb(211, 211, 211), 1);
        public static readonly Pen LightWhitePen2 = new Pen(Color.FromArgb(211, 211, 211), 2);
        public static readonly Pen RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        public static readonly Pen GreenPen = new Pen(Color.FromArgb(0, 255, 0), 2);
        public static readonly Pen BluePen = new Pen(Color.FromArgb(0, 0, 255), 2);

        #endregion

        #region ;;;;;;;;;;可 重 用 的 字 体;;;;;;;;;;;;

        public static readonly Font Font12 = new Font("宋体", 12);
        public static readonly Font Font10B = new Font("宋体", 10, FontStyle.Bold);
        public static readonly Font Font12B = new Font("宋体", 12, FontStyle.Bold);
        public static readonly Font Font14B = new Font("宋体", 14, FontStyle.Bold);
        public static readonly Font Font14 = new Font("宋体", 14);
        public static readonly Font Font16B = new Font("宋体", 16, FontStyle.Bold);
        public static readonly Font Font24B = new Font("宋体", 24, FontStyle.Bold);


        #endregion

        #region ;;;;;;;;;;可 重 用 的 画 刷;;;;;;;;;;;

        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255)); //白色画刷
        public static readonly SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(192, 192, 192)); //灰色画刷
        public static readonly SolidBrush DeadBrush = new SolidBrush(Color.FromArgb(84, 84, 84)); //暗色画刷
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255)); //灰色画刷
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0)); //灰色画刷
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0)); //黑色画刷
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0)); //黄色画刷
        public static readonly SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0)); //绿色画刷

        #endregion
    }
}
