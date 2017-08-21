using System.ComponentModel.Composition;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 车辆模型
    /// </summary>
    [Export]
    public class CarModel : ModelBase
    {
        private CABState m_CABStateTwo;
        private CABState m_CABStateOne;
        private PantographState m_PantographStateTwo;
        private PantographState m_PantographStateOne;
        private EscapeDoorState m_EscapeDoorStateOne;
        private EscapeDoorState m_EscapeDoorStateTwo;
        private WorkState m_WorkState;
        private Direction m_Direction;

        /// <summary>
        /// F002车受电弓状态
        /// </summary>
        public PantographState PantographStateOne
        {
            get { return m_PantographStateOne; }
            set
            {
                if (value == m_PantographStateOne)
                {
                    return;
                }
                m_PantographStateOne = value;
                RaisePropertyChanged(() => PantographStateOne);
            }
        }

        /// <summary>
        /// F005车受电弓状态
        /// </summary>
        public PantographState PantographStateTwo
        {
            get { return m_PantographStateTwo; }
            set
            {
                if (value == m_PantographStateTwo)
                {
                    return;
                }
                m_PantographStateTwo = value;
                RaisePropertyChanged(() => PantographStateTwo);
            }
        }

        /// <summary>
        /// F001司机室激活
        /// </summary>
        public CABState CABStateOne
        {
            get { return m_CABStateOne; }
            set
            {
                if (value == m_CABStateOne)
                {
                    return;
                }
                m_CABStateOne = value;
                RaisePropertyChanged(() => CABStateOne);
            }
        }

        /// <summary>
        /// F006司机室激活
        /// </summary>
        public CABState CABStateTwo
        {
            get { return m_CABStateTwo; }
            set
            {
                if (value == m_CABStateTwo)
                {
                    return;
                }
                m_CABStateTwo = value;
                RaisePropertyChanged(() => CABStateTwo);
            }
        }

        /// <summary>
        /// 司机室1逃生门状态
        /// </summary>
        public EscapeDoorState EscapeDoorStateOne
        {
            get { return m_EscapeDoorStateOne; }
            set
            {
                if (value == m_EscapeDoorStateOne)
                {
                    return;
                }
                m_EscapeDoorStateOne = value;
                RaisePropertyChanged(() => EscapeDoorStateOne);
            }
        }

        /// <summary>
        /// 司机室2逃生门
        /// </summary>
        public EscapeDoorState EscapeDoorStateTwo
        {
            get { return m_EscapeDoorStateTwo; }
            set
            {
                if (value == m_EscapeDoorStateTwo)
                {
                    return;
                }
                m_EscapeDoorStateTwo = value;
                RaisePropertyChanged(() => EscapeDoorStateTwo);
            }
        }

        /// <summary>
        /// 工况
        /// </summary>
        public WorkState WorkState
        {
            get { return m_WorkState; }
            set
            {
                if (value == m_WorkState)
                {
                    return;
                }
                m_WorkState = value;
                RaisePropertyChanged(() => WorkState);
            }
        }

        /// <summary>
        /// 列车运行方向
        /// </summary>
        public Direction Direction
        {
            get { return m_Direction; }
            set
            {
                if (value == m_Direction)
                {
                    return;
                }
                m_Direction = value;
                RaisePropertyChanged(() => Direction);
            }
        }
    }
}