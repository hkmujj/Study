using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 网格控件
    /// </summary>
    [Obsolete("功能未完成")]
    public class GDIDataGrid : CommonInnerControlBase
    {
        /// <summary>
        /// 中间线的颜色
        /// </summary>
        public Pen GridPen { set; get; }

        /// <summary>
        /// 边框
        /// </summary>
        public Pen OutLinePen { set; get; }

        /// <summary>
        /// 宽度
        /// </summary>
        private readonly List<int> m_Widths;

        /// <summary>
        /// 高度
        /// </summary>
        private readonly List<int> m_Hights;



        /// <summary>
        /// 数据
        /// </summary>
        public DataTable DataTable { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public GDIDataGrid(Point location, Size size)
            : this()
        {
            OutLineRectangle = new Rectangle();
            Size = size;
            Location = location;
        }

        /// <summary>
        /// 
        /// </summary>
        public GDIDataGrid()
        {
            GridPen = new Pen(Color.FromArgb(210, 210, 210));
            OutLinePen = new Pen(Color.Black, 2);
            m_Widths = new List<int>();
            m_Hights = new List<int>();
            DataTable = new DataTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnWidth"></param>
        public void AddColumn(string columnName, int columnWidth)
        {
            DataTable.Columns.Add(columnName, typeof(ICommonInnerControl));
            m_Widths.Add(columnWidth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowHight"></param>
        public void AddRow(int rowHight)
        {
            var line = DataTable.NewRow();
            DataTable.Rows.Add(line);
            m_Hights.Add(rowHight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">从 0 开始</param>
        /// <param name="hight"></param>
        public void SetRowHight(int row, int hight)
        {
            CheckRow(row);
            var dif = hight - m_Hights[row];
            for (int i = row + 1; i < m_Hights.Count; i++)
            {
                var rd = DataTable.Rows[row];
                for (int j = 0; j < m_Widths.Count; j++)
                {
                    var ctrol = rd[j] as ICommonInnerControl;
                    if (ctrol != null)
                    {
                        ctrol.Location = new Point(ctrol.Location.X, ctrol.Location.Y + dif);
                    }
                }
            }
            var dc = DataTable.Rows[row];
            for (int i = 0; i < DataTable.Columns.Count; i++)
            {
                var ctrol = dc[i] as ICommonInnerControl;
                if (ctrol != null)
                {
                    ctrol.Size = new Size(ctrol.Size.Width, hight);
                }

            }
            m_Hights[row] = hight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column">从 0 开始</param>
        /// <param name="width"></param>
        public void SetColumWidth(int column, int width)
        {
            CheckColumn(column);
            var dif = width - m_Widths[column];
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                for (int j = column + 1; j < DataTable.Columns.Count; j++)
                {
                    var ctol = DataTable.Rows[i][j] as ICommonInnerControl;
                    if (ctol != null)
                    {
                        ctol.Location = new Point(ctol.Location.X + dif, ctol.Location.Y);
                    }
                }
            }

            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                var ctol = DataTable.Rows[i][column] as ICommonInnerControl;
                if (ctol != null)
                {
                    ctol.Size = new Size(width, ctol.Size.Height);
                }
            }
            m_Widths[column] = width;
        }

        /// <summary>
        /// 获得row 行 column 列的大小和位置
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Rectangle GetRectangleOf(int row, int column)
        {
            CheckRow(row);
            CheckColumn(column);

            var xoff = 0;
            for (int i = 0; i < row; i++)
            {
                xoff += m_Hights[i];
            }

            var yoff = 0;
            for (int i = 0; i < column; i++)
            {
                yoff += m_Widths[i];
            }

            return new Rectangle(Location.X + xoff, Location.Y + yoff, m_Widths[column], m_Hights[row]);
        }

        private void CheckColumn(int column)
        {
            if (column > m_Widths.Count)
            {
                throw new Exception(string.Format("There is only {0} columns. can not access column of {1}", m_Widths.Count,
                    column));
            }
        }

        private void CheckRow(int row)
        {
            if (row > m_Hights.Count)
            {
                throw new Exception(string.Format("There is only {0} rows. can not access row of {1}", m_Hights.Count, row));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        public void DeleteRow(int idx)
        {
            DataTable.Rows.RemoveAt(idx);
            m_Hights.RemoveAt(idx);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        public void DeleteColumn(int idx)
        {
            DataTable.Columns.RemoveAt(idx);
            m_Widths.RemoveAt(idx);
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                for (int j = 0; j < DataTable.Columns.Count; j++)
                {
                    var ctol = DataTable.Rows[i][j] as ICommonInnerControl;
                    if (ctol != null)
                    {
                        ctol.OnDraw(g);
                    }
                }
            }
        }
    }
}
