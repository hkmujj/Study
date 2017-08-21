using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V3C0NoMessage: baseClass
    {
        public override string GetInfo()
        {
            return "无通讯";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawString(
                "无通讯......",
                Global.m_FontArial15B,
                Brushes.White,
                new PointF(380,300)
                );
            if (BoolList[UIObj.InBoolList[0]])
            {
                append_postCmd(CmdType.ChangePage, 103, 0, 0);
            }
        }

    }
}
