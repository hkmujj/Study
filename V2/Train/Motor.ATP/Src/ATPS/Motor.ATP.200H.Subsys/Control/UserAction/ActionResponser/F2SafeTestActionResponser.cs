using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F2SafeTestActionResponser : F2OrdinaryActionResponser
    {
        public F2SafeTestActionResponser(IDriverSelectableItem item) : base(item)
        {
        }

        /// <summary>
        /// ��Ӧ��������DriverActionResponserBase �ṩ���������¼������ڵ�������Ӧ
        /// </summary>
        public override void ResponseMouseUp()
        {

        }
    }
}