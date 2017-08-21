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
    /// EscapeDoor.xaml 的交互逻辑
    /// </summary>
    public partial class EscapeDoor : UserControl
    {
        public EscapeDoor()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DoorStateProperty = DependencyProperty.Register(
            "DoorState", typeof(EscapeDoorState), typeof(EscapeDoor), new PropertyMetadata(default(EscapeDoorState), OnStateChanged));

        private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EscapeDoor)d).OnStateChanged();
        }

        private void OnStateChanged()
        {
            switch (DoorState)
            {
                case EscapeDoorState.Normal:
                    Rectangle.Fill = Brushes.Transparent;
                    break;
                case EscapeDoorState.Lock:
                    Rectangle.Fill = Brushes.Yellow;
                    break;
                case EscapeDoorState.UnLock:
                    Rectangle.Fill = Brushes.WhiteSmoke;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public EscapeDoorState DoorState { get { return (EscapeDoorState)GetValue(DoorStateProperty); } set { SetValue(DoorStateProperty, value); } }
    }

    public enum EscapeDoorState
    {

        Normal,
        [Description("逃生门/通道门锁上")]
        Lock,
        [Description("逃生门/通道门未锁")]
        UnLock,
    }
}
