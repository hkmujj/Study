using System.Diagnostics;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.SoftSwitch.Detail
{
    public class SoftSwitchItem : NotificationObject
    {
        private SoftSwitchDirection m_Direction;

        [DebuggerStepThrough]
        public SoftSwitchItem(SoftSwitchItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
        }

        public SoftSwitchItemConfig ItemConfig { private set; get; }

        public SoftSwitchDirection Direction
        {
            set
            {
                if (value == m_Direction)
                {
                    return;
                }

                m_Direction = value;
                RaisePropertyChanged(() => Direction);
            }
            get { return m_Direction; }
        }
    }
}