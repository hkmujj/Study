using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F1WatchDogTestActionResponser : F1OrdinaryActionResponser
    {
        public F1WatchDogTestActionResponser(IDriverSelectableItem item) : base(item)
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