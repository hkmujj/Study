using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Urban.Wuxi.TMS.黑屏
{
    /// <summary>
    /// 课程结束黑屏
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class BlackScreenClassOver : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "课程结束黑屏";
        }
        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }
        #endregion

        public override void paint(Graphics dcGs)
        {
            
        }

       
    }

}
