using CommonUtil.Controls;

namespace CRH2MMI.Common.Control
{
    /// <summary>
    /// TableControl 的元素
    /// </summary>
    class MMITableControlItem : CommonInnerControlBase
    {
        ///// <summary>
        ///// 是否被选中变化
        ///// </summary>
        //public EventHandler SelectedChanged;

        /// <summary>
        /// 导航
        /// </summary>
        public IInnerControl Navigation { set; get; }

        /// <summary>
        ///  是否被选中
        /// </summary>
        public bool IsSelected { set; get; }

        /// <summary>
        /// 内容 
        /// </summary>
        public IInnerControl Content { set; get; }

    }
}
