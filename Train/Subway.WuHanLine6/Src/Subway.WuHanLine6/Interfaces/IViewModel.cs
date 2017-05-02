using Microsoft.Practices.Prism.Regions;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.Interfaces
{
    /// <summary>
    /// ViewMdoel 接口
    /// </summary>
    public interface IViewModel : INavigationAware
    {
        /// <summary>
        /// Model
        /// </summary>
        WuHanModel Model { get; }
    }
}