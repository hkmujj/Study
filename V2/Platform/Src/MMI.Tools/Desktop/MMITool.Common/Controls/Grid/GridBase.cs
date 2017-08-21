using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace MMITool.Common.Controls.Grid
{
    /// <summary>
    /// 网格的控件
    /// </summary>
    public abstract class GridBase : CommonInnerControlBase
    {
        /// <summary>
        /// 是否反转横向
        /// </summary>
        public bool IsReversalHorizontal { set; get; }

        /// <summary>
        /// 格子的列个数
        /// </summary>
        public int ColoumnCellCount { private set; get; }

        /// <summary>
        /// 格子的行个数
        /// </summary>
        public int RowCellCount { private set; get; }

        /// <summary>
        /// 内部元素的工厂
        /// </summary>
        public IGridItemFactory ItemFactory { protected set; get; }

        /// <summary>
        /// 网格线是否可见
        /// </summary>
        public bool LineVisible { set; get; }

        /// <summary>
        /// 格子的大小 
        /// </summary>
        public SizeF CellSize { private set; get; }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// 
        /// </summary>
        protected readonly GridItemBase[][] m_Children;

        /// <summary>
        /// 所有的元素, 用于绘图
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected readonly List<GridItemBase> m_AllItems;

        /// <summary>
        /// 所有的元素
        /// </summary>
        public ReadOnlyCollection<GridItemBase> AllItems { get { return m_AllItems.AsReadOnly(); } }

        /// <summary>
        /// 
        /// </summary>
        public GridItemBase this[int rowIdx, int columnIndx]
        {
            get { return m_Children[rowIdx][columnIndx]; }
            private set
            {
                m_Children[rowIdx][columnIndx] = value;
                if (value != null)
                {
                    value.Parent = this;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="coloumnCellCount">列格子的个数, 最小值默认 1</param>
        /// <param name="rowCellCount">行格子的个数, 最小值默认 1</param>
        protected GridBase(Point location, Size size, int rowCellCount, int coloumnCellCount)
        {
            ItemFactory = GridItemFactory.Instance;

            IsReversalHorizontal = false;
            Location = location;
            Size = size;
            RowCellCount = rowCellCount < 1 ? 1 : rowCellCount;
            ColoumnCellCount = coloumnCellCount < 1 ? 1 : coloumnCellCount;

            CellSize = new Size(Size.Width / ColoumnCellCount, Size.Height / RowCellCount);

            m_AllItems = new List<GridItemBase>();
            m_Children = new GridItemBase[RowCellCount + 1][];

            for (int i = 0; i < m_Children.Length; i++)
            {
                m_Children[i] = new GridItemBase[ColoumnCellCount + 1];
            }

        }

        /// <summary>
        /// 增加一个交点的内容 
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="columnIndx"></param>
        /// <param name="itemType"></param>
        public GridItemBase AddIntersectionItem(int rowIdx, int columnIndx, GridItemType itemType)
        {
            var col = IsReversalHorizontal ? ColoumnCellCount - columnIndx : columnIndx;
            var it = ItemFactory.CreateItem(itemType);
            it.RowIndex = rowIdx;
            it.ColumnIndex = col;
            it.OutLineRectangle = GetItemBound(rowIdx, columnIndx);
            this[rowIdx, col] = it;
            m_AllItems.Add(it);
            return it;
        }

        /// <summary>
        /// 增加一个交点的内容 
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="columnIndx"></param>
        /// <param name="itemBase"></param>
        public GridItemBase AddIntersectionItem(int rowIdx, int columnIndx, GridItemBase itemBase)
        {
            if (itemBase == null)
            {
                return null;
            }
            var col = IsReversalHorizontal ? ColoumnCellCount - columnIndx : columnIndx;
            itemBase.RowIndex = rowIdx;
            itemBase.ColumnIndex = col;
            itemBase.OutLineRectangle = GetItemBound(rowIdx, columnIndx);
            this[rowIdx, col] = itemBase;
            m_AllItems.Add(itemBase);
            return itemBase;
        }

        /// <summary>
        /// 删除一个交点的内容
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="columnIndx"></param>
        public void RemoveIntersectionItem(int rowIdx, int columnIndx)
        {
            var col = IsReversalHorizontal ? ColoumnCellCount - columnIndx : columnIndx;
            this[rowIdx, col] = null;
            m_AllItems.Remove(this[rowIdx, col]);
        }

        /// <summary>
        /// 获得第 rowIdx 行, colIdx列交点的坐标,如果是 GridLine则返回交点位置, 如果是GridCell则返回交点格子的左上角坐标
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public PointF GetIntersectionLocationF(int rowIdx, int colIdx)
        {
            if (rowIdx > RowCellCount || colIdx > ColoumnCellCount)
            {
                throw new IndexOutOfRangeException();
            }
            return new PointF(Location.X + colIdx * Size.Width / ColoumnCellCount, Location.Y + rowIdx * Size.Height / RowCellCount);
        }

        /// <summary>
        /// 获得 内部 元素的大小 
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public abstract RectangleF GetItemBoundF(int rowIdx, int colIdx);

        /// <summary>
        /// 获得 内部 元素的大小 
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public abstract Rectangle GetItemBound(int rowIdx, int colIdx);

        /// <summary>
        /// 获得交点的中心点位置
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public abstract Point GetIntersectionCenter(int rowIdx, int colIdx);


        /// <summary>
        /// 获得交点的中心点位置
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public abstract PointF GetIntersectionCenterF(int rowIdx, int colIdx);

        /// <summary>
        /// 获得第 rowIdx 行, colIdx列交点的坐标, 如果是 GridLine则返回交点位置, 如果是GridCell则返回交点格子的左上角坐标
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public Point GetIntersectionLocation(int rowIdx, int colIdx)
        {
            if (rowIdx > RowCellCount || colIdx > ColoumnCellCount)
            {
                throw new IndexOutOfRangeException();
            }
            return new Point(Location.X + colIdx * Size.Width / ColoumnCellCount, Location.Y + rowIdx * Size.Height / RowCellCount);
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_AllItems.ForEach(e => e.OnDraw(g));
        }

        /// <summary>
        /// 刷新并绘图, 会调用 Refresh
        /// </summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            m_AllItems.ForEach( e => e.OnPaint(g));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool OnMouseDown(Point point)
        {
            return m_AllItems.Any(a => a.OnMouseDown(point));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool OnMouseUp(Point point)
        {
            return m_AllItems.Any(a => a.OnMouseUp(point));
        }

        /// <summary>
        /// 反转自身
        /// </summary>
        public override void Reverse()
        {
            for (int i = 0; i < RowCellCount + 1; i++)
            {
                for (int j = 0; j < (ColoumnCellCount + 1) / 2; j++)
                {
                    var revIdx = ColoumnCellCount - j;
                    var data1 = m_Children[i][j];
                    var data2 = m_Children[i][revIdx];

                    if (data1 == null && data2 == null)
                    {
                        continue;
                    }
                    if (data1 != null && data2 != null)
                    {
                        var rt = m_Children[i][j].OutLineRectangle;
                        var autoChangState1 = m_Children[i][j].ContentAutoSize;
                        var autoChangState2 = m_Children[i][revIdx].ContentAutoSize;
                        m_Children[i][j].ContentAutoSize = false;
                        m_Children[i][revIdx].ContentAutoSize = false;
                        m_Children[i][j].OutLineRectangle = m_Children[i][revIdx].OutLineRectangle;
                        m_Children[i][j].ColumnIndex = revIdx;
                        m_Children[i][revIdx].OutLineRectangle = rt;
                        m_Children[i][j].Reverse();
                        m_Children[i][revIdx].ColumnIndex = j;
                        m_Children[i][revIdx].Reverse();

                        m_Children[i][j] = data2;
                        m_Children[i][revIdx] = data1;

                        m_Children[i][j].ContentAutoSize = autoChangState2;
                        m_Children[i][revIdx].ContentAutoSize = autoChangState1;
                        continue;
                    }
                    if (data1 != null && data2 == null)
                    {
                        var loc = GetItemBound(i, j);
                        var offset = new Point(data1.Location.X - loc.X, data1.Location.Y - loc.Y);
                        var newloc = GetItemBound(i, revIdx);
                        var autoChangState = m_Children[i][j].ContentAutoSize;
                        m_Children[i][j].ContentAutoSize = false;
                        m_Children[i][revIdx] = m_Children[i][j];
                        m_Children[i][j] = null;
                        m_Children[i][revIdx].ColumnIndex = revIdx;
                        m_Children[i][revIdx].OutLineRectangle =
                            new Rectangle(new Point(newloc.X + offset.X, newloc.Y + offset.Y),
                                m_Children[i][revIdx].OutLineRectangle.Size);
                        m_Children[i][revIdx].Reverse();
                        m_Children[i][revIdx].ContentAutoSize = autoChangState;
                        continue;
                    }
                    if (data1 == null && data2 != null)
                    {
                        var loc = GetItemBound(i, revIdx);
                        var offset = new Point(data2.Location.X - loc.X, data2.Location.Y - loc.Y);
                        var newloc = GetItemBound(i, j);
                        m_Children[i][j] = m_Children[i][revIdx];
                        var autoChangState = m_Children[i][j].ContentAutoSize;
                        m_Children[i][j].ContentAutoSize = false;
                        m_Children[i][revIdx] = null;
                        m_Children[i][j].ColumnIndex = j;
                        m_Children[i][j].OutLineRectangle =
                            new Rectangle(new Point(newloc.X + offset.X, newloc.Y + offset.Y),
                                m_Children[i][j].OutLineRectangle.Size);
                        m_Children[i][j].Reverse();
                        m_Children[i][j].ContentAutoSize = autoChangState;
                        continue;
                    }
                }
            }

        }

        private Point GetCenterPoint()
        {
            return new Point(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);
        }
    }
}
