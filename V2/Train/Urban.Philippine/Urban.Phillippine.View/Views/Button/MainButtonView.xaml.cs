using System.Windows.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;

namespace Urban.Phillippine.View.Views.Button
{
    /// <summary>
    ///     MainButtonView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ButtonRegion, IsDefaultView = true)]
    public partial class MainButtonView : UserControl
    {
        public MainButtonView()
        {
            InitializeComponent();
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonInitEvent>().Subscribe((args) =>
            {
                if (args.ViewName == ControlNames.MainButtonView)
                {
                    MainButton.IsChecked = true;
                }
            },ThreadOption.UIThread);
        }
    }
}