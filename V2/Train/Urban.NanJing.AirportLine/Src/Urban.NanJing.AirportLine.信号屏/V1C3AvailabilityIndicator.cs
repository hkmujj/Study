using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C3AvailabilityIndicator:baseClass
    { 
        public override string GetInfo()
        {
            return "可用性指示";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics g)
        {
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    Brush b = Brushes.Lime;
                    if (i == 1)
                        b = Brushes.Red;
                    g.DrawString("ATP",
                        Global.m_FontArial15B,
                        b,
                        new RectangleF(690, 58, 100, 48),
                        Global.m_SfCc
                        );
                    break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    Brush b = Brushes.Lime;
                    if (i == 1)
                        b = Brushes.Red;
                    g.DrawString(
                        "ATO",
                        Global.m_FontArial15B,
                        b,
                        new RectangleF(594, 58, 92, 48),
                        Global.m_SfCc
                        );
                    break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    Brush b = Brushes.Lime;
                    if (i == 1)
                        b = Brushes.Red;
                    g.DrawString(
                        "IATP",
                        Global.m_FontArial15B, 
                        b, 
                        new RectangleF(504, 58,86,48),
                        Global.m_SfCc
                        );
                    break;
                }
            }

            base.paint(g);
        }
    }
}
