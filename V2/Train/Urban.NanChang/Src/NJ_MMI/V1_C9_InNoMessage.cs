using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C9_InNoMessage : baseClass
    {
        public override string GetInfo()
        {
            return "进入无通讯";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                append_postCmd(CmdType.ChangePage, 106, 0, 0);
            }
        }
    }
}
