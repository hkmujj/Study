using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Subway.ShenZhenLine9.Views.Custom.Door
{
    public class InterDoorAndEvacuteDoor : ContentControl
    {
        public InterDoorAndEvacuteDoor()
        {
            Content = new Border() { Background = new SolidColorBrush(Colors.White) };
        }
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen", typeof(bool), typeof(InterDoorAndEvacuteDoor), new PropertyMetadata(default(bool), OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((InterDoorAndEvacuteDoor)d).IsOpenChanged();
        }

        private void IsOpenChanged()
        {
            if (IsOpen)
            {
                Content = new Border() { Background = new SolidColorBrush(Colors.Red) };
            }
            else
            {
                Content = new Border() { Background = new SolidColorBrush(Colors.White) };
            }
        }

        public bool IsOpen { get { return (bool)GetValue(IsOpenProperty); } set { SetValue(IsOpenProperty, value); } }
    }
}
