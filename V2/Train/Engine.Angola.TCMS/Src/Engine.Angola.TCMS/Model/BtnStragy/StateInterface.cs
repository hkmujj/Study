﻿using Engine.Angola.TCMS.Constant;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Diagnostics;
using CommonUtil.Util;
using Engine.Angola.TCMS.View.Shell;

namespace Engine.Angola.TCMS.Model.BtnStragy
{
    [DebuggerDisplay("Id={Id} Title={Title}")]
    public class StateInterface : NotificationObject, IStateInterface, IRaiseResourceChangedProvider
    {
        private BtnItem m_BtnF5;
        private BtnItem m_BtnF4;
        private BtnItem m_BtnF3;
        private BtnItem m_BtnF2;
        private BtnItem m_BtnF1;
        private string m_Title;

        private readonly IRegionManager m_RegionManager;

        public StateInterface()
        {
            m_RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }

        public string Title
        {
            get { return m_Title; }
            set
            {
                if (value == m_Title)
                {
                    return;
                }

                m_Title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string ContentViewName { set; get; }

        public StateInterfaceKey Id { get; set; }


        public BtnItem BtnF1
        {
            get { return m_BtnF1; }
            set
            {
                if (Equals(value, m_BtnF1))
                {
                    return;
                }

                m_BtnF1 = value;
                RaisePropertyChanged(() => BtnF1);
            }
        }

        public BtnItem BtnF2
        {
            get { return m_BtnF2; }
            set
            {
                if (Equals(value, m_BtnF2))
                {
                    return;
                }

                m_BtnF2 = value;
                RaisePropertyChanged(() => BtnF2);
            }
        }

        public BtnItem BtnF3
        {
            get { return m_BtnF3; }
            set
            {
                if (Equals(value, m_BtnF3))
                {
                    return;
                }

                m_BtnF3 = value;
                RaisePropertyChanged(() => BtnF3);
            }
        }

        public BtnItem BtnF4
        {
            get { return m_BtnF4; }
            set
            {
                if (Equals(value, m_BtnF4))
                {
                    return;
                }

                m_BtnF4 = value;
                RaisePropertyChanged(() => BtnF4);
            }
        }

        public BtnItem BtnF5
        {
            get { return m_BtnF5; }
            set
            {
                if (Equals(value, m_BtnF5))
                {
                    return;
                }

                m_BtnF5 = value;
                RaisePropertyChanged(() => BtnF5);
            }
        }

        public void UpdateState()
        {
            string contentRegionName = RegionNames.ShellContent;

            string contentLayoutView = typeof(AngolaTCMSShellLayout).FullName;

            try
            {
                m_RegionManager.RequestNavigate(contentRegionName, ContentViewName);
                m_RegionManager.RequestNavigate(RegionNames.ShellRegion, contentLayoutView);
            }
            catch (Exception e)
            {
                AppLog.Error("Can not navigate to content view , where regionname ={0}, view name = {1}, {2}",
                    contentRegionName, ContentViewName, e);
            }

            BtnF1.UpdateState();
            BtnF2.UpdateState();
            BtnF3.UpdateState();
            BtnF4.UpdateState();
            BtnF5.UpdateState();
        }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => Title);
            RaisePropertyChanged(() => BtnF1);
            RaisePropertyChanged(() => BtnF2);
            RaisePropertyChanged(() => BtnF3);
            RaisePropertyChanged(() => BtnF4);
            RaisePropertyChanged(() => BtnF5);
        }
    }
}
