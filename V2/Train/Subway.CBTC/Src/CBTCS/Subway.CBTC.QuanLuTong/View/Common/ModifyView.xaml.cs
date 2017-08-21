using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Subway.CBTC.QuanLuTong.View.Common
{
    /// <summary>
    /// ModifyView.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyView 
    {
        public ModifyView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HeadImageProperty = DependencyProperty.Register(
            "HeadImage", typeof(ImageSource), typeof(ModifyView), new PropertyMetadata(default(ImageSource)));

        public ImageSource HeadImage
        {
            get { return (ImageSource) GetValue(HeadImageProperty); }
            set { SetValue(HeadImageProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(ModifyView), new PropertyMetadata(default(double)));

        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register(
            "AddCommand", typeof(ICommand), typeof(ModifyView), new PropertyMetadata(default(ICommand)));

        public ICommand AddCommand
        {
            get { return (ICommand) GetValue(AddCommandProperty); }
            set { SetValue(AddCommandProperty, value); }
        }

        public static readonly DependencyProperty DecreaseCommandProperty = DependencyProperty.Register(
            "DecreaseCommand", typeof(ICommand), typeof(ModifyView), new PropertyMetadata(default(ICommand)));

        public ICommand DecreaseCommand
        {
            get { return (ICommand) GetValue(DecreaseCommandProperty); }
            set { SetValue(DecreaseCommandProperty, value); }
        }
    }
}
