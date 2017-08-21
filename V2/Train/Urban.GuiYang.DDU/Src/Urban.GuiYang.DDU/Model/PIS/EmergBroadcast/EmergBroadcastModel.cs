using System;
using System.ComponentModel.Composition;
using DevExpress.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Utils;

namespace Urban.GuiYang.DDU.Model.PIS.EmergBroadcast
{
    [Export]
    public class EmergBroadcastModel : NotificationObject
    {
        private EmergBroadcastConfig m_SelectedEmergBroadcast;
        public Lazy<PageWrapper<EmergBroadcastConfig>> ShowingBroadcastCollection { get; set; }

        public DelegateCommand PageUpCommand { get; set; }

        public DelegateCommand PageDownCommand { get; set; }

        public DelegateCommand<CommandParameter> SigleBroadcastCommand { get; set; }

        public DelegateCommand<CommandParameter> CircleBroadcastCommand { get; set; }

        public DelegateCommand SelectedListBoxCommand { get; set; }

        public EmergBroadcastConfig SelectedEmergBroadcast
        {
            get { return m_SelectedEmergBroadcast; }
            set
            {
                if (Equals(value, m_SelectedEmergBroadcast))
                {
                    return;
                }
                m_SelectedEmergBroadcast = value;
                RaisePropertyChanged(() => SelectedEmergBroadcast);
            }
        }
    }
}