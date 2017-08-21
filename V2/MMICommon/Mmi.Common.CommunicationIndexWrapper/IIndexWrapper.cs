using CommonUtil.Model;

namespace Mmi.Common.CommunicationIndexWrapper
{
    public interface IIndexWrapper
    {
        IReadOnlyDictionary<string, int> NameIndexDictionary { get; }
    }
}