using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.ConfigModel;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class HandleTestModel : ModelBase
    {
        private HandleTestState m_Handle1TestState1;
        private HandleTestState m_Handle1TestState2;
        private HandleTestState m_Handle1TestState3;
        private HandleTestState m_Handle1TestState4;
        private HandleTestState m_Handle1TestState5;
        private HandleTestState m_Handle1TestState6;
        private HandleTestState m_Handle1TestState7;
        private HandleTestState m_Handle1TestState8;
        private HandleTestState m_Handle1TestState9;
        private HandleTestState m_Handle1TestState10;
        private HandleTestState m_Handle1TestState11;
        private HandleTestState m_Handle1TestState12;
        private HandleTestState m_Handle1TestState13;
        private HandleTestState m_Handle1TestState14;
        private HandleTestState m_Handle1TestState15;
        private HandleTestState m_Handle1TestState16;
        private HandleTestState m_Handle0TestState1;
        private HandleTestState m_Handle0TestState2;
        private HandleTestState m_Handle0TestState3;
        private HandleTestState m_Handle0TestState4;
        private HandleTestState m_Handle0TestState5;
        private HandleTestState m_Handle0TestState6;
        private HandleTestState m_Handle0TestState7;
        private HandleTestState m_Handle0TestState8;
        private HandleTestState m_Handle0TestState9;
        private HandleTestState m_Handle0TestState10;
        private HandleTestState m_Handle0TestState11;
        private HandleTestState m_Handle0TestState12;
        private HandleTestState m_Handle0TestState13;
        private HandleTestState m_Handle0TestState14;
        private HandleTestState m_Handle0TestState15;
        private HandleTestState m_Handle0TestState16;

        private ObservableCollection<HandleTestInfo> m_AllHandleTestInfo;
        private int m_HandleTestInfoNumDisplay;
        private string m_HandleTestInfoDisplay;
        private bool m_IsEnable;

        [ImportingConstructor]
        public HandleTestModel(HandleTestController handleTestController)
        {

            m_AllHandleTestInfo = new ObservableCollection<HandleTestInfo>(GlobalParam.Instance.HandleTestInfos.OrderBy(o => o.Index));

            HandleTestController = handleTestController;
            HandleTestController.ViewModel = this;
            HandleTestController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public HandleTestController HandleTestController { get; set; }

        /// <summary>
        /// 所有制动试验提示文本
        /// </summary>
        public ObservableCollection<HandleTestInfo> AllHandleTestInfo
        {
            get { return m_AllHandleTestInfo; }
            set
            {
                if (value == m_AllHandleTestInfo)
                {
                    return;
                }
                m_AllHandleTestInfo = value;
                RaisePropertyChanged(() => AllHandleTestInfo);
            }
        }

        /// <summary>
        /// 提示文本是否显示 
        /// </summary>
        public bool IsEnable
        {
            get { return m_IsEnable; }
            set
            {
                if (value == m_IsEnable)
                {
                    return;
                }
                m_IsEnable = value;
                RaisePropertyChanged(() => IsEnable);
            }
        }

        /// <summary>
        /// 需要显示的制动试验提示文本的编号
        /// </summary>
        public int HandleTestInfoNumDisplay
        {
            get { return m_HandleTestInfoNumDisplay; }
            set
            {
                if (value == m_HandleTestInfoNumDisplay)
                {
                    return;
                }
                m_HandleTestInfoNumDisplay = value;
                HandleTestController.UpdateHandleTestInfoDisplay(value);
                RaisePropertyChanged(() => HandleTestInfoNumDisplay);
            }
        }

        /// <summary>
        /// 需要显示的制动试验提示文本
        /// </summary>
        public string HandleTestInfoDisplay
        {
            get { return m_HandleTestInfoDisplay; }
            set
            {
                if (value == m_HandleTestInfoDisplay)
                {
                    return;
                }
                m_HandleTestInfoDisplay = value;
                RaisePropertyChanged(() => HandleTestInfoDisplay);
            }
        }

        /// <summary>
        /// 司机室1手柄1
        /// </summary>
        public HandleTestState Handle1TestState1
        {
            get { return m_Handle1TestState1; }
            set
            {
                if (value == m_Handle1TestState1)
                {
                    return;
                }
                m_Handle1TestState1 = value;
                RaisePropertyChanged(() => Handle1TestState1);
            }
        }

        /// <summary>
        /// 司机室1手柄2
        /// </summary>
        public HandleTestState Handle1TestState2
        {
            get { return m_Handle1TestState2; }
            set
            {
                if (value == m_Handle1TestState2)
                {
                    return;
                }
                m_Handle1TestState2 = value;
                RaisePropertyChanged(() => Handle1TestState2);
            }
        }

        /// <summary>
        /// 司机室1手柄3
        /// </summary>
        public HandleTestState Handle1TestState3
        {
            get { return m_Handle1TestState3; }
            set
            {
                if (value == m_Handle1TestState3)
                {
                    return;
                }
                m_Handle1TestState3 = value;
                RaisePropertyChanged(() => Handle1TestState3);
            }
        }

        /// <summary>
        /// 司机室1手柄4
        /// </summary>
        public HandleTestState Handle1TestState4
        {
            get { return m_Handle1TestState4; }
            set
            {
                if (value == m_Handle1TestState4)
                {
                    return;
                }
                m_Handle1TestState4 = value;
                RaisePropertyChanged(() => Handle1TestState4);
            }
        }

        /// <summary>
        /// 司机室1手柄5
        /// </summary>
        public HandleTestState Handle1TestState5
        {
            get { return m_Handle1TestState5; }
            set
            {
                if (value == m_Handle1TestState5)
                {
                    return;
                }
                m_Handle1TestState5 = value;
                RaisePropertyChanged(() => Handle1TestState5);
            }
        }

        /// <summary>
        /// 司机室1手柄6
        /// </summary>
        public HandleTestState Handle1TestState6
        {
            get { return m_Handle1TestState6; }
            set
            {
                if (value == m_Handle1TestState6)
                {
                    return;
                }
                m_Handle1TestState6 = value;
                RaisePropertyChanged(() => Handle1TestState6);
            }
        }

        /// <summary>
        /// 司机室1手柄7
        /// </summary>
        public HandleTestState Handle1TestState7
        {
            get { return m_Handle1TestState7; }
            set
            {
                if (value == m_Handle1TestState7)
                {
                    return;
                }
                m_Handle1TestState7 = value;
                RaisePropertyChanged(() => Handle1TestState7);
            }
        }

        /// <summary>
        /// 司机室1手柄8
        /// </summary>
        public HandleTestState Handle1TestState8
        {
            get { return m_Handle1TestState8; }
            set
            {
                if (value == m_Handle1TestState8)
                {
                    return;
                }
                m_Handle1TestState8 = value;
                RaisePropertyChanged(() => Handle1TestState8);
            }
        }

        /// <summary>
        /// 司机室1手柄9
        /// </summary>
        public HandleTestState Handle1TestState9
        {
            get { return m_Handle1TestState9; }
            set
            {
                if (value == m_Handle1TestState9)
                {
                    return;
                }
                m_Handle1TestState9 = value;
                RaisePropertyChanged(() => Handle1TestState9);
            }
        }

        /// <summary>
        /// 司机室1手柄10
        /// </summary>
        public HandleTestState Handle1TestState10
        {
            get { return m_Handle1TestState10; }
            set
            {
                if (value == m_Handle1TestState10)
                {
                    return;
                }
                m_Handle1TestState10 = value;
                RaisePropertyChanged(() => Handle1TestState10);
            }
        }

        /// <summary>
        /// 司机室1手柄11
        /// </summary>
        public HandleTestState Handle1TestState11
        {
            get { return m_Handle1TestState11; }
            set
            {
                if (value == m_Handle1TestState11)
                {
                    return;
                }
                m_Handle1TestState11 = value;
                RaisePropertyChanged(() => Handle1TestState11);
            }
        }

        /// <summary>
        /// 司机室1手柄12
        /// </summary>
        public HandleTestState Handle1TestState12
        {
            get { return m_Handle1TestState12; }
            set
            {
                if (value == m_Handle1TestState12)
                {
                    return;
                }
                m_Handle1TestState12 = value;
                RaisePropertyChanged(() => Handle1TestState12);
            }
        }

        /// <summary>
        /// 司机室1手柄13
        /// </summary>
        public HandleTestState Handle1TestState13
        {
            get { return m_Handle1TestState13; }
            set
            {
                if (value == m_Handle1TestState13)
                {
                    return;
                }
                m_Handle1TestState13 = value;
                RaisePropertyChanged(() => Handle1TestState13);
            }
        }

        /// <summary>
        /// 司机室1手柄14
        /// </summary>
        public HandleTestState Handle1TestState14
        {
            get { return m_Handle1TestState14; }
            set
            {
                if (value == m_Handle1TestState14)
                {
                    return;
                }
                m_Handle1TestState14 = value;
                RaisePropertyChanged(() => Handle1TestState14);
            }
        }

        /// <summary>
        /// 司机室1手柄15
        /// </summary>
        public HandleTestState Handle1TestState15
        {
            get { return m_Handle1TestState15; }
            set
            {
                if (value == m_Handle1TestState15)
                {
                    return;
                }
                m_Handle1TestState15 = value;
                RaisePropertyChanged(() => Handle1TestState15);
            }
        }

        /// <summary>
        /// 司机室1手柄16
        /// </summary>
        public HandleTestState Handle1TestState16
        {
            get { return m_Handle1TestState16; }
            set
            {
                if (value == m_Handle1TestState16)
                {
                    return;
                }
                m_Handle1TestState16 = value;
                RaisePropertyChanged(() => Handle1TestState16);
            }
        }

        /// <summary>
        /// 司机室0手柄1
        /// </summary>
        public HandleTestState Handle0TestState1
        {
            get { return m_Handle0TestState1; }
            set
            {
                if (value == m_Handle0TestState1)
                {
                    return;
                }
                m_Handle0TestState1 = value;
                RaisePropertyChanged(() => Handle0TestState1);
            }
        }

        /// <summary>
        /// 司机室0手柄2
        /// </summary>
        public HandleTestState Handle0TestState2
        {
            get { return m_Handle0TestState2; }
            set
            {
                if (value == m_Handle0TestState2)
                {
                    return;
                }
                m_Handle0TestState2 = value;
                RaisePropertyChanged(() => Handle0TestState2);
            }
        }

        /// <summary>
        /// 司机室0手柄3
        /// </summary>
        public HandleTestState Handle0TestState3
        {
            get { return m_Handle0TestState3; }
            set
            {
                if (value == m_Handle0TestState3)
                {
                    return;
                }
                m_Handle0TestState3 = value;
                RaisePropertyChanged(() => Handle0TestState3);
            }
        }

        /// <summary>
        /// 司机室0手柄4
        /// </summary>
        public HandleTestState Handle0TestState4
        {
            get { return m_Handle0TestState4; }
            set
            {
                if (value == m_Handle0TestState4)
                {
                    return;
                }
                m_Handle0TestState4 = value;
                RaisePropertyChanged(() => Handle0TestState4);
            }
        }

        /// <summary>
        /// 司机室0手柄5
        /// </summary>
        public HandleTestState Handle0TestState5
        {
            get { return m_Handle0TestState5; }
            set
            {
                if (value == m_Handle0TestState5)
                {
                    return;
                }
                m_Handle0TestState5 = value;
                RaisePropertyChanged(() => Handle0TestState5);
            }
        }

        /// <summary>
        /// 司机室0手柄6
        /// </summary>
        public HandleTestState Handle0TestState6
        {
            get { return m_Handle0TestState6; }
            set
            {
                if (value == m_Handle0TestState6)
                {
                    return;
                }
                m_Handle0TestState6 = value;
                RaisePropertyChanged(() => Handle0TestState6);
            }
        }

        /// <summary>
        /// 司机室0手柄7
        /// </summary>
        public HandleTestState Handle0TestState7
        {
            get { return m_Handle0TestState7; }
            set
            {
                if (value == m_Handle0TestState7)
                {
                    return;
                }
                m_Handle0TestState7 = value;
                RaisePropertyChanged(() => Handle0TestState7);
            }
        }

        /// <summary>
        /// 司机室0手柄8
        /// </summary>
        public HandleTestState Handle0TestState8
        {
            get { return m_Handle0TestState8; }
            set
            {
                if (value == m_Handle0TestState8)
                {
                    return;
                }
                m_Handle0TestState8 = value;
                RaisePropertyChanged(() => Handle0TestState8);
            }
        }

        /// <summary>
        /// 司机室0手柄9
        /// </summary>
        public HandleTestState Handle0TestState9
        {
            get { return m_Handle0TestState9; }
            set
            {
                if (value == m_Handle0TestState9)
                {
                    return;
                }
                m_Handle0TestState9 = value;
                RaisePropertyChanged(() => Handle0TestState9);
            }
        }

        /// <summary>
        /// 司机室0手柄10
        /// </summary>
        public HandleTestState Handle0TestState10
        {
            get { return m_Handle0TestState10; }
            set
            {
                if (value == m_Handle0TestState10)
                {
                    return;
                }
                m_Handle0TestState10 = value;
                RaisePropertyChanged(() => Handle0TestState10);
            }
        }

        /// <summary>
        /// 司机室0手柄11
        /// </summary>
        public HandleTestState Handle0TestState11
        {
            get { return m_Handle0TestState11; }
            set
            {
                if (value == m_Handle0TestState11)
                {
                    return;
                }
                m_Handle0TestState11 = value;
                RaisePropertyChanged(() => Handle0TestState11);
            }
        }

        /// <summary>
        /// 司机室0手柄12
        /// </summary>
        public HandleTestState Handle0TestState12
        {
            get { return m_Handle0TestState12; }
            set
            {
                if (value == m_Handle0TestState12)
                {
                    return;
                }
                m_Handle0TestState12 = value;
                RaisePropertyChanged(() => Handle0TestState12);
            }
        }

        /// <summary>
        /// 司机室0手柄13
        /// </summary>
        public HandleTestState Handle0TestState13
        {
            get { return m_Handle0TestState13; }
            set
            {
                if (value == m_Handle0TestState13)
                {
                    return;
                }
                m_Handle0TestState13 = value;
                RaisePropertyChanged(() => Handle0TestState13);
            }
        }

        /// <summary>
        /// 司机室0手柄14
        /// </summary>
        public HandleTestState Handle0TestState14
        {
            get { return m_Handle0TestState14; }
            set
            {
                if (value == m_Handle0TestState14)
                {
                    return;
                }
                m_Handle0TestState14 = value;
                RaisePropertyChanged(() => Handle0TestState14);
            }
        }

        /// <summary>
        /// 司机室0手柄15
        /// </summary>
        public HandleTestState Handle0TestState15
        {
            get { return m_Handle0TestState15; }
            set
            {
                if (value == m_Handle0TestState15)
                {
                    return;
                }
                m_Handle0TestState15 = value;
                RaisePropertyChanged(() => Handle0TestState15);
            }
        }

        /// <summary>
        /// 司机室0手柄16
        /// </summary>
        public HandleTestState Handle0TestState16
        {
            get { return m_Handle0TestState16; }
            set
            {
                if (value == m_Handle0TestState16)
                {
                    return;
                }
                m_Handle0TestState16 = value;
                RaisePropertyChanged(() => Handle0TestState16);
            }
        }
    }
}