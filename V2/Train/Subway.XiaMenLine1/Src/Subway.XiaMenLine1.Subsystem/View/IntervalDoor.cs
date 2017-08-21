using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Subsystem.Constant;

namespace Subway.XiaMenLine1.Subsystem.View
{
    public class IntervalDoor: ContentControl
    {
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State", typeof(IntervalDoorState), typeof(IntervalDoor), new PropertyMetadata(default(IntervalDoorState), OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((IntervalDoor)d).OnStateChanged();
        }

        private void OnStateChanged()
        {
            switch (State)
            {
                case IntervalDoorState.UnlockOrOpen:
                    Content = new Border() { Background = SolidColorBrushMgr.WhiteBrush };
                    break;
                case IntervalDoorState.Locked:
                    Content = new Border() { Background = SolidColorBrushMgr.LawnGreenBrush };
                    break;
                case IntervalDoorState.UnKown:
                    Content = new Image() { Source = (this.FindResource("门状态未知") as BitmapImage), Stretch = Stretch.Fill };
                    break;
                case IntervalDoorState.Null:
                    Content = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IntervalDoorState State { get { return (IntervalDoorState)GetValue(StateProperty); } set { SetValue(StateProperty, value); } }
    }
}
