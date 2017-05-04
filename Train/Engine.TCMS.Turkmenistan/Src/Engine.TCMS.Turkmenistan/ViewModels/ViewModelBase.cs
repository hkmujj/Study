using Engine.TCMS.Turkmenistan.Interfaces;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.Turkmenistan.ViewModels
{
    public abstract class ViewModelBase : NotificationObject, IViewModels
    {
        /// <summary>
        /// 课程结束清除的数据
        /// </summary>
        public virtual void Clear()
        {

        }

        /// <summary>
        /// 课程开始初始化的数据
        /// </summary>
        public virtual void Initlization()
        {

        }
    }
}