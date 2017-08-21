using System.Windows.Media;

namespace LightRail.HMI.SZLHLF
{
    public class GDICommonColor
    {
        /// <summary>
        /// 白色
        /// </summary>
        public readonly static System.Windows.Media.Color WhiteColor = System.Windows.Media.Color.FromRgb(255, 255, 255);

        /// <summary>
        /// 绿色
        /// </summary>
        public static readonly System.Windows.Media.Color GreenColor = System.Windows.Media.Color.FromRgb(0, 255, 0);

        public  readonly static SolidColorBrush WhiteBrush = new SolidColorBrush(WhiteColor);
        public  readonly static SolidColorBrush GreenBrush = new SolidColorBrush(GreenColor);
    }
}
