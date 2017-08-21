using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Subway.ShenZhenLine11.Views.CommonViews
{
    /// <summary>
    /// DriveDoor.xaml 的交互逻辑
    /// </summary>
    public partial class DriveDoor : UserControl
    {
        public DriveDoor()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DoorStateProperty = DependencyProperty.Register(
            "DoorState", typeof(DriveDoorState), typeof(DriveDoor), new PropertyMetadata(default(DriveDoorState), OnDriveDoorChanged));

        private static void OnDriveDoorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DriveDoor)d).OnDriveDoorChanged();
        }

        private void OnDriveDoorChanged()
        {
            switch (DoorState)
            {
                case DriveDoorState.Normal:
                    Border.Background = Brushes.Transparent;
                    TextBlock.Text = string.Empty;
                    break;
                case DriveDoorState.Open:
                    Border.Background = Brushes.Yellow;
                    TextBlock.Text = "开";
                    break;
                case DriveDoorState.Close:
                    Border.Background = Brushes.LawnGreen;
                    TextBlock.Text = "关";
                    break;
                case DriveDoorState.Falut:
                    Border.Background = Brushes.Red;
                    TextBlock.Text = string.Empty;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DriveDoorState DoorState { get { return (DriveDoorState)GetValue(DoorStateProperty); } set { SetValue(DoorStateProperty, value); } }
    }

    public enum DriveDoorState
    {
        Normal,
        [Description("司机室门开")]
        Open,
        [Description("司机室门关")]
        Close,
        [Description("司机室门故障")]
        Falut
    }
}
