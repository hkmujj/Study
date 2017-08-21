using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Controller;
using LightRail.HMI.SZLHLF.Interface;
using LightRail.HMI.SZLHLF.Model.ConfigModel;

namespace LightRail.HMI.SZLHLF.Model.EmergencyBroadcastModel
{
    [Export]
    [Export(typeof(IModels))]
    public class EmergencyBroadcastInfoModel : ModelBase
    {

        private ObservableCollection<EmergencyBroadcastItem> m_EmergencyBroadcastItems;

        private ObservableCollection<EmergencyBroadcastItem> m_EmergencyBroadcastDisplayItems;

        [ImportingConstructor]
        public EmergencyBroadcastInfoModel(EmergencyBroadcastController emergencyBroadcastController)
        {
            EmergencyBroadcastItems = new ObservableCollection<EmergencyBroadcastItem>(GlobalParam.Instance.EmergencyBroadcastItems);

            EmergencyBroadcastController = emergencyBroadcastController;
            EmergencyBroadcastController.ViewModel = new Lazy<EmergencyBroadcastInfoModel>(() => this);
            EmergencyBroadcastController.Initalize();
            Instance = this;
        }

        override public void Initialize()
        {
            EmergencyBroadcastController.Initalize();
        }

        public override void Clear()
        {
            EmergencyBroadcastController.Clear();
        }

        public static EmergencyBroadcastInfoModel Instance;

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
        public EmergencyBroadcastController EmergencyBroadcastController { private set; get; }
        
        /// <summary>
        /// 需要显示的紧急广播表
        /// </summary>
        public ObservableCollection<EmergencyBroadcastItem> EmergencyBroadcastDisplayItems
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