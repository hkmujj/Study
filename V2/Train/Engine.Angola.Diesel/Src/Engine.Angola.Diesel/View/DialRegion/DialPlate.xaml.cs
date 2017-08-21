using System;
using System.Windows;
using System.Windows.Media;

namespace Engine.Angola.Diesel.View.DialRegion
{
    /// <summary>
    /// DialPlate.xaml 的交互逻辑
    /// </summary>
    public partial class DialPlate
    {
        public DialPlate()
        {
            InitializeComponent();
            LongWhite1.Visibility = Visibility.Hidden;
            LongWhite2.Visibility = Visibility.Visible;
            ShortWhite1.Visibility = Visibility.Hidden;
            ShortWhite2.Visibility = Visibility.Visible;
            Red1.Visibility = Visibility.Visible;
            Red2.Visibility = Visibility.Hidden;
        }

        public static readonly DependencyProperty DialPlateStyleProperty = DependencyProperty.Register(
            "DialPlateStyle", typeof(DialPlateStyle), typeof(DialPlate),
            new PropertyMetadata(default(DialPlateStyle), OnDialPlateStyleChanged));

        private static void OnDialPlateStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialPlate) d).OnDialPlateStyleChanged();
        }

        private void OnDialPlateStyleChanged()
        {
            switch (DialPlateStyle)
            {
                case DialPlateStyle.Style1:
                case DialPlateStyle.Style4:
                    LongWhite1.Visibility = Visibility.Hidden;
                    LongWhite2.Visibility = Visibility.Visible;
                    ShortWhite1.Visibility = Visibility.Hidden;
                    ShortWhite2.Visibility = Visibility.Visible;
                    Red1.Visibility = Visibility.Visible;
                    Red2.Visibility = Visibility.Hidden;
                    break;
                case DialPlateStyle.Style2:

                    LongWhite1.Visibility = Visibility.Visible;
                    LongWhite2.Visibility = Visibility.Hidden;
                    ShortWhite1.Visibility = Visibility.Visible;
                    ShortWhite2.Visibility = Visibility.Hidden;
                    Red1.Visibility = Visibility.Hidden;
                    Red2.Visibility = Visibility.Visible;
                    break;
                case DialPlateStyle.Style3:

                    LongWhite1.Visibility = Visibility.Hidden;
                    LongWhite2.Visibility = Visibility.Hidden;
                    ShortWhite1.Visibility = Visibility.Hidden;
                    ShortWhite2.Visibility = Visibility.Hidden;
                    Red1.Visibility = Visibility.Visible;
                    Red2.Visibility = Visibility.Visible;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DialPlateStyle DialPlateStyle
        {
            get { return (DialPlateStyle) GetValue(DialPlateStyleProperty); }
            set { SetValue(DialPlateStyleProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource",
            typeof(ImageSource), typeof(DialPlate), new PropertyMetadata(default(ImageSource)));

        public ImageSource ImageSource
        {
            get { return (ImageSource) GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty PointerAngleProperty = DependencyProperty.Register("PointerAngle",
            typeof(double), typeof(DialPlate), new PropertyMetadata(default(double)));

        public double PointerAngle
        {
            get { return (double) GetValue(PointerAngleProperty); }
            set { SetValue(PointerAngleProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(float), typeof(DialPlate), new PropertyMetadata(default(float)));

        public float Value { get { return (float) GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }
    }
}
