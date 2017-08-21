using System;
using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.Domain.TrainState.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.TrainState
{
    [Export]
    public class TrainStateModel : NotificationObject
    {
        private float m_Isin;
        private float m_RunningMileage;
        private TimeSpan m_RunningTime;


        [ImportingConstructor]
        public TrainStateModel()
        {
            TruckState1 = new TruckState();
            TruckState2 = new TruckState();
        }

        public TruckState TruckState1 { get; private set; }

        public TruckState TruckState2 { get; private set; }

        public TimeSpan RunningTime
        {
            get { return m_RunningTime; }
            set
            {
                if (value.Equals(m_RunningTime))
                {
                    return;
                }

                m_RunningTime = value;
                RaisePropertyChanged(() => RunningTime);
            }
        }

        public float RunningMileage
        {
            get { return m_RunningMileage; }
            set
            {
                if (value.Equals(m_RunningMileage))
                {
                    return;
                }

                m_RunningMileage = value;
                RaisePropertyChanged(() => RunningMileage);
            }
        }

        public float Isin
        {
            get { return m_Isin; }
            set
            {
                if (value.Equals(m_Isin))
                {
                    return;
                }

                m_Isin = value;
                RaisePropertyChanged(() => Isin);
            }
        }
    }
}