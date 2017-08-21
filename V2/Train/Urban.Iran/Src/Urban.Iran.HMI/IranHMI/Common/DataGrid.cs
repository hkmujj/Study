using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using CommonUtil.Annotations;
using CommonUtil.Controls;
using CommonUtil.Util;

namespace Urban.Iran.HMI.Common
{
    public class DataGrid : CommonInnerControlBase
    {
        /// <summary>
        /// SelectedRow �仯 
        /// </summary>
        public event Action<DataGrid> SelectedRowChanged;

        /// <summary>
        /// ��ͷ�Ƿ�ɼ�
        /// </summary>
        public bool IsColoumnHeadVisible { set; get; }


        public bool IsRowHeadVisiable { set; get; }

        /// <summary>
        /// //�Ƿ���ʾ����б��
        /// </summary>
        public bool IsShowRowHeadNum { set; get; }

        /// <summary>
        /// //�и�
        /// </summary>
        public int RowHeight { set; get; }

        /// <summary>
        /// //�п�
        /// </summary>
        public List<int> ColumnWidth { set; get; }

        /// <summary>
        /// �����ʾ����
        /// </summary>
        public int MaxRowCount { set; get; } //

        /// <summary>
        /// �����ͷ���
        /// </summary>
        public int RowHeadWidth { set; get; } //

        /// <summary>
        /// �����ͷ�߶�
        /// </summary>
        public int RowHeadHeight { set; get; } //

        /// <summary>
        /// ���������ʾ�����ݼ�
        /// </summary>
        public DataTable DataSource { private set; get; } //

        /// <summary>
        /// ѡ�е���
        /// </summary>
        public DataRow SelectedRow
        {
            private set
            {
                if (value != m_SelectedRow)
                {
                    m_SelectedRow = value;   
                    OnSelectedRowChanged(this);
                }
            }
            get { return m_SelectedRow; }
        }

        /// <summary>
        /// ��������ʼλֵ
        /// </summary>
        private float m_BarPos;

        /// <summary>
        /// �����
        /// </summary>
        private int m_GridWidth;

        /// <summary>
        /// ���߶�
        /// </summary>
        private int m_GridHeight;

        /// <summary>
        /// �����ʾ����ʼ��
        /// </summary>
        private int m_StartRow;

        /// <summary>
        /// ���������黬������
        /// </summary>
        private float m_ScorStep;

        /// <summary>
        /// ���黬������в���
        /// </summary>
        private int m_RowStep;

        /// <summary>
        /// ���������ϰ�ť��
        /// </summary>
        private Rectangle m_UpBtnRegion;
        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        public bool IsDisplayBar { get; set; }
        /// <summary>
        /// ���������°�ť��
        /// </summary>
        private Rectangle m_DownBtnRegion;

        /// <summary>
        /// ������ͼƬ
        /// </summary>
        private static Bitmap[] m_ScrollBar;

        private List<DataGridRow> m_DataGridRowCollection;
        private Pen m_GridLinePen;
        private DataRow m_SelectedRow;

        public ReadOnlyCollection<DataGridRow> DataGridRowCollection
        {
            get { return m_DataGridRowCollection.AsReadOnly(); }
        }

        public Pen GridLinePen
        {
            set
            {
                m_GridLinePen = value ?? GdiCommon.DarkGreyPen;
            }
            get { return m_GridLinePen; }
        }

        public DataGrid(DataTable dataSource)
        {
            GridLinePen = new Pen(Color.FromArgb(16, 25, 36));
            IsColoumnHeadVisible = true;
            DataSource = dataSource;
            dataSource.RowChanged += DataSourceOnRowChanged;
            ColumnWidth = new List<int>();
            IsShowRowHeadNum = false;
            RowHeight = 38;
            MaxRowCount = 10;
            RowHeadWidth = 20;
            RowHeadHeight = 28;
            m_StartRow = 0;
            m_RowStep = 1;
            if (m_ScrollBar == null)
            {
                m_ScrollBar = new[]
                {
                    new Bitmap(GlobleView.Instance.RecPath + "\\frame/scorllHBar.jpg"),
                    new Bitmap(GlobleView.Instance.RecPath + "\\frame/bar.jpg")
                };
            }
            m_BarPos = 17;

        }

