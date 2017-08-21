using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Excel.Interface;
using General.CIR.Controller.ViewModelController;
using General.CIR.Interfaces;
using General.CIR.Models;
using General.CIR.Models.States;
using General.CIR.Models.Units;

namespace General.CIR.ViewModels
{
    [Export, Export(typeof(ICIRStatusChanged))]
    public class SettingViewModel : ViewModelBase
    {
        private ObservableCollection<SettingItem> m_AllItems;

        private double m_Screen;

        private SettingItem m_SelectItem;

        private string m_Trips;

        private Visibility m_TripsVisibility;

        private string m_TrainNumber;

        private InputText m_MaininstanceText;

        private InputText m_TrainNumberText;

        public ObservableCollection<SettingItem> AllItems
        {
            get
            {
                return m_AllItems;
            }
            private set
            {
                bool flag = Equals(value, m_AllItems);
                if (!flag)
                {
                    m_AllItems = value;
                    RaisePropertyChanged<ObservableCollection<SettingItem>>(() => AllItems);
                }
            }
        }

        public SettingItem SelectItem
        {
            get
            {
                return m_SelectItem;
            }
            set
            {
                bool flag = Equals(value, m_SelectItem);
                if (!flag)
                {
                    m_SelectItem = value;
                    RaisePropertyChanged<SettingItem>(() => SelectItem);
                }
            }
        }

        public double Screen
        {
            get
            {
                return m_Screen;
            }
            set
            {
                bool flag = value.Equals(m_Screen) || value < 0.0;
                if (!flag)
                {
                    m_Screen = value;
                    RaisePropertyChanged<double>(() => Screen);
                }
            }
        }

        public SettingViewController Controller { get; }

        public string Trips
        {
            get
            {
                return m_Trips;
            }
            set
            {
                bool flag = value == m_Trips;
                if (!flag)
                {
                    m_Trips = value;
                    RaisePropertyChanged<string>(() => Trips);
                }
            }
        }

        public Visibility TripsVisibility
        {
            get
            {
                return m_TripsVisibility;
            }
            set
            {
                bool flag = value == m_TripsVisibility;
                if (!flag)
                {
                    m_TripsVisibility = value;
                    RaisePropertyChanged<Visibility>(() => TripsVisibility);
                }
            }
        }

        public string TrainNumber
        {
            get
            {
                return m_TrainNumber;
            }
            set
            {
                bool flag = value == m_TrainNumber;
                if (!flag)
                {
                    m_TrainNumber = value;
                    TrainNumberText.Text = value;
                    TrainNumberText.BindableCaretIndex = value.Length;
                    RaisePropertyChanged<string>(() => TrainNumber);
                }
            }
        }

        public InputText TrainNumberText
        {
            get
            {
                return m_TrainNumberText;
            }
            set
            {
                bool flag = Equals(value, m_TrainNumberText);
                if (!flag)
                {
                    m_TrainNumberText = value;
                    RaisePropertyChanged("TrainNumberText");
                }
            }
        }

        [Import]
        public OutLibViewModel OutLibViewModel
        {
            get;
            private set;
        }

        public InputText MaininstanceText
        {
            get
            {
                return m_MaininstanceText;
            }
            set
            {
                bool flag = Equals(value, m_MaininstanceText);
                if (!flag)
                {
                    m_MaininstanceText = value;
                    RaisePropertyChanged<InputText>(() => MaininstanceText);
                }
            }
        }

        [ImportingConstructor]
        public SettingViewModel(SettingViewController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            Controller.Initialize();
        }

        public override void Initaliation()
        {
            Load();
            SelectItem = AllItems.FirstOrDefault<SettingItem>();
            TripsVisibility = Visibility.Hidden;
            MaininstanceText = new InputText();
            TrainNumberText = new InputText();
        }

        private void Load()
        {
            IEnumerable<SettingItem> collection = ExcelParser.Parser<SettingItem>(GlobalParams.Instance.SystemConfig.AppConfigPath);
            AllItems = new ObservableCollection<SettingItem>(collection);
        }
    }
}
