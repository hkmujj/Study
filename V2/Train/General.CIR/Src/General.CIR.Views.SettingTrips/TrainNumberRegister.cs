using System.Windows.Controls;
using System.Windows.Markup;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace General.CIR.Views.SettingTrips
{
	[ViewExport(RegionName = "General.CIR.SettingTrips")]
	public partial class TrainNumberRegister : UserControl, IComponentConnector
	{

		public TrainNumberRegister()
		{
			InitializeComponent();
		}

	}
}
