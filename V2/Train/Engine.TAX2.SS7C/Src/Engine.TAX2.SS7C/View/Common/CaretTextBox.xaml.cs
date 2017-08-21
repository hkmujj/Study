using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CommonUtil.Util;

namespace Engine.TAX2.SS7C.View.Common
{
    /// <summary>
    /// CaretTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class CaretTextBox
    {
        /// <summary>
        /// 
        /// </summary>
        public CaretTextBox()
        {
            InitializeComponent();

            PartCustomTextBox.SelectionChanged += (sender, args) =>
            {
                if (CaretTextModel != null && PartCustomTextBox.IsFocused)
                {
                    CaretTextModel.BindableCaretIndex = PartCustomTextBox.CaretIndex;
                }
            };
            LostFocus +=
                (sender, e) => PartCaret.Visibility = AlwaysShowCaret ? Visibility.Visible : Visibility.Collapsed;
            GotFocus += (sender, e) => PartCaret.Visibility = Visibility.Visible;
            Loaded += (sender, args) =>
            {
                UpdateCaretVisible();
                MoveCustomCaret();
            };

        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CaretTextModelProperty = DependencyProperty.Register(
            "CaretTextModel", typeof(ICaretTextModel), typeof(CaretTextBox),
            new PropertyMetadata(default(ICaretTextModel), CaretTextModelPropertyChangedCallback));

        /// <summary>
        /// 
        /// </summary>
        public ICaretTextModel CaretTextModel
        {
            get { return (ICaretTextModel) GetValue(CaretTextModelProperty); }
            set { SetValue(CaretTextModelProperty, value); }
        }

        /// <summary>
        ///  掩码
        /// </summary>
        public static readonly DependencyProperty PasswordMaskCharProperty = DependencyProperty.Register(
            "PasswordMaskChar", typeof(char), typeof(CaretTextBox), new PropertyMetadata(default(char)));

        /// <summary>
        /// 掩码
        /// </summary>
        public char PasswordMaskChar
        {
            get { return (char) GetValue(PasswordMaskCharProperty); }
            set { SetValue(PasswordMaskCharProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty AlwaysShowCaretProperty = DependencyProperty.Register(
            "AlwaysShowCaret", typeof(bool), typeof(CaretTextBox),
            new PropertyMetadata(default(bool), AlwaysShowCaretPropertyChangedCallback));

        /// <summary>
        /// 一直显示补字符
        /// </summary>
        public bool AlwaysShowCaret
        {
            get { return (bool) GetValue(AlwaysShowCaretProperty); }
            set { SetValue(AlwaysShowCaretProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CaretControlProperty = DependencyProperty.Register(
            "CaretControl", typeof(FrameworkElement), typeof(CaretTextBox),
            new PropertyMetadata(default(FrameworkElement)));

        /// <summary>
        /// 
        /// </summary>
        public FrameworkElement CaretControl
        {
            get { return (FrameworkElement) GetValue(CaretControlProperty); }
            set { SetValue(CaretControlProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CaretLocationProperty = DependencyProperty.Register(
            "CaretLocation", typeof(CaretLocation), typeof(CaretTextBox), new PropertyMetadata(default(CaretLocation)));

        /// <summary>
        /// 
        /// </summary>
        public CaretLocation CaretLocation
        {
            get { return (CaretLocation) GetValue(CaretLocationProperty); }
            set { SetValue(CaretLocationProperty, value); }
        }

        private static void CaretTextModelPropertyChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs args)
        {
            var o = (CaretTextBox) d;
            var old = args.OldValue as ICaretTextModel;
            if (old != null)
            {
                old.PropertyChanged -= o.CaretTextModelOnPropertyChanged;
            }
            var n = args.NewValue as ICaretTextModel;
            if (n != null)
            {
                n.PropertyChanged += o.CaretTextModelOnPropertyChanged;
                o.UpdateText();
                o.MoveCustomCaret();
            }
        }

        private void CaretTextModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {

            if (args.PropertyName == PropertySupport.ExtractPropertyName<ICaretTextModel, int>(c => c.BindableCaretIndex))
            {
                MoveCustomCaret();
            }
            else if (args.PropertyName == PropertySupport.ExtractPropertyName<ICaretTextModel, string>(c => c.Text))
            {
                UpdateText();
            }
        }

        private void UpdateText()
        {
            if (CaretTextModel != null)
            {
                PartCustomTextBox.Text = PasswordMaskChar != default(char)
                    ? new string(Enumerable.Repeat(PasswordMaskChar, CaretTextModel.Text.Length).ToArray())
                    : CaretTextModel.Text;
            }
        }

        private static void AlwaysShowCaretPropertyChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs args)
        {
            var txt = (CaretTextBox) d;

            txt.UpdateCaretVisible();
        }

        private static void BindableCaretIndexPropertyChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs args)
        {
            ((CaretTextBox) d).MoveCustomCaret();
        }

        private void UpdateCaretVisible()
        {
            if (PartCaret == null)
            {
                return;
            }

            if (AlwaysShowCaret)
            {
                PartCaret.Visibility = Visibility.Visible;
            }
            else if (IsFocused)
            {
                PartCaret.Visibility = Visibility.Visible;
            }
            else
            {
                PartCaret.Visibility = Visibility.Collapsed;
            }
        }

        private void MoveCustomCaret()
        {
            if (PartCustomTextBox == null || CaretTextModel == null)
            {
                return;
            }

            var idx = CaretTextModel.BindableCaretIndex - (CaretLocation == CaretLocation.Bottom ? 1 : 0);

            if (idx < 0)
            {
                if (!PartCustomTextBox.Text.Any())
                {
                    return;
                }

                CaretTextModel.BindableCaretIndex = 1;
                idx = 0;
            }

            var caretRect = PartCustomTextBox.GetRectFromCharacterIndex(idx);
            var caretLocation = caretRect.Location;

            if (!double.IsInfinity(caretLocation.X))
            {
                Canvas.SetLeft(PartCaret, caretLocation.X);
            }

            if (!double.IsInfinity(caretLocation.Y))
            {
                Canvas.SetTop(PartCaret,
                    caretLocation.Y + (CaretLocation == CaretLocation.Bottom ? caretRect.Height : 0));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICaretTextModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        int BindableCaretIndex { set; get; }

        /// <summary>
        /// 显示文本
        /// </summary>
        string Text { set; get; }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum CaretLocation
    {
        /// <summary>
        /// 
        /// </summary>
        Right,

        /// <summary>
        /// 
        /// </summary>
        Bottom
    }
}