        private void DataSourceOnRowChanged(object sender, DataRowChangeEventArgs dataRowChangeEventArgs)
        {
            switch (dataRowChangeEventArgs.Action)
            {
                case DataRowAction.Nothing:
                    break;
                case DataRowAction.Delete:
                    break;
                case DataRowAction.Change:
                    var row = m_DataGridRowCollection.Find(f => f.DataRow == dataRowChangeEventArgs.Row);
                    if (row != null)
                    {
                        row.RefreshTextsByDataRow();
                    }
                    break;
                case DataRowAction.Rollback:
                    break;
                case DataRowAction.Commit:
                    break;
                case DataRowAction.Add:
                    break;
                case DataRowAction.ChangeOriginal:
                    break;
                case DataRowAction.ChangeCurrentAndOriginal:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SelectedRow = null;
        }

        public override void Init()
        {
            if (!IsRowHeadVisiable)
            {
                RowHeadWidth = 0;
            }
            m_BarPos = Location.Y + 17;
            m_GridHeight = MaxRowCount * RowHeight + (IsColoumnHeadVisible ? RowHeadHeight : 0);
            m_GridWidth = RowHeadWidth;

            InitalzeDataRows();


            foreach (var ceilWidth in ColumnWidth)
            {
                m_GridWidth += ceilWidth;
            }
            m_UpBtnRegion = new Rectangle(Location.X + m_GridWidth - 17, Location.Y, 16, 16);
            m_DownBtnRegion = new Rectangle(Location.X + m_GridWidth - 17, Location.Y + m_GridHeight - 17, 16, 16);


            float scrollLen = m_GridHeight - 34 - 175;
            var pageCount = DataSource.Rows.Count / MaxRowCount;
            if (pageCount >= 1 && pageCount <= 3)
            {
                m_ScorStep = scrollLen / (DataSource.Rows.Count - MaxRowCount);
                m_RowStep = 1;
            }
            else if (pageCount > 3)
            {
                m_RowStep = MaxRowCount;
                if (DataSource.Rows.Count % MaxRowCount == 0)
                {
                    m_ScorStep = scrollLen / (pageCount - 1);
                }
                else
                {
                    m_ScorStep = scrollLen / pageCount;
                }
            }

            m_OutLineRectangle = new Rectangle(Location, new Size(m_DownBtnRegion.Right, m_DownBtnRegion.Bottom));
        }

        private void InitalzeDataRows()
        {
            if (DataSource == null)
            {
                LogMgr.Error("Can not InitalzeDataRows, the DataSource is null.");
                return;
            }

            m_DataGridRowCollection = new List<DataGridRow>();

            var rowCount = Math.Min(MaxRowCount, DataSource.Rows.Count);
            var colCount = DataSource.Columns.Count;
            var tempX = Location.X + (IsRowHeadVisiable ? RowHeadWidth : 0);
            var tempY = Location.Y + (IsColoumnHeadVisible ? RowHeadHeight : 0);
            for (int i = 0; i < rowCount; i++)
            {
                var rows = new List<GDIRectText>();
                for (int j = 0; j < colCount; j++)
                {
                    var temp = new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(tempX, tempY, ColumnWidth[j], RowHeight),
                        TextFormat = new StringFormat()
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Center,
                        }
                    };
                    temp.TextFormat.SetTabStops(0, new[] { 100f, 100f });
                    rows.Add(temp);

                    tempX += ColumnWidth[j];
                }
                tempX = Location.X + (IsRowHeadVisiable ? RowHeadWidth : 0);
                tempY += RowHeight;
                m_DataGridRowCollection.Add(new DataGridRow(rows.AsReadOnly()) { DataRow = DataSource.Rows[i] });
            }

            Selected();
        }

