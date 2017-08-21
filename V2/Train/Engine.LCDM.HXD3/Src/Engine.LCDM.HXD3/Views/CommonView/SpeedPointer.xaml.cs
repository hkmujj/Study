using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Engine.LCDM.HXD3.Views.CommonView
{
    /// <summary>
    /// SpeedPointer.xaml 的交互逻辑
    /// </summary>
    public partial class SpeedPointer : UserControl
    {
        public SpeedPointer()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty InitAngleRedProperty = DependencyProperty.Register(
            "InitAngleRed", typeof(double), typeof(SpeedPointer), new PropertyMetadata(default(double), OnRedChanged));

        public static readonly DependencyProperty InitAngleWhiteProperty = DependencyProperty.Register(
            "InitAngleWhite", typeof (double), typeof (SpeedPointer),
            new PropertyMetadata(default(double), OnWhiteChanged));
        private static void OnRedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(CurrentAnhleRedProperty, e.NewValue);
        }
        private static void OnWhiteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(CurrentAnhleWhiteProperty, e.NewValue);
        }

        public double InitAngleRed
        {
            get { return (double)GetValue(InitAngleRedProperty); }
            set { SetValue(InitAngleRedProperty, value); }
        }

        public double InitAngleWhite
        {
            get { return (double)GetValue(InitAngleWhiteProperty); }
            set { SetValue(InitAngleWhiteProperty, value); }
        }
        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.Register(
            "MaxAngle", typeof(double), typeof(SpeedPointer), new PropertyMetadata(default(double)));

        public double MaxAngle
        {
            get { return (double)GetValue(MaxAngleProperty); }
            set { SetValue(MaxAngleProperty, value); }
        }
        public static readonly DependencyProperty CurrentAnhleRedProperty = DependencyProperty.Register(
           "CurrentRedAnhle", typeof(double), typeof(SpeedPointer), new PropertyMetadata(default(double), OnCurrentChanged));
        public static readonly DependencyProperty CurrentAnhleWhiteProperty = DependencyProperty.Register(
           "CurrentWhiteAnhle", typeof(double), typeof(SpeedPointer), new PropertyMetadata(default(double), OnCurrentChanged));
        private static void OnCurrentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SpeedPointer)d).CurrenChanged(e);
        }

        private void CurrenChanged(DependencyPropertyChangedEventArgs e)
        {
            RotateTransformRed.Angle = CurrentRedAnhle;
            RotateTransformWhite.Angle = CurrentWhiteAnhle;
        }
        public double CurrentRedAnhle
        {
            get { return (double)GetValue(CurrentAnhleRedProperty); }
            set
            {
                SetValue(CurrentAnhleRedProperty, value);
            }
        }
        public double CurrentWhiteAnhle
        {
            get { return (double) GetValue(CurrentAnhleWhiteProperty); }
            set
            {
                SetValue(CurrentAnhleWhiteProperty,value);
            }
        }
    }
}
