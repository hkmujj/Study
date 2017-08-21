using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V3_C0_NoMessage : baseClass
    {
        public override string GetInfo()
        {
            return "无通讯";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString(
                "无通讯......",
                new Font("Arial", 15, FontStyle.Bold),
                Brushes.White,
                new PointF(380, 300)
                );
            if (!BoolList[UIObj.InBoolList[0]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
        }
    }
}
