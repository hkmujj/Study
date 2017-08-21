using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IAPSViewModel))]
    public class APSViewModel : ViewModelBase, IAPSViewModel
    {
        public APSViewModel()
        {
            var tmp = ExcelParser.Parser<APSViewUnit>(GlobalParam.InitParam.AppConfig.AppPaths.ConfigDirectory);
            APS = new ObservableCollection<APSViewUnit>(tmp);
        }

        public ObservableCollection<APSViewUnit> APS { get; set; }
    }
}