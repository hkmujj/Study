using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.HMI.SS3B.CommonView
{
    /// <summary>
    /// MainViewTable.xaml 的交互逻辑
    /// </summary>
    public partial class MainViewTable : ItemsControl
    {
        public MainViewTable()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty BackBrush1Property = DependencyProperty.Register(
            "BackBrush", typeof(SolidColorBrush), typeof(MainViewTable), new PropertyMetadata(default(SolidColorBrush)));

        
        public SolidColorBrush BackBrush1
        {
            get { return (SolidColorBrush)GetValue(BackBrush1Property); }
            set { SetValue(BackBrush1Property, value); }
        }

        public static readonly DependencyProperty BackBrush2Property = DependencyProperty.Register(
            "BackBrush", typeof(SolidColorBrush), typeof(MainViewTable), new PropertyMetadata(default(SolidColorBrush)));


        public SolidColorBrush BackBrush2
        {
            get { return (SolidColorBrush)GetValue(BackBrush2Property); }
            set { SetValue(BackBrush2Property, value); }
        }
    }
}
