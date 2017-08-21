using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Road;
using Tram.CBTC.Infrasturcture.ViewModel.Monitor;
using Tram.CBTC.NRIET.Controller;
using Tram.CBTC.NRIET.Model;
using Tram.CBTC.NRIET.Model.Domain;

namespace Tram.CBTC.NRIET.ViewModel
{
    [Export]
    public class DomainViewModel : NotificationObject, ICBTCProvider
    {
        private StartViewModel m_StartViewModel;

        private KeyboardViewModel m_KeyboardViewModel;

        private SettingViewModel m_SettingViewModel;

        [ImportingConstructor]
        public DomainViewModel(DomainController controller, DomainModel model)
        {
            Controller = controller;
            Model = model;
            Domain = new Domain(GlobalParam.Instance.InitParam);

            m_StartViewModel = new StartViewModel(this);
            m_KeyboardViewModel = new KeyboardViewModel(this);
            m_SettingViewModel = new SettingViewModel(this);



        }
        

        public StartViewModel StartViewModel
        {
            get { return m_StartViewModel; }
            set
            {
                if (value.Equals(m_StartViewModel))
                {
                    return;
                }

                m_StartViewModel = value;

                RaisePropertyChanged(() => StartViewModel);
            }
        }


        public KeyboardViewModel KeyboardViewModel
        {
            get { return m_KeyboardViewModel; }
            set
            {
                if (value.Equals(m_KeyboardViewModel))
                {
                    return;
                }

                m_KeyboardViewModel = value;

                RaisePropertyChanged(() => KeyboardViewModel);
            }
        }


        public SettingViewModel SettingViewModel
        {
            get { return m_SettingViewModel; }
            set
            {
                if (value.Equals(m_SettingViewModel))
                {
                    return;
                }

                m_SettingViewModel = value;

                RaisePropertyChanged(() => SettingViewModel);
            }
        }


        public Domain Domain { get; set; }

        public DomainController Controller { private set; get; }

        public DomainModel Model { private set; get; }

        public global::Tram.CBTC.Infrasturcture.Model.CBTC CBTC { get { return Domain; } }
    }
}