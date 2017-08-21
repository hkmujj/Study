using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F6RequestOkDriverIDActionResponser : F6OrdinaryActionResponser
    {
        public F6RequestOkDriverIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        /// <summary>��Ӧ��������DriverActionResponserBase �ṩ���������¼������ڵ�������Ӧ</summary>
        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_����ȷ��ȡ��˾����);
        }
    }
}