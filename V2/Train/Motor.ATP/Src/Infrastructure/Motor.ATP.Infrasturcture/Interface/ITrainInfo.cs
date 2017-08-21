using System.Collections.ObjectModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 车辆相关信息
    /// </summary>
    public interface ITrainInfo : IATPPartial
    {
        /// <summary>
        /// 当前列车系个数
        /// </summary>
        int CurrentTrainGroupCount { get; }

        /// <summary>
        /// 列车长度
        /// </summary>
        ObservableCollection<float> TrainLegth { get; }

        /// <summary>
        /// 速度信息
        /// </summary>
        ISpeed Speed { get; }

        /// <summary>
        /// 制动信息
        /// </summary>
        IBrake Brake { get; }

        /// <summary>
        /// 驾驶信息
        /// </summary>
        IDriver Driver { get; }

        /// <summary>
        /// 车站信息
        /// </summary>
        IStation Station { get; }

        /// <summary>
        /// 公里标
        /// </summary>
        IKilometerPost KilometerPost { get; }

        /// <summary>
        /// 连接状态
        /// </summary>
        IConnectState ConnectState { get; }


    }
}