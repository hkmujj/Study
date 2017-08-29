using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Controller;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class WarringSumMenuModel : ModelBase
    {

        public bool m_Btn1Enable;
        public bool m_Btn2Enable;
        public bool m_Btn3Enable;
        public bool m_Btn4Enable;
        public bool m_Btn5Enable;
        public bool m_Btn6Enable;
        public bool m_Btn7Enable;
        public bool m_Btn8Enable;
        public bool m_Btn9Enable;
        public bool m_Btn10Enable;
        public bool m_Btn11Enable;
        public bool m_Btn12Enable;
        public bool m_Btn13Enable;
        public bool m_Btn14Enable;

        [ImportingConstructor]
        public WarringSumMenuModel()
        {

        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn1Enable
        {
            get { return m_Btn1Enable; }
            set
            {
                if (value == m_Btn1Enable)
                {
                     return;
                }
                m_Btn1Enable = value;
                RaisePropertyChanged(() => Btn1Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn2Enable
        {
            get { return m_Btn2Enable; }
            set
            {
                if (value == m_Btn2Enable)
                {
                    return;
                }
                m_Btn2Enable = value;
                RaisePropertyChanged(() => Btn2Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn3Enable
        {
            get { return m_Btn3Enable; }
            set
            {
                if (value == m_Btn3Enable)
                {
                    return;
                }
                m_Btn3Enable = value;
                RaisePropertyChanged(() => Btn3Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn4Enable
        {
            get { return m_Btn4Enable; }
            set
            {
                if (value == m_Btn4Enable)
                {
                    return;
                }
                m_Btn4Enable = value;
                RaisePropertyChanged(() => Btn4Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn5Enable
        {
            get { return m_Btn5Enable; }
            set
            {
                if (value == m_Btn5Enable)
                {
                    return;
                }
                m_Btn5Enable = value;
                RaisePropertyChanged(() => Btn5Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn6Enable
        {
            get { return m_Btn6Enable; }
            set
            {
                if (value == m_Btn6Enable)
                {
                    return;
                }
                m_Btn6Enable = value;
                RaisePropertyChanged(() => Btn6Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn7Enable
        {
            get { return m_Btn7Enable; }
            set
            {
                if (value == m_Btn7Enable)
                {
                    return;
                }
                m_Btn7Enable = value;
                RaisePropertyChanged(() => Btn7Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn8Enable
        {
            get { return m_Btn8Enable; }
            set
            {
                if (value == m_Btn8Enable)
                {
                    return;
                }
                m_Btn8Enable = value;
                RaisePropertyChanged(() => Btn8Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn9Enable
        {
            get { return m_Btn9Enable; }
            set
            {
                if (value == m_Btn9Enable)
                {
                    return;
                }
                m_Btn9Enable = value;
                RaisePropertyChanged(() => Btn9Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn10Enable
        {
            get { return m_Btn10Enable; }
            set
            {
                if (value == m_Btn10Enable)
                {
                    return;
                }
                m_Btn10Enable = value;
                RaisePropertyChanged(() => Btn10Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn11Enable
        {
            get { return m_Btn11Enable; }
            set
            {
                if (value == m_Btn11Enable)
                {
                    return;
                }
                m_Btn11Enable = value;
                RaisePropertyChanged(() => Btn11Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn12Enable
        {
            get { return m_Btn12Enable; }
            set
            {
                if (value == m_Btn12Enable)
                {
                    return;
                }
                m_Btn12Enable = value;
                RaisePropertyChanged(() => Btn12Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn13Enable
        {
            get { return m_Btn13Enable; }
            set
            {
                if (value == m_Btn13Enable)
                {
                    return;
                }
                m_Btn13Enable = value;
                RaisePropertyChanged(() => Btn13Enable);
            }
        }

        /// <summary>
        /// 是否允许选择
        /// </summary>
        public bool Btn14Enable
        {
            get { return m_Btn14Enable; }
            set
            {
                if (value == m_Btn14Enable)
                {
                    return;
                }
                m_Btn14Enable = value;
                RaisePropertyChanged(() => Btn14Enable);
            }
        }


    }
}