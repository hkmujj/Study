using System.Drawing;

namespace Urban.ATC.Siemens.WPF.Control
{
    public class GDICommon
    {
        /// <summary>
        /// 背景色
        /// </summary>
        public readonly static Color BacgroundColor = Color.FromArgb(3, 17, 34);

        /// <summary>
        /// 白色
        /// </summary>
        public readonly static Color WhiteColor = Color.White;

        /// <summary>
        /// 黑色
        /// </summary>
        public readonly static Color BalackColor = Color.Black;

        /// <summary>
        /// 浅灰色
        /// </summary>
        public readonly static Color LightGreyColor = Color.FromArgb(195, 195, 195);

        /// <summary>
        /// 中灰色
        /// </summary>
        public readonly static Color MeduymGreyColor = Color.FromArgb(150, 150, 150);

        /// <summary>
        /// 深灰色
        /// </summary>
        public readonly static Color DarkGreyColor = Color.FromArgb(85, 85, 85);

        /// <summary>
        /// 深蓝色
        /// </summary>
        public readonly static Color VeryDarkBlue = Color.FromArgb(4, 12, 25);

        /// <summary>
        /// 淡绿色
        /// </summary>
        public readonly static Color LightGreenColor = Color.FromArgb(45, 144, 51);

        /// <summary>
        /// 深绿色
        /// </summary>
        public readonly static Color DarkGreenColor = Color.FromArgb(12, 58, 12);

        /// <summary>
        /// 黄色
        /// </summary>
        public readonly static Color YellowColor = Color.FromArgb(255, 255, 0);

        /// <summary>
        /// 深黄色
        /// </summary>
        public readonly static Color DarkYellowColor = Color.FromArgb(117, 105, 0);

        /// <summary>
        /// 橙色
        /// </summary>
        public readonly static Color OrangeColor = Color.FromArgb(234, 140, 0);

        /// <summary>
        /// 红色
        /// </summary>
        public readonly static Color RedColor = Color.FromArgb(191, 0, 2);

        /// <summary>
        /// 灰蓝色
        /// </summary>
        public readonly static Color GreyBlueColor = Color.FromArgb(81, 91, 109);

        /// <summary>
        /// 蓝灰色
        /// </summary>
        public readonly static Color BlueGreyColor = Color.FromArgb(37, 69, 93);

        /// <summary>
        /// 淡蓝灰色
        /// </summary>
        public readonly static Color LightBlueGryColor = Color.FromArgb(128, 139, 158);

        /// <summary>
        /// 标志颜色
        /// </summary>
        public static readonly Color LogColor = Color.FromArgb(0, 194, 194);

        public readonly static SolidBrush BacgroundBrush = new SolidBrush(BacgroundColor);
        public readonly static SolidBrush WhiteBrush = new SolidBrush(WhiteColor);
        public readonly static SolidBrush BalackBrush = new SolidBrush(BalackColor);
        public readonly static SolidBrush LightGreyBrush = new SolidBrush(LightGreyColor);
        public readonly static SolidBrush MeduymGreyBrush = new SolidBrush(MeduymGreyColor);
        public readonly static SolidBrush DarkGreyBrush = new SolidBrush(DarkGreyColor);
        public readonly static SolidBrush VeryDarkBrush = new SolidBrush(VeryDarkBlue);
        public readonly static SolidBrush LightGreenBrush = new SolidBrush(LightGreenColor);
        public readonly static SolidBrush DarkGreenBrush = new SolidBrush(DarkGreenColor);
        public readonly static SolidBrush YellowBrush = new SolidBrush(YellowColor);
        public readonly static SolidBrush DarkYellowBrush = new SolidBrush(DarkYellowColor);
        public readonly static SolidBrush OrangeBrush = new SolidBrush(OrangeColor);
        public readonly static SolidBrush RedBrush = new SolidBrush(RedColor);
        public readonly static SolidBrush GreyBlueBrush = new SolidBrush(GreyBlueColor);
        public readonly static SolidBrush BlueGreyBrush = new SolidBrush(BlueGreyColor);
        public readonly static SolidBrush LightBlueGryBrush = new SolidBrush(LightBlueGryColor);

        public static readonly Pen LightGreyPen = new Pen(LightBlueGryColor);
    }
}