using System;
using System.Windows;
using System.Windows.Controls;
using Subway.WuHanLine6.Interfaces;

namespace Subway.WuHanLine6.Views.Common
{
    /// <summary>
    /// OrientationView.xaml 的交互逻辑
    /// </summary>
    public partial class OrientationView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public OrientationView()
        {
            InitializeComponent();
            OnDirctionChanged();
        }

        /// <summary>
        /// 列车方向
        /// </summary>
        public static readonly DependencyProperty TrainDirectionProperty = DependencyProperty.Register(
            "TrainDirection", typeof(Direction), typeof(OrientationView), new PropertyMetadata(default(Direction), OnDirctionChanged));

        private static void OnDirctionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((OrientationView)d).OnDirctionChanged();
        }

        private void OnDirctionChanged()
        {
            switch (TrainDirection)
            {
                case Direction.Normal:
                    Grid.Visibility = Visibility.Hidden;
                    Left.Visibility = Visibility.Hidden;
                    Right.Visibility = Visibility.Hidden;
                    break;
                case Direction.Left:
                    Grid.Visibility = Visibility.Visible;
                    Left.Visibility = Visibility.Visible;
                    Right.Visibility = Visibility.Hidden;
                    break;
                case Direction.Right:
                    Grid.Visibility = Visibility.Visible;
                    Left.Visibility = Visibility.Hidden;
                    Right.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 列车方向
        /// </summary>
        public Direction TrainDirection { get { return (Direction)GetValue(TrainDirectionProperty); } set { SetValue(TrainDirectionProperty, value); } }
    }
}
