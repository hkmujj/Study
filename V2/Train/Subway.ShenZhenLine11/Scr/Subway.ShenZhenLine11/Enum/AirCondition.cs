using System.ComponentModel;

namespace Subway.ShenZhenLine11.Enum
{
    public enum AirCondition
    {
        Normal,
        [Description("空调严重故障")]
        Falut,
        [Description("空调中等故障")]
        Warn,
        [Description("紧急通风模式")]
        EmergencyAir,
        [Description("通风模式")]
        Air,
        [Description("限制制冷模式")]
        Limit,
        [Description("正常运行")]
        Run,
        [Description("关机状态")]
        Off,
    }

    public enum AssistPowerAC
    {
        Normal,
        [Description("AC/DC严重故障")]
        Fault,
        [Description("AC/DC中等故障")]
        Warn,
        [Description("AC/DC正常运行")]
        Run,
        [Description("关机状态")]
        Off,
    }
    public enum AssistPowerDC
    {
        Normal,
        [Description("DCDC严重故障")]
        Fault,
        [Description("DCDC中等故障")]
        Warn,
        [Description("DCDC正常运行")]
        Run,
        [Description("关机状态")]
        Off,
    }

    public enum Door
    {
        Normal,
        [Description("紧急情况下从里圈或外面打开")]
        Emergency,
        [Description("门切除")]
        Cut,
        [Description("门故障")]
        SeverityFault,
        [Description("门警告")]
        MediumFaulr,
        [Description("门检测到有障碍")]
        Check,
        [Description("门打开,无故障")]
        Open,
        [Description("门关闭,无故障")]
        Close
    }

    public enum GalleryDoor
    {
        Normal,
        Open,
        Close
    }

    public enum EmergncyTalk
    {
        Normal,
        [Description("紧急对讲故障")]
        Falut,
        [Description("请求对话")]
        FareActive,
        [Description("通话当中")]
        DriverActive,
        [Description("待机状态")]
        UnActive
    }

    public enum Brake
    {
        Normal,
        [Description("停放制动施加")]
        Park,
        [Description("停放制动缓解")]
        ParkRemit,
        [Description("制动切除")]
        Cut,
        [Description("制动严重故障")]
        SeverityFault,
        [Description("制动中等故障")]
        MediumFaulr,
        [Description("制动施加")]
        BrakeInfliction,
        [Description("制动缓解")]
        BarkeRemit
    }

    public enum Traction
    {
        Normal,
        [Description("牵引严重故障")]
        Fault,
        [Description("牵引中等故障")]
        Warn,
        [Description("正常运行")]
        Active,
        [Description("待机状态")]
        Off,
    }

    public enum Smoke
    {
        Normal,
        [Description("火灾模式")]
        Smoke,
        [Description("故障模式")]
        Fault,
        [Description("待机状态")]
        NoSmoke
    }

    public enum AirPump
    {
        Normal,
        [Description("空压机严重故障")]
        Fault,
        [Description("空压机中等故障")]
        Warn,
        [Description("空压机正常运行")]
        Run,
        [Description("待机状态")]
        Off
    }

    public enum HighSpeed
    {
        Normal,
        [Description("合模式")]
        Close,
        [Description("分模式")]
        Open
    }

    public enum Pantograph
    {
        Normal,
        [Description("库用插头连接且供电")]
        ConnectPower,
        [Description("库用插头连接未供电")]
        ConnectUnPower,
        [Description("升起故障")]
        PantographUpFault,
        [Description("降弓故障")]
        PantographDownFault,
        [Description("升弓")]
        PantographUp,
        [Description("落弓")]
        PantographDown,
        [Description("隔离")]
        PantographSeparate,
        [Description("升弓无网压")]
        PantographNetPressure,
    }
}