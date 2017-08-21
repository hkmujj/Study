using System;
using System.Drawing;

namespace MMITool.Common.Controls.Grid.GridLine
{
    /// <summary>
    /// 网格线
    /// </summary>
    public class GridLine : GridBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="coloumnCellCount">列格子的个数, 最小值默认 1</param>
        /// <param name="rowCellCount">行格子的个数, 最小值默认 1</param>
        public GridLine(Point location, Size size,int rowCellCount, int coloumnCellCount):
            base(location, size, rowCellCount, coloumnCellCount)
        {
            LinePen = new Pen(Color.White);
            LineVisible = true;
        }


        /// <summary>
        /// 画线的笔
        /// </summary>
        public Pen LinePen { set; get; }

        /// <summary>
        /// 获得 内部 元素的大小 
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public override RectangleF GetItemBoundF(int rowIdx, int colIdx)
        {
            if (rowIdx > RowCellCount || colIdx > ColoumnCellCount)
            {
                throw new IndexOutOfRangeException();
            }
            return new RectangleF(Location.X + colIdx*Size.Width/ColoumnCellCount - CellSize.Width/2,
                Location.Y + rowIdx*Size.Height/RowCellCount - CellSize.Height/2,
                CellSize.Width, CellSize.Height);
        }

        /// <summary>
        /// 获得 内部 元素的大小 
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public override Rectangle GetItemBound(int rowIdx, int colIdx)
        {
            if (rowIdx > RowCellCount || colIdx > ColoumnCellCount)
            {
                throw new IndexOutOfRangeException();
            }
            return new Rectangle(new Point((int)(Location.X + (float)(colIdx - 0.5) * (float)Size.Width / ColoumnCellCount),
                (int)(Location.Y + (float)(rowIdx - 0.5) * (float)Size.Height / (float)RowCellCount)),
                CellSize.ToSize());
        }

        /// <summary>
        /// 获得交点的中心点位置
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public override Point GetIntersectionCenter(int rowIdx, int colIdx)
        {
            return GetIntersectionLocation(rowIdx, colIdx);
        }

        /// <summary>
        /// 获得交点的中心点位置
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public override PointF GetIntersectionCenterF(int rowIdx, int colIdx)
        {
            return GetIntersectionLocationF(rowIdx, colIdx);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            DrawLines(g);

            base.OnDraw(g);
        }

        private void DrawLines(Graphics g)
        {
            if (!LineVisible)
            {
                return;
            }

            g.DrawRectangle(LinePen, OutLineRectangle);

            for (int i = 0; i < RowCellCount; i++)
            {
                int yOffset = i*Size.Height/RowCellCount;
                g.DrawLine(LinePen, Location.X, Location.Y + yOffset, OutLineRectangle.Right, OutLineRectangle.Top + yOffset);
            }

            for (int i = 0; i < ColoumnCellCount; i++)
            {
                var xOffset = i*Size.Width/ColoumnCellCount;
                g.DrawLine(LinePen, Location.X + xOffset, Location.Y, OutLineRectangle.Left + xOffset, OutLineRectangle.Bottom);
            }
        }


        /// <summary>
        /// 刷新并绘图, 会调用 Refresh
        /// </summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            DrawLines(g);

            base.OnPaint(g);
        }
    }
}