        public void Selected(DataRow row = null)
        {
            m_DataGridRowCollection.ForEach(e => e.IsSelected = false);
            var select = m_DataGridRowCollection.Find(f => f.DataRow == row);
            if (select != null)
            {
                select.IsSelected = true;
            }
        }

        public override bool OnMouseDown(Point point)
        {
            if (!OutLineRectangle.Contains(point))
            {
                return false;
            }

            if (m_UpBtnRegion.Contains(point) && IsDisplayBar)
            {
                GoBack();
                return true;
            }
            if (m_DownBtnRegion.Contains(point) && IsDisplayBar)
            {
                GoForward();
                return true;
            }
            foreach (var dataGridRow in m_DataGridRowCollection)
            {
                dataGridRow.OnMouseDown(point);
            }
            var selected = m_DataGridRowCollection.Find(f => f.IsSelected);
            SelectedRow = selected != null ? selected.DataRow : null;
            return SelectedRow != null;
        }

        private void GoForward()
        {
            var scrollMax = (m_RowStep == MaxRowCount) ? DataSource.Rows.Count - 1 : DataSource.Rows.Count - MaxRowCount;
            if ((m_StartRow + m_RowStep) <= scrollMax)
            {
                m_BarPos += m_ScorStep;
                m_StartRow += m_RowStep;

                RefreshDataGridRow();
            }
        }

        private void GoBack()
        {
            if (m_StartRow - m_RowStep < 0)
            {
                return;
            }
            m_BarPos -= m_ScorStep;
            m_StartRow -= m_RowStep;

            RefreshDataGridRow();
        }

        private void RefreshDataGridRow()
        {
            var endRow = (m_StartRow + MaxRowCount < DataSource.Rows.Count)
                ? m_StartRow + MaxRowCount
                : DataSource.Rows.Count;
            for (int i = m_StartRow; i < endRow; i++)
            {
                var row = DataSource.Rows[i];
                var data = m_DataGridRowCollection[i - m_StartRow];
                data.DataRow = row;
                data.IsSelected = row == SelectedRow;
            }
        }


        public override void OnDraw(Graphics g)
        {
            m_DataGridRowCollection.ForEach(e => e.OnDraw(g));

            DrawGridFrame(g);
        }

        private void DrawGridFrame(Graphics g)
        {
            var tempStart = Location;
            var tempEnd = Location;
            tempEnd.Y += m_GridHeight;

            DrawRowHeadIfNeed(g, tempStart, tempEnd);

            DrawContentGrid(g, tempStart, tempEnd);

            DrawColoumnHeadIfNeed(g);

            DrawScrollbarIfNeed(g);

        }

        private void DrawRowHeadIfNeed(Graphics g, Point tempStart, Point tempEnd)
        {
            if (IsRowHeadVisiable)
            {
                //��ͷ
                g.FillRectangle(GdiCommon.GridHeadBrush, Location.X, Location.Y, m_GridWidth, RowHeadHeight);
                g.FillRectangle(GdiCommon.GridHeadBrush, Location.X, Location.Y, RowHeadWidth, m_GridHeight);
                //tempStart.X += IsRowHeadVisiable ? RowHeadWidth : 0;
                //tempEnd.X += IsRowHeadVisiable ? RowHeadWidth : 0;
                //����
                g.DrawLine(GridLinePen, tempStart, tempEnd);
                tempStart.X += RowHeadWidth;
                tempEnd.X += RowHeadWidth;
                g.DrawLine(GridLinePen, tempStart, tempEnd);
                if (IsShowRowHeadNum)
                {
                    var tmp = new PointF(Location.X, Location.Y + RowHeadHeight);
                    var tmpSize = new SizeF(RowHeadHeight, RowHeadWidth);
                    for (int i = 0; i < MaxRowCount; i++)
                    {
                        g.DrawString((i + 1).ToString(), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, new RectangleF(tmp, tmpSize), GdiCommon.CenterFormat);
                        tmp.Y += RowHeight;
                    }
                }
            }
        }

