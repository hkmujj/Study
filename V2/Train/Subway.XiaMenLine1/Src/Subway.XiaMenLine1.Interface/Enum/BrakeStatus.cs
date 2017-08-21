using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum BrakeStatus
    {
        Null,
        Normal,
        Select,
        BrakeCut,
        BrakeFault,

        [HelpDescription("停放制动施加")]
        ParkBrakeExert,
        [HelpDescription("停放制动切除")]
        ParkBrakeCut,
        [HelpDescription("停放制动缓解")]
        ParkBrakeRelieve,
        [HelpDescription("停放制动未知")]
        ParkBrakeUnkown,

        [HelpDescription("常用制动切除")]
        NormalBrakeCut,

        NormalBrakeFalut,
        [HelpDescription("常用制动施加")]
        NormalBrakeExert,
        [HelpDescription("常用制动缓解")]
        NormalBrakeRelieve,
        [HelpDescription("常用制动未知")]
        NormalBrakeUnkown,
    }

    public class BrakeStatusHelper
    {
        public static readonly ReadOnlyCollection<BrakeStatus> AllStateCollection =
            (new List<BrakeStatus>(typeof (BrakeStatus).GetEnumValues().Cast<BrakeStatus>())).AsReadOnly();
    }
}