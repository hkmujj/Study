using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Engine.HMI.SS3B.Interface;

namespace Engine.HMI.SS3B.CommonView
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
                var height = this.Grid.ActualHeight;
                var width = this.Grid.ActualWidth;
                var rex = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                    StrokeThickness = 1
                };
                var lineArrowMarginY = (double)height * (1 - (double)GraduationResouce.GetScal(GraduationResouce.Enmergency));
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
                var lineHeight = this.Grid.ActualHeight / GraduationResouce.LineCount;
                var isDrawText = GraduationResouce.Souce != null && GraduationResouce.Souce.Keys.Count > 0;
                for (int i = GraduationResouce.LineCount - 1; i >= 0; i--)
                {
                    var y = lineHeight * (i + 1);
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
                                    Top = y,
                                    Right = 0,
                                    Bottom = 0,
                                },
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top
                            };
                            this.Grid.Children.Add(text);
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
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top
                            };
                            this.Grid.Children.Add(text);
                        }

                    }
                    if ((i + 1) % (GraduationResouce.Contrast + 1) == 0)
                    {
                        var line = new Line()
                        {
                            X1 = width * (1 - GraduationResouce.LongLength),
                            X2 = width,
                            Y1 = y,
                            Y2 = y,
                            Stroke = Brushes.Yellow,
                            StrokeThickness = 1
                        };
                        this.Grid.Children.Add(line);
                    }
                    else
                    {
                        var line = new Line()
                        {
                            X1 = width * (1 - GraduationResouce.ShortLength),
                            X2 = width,
                            Y1 = y,
                            Y2 = y,
                            Stroke = Brushes.Yellow,
                            StrokeThickness = 1,

                        };
                        this.Grid.Children.Add(line);
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
            this.Grid.Loaded += Canvas_Loaded;
        }

        void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            InitLine();
        }

    }
}
