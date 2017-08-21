namespace ATP200C.PublicComponents
{
    enum MenuName
    {
        /// <summary>
        /// 未选择
        /// 1级菜单
        /// </summary>
        F0 = 0,

        /// <summary>
        /// 数据
        /// 2级菜单
        /// </summary>
        F1Data = 1,

        /// <summary>
        /// 司机号
        /// 2级菜单
        /// </summary>
        F1DriverId = 11,

        /// <summary>
        /// 车次号
        /// 2级菜单
        /// </summary>
        F1TrainId = 12,

        /// <summary>
        /// 列车数据
        /// 2级菜单
        /// </summary>
        F1TrainData = 13,

        /// <summary>
        /// 模式
        /// 2级菜单
        /// </summary>
        F2Model = 2,

        /// <summary>
        /// 载频
        /// 2级菜单
        /// </summary>
        F3Carrier = 3,

        /// <summary>
        /// 等级
        /// 2级菜单
        /// </summary>
        F4Level = 4,

        /// <summary>
        /// 音量亮度
        /// </summary>
        F5VolumeBrightness = 5,

        /// <summary>
        /// 亮度
        /// </summary>
        F5Brightness = 52,

        /// <summary>
        /// 音量
        /// </summary>
        F5Volume = 53,


    }

    public enum BtnTypeName
    {
        F1 = 0,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,

        Num1,
        Num2,
        Num3,
        Num4,
        Num5,
        Num6,
        Num7,
        Num8,
        Num9,
        Num0,

        Switch,
    }

    public enum FontName
    {
        Arial,
        宋体,
        黑体,
        幼圆,
    }

    public enum FontRelated
    {
        居中,
        靠左,
        靠右,
        靠左上,
        靠左下,
    }

    public enum RectRiseDirection
    {
        上,
        下,
        左,
        右,
    }

    public enum ControModelEnum
    {
        Null = 0,
        完全 = 1,
        目视 = 2,
        部分 = 3,
        引导 = 4,
        调车 = 5,
        LKJ = 6,
        待机 = 7,
        隔离 = 8,
        冒进 = 10,
        冒后 = 11,
    }

    enum TaskFlow
    {
        /// <summary>
        /// 上电
        /// </summary>
        PowerOn,

        /// <summary>
        /// 自检
        /// </summary>
        SelfCheck,

        /// <summary>
        /// 任务数据
        /// </summary>
        TaskData,

        /// <summary>
        /// 等级选择
        /// </summary>
        SelectLevel,

        /// <summary>
        /// CTCS-0
        /// </summary>
        CTCS0,

        /// <summary>
        /// 载频选择
        /// </summary>
        SelectCarrier,

        /// <summary>
        /// C0级运行
        /// </summary>
        RunC0,

        /// <summary>
        /// CTCS-2级
        /// </summary>
        CTCS2,

        /// <summary>
        /// 模式选择
        /// </summary>
        SelectModel,

        /// <summary>
        /// 调车
        /// </summary>
        Shunt,

        /// <summary>
        /// C2运行
        /// </summary>
        RunC2,

        /// <summary>
        /// 任务执行
        /// </summary>
        TaskDo,

        /// <summary>
        /// 任务结束
        /// </summary>
        TaskOver,
    }

    public enum CTCSLevel
    {
        None,
        CTCS0,
        CTCS1,
        CTCS2,
        TVM,
    }

    /// <summary>
    /// 表盘绘制模式枚举
    /// </summary>
    public enum DrawModelEnum
    {
        /// <summary>
        /// 全部绘制 包括弧形，和刻度
        /// </summary>
        DrawDetail,

        /// <summary>
        /// 绘制刻度
        /// </summary>
        DrawKeDu,

        /// <summary>
        /// 均不绘制
        /// </summary>
        DrawNull
    }
}
