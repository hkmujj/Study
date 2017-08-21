using Microsoft.Practices.Prism.ViewModel;
using Subway.WuHanLine6.Interfaces;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// Model 基类
    /// </summary>
    public class ModelBase : NotificationObject, IModels
    {
        /// <summary>
        /// 数据清除方法
        /// </summary>
        public virtual void Clear()
        {
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public virtual void Initialize()
        {
        }
    }
}