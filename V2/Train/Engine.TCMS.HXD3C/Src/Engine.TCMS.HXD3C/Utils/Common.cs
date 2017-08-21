using System.Drawing;

namespace Engine.TCMS.HXD3C.Utils
{
    public class Common
    {
        #region:::::::::::::::::::::::::::线条颜色::::::::::::::::::::::::::::::::#

        public static readonly Pen WhitePen1 = new Pen(Color.White, 1);
        public static readonly Pen WhitePen2 = new Pen(Color.White, 2);

        public static readonly Pen BlackPen1 = new Pen(Color.Black, 1);
        public static readonly Pen BlackPen2 = new Pen(Color.Black, 2);

        public static readonly Pen RedPen1 = new Pen(Color.Red, 1);

        public static readonly Pen YellowPen1 = new Pen(Color.Yellow, 1);
        public static readonly Pen YellowPen2 = new Pen(Color.Yellow, 2);
        public static readonly Pen YellowPen3 = new Pen(Color.Yellow, 3);

        public static readonly Pen GreenPen = new Pen(Color.Green, 2);
        public static readonly Pen GreenPen1 = new Pen(Color.Green, 1);

        #endregion#

        #region:::::::::::::::::::::::::::画笔颜色::::::::::::::::::::::::::::::::#

        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.Black);
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.White);
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));
        public static readonly SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        public static readonly SolidBrush PinkBrush = new SolidBrush(Color.FromArgb(255, 0, 255));
        public static readonly SolidBrush MarineBlueBrush = new SolidBrush(Color.FromArgb(0, 255, 255));

        #endregion#

        #region:::::::::::::::::::::::::::字体格式::::::::::::::::::::::::::::::::#

        public static readonly Font Txt1FontR = new Font("宋体", 1, FontStyle.Regular);
        public static readonly Font Txt10FontR = new Font("宋体", 10, FontStyle.Regular);
        public static readonly Font Txt12FontR = new Font("宋体", 12, FontStyle.Regular);
        public static readonly Font Txt13FontR = new Font("宋体", 13, FontStyle.Regular);
        public static readonly Font Txt14FontR = new Font("宋体", 14, FontStyle.Regular);
        public static readonly Font Txt16FontR = new Font("宋体", 16, FontStyle.Regular);
        public static readonly Font Txt20FontR = new Font("宋体", 20, FontStyle.Regular);
        public static readonly Font Txt24FontR = new Font("宋体", 24, FontStyle.Regular);
        public static readonly Font Txt30FontR = new Font("宋体", 30, FontStyle.Regular);

        public static readonly Font Txt1FontB = new Font("宋体", 1, FontStyle.Bold);
        public static readonly Font Txt10FontB = new Font("宋体", 10, FontStyle.Bold);
        public static readonly Font Txt12FontB = new Font("宋体", 12, FontStyle.Bold);
        public static readonly Font Txt13FontB = new Font("宋体", 13, FontStyle.Bold);
        public static readonly Font Txt14FontB = new Font("宋体", 14, FontStyle.Bold);
        public static readonly Font Txt16FontB = new Font("宋体", 16, FontStyle.Bold);
        public static readonly Font Txt20FontB = new Font("宋体", 20, FontStyle.Bold);
        public static readonly Font Txt24FontB = new Font("宋体", 24, FontStyle.Bold);
        public static readonly Font Txt30FontB = new Font("宋体", 30, FontStyle.Bold);

        public static readonly Font Txt1FontLr = new Font("LcdD", 1, FontStyle.Regular);
        public static readonly Font Txt10FontLr = new Font("LcdD", 10, FontStyle.Regular);
        public static readonly Font Txt12FontLr = new Font("LcdD", 12, FontStyle.Regular);
        public static readonly Font Txt13FontLr = new Font("LcdD", 13, FontStyle.Regular);
        public static readonly Font Txt14FontLr = new Font("LcdD", 14, FontStyle.Regular);
        public static readonly Font Txt16FontLr = new Font("LcdD", 16, FontStyle.Regular);
        public static readonly Font Txt20FontLr = new Font("LcdD", 20, FontStyle.Regular);
        public static readonly Font Txt24FontLr = new Font("LcdD", 24, FontStyle.Regular);
        public static readonly Font Txt30FontLr = new Font("LcdD", 30, FontStyle.Regular);
        public static readonly Font Txt34FontLr = new Font("LcdD", 34, FontStyle.Regular);

        public static readonly Font Txt1FontLb = new Font("LcdD", 1, FontStyle.Bold);
        public static readonly Font Txt10FontLb = new Font("LcdD", 10, FontStyle.Bold);
        public static readonly Font Txt12FontLb = new Font("LcdD", 12, FontStyle.Bold);
        public static readonly Font Txt13FontLb = new Font("LcdD", 13, FontStyle.Bold);
        public static readonly Font Txt14FontLb = new Font("LcdD", 14, FontStyle.Bold);
        public static readonly Font Txt16FontLb = new Font("LcdD", 16, FontStyle.Bold);
        public static readonly Font Txt20FontLb = new Font("LcdD", 20, FontStyle.Bold);
        public static readonly Font Txt24FontLb = new Font("LcdD", 24, FontStyle.Bold);
        public static readonly Font Txt30FontLb = new Font("LcdD", 30, FontStyle.Bold);
        public static readonly Font Txt34FontLb = new Font("LcdD", 34, FontStyle.Bold);

        public static readonly StringFormat CenterStringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public static readonly StringFormat DrawFormat = new StringFormat();
        public static readonly StringFormat RightFormat = new StringFormat();
        public static readonly StringFormat LeftFormat = new StringFormat();

        #endregion#


        #region 底部按键内容 Str视图ID

        public static readonly string[] Str201 = {"机器状态", "", "蓄电池", "试运行", "开放状态", "检修状态", "故障履历", "亮度  中"};

        public static readonly string[] Str202 = {"主变流器", "开关状态", "风机状态", "辅助电源", "空制状态", "轴温状态", "故障履历", "返回"};

        public static readonly string[] Str203 = {"机器状态", "", "蓄电池", "试运行", "开放状态", "检修状态", "故障履历", "返回"};

        public static readonly string[] Str204 = {"机器状态", "", "蓄电池", "试运行", "", "", "故障履历", "返回"};

        public static readonly string[] Str205 = { "", "", "", "", "", "", "", "返回" };

        public static readonly string[] Str217 = {"主司控器", "起动", "零级位", "辅助电源", "显示灯", "无人警惕", "轮缘润滑", "返回"};

        public static readonly string[] Str219 = {"记录", "设定", "试验", "状态", "", "", "", "返回"};

        #endregion
    }
}
