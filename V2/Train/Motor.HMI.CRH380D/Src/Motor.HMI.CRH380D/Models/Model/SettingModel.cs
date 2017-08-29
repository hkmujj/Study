using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Event;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class SettingModel : ModelBase
    {
        private float m_Light;
        private bool m_HMIBlack;

        [ImportingConstructor]
        public SettingModel(SettingController settingController)
        {
            SettingController = settingController;
            settingController.ViewModel = this;
            settingController.Initalize();
            HMIBlack = true;
        }

        /// <summary>
        /// 控制
        /// </summary>
        public SettingController SettingController { get; set; }

        /// <summary>
        /// 亮度
        /// </summary>
        public float Light
        {
            get { return m_Light; }
            set
            {
                if (value == m_Light)
                {
                    return;
                }
                m_Light = value;
                RaisePropertyChanged(()=>Light);
            }
        }

        /// <summary>
        /// 黑屏
        /// </summary>
        public bool HMIBlack
        {
            get { return m_HMIBlack; }
            set
            {
                if (value == m_HMIBlack)
                {
                    return;
                }
                m_HMIBlack = value;

                ServiceLocator.Current.GetInstance<IEventAggregator>()
                    .GetEvent<PowerStateChangedEvent>()
                    .Publish(new PowerStateChangedEvent.Args(value));

                RaisePropertyChanged(() => HMIBlack);
            }
        }
    }
}