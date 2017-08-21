using System.Collections.ObjectModel;
using System.Linq;
using Subway.XiaMenLine1.Interface.Attibutes;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum EmergencyTalkState
    {
        Null,

        Normal,
        Select,
        Fault,

        [HelpDescription("乘客紧急通讯单元故障")]
        UnitFalt,
        [HelpDescription("乘客紧急通讯单元激活，乘客要求紧急对讲")]
        UnitRequest,
        [HelpDescription("乘客紧急通讯单元激活，司机已打开通讯通道")]
        UnitActive,
        [HelpDescription("乘客紧急通讯单元正常，未激活")]
        UnitNormal
    }

    public static class EmergencyTalkStateHelper
    {
        public static readonly ReadOnlyCollection<EmergencyTalkState> AllTalkStates = new ReadOnlyCollection<EmergencyTalkState>(typeof(EmergencyTalkState).GetEnumValues().Cast<EmergencyTalkState>().ToArray()); 
    }
}