using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.基本画面
{
    public class BasePage3C : BasePageContent
    {
        private List<CommonInnerControlBase> m_ConstInfos;

        /// <summary>
        /// 充电机
        /// </summary>
        private List<RectangleF> m_ChargingGeneratorRegions;

        public BasePage3C(DMIBasePage srcObj)
            : base(srcObj)
        {
            RefreshAction += OnRefreshAction;
        }

        private void OnRefreshAction(object o)
        {
            ResponseBtnEvent();

            for (var index = 0; index < m_BValue.Length; index++)
            {
                if (SrcObj.DMITitle.TrainInSide)
                {
                    m_BValue[index] =
                        SrcObj.BoolList[
                            SrcObj.GetInBoolIndex(m_BValueAllArrange[SrcObj.DMITitle.MarshallMode ? 1 : 0][index])];
                }
                else
                {
                    m_BValue[index] = SrcObj.BoolList[SrcObj.GetInBoolIndex(m_RawBValueArrange[index])];
                }
            }
        }


        public override void Init()
        {
            SrcObj.append_postCmd(CmdType.SetInBoolValue, SrcObj.GetInBoolIndex(InBoolKeys.Inb16编接口), 1, 0);

            m_ChangingRect = new NeedChangeLength[3];
            BtnStr = new[] {"维护", "系统", "门", "ATP\n系统", "紧急", "准备整备", "牵引\n制动", "自动速度控制", "开关", ""};

            int index;

            #region :::::::

            int[] tmp = {1, 2, 3, 4};
            m_DynamoDictionary1 = tmp.ToDictionary(num => num,
                num => SrcObj.GetInBoolIndex(string.Format("接触网电压充电机{0}-状态未知", num)));

            m_RawBValueArrange = new List<string>
            {
                InBoolKeys.Inb接触网电压充电机1_蓝,
                InBoolKeys.Inb接触网电压充电机1_黄,
                InBoolKeys.Inb接触网电压充电机2_蓝,
                InBoolKeys.Inb接触网电压充电机2_黄,
                InBoolKeys.Inb接触网电压充电机3_蓝,
                InBoolKeys.Inb接触网电压充电机3_黄,
                InBoolKeys.Inb接触网电压充电机4_蓝,
                InBoolKeys.Inb接触网电压充电机4_黄,

            };

            m_BValueAllArrange = new List<string[]>
            {
                new[]
                {
                    InBoolKeys.Inb接触网电压充电机2_蓝,
                    InBoolKeys.Inb接触网电压充电机2_黄,
                    InBoolKeys.Inb接触网电压充电机1_蓝,
                    InBoolKeys.Inb接触网电压充电机1_黄,
                    InBoolKeys.Inb接触网电压充电机4_蓝,
                    InBoolKeys.Inb接触网电压充电机4_黄,
                    InBoolKeys.Inb接触网电压充电机3_蓝,
                    InBoolKeys.Inb接触网电压充电机3_黄,
                },
                new[]
                {
                    InBoolKeys.Inb接触网电压充电机4_蓝,
                    InBoolKeys.Inb接触网电压充电机4_黄,
                    InBoolKeys.Inb接触网电压充电机3_蓝,
                    InBoolKeys.Inb接触网电压充电机3_黄,
                    InBoolKeys.Inb接触网电压充电机2_蓝,
                    InBoolKeys.Inb接触网电压充电机2_黄,
                    InBoolKeys.Inb接触网电压充电机1_蓝,
                    InBoolKeys.Inb接触网电压充电机1_黄,
                }
            };

            #endregion

            m_BValue = new bool[m_RawBValueArrange.Count];

            #region :::::::

            #endregion

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBasePage, ref m_RectsList))
            {

            }

            InitalizeChargingGeneratorRegions();

            m_Rectangle = new List<RectangleF>();
            m_Rectangle = new List<RectangleF>
            {
                new RectangleF(626, 333, 23, 24),
                new RectangleF(626, 303, 23, 24),
                new RectangleF(626, 271, 23, 24),
                new RectangleF(626, 239, 23, 24),
                new RectangleF(626, 115, 23, 24),

            };
            for (index = 0; index < 2; index++)
            {
                m_ChangingRect[index] = new NeedChangeLength(m_RectsList[11 + index*2], 16000f, RectRiseDirection.上,
                    false);
            }
            m_ChangingRect[2] = new NeedChangeLength(m_RectsList[15], 1100f, RectRiseDirection.上, false);

            InitalizeConstInfos();


        }

        private void InitalizeChargingGeneratorRegions()
        {
            var groupX2 = (int) m_RectsList[12 + 1*2].X - 1;
            m_ChargingGeneratorRegions = new List<RectangleF>
            {
                m_RectsList[4 + 0],
                m_RectsList[4 + 1],
                new RectangleF(groupX2, m_RectsList[4 + 2].Y, m_RectsList[4 + 2].Width, m_RectsList[4 + 2].Height),
                new RectangleF(groupX2 + m_RectsList[4 + 2].Width, m_RectsList[4 + 3].Y, m_RectsList[4 + 3].Width,
                    m_RectsList[4 + 3].Height),
            };
        }

        private void InitalizeConstInfos()
        {
            var volTxt = new Rectangle(30, 30, 140, 40);
            var groupX1 = volTxt.X;
            var groupY1 = volTxt.Bottom + 5;
            var groupSize = new Size(120, 40);
            var groupX2 = (int) m_RectsList[12 + 1*2].X - 1;
            var volZeroY = 440;

            m_ConstInfos = new List<CommonInnerControlBase>
            {
                new GDIRectText
                {
                    Text = "接触网电压",
                    DrawFont = FontsItems.FontE12,
                    TextFormat = FontsItems.TheAlignment(FontRelated.靠左),
                    OutLineRectangle = volTxt,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.TextBrush = SrcObj.DMITitle.NightMode
                            ? SolidBrushsItems.BlackBrush
                            : SolidBrushsItems.WhiteBrush;
                    }
                },
                new GDIRectText
                {
                    Text = "网侧电流",
                    DrawFont = FontsItems.FontE12,
                    TextFormat = FontsItems.TheAlignment(FontRelated.靠左),
                    OutLineRectangle = Rectangle.Ceiling(m_RectsList[10]),
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.TextBrush = SrcObj.DMITitle.NightMode
                            ? SolidBrushsItems.BlackBrush
                            : SolidBrushsItems.WhiteBrush;
                    }
                },
                new GDIRectText
                {
                    Text = "主页面",
                    DrawFont = FontsItems.FontC11,
                    TextFormat = FontsItems.TheAlignment(FontRelated.靠左),
                    OutLineRectangle = Rectangle.Ceiling(m_RectsList[1]),
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.TextBrush = SrcObj.DMITitle.NightMode
                            ? SolidBrushsItems.BlackBrush
                            : SolidBrushsItems.WhiteBrush;
                    }
                },
                new GDIRectText
                {
                    Text = "动车组1",
                    DrawFont = FontsItems.FontE12,
                    TextFormat = FontsItems.TheAlignment(FontRelated.靠左),
                    OutLineRectangle = new Rectangle(groupX1, groupY1, groupSize.Width, groupSize.Height),
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.TextBrush = SrcObj.DMITitle.NightMode
                            ? SolidBrushsItems.BlackBrush
                            : SolidBrushsItems.WhiteBrush;
                        txt.Visible = SrcObj.DMITitle.MarshallMode;
                    }
                },
                new GDIRectText
                {
                    Text = "动车组2",
                    DrawFont = FontsItems.FontE12,
                    TextFormat = FontsItems.TheAlignment(FontRelated.靠左),
                    OutLineRectangle = new Rectangle(groupX2, groupY1, groupSize.Width, groupSize.Height),
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.TextBrush = SrcObj.DMITitle.NightMode
                            ? SolidBrushsItems.BlackBrush
                            : SolidBrushsItems.WhiteBrush;
                        txt.Visible = SrcObj.DMITitle.MarshallMode;
                    }
                },
                new Line
                {
                    StartPoint = new Point(groupX2, groupY1),
                    EndPoint = new Point(groupX2, volZeroY),
                    RefreshAction = o =>
                    {
                        var txt = (Line) o;
                        txt.Color = SrcObj.DMITitle.NightMode
                            ? SolidBrushsItems.BlackBrush.Color
                            : SolidBrushsItems.WhiteBrush.Color;
                        txt.Visible = SrcObj.DMITitle.MarshallMode;
                    }
                },
            };
        }

        public override void OnDraw(Graphics g)
        {
            PaintGroundImage(g);

            PaintState(g);

            PaintRectangles(g);
        }


        private void PaintRectangles(Graphics g)
        {
            //接触网电压条
            DrawVoltage(g, 0, SrcObj.GetInFloatValue(InFloatKeys.Inf动车组1接触网电压));
            if (SrcObj.DMITitle.MarshallMode)
            {
                DrawVoltage(g, 1, SrcObj.GetInFloatValue(InFloatKeys.Inf动车组2接触网电压));
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////
            //网侧电流条
            m_ChangingRect[2].DrawRectangle(g,
                SrcObj.GetInFloatValue(InFloatKeys.Inf网侧电流),
                SolidBrushsItems.BlueBrush1,
                SrcObj.DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
            //小三角位置
            //小三角位置
            if (SrcObj.OutBoolList[SrcObj.GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_600A)])
            {
                g.DrawImage(MSImages.小三角_0_0, m_Rectangle[2]);
            }
            else if (SrcObj.OutBoolList[SrcObj.GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_最大)])
            {
                g.DrawImage(MSImages.小三角_0_0, m_Rectangle[3]);
            }
            else if (SrcObj.OutBoolList[SrcObj.GetOutBoolIndex(OutBoolKeys.Oub网侧电流_300A)])
            {
                g.DrawImage(MSImages.小三角_0_0, m_Rectangle[0]);
            }
            else if (SrcObj.OutBoolList[SrcObj.GetOutBoolIndex(OutBoolKeys.Oub网侧电流_400A)])
            {
                g.DrawImage(MSImages.小三角_0_0, m_Rectangle[1]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="trainGroupNumb">动车组号, 0, 1</param>
        /// <param name="value"></param>
        private void DrawVoltage(Graphics g, int trainGroupNumb, float value)
        {
            if (value > 0)
            {
                g.FillRectangle(SolidBrushsItems.BlueBrush1, m_RectsList[12 + trainGroupNumb*2]);
                g.DrawRectangle(SrcObj.DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                    Rectangle.Round(m_RectsList[12 + trainGroupNumb*2]));
            }
            if (value >= 16000)
            {
                m_ChangingRect[trainGroupNumb].DrawRectangle(g,
                    value - 16000,
                    SolidBrushsItems.BlueBrush1,
                    SrcObj.DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
            }
        }

        private void PaintState(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnPaint(g));


            for (int i = 0; i < (SrcObj.DMITitle.MarshallMode ? 4 : 2); i++)
            {
                //充电机1
                if (SrcObj.BoolList[m_DynamoDictionary1[i + 1]])
                {
                    g.DrawImage(CommonImages.StateUnkown, m_ChargingGeneratorRegions[i]);
                }
                else if (m_BValue[i*2])
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.xfk_1_0 : MSImages.xfk_0_0,
                        m_ChargingGeneratorRegions[i]);
                }
                else if (m_BValue[1 + i*2])
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.xfk_1_1 : MSImages.xfk_0_1,
                        m_ChargingGeneratorRegions[i]);
                }
                else
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.xfk_1_2 : MSImages.xfk_0_2,
                        m_ChargingGeneratorRegions[i]);
                }
            }
        }

        private void PaintGroundImage(Graphics g)
        {
            var volImage = SrcObj.DMITitle.MarshallMode ? MSImages._380网压_0 : MSImages._3C网压_0;
            g.DrawImage(volImage, m_RectsList[2].X, m_RectsList[2].Y, volImage.Width, volImage.Height);
            g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.powerCurrent_1 : MSImages.powerCurrent_0, m_RectsList[3]);
        }
    }
}