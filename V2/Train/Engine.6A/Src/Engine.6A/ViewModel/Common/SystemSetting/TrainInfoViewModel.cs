using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;

namespace Engine._6A.ViewModel.Common.SystemSetting
{
    [Export(typeof(ITrainInfoViewModel))]
    public class TrainInfoViewModel : ViewModelBase, ITrainInfoViewModel
    {
        [Import]
        public IMonitorDataViewModel MonitorData { get; private set; }
        [Import]
        public IMicrocomputerInfoViewModel MicrocomputerInfo { get; private set; }
        [Import]
        public IElectropsychometerTestViewModel ElectropsychometerTest { get; private set; }
        [Import]
        public IOlLevelMeterTestViewModel OlLevelMeterTest { get; private set; }

        public TrainInfoViewModel()
        {
            ViewCollection = new ObservableCollection<string>
            {
                CoontrolNameBase.MonitorView,
                CoontrolNameBase.MicrocomputerInfoView,
                CoontrolNameBase.ElectropsychometerTestView,
                CoontrolNameBase.OilLevelMeterTestView
            };

        }
        public ObservableCollection<string> ViewCollection { get; set; }
    }
}