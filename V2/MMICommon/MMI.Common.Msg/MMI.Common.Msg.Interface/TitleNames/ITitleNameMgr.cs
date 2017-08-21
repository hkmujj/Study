namespace MMI.Common.Msg.Interface.TitleNames
{
    public interface ITitleNameMgr : IPaging<ITitle>, IInfo<string, ITitle>
    {
        string GetName(string key);
    }
}