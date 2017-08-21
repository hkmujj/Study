using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.HVAC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Standby : HMIBase
    {
        public override string GetInfo()
        {
            return "´ý»ú";
        }
    }
}