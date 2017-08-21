using System.Collections.ObjectModel;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS.Domain
{
    public class Car : NotificationObject
    {
        private ObservableCollection<AxisState> m_AxisStates;
        private bool m_IsDriver;
        private PantographState m_PantographState;
        private bool m_EndPointVisible;

        public bool EndPointVisible
        {
            set
            {
                if (value == m_EndPointVisible)
                {
                    return;
                }

                m_EndPointVisible = value;
                RaisePropertyChanged(() => EndPointVisible);
            }
            get { return m_EndPointVisible; }
        }

        public PantographState PantographState
        {
            set
            {
                if (value == m_PantographState)
                {
                    return;
                }

                m_PantographState = value;
                RaisePropertyChanged(() => PantographState);
            }
            get { return m_PantographState; }
        }

        /// <summary>
        /// 司机室
        /// </summary>
        public bool IsDriver
        {
            set
            {
                if (value == m_IsDriver)
                {
                    return;
                }

                m_IsDriver = value;
                RaisePropertyChanged(() => IsDriver);
            }
            get { return m_IsDriver; }
        }

        public ObservableCollection<AxisState> AxisStates
        {
            set
            {
                if (Equals(value, m_AxisStates))
                {
                    return;
                }

                m_AxisStates = value;
                RaisePropertyChanged(() => AxisStates);
            }
            get { return m_AxisStates; }
        }

        public Car()
        {
            AxisStates = new ObservableCollection<AxisState>()
            {
                AxisState.Yellow,
                AxisState.Blue,
                AxisState.Green,
            };
        }
    }
}