using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class VOBCModel : ModelBase
    {
        private VOBCState m_MC00;
        private VOBCState m_MC01;
      
        /// <summary>
        /// MC00车激活状态
        /// </summary>
        public VOBCState MC00
        {
            get { return m_MC00; }
            set
            {
                if (value == m_MC00)
                {
                    return;
                }
                m_MC00 = value;
                RaisePropertyChanged(() => MC00);
            }
        }

        /// <summary>
        /// MC01车激活状态
        /// </summary>
        public VOBCState MC01
        {
            get { return m_MC01; }
            set
            {
                if (value == m_MC01)
                {
                    return;
                }
                m_MC01 = value;
                RaisePropertyChanged(() => MC01);
            }
        }
    }
}