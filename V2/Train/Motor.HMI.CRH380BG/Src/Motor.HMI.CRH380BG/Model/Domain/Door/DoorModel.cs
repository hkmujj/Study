using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Model.Domain.Door
{
    [Export]
    public class DoorModel : NotificationObject
    {
        private DoorDisableType m_RightDoor1DisableType;
        private DoorDisableType m_RightDoor2DisableType;
        private DoorDisableType m_RightDoor3DisableType;
        private DoorDisableType m_RightDoor4DisableType;
        private DoorDisableType m_RightDoor5DisableType;
        private DoorDisableType m_RightDoor6DisableType;
        private DoorDisableType m_RightDoor7DisableType;
        private DoorDisableType m_RightDoor8DisableType;
        private DoorDisableType m_LeftDoor1DisableType;
        private DoorDisableType m_LeftDoor2DisableType;
        private DoorDisableType m_LeftDoor3DisableType;
        private DoorDisableType m_LeftDoor4DisableType;
        private DoorDisableType m_LeftDoor5DisableType;
        private DoorDisableType m_LeftDoor6DisableType;
        private DoorDisableType m_LeftDoor7DisableType;
        private DoorDisableType m_LeftDoor8DisableType;
        private DoorOpenType m_Door1OpenType;
        private DoorOpenType m_Door2OpenType;
        private DoorOpenType m_Door3OpenType;
        private DoorOpenType m_Door4OpenType;
        private DoorOpenType m_Door6OpenType;
        private DoorOpenType m_Door7OpenType;
        private DoorOpenType m_Door8OpenType;
        private DoorState m_Car1RightDoor2State;
        private DoorState m_Car1LeftDoor2State;
        private DoorState m_Car2RightDoor1State;
        private DoorState m_Car2RightDoor2State;
        private DoorState m_Car2LeftDoor1State;
        private DoorState m_Car2LeftDoor2State;
        private DoorState m_Car3RightDoor1State;
        private DoorState m_Car3RightDoor2State;
        private DoorState m_Car3LeftDoor1State;
        private DoorState m_Car3LeftDoor2State;
        private DoorState m_Car4RightDoor1State;
        private DoorState m_Car4LeftDoor1State;
        private DoorState m_Car6RightDoor1State;
        private DoorState m_Car6RightDoor2State;
        private DoorState m_Car6LeftDoor1State;
        private DoorState m_Car6LeftDoor2State;
        private DoorState m_Car7RightDoor1State;
        private DoorState m_Car7RightDoor2State;
        private DoorState m_Car7LeftDoor1State;
        private DoorState m_Car7LeftDoor2State;
        private DoorState m_Car8RightDoor1State;
        private DoorState m_Car8LeftDoor1State;


        public DoorDisableType RightDoor1DisableType
        {
            set
            {
                if (value == m_RightDoor1DisableType)
                {
                    return;
                }

                m_RightDoor1DisableType = value;
                RaisePropertyChanged(() => RightDoor1DisableType);
            }
            get { return m_RightDoor1DisableType; }
        }
        public DoorDisableType RightDoor2DisableType
        {
            set
            {
                if (value == m_RightDoor2DisableType)
                {
                    return;
                }

                m_RightDoor2DisableType = value;
                RaisePropertyChanged(() => RightDoor2DisableType);
            }
            get { return m_RightDoor2DisableType; }
        }
        public DoorDisableType RightDoor3DisableType
        {
            set
            {
                if (value == m_RightDoor3DisableType)
                {
                    return;
                }

                m_RightDoor3DisableType = value;
                RaisePropertyChanged(() => RightDoor3DisableType);
            }
            get { return m_RightDoor3DisableType; }
        }
        public DoorDisableType RightDoor4DisableType
        {
            set
            {
                if (value == m_RightDoor4DisableType)
                {
                    return;
                }

                m_RightDoor4DisableType = value;
                RaisePropertyChanged(() => RightDoor4DisableType);
            }
            get { return m_RightDoor4DisableType; }
        }
        public DoorDisableType RightDoor5DisableType
        {
            set
            {
                if (value == m_RightDoor5DisableType)
                {
                    return;
                }

                m_RightDoor5DisableType = value;
                RaisePropertyChanged(() => RightDoor5DisableType);
            }
            get { return m_RightDoor5DisableType; }
        }
        public DoorDisableType RightDoor6DisableType
        {
            set
            {
                if (value == m_RightDoor6DisableType)
                {
                    return;
                }

                m_RightDoor6DisableType = value;
                RaisePropertyChanged(() => RightDoor6DisableType);
            }
            get { return m_RightDoor6DisableType; }
        }
        public DoorDisableType RightDoor7DisableType
        {
            set
            {
                if (value == m_RightDoor7DisableType)
                {
                    return;
                }

                m_RightDoor7DisableType = value;
                RaisePropertyChanged(() => RightDoor7DisableType);
            }
            get { return m_RightDoor7DisableType; }
        }
        public DoorDisableType RightDoor8DisableType
        {
            set
            {
                if (value == m_RightDoor8DisableType)
                {
                    return;
                }

                m_RightDoor8DisableType = value;
                RaisePropertyChanged(() => RightDoor8DisableType);
            }
            get { return m_RightDoor8DisableType; }
        }
        public DoorDisableType LeftDoor1DisableType
        {
            set
            {
                if (value == m_LeftDoor1DisableType)
                {
                    return;
                }

                m_LeftDoor1DisableType = value;
                RaisePropertyChanged(() => LeftDoor1DisableType);
            }
            get { return m_LeftDoor1DisableType; }
        }
        public DoorDisableType LeftDoor2DisableType
        {
            set
            {
                if (value == m_LeftDoor2DisableType)
                {
                    return;
                }

                m_LeftDoor2DisableType = value;
                RaisePropertyChanged(() => LeftDoor2DisableType);
            }
            get { return m_LeftDoor2DisableType; }
        }
        public DoorDisableType LeftDoor3DisableType
        {
            set
            {
                if (value == m_LeftDoor3DisableType)
                {
                    return;
                }

                m_LeftDoor3DisableType = value;
                RaisePropertyChanged(() => LeftDoor3DisableType);
            }
            get { return m_LeftDoor3DisableType; }
        }
        public DoorDisableType LeftDoor4DisableType
        {
            set
            {
                if (value == m_LeftDoor4DisableType)
                {
                    return;
                }

                m_LeftDoor4DisableType = value;
                RaisePropertyChanged(() => LeftDoor4DisableType);
            }
            get { return m_LeftDoor4DisableType; }
        }
        public DoorDisableType LeftDoor5DisableType
        {
            set
            {
                if (value == m_LeftDoor5DisableType)
                {
                    return;
                }

                m_LeftDoor5DisableType = value;
                RaisePropertyChanged(() => LeftDoor5DisableType);
            }
            get { return m_LeftDoor5DisableType; }
        }
        public DoorDisableType LeftDoor6DisableType
        {
            set
            {
                if (value == m_LeftDoor6DisableType)
                {
                    return;
                }

                m_LeftDoor6DisableType = value;
                RaisePropertyChanged(() => LeftDoor6DisableType);
            }
            get { return m_LeftDoor6DisableType; }
        }
        public DoorDisableType LeftDoor7DisableType
        {
            set
            {
                if (value == m_LeftDoor7DisableType)
                {
                    return;
                }

                m_LeftDoor7DisableType = value;
                RaisePropertyChanged(() => LeftDoor7DisableType);
            }
            get { return m_LeftDoor7DisableType; }
        }
        public DoorDisableType LeftDoor8DisableType
        {
            set
            {
                if (value == m_LeftDoor8DisableType)
                {
                    return;
                }

                m_LeftDoor8DisableType = value;
                RaisePropertyChanged(() => LeftDoor8DisableType);
            }
            get { return m_LeftDoor8DisableType; }
        }

        public DoorOpenType Door1OpenType
        {
            set
            {
                if (value == m_Door1OpenType)
                {
                    return;
                }

                m_Door1OpenType = value;
                RaisePropertyChanged(() => Door1OpenType);
            }
            get { return m_Door1OpenType; }
        }
        public DoorOpenType Door2OpenType
        {
            set
            {
                if (value == m_Door2OpenType)
                {
                    return;
                }

                m_Door2OpenType = value;
                RaisePropertyChanged(() => Door2OpenType);
            }
            get { return m_Door2OpenType; }
        }
        public DoorOpenType Door3OpenType
        {
            set
            {
                if (value == m_Door3OpenType)
                {
                    return;
                }

                m_Door3OpenType = value;
                RaisePropertyChanged(() => Door3OpenType);
            }
            get { return m_Door3OpenType; }
        }
        public DoorOpenType Door4OpenType
        {
            set
            {
                if (value == m_Door4OpenType)
                {
                    return;
                }

                m_Door4OpenType = value;
                RaisePropertyChanged(() => Door4OpenType);
            }
            get { return m_Door4OpenType; }
        }
        public DoorOpenType Door6OpenType
        {
            set
            {
                if (value == m_Door6OpenType)
                {
                    return;
                }

                m_Door6OpenType = value;
                RaisePropertyChanged(() => Door6OpenType);
            }
            get { return m_Door6OpenType; }
        }
        public DoorOpenType Door7OpenType
        {
            set
            {
                if (value == m_Door7OpenType)
                {
                    return;
                }

                m_Door7OpenType = value;
                RaisePropertyChanged(() => Door7OpenType);
            }
            get { return m_Door7OpenType; }
        }

        public DoorOpenType Door8OpenType
        {
            set
            {
                if (value == m_Door8OpenType)
                {
                    return;
                }

                m_Door8OpenType = value;
                RaisePropertyChanged(() => Door8OpenType);
            }
            get { return m_Door8OpenType; }
        }
       public DoorState Car1RightDoor2State
        {
            set
            {
                if (value == m_Car1RightDoor2State)
                {
                    return;
                }

                m_Car1RightDoor2State = value;
                RaisePropertyChanged(() => Car1RightDoor2State);
            }
            get { return m_Car1RightDoor2State; }
        }

        public DoorState Car1LeftDoor2State
        {
            set
            {
                if (value == m_Car1LeftDoor2State)
                {
                    return;
                }

                m_Car1LeftDoor2State = value;
                RaisePropertyChanged(() => Car1LeftDoor2State);
            }
            get { return m_Car1LeftDoor2State; }
        }

        public DoorState Car2RightDoor1State
        {
            set
            {
                if (value == m_Car2RightDoor1State)
                {
                    return;
                }

                m_Car2RightDoor1State = value;
                RaisePropertyChanged(() => Car2RightDoor1State);
            }
            get { return m_Car2RightDoor1State; }
        }

        public DoorState Car2RightDoor2State
        {
            set
            {
                if (value == m_Car2RightDoor2State)
                {
                    return;
                }

                m_Car2RightDoor2State = value;
                RaisePropertyChanged(() => Car2RightDoor2State);
            }
            get { return m_Car2RightDoor2State; }
        }

        public DoorState Car2LeftDoor1State
        {
            set
            {
                if (value == m_Car2LeftDoor1State)
                {
                    return;
                }

                m_Car2LeftDoor1State = value;
                RaisePropertyChanged(() => Car2LeftDoor1State);
            }
            get { return m_Car2LeftDoor1State; }
        }

        public DoorState Car2LeftDoor2State
        {
            set
            {
                if (value == m_Car2LeftDoor2State)
                {
                    return;
                }

                m_Car2LeftDoor2State = value;
                RaisePropertyChanged(() => Car2LeftDoor2State);
            }
            get { return m_Car2LeftDoor2State; }
        }

        public DoorState Car3RightDoor1State
        {
            set
            {
                if (value == m_Car3RightDoor1State)
                {
                    return;
                }

                m_Car3RightDoor1State = value;
                RaisePropertyChanged(() => Car3RightDoor1State);
            }
            get { return m_Car3RightDoor1State; }
        }

        public DoorState Car3RightDoor2State
        {
            set
            {
                if (value == m_Car3RightDoor2State)
                {
                    return;
                }

                m_Car3RightDoor2State = value;
                RaisePropertyChanged(() => Car3RightDoor2State);
            }
            get { return m_Car3RightDoor2State; }
        }

        public DoorState Car3LeftDoor1State
        {
            set
            {
                if (value == m_Car3LeftDoor1State)
                {
                    return;
                }

                m_Car3LeftDoor1State = value;
                RaisePropertyChanged(() => Car3LeftDoor1State);
            }
            get { return m_Car3LeftDoor1State; }
        }

        public DoorState Car3LeftDoor2State
        {
            set
            {
                if (value == m_Car3LeftDoor2State)
                {
                    return;
                }

                m_Car3LeftDoor2State = value;
                RaisePropertyChanged(() => Car3LeftDoor2State);
            }
            get { return m_Car3LeftDoor2State; }
        }

        public DoorState Car4RightDoor1State
        {
            set
            {
                if (value == m_Car4RightDoor1State)
                {
                    return;
                }

                m_Car4RightDoor1State = value;
                RaisePropertyChanged(() => Car4RightDoor1State);
            }
            get { return m_Car4RightDoor1State; }
        }

        public DoorState Car4LeftDoor1State
        {
            set
            {
                if (value == m_Car4LeftDoor1State)
                {
                    return;
                }

                m_Car4LeftDoor1State = value;
                RaisePropertyChanged(() => Car4LeftDoor1State);
            }
            get { return m_Car4LeftDoor1State; }
        }

        public DoorState Car6RightDoor1State
        {
            set
            {
                if (value == m_Car6RightDoor1State)
                {
                    return;
                }

                m_Car6RightDoor1State = value;
                RaisePropertyChanged(() => Car6RightDoor1State);
            }
            get { return m_Car6RightDoor1State; }
        }

        public DoorState Car6RightDoor2State
        {
            set
            {
                if (value == m_Car6RightDoor2State)
                {
                    return;
                }

                m_Car6RightDoor2State = value;
                RaisePropertyChanged(() => Car6RightDoor2State);
            }
            get { return m_Car6RightDoor2State; }
        }

        public DoorState Car6LeftDoor1State
        {
            set
            {
                if (value == m_Car6LeftDoor1State)
                {
                    return;
                }

                m_Car6LeftDoor1State = value;
                RaisePropertyChanged(() => Car6LeftDoor1State);
            }
            get { return m_Car6LeftDoor1State; }
        }

        public DoorState Car6LeftDoor2State
        {
            set
            {
                if (value == m_Car6LeftDoor2State)
                {
                    return;
                }

                m_Car6LeftDoor2State = value;
                RaisePropertyChanged(() => Car6LeftDoor2State);
            }
            get { return m_Car6LeftDoor2State; }
        }
        public DoorState Car7RightDoor1State
        {
            set
            {
                if (value == m_Car7RightDoor1State)
                {
                    return;
                }

                m_Car7RightDoor1State = value;
                RaisePropertyChanged(() => Car7RightDoor1State);
            }
            get { return m_Car7RightDoor1State; }
        }

        public DoorState Car7RightDoor2State
        {
            set
            {
                if (value == m_Car7RightDoor2State)
                {
                    return;
                }

                m_Car7RightDoor2State = value;
                RaisePropertyChanged(() => Car7RightDoor2State);
            }
            get { return m_Car7RightDoor2State; }
        }

        public DoorState Car7LeftDoor1State
        {
            set
            {
                if (value == m_Car7LeftDoor1State)
                {
                    return;
                }

                m_Car7LeftDoor1State = value;
                RaisePropertyChanged(() => Car7LeftDoor1State);
            }
            get { return m_Car7LeftDoor1State; }
        }

        public DoorState Car7LeftDoor2State
        {
            set
            {
                if (value == m_Car7LeftDoor2State)
                {
                    return;
                }

                m_Car7LeftDoor2State = value;
                RaisePropertyChanged(() => Car7LeftDoor2State);
            }
            get { return m_Car7LeftDoor2State; }
        }
        public DoorState Car8RightDoor1State
        {
            set
            {
                if (value == m_Car8RightDoor1State)
                {
                    return;
                }

                m_Car8RightDoor1State = value;
                RaisePropertyChanged(() => Car8RightDoor1State);
            }
            get { return m_Car8RightDoor1State; }
        }
        public DoorState Car8LeftDoor1State
        {
            set
            {
                if (value == m_Car8LeftDoor1State)
                {
                    return;
                }

                m_Car8LeftDoor1State = value;
                RaisePropertyChanged(() => Car8LeftDoor1State);
            }
            get { return m_Car8LeftDoor1State; }
        }
    }
}
