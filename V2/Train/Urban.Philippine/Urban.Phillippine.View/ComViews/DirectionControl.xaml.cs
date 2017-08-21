using System;
using System.Windows;
using System.Windows.Controls;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.ComViews
{
    /// <summary>
    ///     DirectionControl.xaml 的交互逻辑
    /// </summary>
    public partial class DirectionControl : UserControl
    {
        public static readonly DependencyProperty DirctionProperty = DependencyProperty.Register(
            "Dirction", typeof(Dirction?), typeof(DirectionControl), new PropertyMetadata(null, OnDirctionChanged));

        public DirectionControl()
        {
            InitializeComponent();
        }

        public Dirction? Dirction
        {
            get { return (Dirction?)GetValue(DirctionProperty); }
            set { SetValue(DirctionProperty, value); }
        }

        private static void OnDirctionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DirectionControl)d).OnDirctionChanged(e);
        }

        private void OnDirctionChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (Dirction)
            {
                case Interface.Enum.Dirction.Up:
                    break;

                case Interface.Enum.Dirction.Down:
                    break;

                case Interface.Enum.Dirction.Left:
                    Left.Visibility = Visibility.Visible;
                    Right.Visibility = Visibility.Collapsed;
                    break;

                case Interface.Enum.Dirction.Roght:
                    Left.Visibility = Visibility.Collapsed;
                    Right.Visibility = Visibility.Visible;
                    break;

                case null:
                    Left.Visibility = Visibility.Collapsed;
                    Right.Visibility = Visibility.Collapsed;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}