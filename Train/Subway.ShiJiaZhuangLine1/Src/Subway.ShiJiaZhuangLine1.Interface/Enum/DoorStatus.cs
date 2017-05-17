using System.Collections.ObjectModel;
using System.Linq;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum DoorStatus
    {
        Null,

        NormalDisplay,
        SelectDisplay,
        CutDisplay,
        FaultDispaly,
        [HelpDescription("紧急解锁")]
        EmeregencyUnlock,

        [HelpDescription("门切除")]
        Cut,
        [HelpDescription("门故障")]
        Fault,

        /// <summary>
        /// 检测到障碍
        /// </summary>
        [HelpDescription("门检测到障碍物")]
        Check,
        Flick,

        [HelpDescription("门关闭")]
        Closed,


        DoorFlick,
        DoorNormal,

        [HelpDescription("门打开")]
        Opened,
        [HelpDescription("门通讯故障")]
        CommunicationFault,

        [HelpDescription("门状态未知")]
        StateUnkown,
        [HelpDescription("门状态闪烁")]
        Twinkle,
    }

    public static class DoorStatusHelper
    {
        public static readonly ReadOnlyCollection<DoorStatus> AllDoorStatus =
            new ReadOnlyCollection<DoorStatus>(typeof (DoorStatus).GetEnumValues().Cast<DoorStatus>().ToArray());
    }
}