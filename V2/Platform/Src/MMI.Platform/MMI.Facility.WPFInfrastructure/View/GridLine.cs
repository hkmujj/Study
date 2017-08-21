using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MMI.Facility.WPFInfrastructure.View
{
    /// <summary>
    /// 
    /// </summary>
    public enum GridLineStyle
    {
        /// <summary>
        /// 如果有内容才增加
        /// </summary>
        ShowIfHasControl,

        /// <summary>
        /// 所有
        /// </summary>
        ShowAll,
    }

    /// <summary>
    /// Gril 线宽度是否相同
    /// </summary>
    public enum GridLineWidthStyle
    {
        /// <summary>
        /// 所有线相同宽度
        /// </summary>
        AllLineSame,
        /// <summary>
        /// 不同
        /// </summary>
        Different,

    }
    /// <summary>
    /// 
    /// </summary>
    public class GridLine : DependencyObject
    {
        /// <summary>
        /// 线宽的附加属性
        /// </summary>
        public static readonly DependencyProperty WidthStyleProperty = DependencyProperty.RegisterAttached(
            "WidthStyle", typeof(GridLineWidthStyle), typeof(GridLine), new PropertyMetadata(default(GridLineWidthStyle)));

        /// <summary>
        /// 设置线宽样式
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetWidthStyle(DependencyObject element, GridLineWidthStyle value)
        {
            element.SetValue(WidthStyleProperty, value);
        }

        /// <summary>
        /// 获取线宽样式
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static GridLineWidthStyle GetWidthStyle(DependencyObject element)
        {
            return (GridLineWidthStyle)element.GetValue(WidthStyleProperty);
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached(
            "Style", typeof(GridLineStyle), typeof(GridLine),
            new PropertyMetadata(GridLineStyle.ShowAll, OnShowBorderChanged));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetStyle(DependencyObject element, GridLineStyle value)
        {
            element.SetValue(StyleProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static GridLineStyle GetStyle(DependencyObject element)
        {
            return (GridLineStyle)element.GetValue(StyleProperty);
        }


        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty BrushProperty = DependencyProperty.RegisterAttached(
            "Brush", typeof(Brush), typeof(GridLine), new PropertyMetadata(default(Brush), OnShowBorderChanged));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetBrush(DependencyObject element, Brush value)
        {
            element.SetValue(BrushProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(BrushProperty);
        }

        public static readonly DependencyProperty WidthProperty = DependencyProperty.RegisterAttached(
            "Width", typeof(double), typeof(GridLine),
            new PropertyMetadata(1d, OnShowBorderChanged));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetWidth(DependencyObject element, double value)
        {
            element.SetValue(WidthProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetWidth(DependencyObject element)
        {
            return (double)element.GetValue(WidthProperty);
        }

        private static void OnShowBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = (Grid)d;
            grid.Loaded -= GridOnLoaded;
            grid.Loaded += GridOnLoaded;
        }

        private static void GridOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var grid = (Grid)sender;

            switch (GetStyle(grid))
            {
                case GridLineStyle.ShowIfHasControl:
                    AttachBorderIfHasControl(grid);
                    break;
                case GridLineStyle.ShowAll:
                    AttachBorderAll(grid);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            grid.Loaded -= GridOnLoaded;
        }

        private static void AttachBorderAll(Grid grid)
        {
            //确定行和列数  
            var rows = grid.RowDefinitions.Count;
            var columns = grid.ColumnDefinitions.Count;

            if (rows < 1)
            {
                rows = 1;
            }
            if (columns < 1)
            {
                columns = 1;
            }
            var settingThickness = GetWidth(grid);

            //每个格子添加一个Border进去  
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var thickness = new Thickness(GetWidth(grid) / 2);
                    if (i == 0)
                    {
                        thickness.Top = settingThickness;
                    }
                    if (i == rows - 1)
                    {
                        thickness.Bottom = settingThickness;
                    }
                    if (j == 0)
                    {
                        thickness.Left = settingThickness;
                    }
                    if (j == columns - 1)
                    {
                        thickness.Right = settingThickness;
                    }

                    var border = new Border()
                    {
                        BorderBrush = GetBrush(grid),
                        BorderThickness = GetWidthStyle(grid) == GridLineWidthStyle.AllLineSame ? thickness : new Thickness(GetWidth(grid))
                    };

                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    grid.Children.Add(border);
                }
            }
        }

        private static void AttachBorderIfHasControl(Grid grid)
        {
            //这种做法自动将控件移动到Border里面来
            var controls = grid.Children;
            var count = controls.Count;
            var rowCount = grid.RowDefinitions.Count;
            var columnCount = grid.ColumnDefinitions.Count;
            for (int i = 0; i < count; i++)
            {
                var item = controls[i] as FrameworkElement;


                var row = Grid.GetRow(item);
                var column = Grid.GetColumn(item);
                var rowspan = Grid.GetRowSpan(item);
                var columnspan = Grid.GetColumnSpan(item);

                var settingThickness = GetWidth(grid);

                var thickness = new Thickness(GetWidth(grid) / 2);
                if (row == 0)
                {
                    thickness.Top = settingThickness;
                }
                if (row + rowspan == rowCount)
                {
                    thickness.Bottom = settingThickness;
                }
                if (column == 0)
                {
                    thickness.Left = settingThickness;
                }
                if (column + columnspan == columnCount)
                {
                    thickness.Right = settingThickness;
                }
                var border = new Border
                {
                    BorderBrush = GetBrush(grid),
                    BorderThickness = GetWidthStyle(grid) == GridLineWidthStyle.AllLineSame ? thickness : new Thickness(GetWidth(grid)),
                };
                Grid.SetRow(border, row);
                Grid.SetColumn(border, column);
                Grid.SetRowSpan(border, rowspan);
                Grid.SetColumnSpan(border, columnspan);

                grid.Children.RemoveAt(i);
                border.Child = item;
                grid.Children.Insert(i, border);
            }
        }
    }
}
