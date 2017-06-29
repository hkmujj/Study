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
            "TextValue", typeof(double), typeof(DwellValuesView), new PropertyMetadata(default(double), OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DwellValuesView)d).ValueChanged();
        }

        private void ValueChanged()
        {
            if (IsOutTime)
            {
                Viewbox.Visibility = TextValue >= 5 ? Visibility.Visible : Visibility.Hidden;
                TextBlock.Text = TextValue.ToString("F0");
                TextBlock.Foreground = new SolidColorBrush(Colors.Red);

            }
            else
            {
                Viewbox.Visibility = Visibility.Hidden;
                TextBlock.Text = TextValue >= 999 ? "< >" : TextValue.ToString("F0");
                TextBlock.Foreground = new SolidColorBrush(Colors.Yellow);
            }
        }

        public double TextValue { get { return (double)GetValue(TextValueProperty); } set { SetValue(TextValueProperty, value); } }

        public static readonly DependencyProperty IsOutTimeProperty = DependencyProperty.Register(
            "IsOutTime", typeof(bool), typeof(DwellValuesView), new PropertyMetadata(default(bool), OnValueChanged));

        public bool IsOutTime { get { return (bool)GetValue(IsOutTimeProperty); } set { SetValue(IsOutTimeProperty, value); } }

    }
}
