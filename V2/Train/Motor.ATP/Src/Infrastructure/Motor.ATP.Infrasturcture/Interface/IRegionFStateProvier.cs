using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRegionFStateProvier : IATPPartial, IVisibility
    {
        /// <summary>
        /// 数据
        /// </summary>
        IItemStateProvider DataStateProvider { get; }

        /// <summary>
        /// 控制模式
        /// </summary>
        IItemStateProvider ControlModelStateProvider { get; }

        /// <summary>
        /// 载频
        /// </summary>
        IItemStateProvider FreqStateProvider { get; }

        /// <summary>
        /// 上行载频
        /// </summary>
        IItemStateProvider UpFreqStateProvider { get; }

        /// <summary>
        /// 下行载频
        /// </summary>
        IItemStateProvider DownFreqStateProvider { get; }

        /// <summary>
        /// 等级
        /// </summary>
        IItemStateProvider CTCSStateProvider { get; }

        /// <summary>
        ///CTCS0
        /// </summary>
        IItemStateProvider CTCS0StateProvider { get; }

        /// <summary>
        /// CTCS2
        /// </summary>
        IItemStateProvider CTCS2StateProvider { get; }

        /// <summary>
        /// CTCS3
        /// </summary>
        IItemStateProvider CTCS3StateProvider { get; }

        /// <summary>
        /// CTCS3D
        /// </summary>
        IItemStateProvider CTCS3DStateProvider { get; }

        /// <summary>
        /// 其它
        /// </summary>
        IItemStateProvider OtherStateProvider { get; }

        /// <summary>
        /// 警惕
        /// </summary>
        IItemStateProvider VigilantStateProvider { get; }

        /// <summary>
        /// 司机号
        /// </summary>
        IItemStateProvider DriverIdStateProvider { get; }

        /// <summary>
        /// 车次号
        /// </summary>
        IItemStateProvider TrainIdStateProvider { get; }

        /// <summary>
        /// 列车数据
        /// </summary>
        IItemStateProvider TrainDataStateProvider { get; }

        /// <summary>
        /// 8辆 
        /// </summary>
        IItemStateProvider EightTrucksStateProvider { get; }

        /// <summary>
        /// 16辆
        /// </summary>
        IItemStateProvider SixteenTrucksStateProvider { get; }

        /// <summary>
        /// RBC数据
        /// </summary>
        IItemStateProvider RBCDataStateProvider { get; }

        /// <summary>
        /// RBC数据ID
        /// </summary>
        IItemStateProvider RBCIDStateProvider { get; }

        /// <summary>
        /// 电话号码
        /// </summary>
        IItemStateProvider TelNumberStateProvider { get; }

        /// <summary>
        /// 网络号码
        /// </summary>
        IItemStateProvider NetNumberStateProvider { get; }

        /// <summary>
        /// 调车 
        /// </summary>
        IEnterableItemStateProvider ShuntingStateProvider { get; }

        /// <summary>
        /// 目视
        /// </summary>
        IEnterableItemStateProvider OnSightStateProvider { get; }

        /// <summary>
        /// 开始
        /// </summary>
        IItemStateProvider StartStateProvider { get; }

        /// <summary>
        /// 机车信号
        /// </summary>
        IEnterableItemStateProvider CabsignalStateProvider { get; }

        /// <summary>
        /// 缓解
        /// </summary>
        IItemStateProvider RelaseStateProvider { get; }

        /// <summary>
        /// 执行制动测试
        /// </summary>
        IItemStateProvider RunBrakeTestStateProvider { get; }

        /// <summary>
        /// 常用制动
        /// </summary>
        IItemStateProvider RunNormalBrakeTestStateProvider { get; }

        /// <summary>
        /// 紧急制动
        /// </summary>
        IItemStateProvider RunEmergencyBrakeTestStateProvider { get; }

        /// <summary>
        /// DMI测试
        /// </summary>
        IItemStateProvider DMITestStateProvider { get; }

        /// <summary>
        /// 退出制动测试
        /// </summary>
        IItemStateProvider QuitTestStateProvider { get; }


        /// <summary>
        /// 略过制动测试
        /// </summary>
        IItemStateProvider SkipRunBrakeTestStateProvider { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IItemStateProvider : INotifyPropertyChanged
    {
        /// <summary>
        /// 使能
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 可见
        /// </summary>
        bool Visible { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IEnterableItemStateProvider : IItemStateProvider
    {
        /// <summary>
        ///  进入还是退出
        /// </summary>
        EnterOrQuit EnterOrQuitState { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnterOrQuit
    {
        /// <summary>
        /// Enter
        /// </summary>
        Enter,

        /// <summary>
        /// Quit
        /// </summary>
        Quit,
    }
}