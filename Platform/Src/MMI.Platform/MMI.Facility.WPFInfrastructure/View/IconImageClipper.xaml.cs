using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MMI.Facility.WPFInfrastructure.View
{
    /// <summary>
    /// 图标文件的裁剪
    /// </summary>
    public partial class IconImageClipper 
    {
        /// <summary>
        /// 
        /// </summary>
        public IconImageClipper()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前选中的
        /// </summary>
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex", typeof(int), typeof(IconImageClipper), new PropertyMetadata(default(int)));

        /// <summary>
        /// 当前选中的
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// 裁剪个数
        /// </summary>
        public static readonly DependencyProperty ClipCountProperty = DependencyProperty.Register(
            "ClipCount", typeof(int), typeof(IconImageClipper), new PropertyMetadata(default(int)));

        /// <summary>
        /// 裁剪个数
        /// </summary>
        public int ClipCount
        {
            get { return (int)GetValue(ClipCountProperty); }
            set { SetValue(ClipCountProperty, value); }
        }

        /// <summary>
        /// 源图
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof(ImageSource), typeof(IconImageClipper), new PropertyMetadata(default(ImageSource)));

        /// <summary>
        /// 源图
        /// </summary>
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        /// <summary>
        /// 每个裁剪图的间隔
        /// </summary>
        public static readonly DependencyProperty ClipIntervalProperty = DependencyProperty.Register(
            "ClipInterval", typeof(double), typeof(IconImageClipper), new PropertyMetadata(default(double)));

        /// <summary>
        /// 每个裁剪图的间隔
        /// </summary>
        public double ClipInterval
        {
            get { return (double) GetValue(ClipIntervalProperty); }
            set { SetValue(ClipIntervalProperty, value); }
        }

        /// <summary>
        /// 方向
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(Orientation), typeof(IconImageClipper), new PropertyMetadata(default(Orientation)));

        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
    }
}
