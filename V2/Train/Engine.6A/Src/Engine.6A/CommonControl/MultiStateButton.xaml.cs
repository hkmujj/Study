using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Engine._6A.EventArgs;

namespace Engine._6A.CommonControl
{
    /// <summary>
    /// MultiStateButton.xaml 的交互逻辑
    /// </summary>
    public partial class MultiStateButton
    {
        public MultiStateButton()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
           "IsSelected", typeof(bool), typeof(MultiStateButton), new PropertyMetadata(default(bool)));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }


        public static readonly RoutedEvent SelectedIndexRoutedEvent = EventManager.RegisterRoutedEvent(
            "SelectedIndexChanged", RoutingStrategy.Bubble, typeof(EventHandler<SelectedIndexChangedRoutedEventArgs>),
            typeof(MultiStateButton));

        public event RoutedEventHandler SelectedIndexChanged
        {
            add { this.AddHandler(SelectedIndexRoutedEvent, value); }
            remove { this.RemoveHandler(SelectedIndexRoutedEvent, value); }
        }

        public static readonly DependencyProperty StateCollectionProperty = DependencyProperty.Register(
            "StateCollection", typeof(IEnumerable<object>), typeof(MultiStateButton),
            new PropertyMetadata(default(IEnumerable<object>), new PropertyChangedCallback(OnStateCollectionChanged)),
            ValidateValueCallback);

        private static bool ValidateValueCallback(object value)
        {
            var objs = value as object[];
            if (objs != null && objs.Any())
            {

            }
            return true;
        }

        public IEnumerable<object> StateCollection
        {
            get { return (IEnumerable<object>)GetValue(StateCollectionProperty); }
            set { SetValue(StateCollectionProperty, value); }
        }


        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex", typeof(int), typeof(MultiStateButton),
            new PropertyMetadata(-1, new PropertyChangedCallback(OnSelectIndexChanged)));

        private static void OnSelectIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var btn = (MultiStateButton)d;
            btn.OnSelectIndexChanged(e);

        }

        void OnSelectIndexChanged(DependencyPropertyChangedEventArgs e)
        {
            var old = (int)e.OldValue;
            if (SelectedIndex < 0)
            {
                return;
            }
            RaiseEvent(new SelectedIndexChangedRoutedEventArgs(SelectedIndexRoutedEvent)
            {
                OldIndex = old,
                NewIndex = SelectedIndex,
                StateCollection = StateCollection
            });
        }
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void ListView1_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnClick()
        {
            base.OnClick();
        }

        private static void OnStateCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var btn = (MultiStateButton)d;
            var arr = e.NewValue as IEnumerable<object>;
            if (arr != null && arr.Any())
            {
                btn.SelectedIndex = 0;
            }
            else
            {
                btn.SelectedIndex = -1;
            }
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            IsPressed = true;

            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            IsPressed = false;

            if (StateCollection != null && StateCollection.Any())
            {
                if (SelectedIndex == -1)
                {
                    SelectedIndex = 0;
                }
                else
                {
                    SelectedIndex = (SelectedIndex + 1) % StateCollection.Count();
                }


            }

            base.OnPreviewMouseUp(e);

        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsPressed = false;
            base.OnMouseLeave(e);
        }

        private void ListView1_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsPressedChanged(e);
        }
    }
}
