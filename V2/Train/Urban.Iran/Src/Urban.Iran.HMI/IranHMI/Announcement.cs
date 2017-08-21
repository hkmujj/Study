using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Announcement : HMIBase
    {

        private DataGrid m_DataGridService;
        private DataGrid m_DataGridEnmergency;
        private Pen m_OutlinePen;
        private List<GDIButton> m_SelectButton;
        private List<CommonInnerControlBase> m_Collection;
        private Color m_BtnSelctedTextColor;
        private Color m_BtnNormalTextColor;
        public static bool m_IsEmergency;
        private int m_SelectIndex;
        protected override bool Initalize()
        {
            m_OutlinePen = new Pen(Color.FromArgb(16, 25, 36), 3f);

            m_SelectButton = new List<GDIButton>()
            {
                ButtonFactory.CreateButton( new Rectangle(28, 135, 97, 62), GlobleParam.ResManagerText.GetString("Button0008")),
                ButtonFactory.CreateButton( new Rectangle(28, 217, 97, 62), GlobleParam.ResManagerText.GetString("Button0009")),
                ButtonFactory.CreateButton(  new Rectangle(700, 313, 97, 62), GlobleParam.ResManagerText.GetString("Button0010")),
                ButtonFactory.CreateButton( new Rectangle(700, 395, 97, 62), GlobleParam.ResManagerText.GetString("Button0011")),
            };
            m_SelectButton[0].ButtonDownEvent += (sender, args) => { m_IsEmergency = false; };
            m_SelectButton[1].ButtonDownEvent += (sender, args) => { m_IsEmergency = true; };
            m_SelectButton[2].ButtonDownEvent += (sender, args) =>
            {
                append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.StopAnnouncement, 1, 0);

            };
            m_SelectButton[2].ButtonUpEvent += (sender, args) =>
            {
                append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.StopAnnouncement, 0, 0);
                append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.Announcement, 0, 0);
            };
            m_SelectButton[3].ButtonDownEvent += (sender, args) =>
            {
                append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.EmergencyFlg, m_IsEmergency ? 1 : 0, 0);
                append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.Announcement, 0, m_SelectIndex);
            };
            m_SelectButton[3].ButtonUpEvent += (sender, args) =>
            {
                //append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.Announcement, 0, 0);
            };
            m_Collection = new List<CommonInnerControlBase>
            {
                new GDIRectText()
                {
                    Text = "",
                    TextBrush = GdiCommon.MediumGreyBrush,
                    NeedDarwOutline = true,
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt12Font,
                    TextFormat = GdiCommon.CenterFormat,
                    OutLineRectangle = new Rectangle(97, 70, 600, 47),
                    OutLinePen = m_OutlinePen,
                    RefreshAction = o=>RefreshText((GDIRectText)o)
                }
            };


            m_BtnNormalTextColor = m_SelectButton.First().ContentTextControl.TextColor;
            m_BtnSelctedTextColor = GdiCommon.OrangePen.Color;
            m_SelectButton.ForEach(e => e.ButtonDownEvent += SelectBtns);

            var dataSource = new DataTable();
            dataSource.Columns.Add(GlobleParam.ResManager.GetString("String258"), typeof(int));
            dataSource.Columns.Add(GlobleParam.ResManager.GetString("String15"), typeof(string));
            var dataSource1 = new DataTable();
            dataSource1.Columns.Add(GlobleParam.ResManager.GetString("String258"), typeof(int));
            dataSource1.Columns.Add(GlobleParam.ResManager.GetString("String15"), typeof(string));
            m_DataGridEnmergency = new DataGrid(dataSource1)
            {
                RowHeight = 30,
                IsRowHeadVisiable = true,
                RowHeadWidth = 0,

                Location = new Point(150, 130),
                MaxRowCount = 10
            };
            m_DataGridService = new DataGrid(dataSource)
            {
                RowHeight = 30,
                IsRowHeadVisiable = true,
                RowHeadWidth = 0,

                Location = new Point(150, 130),
                MaxRowCount = 10
            };
            m_DataGridEnmergency.ColumnWidth.Add(85);
            m_DataGridEnmergency.ColumnWidth.Add(385);
            m_DataGridService.ColumnWidth.Add(85);
            m_DataGridService.ColumnWidth.Add(385);

            foreach (var info in GlobleParam.Instance.AnnounceInfoCollectionService)
            {
                var dr = m_DataGridService.DataSource.NewRow();
                dr[0] = info.Code;
                dr[1] = info.Msg;
                m_DataGridService.DataSource.Rows.Add(dr);
            }
            foreach (var info in GlobleParam.Instance.AnnounceInfoCollectionEmergency)
            {
                var dr = m_DataGridEnmergency.DataSource.NewRow();
                dr[0] = info.Code;
                dr[1] = info.Msg;
                m_DataGridEnmergency.DataSource.Rows.Add(dr);
            }
            m_DataGridService.Init();
            m_DataGridEnmergency.Init();
            return true;
        }

        private void RefreshText(GDIRectText item)
        {
            //TODO 显示Text相关逻辑
            return;
        }

        private void SelectBtns(object sender, EventArgs eventArgs)
        {
            var select = (GDIButton)sender;
            m_SelectButton.ForEach(e => e.TextColor = m_BtnNormalTextColor);
            select.TextColor = m_BtnSelctedTextColor;
        }


        public override string GetInfo()
        {
            return "Announcement";
        }

        public override void paint(Graphics g)
        {

            m_Collection.ForEach(e => e.OnPaint(g));

            m_SelectButton.ForEach(e => e.OnDraw(g));
            var vis = GlobleParam.Instance.WorkModel != HMIWorkModel.NoActoveDrive;
            m_SelectButton[2].Visible = vis;
            m_SelectButton[3].Visible = vis;
            if (m_IsEmergency)
            {
                m_DataGridEnmergency.OnDraw(g);
            }
            else
            {
                m_DataGridService.OnDraw(g);
            }

        }

        public override bool mouseDown(Point point)
        {
            var any = m_SelectButton.Any(a => a.OnMouseDown(point));

            if (!any)
            {
                if (!m_IsEmergency)
                {
                    any = m_DataGridService.OnMouseDown(point);
                    if (any)
                    {
                        m_SelectIndex = Convert.ToInt32(m_DataGridService.SelectedRow[0]);
                    }
                }
                else
                {
                    any = m_DataGridEnmergency.OnMouseDown(point);
                    if (any)
                    {
                        m_SelectIndex = Convert.ToInt32(m_DataGridEnmergency.SelectedRow[0]) + 8;
                    }
                }
            }

            return any;
        }

        public override bool mouseUp(Point point)
        {
            var any = m_SelectButton.Any(a => a.OnMouseUp(point));
            if (!any)
            {
                any = !m_IsEmergency ? m_DataGridService.OnMouseUp(point) : m_DataGridEnmergency.OnMouseUp(point);
            }
            return any;
        }
    }
}