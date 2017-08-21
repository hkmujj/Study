using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH3C380B.Test
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class CRH3C380BTestMain : baseClass
    {
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            UIObj.InBoolList.ForEach(e => append_postCmd(CmdType.SetInBoolValue, e, 1, 1));
        }
    }
}