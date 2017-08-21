using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IVVVFViewModel))]
    public class VVVFViewModel : ViewModelBase, IVVVFViewModel
    {
        private ObservableCollection<VVVFViewUnit> m_VVVFViewUnits;

        public VVVFViewModel()
        {
            var tmp = ExcelParser.Parser<VVVFViewUnit>(GlobalParam.InitParam.AppConfig.AppPaths.ConfigDirectory);
            VVVFViewUnits = new ObservableCollection<VVVFViewUnit>(tmp);
        }

        public ObservableCollection<VVVFViewUnit> VVVFViewUnits
        {
            get { return m_VVVFViewUnits; }
            set
            {
                if (Equals(value, m_VVVFViewUnits))
                {
                    return;
                }
                m_VVVFViewUnits = value;
                RaisePropertyChanged(() => VVVFViewUnits);
            }
        }
    }
}