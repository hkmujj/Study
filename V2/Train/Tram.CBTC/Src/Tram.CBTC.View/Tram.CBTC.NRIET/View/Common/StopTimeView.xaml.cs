using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MMI.Facility.WPFInfrastructure.View;

namespace Tram.CBTC.NRIET.View.Common
{
    /// <summary>
    /// StopTimeView.xaml 的交互逻辑
    /// </summary>
    public partial class StopTimeView : UserControl
    {
        public StopTimeView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StopTimeSecProperty = DependencyProperty.Register(
            "StopTimeSec", typeof(float), typeof(StopTimeView), new PropertyMetadata(default(float), (sender, args) => { ((StopTimeView)sender).SetTextStatus(sender); }));

        public float StopTimeSec
        {
            get { return (float)GetValue(StopTimeSecProperty); }
            set { SetValue(StopTimeSecProperty, value); }
        }

        public static readonly DependencyProperty VisibleProperty = DependencyProperty.Register(
            "Visible", typeof(bool), typeof(StopTimeView), new PropertyMetadata(default(bool), (sender, args) => { ((StopTimeView)sender).SetTextStatus(sender); }));

        public bool Visible
        {
            get { return (bool)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        public static readonly DependencyProperty IsFlickerProperty = DependencyProperty.Register(
            "IsFlicker", typeof(bool), typeof(StopTimeView), new PropertyMetadata(default(bool), (sender, args) => { ((StopTimeView)sender).SetTextStatus(sender); }));

        public bool IsFlicker
        {
            get { return (bool)GetValue(IsFlickerProperty); }
            set { SetValue(IsFlickerProperty, value); }
        }

        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register(
            "TextColor", typeof(Brush), typeof(StopTimeView), new PropertyMetadata(default(Brush), (sender, args) => { ((StopTimeView)sender).SetTextStatus(sender); }));

        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        private void SetTextStatus(DependencyObject d)
        {
            TextBlock.Text = StopTimeSec.ToString("F0");
            TextBlock.Foreground = TextColor;
            if (Visible)
            {
                if (IsFlicker)
                {
                    FlickerManager.SetDurationMiliSecond(TextBlock, 300);
                    FlickerManager.SetFlicking(TextBlock, IsFlicker);
                    FlickerManager.SetVisibilityAfterFlicking(TextBlock, Visibility.Visible);
                }
                else
                {
                    FlickerManager.SetFlicking(TextBlock, false);
                    TextBlock.Visibility = Visibility.Visible;
                }

            }
            else
            {
                TextBlock.Visibility = Visibility.Hidden;
            }

        }
    }
}
