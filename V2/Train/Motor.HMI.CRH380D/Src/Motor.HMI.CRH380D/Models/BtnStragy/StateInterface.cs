using System;
using System.Diagnostics;
using System.Linq;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.HMI.CRH380D.Constant;

namespace Motor.HMI.CRH380D.Models.BtnStragy
{
    [DebuggerDisplay("Id={Id} Title={Title}")]
    public class StateInterface : NotificationObject, IStateInterface, IRaiseResourceChangedProvider
    {
        private BtnItem m_BtnB30;
        private BtnItem m_BtnB29;
        private BtnItem m_BtnB28;
        private BtnItem m_BtnB27;
        private BtnItem m_BtnB26;
        private BtnItem m_BtnB25;
        private BtnItem m_BtnB24;
        private BtnItem m_BtnB23;
        private BtnItem m_BtnB22;
        private BtnItem m_BtnB21;
        private BtnItem m_BtnB20;
        private BtnItem m_BtnB19;
        private BtnItem m_BtnB18;
        private BtnItem m_BtnB17;
        private BtnItem m_BtnB16;
        private BtnItem m_BtnB15;
        private BtnItem m_BtnB14;
        private BtnItem m_BtnB13;
        private BtnItem m_BtnB12;
        private BtnItem m_BtnB11;
        private BtnItem m_BtnB10;
        private BtnItem m_BtnB9;
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
        public string ContentViewTitle { set;get; }

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

        public BtnItem BtnB11
        {
            get { return m_BtnB11; }
            set
            {
                if (Equals(value, m_BtnB11))
                {
                    return;
                }

                m_BtnB11 = value;
                RaisePropertyChanged(() => BtnB11);
            }
        }

        public BtnItem BtnB12
        {
            get { return m_BtnB12; }
            set
            {
                if (Equals(value, m_BtnB12))
                {
                    return;
                }

                m_BtnB12 = value;
                RaisePropertyChanged(() => BtnB12);
            }
        }

        public BtnItem BtnB13
        {
            get { return m_BtnB13; }
            set
            {
                if (Equals(value, m_BtnB13))
                {
                    return;
                }

                m_BtnB13 = value;
                RaisePropertyChanged(() => BtnB13);
            }
        }

        public BtnItem BtnB14
        {
            get { return m_BtnB14; }
            set
            {
                if (Equals(value, m_BtnB14))
                {
                    return;
                }

                m_BtnB14 = value;
                RaisePropertyChanged(() => BtnB14);
            }
        }

        public BtnItem BtnB15
        {
            get { return m_BtnB15; }
            set
            {
                if (Equals(value, m_BtnB15))
                {
                    return;
                }

                m_BtnB15 = value;
                RaisePropertyChanged(() => BtnB15);
            }
        }

        public BtnItem BtnB16
        {
            get { return m_BtnB16; }
            set
            {
                if (Equals(value, m_BtnB16))
                {
                    return;
                }

                m_BtnB16 = value;
                RaisePropertyChanged(() => BtnB16);
            }
        }

        public BtnItem BtnB17
        {
            get { return m_BtnB17; }
            set
            {
                if (Equals(value, m_BtnB17))
                {
                    return;
                }

                m_BtnB17 = value;
                RaisePropertyChanged(() => BtnB17);
            }
        }

        public BtnItem BtnB18
        {
            get { return m_BtnB18; }
            set
            {
                if (Equals(value, m_BtnB18))
                {
                    return;
                }

                m_BtnB18 = value;
                RaisePropertyChanged(() => BtnB18);
            }
        }

        public BtnItem BtnB19
        {
            get { return m_BtnB19; }
            set
            {
                if (Equals(value, m_BtnB19))
                {
                    return;
                }

                m_BtnB19 = value;
                RaisePropertyChanged(() => BtnB19);
            }
        }

        public BtnItem BtnB20
        {
            get { return m_BtnB20; }
            set
            {
                if (Equals(value, m_BtnB20))
                {
                    return;
                }

                m_BtnB20 = value;
                RaisePropertyChanged(() => BtnB20);
            }
        }

        public BtnItem BtnB21
        {
            get { return m_BtnB21; }
            set
            {
                if (Equals(value, m_BtnB21))
                {
                    return;
                }

                m_BtnB21 = value;
                RaisePropertyChanged(() => BtnB21);
            }
        }

        public BtnItem BtnB22
        {
            get { return m_BtnB22; }
            set
            {
                if (Equals(value, m_BtnB22))
                {
                    return;
                }

                m_BtnB22 = value;
                RaisePropertyChanged(() => BtnB22);
            }
        }

        public BtnItem BtnB23
        {
            get { return m_BtnB23; }
            set
            {
                if (Equals(value, m_BtnB23))
                {
                    return;
                }

                m_BtnB23 = value;
                RaisePropertyChanged(() => BtnB23);
            }
        }

        public BtnItem BtnB24
        {
            get { return m_BtnB24; }
            set
            {
                if (Equals(value, m_BtnB24))
                {
                    return;
                }

                m_BtnB24 = value;
                RaisePropertyChanged(() => BtnB24);
            }
        }

        public BtnItem BtnB25
        {
            get { return m_BtnB25; }
            set
            {
                if (Equals(value, m_BtnB25))
                {
                    return;
                }

                m_BtnB25 = value;
                RaisePropertyChanged(() => BtnB25);
            }
        }

