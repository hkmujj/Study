using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Interfaces;
using Motor.HMI.CRH380D.Models.BtnStragy;
using Motor.HMI.CRH380D.Models.Model;

namespace Motor.HMI.CRH380D.Models
{
    [Export]
    [Export(typeof(IModels))]
    public class DomainModel : ModelBase
    {
        private IStateInterface m_CurrentStateInterface;

        /// <summary>
        /// 标题栏
        /// </summary>
        [Import]
        public TitleModel TitleModel { get; private set; }

        /// <summary>
        /// 激活
        /// </summary>
        [Import]
        public ActivateModel ActivateModel { get; private set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        [Import]
        public TrainModel TrainModel { get; private set; }

        /// <summary>
        /// 外门状态
        /// </summary>
        [Import]
        public DoorModel DoorModel { get; private set; }

        /// <summary>
        /// 门状态
        /// </summary>
        [Import]
        public StationModel StationModel { get; private set; }

        /// <summary>
        /// 警报汇总
        /// </summary>
        [Import]
        public WarringSumMenuModel WarringSumMenuModel { get; private set; }

        /// <summary>
        /// 互锁
        /// </summary>
        [Import]
        public InterLockModel InterLockModel { get; private set; }

        /// <summary>
        /// 高压
        /// </summary>
        [Import]
        public HighVoltyageModel HighVoltyageModel { get; private set; }

        /// <summary>
        /// 牵引
        /// </summary>
        [Import]
        public TractionModel TractionModel { get; private set; }

        /// <summary>
        /// 制动
        /// </summary>
        [Import]
        public BreakModel BreakModel { get; private set; }

        /// <summary>
        /// 司机室舒适度
        /// </summary>
        [Import]
        public DriverComfortModel DriverComfortModel { get; private set; }

        /// <summary>
        /// 客室舒适度
        /// </summary>
        [Import]
        public CarComfortModel CarComfortModel { get; private set; }

        /// <summary>
        /// 司机室状态
        /// </summary>
        [Import]
        public VOBCModel VOBCModel { get; private set; }


        /// <summary>
        /// 供风
        /// </summary>
        [Import]
        public AirSupplyModel AirSupplyModel { get; private set; }

        /// <summary>
        /// 转向架
        /// </summary>
        [Import]
        public BogieModel BogieModel { get; private set; }

        /// <summary>
        /// 牵引/制动
        /// </summary>
        [Import]
        public TractionAndBreakModel TractionAndBreakModel { get; private set; }

        /// <summary>
        /// 卫生设备
        /// </summary>
        [Import]
        public WCDeviceModel WCDeviceModel { get; private set; }

        /// <summary>
        /// 制动试验概况
        /// </summary>
        [Import]
        public BreakTestModel BreakTestModel { get; private set; }

        /// <summary>
        /// 手柄测试
        /// </summary>
        [Import]
        public HandleTestModel HandleTestModel { get; private set; }

        /// <summary>
        /// 列车状态
        /// </summary>
        [Import]
        public TrainStatusModel TrainStatusModel { get; private set; }
        
        /// <summary>
        /// 直流供电
        /// </summary>
        [Import]
        public DCModel DCModel { get; private set; }

        /// <summary>
        /// 火灾探测器
        /// </summary>
        [Import]
        public FireDeviceModel FireDeviceModel { get; private set; }

        /// <summary>
        /// 设置
        /// </summary>
        [Import]
        public SettingModel SettingModel { get; private set; }

        /// <summary>
        /// 登陆
        /// </summary>
        [Import]
        public LoginInfoModel LoginInfoModel { get; private set; }

        /// <summary>
        /// 事件
        /// </summary>
        [Import]
        public EventInfoModel EventInfoModel { get; private set; }

        //按键
        private IStateInterface m_StateInterface;
        
        /// <summary>
        /// 按键
        /// </summary>
        public IStateInterface StateInterface
        {
            private set
            {
                if (Equals(value, m_StateInterface))
                {
                    return;
                }

                m_StateInterface = value;
                m_StateInterface.UpdateState();
                RaisePropertyChanged(() => StateInterface);
            }
            get { return m_StateInterface; }
        }

        /// <summary>
        /// 更新按键
        /// </summary>
        /// <param name="current"></param>
        public void UpdateState(IStateInterface current)
        {
            StateInterface = current;
        }
    }
}