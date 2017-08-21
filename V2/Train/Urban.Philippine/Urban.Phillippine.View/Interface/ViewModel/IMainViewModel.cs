using System.Collections.ObjectModel;
using System.Windows;
using Urban.Phillippine.View.Config.MainViewStatusConfig;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IMainViewModel : IViewModelBase, IStatusChanged
    {
        ObservableCollection<DoorUnit> DoorUnits { get; set; }
        ObservableCollection<VVVFUnit> VVVFUnits { get; set; }
        ObservableCollection<APSUnit> APSUnits { get; set; }
        ObservableCollection<HscbUnit> HscbUnits { get; set; }
        ObservableCollection<BogieUnit> BogieUnits { get; set; }
        ObservableCollection<VACUnit> VACUnits { get; set; }
        ObservableCollection<TemperatureUnit> TemperatureUnits { get; set; }
        ObservableCollection<PcuUnit> PcuUnits { get; set; }
        ObservableCollection<PantographUnit> PantographUnits { get; set; }
        ObservableCollection<CabUnit> CabUnits { get; set; }
        Dirction? Dirction { get; set; }
        string TrainNumber { get; set; }
        string Maintainer { get; set; }
        string MasterNumber { get; set; }
        AtpModel AtpModel { get; set; }
        Visibility TarinNoVisibility { get; set; }
        Visibility MaintainerVisibility { get; set; }
        Visibility MasterVisibility { get; set; }
        Visibility AtpModelVisibility { get; set; }
        bool TractionAloow { get; set; }
        bool Sanding { get; set; }
        bool Sliding { get; set; }
        bool SandBoxLow { get; set; }

        /// <summary>
        ///     限速
        /// </summary>
        double LimitSpeed { get; set; }
    }
}