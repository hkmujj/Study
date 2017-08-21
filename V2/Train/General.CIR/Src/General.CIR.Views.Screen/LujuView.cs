using System.Windows.Controls;
using System.Windows.Markup;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace General.CIR.Views.Screen
{
	[ViewExport(RegionName = "General.CIR.ScreenMaster")]
	public partial class LujuView : UserControl, IComponentConnector
	{
		

		public LujuView()
		{
			InitializeComponent();
		}

		private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;
			if (listBox != null)
			{
				listBox.ScrollIntoView(listBox.SelectedItem);
			}
		}

	
	}
}
