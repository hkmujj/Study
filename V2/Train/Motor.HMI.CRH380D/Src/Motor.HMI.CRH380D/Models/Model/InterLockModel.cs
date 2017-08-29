using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class InterLockModel : ModelBase
    {
        public bool m_Btn1State;
        public bool m_Btn2State;
        public bool m_Btn3State;
        public bool m_Btn4State;

        [ImportingConstructor]
        public InterLockModel()
        {

        }
        
        /// <summary>
        /// 可旁通状态
        /// </summary>
        public bool Btn1State
        {
            get { return m_Btn1State; }
            set
            {
                if (value == m_Btn1State)
                {
                     return;
                }
                m_Btn1State = value;
                RaisePropertyChanged(() => Btn1State);
            }
        }

        /// <summary>
        /// 阻断状态
        /// </summary>
        public bool Btn2State
        {
            get { return m_Btn2State; }
            set
            {
                if (value == m_Btn2State)
                {
                    return;
                }
                m_Btn2State = value;
                RaisePropertyChanged(() => Btn2State);
            }
        }

        /// <summary>
        /// 紧急制动状态
        /// </summary>
        public bool Btn3State
        {
            get { return m_Btn3State; }
            set
            {
                if (value == m_Btn3State)
                {
                    return;
                }
                m_Btn3State = value;
                RaisePropertyChanged(() => Btn3State);
            }
        }

        /// <summary>
        /// 限速状态
        /// </summary>
        public bool Btn4State
        {
            get { return m_Btn4State; }
            set
            {
                if (value == m_Btn4State)
                {
                    return;
                }
                m_Btn4State = value;
                RaisePropertyChanged(() => Btn4State);
            }
        }
    }
}