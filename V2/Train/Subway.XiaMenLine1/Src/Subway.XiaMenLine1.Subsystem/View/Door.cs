using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.View.Doors;

namespace Subway.XiaMenLine1.Subsystem.View
{
    public class Door : ContentControl
    {
        public Door()
        {
            //this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Subway.XiaMenLine11.Subsystem;component/AppResouce.xaml", UriKind.RelativeOrAbsolute) };
        }
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            "Status", typeof(DoorStatus), typeof(Door), new PropertyMetadata(default(DoorStatus), OnStatusChanged));

        private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Door)d).OnStatusChanged();
        }

        private void OnStatusChanged()
        {
            switch (Status)
            {
                case DoorStatus.Null:
                    Content = null;
                    break;
                case DoorStatus.NormalDisplay:
                    Content = null;
                    break;
                case DoorStatus.SelectDisplay:
                    Content = null;
                    break;
                case DoorStatus.CutDisplay:
                    Content = null;
                    break;
                case DoorStatus.FaultDispaly:
                    Content = null;
                    break;
                case DoorStatus.EmeregencyUnlock:
                    Content = new Image() { Source = (this.FindResource("门紧急解锁") as BitmapImage),Stretch = Stretch.Fill};
                    break;
                case DoorStatus.Cut:
                    Content = new Image() { Source = (this.FindResource("门切除") as BitmapImage), Stretch = Stretch.Fill };
                    break;
                case DoorStatus.OpenFault:
                    Content = new DoorOpenFault();
                    break;
                case DoorStatus.CloseFault:
                    Content = new DoorCloseFault();
                    break;
                case DoorStatus.CloseCheck:
                    Content = new DoorCloseCheck();
                    break;
                case DoorStatus.OpenCheck:
                    Content = new DoorOpenCheck();
                    break;
                case DoorStatus.Flick:
                    Content = null;
                    break;
                case DoorStatus.Closed:
                    Content = new Border() { Background = SolidColorBrushMgr.LawnGreenBrush };
                    break;
                case DoorStatus.DoorFlick:
                    Content = null;
                    break;
                case DoorStatus.DoorNormal:
                    Content = null;
                    break;
                case DoorStatus.Opened:
                    Content = new Border() { Background = SolidColorBrushMgr.WhiteBrush };
                    break;
                case DoorStatus.CommunicationFault:
                    Content = new Image() { Source = (this.FindResource("门通讯故障") as BitmapImage), Stretch = Stretch.Fill };
                    break;
                case DoorStatus.StateUnkown:
                    Content = new Image() { Source = (this.FindResource("门状态未知") as BitmapImage), Stretch = Stretch.Fill };
                    break;
                default:
                    Content = null;
                    break;
            }
        }

        public DoorStatus Status { get { return (DoorStatus)GetValue(StatusProperty); } set { SetValue(StatusProperty, value); } }
    }
}