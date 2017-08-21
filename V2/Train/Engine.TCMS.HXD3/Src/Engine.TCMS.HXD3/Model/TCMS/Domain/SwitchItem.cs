using System.Diagnostics;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS.Domain
{
    public class SwitchItem : NotificationObject
    {
        private SwitchItemState m_State;

        [DebuggerStepThrough]
        public SwitchItem(SwitchStateConfig config)
        {
            Config = config;
        }

        public SwitchStateConfig Config { private set; get; }

        public SwitchItemState State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                {
                    return;
                }

                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }
    }
}