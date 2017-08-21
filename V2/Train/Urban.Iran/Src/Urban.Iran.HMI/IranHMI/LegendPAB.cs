using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class LegendPAB : HMIBase
    {

        protected override bool Initalize()
        {
            return true;
        }

        public override string GetInfo()
        {
            return "PAB图例";
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = IranViewIndex.PAB;

            g.DrawImage(Images[0], HMICommon.ContentRectangle);

        }
    }
}