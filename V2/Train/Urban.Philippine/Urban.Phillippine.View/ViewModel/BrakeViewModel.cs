using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IBrakeViewModel))]
    public class BrakeViewModel : ViewModelBase, IBrakeViewModel
    {
        public BrakeViewModel()
        {
            var path = GlobalParam.InitParam.AppConfig.AppPaths.ConfigDirectory;
            Brake = new ObservableCollection<BrakeViewUnit>(ExcelParser.Parser<BrakeViewUnit>(path));
        }

        public ObservableCollection<BrakeViewUnit> Brake { get; set; }
    }
}