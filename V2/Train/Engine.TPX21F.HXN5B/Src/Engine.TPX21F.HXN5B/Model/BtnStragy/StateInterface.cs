using System;
using System.Diagnostics;
using CommonUtil.Util;
using Engine.TPX21F.HXN5B.Constant;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TPX21F.HXN5B.Model.BtnStragy
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
        private BtnItem m_BtnQuery;
        private BtnItem m_BtnLeft;
        private BtnItem m_BtnRight;
        private BtnItem m_BtnUp;
        private BtnItem m_BtnDown;
        private BtnItem m_BtnSaveAs;
        private BtnItem m_BtnSetting;
        private BtnItem m_BtnOK;
        private BtnItem m_BtnBTemperature;
        private BtnItem m_BtnBLightDown;
        private BtnItem m_BtnBLightUp;
        private BtnItem m_BtnBContrast;
        private BtnItem m_BtnBSoundDown;
        private BtnItem m_BtnBSoundUp;
        private BtnItem m_BtnBContext;
        private BtnItem m_BtnBExclamation;
        private BtnItem m_BtnBQuestionMark;
        private BtnItem m_BtnBReturn;
        private BtnItem m_BtnBOK;

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

        public BtnItem BtnOK
        {
            set
            {
                if (Equals(value, m_BtnOK))
                {
                    return;
                }

                m_BtnOK = value;
                RaisePropertyChanged(() => BtnOK);
            }
            get { return m_BtnOK; }
        }

        public BtnItem BtnSetting
        {
            set
            {
                if (Equals(value, m_BtnSetting))
                {
                    return;
                }

                m_BtnSetting = value;
                RaisePropertyChanged(() => BtnSetting);
            }
            get { return m_BtnSetting; }
        }

        public BtnItem BtnSaveAs
        {
            set
            {
                if (Equals(value, m_BtnSaveAs))
                {
                    return;
                }

                m_BtnSaveAs = value;
                RaisePropertyChanged(() => BtnSaveAs);
            }
            get { return m_BtnSaveAs; }
        }

        public BtnItem BtnDown
        {
            set
            {
                if (Equals(value, m_BtnDown))
                {
                    return;
                }

                m_BtnDown = value;
                RaisePropertyChanged(() => BtnDown);
            }
            get { return m_BtnDown; }
        }

        public BtnItem BtnUp
        {
            set
            {
                if (Equals(value, m_BtnUp))
                {
                    return;
                }

                m_BtnUp = value;
                RaisePropertyChanged(() => BtnUp);
            }
            get { return m_BtnUp; }
        }

        public BtnItem BtnRight
        {
            set
            {
                if (Equals(value, m_BtnRight))
                {
                    return;
                }

                m_BtnRight = value;
                RaisePropertyChanged(() => BtnRight);
            }
            get { return m_BtnRight; }
        }

        public BtnItem BtnLeft
        {
            set
            {
                if (Equals(value, m_BtnLeft))
                {
                    return;
                }

                m_BtnLeft = value;
                RaisePropertyChanged(() => BtnLeft);
            }
            get { return m_BtnLeft; }
        }

        public BtnItem BtnQuery
        {
            set
            {
                if (Equals(value, m_BtnQuery))
                {
                    return;
                }

                m_BtnQuery = value;
                RaisePropertyChanged(() => BtnQuery);
            }
            get { return m_BtnQuery; }
        }

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

        public BtnItem BtnBOK
        {
            set
            {
                if (Equals(value, m_BtnBOK))
                {
                    return;
                }

                m_BtnBOK = value;
                RaisePropertyChanged(() => BtnBOK);
            }
            get { return m_BtnBOK; }
        }

        public BtnItem BtnBReturn
        {
            set
            {
                if (Equals(value, m_BtnBReturn))
                {
                    return;
                }

                m_BtnBReturn = value;
                RaisePropertyChanged(() => BtnBReturn);
            }
            get { return m_BtnBReturn; }
        }

        public BtnItem BtnBQuestionMark
        {
            set
            {
                if (Equals(value, m_BtnBQuestionMark))
                {
                    return;
                }

                m_BtnBQuestionMark = value;
                RaisePropertyChanged(() => BtnBQuestionMark);
            }
            get { return m_BtnBQuestionMark; }
        }

        public BtnItem BtnBExclamation
        {
            set
            {
                if (Equals(value, m_BtnBExclamation))
                {
                    return;
                }

                m_BtnBExclamation = value;
                RaisePropertyChanged(() => BtnBExclamation);
            }
            get { return m_BtnBExclamation; }
        }

        public BtnItem BtnBContext
        {
            set
            {
                if (Equals(value, m_BtnBContext))
                {
                    return;
                }

                m_BtnBContext = value;
                RaisePropertyChanged(() => BtnBContext);
            }
            get { return m_BtnBContext; }
        }

        public BtnItem BtnBSoundUp
        {
            set
            {
                if (Equals(value, m_BtnBSoundUp))
                {
                    return;
                }

                m_BtnBSoundUp = value;
                RaisePropertyChanged(() => BtnBSoundUp);
            }
            get { return m_BtnBSoundUp; }
        }

        public BtnItem BtnBSoundDown
        {
            set
            {
                if (Equals(value, m_BtnBSoundDown))
                {
                    return;
                }

                m_BtnBSoundDown = value;
                RaisePropertyChanged(() => BtnBSoundDown);
            }
            get { return m_BtnBSoundDown; }
        }

        public BtnItem BtnBContrast
        {
            set
            {
                if (Equals(value, m_BtnBContrast))
                {
                    return;
                }

                m_BtnBContrast = value;
                RaisePropertyChanged(() => BtnBContrast);
            }
            get { return m_BtnBContrast; }
        }

        public BtnItem BtnBLightUp
        {
            set
            {
                if (Equals(value, m_BtnBLightUp))
                {
                    return;
                }

                m_BtnBLightUp = value;
                RaisePropertyChanged(() => BtnBLightUp);
            }
            get { return m_BtnBLightUp; }
        }

        public BtnItem BtnBLightDown
        {
            set
            {
                if (Equals(value, m_BtnBLightDown))
                {
                    return;
                }

                m_BtnBLightDown = value;
                RaisePropertyChanged(() => BtnBLightDown);
            }
            get { return m_BtnBLightDown; }
        }

        public BtnItem BtnBTemperature
        {
            set
            {
                if (Equals(value, m_BtnBTemperature))
                {
                    return;
                }

                m_BtnBTemperature = value;
                RaisePropertyChanged(() => BtnBTemperature);
            }
            get { return m_BtnBTemperature; }
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

        public void UpdateState()
        {
            try
            {
                m_RegionManager.RequestNavigate(RegionNames.ContentContent, ContentViewName);
            }
            catch (Exception e)
            {
                AppLog.Error("Can not navigate to content view , where regionname ={0}, view name = {1}, {2}",
                    RegionNames.ContentContent, ContentViewName, e);
            }
            BtnOK.UpdateState();
            BtnDown.UpdateState();
            BtnUp.UpdateState();
            BtnRight.UpdateState();
            BtnLeft.UpdateState();
            //BtnQuery.UpdateState();
            BtnB10.UpdateState();
            BtnB9.UpdateState();
            BtnB8.UpdateState();
            BtnB7.UpdateState();
            BtnB6.UpdateState();
            BtnB5.UpdateState();
            BtnB4.UpdateState();
            BtnB3.UpdateState();
            BtnB2.UpdateState();
            BtnB1.UpdateState();
            BtnBOK.UpdateState();
            BtnBReturn.UpdateState();
            BtnBQuestionMark.UpdateState();
            BtnBExclamation.UpdateState();
            BtnBContext.UpdateState();
            BtnBSoundUp.UpdateState();
            BtnBSoundDown.UpdateState();
            BtnBContrast.UpdateState();
            BtnBLightUp.UpdateState();
            BtnBLightDown.UpdateState();
            BtnBTemperature.UpdateState();
        }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => Title);
            RaisePropertyChanged(() => BtnOK);
            RaisePropertyChanged(() => BtnSetting);
            RaisePropertyChanged(() => BtnSaveAs);
            RaisePropertyChanged(() => BtnDown);
            RaisePropertyChanged(() => BtnUp);
            RaisePropertyChanged(() => BtnRight);
            RaisePropertyChanged(() => BtnLeft);
            RaisePropertyChanged(() => BtnQuery);
            RaisePropertyChanged(() => BtnB10);
            RaisePropertyChanged(() => BtnB9);
            RaisePropertyChanged(() => BtnB8);
            RaisePropertyChanged(() => BtnB7);
            RaisePropertyChanged(() => BtnB6);
            RaisePropertyChanged(() => BtnB5);
            RaisePropertyChanged(() => BtnB4);
            RaisePropertyChanged(() => BtnB3);
            RaisePropertyChanged(() => BtnB2);
            RaisePropertyChanged(() => BtnB1);
            RaisePropertyChanged(() => BtnBOK);
            RaisePropertyChanged(() => BtnBReturn);
            RaisePropertyChanged(() => BtnBQuestionMark);
            RaisePropertyChanged(() => BtnBExclamation);
            RaisePropertyChanged(() => BtnBContext);
            RaisePropertyChanged(() => BtnBSoundUp);
            RaisePropertyChanged(() => BtnBSoundDown);
            RaisePropertyChanged(() => BtnBContrast);
            RaisePropertyChanged(() => BtnBLightUp);
            RaisePropertyChanged(() => BtnBLightDown);
            RaisePropertyChanged(() => BtnBTemperature);
        }
    }
}