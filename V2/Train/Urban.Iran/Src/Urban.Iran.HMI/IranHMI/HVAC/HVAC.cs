using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Domain;
using Urban.Iran.HMI.Extention;

namespace Urban.Iran.HMI.HVAC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HVAC : HMIBase
    {
        private Bitmap[] m_BtnBkImg;

        private List<GDIButton> m_SetTemperatureBtns;

        private GDIButton m_NextPageBtn;

        private List<GDIButton> m_SelectableBtns;

        private GDIRectText m_TemperatureText;

        private DataGrid m_ContentGrid;

        private int m_SettedTemperature;

        private Color m_BtnSelctedTextColor;
        private Color m_BtnNormalTextColor;
        private bool m_CanSetTemperature;

        private const int MinValue = 21;
        private const int MaxValue = 29;
        public static bool isOff = false;
        public bool CanSetTemperature
        {
            set
            {
                m_CanSetTemperature = value;
                UpdateSetButtonState();
                UpdateTemperaturTextState();
            }
            get { return m_CanSetTemperature; }
        }



        private int SettedTemperature
        {
            set
            {
                if (m_SettedTemperature == value)
                {
                    return;
                }
                m_SettedTemperature = value;
                RefreshContentGridTemperature();
                RefreshSettingTemperature();
            }
            get { return m_SettedTemperature; }
        }

        private void RefreshSettingTemperature()
        {
            if (m_TemperatureText != null)
            {
                m_TemperatureText.Text = m_SettedTemperature.ToString("0℃");
            }
        }

        private void RefreshContentGridTemperature()
        {
            if (m_ContentGrid != null && m_ContentGrid.DataSource != null)
            {
                var row = m_ContentGrid.DataSource.Rows[1];
                foreach (var car in CarNameConstant.CarNameCollection)
                {
                    row[car] = m_SettedTemperature.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private void UpdateTemperaturTextState()
        {
            m_TemperatureText.IsEnable = CanSetTemperature;
        }

        private void UpdateSetButtonState()
        {
            m_SetTemperatureBtns.ForEach(e => e.IsEnable = CanSetTemperature);
        }

        public override string GetInfo()
        {
            return "HVAC";
        }

        protected override bool Initalize()
        {
            SettedTemperature = MinValue;

            m_BtnBkImg = new[]
            {
                new Bitmap(RecPath + "\\frame/btnUp.jpg"),
                new Bitmap(RecPath + "\\frame/btnDown.jpg"),
                new Bitmap(RecPath + "\\frame/edit.jpg"),
            };

            m_TemperatureText = new IranUnenableShadowText()
            {
                OutLineRectangle = new Rectangle(520, 402, 97, 62),
                BKImage = m_BtnBkImg[2],
                DrawFont = GdiCommon.Txt12Font,
                TextBrush = GdiCommon.WhiteBrush,
                TextFormat = GdiCommon.CenterFormat
            };
            RefreshSettingTemperature();

            InitalizeContentGrid();

            InitalizeBtns();

            return true;
        }

        private void InitalizeBtns()
        {
            m_SetTemperatureBtns = new List<GDIButton>()
            {
                ButtonFactory.CreateShadowButton(
                    new Rectangle(m_TemperatureText.OutLineRectangle.Left - 10 - 58, 402, 58, 62), "-",
                    btn =>
                    {
                        btn.TextControl.DrawFont = GdiCommon.Txt18Font;
                        btn.TextColor = Color.White;
                        btn.ButtonDownEvent = (sender, args) =>
                        {
                            if (SettedTemperature <= MinValue)
                            {
                                return;
                            }
                            SettedTemperature -= 2;
                        };
                        btn.ContentTextControl.DrawFont = GdiCommon.Txt22FontBold;
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(m_TemperatureText.OutLineRectangle.Right + 10, 402, 58, 62),
                    "+",
                    btn =>
                    {
                        btn.TextControl.DrawFont = GdiCommon.Txt22FontBold;
                        btn.TextColor = Color.White;
                        btn.ButtonDownEvent = (sender, args) =>
                        {
                            if (SettedTemperature >= MaxValue)
                            {
                                return;
                            }
                            SettedTemperature+=2;
                        };
                    }),


            };

            m_NextPageBtn = ButtonFactory.CreateButton(new Rectangle(701, 402, 97, 62),
                GlobleParam.ResManager.GetString("String245") + "▶",
                btn => btn.ButtonDownEvent = (sender, args) => ChangedPage(IranViewIndex.HVACPage2));

            m_SelectableBtns = new List<GDIButton>()
            {
                ButtonFactory.CreateShadowButton(new Rectangle(9, 402, 97, 62), GlobleParam.ResManager.GetString("String77"),
                    button => { button.Tag = new AutoHVACWorkState(this);
                                  button.TextColor = GdiCommon.MediumGreyBrush.Color;
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(115, 402, 97, 62), GlobleParam.ResManager.GetString("String78"),
                    btn => {
                        btn.Tag = new ManualHVACWorkState(this);
                               btn.TextColor = GdiCommon.MediumGreyBrush.Color;
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(221, 402, 97, 62), GlobleParam.ResManager.GetString("String79"),
                    btn => btn.Tag = new VentilationHVACWorkState(this)),
                ButtonFactory.CreateShadowButton(new Rectangle(326, 402, 97, 62), GlobleParam.ResManager.GetString("String80"),
                    btn => btn.Tag = new OffHVACWorkState(this)),
            };

            m_BtnNormalTextColor = m_SelectableBtns.First().ContentTextControl.TextColor;
            m_BtnSelctedTextColor = GdiCommon.OrangePen.Color;

            m_SelectableBtns.ForEach(e => e.ButtonDownEvent += SelectBtns);
            //m_SelectableBtns.ForEach(e => e.ButtonDownEvent += (sender, args) =>  ChangedPage(IranViewIndex.HVACPage2));
            m_SelectableBtns[0].ButtonDownEvent += (sender, args) => isOff = false;
            m_SelectableBtns[1].ButtonDownEvent += (sender, args) => isOff = false;
            m_SelectableBtns[2].ButtonDownEvent += (sender, args) => isOff = false;
            m_SelectableBtns[3].ButtonDownEvent += (sender, args) => isOff = true;
        }

        private void SelectBtns(object sender, EventArgs eventArgs)
        {

            var select = (GDIButton)sender;
            if (!select.IsEnable)
            {
                return;
            }
            select.IsEnable = true;
            m_SelectableBtns.ForEach(e => e.TextColor = m_BtnNormalTextColor);
            select.TextColor = m_BtnSelctedTextColor;
            var work = (HVACWorkState)select.Tag;
            work.SwithToThis();
        }

        private void InitalizeContentGrid()
        {
            var dt = new DataTable("HVAC");
            dt.Columns.Add("Model", typeof(string));
            dt.Columns.Add(CarNameConstant.Car01011, typeof(string));
            dt.Columns.Add(CarNameConstant.Car01012, typeof(string));
            dt.Columns.Add(CarNameConstant.Car01013, typeof(string));
            dt.Columns.Add(CarNameConstant.Car01014, typeof(string));
            dt.Columns.Add(CarNameConstant.Car01015, typeof(string));

            dt.Rows.Add(new object[] { GlobleParam.ResManager.GetString("String88"), "25", "25", "25", "25", "25", });
            dt.Rows.Add(new object[]
            {
                GlobleParam.ResManager.GetString("String89"), m_SettedTemperature.ToString(),
                m_SettedTemperature.ToString(), m_SettedTemperature.ToString(), m_SettedTemperature.ToString(),
                m_SettedTemperature.ToString(),
            });
            dt.Rows.Add(new object[]
            {
                GlobleParam.ResManager.GetString("String90"),
                EnumUtil.GetDescription(HVACControlModel.Central).First(),
                EnumUtil.GetDescription(HVACControlModel.Central).First(),
                EnumUtil.GetDescription(HVACControlModel.Central).First(),
                EnumUtil.GetDescription(HVACControlModel.Central).First(),
                EnumUtil.GetDescription(HVACControlModel.Central).First(),
            });
            dt.Rows.Add(new object[]
            {
                GlobleParam.ResManager.GetString("String91"),
                EnumUtil.GetDescription(HVACWorkingModel.EmVent).First(),
                EnumUtil.GetDescription(HVACWorkingModel.EmVent).First(),
                EnumUtil.GetDescription(HVACWorkingModel.EmVent).First(),
                EnumUtil.GetDescription(HVACWorkingModel.EmVent).First(),
                EnumUtil.GetDescription(HVACWorkingModel.EmVent).First(),
            });

            var pen = new Pen(Color.FromArgb(92, 95, 93), 2);
            m_ContentGrid = new DataGrid(dt)
            {
                RowHeight = 30,
                RowHeadWidth = 31,
                IsRowHeadVisiable = false,
                IsShowRowHeadNum = false,
                IsColoumnHeadVisible = false,
                MaxRowCount = 4,
                GridLinePen = pen,
                Location = new Point(10, 135)
            };
            m_ContentGrid.ColumnWidth.Add(130);
            m_ContentGrid.ColumnWidth.Add(120);
            m_ContentGrid.ColumnWidth.Add(120);
            m_ContentGrid.ColumnWidth.Add(120);
            m_ContentGrid.ColumnWidth.Add(120);
            m_ContentGrid.ColumnWidth.Add(120);
            m_ContentGrid.Init();

            foreach (var txt in m_ContentGrid.DataGridRowCollection.Select(s => s.RowTexts).SelectMany(s => s.Skip(1)))
            {
                txt.TextFormat.Alignment = StringAlignment.Center;
            }

        }

        public override void paint(Graphics g)
        {

            m_ContentGrid.OnDraw(g);

            m_SelectableBtns.ForEach(e => e.OnDraw(g));

            m_SelectableBtns.ForEach(f => f.IsEnable = GlobleParam.Instance.WorkModel != HMIWorkModel.NoActoveDrive);

            if (GlobleParam.Instance.WorkModel == HMIWorkModel.NoActoveDrive)
            {
                CanSetTemperature = false;
            }

            m_SetTemperatureBtns.ForEach(e => e.OnDraw(g));

            m_NextPageBtn.OnDraw(g);

            m_TemperatureText.OnDraw(g);
        }

        public override bool mouseDown(Point point)
        {
            return m_SetTemperatureBtns.Any(a => a.OnMouseDown(point)) || m_SelectableBtns.Any(a => a.OnMouseDown(point)) || m_NextPageBtn.OnMouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            return m_SetTemperatureBtns.Any(a => a.OnMouseUp(point));
        }

        private abstract class HVACWorkState
        {
            protected HVAC m_HVAC;

            protected HVACWorkState(HVAC hvac)
            {
                m_HVAC = hvac;
            }

            public virtual void SwithToThis()
            {

            }
        }

        private class OffHVACWorkState : HVACWorkState
        {
            public OffHVACWorkState(HVAC hvac)
                : base(hvac)
            {
            }

            public override void SwithToThis()
            {
                var row = m_HVAC.m_ContentGrid.DataSource.Rows[3];
                foreach (var car in CarNameConstant.CarNameCollection)
                {
                    row[car] = Resource_AR.Text0129;
                }

                m_HVAC.CanSetTemperature = false;
            }
        }

        private class VentilationHVACWorkState : HVACWorkState
        {
            public VentilationHVACWorkState(HVAC hvac)
                : base(hvac)
            {
            }

            public override void SwithToThis()
            {
                var row = m_HVAC.m_ContentGrid.DataSource.Rows[3];
                foreach (var car in CarNameConstant.CarNameCollection)
                {
                    row[car] = Resource_AR.Text0132;
                }
            }
        }

        private class ManualHVACWorkState : HVACWorkState
        {
            public ManualHVACWorkState(HVAC hvac)
                : base(hvac)
            {
            }
            public override void SwithToThis()
            {
                var row = m_HVAC.m_ContentGrid.DataSource.Rows[3];
                foreach (var car in CarNameConstant.CarNameCollection)
                {
                    row[car] = Resource_AR.Text0131;
                }
                m_HVAC.CanSetTemperature = true;
            }
        }

        private class AutoHVACWorkState : HVACWorkState
        {
            public AutoHVACWorkState(HVAC hvac)
                : base(hvac)
            {
            }
            public override void SwithToThis()
            {
                var row = m_HVAC.m_ContentGrid.DataSource.Rows[3];
                foreach (var car in CarNameConstant.CarNameCollection)
                {
                    row[car] = Resource_AR.Text0132;
                }

                m_HVAC.CanSetTemperature = false;
            }
        }


    }
}