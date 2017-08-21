using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.ATP._300T.Config;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.共用;

namespace Motor.ATP._300T.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ATPBlackScreen : ATPBaseClass
    {
        private StartView m_StartView;

        private ICourseService m_CourseService;

        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "黑屏视图";
        }

        //6
        public override bool Initalize()
        {
            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();

            m_StartView = new StartView() { ProgressImage = ComImages.进度条 };
            m_StartView.StartCompleted += () =>
            {
                append_postCmd(CmdType.ChangePage, (int)ViewConfig.MainInitalize, 0, 0);
            };//黑屏点亮

            append_postCmd(CmdType.SetInBoolValue, GetInBoolIndex(InBoolKeys.Inb黑屏), 1, 0);

            return true;
        }

        #endregion

        public override void paint(Graphics g)
        {
            if (m_CourseService.CurrentCourseState == CourseState.Stoped)
            {
                return;
            }
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub开机检测中), 1, 0);
            if (BoolList[GetInBoolIndex(InBoolKeys.Inb黑屏)])
            {
                m_StartView.OnPaint(g);
            }
            else
            {
                m_StartView.Restart();
            }
        }

        public override bool mouseDown(Point point)
        {
            m_StartView.SkipProgress();
            return true;
        }
    }
}