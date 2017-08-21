using System.Diagnostics;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS.Domain
{
    public class WindMachineItem :NotificationObject
    {
        private bool m_Value;

        [DebuggerStepThrough]
        public WindMachineItem(WindMachineStateConfig config)
        {
            Config = config;
        }

        public WindMachineStateConfig Config { private set; get; }

        public bool Value
        {
            set
            {
                if (value == m_Value)
                { return;}

                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
            get { return m_Value; }
        }
    }
}