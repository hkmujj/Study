using System.Diagnostics;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Motor.HMI.CRH1A.Common
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class LuminanceConctrol : CRH1BaseClass
    {
        public override bool Initialize()
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.FillRegion(new SolidBrush(Color.FromArgb(255-(155+(int)GT_Settings.Valuef),255, 255, 255)), dcGs.Clip);
        }
    }
}