using System;
using System.Diagnostics;
using CommonUtil.Util;
using Engine.TAX2.SS7C.Constant;
using Engine.TAX2.SS7C.Model.ConfigModel;
using Engine.TAX2.SS7C.View.Layout;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TAX2.SS7C.Model.BtnStragy
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

        private readonly IRegionManager m_RegionManager;
        private BtnItem m_BtnB9;
        private BtnItem m_BtnB10;
        private BtnItem m_BtnVigilant;
        private BtnItem m_BtnRelase;
        private BtnItem m_BtnUnlock;
        private BtnItem m_BtnUp;
        private BtnItem m_BtnDown;
        private BtnItem m_BtnRight;
        private BtnItem m_BtnLeft;
        private BtnItem m_BtnSetting;
        private BtnItem m_BtnOk;
        private BtnItem m_BtnSaveAs;
        private BtnItem m_BtnQuery;

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

        public ContentViewLocation ContentViewLocation { set; get; }

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

        public BtnItem BtnQuery
        {
            get { return m_BtnQuery; }
            set
            {
                if (Equals(value, m_BtnQuery))
                {
                    return;
                }

                m_BtnQuery = value;
                RaisePropertyChanged(() => BtnQuery);
            }
        }

        public BtnItem BtnSaveAs
        {
            get { return m_BtnSaveAs; }
            set
            {
                if (Equals(value, m_BtnSaveAs))
                {
                    return;
                }

                m_BtnSaveAs = value;
                RaisePropertyChanged(() => BtnSaveAs);
            }
        }

        public BtnItem BtnOk
        {
            get { return m_BtnOk; }
            set
            {
                if (Equals(value, m_BtnOk))
                {
                    return;
                }

                m_BtnOk = value;
                RaisePropertyChanged(() => BtnOk);
            }
        }

        public BtnItem BtnSetting
        {
            get { return m_BtnSetting; }
            set
            {
                if (Equals(value, m_BtnSetting))
                {
                    return;
                }

                m_BtnSetting = value;
                RaisePropertyChanged(() => BtnSetting);
            }
        }

        public BtnItem BtnLeft
        {
            get { return m_BtnLeft; }
            set
            {
                if (Equals(value, m_BtnLeft))
                {
                    return;
                }

                m_BtnLeft = value;
                RaisePropertyChanged(() => BtnLeft);
            }
        }

        public BtnItem BtnRight
        {
            get { return m_BtnRight; }
            set
            {
                if (Equals(value, m_BtnRight))
                {
                    return;
                }

                m_BtnRight = value;
                RaisePropertyChanged(() => BtnRight);
            }
        }

        public BtnItem BtnDown
        {
            get { return m_BtnDown; }
            set
            {
                if (Equals(value, m_BtnDown))
                {
                    return;
                }

                m_BtnDown = value;
                RaisePropertyChanged(() => BtnDown);
            }
        }

        public BtnItem BtnUp
        {
            get { return m_BtnUp; }
            set
            {
                if (Equals(value, m_BtnUp))
                {
                    return;
                }

                m_BtnUp = value;
                RaisePropertyChanged(() => BtnUp);
            }
        }

        public BtnItem BtnUnlock
        {
            get { return m_BtnUnlock; }
            set
            {
                if (Equals(value, m_BtnUnlock))
                {
                    return;
                }

                m_BtnUnlock = value;
                RaisePropertyChanged(() => BtnUnlock);
            }
        }

        public BtnItem BtnRelase
        {
            get { return m_BtnRelase; }
            set
            {
                if (Equals(value, m_BtnRelase))
                {
                    return;
                }

                m_BtnRelase = value;
                RaisePropertyChanged(() => BtnRelase);
            }
        }

        public BtnItem BtnVigilant
        {
            get { return m_BtnVigilant; }
            set
            {
                if (Equals(value, m_BtnVigilant))
                {
                    return;
                }

                m_BtnVigilant = value;
                RaisePropertyChanged(() => BtnVigilant);
            }
        }

        public void UpdateState()
        {
            string contentRegionName = ContentViewLocation == ContentViewLocation.Up
                ? RegionNames.ContentUpContent
                : RegionNames.ContentDownContent;

            string contentLayoutView = ContentViewLocation == ContentViewLocation.Up
                ? typeof(ShellContentStyle1Layout).FullName : typeof(ShellContentStyle2Layout).FullName;

            try
            {
                m_RegionManager.RequestNavigate(contentRegionName, ContentViewName);
                m_RegionManager.RequestNavigate(RegionNames.ShellContent, contentLayoutView);
            }
            catch (Exception e)
            {
                AppLog.Error("Can not navigate to content view , where regionname ={0}, view name = {1}, {2}",
                    contentRegionName, ContentViewName, e);
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
            RaisePropertyChanged(() => BtnB7);
            RaisePropertyChanged(() => BtnB8);
        }
    }
}