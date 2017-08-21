using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.HMI.CRH5A.Resource;
using Motor.HMI.CRH5A.底层共用;

namespace Motor.HMI.CRH5A.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BlackScreen : CRH5ABase
    {
        private StartView m_StartView;

        //2
        public override string GetInfo()
        {
            return "黑屏视图";
        }

        //6
        public override bool Initalize()
        {
            m_StartView = new StartView() { OutLineRectangle = new Rectangle(0, 0, 800, 600) };
            m_StartView.StartCompleted += () => append_postCmd(CmdType.ChangePage, ScreenIdentification == ScreenIdentification.ScreenTs ? 21 : 1, 0, 0);
            DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += (sender, args) =>
            {
                if (args.CourseService.CurrentCourseState == CourseState.Stoped)
                {
                    m_StartView.Stop();
                }
            };
            InitEditData();
            return true;
        }

        private void InitEditData()
        {
            append_postCmd(CmdType.SetInBoolValue, GetInBoolIndex(InBoolKeys.InBTS黑屏478), 1, 0);
            append_postCmd(CmdType.SetInBoolValue, GetInBoolIndex(InBoolKeys.InBTD黑屏479), 1, 0);
            append_postCmd(CmdType.SetInBoolValue, GetInBoolIndex(InBoolKeys.InB重联标志481), 1, 0);
        }
        public override void Paint(Graphics g)
        {
            var value = ScreenIdentification == ScreenIdentification.ScreenTd
                ? GetInBoolValue(InBoolKeys.InBTD黑屏479)
                : GetInBoolValue(InBoolKeys.InBTS黑屏478);

            if (!value)
            {
                m_StartView.Stop();
            }
            m_StartView.OnDraw(g);
            if (value)
            {
                if (m_StartView.Stopped)
                {
                    m_StartView.Start();
                }
                // 任意按键
                if (ButtonsScreen.BtnState != null)
                {
                    m_StartView.Skip();
                }
            }
        }
    }
}