        private void DrawContentGrid(Graphics g, Point tempStart, Point tempEnd)
        {
            g.DrawLine(GridLinePen, tempStart, tempEnd);
            tempStart.X += IsRowHeadVisiable ? RowHeadWidth : 0;
            tempEnd.X += IsRowHeadVisiable ? RowHeadWidth : 0;
            foreach (var col in ColumnWidth)
            {
                tempStart.X += col;
                tempEnd.X += col;
                g.DrawLine(GridLinePen, tempStart, tempEnd);
            }

            //����
            tempStart = Location;
            tempEnd = Location;
            tempEnd.X += m_GridWidth;
            g.DrawLine(GridLinePen, tempStart, tempEnd);

            tempStart.Y += (IsColoumnHeadVisible ? RowHeadHeight : 0);
            tempEnd.Y += (IsColoumnHeadVisible ? RowHeadHeight : 0);

            for (var index = 0; index < MaxRowCount + 1; ++index)
            {
                g.DrawLine(GridLinePen, tempStart, tempEnd);
                tempStart.Y += RowHeight;
                tempEnd.Y += RowHeight;
            }
        }

        private void DrawScrollbarIfNeed(Graphics g)
        {
            var tempX = Location.X + m_GridWidth - 17;
            if (DataSource.Rows.Count > MaxRowCount && IsDisplayBar)
            {
                g.DrawImage(m_ScrollBar[0], tempX, Location.Y, 17, m_GridHeight);
                g.DrawImage(m_ScrollBar[1], tempX, m_BarPos, 17, 175);
            }
        }

        private void DrawColoumnHeadIfNeed(Graphics g)
        {
            if (IsColoumnHeadVisible)
            {
                int tempX = Location.X + RowHeadWidth + 5;
                for (var index = 0; index < DataSource.Columns.Count; ++index)
                {
                    g.DrawString(DataSource.Columns[index].ColumnName, GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush,
                        new Point(tempX, Location.Y + 5));
                    tempX += ColumnWidth[index];
                }
            }
        }

        void RefreshData()
        {
            m_DataGridRowCollection = new List<DataGridRow>();
            var rowCount = Math.Min(MaxRowCount, DataSource.Rows.Count - m_StartRow);
            var colCount = DataSource.Columns.Count;
            var tempX = Location.X + (IsRowHeadVisiable ? RowHeadWidth : 0);
            var tempY = Location.Y + (IsColoumnHeadVisible ? RowHeadHeight : 0);
            for (int i = m_StartRow; i < m_StartRow + rowCount; i++)
            {
                var rows = new List<GDIRectText>();
                for (int j = 0; j < colCount; j++)
                {
                    var temp = new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(tempX, tempY, ColumnWidth[j], RowHeight),
                        TextFormat = new StringFormat()
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Center,
                        }
                    };
                    temp.TextFormat.SetTabStops(0, new[] { 100f, 100f });
                    rows.Add(temp);

                    tempX += ColumnWidth[j];
                }
                tempX = Location.X + (IsRowHeadVisiable ? RowHeadWidth : 0);
                tempY += RowHeight;
                m_DataGridRowCollection.Add(new DataGridRow(rows.AsReadOnly()) { DataRow = DataSource.Rows[i] });
            }

        }

        /// <summary>
        /// ����һҳ
        /// </summary>
        public void GotoFirstPage()
        {
            m_StartRow = 0;
            RefreshData();
        }

        /// <summary>
        /// �����һ��
        /// </summary>
        public void GotoLastPage()
        {
            m_StartRow = DataSource.Rows.Count / MaxRowCount * MaxRowCount;
            RefreshData();
        }

        public void GotoNextPage()
        {
            if (m_StartRow + MaxRowCount < DataSource.Rows.Count)
            {
                m_StartRow += MaxRowCount;
                RefreshData();
            }
        }

        public void GotoPreviousPage()
        {
            if (m_StartRow - MaxRowCount >= 0)
            {
                m_StartRow -= MaxRowCount;
                RefreshData();
            }
        }

        protected virtual void OnSelectedRowChanged(DataGrid obj)
        {
            Action<DataGrid> handler = SelectedRowChanged;
            if (handler != null)
            {
                handler(obj);
            }
        }
    }
}