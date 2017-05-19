using Microsoft.Practices.Prism.Regions;
using Subway.ShiJiaZhuangLine1.Interface.Model;

namespace Subway.ShiJiaZhuangLine1.Interface
{
    public interface IContentPage : INavigationAware
    {
        IMMI MMI { get; }

    }
}