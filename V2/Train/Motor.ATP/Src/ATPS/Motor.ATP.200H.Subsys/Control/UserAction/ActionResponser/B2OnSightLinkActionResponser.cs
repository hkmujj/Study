using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class B2OnSightLinkActionResponser : B2OrdinaryActionResponser
    {
        public B2OnSightLinkActionResponser(IDriverSelectableItem item) : base(item)
        {
        }

        /// <summary>
        /// ��Ӧ��������DriverActionResponserBase �ṩ���������¼������ڵ�������Ӧ
        /// </summary>
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKeys.Root_ģʽȷ��ȡ��Ŀ��);
        }
    }
}