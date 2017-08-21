using System.Collections.ObjectModel;
using System.Linq;
using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
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
        [HelpDescription("门开门故障")]
        OpenFault,
        [HelpDescription("门关门故障")]
        CloseFault,
        /// <summary>
        /// 检测到障碍
        /// </summary>
        [HelpDescription("门关门障碍检测")]
        CloseCheck,
        [HelpDescription("门开门障碍检测")]
        OpenCheck,
        Flick,

        [HelpDescription("门关好")]
        Closed,


        DoorFlick,
        DoorNormal,

        [HelpDescription("门打开")]
        Opened,
        [HelpDescription("门通讯故障")]
        CommunicationFault,

        [HelpDescription("门状态未知")]
        StateUnkown,
    }

    public static class DoorStatusHelper
    {
        public static readonly ReadOnlyCollection<DoorStatus> AllDoorStatus =
            new ReadOnlyCollection<DoorStatus>(typeof(DoorStatus).GetEnumValues().Cast<DoorStatus>().ToArray());
    }
}