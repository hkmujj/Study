using System.Windows;
using System.Windows.Controls;
using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Subsystem.Controls
{
    public class HightBreakerIndicator : Control
    {
        static HightBreakerIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HightBreakerIndicator), new FrameworkPropertyMetadata(typeof(HightBreakerIndicator)));
        }

        public static readonly DependencyProperty FrsmHighSpeedProperty = DependencyProperty.Register(
            "FrsmHighSpeed", typeof (FrsmHighSpeed), typeof (HightBreakerIndicator), new PropertyMetadata(FrsmHighSpeed.HighJoin));

        public FrsmHighSpeed FrsmHighSpeed
        {
            get { return (FrsmHighSpeed) GetValue(FrsmHighSpeedProperty); }
            set { SetValue(FrsmHighSpeedProperty, value); }
        }
    }
}