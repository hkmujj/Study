using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Engine.Dial.Turkmenistan.Config;
using Engine.Dial.Turkmenistan.Converter;
using Engine.Dial.Turkmenistan.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.Dial.Turkmenistan.Views
{
    /// <summary>
    /// DialShell.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(DialShell))]
    public partial class DialShell : UserControl
    {
        [ImportingConstructor]
        public DialShell(TurkmenistanViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
            Init(viewmodel);
        }

        private void Init(TurkmenistanViewModel viewmdle)
        {
            RootPanel.Children.Clear();

            foreach (var config in viewmdle.Config.Dials)
            {
                Image image = new Image();
                image.Width = config.Width;
                image.Height = config.Height;
                Canvas.SetLeft(image, config.Left);
                Canvas.SetRight(image, config.Right);
                Canvas.SetTop(image, config.Top);
                Canvas.SetBottom(image, config.Bottom);
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                if (config.DialType == DialType.Dial)
                {
                    image.RenderTransform = new RotateTransform(config.Angle);
                }
                else if (config.DialType == DialType.Pointer)
                {
                    string path = string.Empty;
                    if (config.Index == 2)
                    {
                        path = "OneDialBlackPointer";
                    }
                    else if (config.Index == 3)
                    {
                        path = "OneDialRedPointer";
                    }
                    else if (config.Index == 4)
                    {
                        path = "TwoDialBlackPointer";
                    }
                    else if (config.Index == 5)
                    {
                        path = "TwoDialRedPointer";
                    }
                    if (!string.IsNullOrEmpty(path))
                    {
                        Binding binding = new Binding() { Source = viewmdle, Path = new PropertyPath(path), Converter = new AngleConverter() { InitAngle = config.InitAngle, MaxAngle = config.MaxAngle, MinValue = config.MinValue, MaxValue = config.MaxValue } };
                        var ratate = new RotateTransform();

                        BindingOperations.SetBinding(ratate, RotateTransform.AngleProperty, binding);
                        image.RenderTransform = ratate;
                    }

                }

                image.Source = new BitmapImage(new Uri(Path.Combine(GlobalConfigParam.Instance.InitParam.AppConfig.AppPaths.ConfigDirectory, config.ImagePath), UriKind.Absolute));



                Panel.SetZIndex(image, config.ZIndex);
                RootPanel.Children.Add(image);

            }


            foreach (var config in viewmdle.Config.Texts)
            {
                var sta = new StackPanel();

                Canvas.SetRight(sta, config.Right);
                Canvas.SetTop(sta, config.Top);

                sta.Orientation = Orientation.Horizontal;
                for (int i = 0; i < 6; ++i)
                {
                    var text = new TextBlock();
                    text.Foreground = new SolidColorBrush(Colors.DarkGray);
                    text.Style = (Style)this.Resources["LEDTextStyle"];
                    text.FontSize = config.FontSize;
                    if (i % 2 == 0)
                    {
                        text.Text = "8";
                        text.Width = config.LongWith;
                    }
                    else
                    {
                        text.Text = ".";
                        text.Width = config.ShortWith;
                    }
                    sta.Children.Add(text);
                }
                Panel.SetZIndex(sta, config.ZIndex);

                var sta1 = new StackPanel();
                Canvas.SetRight(sta1, config.Right);
                Canvas.SetTop(sta1, config.Top);

                sta1.Orientation = Orientation.Horizontal;
                for (int i = 0; i < 6; ++i)
                {
                    var text = new TextBlock();
                    text.Foreground = new SolidColorBrush(Colors.Yellow);
                    text.Style = (Style)this.Resources["LEDTextStyle"];
                    text.FontSize = config.FontSize;

                    var bing = new Binding() { Source = viewmdle,Mode =BindingMode.OneWay ,Path = new PropertyPath("LEDValue"), Converter = new LEDValueConverter(), ConverterParameter = i };
                    BindingOperations.SetBinding(text, TextBlock.TextProperty, bing);

                    if (i % 2 == 0)
                    {
                        text.Width = config.LongWith;
                    }
                    else
                    {
                        text.Width = config.ShortWith;
                    }
                    sta1.Children.Add(text);
                }
                Panel.SetZIndex(sta, config.ZIndex);
                Panel.SetZIndex(sta1, config.ZIndex + 1);

                RootPanel.Children.Add(sta);
                RootPanel.Children.Add(sta1);
            }


        }
        public DialShell()
        {
            InitializeComponent();
            DataContext = ServiceLocator.Current.GetInstance<TurkmenistanViewModel>();
        }
    }
}