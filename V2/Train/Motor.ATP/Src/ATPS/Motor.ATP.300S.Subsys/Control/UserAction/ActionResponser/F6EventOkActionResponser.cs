using System.Linq;
using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F6EventOkActionResponser : F6OrdinaryActionResponser
    {
        private readonly IInfomationService m_InfomationService;

        public F6EventOkActionResponser(IDriverSelectableItem item)
            : base(item)
        {
            m_InfomationService = App.Current.ServiceManager.GetService<IInfomationService>(ATPType.ATP300S.ToString());
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.GotoRoot();
            if (m_InfomationService.CurrentEnsureingInfomation != null)
            {
                ATP.SendInterface.EnsureMessage(new SendModel<IInfomationItem>(m_InfomationService.EnsureCurrentInfomation()));
            }
        }
    }
}