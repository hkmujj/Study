using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IIOStateViewModel))]
    public class IOStateViewModel : ViewModelBase, IIOStateViewModel
    {
        private ObservableCollection<IOStateViewUnit> m_IOState;

        public IOStateViewModel()
        {
            var tmp = ExcelParser.Parser<IOStateViewUnit>(GlobalParam.InitParam.AppConfig.AppPaths.ConfigDirectory);
            IOState = new ObservableCollection<IOStateViewUnit>(tmp);
        }

        public ObservableCollection<IOStateViewUnit> IOState
        {
            get { return m_IOState; }
            set
            {
                if (Equals(value, m_IOState))
                {
                    return;
                }
                m_IOState = value;
                RaisePropertyChanged(() => IOState);
            }
        }
    }
}