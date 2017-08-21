using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Model;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class PIS : HMIBase
    {
        private static List<GDIButton> m_BtnArr;
        private DataGrid m_DataGrid;
        private GDIButton m_LegendBtn;
        private Image[] m_Img;
        private static readonly Color StationSelectedColor = GdiCommon.OrangePen.Color;

        private static List<PISStationSelectUnit> m_StationSelectUnits;

        private static PISStationSelectUnit m_CurrentSelectedUnit;

        private static List<GDIButton> m_ArrowBtns;

        private GDIButton CurrntButton
        {
            set
            {
                if (m_CurrntButton == value)
                {
                    return;
                }
                m_CurrntButton = value;
                m_IsSkipStation = value != null && value == m_BtnArr[0];
                m_BtnArr[0].BackImage = value != null && value == m_BtnArr[0] ? m_Img[0] : m_Img[1];

            }
        }

        private static GDIButton m_ConfirmBtn;
        private static bool m_IsSkipStation;
        private static List<GDIButton> m_BroadcastBtns;
        private static GDIButton m_CurrntButton;
        public static bool isWake = true;
        public override string GetInfo()
        {
            return "PIS";
        }

        public static void ResetPis()
        {
            m_BtnArr[1].IsEnable = true;
            m_StationSelectUnits.ForEach(f => f.Visible = false);
            m_ArrowBtns.ForEach(f => f.Visible = false);
            m_ConfirmBtn.Visible = false;
            m_BtnArr[2].IsEnable = false;
            m_BtnArr[1].TextColor = GdiCommon.MediumGreyBrush.Color;
            m_BtnArr[2].TextColor = GdiCommon.MediumGreyBrush.Color;
        }


        protected override bool Initalize()
        {
            m_CurrentSelectedUnit = null;
            m_Img = new Image[2];
            m_Img[0] = Image.FromFile(Path.Combine(RecPath, "frame\\btnWarning.jpg"));
            m_Img[1] = Image.FromFile(Path.Combine(RecPath, "frame\\btnBkNormal.jpg"));
            m_BtnArr = new List<GDIButton>
            {
                ButtonFactory.CreateShadowButton(new Rectangle(361, 402, 97, 62),
                    GlobleParam.ResManager.GetString("String252")),
                ButtonFactory.CreateShadowButton(new Rectangle(601, 402, 97, 62),
                    GlobleParam.ResManager.GetString("String257") ),
                ButtonFactory.CreateShadowButton(GlobleRect.m_HlepRect, GlobleParam.ResManager.GetString("String115")),

            };
            m_BtnArr[0].ButtonDownEvent += (sender, args) =>
            {
                append_postCmd(CmdType.SetBoolValue, GlobleParam.Instance.OutBoolDictionary["Skip Station"], 1, 0);
            }; //越站
            m_BtnArr[0].ButtonUpEvent += (sender, args) =>
            {
                append_postCmd(CmdType.SetBoolValue, GlobleParam.Instance.OutBoolDictionary["Skip Station"], 0, 0);
            }; //越站

            m_BtnArr[1].ButtonDownEvent += (sender, args) =>
                {
                    append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.AutoBroadcastFlg, 0, 0);
                    m_BtnArr[1].IsEnable = false;
                    m_BtnArr[2].IsEnable = true;
                    m_StationSelectUnits.ForEach(f => f.Visible = true);
                    m_ArrowBtns.ForEach(f => f.Visible = true);
                    m_ConfirmBtn.Visible = true;

                }; //手动
                   //m_BtnArr[1].ButtonUpEvent += (sender, args) =>
                   //{
                   //    ((GDIButton)sender).IsEnable = false;
                   //    m_BtnArr[1].TextColor = GdiCommon.MediumGreyBrush.Color;
                   //    m_BtnArr[2].TextColor = GdiCommon.MediumGreyBrush.Color;
                   //}; //手动


            m_BtnArr[2].ButtonDownEvent += (sender, args) =>
                {
                    append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.AutoBroadcastFlg, 1, 0);
                    m_BtnArr[1].IsEnable = true;
                    m_BtnArr[2].IsEnable = false;
                    m_StationSelectUnits.ForEach(f => f.Visible = false);
                    m_ArrowBtns.ForEach(f => f.Visible = false);
                    m_ConfirmBtn.Visible = false;

                }; //自动
                   //m_BtnArr[2].ButtonUpEvent += (sender, args) =>
                   // {
                   //     ((GDIButton)sender).IsEnable = false;
                   //     m_BtnArr[1].TextColor = GdiCommon.MediumGreyBrush.Color;
                   //     m_BtnArr[2].TextColor = GdiCommon.MediumGreyBrush.Color;
                   // }; //自动

            m_BroadcastBtns = new List<GDIButton>()
            {
                ButtonFactory.CreateButton(new Rectangle(361, 67, 97, 62), GlobleParam.ResManager.GetString("String249")),
                ButtonFactory.CreateButton(new Rectangle(361, 134, 97, 62),
                    GlobleParam.ResManager.GetString("String250")),
                ButtonFactory.CreateButton(new Rectangle(361, 201, 97, 62),
                    GlobleParam.ResManager.GetString("String251")),
            };
            m_BroadcastBtns[0].ButtonDownEvent += (sender, args) => { append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.ArriveBroadcast, 1, 0); };
            m_BroadcastBtns[0].ButtonUpEvent += (sender, args) => { append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.ArriveBroadcast, 0, 0); };
            m_BroadcastBtns[1].ButtonDownEvent += (sender, args) => { append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.DepartBroadcast, 1, 0); };
            m_BroadcastBtns[1].ButtonUpEvent += (sender, args) => { append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.DepartBroadcast, 0, 0); };
            m_BroadcastBtns[2].ButtonDownEvent += (sender, args) => { append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.StopBroadcast, 1, 0); };
            m_BroadcastBtns[2].ButtonUpEvent += (sender, args) => { append_postCmd(CmdType.SetBoolValue, (int)SendOutboolType.StopBroadcast, 0, 0); };
            //提交
            m_ConfirmBtn = ButtonFactory.CreateButton(new Rectangle(500, 263, 110, 65),
                GlobleParam.ResManager.GetString("String256"),
                btn => btn.ButtonDownEvent = (sender, args) =>
                {
                    isWake = false;
                    if (GlobleParam.Instance.StationList.Values.Contains(m_StationSelectUnits[0].StaionName))
                    {
                        var index = GlobleParam.Instance.StationList.First(f => f.Value == m_StationSelectUnits[0].StaionName);
                        {
                            append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.StartStation, 0, index.Key);
                        }
                    }
                    if (GlobleParam.Instance.StationList.Values.Contains(m_StationSelectUnits[1].StaionName))
                    {
                        var index = GlobleParam.Instance.StationList.First(f => f.Value == m_StationSelectUnits[1].StaionName);
                        {
                            append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.NextStation, 0, index.Key);
                        }
                    }
                    if (GlobleParam.Instance.StationList.Values.Contains(m_StationSelectUnits[2].StaionName))
                    {
                        var index = GlobleParam.Instance.StationList.First(f => f.Value == m_StationSelectUnits[2].StaionName);
                        {
                            append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.EndStation, 0, index.Key);
                        }
                    }
                });
            //箭头
            m_ArrowBtns = new List<GDIButton>()
            {
                ButtonFactory.CreateButton(new Rectangle(465, 400, 68, 68), btn =>
                {
                    btn.BackImage = Images[0];
                }),
                ButtonFactory.CreateButton(new Rectangle(533, 400, 68, 68), btn =>
                {
                    btn.BackImage = Images[1];
                }),
            };
            m_ArrowBtns[0].ButtonDownEvent += (sender, args) =>
            {
                if (((GDIButton)sender).Visible)
                {
                    m_DataGrid.GotoPreviousPage();
                }
            };
            m_ArrowBtns[1].ButtonDownEvent += (sender, args) =>
            {
                if (((GDIButton)sender).Visible)
                {
                    m_DataGrid.GotoNextPage();
                }

            };
            InitalizeDataGrid();
            //说明
            m_LegendBtn = ButtonFactory.CreateButton(new Rectangle(700, 338, 102, 62),
                GlobleParam.ResManager.GetString("String32"));

            m_LegendBtn.ButtonDownEvent += (sender, pt) => ChangedPage(IranViewIndex.PISLegend);

            InitalizeSelectStationBtns();

            return true;
        }

        private void InitalizeDataGrid()
        {
            var dataSource = new DataTable();
            dataSource.Columns.Add("Code", typeof(int));
            dataSource.Columns.Add("Station Name", typeof(string));
            m_DataGrid = new DataGrid(dataSource)
            {
                Location = new Point(1, 67),
                IsRowHeadVisiable = true,
                IsDisplayBar = false
            };
            m_DataGrid.ColumnWidth.Add(85);
            m_DataGrid.ColumnWidth.Add(245);
            foreach (var kvp in GlobleParam.Instance.StationList)
            {
                var dr = m_DataGrid.DataSource.NewRow();
                dr[0] = kvp.Key;
                dr[1] = kvp.Value;
                m_DataGrid.DataSource.Rows.Add(dr);
            }
            m_DataGrid.Init();
        }

        private static void InitalizeSelectStationBtns()
        {
            var format = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            var pen = new Pen(GdiCommon.OrangePen.Color, 3);
            m_StationSelectUnits = new List<PISStationSelectUnit>()
            {
                new PISStationSelectUnit(
                    ButtonFactory.CreateButton(new Rectangle(500, 69, 110, 65),
                        GlobleParam.ResManager.GetString("String105")),
                    new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(616, 69, 180, 60),
                        NeedDarwOutline = true,
                        OutLinePen = pen,
                        Text = "",
                        TextFormat = format,
                        TextColor = GdiCommon.MediumGreyPen.Color,
                        BkColor = GdiCommon.DarkBlueBrush.Color,
                    })
                {ButtonDownEvent = SelectStationBtnDownEvent},
                new PISStationSelectUnit(
                    ButtonFactory.CreateButton(new Rectangle(500, 133, 110, 65),
                        GlobleParam.ResManager.GetString("String254")),
                    new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(616, 135, 180, 60),
                        NeedDarwOutline = true,
                        OutLinePen = pen,
                        Text = "",
                        TextFormat = format,
                        TextColor = GdiCommon.MediumGreyPen.Color,
                        BkColor = GdiCommon.DarkBlueBrush.Color,
                    }) {ButtonDownEvent = SelectStationBtnDownEvent},
                new PISStationSelectUnit(
                    ButtonFactory.CreateButton(new Rectangle(500, 198, 110, 65),
                        GlobleParam.ResManager.GetString("String255")),
                    new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(616, 201, 180, 60),
                        NeedDarwOutline = true,
                        OutLinePen = pen,
                        Text = "",
                        TextFormat = format,
                        TextColor = GdiCommon.MediumGreyPen.Color,
                        BkColor = GdiCommon.DarkBlueBrush.Color,
                    }) {ButtonDownEvent = SelectStationBtnDownEvent},
            };
        }

        private static void SelectStationBtnDownEvent(object sender, EventArgs eventArgs)
        {
            var sbtn = (PISStationSelectUnit)sender;
            if (sbtn.Visible)
            {
                SelectStationUnit(sbtn);
            }

        }
        /// <summary>
        /// 当前选中的站点类型 起始站 下一站 终点站
        /// </summary>
        /// <param name="unit"></param>
        private static void SelectStationUnit(PISStationSelectUnit unit)
        {
            m_CurrentSelectedUnit = unit;
            m_StationSelectUnits.ForEach(e => e.SetComponentColor(GdiCommon.MediumGreyPen.Color));
            if (unit != null)
            {
                unit.SetComponentColor(StationSelectedColor);
            }
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {
                SelectStationUnit(null);
                m_DataGrid.Selected();
            }
        }

        public override void paint(Graphics g)
        {
            if (GlobleParam.Instance.WorkModel != HMIWorkModel.NoActoveDrive)
            {
                m_BroadcastBtns.ForEach(e => e.OnDraw(g));

                m_ConfirmBtn.OnDraw(g);

                foreach (var btn in m_BtnArr)
                {
                    btn.OnDraw(g);
                }

                m_LegendBtn.OnDraw(g);

            }


            m_StationSelectUnits.ForEach(e => e.OnDraw(g));

            m_ArrowBtns.ForEach(e => e.OnDraw(g));


            m_DataGrid.OnDraw(g);


        }

        public override bool mouseDown(Point point)
        {
            if (m_StationSelectUnits.Any(btn => btn.OnMouseDown(point)))
            {
                m_DataGrid.Selected();
                CurrntButton = null;
                return true;
            }
            if (m_BroadcastBtns.Any(btn => btn.OnMouseDown(point)))
            {
                CurrntButton = null;
                return true;
            }
            if (m_ArrowBtns.Any(btn => btn.OnMouseDown(point)))
            {
                SelectStationUnit(null);
                CurrntButton = null;
                return true;
            }
            if (m_ConfirmBtn.OnMouseDown(point))
            {
                SelectStationUnit(null);
                CurrntButton = null;
                return true;
            }
            foreach (var t in m_BtnArr.Where(t => t.OnMouseDown(point)))
            {
                SelectStationUnit(null);
                CurrntButton = t;
                break;
            }

            if (m_DataGrid.OnMouseDown(point))
            {
                if (m_CurrentSelectedUnit != null && m_DataGrid.SelectedRow != null)
                {
                    m_CurrentSelectedUnit.StaionName = m_DataGrid.SelectedRow[1].ToString();
                }
                if (m_IsSkipStation && m_DataGrid.SelectedRow != null)
                {
                    append_postCmd(CmdType.SetFloatValue, (int)SendFloatType.SkipStation, 0, Convert.ToInt32(m_DataGrid.SelectedRow[0]));
                }
            }

            if (m_LegendBtn.OnMouseDown(point))
            {
                CurrntButton = null;
                SelectStationUnit(null);
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            if (m_StationSelectUnits.Any(btn => btn.OnMouseUp(point)))
            {
                return true;
            }
            if (m_BroadcastBtns.Any(btn => btn.OnMouseUp(point)))
            {
                return true;
            }
            if (m_ArrowBtns.Any(btn => btn.OnMouseUp(point)))
            {
                return true;
            }
            if (m_ConfirmBtn.OnMouseUp(point))
            {
                return true;
            }
            if (m_BtnArr.Any(btn => btn.OnMouseUp(point)))
            {

            }
            if (m_DataGrid.OnMouseUp(point))
            {

            }

            if (m_LegendBtn.OnMouseUp(point))
            {

            }
            return true;
        }
    }
}