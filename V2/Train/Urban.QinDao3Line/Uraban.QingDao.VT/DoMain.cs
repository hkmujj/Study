using System.Drawing;

using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface;

namespace Urban.QingDao.VT
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DoMain : baseClass
    {
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics g)
        {

            base.paint(g);
        }
    }
}