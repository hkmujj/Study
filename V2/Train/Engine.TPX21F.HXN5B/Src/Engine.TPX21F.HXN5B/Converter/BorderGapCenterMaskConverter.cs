using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Engine.TPX21F.HXN5B.View.Common;

namespace Engine.TPX21F.HXN5B.Converter
{
    public class BorderGapCenterMaskConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var typeFromHandle = typeof(double);
            if (values == null || values.Length != 3 || values[0] == null || values[1] == null ||
                values[2] == null 
                || !(values[0] is GroupBox) ||
                !typeFromHandle.IsInstanceOfType(values[1]) ||
                !typeFromHandle.IsInstanceOfType(values[2])  
                )
            {
                return DependencyProperty.UnsetValue;
            }


            var width = (double) values[1];
            var height = (double) values[2];
     
            if (Math.Abs(width) < double.Epsilon || Math.Abs(height) < double.Epsilon)
            {
                return null;
            }

            var pixels = width;
            var gb = (GroupBox)values[0];
            if (Math.Abs(GroupBoxHeader.GetHeaderWidth(gb) - default (double)) > double.Epsilon)
            {
                pixels = Math.Min(GroupBoxHeader.GetHeaderWidth(gb), width);
            }
            else if (Math.Abs(GroupBoxHeader.GetHeaderMargin(gb) - default (double)) > double.Epsilon)
            {
                pixels = Math.Max(0, width - GroupBoxHeader.GetHeaderMargin(gb));
            }

            var expr = new Grid
            {
                Width = width,
                Height = height
            };
            var columnDefinition = new ColumnDefinition();
            var columnDefinition2 = new ColumnDefinition();
            var columnDefinition3 = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1.0, GridUnitType.Star);
            columnDefinition2.Width = new GridLength(pixels);
            columnDefinition3.Width = new GridLength(1.0, GridUnitType.Star);
            expr.ColumnDefinitions.Add(columnDefinition);
            expr.ColumnDefinitions.Add(columnDefinition2);
            expr.ColumnDefinitions.Add(columnDefinition3);
            var rowDefinition = new RowDefinition();
            var rowDefinition2 = new RowDefinition();
            rowDefinition.Height = new GridLength(height/2.0);
            rowDefinition2.Height = new GridLength(1.0, GridUnitType.Star);
            expr.RowDefinitions.Add(rowDefinition);
            expr.RowDefinitions.Add(rowDefinition2);
            var rectangle = new Rectangle();
            var rectangle2 = new Rectangle();
            var rectangle3 = new Rectangle();
            rectangle.Fill = Brushes.Black;
            rectangle2.Fill = Brushes.Black;
            rectangle3.Fill = Brushes.Black;
            Grid.SetRowSpan(rectangle, 2);
            Grid.SetRow(rectangle, 0);
            Grid.SetColumn(rectangle, 0);
            Grid.SetRow(rectangle2, 1);
            Grid.SetColumn(rectangle2, 1);
            Grid.SetRowSpan(rectangle3, 2);
            Grid.SetRow(rectangle3, 0);
            Grid.SetColumn(rectangle3, 2);
            expr.Children.Add(rectangle);
            expr.Children.Add(rectangle2);
            expr.Children.Add(rectangle3);
            return new VisualBrush(expr);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[]
            {
                Binding.DoNothing
            };
        }
    }

}