        public BtnItem BtnB26
        {
            get { return m_BtnB26; }
            set
            {
                if (Equals(value, m_BtnB26))
                {
                    return;
                }

                m_BtnB26 = value;
                RaisePropertyChanged(() => BtnB26);
            }
        }

        public BtnItem BtnB27
        {
            get { return m_BtnB27; }
            set
            {
                if (Equals(value, m_BtnB27))
                {
                    return;
                }

                m_BtnB27 = value;
                RaisePropertyChanged(() => BtnB27);
            }
        }

        public BtnItem BtnB28
        {
            get { return m_BtnB28; }
            set
            {
                if (Equals(value, m_BtnB28))
                {
                    return;
                }

                m_BtnB28 = value;
                RaisePropertyChanged(() => BtnB28);
            }
        }

        public BtnItem BtnB29
        {
            get { return m_BtnB29; }
            set
            {
                if (Equals(value, m_BtnB29))
                {
                    return;
                }

                m_BtnB29 = value;
                RaisePropertyChanged(() => BtnB29);
            }
        }

        public BtnItem BtnB30
        {
            get { return m_BtnB30; }
            set
            {
                if (Equals(value, m_BtnB30))
                {
                    return;
                }

                m_BtnB30 = value;
                RaisePropertyChanged(() => BtnB30);
            }
        }

        public void UpdateState()
        {
            try
            {
                var att = GetType().Assembly.GetType(ContentViewName).GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as ViewExportAttribute;
                if (att != null)
                {
                    m_RegionManager.RequestNavigate(att.RegionName, ContentViewName);
                }
            }
            catch (Exception e)
            {
                AppLog.Error("Can not navigate to content view , where regionname ={0}, view name = {1}, {2}",
                    RegionNames.ShellContent, ContentViewName, e);
            }

            if (BtnB1 != null)
            {
                BtnB1.UpdateState();
            }


            if (BtnB2 != null)
            {
                BtnB2.UpdateState();
            }

            if (BtnB3 != null)
            {
                BtnB3.UpdateState();
            }


            if (BtnB4 != null)
            {
                BtnB4.UpdateState();
            }


            if (BtnB5 != null)
            {
                BtnB5.UpdateState();
            }


            if (BtnB6 != null)
            {
                BtnB6.UpdateState();
            }


            if (BtnB7 != null)
            {
                BtnB7.UpdateState();
            }


            if (BtnB8 != null)
            {
                BtnB8.UpdateState();
            }


            if (BtnB9 != null)
            {
                BtnB9.UpdateState();
            }

            if (BtnB10 != null)
            {
                BtnB10.UpdateState();
            }


            if (BtnB11 != null)
            {
                BtnB11.UpdateState();
            }


            if (BtnB12 != null)
            {
                BtnB12.UpdateState();
            }

            if (BtnB13 != null)
            {
                BtnB13.UpdateState();
            }


            if (BtnB14 != null)
            {
                BtnB14.UpdateState();
            }


            if (BtnB15 != null)
            {
                BtnB15.UpdateState();
            }


            if (BtnB16 != null)
            {
                BtnB16.UpdateState();
            }


            if (BtnB17 != null)
            {
                BtnB17.UpdateState();
            }


            if (BtnB18 != null)
            {
                BtnB18.UpdateState();
            }


            if (BtnB19 != null)
            {
                BtnB19.UpdateState();
            }

            if (BtnB20 != null)
            {
                BtnB20.UpdateState();
            }

            if (BtnB21 != null)
            {
                BtnB21.UpdateState();
            }

            if (BtnB22 != null)
            {
                BtnB22.UpdateState();
            }

            if (BtnB23 != null)
            {
                BtnB23.UpdateState();
            }

            if (BtnB24 != null)
            {
                BtnB24.UpdateState();
            }

            if (BtnB25 != null)
            {
                BtnB25.UpdateState();
            }

            if (BtnB26 != null)
            {
                BtnB26.UpdateState();
            }

            if (BtnB27 != null)
            {
                BtnB27.UpdateState();
            }

            if (BtnB28 != null)
            {
                BtnB28.UpdateState();
            }

            if (BtnB29 != null)
            {
                BtnB29.UpdateState();
            }

            if (BtnB30 != null)
            {
                BtnB30.UpdateState();
            }
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
            RaisePropertyChanged(() => BtnB9);
            RaisePropertyChanged(() => BtnB10);
            RaisePropertyChanged(() => BtnB11);
            RaisePropertyChanged(() => BtnB12);
            RaisePropertyChanged(() => BtnB13);
            RaisePropertyChanged(() => BtnB14);
            RaisePropertyChanged(() => BtnB15);
            RaisePropertyChanged(() => BtnB16);
            RaisePropertyChanged(() => BtnB17);
            RaisePropertyChanged(() => BtnB18);
            RaisePropertyChanged(() => BtnB19);
            RaisePropertyChanged(() => BtnB20);
            RaisePropertyChanged(() => BtnB21);
            RaisePropertyChanged(() => BtnB22);
            RaisePropertyChanged(() => BtnB23);
            RaisePropertyChanged(() => BtnB24);
            RaisePropertyChanged(() => BtnB25);
            RaisePropertyChanged(() => BtnB26);
            RaisePropertyChanged(() => BtnB27);
            RaisePropertyChanged(() => BtnB28);
            RaisePropertyChanged(() => BtnB29);
            RaisePropertyChanged(() => BtnB30);
        }
    }
}