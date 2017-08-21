using System.Windows;
using System.Windows.Controls;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.View.Common
{
    public class NavigationRadioButton : RadioButton
    {
        static NavigationRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationRadioButton), new FrameworkPropertyMetadata(typeof(NavigationRadioButton)));
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State", typeof(NavigationButtonState), typeof(NavigationRadioButton), new PropertyMetadata(default(NavigationButtonState)));

        public NavigationButtonState State
        {
            get { return (NavigationButtonState) GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
    }
    
}