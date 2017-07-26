using System.ComponentModel;
using MMI.Facility.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// ATP
    /// </summary>
    public interface IATP : IIdentityProvider<ScreenIdentity>, IVisibility, INotifyPropertyChanged
    {

        /// <summary>
        /// 服务
        /// </summary>
        IServiceManager ServiceManager { get; }

        /// <summary>
        /// 消息服务
        /// </summary>
        IInfomationService InfomationService { get; }

        /// <summary>
        /// 数据服务
        /// </summary>
        ICommunicationDataService DataService { get; }

        /// <summary>
        /// 司机接口控制
        /// </summary>
        IDriverInterfaceController DriverInterfaceController { get; }

        /// <summary>
        /// 发送接口
        /// </summary>
        ISendInterface SendInterface { set; get; }

        /// <summary>
        /// ATP类型
        /// </summary>
        ATPType ATPType { get; }

        /// <summary>
        /// MMI类型
        /// </summary>
        MMILocation MMILocation { get; }

        /// <summary>
        /// 显示类型
        /// </summary>
        DisplayType DisplayType { get; }

        /// <summary>
        /// 辅助显示信息， LED显示内容
        /// </summary>
        IAssistDisplayInfo AssistDisplayInfo { get; }

        /// <summary>
        /// 版本信息
        /// </summary>
        IVersionManager VersionManager { get; }

        /// <summary>
        /// 电源状态
        /// </summary>
        IATPPower ATPPower { get; }

        /// <summary>
        /// 机控， 人控
        /// </summary>
        TrainControlType TrainControlType { get; }

        /// <summary>
        /// 机控， 人控 是否可见
        /// </summary>
        bool TrainControlTypeVisible { get; }

        /// <summary>
        /// ATP 消息集合
        /// </summary>
        IMessage Message { get; }

        /// <summary>
        /// 车辆信息.
        /// </summary>
        ITrainInfo TrainInfo { get; }

        /// <summary>
        /// CTCS
        /// </summary>
        ICTCS CTCS { get; }

        /// <summary>
        /// 控制模式,
        /// </summary>
        IControlModel ControlModel { get; }

        /// <summary>
        /// 机车信号,
        /// </summary>
        ICabSignal CabSignal { get; }
        
        /// <summary>
        /// 紧急消息
        /// </summary>
        IEmergencyInfo EmergencyInfo { get; }

        /// <summary>
        /// 报警介入
        /// </summary>
        IWarningIntervention WarningIntervention { get; }

        /// <summary>
        /// 速度监视
        /// </summary>
        ISpeedMonitoringSection SpeedMonitoringSection { get; }

        /// <summary>
        /// 预告信息
        /// </summary>
        IForecastInformation ForecastInformation { get; }

        /// <summary>
        /// 其它信息
        /// </summary>
        IOther Other { get; }

        /// <summary>
        /// 硬件按键
        /// </summary>
        IATPHardwareButton ATPHardwareButton { get; }

        /// <summary>
        /// 坡度信息
        /// </summary>
        IGradientInfomation GradientInfomation { get; }

        /// <summary>
        /// F 区按键状态提供
        /// </summary>
        IRegionFStateProvier RegionFStateProvier { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="identity"></param>
        void Initalize(ScreenIdentity identity);

        /// <summary>
        /// 故障发生
        /// </summary>
        void OnFaultOccused(IInfomationItem faultItem);

        /// <summary>
        /// 故障解决
        /// </summary>
        void OnFaultFixed(IInfomationItem faultItem);
    }
}
