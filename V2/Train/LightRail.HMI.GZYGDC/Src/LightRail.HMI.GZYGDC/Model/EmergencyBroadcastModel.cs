using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using CommonUtil.Util;
using LightRail.HMI.GZYGDC.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class EmergencyBroadcastModel : NotificationObject
    {

        private ObservableCollection<EmergencyBroadcastItem> m_EmergencyBroadcastItems;

        private IEnumerable<EmergencyBroadcastItem> m_EmergencyBroadcastDisplayItems;

        

        public EmergencyBroadcastModel()
        {
            EmergencyBroadcastItems = new ObservableCollection<EmergencyBroadcastItem>(GlobalParam.Instance.EmergencyBroadcastItems);

            
        }


        /// <summary>
        /// 所有紧急广播表
        /// </summary>
        public ObservableCollection<EmergencyBroadcastItem> EmergencyBroadcastItems
        {
            get { return m_EmergencyBroadcastItems; }
            set
            {
                if (Equals(value, m_EmergencyBroadcastItems))
                {
                    return;
                }
                m_EmergencyBroadcastItems = value;
                RaisePropertyChanged(() => EmergencyBroadcastItems);
            }
        }


        /// <summary>
        /// 需要显示的紧急广播表
        /// </summary>
        public IEnumerable<EmergencyBroadcastItem> EmergencyBroadcastDisplayItems
        {
            get { return m_EmergencyBroadcastDisplayItems; }
            set
            {
                if (Equals(value, m_EmergencyBroadcastDisplayItems))
                {
                    return;
                }
                m_EmergencyBroadcastDisplayItems = value;
                RaisePropertyChanged(() => EmergencyBroadcastDisplayItems);
            }
        }

        
    }
}