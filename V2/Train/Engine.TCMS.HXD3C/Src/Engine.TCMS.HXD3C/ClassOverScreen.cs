using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ClassOverScreen : baseClass
    {
        public override string GetInfo()
        {
            return "¿Î³Ì½áÊø";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            return true;
        }
    }
}