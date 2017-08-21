using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.维修
{
    public enum DMIRemoteDataTransferState
    {
        Prepare,

        Trainsfering,

        Countersign,
    }

    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIRemoteDataTransfer : CRH3C380BBase
    {
        #region ::::::::::::::::::::::::::: init values :::::::::::::::::::::::::::

        private GraphicsPath m_TrianglePath;
        private GraphicsPath m_TrianglePath1;
        private bool m_IsInitialize;

        private bool m_IsVisible;

        private bool IsVisible
        {
            set
            {
                m_IsVisible = value;
                m_BtnStr[0] = m_IsVisible ? "" : "初始化";
                for (int i = 0; i < 10; i++)
                {
                    DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
                }
            }
            get { return m_IsVisible; }
        }

        private GDIRectText m_RectTextCountersign;

        private GDIRectText m_RectTextCancel;

        private List<RectangleF> m_RectsList;

        private float[] m_FValue;

        private readonly string[] m_BtnStr =
        {
            "初始化",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private DMIRemoteDataTransferState State
        {
            set
            {
                m_State = value;
                switch (value)
                {
                    case DMIRemoteDataTransferState.Prepare:
                        m_TransfText.BackColorVisible = false;
                        m_TransfingText.Visible = false;
                        m_SentOverText.Visible = false;
                        m_RectTextCountersign.Visible = false;
                        m_RectTextCancel.Visible = false;
                        break;
                    case DMIRemoteDataTransferState.Trainsfering:
                        m_TransfText.BackColorVisible = false;
                        m_TransfingText.Visible = true;
                        m_SentOverText.Visible = true;
                        m_RectTextCountersign.Visible = false;
                        m_RectTextCancel.Visible = false;
                        break;
                    case DMIRemoteDataTransferState.Countersign:
                        m_TransfText.BackColorVisible = true;
                        m_TransfingText.Visible = false;
                        m_SentOverText.Visible = false;
                        m_RectTextCountersign.Visible = true;
                        m_RectTextCancel.Visible = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }

                DMITitleOnNightModelChanged(DMITitle);
            }
            get { return m_State; }
        }

        private GDIRectText m_TransfingText;

        private GDIRectText m_TransfText;
        private DMIRemoteDataTransferState m_State;
        private GDIRectText m_SentOverText;
        private int m_TransferingCount;

        #endregion

        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "DMI远程数据传输";
        }

        //6
        public override bool Initalize()
        {
            InitData();

            DMITitle.NightModelChanged += DMITitleOnNightModelChanged;

            m_TransfingText = new GDIRectText
            {
                Text = "正在传输中…",
                Visible = false,
                DrawFont = FontsItems.FontC24B,
                OutLineRectangle = new Rectangle(150, 400, 500, (int) m_RectsList[12].Height),
                BackColorVisible = true,
                TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                BkColor = Color.White,
                TextColor = Color.Black
            };

            m_TransfText = new GDIRectText
            {
                Text = "远程数据传输",
                OutLineRectangle = new Rectangle(20, (int) m_RectsList[12].Y, 760, (int) m_RectsList[12].Height),
                DrawFont = FontsItems.FontC24B,
                TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                BackColorVisible = true,
                BkColor = Color.White,
                TextColor = Color.Black
            };

            //确认
            m_RectTextCountersign = new GDIRectText
            {
                OutLineRectangle = new Rectangle(680, 440, 110, 60),
                Text = "初始化：\n远程数据传输",
                TextColor = Color.Black,
                BkColor = Color.White,
                OutLinePen = PenItems.BlackPen,
                NeedDarwOutline = true

            };
            //取消 按钮指示
            m_RectTextCancel = new GDIRectText
            {
                OutLineRectangle = new Rectangle(680, 40, 110, 60),
                Text = "取消",
                TextColor = Color.White,
                BkColor = Color.Black,
                NeedDarwOutline = true
            };
            m_SentOverText = new GDIRectText
            {
                Text = "传输完成后",
                Visible = false,
                DrawFont = FontsItems.FontC24B,
                OutLineRectangle = new Rectangle(580, 460, 200, (int) m_RectsList[12].Height),
                BackColorVisible = true,
                TextFormat = FontsItems.TheAlignment(FontRelated.靠右),
                BkColor = Color.Black,
                TextColor = Color.White
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
            State = DMIRemoteDataTransferState.Prepare;

            return true;
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

            IsVisible = false;
            m_IsInitialize = false;
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }

            DMITitleOnNightModelChanged(DMITitle);
        }

        private void DMITitleOnNightModelChanged(DMITitle dmiTitle)
        {
            if (!dmiTitle.NightMode)
            {
                m_TransfingText.BkColor = Color.White;
                m_TransfText.TextColor = Color.Black;
                if (m_TransfText.BackColorVisible)
                {
                    m_TransfText.TextColor = Color.Black;
                    m_TransfText.BkColor = Color.White;
                }
                else
                {
                    m_TransfText.TextColor = !m_IsInitialize ? Color.White : Color.FromArgb(118, 118, 118);
                    m_TransfText.BkColor = Color.Black;
                }
            }
            else
            {
                m_TransfingText.BkColor = Color.Black;
                m_TransfText.TextColor = Color.White;
                if (m_TransfText.BackColorVisible)
                {
                    m_TransfText.TextColor = Color.Black;
                    m_TransfText.BkColor = Color.White;
                }
                else
                {
                    m_TransfText.TextColor = Color.Black;
                    m_TransfText.BkColor = Color.White;
                }
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            RefreshState();

            DrawOn(g);

            return;

            PaintRectangles(g);
        }

        private void DrawOn(Graphics g)
        {
            g.DrawString("维护; 远程数据传输",
                FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1],
                FontsItems.TheAlignment(FontRelated.靠左));


            m_TransfText.OnPaint(g);

            m_TransfingText.OnPaint(g);
            m_SentOverText.OnPaint(g);
            if (m_RectTextCancel.Visible)
            {
                m_RectTextCancel.OnPaint(g);
                m_RectTextCountersign.OnPaint(g);
                g.FillPath(Brushes.Black, m_TrianglePath);
                g.FillPath(Brushes.White, m_TrianglePath1);
            }
        }

        private void RefreshState()
        {
            if (State == DMIRemoteDataTransferState.Trainsfering)
            {
                m_TransferingCount++;
                if (m_TransferingCount > 25)
                {
                    m_TransferingCount = 0;
                    State = DMIRemoteDataTransferState.Prepare;
                }
                m_IsInitialize = true;
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //取消
                        if (State == DMIRemoteDataTransferState.Prepare)
                        {
                            append_postCmd(CmdType.ChangePage, 110, 0, 0); //维护
                        }
                        else
                        {
                            m_IsInitialize = false;
                            IsVisible = false;
                            State = DMIRemoteDataTransferState.Prepare;
                        }
                        break;
                    case 5: //确认
                        if (State == DMIRemoteDataTransferState.Countersign)
                        {
                            IsVisible = true;
                            State = DMIRemoteDataTransferState.Trainsfering;
                        }
                        break;
                    // 初始化
                    case 6:
                        if (State == DMIRemoteDataTransferState.Prepare && !IsVisible)
                        {
                            IsVisible = true;
                            State = DMIRemoteDataTransferState.Countersign;
                        }
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0); //
                        break;
                }
            }

            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("维护; 远程数据传输", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            g.DrawString("远程数据传输", FontsItems.FontC24B,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[12], FontsItems.TheAlignment(FontRelated.居中));
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIRemoteDataTransfer, ref m_RectsList))
            {

            }
        }

        #endregion
    }
}