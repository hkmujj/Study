using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Engine.HMI.SS3B.CommonView;
using Engine.HMI.SS3B.Interface;

namespace Engine.HMI.SS3B.View.View.KunMing
{
    /// <summary>
    /// GraduationLine.xaml 的交互逻辑
    /// </summary>
    public partial class GraduationLine : UserControl
    {
        private IGraduationResouce m_GraduationResouce;


        private void InitLine()
        {
            Grid.Children.Clear();
            if (GraduationResouce != null)
            {
                var height = Grid.ActualHeight;
                var width = Grid.ActualWidth;
                var rex = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                    StrokeThickness = 1
                };
                var lineArrowMarginY = (double)(height - GraduationResouce.Offset) * (1 - (double)GraduationResouce.GetScal(GraduationResouce.Enmergency));
                var arr = new RightArrow
                {
                    Margin = new Thickness(0.6 * width, lineArrowMarginY, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                };
                var arr1 = new LeftArrow()
                {
                    Margin = new Thickness(0.1 * width, lineArrowMarginY, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                };

                Grid.Children.Add(rex);
                var lineHeight = (Grid.ActualHeight - GraduationResouce.Offset) / GraduationResouce.LineCount;
                var isDrawText = GraduationResouce.Souce != null && GraduationResouce.Souce.Keys.Count > 0;
                for (int i = GraduationResouce.LineCount - 1; i >= 0; i--)
                {
                    var y = lineHeight * (i + 1) + GraduationResouce.Offset;
                    bool isText = GraduationResouce.Souce.ContainsKey(i + 2);
                    if (isDrawText)
                    {
                        if (GraduationResouce.Souce.ContainsKey(i + 1))
                        {
                            var text = new TextBlock()
                            {
                                Text = GraduationResouce.Souce[i + 1].ToString(CultureInfo.InvariantCulture),
                                Foreground = Brushes.Yellow,
                                Margin = new Thickness()
                                {
                                    Left = 0,
                                    Top = y - 2 * lineHeight,
                                    Right = 0,
                                    Bottom = 0,
                                },
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Top
                            };
                            Grid.Children.Add(text);
                        }
                        else if (i == 0 && GraduationResouce.Souce.ContainsKey(i))
                        {
                            var text = new TextBlock()
                            {
                                Text = GraduationResouce.Souce[i].ToString(CultureInfo.InvariantCulture),
                                Foreground = Brushes.Yellow,
                                Margin = new Thickness()
                                {
                                    Left = 0,
                                    Top = 0,
                                    Right = 0,
                                    Bottom = 0,
                                },
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Top
                            };
                            Grid.Children.Add(text);
                        }

                    }
                    if ((i + 1) % (GraduationResouce.Contrast + 1) == 0)
                    {
                        var line = new Line()
                        {
                            X1 = 0,
                            X2 = width * GraduationResouce.LongLength,
                            Y1 = y,
                            Y2 = y,
                            Stroke = Brushes.Yellow,
                            StrokeThickness = isText ? 3 : 1
                        };
                        var line1 = new Line()
                        {
                            X1 = width,
                            X2 = width - width * GraduationResouce.LongLength,
                            Y1 = y,
                            Y2 = y,
                            Stroke = Brushes.Yellow,
                            StrokeThickness = isText ? 3 : 1
                        };
                        Grid.Children.Add(line);
                        Grid.Children.Add(line1);
                    }
                    else
                    {
                        var line = new Line()
                        {
                            X1 = 0,
                            X2 = width * GraduationResouce.ShortLength,
                            Y1 = y,
                            Y2 = y,
                            Stroke = Brushes.Yellow,
                            StrokeThickness = isText ? 3 : 1,

                        };
                        var line1 = new Line()
                        {
                            X1 = width,
                            X2 = width - width * GraduationResouce.ShortLength,
                            Y1 = y,
                            Y2 = y,
                            Stroke = Brushes.Yellow,
                            StrokeThickness = isText ? 3 : 1,

                        };
                        Grid.Children.Add(line);
                        Grid.Children.Add(line1);
                    }
                }
                Grid.Children.Add(arr);
                Grid.Children.Add(arr1);
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            InitLine();
            return base.MeasureOverride(constraint);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            InitLine();
            base.OnRenderSizeChanged(sizeInfo);
        }

        public IGraduationResouce GraduationResouce
        {
            get { return m_GraduationResouce; }
            set
            {
                // ReSharper disable once RedundantCheckBeforeAssignment
                if (m_GraduationResouce == value)
                {
                    return;
                }
                m_GraduationResouce = value;
                //  InitLine();
            }
        }


        public GraduationLine()
        {
            InitializeComponent();
            Grid.Loaded += Canvas_Loaded;
        }

        void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            InitLine();
        }

    }
}
