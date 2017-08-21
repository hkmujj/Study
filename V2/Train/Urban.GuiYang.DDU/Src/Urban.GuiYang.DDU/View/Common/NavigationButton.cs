using System.Windows;
using System.Windows.Controls;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.View.Common
{
    public class NavigationButton : Button
    {
        static NavigationButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationButton), new FrameworkPropertyMetadata(typeof(NavigationButton)));
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State", typeof(NavigationButtonState), typeof(NavigationButton), new PropertyMetadata(default(NavigationButtonState)));

        public NavigationButtonState State
        {
            get { return (NavigationButtonState) GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
    }
}