using System;
using System.Collections.Generic;
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
using MMI.Facility.WPFInfrastructure.View;

namespace Subway.CBTC.THALES.View.Common
{
    /// <summary>
    /// DwellValuesView.xaml 的交互逻辑
    /// </summary>
    public partial class DwellValuesView : UserControl
    {
        public DwellValuesView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register(
            "TextValue", typeof(double), typeof(DwellValuesView), new PropertyMetadata(-1d, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DwellValuesView)d).ValueChanged();
        }

        private void ValueChanged()
        {
            if (TextValue >= 999)
            {
                TextBlock.Text = "< >";
                TextBlock.Foreground = new SolidColorBrush(Colors.Yellow);
                FlickerManager.SetFlicking(Viewbox, false);
                return;
            }

            TextBlock.Text = TextValue.ToString("F0");
            if (IsOutTime)
            {
                FlickerManager.SetFlicking(Viewbox, TextValue >= 2);
                TextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FlickerManager.SetFlicking(Viewbox, false);
                TextBlock.Foreground = TextValue <= 5 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Yellow);
            }
        }

        public double TextValue { get { return (double)GetValue(TextValueProperty); } set { SetValue(TextValueProperty, value); } }

        public static readonly DependencyProperty IsOutTimeProperty = DependencyProperty.Register(
            "IsOutTime", typeof(bool), typeof(DwellValuesView), new PropertyMetadata(default(bool), OnValueChanged));

        public bool IsOutTime { get { return (bool)GetValue(IsOutTimeProperty); } set { SetValue(IsOutTimeProperty, value); } }

    }
}
