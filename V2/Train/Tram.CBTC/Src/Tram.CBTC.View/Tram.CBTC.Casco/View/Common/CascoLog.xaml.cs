using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tram.CBTC.Casco.View.Common
{
    /// <summary>
    /// CascoLog.xaml 的交互逻辑
    /// </summary>
    public partial class CascoLog : UserControl
    {
        public CascoLog()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LogBrushOneProperty = DependencyProperty.Register(
            "LogBrushOne", typeof(Brush), typeof(CascoLog), new PropertyMetadata(default(Brush)));

        public Brush LogBrushOne { get { return (Brush)GetValue(LogBrushOneProperty); } set { SetValue(LogBrushOneProperty, value); } }

        public static readonly DependencyProperty LogBrushTwoProperty = DependencyProperty.Register(
            "LogBrushTwo", typeof(Brush), typeof(CascoLog), new PropertyMetadata(default(Brush)));

        public Brush LogBrushTwo { get { return (Brush)GetValue(LogBrushTwoProperty); } set { SetValue(LogBrushTwoProperty, value); } }
    }
}
