using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class InterLockModel : ModelBase
    {
        public ButtonColorState m_Btn1;
        public ButtonColorState m_Btn2;
        public ButtonColorState m_Btn3;
        public ButtonColorState m_Btn4;

        [ImportingConstructor]
        public InterLockModel()
        {

        }
        
        /// <summary>
        /// 可旁通状态
        /// </summary>
        public ButtonColorState Btn1
        {
            get { return m_Btn1; }
            set
            {
                if (value == m_Btn1)
                {
                     return;
                }
                m_Btn1 = value;
                RaisePropertyChanged(() => Btn1);
            }
        }

        /// <summary>
        /// 阻断状态
        /// </summary>
        public ButtonColorState Btn2
        {
            get { return m_Btn2; }
            set
            {
                if (value == m_Btn2)
                {
                    return;
                }
                m_Btn2 = value;
                RaisePropertyChanged(() => Btn2);
            }
        }

        /// <summary>
        /// 紧急制动状态
        /// </summary>
        public ButtonColorState Btn3
        {
            get { return m_Btn3; }
            set
            {
                if (value == m_Btn3)
                {
                    return;
                }
                m_Btn3 = value;
                RaisePropertyChanged(() => Btn3);
            }
        }

        /// <summary>
        /// 限速状态
        /// </summary>
        public ButtonColorState Btn4
        {
            get { return m_Btn4; }
            set
            {
                if (value == m_Btn4)
                {
                    return;
                }
                m_Btn4 = value;
                RaisePropertyChanged(() => Btn4);
            }
        }
    }
}