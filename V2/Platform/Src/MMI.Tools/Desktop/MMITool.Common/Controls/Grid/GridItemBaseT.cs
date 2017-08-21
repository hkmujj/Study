using System.Drawing;

namespace MMITool.Common.Controls.Grid
{
    ///// <summary>
    ///// 网格线上的单元基类
    ///// </summary>
    //[DebuggerDisplay("RowIndex = {RowIndex}, ColumnIndex={ColumnIndex},ItemType = {ItemType}")]
    //public class GridLineItemBase : CommonInnerControlBase
    //{
    //    private int m_RowIndex;

    //    /// <summary>
    //    /// RowIndex
    //    /// </summary>
    //    public EventHandler RowIndexChanged;
    //    /// <summary>
    //    /// ColumnIndex
    //    /// </summary>
    //    public EventHandler ColumnIndexChanged;

    //    private int m_ColumnIndex;

    //    /// <summary>
    //    /// 当outline变化时内部控件是否能自动改变大小 
    //    /// </summary>
    //    public bool CanContentAutoSize { set; get; }

    //    /// <summary>
    //    /// 内部元素的大小 
    //    /// </summary>
    //    public virtual Size ContentSize { set; get; }

    //    /// <summary>
    //    /// ItemType
    //    /// </summary>
    //    public GridLineItemType ItemType { protected set; get; }

    //    /// <summary>
    //    /// 行索引
    //    /// </summary>
    //    public int RowIndex
    //    {
    //        set
    //        {
    //            m_RowIndex = value; 
    //            HandleUtil.OnHandle(RowIndexChanged,this, null);
    //        }
    //        get { return m_RowIndex; }
    //    }

    //    /// <summary>
    //    /// 列索引
    //    /// </summary>
    //    public int ColumnIndex
    //    {
    //        set
    //        {
    //            m_ColumnIndex = value;
    //            HandleUtil.OnHandle(ColumnIndexChanged, this, null);
    //        }
    //        get { return m_ColumnIndex; }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    protected GridLineItemBase()
    //    {
    //        CanContentAutoSize = true;
    //    }

    //}

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GridItemBaseT<T> : GridItemBase where T : CommonInnerControlBase
    {
        /// <summary>
        /// m_InnerContrl
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected T m_InnerContrl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_InnerContrl.OnDraw(g);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            m_InnerContrl.Refresh();

            m_InnerContrl.OnDraw(g);
        }

        /// <summary>
        /// 获取内部的控件
        /// </summary>
        /// <returns></returns>
        public T GetInnerContrl()
        {
            return m_InnerContrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point point)
        {
            return OutLineRectangle.Contains(point);
        }
    }
}
