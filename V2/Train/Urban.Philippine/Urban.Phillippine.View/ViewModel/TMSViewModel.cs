using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Excel.Interface;
using Microsoft.Practices.Prism.Commands;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(ITMSViewModel))]
    public class TMSViewModel : ViewModelBase, ITMSViewModel
    {
        private ObservableCollection<TMSViewUnit> m_DisplayTMS;

        public TMSViewModel()
        {
            ChangedDisplay = new DelegateCommand<string>(args =>
            {
                if (string.IsNullOrEmpty(args))
                {
                    return;
                }
                var result = 0;
                if (int.TryParse(args, out result))
                {
                    DisplayTMS = new ObservableCollection<TMSViewUnit>(TMS.Where(w => w.Train == result));
                }
            });
            var tmp = ExcelParser.Parser<TMSViewUnit>(GlobalParam.InitParam.AppConfig.AppPaths.ConfigDirectory);
            TMS = new ObservableCollection<TMSViewUnit>(tmp);
            DisplayTMS = new ObservableCollection<TMSViewUnit>(TMS.Where(w => w.Train == 1));
        }

        public ObservableCollection<TMSViewUnit> TMS { get; set; }

        public ObservableCollection<TMSViewUnit> DisplayTMS
        {
            get { return m_DisplayTMS; }
            set
            {
                if (Equals(value, m_DisplayTMS))
                {
                    return;
                }
                m_DisplayTMS = value;
                RaisePropertyChanged(() => DisplayTMS);
            }
        }

        public ICommand ChangedDisplay { get; set; }
    }
}