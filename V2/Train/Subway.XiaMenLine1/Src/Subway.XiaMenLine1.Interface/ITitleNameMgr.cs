namespace Subway.XiaMenLine1.Interface
{
    public interface ITitleNameMgr : IPaging<ITitle>, IInfo<string, ITitle>
    {
        string GetName(string key);
    }
}