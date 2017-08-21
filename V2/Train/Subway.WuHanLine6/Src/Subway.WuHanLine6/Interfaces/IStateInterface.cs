using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Subway.WuHanLine6.Events;
using Subway.WuHanLine6.Models.BtnModel;

namespace Subway.WuHanLine6.Interfaces
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IStateInterface : INotifyPropertyChanged
    {
        /// <summary>
        /// 标题名称
        /// </summary>
        string TitleName { get; set; }

        /// <summary>
        /// 当前视图
        /// </summary>
        string CurrentView { get; set; }

        /// <summary>
        /// 按钮01
        /// </summary>
        BtnItem Btn01 { get; set; }

        /// <summary>
        /// 按钮02
        /// </summary>
        BtnItem Btn02 { get; set; }

        /// <summary>
        /// 按钮03
        /// </summary>
        BtnItem Btn03 { get; set; }

        /// <summary>
        /// 按钮04
        /// </summary>
        BtnItem Btn04 { get; set; }

        /// <summary>
        /// 按钮05
        /// </summary>
        BtnItem Btn05 { get; set; }

        /// <summary>
        /// 按钮06
        /// </summary>
        BtnItem Btn06 { get; set; }

        /// <summary>
        /// 按钮07
        /// </summary>
        BtnItem Btn07 { get; set; }

        /// <summary>
        /// 按钮08
        /// </summary>
        BtnItem Btn08 { get; set; }

        /// <summary>
        /// 按钮09
        /// </summary>
        BtnItem Btn09 { get; set; }

        /// <summary>
        /// 按钮10
        /// </summary>
        BtnItem Btn10 { get; set; }

        /// <summary>
        /// 按钮11
        /// </summary>
        BtnItem Btn11 { get; set; }

        /// <summary>
        /// 按钮12
        /// </summary>
        BtnItem Btn12 { get; set; }

        /// <summary>
        /// 更新视图
        /// </summary>
        void UpdateState();
    }

    /// <summary>
    ///
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateInterface : NotificationObject, IStateInterface
    {
        private string m_TitleName;

        /// <summary>
        /// 标题名称
        /// </summary>
        public string TitleName
        {
            get { return m_TitleName; }
            set
            {
                if (value == m_TitleName)
                {
                    return;
                }
                m_TitleName = value;
                RaisePropertyChanged(() => TitleName);
            }
        }

        /// <summary>
        /// 当前视图
        /// </summary>
        public string CurrentView { get; set; }

        /// <summary>
        /// 按钮01
        /// </summary>
        public BtnItem Btn01 { get; set; }

        /// <summary>
        /// 按钮02
        /// </summary>
        public BtnItem Btn02 { get; set; }

        /// <summary>
        /// 按钮03
        /// </summary>
        public BtnItem Btn03 { get; set; }

        /// <summary>
        /// 按钮04
        /// </summary>
        public BtnItem Btn04 { get; set; }

        /// <summary>
        /// 按钮05
        /// </summary>
        public BtnItem Btn05 { get; set; }

        /// <summary>
        /// 按钮06
        /// </summary>
        public BtnItem Btn06 { get; set; }

        /// <summary>
        /// 按钮07
        /// </summary>
        public BtnItem Btn07 { get; set; }

        /// <summary>
        /// 按钮08
        /// </summary>
        public BtnItem Btn08 { get; set; }

        /// <summary>
        /// 按钮09
        /// </summary>
        public BtnItem Btn09 { get; set; }

        /// <summary>
        /// 按钮10
        /// </summary>
        public BtnItem Btn10 { get; set; }

        /// <summary>
        /// 按钮11
        /// </summary>
        public BtnItem Btn11 { get; set; }

        /// <summary>
        /// 按钮12
        /// </summary>
        public BtnItem Btn12 { get; set; }

        /// <summary>
        /// 更新视图
        /// </summary>
        public void UpdateState()
        {
            EventAggregator.GetEvent<NavigatorEvent>().Publish(CurrentView);
        }

        /// <summary>
        /// 事件聚合器
        /// </summary>
        [Import]
        public IEventAggregator EventAggregator { get; private set; }
    }
}