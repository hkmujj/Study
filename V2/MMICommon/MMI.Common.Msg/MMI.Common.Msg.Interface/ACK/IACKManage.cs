namespace MMI.Common.Msg.Interface.ACK
{

    public interface IACKManage : IManage<int, IACKDetail>
    {
        IACKDetail GetCurrentInfo();
        bool HasInfo(int logic);
    }
}