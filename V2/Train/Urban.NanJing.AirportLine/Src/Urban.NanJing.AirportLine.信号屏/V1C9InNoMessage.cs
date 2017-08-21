using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C9InNoMessage : baseClass
    {
        public override string GetInfo()
        {
            return "进入无通讯";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics g)
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                append_postCmd(CmdType.ChangePage, 106, 0, 0);
            }
        }
    }
}
