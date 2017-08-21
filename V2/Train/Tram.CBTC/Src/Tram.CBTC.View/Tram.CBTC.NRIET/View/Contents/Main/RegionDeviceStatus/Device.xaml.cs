using System;
using System.Windows;
using System.Windows.Controls;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.NRIET.View.Contents.Main.RegionDeviceStatus
{
    /// <summary>
    /// Device.xaml 的交互逻辑
    /// </summary>
    public partial class Device : UserControl
    {
        public Device()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 设备标题
        /// </summary>
        public static readonly DependencyProperty DeviceTitleProperty =
            DependencyProperty.Register("DeviceTitle",
                typeof(string),
                typeof(Device),
                new FrameworkPropertyMetadata("",
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    DeviceTitlePropertyChanged,
                    null));

        private static void DeviceTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Device)d).OnDeviceTitlePropertyChanged(e);
        }

        private void OnDeviceTitlePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            TextBlockDeviceTitle.Text = DeviceTitle;
        }

        /// <summary>
        /// 设备标题
        /// </summary>
        public string DeviceTitle
        {
            get
            {
                return (string)this.GetValue(DeviceTitleProperty);
            }
            set
            {
                this.SetValue(DeviceTitleProperty, value);
            }
        }






        public static readonly DependencyProperty DeviceStateProperty = DependencyProperty.Register(
            "DeviceState", typeof(DeviceState), typeof(Device), new PropertyMetadata(default(DeviceState), OnDeviceStateChanged));

        private static void OnDeviceStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Device)d).DeviceSatteChanged();
        }

        private void DeviceSatteChanged()
        {
            PathDeviceFault1.Visibility = Visibility.Hidden;
            PathDeviceFault2.Visibility = Visibility.Hidden;
            BorderRedundancyDeviceFault.Visibility = Visibility.Hidden;
            switch (DeviceState)
            {
                //case DeviceState.CurrentNormal:
                 //   break;
                //case DeviceState.RemoteNormal:
                //    break;
                case DeviceState.CurrentFault:
                case DeviceState.RemoteFault:
                    BorderRedundancyDeviceFault.Visibility = Visibility.Visible;
                    break;
                case DeviceState.AllFault:
                    PathDeviceFault1.Visibility = Visibility.Visible;
                    PathDeviceFault2.Visibility = Visibility.Visible;
                    BorderRedundancyDeviceFault.Visibility = Visibility.Visible;
                    break;
                case DeviceState.AllNormal:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DeviceState DeviceState { get { return (DeviceState)GetValue(DeviceStateProperty); } set { SetValue(DeviceStateProperty, value); } }


    }
}
