using Microsoft.Practices.Prism.Regions;

namespace Engine._6A.Interface
{
    public interface IButtonResponse : INavigationAware
    {
        /// <summary>
        /// 在当前视图
        /// </summary>
        bool IsCurrent { get; set; }
    }

}