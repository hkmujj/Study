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
using Engine.LCDM.HXD3.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HXD3.Views.CommonView
{
    /// <summary>
    /// TrainNumbInfo.xaml 的交互逻辑
    /// </summary>
    public partial class TrainNumbInfo : UserControl
    {
        public TrainNumbInfo()
        {
            InitializeComponent();
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<PictureIndexChanged>().Subscribe((args) =>
            {
                Grid.SetColumn(Image, args.Index);
            });
        }
    }
}
