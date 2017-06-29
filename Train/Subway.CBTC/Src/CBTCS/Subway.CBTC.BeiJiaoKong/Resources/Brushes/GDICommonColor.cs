using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Subway.CBTC.BeiJiaoKong.Resources.Brushes
{
    public class GDICommonColor
    {
        private Color c = Color.Black;

        /// <summary>
        /// 背景色
        /// </summary>
        public readonly static System.Windows.Media.Color BacgroundColor = System.Windows.Media.Color.FromRgb(3, 17, 34);

        /// <summary>
        /// 白色
        /// </summary>
        public readonly static System.Windows.Media.Color WhiteColor = System.Windows.Media.Color.FromRgb(255, 255, 255);

        /// <summary>
        /// 黑色
        /// </summary>
        public readonly static System.Windows.Media.Color BalackColor = System.Windows.Media.Color.FromRgb(0, 0, 0);

        /// <summary>
        /// 浅灰色
        /// </summary>
        public readonly static System.Windows.Media.Color LightGreyColor = System.Windows.Media.Color.FromRgb(195, 195, 195);

        /// <summary>
        /// 中灰色
        /// </summary>
        public readonly static System.Windows.Media.Color MeduymGreyColor = System.Windows.Media.Color.FromRgb(150, 150, 150);

        /// <summary>
        /// 深灰色
        /// </summary>
        public readonly static System.Windows.Media.Color DarkGreyColor = System.Windows.Media.Color.FromRgb(85, 85, 85);

        /// <summary>
        /// 深蓝色
        /// </summary>
        public readonly static System.Windows.Media.Color VeryDarkBlue = System.Windows.Media.Color.FromRgb(4, 12, 25);

        /// <summary>
        /// 淡绿色
        /// </summary>
        public readonly static System.Windows.Media.Color LightGreenColor = System.Windows.Media.Color.FromRgb(45, 144, 51);

        /// <summary>
        /// 深绿色
        /// </summary>
        public readonly static System.Windows.Media.Color DarkGreenColor = System.Windows.Media.Color.FromRgb(12, 58, 12);

        /// <summary>
        /// 黄色
        /// </summary>
        public readonly static System.Windows.Media.Color YellowColor = System.Windows.Media.Color.FromRgb(255, 255, 0);

        /// <summary>
        /// 深黄色
        /// </summary>
        public readonly static System.Windows.Media.Color DarkYellowColor = System.Windows.Media.Color.FromRgb(117, 105, 0);

        /// <summary>
        /// 橙色
        /// </summary>
        public readonly static System.Windows.Media.Color OrangeColor = System.Windows.Media.Color.FromRgb(234, 140, 0);

        /// <summary>
        /// 红色
        /// </summary>
        public readonly static System.Windows.Media.Color RedColor = System.Windows.Media.Color.FromRgb(191, 0, 2);

        /// <summary>
        /// 灰蓝色
        /// </summary>
        public readonly static System.Windows.Media.Color GreyBlueColor = System.Windows.Media.Color.FromRgb(81, 91, 109);

        /// <summary>
        /// 蓝灰色
        /// </summary>
        public readonly static System.Windows.Media.Color BlueGreyColor = System.Windows.Media.Color.FromRgb(37, 69, 93);

        /// <summary>
        /// 淡蓝灰色
        /// </summary>
        public readonly static System.Windows.Media.Color LightBlueGryColor = System.Windows.Media.Color.FromRgb(128, 139, 158);

        /// <summary>
        /// 标志颜色
        /// </summary>
        /// 
        public static readonly System.Windows.Media.Color LogColor = System.Windows.Media.Color.FromRgb(0, 194, 194);

        public readonly static SolidColorBrush BacgroundBrush = new SolidColorBrush(BacgroundColor);
        public readonly static SolidColorBrush WhiteBrush = new SolidColorBrush(WhiteColor);
        public readonly static SolidColorBrush BalackBrush = new SolidColorBrush(BalackColor);
        public readonly static SolidColorBrush LightGreyBrush = new SolidColorBrush(LightGreyColor);
        public readonly static SolidColorBrush MeduymGreyBrush = new SolidColorBrush(MeduymGreyColor);
        public readonly static SolidColorBrush DarkGreyBrush = new SolidColorBrush(DarkGreyColor);
        public readonly static SolidColorBrush VeryDarkBrush = new SolidColorBrush(VeryDarkBlue);
        public readonly static SolidColorBrush LightGreenBrush = new SolidColorBrush(LightGreenColor);
        public readonly static SolidColorBrush DarkGreenBrush = new SolidColorBrush(DarkGreenColor);
        public readonly static SolidColorBrush YellowBrush = new SolidColorBrush(YellowColor);
        public readonly static SolidColorBrush DarkYellowBrush = new SolidColorBrush(DarkYellowColor);
        public readonly static SolidColorBrush OrangeBrush = new SolidColorBrush(OrangeColor);
        public readonly static SolidColorBrush RedBrush = new SolidColorBrush(RedColor);
        public readonly static SolidColorBrush GreyBlueBrush = new SolidColorBrush(GreyBlueColor);
        public readonly static SolidColorBrush BlueGreyBrush = new SolidColorBrush(BlueGreyColor);
        public readonly static SolidColorBrush LightBlueGryBrush = new SolidColorBrush(LightBlueGryColor);
        public readonly static SolidColorBrush LogBrush = new SolidColorBrush(LogColor);
        public readonly static SolidColorBrush TransparentBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
    }
}