using System.Diagnostics;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    [DebuggerDisplay("Index={CarIndex}, State={State}")]
    public class CarItem<TState, TConfig> : NotificationObject where TConfig : ISetValueProvider
    {
        private TState m_State;
        private bool m_Visible;

        [DebuggerStepThrough]
        public CarItem(TConfig itemConfig, int carIndex, int itemIndex = 0)
        {
            ItemConfig = itemConfig;
            CarIndex = carIndex;
            ItemIndex = itemIndex;
        }

        public int CarIndex { get; private set; }

        public int ItemIndex { get; private set; }

        public TConfig ItemConfig { get; private set; }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        public TState State
        {
            get { return m_State; }
            set
            {
                if (Equals(value, m_State))
                {
                    return;
                }

                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }
    }
}