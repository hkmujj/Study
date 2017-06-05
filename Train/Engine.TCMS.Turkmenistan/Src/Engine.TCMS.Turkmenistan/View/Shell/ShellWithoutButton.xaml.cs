using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model;
using Engine.TCMS.Turkmenistan.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.View.Shell
{
    /// <summary>
    /// ShellWithoutButton.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWithoutButton : UserControl
    {
        public ShellWithoutButton()
        {
            InitializeComponent();
            Loaded += ShellWithoutButton_Loaded;
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ReSourceChangedEvent>()
                .Subscribe(ResourceChanged, ThreadOption.UIThread);
        }

        private void ResourceChanged(string obj)
        {
            if (GlobalParam.Instance.IsTurkmenistan)
            {
                this.FontFamily=new FontFamily("Times New Roman");
                this.Resources.MergedDictionaries[0] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Text/StringResource-tm.xaml")),
                };
                this.Resources.MergedDictionaries[1] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanTextStyles-tm.xaml")),
                };
                this.Resources.MergedDictionaries[2] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanButtonStyle-tm.xaml")),
                };
                this.Resources.MergedDictionaries[3] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanItemControlStyle-tm.xaml")),
                };
                this.Resources.MergedDictionaries[4] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanProgressBarStyle-tm.xaml")),
                };
            }
            else
            {
                this.FontFamily = new FontFamily("宋体");
                this.Resources.MergedDictionaries[0] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Text/StringResource-ch.xaml")),
                };
                this.Resources.MergedDictionaries[1] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanTextStyles-ch.xaml")),
                };
                this.Resources.MergedDictionaries[2] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanButtonStyle-ch.xaml")),
                };
                this.Resources.MergedDictionaries[3] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanItemControlStyle-ch.xaml")),
                };
                this.Resources.MergedDictionaries[4] = new ResourceDictionary()
                {
                    Source =
                        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanProgressBarStyle-ch.xaml")),
                };
            }

        }

        private void ShellWithoutButton_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = ServiceLocator.Current.GetInstance<DomainViewModel>();
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source =
                     new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                         Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Text/StringResource-ch.xaml")),
            });
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source =
                    new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                        Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanTextStyles-ch.xaml")),
            });
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source =
                    new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                        Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanButtonStyle-ch.xaml")),
            });
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source =
                    new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                        Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanItemControlStyle-ch.xaml")),
            });
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source =
                    new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                        Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "Styles/TurkmenistanProgressBarStyle-ch.xaml")),
            });
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source =
                    new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                        Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "TurkmenistanResource.xaml")),
            });
           
        }
    }
}
