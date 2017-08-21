using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class TCMSViewModel : NotificationObject
    {
        private bool m_Visible;

        [ImportingConstructor]
        public TCMSViewModel(MainDataViewModel mainDataViewModel, TrainViewModel trainViewModel,
            TowEleMachineViewModel towEleMachineViewModel, SwitchStateViewModel switchStateViewModel,
            WindMachineStateViewModel windMachineStateViewModel, AssitPowerViewModel assitPowerViewModel,
            AirBrakeViewModel airBrakeViewModel, TowBrakeViewModel towBrakeViewModel, FaultViewModel faultViewModel,
            TestRunViewModel testRunViewModel, OpenStateViewModel openStateViewModel,
            PasswordSetteingViewModel passwordSetteingViewModel,
            PasswordInputKeyBoardModel inputKeyBoardModel, SignalInfoViewModel signalInfoViewModel,
            TotalWalkingDistanceViewModel totalWalkingDistanceViewModel, ActionCountViewModel actionCountViewModel,
            TransferInfoViewModel transferInfoViewModel, SoftWareVersionModel softWareVersionModel,
            MasterDriverControlViewModel masterDriverControlViewModel, StartTestViewModel startTestViewModel,
            DisplayLightViewModel displayLightViewModel, AssistPowerTestViewModel assistPowerTestViewModel,
            ZeroLevelTestViewModel zeroLevelTestViewModel)
        {
            Visible = true;
            MainDataViewModel = mainDataViewModel;
            TrainViewModel = trainViewModel;
            TowEleMachineViewModel = towEleMachineViewModel;
            SwitchStateViewModel = switchStateViewModel;
            WindMachineStateViewModel = windMachineStateViewModel;
            AssitPowerViewModel = assitPowerViewModel;
            AirBrakeViewModel = airBrakeViewModel;
            TowBrakeViewModel = towBrakeViewModel;
            FaultViewModel = faultViewModel;
            TestRunViewModel = testRunViewModel;
            OpenStateViewModel = openStateViewModel;
            InputKeyBoardModel = inputKeyBoardModel;
            SignalInfoViewModel = signalInfoViewModel;
            TotalWalkingDistanceViewModel = totalWalkingDistanceViewModel;
            ActionCountViewModel = actionCountViewModel;
            TransferInfoViewModel = transferInfoViewModel;
            SoftWareVersionModel = softWareVersionModel;
            MasterDriverControlViewModel = masterDriverControlViewModel;
            StartTestViewModel = startTestViewModel;
            DisplayLightViewModel = displayLightViewModel;
            AssistPowerTestViewModel = assistPowerTestViewModel;
            ZeroLevelTestViewModel = zeroLevelTestViewModel;
            PasswordSetteingViewModel = passwordSetteingViewModel;
            PasswordSetteingViewModel.Parent = this;
        }


        public bool Visible
        {
            set
            {
                if (value == m_Visible)
                {
                    return;
                }

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
            get { return m_Visible; }
        }

        /// <summary>
        /// 检修密码输入Model
        /// </summary>
        public PasswordSetteingViewModel PasswordSetteingViewModel { get; private set; }

        public HXD3TCMSViewModel Parent { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public MainDataViewModel MainDataViewModel { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public TowBrakeViewModel TowBrakeViewModel { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public TrainViewModel TrainViewModel { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public TowEleMachineViewModel TowEleMachineViewModel { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public SwitchStateViewModel SwitchStateViewModel { private set; get; }

        public WindMachineStateViewModel WindMachineStateViewModel { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public AssitPowerViewModel AssitPowerViewModel { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public AirBrakeViewModel AirBrakeViewModel { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public FaultViewModel FaultViewModel { private set; get; }


        /// <summary>
        /// 
        /// </summary>
        public TestRunViewModel TestRunViewModel { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public OpenStateViewModel OpenStateViewModel { private set; get; }

        /// <summary>
        /// 密码输入键盘Model
        /// </summary>
        public PasswordInputKeyBoardModel InputKeyBoardModel { get; private set; }

        /// <summary>
        /// 信号信息Model
        /// </summary>
        public SignalInfoViewModel SignalInfoViewModel { get; private set; }

        /// <summary>
        /// 总走行距离Model
        /// </summary>
        public TotalWalkingDistanceViewModel TotalWalkingDistanceViewModel { get; private set; }

        /// <summary>
        /// 动作次数Model
        /// </summary>
        public ActionCountViewModel ActionCountViewModel { get; private set; }

        /// <summary>
        /// 传递信息Model
        /// </summary>
        public TransferInfoViewModel TransferInfoViewModel { get; private set; }

        /// <summary>
        /// 软件版本Model
        /// </summary>
        public SoftWareVersionModel SoftWareVersionModel { get; private set; }

        /// <summary>
        /// 主司控器测试Model
        /// </summary>
        public MasterDriverControlViewModel MasterDriverControlViewModel { get; private set; }

        /// <summary>
        /// 起动测试Model
        /// </summary>
        public StartTestViewModel StartTestViewModel { get; private set; }

        /// <summary>
        /// 显示灯测试Model
        /// </summary>
        public DisplayLightViewModel DisplayLightViewModel { get; private set; }

        /// <summary>
        /// 辅助电源测试
        /// </summary>
        public AssistPowerTestViewModel AssistPowerTestViewModel { private set; get; }

        /// <summary>
        /// 零级位测试
        /// </summary>
        public ZeroLevelTestViewModel ZeroLevelTestViewModel { private set; get; }
    }
}