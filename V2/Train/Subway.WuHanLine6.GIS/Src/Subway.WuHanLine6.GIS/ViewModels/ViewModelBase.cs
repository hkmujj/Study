using Microsoft.Practices.Prism.ViewModel;
using Subway.WuHanLine6.GIS.Interfaces;

namespace Subway.WuHanLine6.GIS.ViewModels
{
    public class ViewModelBase : NotificationObject, IViewModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Initlization()
        {

        }

        /// <summary>
        /// 清除
        /// </summary>
        public virtual void Clear()
        {

        }
    }
}