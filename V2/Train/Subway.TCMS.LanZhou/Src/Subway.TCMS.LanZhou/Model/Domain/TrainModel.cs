using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.Domain.Car;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain
{
    [Export]
    public class TrainModel : NotificationObject
    {
        private List<CarModel> m_CarModels;
        private IEnumerable<IEnumerable<CarModel>> m_GroupedCarModels;
        private int m_CurrentShowingGroupId;
        private RunningModel m_RunningModel; 
        private float m_TouchNetVoltage;
        private float m_CurrentSpeed;
        private TrainBreakWarning m_TrainBreakWarning;
        private RunningDirection m_RunningDirection;
        private SpeedLimitReason m_SpeedLimitReason;
        private IEnumerable<CarModel> m_CurrentShowingCars;
        private bool m_IsSelectedTowPage;
        private CarTowBrakePercentData m_CarTowBrakePertenctBase;

        public TrainModel()
        {
            m_CarTowBrakePertenctBase = new CarTowBrakePercentData();
        }

        public RunningDirection RunningDirection {
            get { return m_RunningDirection; }
            set
            {
                if (value == m_RunningDirection)
                {
                    return;
                }

                m_RunningDirection = value;
                RaisePropertyChanged(() => RunningDirection);
            }
        }

        public TrainBreakWarning TrainBreakWarning
        {
            get { return m_TrainBreakWarning; }
            set
            {
                if (value == m_TrainBreakWarning)
                {
                    return;
                }

                m_TrainBreakWarning = value;
                RaisePropertyChanged(() => TrainBreakWarning);
            }
        }

        public bool IsSelectedTowPage
        {
            get { return m_IsSelectedTowPage; }
            set
            {
                if (value == m_IsSelectedTowPage)
                {
                    return;
                }
                m_IsSelectedTowPage = value;
                RaisePropertyChanged(() => IsSelectedTowPage);
            }
        }

        public List<CarModel> CarModels
        {
            get { return m_CarModels; }
            set
            {
                if (Equals(value, m_CarModels))
                {
                    return;
                }
                m_CarModels = value;
                UpdateCurrentShowingCars();
                RaisePropertyChanged(() => CarModels);
            }
        }

        public int CurrentShowingGroupId
        {
            get { return m_CurrentShowingGroupId; }
            set
            {
                if (value == m_CurrentShowingGroupId)
                {
                    return;
                }
                m_CurrentShowingGroupId = value;
                UpdateCurrentShowingCars();
                RaisePropertyChanged(() => CurrentShowingGroupId);
            }
        }

        public IEnumerable<CarModel> CurrentShowingCars
        {
            private set
            {
                if (Equals(value, m_CurrentShowingCars))
                {
                    return;
                }
                m_CurrentShowingCars = value;
                RaisePropertyChanged(() => CurrentShowingCars);
            }
            get { return m_CurrentShowingCars; }
        }

        //public ObservableCollection<float> MainWindPressure { get; set; }

        public float CurrentSpeed
        {
            get { return m_CurrentSpeed; }
            set
            {
                if (value.Equals(m_CurrentSpeed))
                {
                    return;
                }
                m_CurrentSpeed = value;
                RaisePropertyChanged(() => CurrentSpeed);
            }
        }

        public float TouchNetVoltage
        {
            get { return m_TouchNetVoltage; }
            set
            {
                if (value.Equals(m_TouchNetVoltage))
                {
                    return;
                }
                m_TouchNetVoltage = value;
                RaisePropertyChanged(() => TouchNetVoltage);
            }
        }

        public CarTowBrakePercentData CarTowBrakePercentData {
            get { return m_CarTowBrakePertenctBase; }
            set
            {
                if (value == m_CarTowBrakePertenctBase)
                {
                    return;
                }
                m_CarTowBrakePertenctBase = value;
                RaisePropertyChanged(() => CarTowBrakePercentData);
            }
        }

        public RunningModel RunningModel
        {
            get { return m_RunningModel; }
            set
            {
                if (value == m_RunningModel)
                {
                    return;
                }
                m_RunningModel = value;
                RaisePropertyChanged(() => RunningModel);
            }
        }

        public SpeedLimitReason SpeedLimitReason
        {
            get { return m_SpeedLimitReason; }
            set
            {
                if (value == m_SpeedLimitReason)
                {
                    return;
                }
                m_SpeedLimitReason = value;
                RaisePropertyChanged(() => SpeedLimitReason);
            }
        }
        

        public void UpdateCurrentShowingCars()
        {
            CurrentShowingCars = CarModels.Where(w => w.GroupId == CurrentShowingGroupId);
        }

    }
}