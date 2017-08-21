namespace Motor.ATP._300D
{
    /// <summary>
    /// 视图类型
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// 未知 -1
        /// </summary>
        Unkown = -1,

        /// <summary>
        /// 黑屏 0
        /// </summary>
        BlackScreen = 0,

        /// <summary>
        /// 黑屏 44
        /// </summary>
        BlackScreen44 =44,

        /// <summary>
        /// 主页面 100
        /// </summary>
        Main = 100,

        /// <summary>
        /// 司机号 101
        /// </summary>
        DriverID = 101,

        /// <summary>
        /// 车次号 102
        /// </summary>
        TrainNumber = 102,

        /// <summary>
        /// 数据 103
        /// </summary>
        Data = 103,

        /// <summary>
        /// 列控等级 104
        /// </summary>
        ControlLevel = 104,

        /// <summary>
        /// 其它 105
        /// </summary>
        Other =  105,

        /// <summary>
        /// 显示列车数据 106
        /// </summary>
        ShowData = 106,

        /// <summary>
        /// ETCS测试 107
        /// </summary>
        ETCSTest = 107,

        /// <summary>
        /// 模式 108
        /// </summary>
        Model = 108,

        /// <summary>
        /// 启动确认 109
        /// </summary>
        ConfirmStart = 109,

        /// <summary>
        /// 输入列车数据 120
        /// </summary>
        InputTrainData = 120,
    }
}