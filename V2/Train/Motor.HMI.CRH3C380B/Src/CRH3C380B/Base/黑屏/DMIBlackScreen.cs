using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH3C380B.Base.自动速度控制;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBlackScreen : CRH3C380BBase
    {
        private ICourseService m_CourseService;

        //2
        public override string GetInfo()
        {
            return "DMI黑屏视图";
        }

        //6
        public override bool Initalize()
        {
            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();

            SetValueWhenDebug();

            return true;
        }

        private void SetValueWhenDebug()
        {
            append_postCmd(CmdType.SetInBoolValue, GetInBoolIndex(InBoolKeys.Inb制动屏黑屏), 1, 0);
            append_postCmd(CmdType.SetInBoolValue, GetInBoolIndex(InBoolKeys.Inb非制动屏黑屏), 1, 0);

            // 故障
            foreach (var it in Enumerable.Range(1400, 1))
            {
                append_postCmd(CmdType.SetInBoolValue, it, 1, 0);
            }

            append_postCmd(CmdType.SetInBoolValue,
                IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb停放制动施加2], 1, 0);
        }

        public override void Paint(Graphics g)
        {
            var flag = IsLeftScreen ? GetInBoolAt(InBoolKeys.Inb非制动屏黑屏) : GetInBoolAt(InBoolKeys.Inb制动屏黑屏);

            if (flag && m_CourseService.CurrentCourseState != CourseState.Stoped)
            {
                DMIASC.CurrentSelectdBtnType = BottomBtnType.Btn01;
                append_postCmd(CmdType.ChangePage, 3, 0, 0); //黑屏点亮
            }
        }
    }
}