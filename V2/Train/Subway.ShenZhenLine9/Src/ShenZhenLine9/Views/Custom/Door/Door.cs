using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.Views.Custom.Door
{
    public class Door : ContentControl
    {
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State", typeof(DoorState), typeof(Door), new PropertyMetadata(default(DoorState), OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Door)d).DoorChanged();
        }

        private void DoorChanged()
        {
            switch (State)
            {
                case DoorState.None:
                    break;
                case DoorState.EmergencyLock:
                    Content = new Border() { Background = new SolidColorBrush(Colors.Black) };
                    break;
                case DoorState.Cut:
                    Content = new Border() { Background = new SolidColorBrush(Colors.Yellow) };
                    break;
                case DoorState.Fault:
                    Content = new Border() { Background = new SolidColorBrush(Colors.Red) };
                    break;
                case DoorState.Check:
                    Content = new CheckDoor();
                    break;
                case DoorState.Flicker:
                    Content = new Border() { Background = new SolidColorBrush(Color.FromArgb(255, 115, 193, 248)) };
                    break;
                case DoorState.Closed:
                    Content = new Border() { Background = new SolidColorBrush(Color.FromArgb(255, 115, 193, 248)) };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DoorState State { get { return (DoorState)GetValue(StateProperty); } set { SetValue(StateProperty, value); } }
    }
}
