using System.Drawing;
using System.Drawing.Imaging;
using ATP200C.MainView;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Course;
using MMI.Facility.Interface.Service;

namespace ATP200C
{
      [GksDataType(DataType.isMMIObjectClass)]
    public class ProjectSwitch : baseClass
    {
        private bool m_InCurrent = true;

        public override bool init(ref int nErrorObjectIndex)
        {
            App.Current.ServiceManager.GetService<IEventService>().CoursStarting += args =>
            {
                var str = args.StartParam as string;
                if (!string.IsNullOrEmpty(str))
                {
                    AppLog.Debug("Start param is string, value = {0} ", str);
                    JudgeInCurrent(str);
                }

                var data = args.StartParam as ICourseStartParameter;
                if (data != null)
                {
                    AppLog.Debug("Start param is ICourseStartParameter, signal flag value = {0} ", data.SignalFlag);
                    JudgeInCurrent(data.SignalFlag);
                }
            };

            return true;
        }

        private void JudgeInCurrent(string str)
        {
            var upper = str.ToUpper();
            if (string.IsNullOrWhiteSpace(str) || (!upper.Contains(UIObj.ParaList[0]) && !upper.Contains(UIObj.ParaList[1])))
            {
                return;
            }
            m_InCurrent = upper.Contains(UIObj.ParaList[0]);
        }

        public override void paint(Graphics g)
        {
            if (!m_InCurrent)
            {
                SwitchToOther();
            }
        }

        private void SwitchToOther()
        {
            var vs = App.Current.ServiceManager.GetService<IViewService>();
            vs.Deactive(ProjectName);

            vs.Active(UIObj.ParaList[1]);
        }
    }
}