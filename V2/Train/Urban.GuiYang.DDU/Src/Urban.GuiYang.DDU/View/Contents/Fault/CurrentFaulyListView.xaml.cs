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
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.Model.Train;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.View.Contents.Fault
{
    /// <summary>
    /// CurrentFaulyListView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentContentAll)]
    public partial class CurrentFaulyListView : UserControl
    {
        public CurrentFaulyListView()
        {
            InitializeComponent();
            IsVisibleChanged += (sender, args) =>
            {
                var viewmol = ServiceLocator.Current.GetInstance<FaultViewModel>();
                viewmol.Model.IsCurrent = (bool)args.NewValue == true;
                viewmol.Controller.FaultLoad.Execute(null);

            };
        }
    }
}
