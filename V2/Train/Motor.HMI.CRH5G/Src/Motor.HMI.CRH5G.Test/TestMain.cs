using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5G.Test
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class CRH5GTestMain : baseClass
    {
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }


        public override void paint(Graphics g)
        {
            UIObj.InBoolList.ForEach(e => append_postCmd(CmdType.SetInBoolValue, e, 1, 1));
        }
    }
}
