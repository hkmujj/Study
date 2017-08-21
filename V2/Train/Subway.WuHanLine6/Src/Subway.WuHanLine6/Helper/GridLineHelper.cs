using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Subway.WuHanLine6.Helper
{
    /// <summary>
    /// Grid Line 帮助类
    /// </summary>
    public class GridLineHelper
    {
        /// <summary>
        /// 显示线条
        /// </summary>
        public static readonly DependencyProperty ShowLineProperty = DependencyProperty.RegisterAttached(
            "ShowLine", typeof(bool), typeof(GridLineHelper), new PropertyMetadata(default(bool), OnShowLineChanged));

        private static void OnShowLineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as Grid;
            if (grid != null)
            {
                if ((bool)e.OldValue)
                {
                    // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
                    grid.Loaded -= GridLoad;
                }
                if ((bool)e.NewValue)
                {
                    grid.Loaded += GridLoad;
                }
            }
        }

        private static void GridLoad(object sender, RoutedEventArgs args)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                var rowCount = grid.RowDefinitions.Count;
                var columnCount = grid.ColumnDefinitions.Count;
                var controls = grid.Children;
                var count = controls.Count;

                for (var i = 0; i < count; i++)
                {
                    var item = controls[i] as FrameworkElement;
                    if (item != null)
                    {
                        var tmp = item as Border;
                        if (tmp == null)
                        {
                            var row = Grid.GetRow(item);
                            var column = Grid.GetColumn(item);
                            var rowspan = Grid.GetRowSpan(item);
                            var columnspan = Grid.GetColumnSpan(item);
                            var settingThickness = GetLineThickness(grid);
                            var thickness = new Thickness(settingThickness / 2);
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
                            var tmpBrush = GetLineBrush(grid);

                            var border = new Border
                            {
                                BorderBrush = tmpBrush,
                                BorderThickness = thickness,
                                Padding = item.Margin
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
        }

        /// <summary>
        /// 设置ShowLine附加属性
        /// </summary>
        /// <param name="element">附加属性的控件</param>
        /// <param name="value">值</param>
        public static void SetShowLine(DependencyObject element, bool value)
        {
            element.SetValue(ShowLineProperty, value);
        }

        /// <summary>
        /// 获取ShouLine的值
        /// </summary>
        /// <param name="element">附加的控件</param>
        /// <returns></returns>
        public static bool GetShowLine(DependencyObject element)
        {
            return (bool)element.GetValue(ShowLineProperty);
        }

        /// <summary>
        /// 线条颜色
        /// </summary>
        public static readonly DependencyProperty LineBrushProperty = DependencyProperty.RegisterAttached(
            "LineBrush", typeof(Brush), typeof(GridLineHelper), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 设置线条颜色
        /// </summary>
        /// <param name="element">附加的控件</param>
        /// <param name="value">值</param>
        public static void SetLineBrush(DependencyObject element, Brush value)
        {
            element.SetValue(LineBrushProperty, value);
        }

        /// <summary>
        /// 获取线条颜色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetLineBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(LineBrushProperty);
        }

        /// <summary>
        /// 线条大小
        /// </summary>
        public static readonly DependencyProperty LineThicknessProperty = DependencyProperty.RegisterAttached(
            "LineThickness", typeof(double), typeof(GridLineHelper), new PropertyMetadata(default(double)));

        /// <summary>
        ///
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetLineThickness(DependencyObject element, double value)
        {
            element.SetValue(LineThicknessProperty, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetLineThickness(DependencyObject element)
        {
            return (double)element.GetValue(LineThicknessProperty);
        }
    }
}