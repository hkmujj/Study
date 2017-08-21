using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine._6A.Constance;
using Engine._6A.Event;
using Engine._6A.Interface;
using Engine._6A.Interface.ViewModel.DataMonitor;
using Engine._6A.Interface.ViewModel.DataMonitor.ForTheColumn;
using Microsoft.Practices.Prism.Regions;

namespace Engine._6A.ViewModel.Common
{
    [Export(ContractName.Axle6, typeof(IDataMonitorViewModel))]
    public class Axle6DataMonitorViewModel : ViewModelBase, IDataMonitorViewModel
    {
        private IForTheColumnOneViewModel m_ForTheColumnOne;
        private IForTheColumnTwoViewModel m_ForTheColumnTwo;

        public Axle6DataMonitorViewModel()
        {
            ColumnCollection = new ObservableCollection<string>
            {
                Axle6ControlName.ForTheColumnPageOneView,
                Axle6ControlName.ForTheColumnPageTwoView
            };
            EventAggregator.GetEvent<ForTheColumnEven>().Subscribe(args =>
            {
                if (args.TrainName.Contains("Up"))
                {
                    ForTheColumnOne = TrainUpOne;
                    ForTheColumnTwo = TrainUoTwo;
                }
                else
                {
                    ForTheColumnOne = TrainDownOne;
                    ForTheColumnTwo = TrainDownTwo;
                }
            });
            EventAggregator.GetEvent<ColumnEvent>().Subscribe(args =>
            {
                RegionManager.RequestNavigate(args.Region, args.StateCollection.ToArray()[args.NewIndex].ToString());
            });
        }
        [Import]
        public IBrakeViewModel Brake { get; private set; }
        [Import]
        public IFirePreventionViewModel FirePrevention { get; private set; }
        [Import]
        public IInsulationViewModel Insulation { get; private set; }

        [Import(ContractName.Axle6)]
        public IRunLineOneViewModelBase RunLineOne { get; private set; }

        public IForTheColumnOneViewModel ForTheColumnOne
        {
            get { return m_ForTheColumnOne; }
            private set
            {
                if (Equals(value, m_ForTheColumnOne))
                {
                    return;
                }
                m_ForTheColumnOne = value;
                RaisePropertyChanged(() => ForTheColumnOne);
            }
        }

        public IForTheColumnTwoViewModel ForTheColumnTwo
        {
            get { return m_ForTheColumnTwo; }
            private set
            {
                if (Equals(value, m_ForTheColumnTwo))
                {
                    return;
                }
                m_ForTheColumnTwo = value;
                RaisePropertyChanged(() => ForTheColumnTwo);
            }
        }

        [Import(ContractName.TrainUp)]
        public IForTheColumnOneViewModel TrainUpOne { get; private set; }
        [Import(ContractName.TrainDown)]
        public IForTheColumnOneViewModel TrainDownOne { get; private set; }
        [Import(ContractName.TrainUp)]
        public IForTheColumnTwoViewModel TrainUoTwo { get; private set; }
        [Import(ContractName.TrainDown)]
        public IForTheColumnTwoViewModel TrainDownTwo { get; private set; }
        [Import]
        public IRunLineTwoViewModel RunLineTwo { get; private set; }
        [Import]
        public ISleepViewModel Sleep { get; private set; }

        public ObservableCollection<string> ColumnCollection { get; set; }
    }
    [Export(ContractName.Axle8, typeof(IDataMonitorViewModel))]
    public class Axle8DataMonitorViewModel : ViewModelBase, IDataMonitorViewModel
    {
        private IBrakeViewModel m_Brake;
        private IInsulationViewModel m_Insulation;
        private IRunLineOneViewModelBase m_RunLineOne;

        public Axle8DataMonitorViewModel()
        {
            EventAggregator.GetEvent<TrainChangedEvent>().Subscribe((args) =>
            {
                if (args.Page.Equals(CoontrolNameBase.Axle8DataMonitorShell))
                {
                    RunLineOne = args.Name.Contains("A") ? RunLineOnePageOne : RunLineOnePageTwo;
                    Brake = args.Name.Contains("A") ? BrakeOne : BrakeTwo;
                    Insulation = args.Name.Contains("A") ? InsulationOne : InsulationTwo;
                }
            });
        }

        public IBrakeViewModel Brake
        {
            get { return m_Brake; }
            private set
            {
                if (Equals(value, m_Brake))
                {
                    return;
                }
                m_Brake = value;
                RaisePropertyChanged(() => Brake);
            }
        }

        [Import]
        public IBrakeViewModel BrakeOne { get; private set; }
        [Import]
        public IBrakeViewModel BrakeTwo { get; private set; }
        [Import]
        public IFirePreventionViewModel FirePrevention { get; private set; }

        public IInsulationViewModel Insulation
        {
            get { return m_Insulation; }
            private set
            {
                if (Equals(value, m_Insulation))
                {
                    return;
                }
                m_Insulation = value;
                RaisePropertyChanged(() => Insulation);
            }
        }

        [Import]
        public IInsulationViewModel InsulationOne { get; private set; }
        [Import]
        public IInsulationViewModel InsulationTwo { get; private set; }


        public IRunLineOneViewModelBase RunLineOne
        {
            get { return m_RunLineOne; }
            private set
            {
                if (Equals(value, m_RunLineOne))
                {
                    return;
                }
                m_RunLineOne = value;
                RaisePropertyChanged(() => RunLineOne);
            }
        }

        public IForTheColumnOneViewModel ForTheColumnOne { get; set; }
        public IForTheColumnTwoViewModel ForTheColumnTwo { get; set; }

        [Import(ContractName.Axle8)]
        public IRunLineOneViewModelBase RunLineOnePageOne { get; private set; }
        [Import(ContractName.Axle8)]
        public IRunLineOneViewModelBase RunLineOnePageTwo { get; private set; }
        [Import]
        public IRunLineTwoViewModel RunLineTwo { get; private set; }
        [Import]
        public ISleepViewModel Sleep { get; private set; }

        public ObservableCollection<string> ColumnCollection { get; set; }

        public override void Clear()
        {
            base.Clear();
            var type = typeof(IDataMonitorViewModel);
            foreach (var tmp in type.GetProperties().Select(info => info.GetValue(this, null)).OfType<IClearData>())
            {
                tmp.Clear();
            }
        }
    }
}