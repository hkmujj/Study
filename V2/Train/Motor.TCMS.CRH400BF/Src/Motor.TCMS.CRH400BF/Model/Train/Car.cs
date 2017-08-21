using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Model.Constant;
using Motor.TCMS.CRH400BF.Model.Train.CartPart;

namespace Motor.TCMS.CRH400BF.Model.Train
{
    [DebuggerDisplay("Name={Name}, Index={Index}")]
    public class Car : NotificationObject
    {
        private string m_Name;
       
        private DoorState m_UpDoorState;
        private DoorState m_DownDoorState;
        private float m_TowPercentSet;
        private float m_TowPercentNow;
        private float m_BrakePercentSet;
        private float m_BrakePercentNow;


        private float m_ShowPercentSet;
        private float m_ShowPercentNow;
        public Car(string name, int index)
        {

            Name = name;
            Index = index;

        }
        public int Index { get; private set; }

        public float ShowPercentSet
        {
            get { return m_ShowPercentSet; }
            set
            {
                if (value.Equals(m_ShowPercentSet))
                {
                    return;
                }

                m_ShowPercentSet = value;
                RaisePropertyChanged(() => ShowPercentSet);
            }
        }

        public float ShowPercentNow
        {
            get { return m_ShowPercentNow; }
            set
            {
                if (value.Equals(m_ShowPercentNow))
                {
                    return;
                }

                m_ShowPercentNow = value;
                RaisePropertyChanged(() => ShowPercentNow);
            }
        }


        public float TowPercentSet
        {
            get { return m_TowPercentSet; }
            set
            {
                if (value.Equals(m_TowPercentSet))
                {
                    return;
                }

                m_TowPercentSet = value;
                RaisePropertyChanged(() => TowPercentSet);
            }
        }

        public float TowPercentNow
        {
            get { return m_TowPercentNow; }
            set
            {
                if (value.Equals(m_TowPercentNow))
                {
                    return;
                }

                m_TowPercentNow = value;
                RaisePropertyChanged(() => TowPercentNow);
            }
        }

        public float BrakePercentSet
        {
            get { return m_BrakePercentSet; }
            set
            {
                if (value.Equals(m_BrakePercentSet))
                {
                    return;
                }

                m_BrakePercentSet = value;
                RaisePropertyChanged(() => BrakePercentSet);
            }
        }

        public float BrakePercentNow
        {
            get { return m_BrakePercentNow; }
            set
            {
                if (value.Equals(m_BrakePercentNow))
                {
                    return;
                }

                m_BrakePercentNow = value;
                RaisePropertyChanged(() => BrakePercentNow);
            }
        }

        public DoorState DownDoorState
        {
            get { return m_DownDoorState; }
            private set
            {
                if (value == m_DownDoorState)
                {
                    return;
                }

                m_DownDoorState = value;
                RaisePropertyChanged(() => DownDoorState);
            }
        }
        public DoorState UpDoorState
        {
            get { return m_UpDoorState; }
            private set
            {
                if (value == m_UpDoorState)
                {
                    return;
                }

                m_UpDoorState = value;
                RaisePropertyChanged(() => UpDoorState);
            }
        }
        public string Name
        {
            get { return m_Name; }
            private set
            {
                if (value == m_Name)
                {
                    return;
                }

                m_Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        public Lazy<TowPage> TowPage { get; set; }
        public Lazy<BrakePage> BrakePage { get; set; }
        public Door Door { get; set; }
        public Lazy<BrakeInfoPage> BrakeInfoPage { get; set; }
        public Lazy<EquipmentCutOffPage> EquipmentCutOffPage { get; set; }
    }
}
