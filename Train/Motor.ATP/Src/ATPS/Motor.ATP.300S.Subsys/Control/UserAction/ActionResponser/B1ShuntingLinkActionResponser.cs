using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class B1ShuntingLinkActionResponser : B1OrdinaryActionResponser
    {
        public B1ShuntingLinkActionResponser(IDriverSelectableItem item) : base(item)
        {
        }

        /// <summary>
        /// ��Ӧ��������DriverActionResponserBase �ṩ���������¼������ڵ�������Ӧ
        /// </summary>
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKeys.Root_ģʽȷ��ȡ������);
        }
    }
}