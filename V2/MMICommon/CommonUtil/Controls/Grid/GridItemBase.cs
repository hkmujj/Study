using System;
using System.Diagnostics;
using System.Drawing;
using CommonUtil.Util;

namespace CommonUtil.Controls.Grid
{
    /// <summary>
    /// grid 的元素
    /// </summary>
    [DebuggerDisplay("RowIndex = {RowIndex}, ColumnIndex={ColumnIndex},ItemType = {ItemType}")]
    public class GridItemBase : CommonInnerControlBase
    {
        /// <summary>
        /// 父容器
        /// </summary>
        public GridBase Parent { set; get; }

        private int m_RowIndex;

        /// <summary>
        /// RowIndex
        /// </summary>
        public EventHandler RowIndexChanged;
        /// <summary>
        /// ColumnIndex
        /// </summary>
        public EventHandler ColumnIndexChanged;

        private int m_ColumnIndex;

        /// <summary>
        /// 当outline变化时内部控件是否能自动改变大小 
        /// </summary>
        public bool ContentAutoSize { set; get; }

        /// <summary>
        /// 内部元素的大小 
        /// </summary>
        public virtual Size ContentSize { set; get; }

        /// <summary>
        /// ItemType
        /// </summary>
        public GridItemType ItemType { protected set; get; }

        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex
        {
            set
            {
                m_RowIndex = value;
                HandleUtil.OnHandle(RowIndexChanged, this, null);
            }
            get { return m_RowIndex; }
        }

        /// <summary>
        /// 列索引
        /// </summary>
        public int ColumnIndex
        {
            set
            {
                m_ColumnIndex = value;
                HandleUtil.OnHandle(ColumnIndexChanged, this, null);
            }
            get { return m_ColumnIndex; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected GridItemBase()
        {
            ContentAutoSize = true;
        }

        /// <summary>
        /// 获取 内部 控件的 位置偏移
        /// </summary>
        /// <returns></returns>
        protected PointF GetInnerCtolLocationOffset()
        {
            var xoffset = (1f - (float)ContentSize.Width / (float)Size.Width) * Size.Width / 2;
            var yoffset = (1f - (float)ContentSize.Height / (float)Size.Height) * Size.Height / 2;
            return new PointF(xoffset, yoffset);
        }

    }
}
