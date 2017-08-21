using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.TrainSetting.Detail
{
    public class AutoStartItem : NotificationObject
    {
        private bool m_Occurse;

        public AutoStartItem(AutoStartEngineItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
        }

        public AutoStartEngineItemConfig ItemConfig { get; private set; }

        public bool Occurse
        {
            get { return m_Occurse; }
            set
            {
                if (value == m_Occurse)
                {
                    return;
                }

                m_Occurse = value;
                RaisePropertyChanged(() => Occurse);
            }
        }
    }
}