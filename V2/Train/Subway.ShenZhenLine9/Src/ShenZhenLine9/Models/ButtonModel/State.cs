using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Models.ButtonModel
{
    /// <summary>
    /// 
    /// </summary>
    public class State : IState
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">Key</param>
        public State(IStateKey key)
        {
            Key = key;
        }

        /// <summary>
        /// 状态Key
        /// </summary>
        public IStateKey Key { get; }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn01 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn02 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn03 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn04 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn05 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn06 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn07 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn08 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn09 { get; set; }

        /// <summary>
        /// 按钮1
        /// </summary>
        public IBtnItem Btn10 { get; set; }

        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateSate()
        {
            var evnet = ServiceLocator.Current.GetInstance<IEventAggregator>();
            evnet.GetEvent<NavigatorEvent>().Publish(Key.View);
        }
    }
}