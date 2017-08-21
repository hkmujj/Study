using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ViewModelBase : NotificationObject, IViewModel
    {
        /// <summary>
        /// 清除运行数据
        /// </summary>
        public virtual void Clear()
        {

        }

        /// <summary>
        /// 初始化运行数据
        /// </summary>
        public virtual void Start()
        {

        }
    }
}