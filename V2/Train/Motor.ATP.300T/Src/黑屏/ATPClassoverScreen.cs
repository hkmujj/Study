using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Motor.ATP._300T.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ATPClassoverScreen : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "课程结束视图";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        #endregion
    }
}
