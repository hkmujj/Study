using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.停放制动
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIParkingBrakes : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;
        private Dictionary<int, int> m_OutBoolIndex;
        private string m_LastStr = string.Empty;
        private readonly string[] m_BtnStr = {"", "", "", "", "", "", "", "", "", "制动\n状态"};
        private List<int> m_Lst;
        private List<int> m_RemoveList;

        /// <summary>
        /// 上面一行的状态
        /// </summary>
        private Dictionary<int, ParkingBrakeRefreshModel> m_ParkingBrakeStateDictionaryUp;

        /// <summary>
        /// 下面一行的状态
        /// </summary>
        private Dictionary<int, ParkingBrakeRefreshModel> m_ParkingBrakeStateDictionaryDown;

        private int[] m_Cars;
        private GraphicsPath m_TrianglePath;
        private GraphicsPath m_TrianglePath1;
        private GDIRectText m_RectTextCountersign;
        private int m_Outkey;
        private GDIRectText m_RectTextCancel;

        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "DMI停放制动";
        }

        //6
        public override bool Initalize()
        {
            m_Lst = new List<int>();
            m_RemoveList = new List<int>();
            m_Cars = GlobalParam.Instance.ProjectType == ProjectType.CRH3C
                ? new[] {2, 4, 5, 7, 10, 12, 13, 15}
                : new[] {2, 7, 10, 15};

            var cars = GlobalParam.Instance.ProjectType == ProjectType.CRH3C
                ? new[] {12, 14, 15, 17, 22, 24, 25, 27}
                : new[] {12, 17, 22, 27};

            m_ParkingBrakeStateDictionaryDown = m_Cars.ToDictionary(kvp => kvp,
                kvp => new ParkingBrakeRefreshModel(new[]
                {
                    GetInBoolIndex(string.Format("停放制动施加{0}", kvp)),
                    GetInBoolIndex(string.Format("停放制动{0}-状态未知", kvp)),
                }));
            m_ParkingBrakeStateDictionaryUp = m_Cars.ToDictionary(kvp => kvp,
                kvp => new ParkingBrakeRefreshModel(new[]
                {
                    GetInBoolIndex(string.Format("停放制动失效{0}", kvp)),
                    GetInBoolIndex(string.Format("停放制动有效{0}", kvp)),
                }));
            m_OutBoolIndex = cars.ToDictionary(car => car,
                car =>
                    GetOutBoolIndex(string.Format("{0}确认停放制动{1}车", cars.Length == 4 ? "380B" : "3C",
                        car)));
            //确认
            m_RectTextCountersign = new GDIRectText
            {
                OutLineRectangle = new Rectangle(680, 440, 110, 60),
                Text = "确认",
                TextColor = Color.White,
                BkColor = Color.Black,
                OutLinePen = PenItems.WhiltPen,
                NeedDarwOutline = true,
                Tag =
                    cars.ToDictionary(car => car,
                        car => GetInBoolIndex(string.Format("{1}{0}车确认手动缓解确认", car, cars.Length == 4 ? "380B" : "3C"))),
                RefreshAction = o => RefreshTag(false),
                Visible = false,


            };
            //取消 按钮指示
            m_RectTextCancel = new GDIRectText
            {
                OutLineRectangle = new Rectangle(680, 40, 110, 60),
                Text = "取消",
                TextColor = Color.White,
                BkColor = Color.Black,
                NeedDarwOutline = true,
                Visible = false
            };
            var x1 = m_RectTextCountersign.OutLineRectangle.Right - 15;
            var x2 = m_RectTextCountersign.OutLineRectangle.Right;
            var y2 = m_RectTextCountersign.OutLineRectangle.GetMidPoint(Direction.Right).Y;
            var y1 = y2 - 10;
            var y3 = y2 + 10;
            TrianglePath(x1, y1, x2, y2, y3, true);
            x1 = m_RectTextCancel.OutLineRectangle.Right - 15;
            x2 = m_RectTextCancel.OutLineRectangle.Right;
            y2 = m_RectTextCancel.OutLineRectangle.GetMidPoint(Direction.Right).Y;
            y1 = y2 - 10;
            y3 = y2 + 10;
            TrianglePath(x1, y1, x2, y2, y3, false);
            UIObj.InBoolList.AddRange(m_ParkingBrakeStateDictionaryUp.Values.SelectMany(s => s.Indexs));
            UIObj.InBoolList.AddRange(m_ParkingBrakeStateDictionaryDown.Values.SelectMany(s => s.Indexs));

            InitData();
            return true;

        }

        public void RefreshTag(bool bl)
        {
            var names = m_RectTextCountersign.Tag as Dictionary<int, int>;
            if (names == null)
            {
                return;
            }

            if (names.Keys.All(a => !BoolList[names[a]]) && !bl)
            {
                DMITitle.BtnContentList[0].BtnStr = string.Empty;
                return;
            }

            foreach (var key in names.Keys)
            {
                if (BoolList[names[key]] && !BoolOldList[names[key]])
                {
                    m_RectTextCountersign.Text = string.Format("确认:\r\n停放制动{0}", key);
                    if (!m_Lst.Contains(key))
                    {
                        m_Lst.Add(key);
                        m_Outkey = key;
                        m_RemoveList.Remove(key);
                        break;
                    }
                    else if (!bl && m_Lst.Contains(key))
                    {
                        m_Outkey = key;
                        DMITitle.BtnContentList[0].BtnStr = m_RectTextCountersign.Visible
                            ? string.Empty
                            : string.Format("{0}确认\r\n手动缓解", key);
                        break;
                    }
                }
                else if (!BoolList[names[key]] && m_Lst.Contains(key))
                {
                    m_Lst.Remove(key);
                    break;
                }
            }

        }

        /// <summary>
        /// 三角
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="bl"></param>
        private void TrianglePath(int x1, int y1, int x2, int y2, int y3, bool bl)
        {
            if (bl)
            {
                m_TrianglePath = new GraphicsPath();
                m_TrianglePath.AddLine(new Point(x1, y1), new Point(x2, y2));
                m_TrianglePath.AddLine(new Point(x2, y2), new Point(x1, y3));
                m_TrianglePath.AddLine(new Point(x1, y3), new Point(x1, y1));
            }
            else
            {
                m_TrianglePath1 = new GraphicsPath();
                m_TrianglePath1.AddLine(new Point(x1, y1), new Point(x2, y2));
                m_TrianglePath1.AddLine(new Point(x2, y2), new Point(x1, y3));
                m_TrianglePath1.AddLine(new Point(x1, y3), new Point(x1, y1));
            }
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2 || DMITitle.BtnContentList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
            foreach (var t in m_Lst.Where(t => !m_RemoveList.Contains(t)))
            {
                DMITitle.BtnContentList[0].BtnStr = string.Format("{0}车确认\r\n手动缓解", t);
            }
            //if (m_lst.Any())
            //{
            //    DMITitle.BtnContentList[0].BtnStr = string.Format("确认手动缓解{0}", m_lst[0]);
            //}
        }

        public override void Paint(Graphics g)
        {
            RefreshParkingBrakeStates();

            GetValue();

            Draw(g);
        }

        private void RefreshParkingBrakeStates()
        {
            foreach (var model in m_ParkingBrakeStateDictionaryUp.Values)
            {
                model.ParkingBrakeState = ParkingBrakeState.None;

                if (BoolList[model.Indexs[0]])
                {
                    model.ParkingBrakeState = ParkingBrakeState.Invalid;
                }
                if (BoolList[model.Indexs[1]])
                {
                    model.ParkingBrakeState = ParkingBrakeState.Effective;
                }
            }

            foreach (var model in m_ParkingBrakeStateDictionaryDown.Values)
            {
                model.ParkingBrakeState = ParkingBrakeState.None;

                if (BoolList[model.Indexs[0]])
                {
                    model.ParkingBrakeState = ParkingBrakeState.Infliction;
                }
                if (BoolList[model.Indexs[1]])
                {
                    model.ParkingBrakeState = ParkingBrakeState.Unknown;
                }
            }
        }

        private void Draw(Graphics g)
        {
            m_ControlCollection.ForEach(e => e.OnPaint(g));
            PaintGroundImage(g);
            PaintRectangles(g);
            m_RectTextCancel.OnPaint(g);
            m_RectTextCountersign.OnPaint(g);
            if (m_RectTextCancel.Visible)
            {
                g.FillPath(Brushes.White, m_TrianglePath);
                g.FillPath(Brushes.White, m_TrianglePath1);
            }
        }

        #endregion

        private void GetValue()
        {
            if (DMIButton.BtnDownList.Count != 0)
            {
                if (DMIButton.BtnDownList[0] == 5 && m_RectTextCountersign.Visible)
                {
                    append_postCmd(CmdType.SetBoolValue, m_OutBoolIndex[m_Outkey], 1, 0);
                }
                return;
            }

            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0:
                        if (!m_RectTextCountersign.Visible)
                        {
                            append_postCmd(CmdType.ChangePage, 21, 0, 0);
                        }
                        m_RectTextCountersign.Visible = DMITitle.BtnContentList[0].BtnStr != string.Empty;
                        m_RectTextCancel.Visible = DMITitle.BtnContentList[0].BtnStr != string.Empty;
                        DMITitle.BtnContentList[0].BtnStr = m_LastStr;

                        break;
                    case 5:
                        if (m_RectTextCountersign.Visible)
                        {
                            m_RectTextCountersign.Visible = DMITitle.BtnContentList[0].BtnStr != string.Empty;
                            m_RectTextCancel.Visible = DMITitle.BtnContentList[0].BtnStr != string.Empty;
                            append_postCmd(CmdType.SetBoolValue, m_OutBoolIndex[m_Outkey], 0, 0);
                            m_RemoveList.Add(m_Outkey);

                        }
                        break;
                    case 6:
                        m_RectTextCountersign.Visible = DMITitle.BtnContentList[0].BtnStr != string.Empty;
                        m_RectTextCancel.Visible = DMITitle.BtnContentList[0].BtnStr != string.Empty;
                        if (DMITitle.BtnContentList[0].BtnStr != string.Empty)
                        {

                            m_LastStr = DMITitle.BtnContentList[0].BtnStr;
                            DMITitle.BtnContentList[0].BtnStr = string.Empty;
                        }
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 21, 0, 0); //
                        break;
                }
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("停放制动", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void PaintGroundImage(Graphics g)
        {
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                foreach (var car in m_Cars)
                {
                    DrawDownRect(g, car);

                    DrawUpRect(g, car);
                }
            }
            else
            {
                for (int i = 0; i < (DMITitle.MarshallMode ? m_Cars.Length : m_Cars.Length/2); i++)
                {
                    var car = m_Cars[i];

                    DrawDownRect(g, car);

                    DrawUpRect(g, car);
                }
            }


            DrawCarNum(g);
        }

        private string GetCarNumOfBl(int i)
        {
            return DMITitle.TrainInSide ? (16 - i).ToString("00") : (i + 1).ToString("00");
        }

        private void DrawCarNum(Graphics g)
        {
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                for (var index = 0; index < 16; index++)
                {
                    //车辆号
                    g.DrawString(GetCarNumOfBl(index)
                        ,
                        FontsItems.FontC11,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_RectsList[20 + index],
                        FontsItems.TheAlignment(FontRelated.居中));
                }
            }
            else
            {
                for (var index = 0; index < (DMITitle.MarshallMode ? 16 : 8); index++)
                {
                    //车辆号
                    g.DrawString(GetCarNum.GetNum(index, DMITitle.MarshallMode, DMITitle.TrainInSide)
                        ,
                        FontsItems.FontC11,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_RectsList[20 + index],
                        FontsItems.TheAlignment(FontRelated.居中));
                }
            }

        }

        private void DrawUpRect(Graphics g, int car)
        {
            var state = m_ParkingBrakeStateDictionaryDown[car].ParkingBrakeState;
            var region = m_RectsList[36 + (car - 1)*2];
            //停放制动施加
            switch (state)
            {
                case ParkingBrakeState.Infliction:
                    g.FillRectangle(SolidBrushsItems.BlueBrush1, region);
                    g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                        Rectangle.Round(region));
                    break;
                case ParkingBrakeState.Unknown:
                    g.DrawImage(CommonImages.StateUnkown, region);
                    break;
                default:
                    g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                        Rectangle.Round(region));
                    break;
            }
        }

        private void DrawDownRect(Graphics g, int car)
        {
            var state1 = m_ParkingBrakeStateDictionaryDown[car].ParkingBrakeState;
            if (state1 == ParkingBrakeState.Unknown)
            {
                return;
            }

            var region = m_RectsList[36 + (car - 1)*2 + 1];
            var state = m_ParkingBrakeStateDictionaryUp[car].ParkingBrakeState;
            switch (state)
            {
                case ParkingBrakeState.Invalid:
                    g.DrawImage(BrakeImages.叉, region);
                    break;
                case ParkingBrakeState.Effective:
                    g.FillRectangle(SolidBrushsItems.BlueBrush1, region);
                    break;
            }
            //框
            g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                Rectangle.Round(region));


        }

        private List<CommonInnerControlBase> m_ControlCollection;

        private void InitData()
        {

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIParkingBrakes, ref m_RectsList))
            {

            }
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                m_ControlCollection = new List<CommonInnerControlBase>
                {
                    new Line(new Point(148, 278), new Point(148, 482))
                    {
                        Color = DMITitle.NightMode ? Color.Black : Color.White,
                        Tag = 1
                    },
                    new GDIRectText
                    {
                        Text = "停放制动",
                        DrawFont = FontsItems.FontC11,
                        TextFormat =
                            new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center},
                        NeedDarwOutline = false,
                        OutLineRectangle = new Rectangle(150, 280, 130, 30),
                        Tag = 1

                    },
                };
            }
            else
            {
                m_ControlCollection = new List<CommonInnerControlBase>
                {
                    new Line(new Point(148, 278), new Point(148, 482))
                    {
                        Color = DMITitle.NightMode ? Color.Black : Color.White,
                        Tag = 1
                    },
                    new Line(new Point(428, 328), new Point(428, 482))
                    {
                        Color = DMITitle.NightMode ? Color.Black : Color.White,
                        Tag = 2,
                        Visible = false
                    },
                    new GDIRectText
                    {
                        Text = "停放制动",
                        DrawFont = FontsItems.FontC11,
                        TextFormat =
                            new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center},
                        NeedDarwOutline = false,
                        OutLineRectangle = new Rectangle(150, 280, 130, 30),
                        Tag = 1

                    },
                    new GDIRectText
                    {
                        Text = "动车组1",
                        DrawFont = FontsItems.FontC11,
                        TextFormat =
                            new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center},
                        NeedDarwOutline = false,
                        OutLineRectangle = new Rectangle(150, 330, 130, 30),
                        RefreshAction = o =>
                        {
                            ((GDIRectText) o).Text = DMITitle.TrainInSide ? "动车组2" : "动车组1";
                        },
                        Tag = 2

                    },
                    new GDIRectText
                    {
                        Text = "动车组2",
                        DrawFont = FontsItems.FontC11,
                        TextFormat =
                            new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center},
                        NeedDarwOutline = false,
                        OutLineRectangle = new Rectangle(430, 330, 130, 30),
                        RefreshAction = o =>
                        {
                            ((GDIRectText) o).Text = DMITitle.TrainInSide ? "动车组1" : "动车组2";
                        },
                        Tag = 3,
                        Visible = false

                    }
                };
                DMITitle.MarshallModeChanged += m =>
                {
                    m_ControlCollection[m_ControlCollection.FindIndex(f => f is Line && (int) f.Tag == 2)].Visible =
                        DMITitle.MarshallMode;
                    m_ControlCollection[m_ControlCollection.FindIndex(f => f is GDIRectText && (int) f.Tag == 3)]
                        .Visible = DMITitle.MarshallMode;
                };
            }

        }
    }
}