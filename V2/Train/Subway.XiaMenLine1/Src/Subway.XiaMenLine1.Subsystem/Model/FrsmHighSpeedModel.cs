using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Interface.Model;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class FrsmHighSpeedModel : NotificationObject, IFrsmHighSpeed
    {
        private FrsmHighSpeed m_Car5Pantograph;
        private FrsmHighSpeed m_Car2Pantograph;
        private FrsmHighSpeed m_Car5Fram;
        private FrsmHighSpeed m_Car2Fram;
        private FrsmHighSpeed m_Car5HighSpeed;
        private FrsmHighSpeed m_Car4HighSpeed;
        private FrsmHighSpeed m_Car3HighSpeed;
        private FrsmHighSpeed m_Car2HighSpeed;

        public FrsmHighSpeedModel()
        {
            Car2HighSpeed = FrsmHighSpeed.HighDisconnect;
            Car3HighSpeed = FrsmHighSpeed.HighDisconnect;
            Car4HighSpeed = FrsmHighSpeed.HighDisconnect;
            Car5HighSpeed = FrsmHighSpeed.HighDisconnect;

            Car2Fram = FrsmHighSpeed.FramCut;
            Car5Fram = FrsmHighSpeed.FramCut;

            Car2Pantograph = FrsmHighSpeed.PantographDown;
            Car5Pantograph = FrsmHighSpeed.PantographDown;
        }
        public FrsmHighSpeed Car2HighSpeed
        {
            get { return m_Car2HighSpeed; }
            set
            {
                if (value == m_Car2HighSpeed)
                {
                    return;
                }
                m_Car2HighSpeed = value;
                RaisePropertyChanged(() => Car2HighSpeed);
            }
        }

        public FrsmHighSpeed Car3HighSpeed
        {
            get { return m_Car3HighSpeed; }
            set
            {
                if (value == m_Car3HighSpeed)
                {
                    return;
                }
                m_Car3HighSpeed = value;
                RaisePropertyChanged(() => Car3HighSpeed);
            }
        }

        public FrsmHighSpeed Car4HighSpeed
        {
            get { return m_Car4HighSpeed; }
            set
            {
                if (value == m_Car4HighSpeed)
                {
                    return;
                }
                m_Car4HighSpeed = value;
                RaisePropertyChanged(() => Car4HighSpeed);
            }
        }

        public FrsmHighSpeed Car5HighSpeed
        {
            get { return m_Car5HighSpeed; }
            set
            {
                if (value == m_Car5HighSpeed)
                {
                    return;
                }
                m_Car5HighSpeed = value;
                RaisePropertyChanged(() => Car5HighSpeed);
            }
        }

        public FrsmHighSpeed Car2Fram
        {
            get { return m_Car2Fram; }
            set
            {
                if (value == m_Car2Fram)
                {
                    return;
                }
                m_Car2Fram = value;
                RaisePropertyChanged(() => Car2Fram);
            }
        }

        public FrsmHighSpeed Car5Fram
        {
            get { return m_Car5Fram; }
            set
            {
                if (value == m_Car5Fram)
                {
                    return;
                }
                m_Car5Fram = value;
                RaisePropertyChanged(() => Car5Fram);
            }
        }

        public FrsmHighSpeed Car2Pantograph
        {
            get { return m_Car2Pantograph; }
            set
            {
                if (value == m_Car2Pantograph)
                {
                    return;
                }
                m_Car2Pantograph = value;
                RaisePropertyChanged(() => Car2Pantograph);
            }
        }

        public FrsmHighSpeed Car5Pantograph
        {
            get { return m_Car5Pantograph; }
            set
            {
                if (value == m_Car5Pantograph)
                {
                    return;
                }
                m_Car5Pantograph = value;
                RaisePropertyChanged(() => Car5Pantograph);
            }
        }

        public void ChangeStatus(object sender, CommunicationDataChangedArgs e)
        {
            ChangeBools(e.ChangedBools);
            ChangeFloats(e.ChangedFloats);
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

        }
      
        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }
    }
}