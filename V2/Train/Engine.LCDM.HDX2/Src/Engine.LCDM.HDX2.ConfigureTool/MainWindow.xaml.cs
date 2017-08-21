using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonUtil.Util;
using Engine.LCDM.HDX2.Entity.Model.Config;

namespace Engine.LCDM.HDX2.ConfigureTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string ConfigFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            "Engine.LCDM.HDX2\\Config\\HXD2LCDMConfig.xml"); 

        public MainWindow()
        {
            InitializeComponent();

            DataContext =
                DataSerialization.DeserializeFromXmlFile<HXD2Config>(ConfigFile);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DataSerialization.SerializeToXmlFile((HXD2Config)DataContext, ConfigFile);
        }
    }
}
