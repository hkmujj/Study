using System;
using System.Drawing;
using CRH2MMI.Common.Models;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;

namespace CRH2MMI.Common.Util
{
    class GDIGridLineHelper
    {
        /// <summary>
        /// 根据配置产生一个gridline
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static GridLine CreateGridLine(GridConfig config)
        {
            var gl = new GridLine(new Point(config.AbsX, config.AbsY), new Size(config.Width, config.Height),
                config.RowCount - 1,
                config.ColumnCount - 1)
            {
                LineVisible = config.LineVisible
            };

            foreach (var row in config.Rows)
            {
                foreach (var column in row.ColumnConfigs)
                {
                    gl.AddIntersectionItem(row.RowIndex, column.ColumIndex, row.ItemType);
                }
            }

            return gl;
        }

        /// <summary>
        /// 根据配置产生一个gridline
        /// </summary>
        /// <param name="config"></param>
        /// <param name="action">对所在的行列的数据进行初始化</param>
        /// <returns></returns>
        public static GridLine CreateGridLine(GridConfig config,
            Action<GridLine, GridColumnConfig, GridItemBase> action)
        {
            var gl = new GridLine(new Point(config.AbsX, config.AbsY), new Size(config.Width, config.Height), config.RowCount - 1,
                config.ColumnCount - 1) { LineVisible = config.LineVisible };

            foreach (var row in config.Rows)
            {
                foreach (var column in row.ColumnConfigs)
                {
                    var item = gl.AddIntersectionItem(row.RowIndex, column.ColumIndex, row.ItemType);
                    if (action != null)
                    {
                        action(gl, column, item);
                    }
                }
            }

            return gl;
        }
    }
}
