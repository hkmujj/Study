namespace Subway.XiaMenLine1.Interface
{
    public interface IBoradercastMgr : IPaging<IBoradcast>, IInfo<int, IBoradcast>
    {
        void ResetPage();
    }
}
