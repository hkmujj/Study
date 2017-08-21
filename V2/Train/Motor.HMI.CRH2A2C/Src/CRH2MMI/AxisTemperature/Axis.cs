using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Roundness;
using CommonUtil.Util;
using CRH2MMI.Bottom;
using CRH2MMI.BreakLocked;
using CRH2MMI.Common;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;
using MMICommon.Controls.Grid;


namespace CRH2MMI.AxisTemperature
{
    /// <summary>
    /// 轴温切除
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Axis : CRH2BaseClass
    {

        /// <summary>
        /// 背景色的区域 
        /// </summary>
        private Rectangle m_BkRectangle = new Rectangle(0, 315, 800, 175);

        // ReSharper disable once InconsistentNaming
        private static readonly SolidBrush m_BkRegionBrush = CRH2Resource.WWBrush;

        /// <summary>
        /// 车辆按键
        /// </summary>
        private List<CRH2Button> m_TrainButton;

        /// <summary>
        /// 选中的车箱
        /// </summary>
        private CRH2Button m_SelectTrain;


        /// <summary>
        /// 轴温的按键
        /// </summary>
        private List<CRH2Button> m_AxisTempreaturBtn;

        /// <summary>
        /// 选中的 轴温的按键
        /// </summary>
        private CRH2Button m_SelectAxisTempreatur;

        /// <summary>
        /// 功能按键
        /// </summary>
        private List<CRH2Button> m_ControlBtn;

        /// <summary>
        /// 选中的功能 m_ControlBtn 中的一个
        /// </summary>
        private CRH2Button m_SelectCtol;

        private List<GridLine> m_StateGridLine;

        private TrainView m_TrainView;

        private CommonSetBtn m_CommonSetBtn;

        /// <summary>
        /// 
        /// </summary>
        private AxisResource m_AxisResource;

        private List<CommonInnerControlBase> m_ConstInfoList;

        private SuggestiveInformation m_SuggestiveInformation;

        public override void paint(Graphics g)
        {
            g.FillRectangle(m_BkRegionBrush, m_BkRectangle);

            m_ConstInfoList.ForEach(e => e.OnDraw(g));

            m_StateGridLine.ForEach(e => e.OnPaint(g));

            m_TrainButton.ForEach(f => f.OnDraw(g));

            m_AxisTempreaturBtn.ForEach(f => f.OnDraw(g));

            m_ControlBtn.ForEach(f => f.OnDraw(g));

            m_CommonSetBtn.OnDraw(g);
        }

        public override bool Init()
        {
            m_AxisResource = new AxisResource(this);
            m_ConstInfoList = new List<CommonInnerControlBase>();
            m_TrainView = TrainView.Instance;
            GlobalEvent.Instance.ReversalChanged += (sender, args) => OnReversal();
            m_CommonSetBtn = new CommonSetBtn()
            {
                SetButtonDown = OnSetButtonDown ,
                SetButtonUp = OnsetButtonUp
            };
            InitUIObj();

            InitBtn();

            InitGridLine();

            InitTitle();

            InitPresentation();

            return true;
        }

        private void OnsetButtonUp(object sender, EventArgs e)
        {
            if (CanSet())
            {
                var key = new AxisTemperatureSendKeyModel()
                          {
                              CarNo = m_TrainButton.IndexOf(m_SelectTrain),
                              AxisTemperaturedLocation = m_AxisTempreaturBtn.IndexOf(m_SelectAxisTempreatur) + 1,
                              Type = (CtrolType) m_ControlBtn.IndexOf(m_SelectCtol)
                          };

                m_AxisResource.SetAxisTemperaturState(key, 0);
            }
            else
            {
                m_SuggestiveInformation = this.GetSameProjectObjcect<SuggestiveInformation>();
                m_SuggestiveInformation.UpdateInformation(new InformationModel("输入错误，请重新设定") { InformationType = InformationType.Error, Location = InformationLocation.Up });

            }

            ReselectAxisTempreatur(null);
            ReselectControl(null);
            ReselectTrain(null);
        }

        private void OnSetButtonDown(object sender, EventArgs e)
        {
            if (!CanSet())
            {
                return;
            }


            var key = new AxisTemperatureSendKeyModel()
            {
                CarNo = m_TrainButton.IndexOf(m_SelectTrain),
                AxisTemperaturedLocation = m_AxisTempreaturBtn.IndexOf(m_SelectAxisTempreatur) + 1,
                Type = (CtrolType)m_ControlBtn.IndexOf(m_SelectCtol)
            };

            m_AxisResource.SetAxisTemperaturState(key, 1);
        }

        private bool CanSet()
        {
            if (m_SelectTrain == null || m_SelectAxisTempreatur == null || m_SelectCtol == null)
            {
                return false;
            }
            return true;
        }

        private void InitUIObj()
        {
            var models =
                GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig.AxisTemperatureSendModels
                    .Cast<CRH2CommunicationPortModel>().ToList();
            models.AddRange(GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig.Grid.SelectMany(s => s.Rows).SelectMany(s => s.ColumnConfigs).Cast<CRH2CommunicationPortModel>());

            InitUIObj(models);
        }

        private void InitPresentation()
        {
            var config = GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig;
            var y = m_StateGridLine.Max(m => m.OutLineRectangle.Bottom) + 20;
            var x = config.Grid.Min(m => m.AbsX) + config.Grid.Max(m => m.Width) / 2 - 200;
            var textSize = new Size(65, 20);
            const int r = 8;
            const int interval = 10;
            m_ConstInfoList.Add(new GDIRectText()
            {
                BkColor = Color.Black,
                OutLineRectangle = new Rectangle(new Point(x, y), new Size(20, textSize.Height)),
                TextColor = Color.White,
                Text = "( ",
                NeedDarwOutline = false,
                Bold = true,
            });

            m_ConstInfoList.Add(new GDIRoundness()
            {
                ContentColor = Color.White,
                NeedDrawArc = false,
                NeedDrawContent = true,
                Center = new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y + textSize.Height / 2),
                R = r,
            });

            m_ConstInfoList.Add(new GDIRectText()
            {
                BkColor = Color.Black,
                OutLineRectangle =
                    new Rectangle(new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y),
                        new Size(textSize.Width, textSize.Height)),
                TextColor = Color.White,
                Text = ": 正常",
                NeedDarwOutline = false,
                Bold = true,
            });
            m_ConstInfoList.Add(new GDIRoundness()
            {
                ContentColor = Color.Yellow,
                NeedDrawArc = false,
                NeedDrawContent = true,
                Center = new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y + textSize.Height / 2),
                R = r,
            });

            m_ConstInfoList.Add(new GDIRectText()
            {
                BkColor = Color.Black,
                OutLineRectangle =
                    new Rectangle(new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y),
                        new Size(textSize.Width, textSize.Height)),
                TextColor = Color.White,
                Text = ": 切除",
                NeedDarwOutline = false,
                Bold = true,
            });
            m_ConstInfoList.Add(new GDIRoundness()
            {
                ContentColor = Color.Red,
                NeedDrawArc = false,
                NeedDrawContent = true,
                Center = new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y + textSize.Height / 2),
                R = r,
            });

            m_ConstInfoList.Add(new GDIRectText()
            {
                BkColor = Color.Black,
                OutLineRectangle =
                    new Rectangle(new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y),
                        new Size(textSize.Width + 10, textSize.Height)),
                TextColor = Color.White,
                Text = ": 异常  )",
                NeedDarwOutline = false,
                Bold = true,
            });
        }


        private void InitTitle()
        {

            m_ConstInfoList.Add(new GDIRectText()
            {
                BkColor = m_BkRegionBrush.Color,
                OutLineRectangle = new Rectangle(m_BkRectangle.X + 10, m_BkRectangle.Y + 10, 300, 20),
                Text = "车厢及轴温选择",
                TextColor = Color.Black,
                NeedDarwOutline = false,
                Bold = true,
            });
        }

        private void InitGridLine()
        {
            GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig.Grid.ForEach(e => e.RefreshRelation());
            m_StateGridLine = new List<GridLine>();
            var titles = new List<GDIRectText>();
            foreach (var gridConfig in GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig.Grid)
            {
                var gv = GDIGridLineHelper.CreateGridLine(gridConfig, OnCreateGridItemAction);

                var glIinit = new GridLineInitialize() {ViewClass = this};
                titles.AddRange(glIinit.CreateTitles(gv, gridConfig));

                m_StateGridLine.Add(gv);
            }

            titles.ForEach(e => e.Bold = true);
            m_ConstInfoList.AddRange(titles);
        }

        private void OnCreateGridItemAction(GridLine gridLine, GridColumnConfig gridColumnConfig, GridItemBase arg3)
        {
            var glIinit = new GridLineInitialize() { ViewClass = this };
            glIinit.InitInnerContrl(gridLine, gridColumnConfig, arg3);

            var it = arg3 as GridRoundnessItem;
            Debug.Assert(it != null, "it != null");
            it.Roundness.RefreshAction = o =>
            {
                var roud = (GDIRoundness)o;
                var bIndxs = gridColumnConfig.InBoolColoumNames.Select(GetInBoolIndex).ToList();
                if (BoolList[bIndxs[1]])
                {
                    roud.ContentColor = gridColumnConfig.Parent.Colors[2];
                }
                else if (BoolList[bIndxs[0]])
                {
                    roud.ContentColor = gridColumnConfig.Parent.Colors[1];
                }
                else
                {
                    roud.ContentColor = gridColumnConfig.Parent.Colors[0];
                }
            };
        }

        private void OnReversal()
        {
            m_StateGridLine.Reverse();
            var mat =
                MatrixHelper.CreateTurnMatrix(
                    LineHelper.GetMidPoint(m_TrainButton.First().Location, m_TrainButton.Last().Location).X,
                    TurnOrientation.Horizontal);
            m_TrainButton.ForEach(e =>
            {
                var p = new[] { e.Location };
                mat.TransformPoints(p);
                e.Location = p[0];
            });
        }

        private void InitBtn()
        {
            m_TrainButton = new List<CRH2Button>();

            for (int i = 0; i < GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig.Grid.Max( m => m.ColumnCount); i++)
            {
                m_TrainButton.Add(new CRH2Button()
                {
                    OutLineRectangle = new Rectangle((i % 8) * 100, 347 + 45 * (i / 8), 100, 45),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent = OnTrainButtonDown,
                    Text = string.Format("{0} 车厢", i + 1),
                    TextColor = Color.White,
                });
            }

            // 抱死
            m_AxisTempreaturBtn = new List<CRH2Button>(2);
            for (int i = 0; i < 2; i++)
            {
                m_AxisTempreaturBtn.Add(new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(i * 152, m_BkRectangle.Bottom - 45 -5, 152, 45),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent = OnAxisTempreaturButtonDown,
                    Text = string.Format("轴温 {0}", i + 1),
                    TextColor = Color.White,
                });
            }

            if (GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig.AxisTemperatureSendModels.Max(m => m.AxisTemperaturedLocation) == 3)
            {
                m_AxisTempreaturBtn.Add(new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(2 * 152, m_BkRectangle.Bottom - 45 - 5, 200, 45),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent = OnAxisTempreaturButtonDown,
                    Text = "牵引电机温度高",
                    TextColor = Color.White,
                });
            }

            // 切除, 复位
            m_ControlBtn = new List<CRH2Button>(2);
            for (int i = 0; i < 2; i++)
            {
                m_ControlBtn.Add(new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(252 + i * 150, m_CommonSetBtn.SetBtnEntity.OutLineRectangle.Top, 150, 45),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent = OnCtolButtonDown,
                    TextColor = Color.White,
                });
            }
            m_ControlBtn[0].Text = "切 　除";
            m_ControlBtn[1].Text = "复   位";

        }

        private void OnAxisTempreaturButtonDown(object sender, EventArgs e)
        {
            ReselectAxisTempreatur(sender as CRH2Button);
        }

        private void OnCtolButtonDown(object sender, EventArgs e)
        {
            ReselectControl(sender as CRH2Button);
        }

        private void OnTrainButtonDown(object sender, EventArgs eventArgs)
        {
            ReselectTrain(sender as CRH2Button);
        }

        private void ReselectTrain(CRH2Button trainButton)
        {
            if (m_SelectTrain != null)
            {
                m_SelectTrain.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectTrain = trainButton;
            if (m_SelectTrain != null)
            {
                m_SelectTrain.CurrentMouseState = MouseState.MouseDown;
            }
        }

        private void ReselectControl(CRH2Button controlButton)
        {
            if (m_SelectCtol != null)
            {
                m_SelectCtol.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectCtol = controlButton;
            if (m_SelectCtol != null)
            {
                m_SelectCtol.CurrentMouseState = MouseState.MouseDown;
            }
        }
        private void ReselectAxisTempreatur(CRH2Button axisTempreaturButton)
        {
            if (m_SelectAxisTempreatur != null)
            {
                m_SelectAxisTempreatur.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectAxisTempreatur = axisTempreaturButton;
            if (m_SelectAxisTempreatur != null)
            {
                m_SelectAxisTempreatur.CurrentMouseState = MouseState.MouseDown;
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                ReselectTrain(null);
                ReselectAxisTempreatur(null);
                ReselectControl(null);

                switch (nParaB)
                {
                    case 12:
                        m_TrainView.Location = GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig.TrainViewLocation.Rectangle.Location;
                        break;
                }
            }
        }

        public override string GetInfo()
        {
            return "轴温切除";
        }

        protected override bool OnMouseDown(Point nPoint)
        {
            if (m_SuggestiveInformation != null)
            {
                m_SuggestiveInformation.ClearInformation();
                m_SuggestiveInformation = null;
            }

            if (m_TrainButton.Any(a => a.OnMouseDown(nPoint)))
            {
                return true;
            }

            if (m_AxisTempreaturBtn.Any(a => a.OnMouseDown(nPoint)))
            {
                return true;
            }

            if (m_ControlBtn.Any(a => a.OnMouseDown(nPoint)))
            {
                return true;
            }

            if (m_CommonSetBtn.OnMouseDown(nPoint))
            {
                return true;
            }

            return false;
        }

        protected override bool OnMouseUp(Point point)
        {
            return m_CommonSetBtn.OnMouseUp(point);
        }
    }
}