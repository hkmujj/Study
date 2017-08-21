using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;

namespace Urban.Phillippine.View.Views.VAC
{
    /// <summary>
    ///     VACShell.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    public partial class VACShell
    {
        public VACShell()
        {
            InitializeComponent();
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonInitEvent>().Subscribe((args) =>
            {
                if (args.ViewName == ControlNames.VACShell)
                {
                    ModelButton.IsChecked = true;
                    // ModelButton.Command.Execute(null);
                }
            }, ThreadOption.UIThread);
        }
    }
}