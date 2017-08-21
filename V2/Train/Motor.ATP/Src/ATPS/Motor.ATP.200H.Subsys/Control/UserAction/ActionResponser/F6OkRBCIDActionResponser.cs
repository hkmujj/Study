using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkRBCIDActionResponser : F6OkActionResponser
    {
        public F6OkRBCIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        /// <summary>��Ӧ��������DriverActionResponserBase �ṩ���������¼������ڵ�������Ӧ</summary>
        public override void ResponseMouseUp()
        {
            NotifyMouseEnvet(MouseState.MouseUp);
        }
    }
}