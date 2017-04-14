using System.Drawing;

namespace MMITool.Common.Controls.Grid.GridCell
{
    /// <summary>
    /// Gridcell
    /// </summary>
    public class GridCell : GridBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="coloumnCellCount"></param>
        /// <param name="rowCellCount"></param>
        public GridCell(Point location, Size size, int rowCellCount, int coloumnCellCount)
            : base(location, size, rowCellCount, coloumnCellCount)
        {
        }

        /// <summary>
        /// 获得 内部 元素的大小 
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public override RectangleF GetItemBoundF(int rowIdx, int colIdx)
        {
            return new RectangleF(GetIntersectionLocationF(rowIdx, colIdx), CellSize);
        }

        /// <summary>
        /// 获得 内部 元素的大小 
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public override Rectangle GetItemBound(int rowIdx, int colIdx)
        {
            return new Rectangle(GetIntersectionLocation(rowIdx, colIdx), CellSize.ToSize());
        }

        /// <summary>
        /// 获得交点的中心点位置
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public override Point GetIntersectionCenter(int rowIdx, int colIdx)
        {
            var loc = GetIntersectionLocation(rowIdx, colIdx);
            return  new Point((int) (loc.X + CellSize.Width/2), (int) (loc.Y + CellSize.Height/2));
        }

        /// <summary>
        /// 获得交点的中心点位置
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public override PointF GetIntersectionCenterF(int rowIdx, int colIdx)
        {
            var loc = GetIntersectionLocation(rowIdx, colIdx);
            return new PointF(loc.X + CellSize.Width / 2, loc.Y + CellSize.Height / 2);
        }
    }
}
