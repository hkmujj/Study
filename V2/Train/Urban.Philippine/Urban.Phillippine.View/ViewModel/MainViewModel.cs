using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using Excel.Interface;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Config.MainViewStatusConfig;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface.Enum;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IMainViewModel))]
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private ObservableCollection<APSUnit> m_APSUnits;
        private AtpModel m_AtpModel;
        private ObservableCollection<BogieUnit> m_BogieUnits;
        private Dirction? m_Dirction;
        private ObservableCollection<DoorUnit> m_DoorUnits;
        private ObservableCollection<HscbUnit> m_HscbUnits;
        private double m_LimitSpeed;
        private string m_Maintainer;
        private string m_MasterNumber;
        private ObservableCollection<PantographUnit> m_PantographUnits;
        private ObservableCollection<PcuUnit> m_PcuUnits;
        private bool m_SandBoxLow;
        private bool m_Sanding;
        private bool m_Sliding;
        private ObservableCollection<TemperatureUnit> m_TemperatureUnits;
        private bool m_TractionAloow;
        private string m_TrainNumber;
        private ObservableCollection<VACUnit> m_VACUnits;
        private ObservableCollection<VVVFUnit> m_VVVFUnits;
        private Visibility m_AtpModelVisibility;
        private Visibility m_MasterVisibility;
        private Visibility m_MaintainerVisibility;
        private Visibility m_TarinNoVisibility;

        public MainViewModel()
        {
            var path = GlobalParam.InitParam.AppConfig.AppPaths.ConfigDirectory;
            DoorUnits = new ObservableCollection<DoorUnit>(ExcelParser.Parser<DoorUnit>(path));
            VVVFUnits = new ObservableCollection<VVVFUnit>(ExcelParser.Parser<VVVFUnit>(path));
            APSUnits = new ObservableCollection<APSUnit>(ExcelParser.Parser<APSUnit>(path));
            HscbUnits = new ObservableCollection<HscbUnit>(ExcelParser.Parser<HscbUnit>(path));
            BogieUnits = new ObservableCollection<BogieUnit>(ExcelParser.Parser<BogieUnit>(path));
            VACUnits = new ObservableCollection<VACUnit>(ExcelParser.Parser<VACUnit>(path));
            TemperatureUnits = new ObservableCollection<TemperatureUnit>(ExcelParser.Parser<TemperatureUnit>(path));
            PcuUnits = new ObservableCollection<PcuUnit>(ExcelParser.Parser<PcuUnit>(path));
            PantographUnits = new ObservableCollection<PantographUnit>(ExcelParser.Parser<PantographUnit>(path));
            CabUnits = new ObservableCollection<CabUnit>(ExcelParser.Parser<CabUnit>(path));
            TractionAloow = false;
            Sliding = false;
            Sanding = false;
            SandBoxLow = false;
            Dirction = null;
            TrainNumber = "Train No.";
            Maintainer = "Maintainer 123";
            MasterNumber = "Master No.1";
            AtpModel = AtpModel.AtpMode;
        }

        public ObservableCollection<DoorUnit> DoorUnits
        {
            get { return m_DoorUnits; }
            set
            {
                if (Equals(value, m_DoorUnits))
                {
                    return;
                }
                m_DoorUnits = value;
                RaisePropertyChanged(() => DoorUnits);
            }
        }

        public ObservableCollection<VVVFUnit> VVVFUnits
        {
            get { return m_VVVFUnits; }
            set
            {
                if (Equals(value, m_VVVFUnits))
                {
                    return;
                }
                m_VVVFUnits = value;
                RaisePropertyChanged(() => VVVFUnits);
            }
        }

        public ObservableCollection<APSUnit> APSUnits
        {
            get { return m_APSUnits; }
            set
            {
                if (Equals(value, m_APSUnits))
                {
                    return;
                }
                m_APSUnits = value;
                RaisePropertyChanged(() => APSUnits);
            }
        }

        public ObservableCollection<HscbUnit> HscbUnits
        {
            get { return m_HscbUnits; }
            set
            {
                if (Equals(value, m_HscbUnits))
                {
                    return;
                }
                m_HscbUnits = value;
                RaisePropertyChanged(() => HscbUnits);
            }
        }

        public ObservableCollection<BogieUnit> BogieUnits
        {
            get { return m_BogieUnits; }
            set
            {
                if (Equals(value, m_BogieUnits))
                {
                    return;
                }
                m_BogieUnits = value;
                RaisePropertyChanged(() => BogieUnits);
            }
        }

        public ObservableCollection<VACUnit> VACUnits
        {
            get { return m_VACUnits; }
            set
            {
                if (Equals(value, m_VACUnits))
                {
                    return;
                }
                m_VACUnits = value;
                RaisePropertyChanged(() => VACUnits);
            }
        }

        public ObservableCollection<TemperatureUnit> TemperatureUnits
        {
            get { return m_TemperatureUnits; }
            set
            {
                if (Equals(value, m_TemperatureUnits))
                {
                    return;
                }
                m_TemperatureUnits = value;
                RaisePropertyChanged(() => TemperatureUnits);
            }
        }

        public ObservableCollection<PcuUnit> PcuUnits
        {
            get { return m_PcuUnits; }
            set
            {
                if (Equals(value, m_PcuUnits))
                {
                    return;
                }
                m_PcuUnits = value;
                RaisePropertyChanged(() => PcuUnits);
            }
        }

        public ObservableCollection<PantographUnit> PantographUnits
        {
            get { return m_PantographUnits; }
            set
            {
                if (Equals(value, m_PantographUnits))
                {
                    return;
                }
                m_PantographUnits = value;
                RaisePropertyChanged(() => PantographUnits);
            }
        }

        public ObservableCollection<CabUnit> CabUnits { get; set; }

        public Dirction? Dirction
        {
            get { return m_Dirction; }
            set
            {
                if (value == m_Dirction)
                {
                    return;
                }
                m_Dirction = value;
                RaisePropertyChanged(() => Dirction);
            }
        }

        public string TrainNumber
        {
            get { return m_TrainNumber; }
            set
            {
                if (value.Equals(m_TrainNumber))
                {
                    return;
                }
                m_TrainNumber = value;
                RaisePropertyChanged(() => TrainNumber);
            }
        }

        public string Maintainer
        {
            get { return m_Maintainer; }
            set
            {
                if (value.Equals(m_Maintainer))
                {
                    return;
                }
                m_Maintainer = value;
                RaisePropertyChanged(() => Maintainer);
            }
        }

        public string MasterNumber
        {
            get { return m_MasterNumber; }
            set
            {
                if (value.Equals(m_MasterNumber))
                {
                    return;
                }
                m_MasterNumber = value;
                RaisePropertyChanged(() => MasterNumber);
            }
        }

        public AtpModel AtpModel
        {
            get { return m_AtpModel; }
            set
            {
                if (value == m_AtpModel)
                {
                    return;
                }
                m_AtpModel = value;
                RaisePropertyChanged(() => AtpModel);
            }
        }

        public Visibility TarinNoVisibility
        {
            get { return m_TarinNoVisibility; }
            set
            {
                if (value == m_TarinNoVisibility)
                {
                    return;
                }
                m_TarinNoVisibility = value;
                RaisePropertyChanged(() => TarinNoVisibility);
            }
        }

        public Visibility MaintainerVisibility
        {
            get { return m_MaintainerVisibility; }
            set
            {
                if (value == m_MaintainerVisibility)
                {
                    return;
                }
                m_MaintainerVisibility = value;
                RaisePropertyChanged(() => MaintainerVisibility);
            }
        }

        public Visibility MasterVisibility
        {
            get { return m_MasterVisibility; }
            set
            {
                if (value == m_MasterVisibility)
                {
                    return;
                }
                m_MasterVisibility = value;
                RaisePropertyChanged(() => MasterVisibility);
            }
        }

        public Visibility AtpModelVisibility
        {
            get { return m_AtpModelVisibility; }
            set
            {
                if (value == m_AtpModelVisibility)
                {
                    return;
                }
                m_AtpModelVisibility = value;
                RaisePropertyChanged(() => AtpModelVisibility);
            }
        }

        public bool TractionAloow
        {
            get { return m_TractionAloow; }
            set
            {
                if (value == m_TractionAloow)
                {
                    return;
                }
                m_TractionAloow = value;
                RaisePropertyChanged(() => TractionAloow);
            }
        }

        public bool Sanding
        {
            get { return m_Sanding; }
            set
            {
                if (value == m_Sanding)
                {
                    return;
                }
                m_Sanding = value;
                RaisePropertyChanged(() => Sanding);
            }
        }

        public bool Sliding
        {
            get { return m_Sliding; }
            set
            {
                if (value == m_Sliding)
                {
                    return;
                }
                m_Sliding = value;
                RaisePropertyChanged(() => Sliding);
            }
        }

        public bool SandBoxLow
        {
            get { return m_SandBoxLow; }
            set
            {
                if (value == m_SandBoxLow)
                {
                    return;
                }
                m_SandBoxLow = value;
                RaisePropertyChanged(() => SandBoxLow);
            }
        }

        public double LimitSpeed
        {
            get { return m_LimitSpeed; }
            set
            {
                if (value.Equals(m_LimitSpeed))
                {
                    return;
                }
                m_LimitSpeed = (int)value;
                RaisePropertyChanged(() => LimitSpeed);
            }
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            VACUnits.Changed(args, iPara);
            DoorUnits.Changed(args, iPara);
            VVVFUnits.Changed(args, iPara);
            APSUnits.Changed(args, iPara);
            HscbUnits.Changed(args, iPara);
            BogieUnits.Changed(args, iPara);
            PcuUnits.Changed(args, iPara);
            PantographUnits.Changed(args, iPara);
            CabUnits.Changed(args, iPara);
        }

        public void Changed(IDictionary<int, float> args, int? ipara = null)
        {
            TemperatureUnits.Changed(args, ipara);
        }
    }
}