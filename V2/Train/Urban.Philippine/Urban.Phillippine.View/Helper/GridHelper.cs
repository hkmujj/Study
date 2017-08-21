using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Urban.Phillippine.View.Helper
{
    public class GridHelper
    {
        public static readonly DependencyProperty ShowBorderProperty =
            DependencyProperty.RegisterAttached("ShowBorder", typeof(bool), typeof(GridHelper),
                new PropertyMetadata(OnShowBorderChanged));

        public static readonly DependencyProperty GridLineThicknessProperty =
            DependencyProperty.RegisterAttached("GridLineThickness", typeof(double), typeof(GridHelper),
                new PropertyMetadata(OnGridLineThicknessChanged));

        public static readonly DependencyProperty GridLineBrushProperty =
            DependencyProperty.RegisterAttached("GridLineBrush", typeof(Brush), typeof(GridHelper),
                new PropertyMetadata(OnGridLineBrushChanged));

        private static void GridLoaded(object sender, RoutedEventArgs e)
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
                            var settingThickness = GetGridLineThickness(grid);
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
                            var tmpBrush = GetGridLineBrush(grid);

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

        #region ShowBorder

        public static bool GetShowBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowBorderProperty);
        }

        public static void SetShowBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowBorderProperty, value);
        }

        private static void OnShowBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as Grid;
            if (grid != null)
            {
                if ((bool)e.OldValue)
                {
                    // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
                    grid.Loaded -= (s, arg) => { };
                }
                if ((bool)e.NewValue)
                {
                    grid.Loaded += GridLoaded;
                }
            }
        }

        #endregion ShowBorder

        #region GridLineThickness

        public static double GetGridLineThickness(DependencyObject obj)
        {
            return (double)obj.GetValue(GridLineThicknessProperty);
        }

        public static void SetGridLineThickness(DependencyObject obj, double value)
        {
            obj.SetValue(GridLineThicknessProperty, value);
        }

        private static void OnGridLineThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion GridLineThickness

        #region GridLineBrush

        public static Brush GetGridLineBrush(DependencyObject obj)
        {
            var brush = (Brush)obj.GetValue(GridLineBrushProperty);
            return brush ?? Brushes.LightGray;
        }

        public static void SetGridLineBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(GridLineBrushProperty, value);
        }

        private static void OnGridLineBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion GridLineBrush
    }
}