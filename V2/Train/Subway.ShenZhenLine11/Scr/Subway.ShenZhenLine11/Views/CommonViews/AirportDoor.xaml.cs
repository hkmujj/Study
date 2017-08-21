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
    /// AirportDoor.xaml 的交互逻辑
    /// </summary>
    public partial class AirportDoor : UserControl
    {
        public AirportDoor()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DoorStateProperty = DependencyProperty.Register(
            "DoorState", typeof(AirportDoorState), typeof(AirportDoor), new PropertyMetadata(default(AirportDoorState), OnDoorStateChanged));

        private static void OnDoorStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AirportDoor)d).OnDoorStateChanged();
        }
        private void OnDoorStateChanged()
        {
            switch (DoorState)
            {
                case AirportDoorState.Normal:
                    Border.Background = Brushes.Transparent;
                    break;
                case AirportDoorState.AllOpen:
                    Border.Background = Brushes.Yellow;
                    break;
                case AirportDoorState.Fault:
                    Border.Background = Brushes.Red;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public AirportDoorState DoorState { get { return (AirportDoorState)GetValue(DoorStateProperty); } set { SetValue(DoorStateProperty, value); } }
    }

    public enum AirportDoorState
    {
        Normal,
        [Description("机场门全开")]
        AllOpen,
        [Description("机场门故障")]
        Fault
    }
}
