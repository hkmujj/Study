namespace MMI.Common.Msg.Interface.Bordercaset
{
    public interface IBoradercastMgr : IPaging<IBoradcast>, IInfo<int, IBoradcast>
    {
        void ResetPage();
    }
}
