using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using MMI.Facility.Interface.Attribute;

namespace Motor.HMI.CRH1A.黑屏
{
    /// <summary>
    /// 课程结束视图
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class ClassOverScreen : CRH1BaseClass
    {
        public override string GetInfo()
        {
            return "课程结束视图";
        }

        public override bool Initialize()
        {
            return true;
        }

        protected override void NavigateTo(ViewConfig to)
        {
        }

        public override void paint(Graphics dcGs)
        {
        }
    }
}
