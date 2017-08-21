using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public interface ICarItem
    {
        bool Visible { get; }

        object State { get; }
    }

    [DebuggerDisplay("Index={CarIndex}, State={State}")]
    public class CarItem<TState, TConfig> : NotificationObject
    {
        private TState m_State;
        private bool m_Visible;

        [DebuggerStepThrough]
        public CarItem(int carIndex, TConfig itemConfig, int itemIndex = 0)
        {
            CarIndex = carIndex;
            ItemConfig = itemConfig;
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
