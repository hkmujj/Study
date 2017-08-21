using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class OperationalData : HMIBase
    {
        private DataGrid m_DataGrid;
        private List<string> m_IndexName;

        public override string GetInfo()
        {
            return "OperationalData";
        }

        protected override bool Initalize()
        {
            m_IndexName = new List<string>();
            for (int i = 0; i < 12; i++)
            {
                m_IndexName.Add(GlobleParam.ResManagerText.GetString("Text" + (i + 105).ToString("0000")));
            }
            InitDataGrid();
            return true;
        }

        private void Getvalues()
        {
            try
            {
                for (int i = 0; i < 12; i++)

                {
                    var s = string.Format("{0}\t{1}",
                        FloatList[Convert.ToInt32(GlobleParam.Instance.FindInFloatIndex(m_IndexName[i]))].ToString("F0"),
                        GlobleParam.ResManagerText.GetString("Text" + (i + 117).ToString("0000")));
                    m_DataGrid.DataSource.Rows[i][1] = s;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void InitDataGrid()
        {
            var datasouce = new DataTable();
            datasouce.Columns.Add("Component/System", typeof(string));
            datasouce.Columns.Add("Status", typeof(string));
            m_DataGrid = new DataGrid(datasouce)
            {
                Location = new Point(1, 70),
                IsRowHeadVisiable = true,
                RowHeadWidth = 40,
                IsShowRowHeadNum = true,
                MaxRowCount = 12,
                RowHeight = 30
            };
            m_DataGrid.ColumnWidth.Add(404);
            m_DataGrid.ColumnWidth.Add(395);
            for (int i = 0; i < 12; i++)
            {
                var dr = m_DataGrid.DataSource.NewRow();
                dr[0] = GlobleParam.ResManagerText.GetString("Text" + (i + 105).ToString("0000"));
                dr[1] = "0".PadRight(20, ' ') + GlobleParam.ResManagerText.GetString("Text" + (i + 117).ToString("0000"));
                m_DataGrid.DataSource.Rows.Add(dr);
            }
            m_DataGrid.Init();
        }

        public override void paint(Graphics g)
        {
            Getvalues();
            m_DataGrid.OnDraw(g);
        }
    }
}