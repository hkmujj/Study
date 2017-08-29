using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380D.Interfaces;

namespace Motor.HMI.CRH380D.Models
{
    /// <summary>
    /// Model 基类
    /// </summary>
    public class ModelBase :NotificationObject, IModels
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