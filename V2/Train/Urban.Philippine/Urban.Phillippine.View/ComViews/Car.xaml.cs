using System;
using System.Windows;
using System.Windows.Controls;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.ComViews
{
    /// <summary>
    ///     Car.xaml 的交互逻辑
    /// </summary>
    public partial class Car : UserControl
    {
        public static readonly DependencyProperty PantographStatusProperty = DependencyProperty.Register(
            "PantographStatus", typeof(PantographStatus), typeof(Car),
            new PropertyMetadata(default(PantographStatus), OnPantograohStatusChanged));

        public static readonly DependencyProperty CabOneStatusProperty = DependencyProperty.Register(
            "CabOneStatus", typeof(CabStatus), typeof(Car),
            new PropertyMetadata(default(CabStatus), OnCabOneStatusChanged));

        public static readonly DependencyProperty CabTwoStatusProperty = DependencyProperty.Register(
            "CabTwoStatus", typeof(CabStatus), typeof(Car),
            new PropertyMetadata(default(CabStatus), OnCabTwoStatusChanged));
        public Car()
        {
            InitializeComponent();
        }

        public PantographStatus PantographStatus
        {
            get { return (PantographStatus)GetValue(PantographStatusProperty); }
            set { SetValue(PantographStatusProperty, value); }
        }

        public CabStatus CabOneStatus
        {
            get { return (CabStatus)GetValue(CabOneStatusProperty); }
            set { SetValue(CabOneStatusProperty, value); }
        }

        public CabStatus CabTwoStatus
        {
            get { return (CabStatus)GetValue(CabTwoStatusProperty); }
            set { SetValue(CabTwoStatusProperty, value); }
        }

        private static void OnPantograohStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Car)d).OnPantograohStatusChanged(e);
        }

        private void OnPantograohStatusChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (PantographStatus)
            {
                case PantographStatus.Up:
                    Pantograph.Content = CarGrid.Resources["PantographUp"];
                    break;

                case PantographStatus.Down:
                    Pantograph.Content = CarGrid.Resources["PantographDown"];
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void OnCabOneStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Car)d).OnCabOneStatusChanged(e);
        }

        private void OnCabOneStatusChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (CabOneStatus)
            {
                case CabStatus.Active:
                    CabOne.Content = CarGrid.Resources["CarDrive1"];
                    break;

                case CabStatus.UnActive:
                    CabOne.Content = CarGrid.Resources["CarDrive2"];
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void OnCabTwoStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Car)d).OnCabTwoStatusChanged(e);
        }

        private void OnCabTwoStatusChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (CabTwoStatus)
            {
                case CabStatus.Active:
                    CabTwo.Content = CarGrid.Resources["CarDrive3"];
                    break;

                case CabStatus.UnActive:
                    CabTwo.Content = CarGrid.Resources["CarDrive4"];
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}