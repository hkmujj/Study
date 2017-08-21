using Microsoft.Practices.Prism.Regions;
using Subway.XiaMenLine1.Interface.Model;

namespace Subway.XiaMenLine1.Interface
{
    public interface IContentPage : INavigationAware
    {
        IMMI MMI { get; }
        bool IsFouce { get; }
    }
}