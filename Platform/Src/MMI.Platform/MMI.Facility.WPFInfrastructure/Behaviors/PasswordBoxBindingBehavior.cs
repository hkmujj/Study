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

        /// <summary>����Ϊ���ӵ� AssociatedObject ����á�</summary>
        /// <remarks>������Ա㽫���ܹҹ��� AssociatedObject��</remarks>
        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += AssociatedObjectOnPasswordChanged;
        }

        /// <summary>����Ϊ���� AssociatedObject ����ʱ��������ʵ�ʷ���֮ǰ�����á�</summary>
        /// <remarks>������Ա㽫���ܴ� AssociatedObject �н���ҹ���</remarks>
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