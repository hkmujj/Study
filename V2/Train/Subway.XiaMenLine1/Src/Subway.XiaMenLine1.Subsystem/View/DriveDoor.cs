using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View
{
    public class DriveDoor : ContentControl
    {
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State", typeof(DriverDoorState), typeof(DriveDoor), new PropertyMetadata(default(DriverDoorState), OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DriveDoor)d).OnStateChanged();
        }

        private void OnStateChanged()
        {
            switch (State)
            {
                case DriverDoorState.UnlockOrOpen:
                    Content = new Border() { Background = SolidColorBrushMgr.WhiteBrush };
                    break;
                case DriverDoorState.Locked:
                    Content = new Border() { Background = SolidColorBrushMgr.LawnGreenBrush };
                    break;
                case DriverDoorState.UnKnow:
                    Content = new Image() { Source = (this.FindResource("门状态未知") as BitmapImage), Stretch = Stretch.Fill };
                    break;
                case DriverDoorState.Null:
                    Content = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DriverDoorState State { get { return (DriverDoorState)GetValue(StateProperty); } set { SetValue(StateProperty, value); } }
    }
}
