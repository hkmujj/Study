using System.Windows.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;

namespace Urban.Phillippine.View.Views.Button
{
    /// <summary>
    /// RIOMButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ButtonRegion)]
    public partial class RIOMButtonView : UserControl
    {
        public RIOMButtonView()
        {
            InitializeComponent();
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonInitEvent>().Subscribe((args) =>
            {
                if (args.ViewName == ControlNames.RIOMButtonView)
                {
                    Riom.IsChecked = true;
                }
            });
        }
    }
}