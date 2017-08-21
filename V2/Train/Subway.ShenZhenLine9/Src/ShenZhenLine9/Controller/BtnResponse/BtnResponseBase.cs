using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Controller.BtnResponse
{
    /// <summary>
    /// 
    /// </summary>
    public class BtnResponseBase : IBtnResponse
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BtnResponseBase()
        {
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        /// <summary>
        /// 事件聚合器
        /// </summary>
        protected IEventAggregator EventAggregator { get; }
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public virtual void MouseUp()
        {
            Response(string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        protected virtual void Response(string key)
        {
            EventAggregator.GetEvent<NavigatorToKeyEvent>().Publish(key);
        }
        /// <summary>
        /// 按钮按下
        /// </summary>
        public void MouseDown()
        {

        }
    }
}