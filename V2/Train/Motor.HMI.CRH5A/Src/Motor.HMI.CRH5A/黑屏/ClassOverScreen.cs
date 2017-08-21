using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH5A.底层共用;

namespace Motor.HMI.CRH5A.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ClassOverScreen : CRH5ABase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "课程结束视图";
        }

        //6
        public override bool Initalize()
        {
            return Init();
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void Paint(Graphics g)
        {
           
        }

        #endregion

        private bool Init()
        {
           
            return true;
        }
     
    }
}
