using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;

namespace Urban.Iran.HMI.Common
{
    public class DataGridRow : CommonInnerControlBase
    {
        private DataRow m_DataRow;
        private bool m_IsSelected;
        public ReadOnlyCollection<GDIRectText> RowTexts { private set; get; }

        public DataRow DataRow
        {
            get { return m_DataRow; }
            set
            {
                if (Equals(value, m_DataRow))
                {
                    return;
                }
                m_DataRow = value;
                OnDataRowChanged();
            }
        }

        /// <summary>
        ///  是否被选中
        /// </summary>
        public bool IsSelected
        {
            set
            {
                if (value == m_IsSelected)
                {
                    return;
                }
                m_IsSelected = value;

                RefreshSelectState();
            }
            get { return m_IsSelected; }
        }

        private void RefreshSelectState()
        {
            if (m_IsSelected)
            {
                if (SelectStrategy != null)
                {
                    foreach (var row in RowTexts)
                    {
                        SelectStrategy(row);
                    }
                }
            }
            else
            {
                if (UnselecteStrategy != null)
                {
                    foreach (var row in RowTexts)
                    {
                        UnselecteStrategy(row);
                    }
                }
            }
        }

        public Action<GDIRectText> SelectStrategy { set; get; }

        public Action<GDIRectText> UnselecteStrategy { set; get; }

        public DataGridRow(ReadOnlyCollection<GDIRectText> rowTexts)
        {
            if (rowTexts == null)
            {
                LogMgr.Error("Can not create DataGridRow which in param ReadOnlyCollection<GDIRectText> is null");
                return;
            }

            RowTexts = rowTexts;
            SelectStrategy = OnSelectStrategy;
            UnselecteStrategy = OnUnselecteStrategy;
            IsSelected = false;
            RefreshSelectState();
        }

        private void OnUnselecteStrategy(GDIRectText gdiRectText)
        {
            gdiRectText.BackColorVisible = false;
            gdiRectText.TextColor = GdiCommon.MediumGreyPen.Color;
        }

        private void OnSelectStrategy(GDIRectText gdiRectText)
        {
            gdiRectText.BkColor = Color.FromArgb(49, 106, 197);
            gdiRectText.TextColor = Color.White;
            gdiRectText.BackColorVisible = true;
        }

        public void RefreshTextsByDataRow()
        {
            OnDataRowChanged();   
        }

        private void OnDataRowChanged()
        {
            if (RowTexts == null || DataRow == null)
            {
                return;
            }

            var texts = RowTexts.GetEnumerator();
            foreach (var gdiRectText in DataRow.ItemArray.Take(RowTexts.Count))
            {
                texts.MoveNext();
                texts.Current.Text = gdiRectText.ToString();
            }
        }

        public override void OnDraw(Graphics g)
        {
            foreach (var row in RowTexts)
            {
                row.OnDraw(g);
            }
        }

        public override bool OnMouseDown(Point point)
        {
            if (RowTexts.Any(a => a.OutLineRectangle.Contains(point)))
            {
                IsSelected = true;
                return true;
            }
            IsSelected = false;
            return false;
        }
    }
}