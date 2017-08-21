using System;
using System.Windows;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;

namespace Engine.TPX21F.HXN5B.View.Common
{
    /// <summary>
    /// SoftSwtichView.xaml 的交互逻辑
    /// </summary>
    public partial class SoftSwtichItemView
    {
        public SoftSwtichItemView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SwitchDirectionProperty = DependencyProperty.Register(
            "SwitchDirection", typeof(SoftSwitchDirection), typeof(SoftSwtichItemView), new PropertyMetadata(default(SoftSwitchDirection), SwitchDirectionPropertyChangedCallback));

        private static void SwitchDirectionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((SoftSwtichItemView) d).OnSwitchDirectionChanged(args);
        }

        private void OnSwitchDirectionChanged(DependencyPropertyChangedEventArgs args)
        {
            switch (SwitchDirection)
            {
                case SoftSwitchDirection.RightUp:
                    RotateTransform.Angle = 135;
                    break;
                case SoftSwitchDirection.RightDown:
                    RotateTransform.Angle = 235;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public SoftSwitchDirection SwitchDirection
        {
            get { return (SoftSwitchDirection) GetValue(SwitchDirectionProperty); }
            set { SetValue(SwitchDirectionProperty, value); }
        }
    }
}
