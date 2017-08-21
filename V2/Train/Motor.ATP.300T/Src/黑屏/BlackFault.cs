using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.共用;

namespace Motor.ATP._300T.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BlackFault : ATPBaseClass
    {
        public override bool Initalize()
        {
            return true;
        }

        public override void paint(Graphics g)
        {
            if (GetInBoolValue(InBoolKeys.Inb黑屏故障))
            {
                g.FillRectangle(SolidBrushsItems.BlackBrush, g.ClipBounds);
            }
        }
    }
}