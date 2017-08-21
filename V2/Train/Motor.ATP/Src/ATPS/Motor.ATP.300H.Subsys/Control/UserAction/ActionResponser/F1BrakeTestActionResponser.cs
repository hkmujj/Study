using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F1BrakeTestActionResponser : F1OrdinaryActionResponser
    {
        public F1BrakeTestActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        /// <summary>
        /// ��Ӧ��������DriverActionResponserBase �ṩ���������¼������ڵ�������Ӧ
        /// </summary>
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_�ƶ�����ѡ��));
        }
    }
}