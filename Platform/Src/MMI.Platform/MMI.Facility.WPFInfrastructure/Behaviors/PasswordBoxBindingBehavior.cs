using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public class PasswordBoxBindingBehavior : Behavior<PasswordBox>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
            "Password", typeof(string), typeof(PasswordBoxBindingBehavior), new PropertyMetadata(default(string), PasswordPropertyChangedCallback));

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get { return (string) GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        private static void PasswordPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((PasswordBoxBindingBehavior) d).UpdatePassword(args);
        }

        private void UpdatePassword(DependencyPropertyChangedEventArgs args)
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.Password = args.NewValue == null ? string.Empty : args.NewValue.ToString();
            }
        }

        /// <summary>在行为附加到 AssociatedObject 后调用。</summary>
        /// <remarks>替代它以便将功能挂钩到 AssociatedObject。</remarks>
        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += AssociatedObjectOnPasswordChanged;
        }

        /// <summary>在行为与其 AssociatedObject 分离时（但在它实际发生之前）调用。</summary>
        /// <remarks>替代它以便将功能从 AssociatedObject 中解除挂钩。</remarks>
        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= AssociatedObjectOnPasswordChanged;
        }

        private void AssociatedObjectOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            var passwordBox = (PasswordBox)sender;

            if (!string.Equals(Password, passwordBox.Password))
            {
                Password = passwordBox.Password;
            }
        }
    }
}