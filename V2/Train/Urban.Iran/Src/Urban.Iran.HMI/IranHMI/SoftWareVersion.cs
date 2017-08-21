using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class SoftWareVersion : HMIBase
    {
        private DataGrid m_DataGrid;
        private List<SoftVersion> m_SvList;

        protected override bool Initalize()
        {
            m_SvList = new List<SoftVersion>();
    
            var sv = new SoftVersion
            {
                m_Unit = "HMI_Tc1",
                m_Type = "Applicaiton",
                m_Version = "HB1 LTAAQ1 0.0.0.1",
                m_DataOfIssue = DateTime.Now
            };
            m_SvList.Add(sv);

            sv.m_Unit = "LN(HMIVr";
            sv.m_Type = "List";
            sv.m_Version = "HB1 TDS text 2.3.1.0";
            sv.m_DataOfIssue = DateTime.Now;
            m_SvList.Add(sv);

            sv.m_Unit = "HMI_Tc1";
            sv.m_Type = "List";
            sv.m_Version = "TDS text: 2.3.1.0";
            sv.m_DataOfIssue = DateTime.Now;
            m_SvList.Add(sv);

            sv.m_Unit = "HMI_Tc1";
            sv.m_Type = "Runtime";
            sv.m_Version = "Off-Target Application: 1.16";
            sv.m_DataOfIssue = DateTime.Now;
            m_SvList.Add(sv);

            sv.m_Unit = "HMI_Tc1";
            sv.m_Type = "Framework";
            sv.m_Version = "Qt-4.6.3-410wrl";
            sv.m_DataOfIssue = DateTime.Now;
            m_SvList.Add(sv);

            var dt = new DataTable();
            dt.Columns.Add(GlobleParam.ResManager.GetString("String128"), typeof(string));
            dt.Columns.Add(GlobleParam.ResManager.GetString("String129"), typeof(string));
            dt.Columns.Add(GlobleParam.ResManager.GetString("String130"), typeof(string));
            dt.Columns.Add(GlobleParam.ResManager.GetString("String131"), typeof(string));
            foreach (var soft in m_SvList)
            {
                var dr = dt.NewRow();
                dr[0] = soft.m_Unit;
                dr[1] = soft.m_Type;
                dr[2] = soft.m_Version;
                dr[3] = soft.m_DataOfIssue.ToString();
                dt.Rows.Add(dr);
            }

            m_DataGrid = new DataGrid(dt) { RowHeadHeight = 30, RowHeadWidth = 30, IsRowHeadVisiable = true, IsShowRowHeadNum = true, Location = new Point(0, 135) };
            m_DataGrid.ColumnWidth.Add(130);
            m_DataGrid.ColumnWidth.Add(150);
            m_DataGrid.ColumnWidth.Add(270);
            m_DataGrid.ColumnWidth.Add(318);

            m_DataGrid.Init();

            return true;
        }

        public override string GetInfo()
        {
            return "SoftWareVersion";
        }

        public override void paint(Graphics g)
        {
            m_DataGrid.OnDraw(g);
        }
    }
}