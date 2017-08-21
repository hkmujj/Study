using System;
using System.Diagnostics;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Constant;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Model.BtnStragy
{
    [DebuggerDisplay("Id={Id} Title={Title}")]
    public class StateInterface : NotificationObject, IStateInterface, IRaiseResourceChangedProvider
    {
        private BtnItem m_BtnB8;
        private BtnItem m_BtnB7;
        private BtnItem m_BtnB6;
        private BtnItem m_BtnB5;
        private BtnItem m_BtnB4;
        private BtnItem m_BtnB3;
        private BtnItem m_BtnB2;
        private BtnItem m_BtnB1;
        private string m_Title;

        private BtnItem m_BtnB9;
        private BtnItem m_BtnB10;

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
        public string IconViewName { set; get; }

        public StateInterfaceKey Id { get; set; }

        public BtnItem BtnB1
        {
            get { return m_BtnB1; }
            set
            {
                if (Equals(value, m_BtnB1))
                {
                    return;
                }

                m_BtnB1 = value;
                RaisePropertyChanged(() => BtnB1);
            }
        }

        public BtnItem BtnB2
        {
            get { return m_BtnB2; }
            set
            {
                if (Equals(value, m_BtnB2))
                {
                    return;
                }

                m_BtnB2 = value;
                RaisePropertyChanged(() => BtnB2);
            }
        }

        public BtnItem BtnB3
        {
            get { return m_BtnB3; }
            set
            {
                if (Equals(value, m_BtnB3))
                {
                    return;
                }

                m_BtnB3 = value;
                RaisePropertyChanged(() => BtnB3);
            }
        }

        public BtnItem BtnB4
        {
            get { return m_BtnB4; }
            set
            {
                if (Equals(value, m_BtnB4))
                {
                    return;
                }

                m_BtnB4 = value;
                RaisePropertyChanged(() => BtnB4);
            }
        }

        public BtnItem BtnB5
        {
            get { return m_BtnB5; }
            set
            {
                if (Equals(value, m_BtnB5))
                {
                    return;
                }

                m_BtnB5 = value;
                RaisePropertyChanged(() => BtnB5);
            }
        }

        public BtnItem BtnB6
        {
            get { return m_BtnB6; }
            set
            {
                if (Equals(value, m_BtnB6))
                {
                    return;
                }

                m_BtnB6 = value;
                RaisePropertyChanged(() => BtnB6);
            }
        }

        public BtnItem BtnB7
        {
            get { return m_BtnB7; }
            set
            {
                if (Equals(value, m_BtnB7))
                {
                    return;
                }

                m_BtnB7 = value;
                RaisePropertyChanged(() => BtnB7);
            }
        }

        public BtnItem BtnB8
        {
            get { return m_BtnB8; }
            set
            {
                if (Equals(value, m_BtnB8))
                {
                    return;
                }

                m_BtnB8 = value;
                RaisePropertyChanged(() => BtnB8);
            }
        }

        public BtnItem BtnB9
        {
            get { return m_BtnB9; }
            set
            {
                if (Equals(value, m_BtnB9))
                {
                    return;
                }

                m_BtnB9 = value;
                RaisePropertyChanged(() => BtnB9);
            }
        }

        public BtnItem BtnB10
        {
            get { return m_BtnB10; }
            set
            {
                if (Equals(value, m_BtnB10))
                {
                    return;
                }

                m_BtnB10 = value;
                RaisePropertyChanged(() => BtnB10);
            }
        }

        public void UpdateState(DomainViewModel domainViewModel)
        {
            try
            {
                domainViewModel.RegionManager.RequestNavigate(RegionNames.MainContentContent,ContentViewName);
                domainViewModel.RegionManager.RequestNavigate(RegionNames.MainTrainStateIcon, IconViewName);
            }
            catch (Exception e)
            {
                AppLog.Error("Can not navigate to content view , where regionname ={0}, view name = {1}, {2}",
                    RegionNames.MainContentContent, ContentViewName, e);
            }
            BtnB1.UpdateState();
            BtnB2.UpdateState();
            BtnB3.UpdateState();
            BtnB4.UpdateState();
            BtnB5.UpdateState();
            BtnB6.UpdateState();
            BtnB7.UpdateState();
            BtnB8.UpdateState();
            BtnB9.UpdateState();
            BtnB10.UpdateState();
        }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => Title);
            RaisePropertyChanged(() => BtnB1);
            RaisePropertyChanged(() => BtnB2);
            RaisePropertyChanged(() => BtnB3);
            RaisePropertyChanged(() => BtnB4);
            RaisePropertyChanged(() => BtnB5);
            RaisePropertyChanged(() => BtnB6);
            RaisePropertyChanged(() => BtnB9);
            RaisePropertyChanged(() => BtnB10);

        }
    }
}