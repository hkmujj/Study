using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace General.CIR.Views.Screen
{
	[ViewExport(RegionName = "General.CIR.ScreenMaster")]
	public partial class RunRegionViews : UserControl
	{
		

		public RunRegionViews()
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
