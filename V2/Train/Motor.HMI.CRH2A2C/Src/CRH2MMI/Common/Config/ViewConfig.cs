using System.ComponentModel;

namespace CRH2MMI.Common.Config
{
    /// <summary>
    /// Description 用于显示标题
    /// </summary>
    enum ViewConfig
    {
        [Description("黑屏")]
        Black = 0,

        /// <summary>
        ///  CRH MENU = 1;
        /// </summary>
        [Description("初始界面")]
        InitView = 1,

        /// <summary>
        /// 行驶状态 = 2
        /// </summary>
        [Description("行驶状态")]
        DriveState = 2,

        /// <summary>
        /// 车辆信息 = 3
        /// </summary>
        [Description("车辆信息")]
        CarInfo = 3,

        /// <summary>
        /// 切除状态 = 4
        /// </summary>
        [Description("切除状态")]
        RemovalState = 4,

        /// <summary>
        ///  制动信息 = 5
        /// </summary>
        [Description("制动信息")]
        BrakeInfo = 5,

        /// <summary>
        /// 牵引变流器信息 编 = 6
        /// </summary>
        [Description("牵引变流器(编)")]
        Tow1 = 6,

        /// <summary>
        /// 电源电压 = 7
        /// </summary>
        [Description("电源电压")]
        PowerVoltage = 7,

        /// <summary>
        ///  车门信息 = 8
        /// </summary>
        [Description("车门信息")]
        DoorInfo = 8,

        /// <summary>
        /// 空转滑行 = 9
        /// </summary>
        [Description("空转滑行")]
        Racing = 9,

        /// <summary>
        /// 牵引变流器信息 车 = 10
        /// </summary>
        [Description("牵引变流器(车)")]
        Tow2 = 10,

        /// <summary>
        ///  累计电力信息= 11
        /// </summary>
        [Description("累计电力")]
        AcumuElec = 11,

        /// <summary>
        ///  轴温切除 = 12
        /// </summary>
        [Description("轴温切除")]
        Axis = 12,

        /// <summary>
        /// 远程控制切除 = 13
        /// </summary>
        [Description("远程控制切除")]
        Telecontr = 13,


        /// <summary>
        /// 抱死切除 = 14 
        /// </summary>
        [Description("抱死切除")]
        BreakLocked = 14,

        /// <summary>
        /// 车次设定 = 15
        /// </summary>
        [Description("车次设定")]
        TNSet = 15,

        /// <summary>
        /// 监控设定 = 16
        /// </summary>
        [Description("监控设定")]
        MonitorSet = 16,

        /// <summary>
        /// 配电盘信息 = 17
        /// </summary>
        [Description("配电盘信息")]
        JackInfo = 17,

        /// <summary>
        /// 当前站设定 = 18
        /// </summary>
        [Description("当前站设定")]
        CurrentStationSet = 18,

        /// <summary>
        /// 车次转换 = 19
        /// </summary>
        [Description("车次转换")]
        TNChange = 19,

        /// <summary>
        /// 联解编组信息 = 20
        /// </summary>
        [Description("联解编组信息")]
        Marsh = 20,

        /// <summary>
        ///  故障一览  = 21
        /// </summary>
        [Description("故障一览")]
        FaultView = 21,

        /// <summary>
        /// 出库信息 = 22
        /// </summary>
        [Description("出库信息")]
        Delivery = 22,

        /// <summary>
        /// 供电分类 = 23
        /// </summary>
        [Description("供电分类")]
        PowerClassfy = 23,

        /// <summary>
        /// 光传输状态 = 24
        /// </summary>
        [Description("光传输状态")]
        Trans = 24,

        /// <summary>
        /// 通知状态 = 25
        /// </summary>
        [Description("通知状态")]
        Notify = 25,

        /// <summary>
        /// 故障信息 = 28
        /// </summary>
        [Description("故障信息")]
        FaultInfo = 28,

        /// <summary>
        /// 试运行 = 30
        /// </summary>
        [Description("试运行")]
        Preoperation = 30,

        /// <summary>
        /// 警惕报警 = 58
        /// </summary>
        [Description("警惕报警")]
        VigilantView = 58,

        /// <summary>
        /// BP救援 = 59
        /// </summary>
        [Description("BP救援")]
        BPRescueView = 59,

        /// <summary>
        /// BP救援 = 59
        /// </summary>
        [Description("BP救援")]
        Bp = 60,

        /// <summary>
        /// 实时轴温检测 = 61
        /// </summary>
        [Description("实时轴温检测")]
        AxleTemperature = 61,

        /// <summary>
        /// 连接头罩信息 = 62
        /// </summary>
        [Description("连接头罩信息")]
        ConnetctHoodInfo = 62,


        /// <summary>
        /// 屏保 = 59
        /// </summary>
        [Description("屏保")]
        Screensavers = 63,

        /// <summary>
        /// 撒砂 = 64
        /// </summary>
        [Description("撒砂检测")]
        SprinkleSand =64,
    }
}