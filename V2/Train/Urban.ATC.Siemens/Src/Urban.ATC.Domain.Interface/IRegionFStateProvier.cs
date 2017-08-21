using System.ComponentModel;

namespace Motor.ATP.Domain.Interface
{
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
        /// 载频
        /// </summary>
        IItemStateProvider UpFreqStateProvider { get; }

        /// <summary>
        /// 载频
        /// </summary>
        IItemStateProvider DownFreqStateProvider { get; }

        /// <summary>
        /// 等级
        /// </summary>
        IItemStateProvider CTCSStateProvider { get; }

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
        /// RBC数据
        /// </summary>
        IItemStateProvider RBCDataStateProvider { get; }

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
    }

    public interface IItemStateProvider : INotifyPropertyChanged
    {
        bool Enabled { get; }

        bool Visible { get; }
    }

    public interface IEnterableItemStateProvider : IItemStateProvider
    {
        /// <summary>
        ///  进入还是退出
        /// </summary>
        EnterOrQuit EnterOrQuitState { get; }
    }

    public enum EnterOrQuit
    {
        Enter,

        Quit,
    }
}