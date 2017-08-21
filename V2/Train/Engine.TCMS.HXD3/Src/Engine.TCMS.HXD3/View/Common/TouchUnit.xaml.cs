using System.Windows;
using System.Windows.Media;

namespace Engine.TCMS.HXD3.View.Common
{
    /// <summary>
    /// ChangingUnit.xaml 的交互逻辑
    /// </summary>
    public partial class TouchUnit
    {
        public TouchUnit()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TouchBrushProperty = DependencyProperty.Register(
            "TouchBrush", typeof(Brush), typeof(TouchUnit), new PropertyMetadata(Brushes.Transparent));

        public Brush TouchBrush
        {
            get { return (Brush) GetValue(TouchBrushProperty); }
            set { SetValue(TouchBrushProperty, value); }
        }
    }
}
