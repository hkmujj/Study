using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.基本画面
{
    public class BasePage380B : BasePageContent
    {
        public BasePage380B(DMIBasePage srcObj) : base(srcObj)
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
            for (var index = 0; index < m_FValue.Length; index++)
            {
                if (SrcObj.DMITitle.TrainInSide)
                {
                    m_FValue[index] =
                        SrcObj.FloatList[
                            SrcObj.GetInFloatIndex(m_FValueAllArrange[SrcObj.DMITitle.MarshallMode ? 1 : 0][index])];
                }
                else
                {
                    m_FValue[index] = SrcObj.FloatList[SrcObj.GetInFloatIndex(m_RawFValueAllArrange[index])];
                }
            }
        }


        public override void Init()
        {
            m_ChangingRect = new NeedChangeLength[11];
            BtnStr = new[] {"维护", "系统", "门", "ATP\n系统", "紧急", "准备整备", "", "自动速度控制", "开关", ""};

            int[] tmp = {1, 2, 3, 4};
            m_DynamoDictionary1 = tmp.ToDictionary(num => num,
                num => SrcObj.GetInBoolIndex(string.Format("接触网电压充电机{0}-状态未知", num)));
            m_DynamoDictionary2 = tmp.ToDictionary(num => num,
                num => SrcObj.GetInBoolIndex(string.Format("充电机{0}-状态未知", num)));
            int index;

            #region :::::::

            m_RawBValueArrange = new List<string>
            {
                InBoolKeys.Inb牵引_受电弓_1_升弓,
                InBoolKeys.Inb牵引_受电弓_1_降弓,
                InBoolKeys.Inb牵引_受电弓_1_切除,
                InBoolKeys.Inb牵引_受电弓_2_升弓,
                InBoolKeys.Inb牵引_受电弓_2_降弓,
                InBoolKeys.Inb牵引_受电弓_2_切除,
                InBoolKeys.Inb牵引_受电弓_3_升弓,
                InBoolKeys.Inb牵引_受电弓_3_降弓,
                InBoolKeys.Inb牵引_受电弓_3_切除,
                InBoolKeys.Inb牵引_受电弓_4_升弓,
                InBoolKeys.Inb牵引_受电弓_4_降弓,
                InBoolKeys.Inb牵引_受电弓_4_切除,
                InBoolKeys.Inb牵引_主断_1_闭合,
                InBoolKeys.Inb牵引_主断_1_断开,
                InBoolKeys.Inb牵引_主断_1_切除,
                InBoolKeys.Inb牵引_主断_2_闭合,
                InBoolKeys.Inb牵引_主断_2_断开,
                InBoolKeys.Inb牵引_主断_2_切除,
                InBoolKeys.Inb牵引_主断_3_闭合,
                InBoolKeys.Inb牵引_主断_3_断开,
                InBoolKeys.Inb牵引_主断_3_切除,
                InBoolKeys.Inb牵引_主断_4_闭合,
                InBoolKeys.Inb牵引_主断_4_断开,
                InBoolKeys.Inb牵引_主断_4_切除,
                InBoolKeys.Inb接触网电压充电机1_蓝,
                InBoolKeys.Inb接触网电压充电机1_黄,
                InBoolKeys.Inb接触网电压充电机2_蓝,
                InBoolKeys.Inb接触网电压充电机2_黄,
                InBoolKeys.Inb接触网电压充电机3_蓝,
                InBoolKeys.Inb接触网电压充电机3_黄,
                InBoolKeys.Inb接触网电压充电机4_蓝,
                InBoolKeys.Inb接触网电压充电机4_黄,
                InBoolKeys.Inb充电机1_蓝,
                InBoolKeys.Inb充电机1_黄,
                InBoolKeys.Inb充电机2_蓝,
                InBoolKeys.Inb充电机2_黄,
                InBoolKeys.Inb充电机3_蓝,
                InBoolKeys.Inb充电机3_黄,
                InBoolKeys.Inb充电机4_蓝,
                InBoolKeys.Inb充电机4_黄,
                InBoolKeys.Inb工况牵引,
                InBoolKeys.Inb工况制动,


            };

            m_BValueAllArrange = new List<string[]>
            {
                new[]
                {
                    InBoolKeys.Inb牵引_受电弓_2_升弓,
                    InBoolKeys.Inb牵引_受电弓_2_降弓,
                    InBoolKeys.Inb牵引_受电弓_2_切除,
                    InBoolKeys.Inb牵引_受电弓_1_升弓,
                    InBoolKeys.Inb牵引_受电弓_1_降弓,
                    InBoolKeys.Inb牵引_受电弓_1_切除,

                    InBoolKeys.Inb牵引_受电弓_4_升弓,
                    InBoolKeys.Inb牵引_受电弓_4_降弓,
                    InBoolKeys.Inb牵引_受电弓_4_切除,
                    InBoolKeys.Inb牵引_受电弓_3_升弓,
                    InBoolKeys.Inb牵引_受电弓_3_降弓,
                    InBoolKeys.Inb牵引_受电弓_3_切除,

                    InBoolKeys.Inb牵引_主断_2_闭合,
                    InBoolKeys.Inb牵引_主断_2_断开,
                    InBoolKeys.Inb牵引_主断_2_切除,
                    InBoolKeys.Inb牵引_主断_1_闭合,
                    InBoolKeys.Inb牵引_主断_1_断开,
                    InBoolKeys.Inb牵引_主断_1_切除,

                    InBoolKeys.Inb牵引_主断_4_闭合,
                    InBoolKeys.Inb牵引_主断_4_断开,
                    InBoolKeys.Inb牵引_主断_4_切除,
                    InBoolKeys.Inb牵引_主断_3_闭合,
                    InBoolKeys.Inb牵引_主断_3_断开,
                    InBoolKeys.Inb牵引_主断_3_切除,
                    InBoolKeys.Inb接触网电压充电机2_蓝,
                    InBoolKeys.Inb接触网电压充电机2_黄,
                    InBoolKeys.Inb接触网电压充电机1_蓝,
                    InBoolKeys.Inb接触网电压充电机1_黄,
                    InBoolKeys.Inb接触网电压充电机4_蓝,
                    InBoolKeys.Inb接触网电压充电机4_黄,
                    InBoolKeys.Inb接触网电压充电机3_蓝,
                    InBoolKeys.Inb接触网电压充电机3_黄,

                    InBoolKeys.Inb充电机2_蓝,
                    InBoolKeys.Inb充电机2_黄,
                    InBoolKeys.Inb充电机1_蓝,
                    InBoolKeys.Inb充电机1_黄,
                    InBoolKeys.Inb充电机4_蓝,
                    InBoolKeys.Inb充电机4_黄,
                    InBoolKeys.Inb充电机3_蓝,
                    InBoolKeys.Inb充电机3_黄,
                    InBoolKeys.Inb工况牵引,
                    InBoolKeys.Inb工况制动,

                },
                new[]
                {
                    InBoolKeys.Inb牵引_受电弓_4_升弓,
                    InBoolKeys.Inb牵引_受电弓_4_降弓,
                    InBoolKeys.Inb牵引_受电弓_4_切除,
                    InBoolKeys.Inb牵引_受电弓_3_升弓,
                    InBoolKeys.Inb牵引_受电弓_3_降弓,
                    InBoolKeys.Inb牵引_受电弓_3_切除,

                    InBoolKeys.Inb牵引_受电弓_2_升弓,
                    InBoolKeys.Inb牵引_受电弓_2_降弓,
                    InBoolKeys.Inb牵引_受电弓_2_切除,
                    InBoolKeys.Inb牵引_受电弓_1_升弓,
                    InBoolKeys.Inb牵引_受电弓_1_降弓,
                    InBoolKeys.Inb牵引_受电弓_1_切除,

                    InBoolKeys.Inb牵引_主断_4_闭合,
                    InBoolKeys.Inb牵引_主断_4_断开,
                    InBoolKeys.Inb牵引_主断_4_切除,
                    InBoolKeys.Inb牵引_主断_3_闭合,
                    InBoolKeys.Inb牵引_主断_3_断开,
                    InBoolKeys.Inb牵引_主断_3_切除,

                    InBoolKeys.Inb牵引_主断_2_闭合,
                    InBoolKeys.Inb牵引_主断_2_断开,
                    InBoolKeys.Inb牵引_主断_2_切除,
                    InBoolKeys.Inb牵引_主断_1_闭合,
                    InBoolKeys.Inb牵引_主断_1_断开,
                    InBoolKeys.Inb牵引_主断_1_切除,

                    InBoolKeys.Inb接触网电压充电机4_蓝,
                    InBoolKeys.Inb接触网电压充电机4_黄,
                    InBoolKeys.Inb接触网电压充电机3_蓝,
                    InBoolKeys.Inb接触网电压充电机3_黄,
                    InBoolKeys.Inb接触网电压充电机2_蓝,
                    InBoolKeys.Inb接触网电压充电机2_黄,
                    InBoolKeys.Inb接触网电压充电机1_蓝,
                    InBoolKeys.Inb接触网电压充电机1_黄,

                    InBoolKeys.Inb充电机4_蓝,
                    InBoolKeys.Inb充电机4_黄,
                    InBoolKeys.Inb充电机3_蓝,
                    InBoolKeys.Inb充电机3_黄,
                    InBoolKeys.Inb充电机2_蓝,
                    InBoolKeys.Inb充电机2_黄,
                    InBoolKeys.Inb充电机1_蓝,
                    InBoolKeys.Inb充电机1_黄,

                    InBoolKeys.Inb工况牵引,
                    InBoolKeys.Inb工况制动,


                }
            };

            #endregion

            m_BValue = new bool[m_RawBValueArrange.Count];

            #region :::::::

            m_RawFValueAllArrange = new List<string>
            {
                InFloatKeys.Inf动车组1接触网电压,
                InFloatKeys.Inf动车组2接触网电压,
                InFloatKeys.Inf网侧电流,
                InFloatKeys.Inf牵引制动百分比1,
                InFloatKeys.Inf牵引制动百分比2,
                InFloatKeys.Inf牵引制动百分比3,
                InFloatKeys.Inf牵引制动百分比4,
                InFloatKeys.Inf牵引制动百分比5,
                InFloatKeys.Inf牵引制动百分比6,
                InFloatKeys.Inf牵引制动百分比7,
                InFloatKeys.Inf牵引制动百分比8,

            };

            m_FValueAllArrange = new List<string[]>
            {
                new[]
                {
                    InFloatKeys.Inf动车组1接触网电压,
                    InFloatKeys.Inf动车组2接触网电压,
                    InFloatKeys.Inf网侧电流,
                    InFloatKeys.Inf牵引制动百分比4,
                    InFloatKeys.Inf牵引制动百分比3,
                    InFloatKeys.Inf牵引制动百分比2,
                    InFloatKeys.Inf牵引制动百分比1,
                    InFloatKeys.Inf牵引制动百分比8,
                    InFloatKeys.Inf牵引制动百分比7,
                    InFloatKeys.Inf牵引制动百分比6,
                    InFloatKeys.Inf牵引制动百分比5,
                },
                new[]
                {
                    InFloatKeys.Inf动车组2接触网电压,
                    InFloatKeys.Inf动车组1接触网电压,
                    InFloatKeys.Inf网侧电流,
                    InFloatKeys.Inf牵引制动百分比8,
                    InFloatKeys.Inf牵引制动百分比7,
                    InFloatKeys.Inf牵引制动百分比6,
                    InFloatKeys.Inf牵引制动百分比5, InFloatKeys.Inf牵引制动百分比4,
                    InFloatKeys.Inf牵引制动百分比3,
                    InFloatKeys.Inf牵引制动百分比2,
                    InFloatKeys.Inf牵引制动百分比1,
                }
            };

            #endregion

            m_FValue = new float[m_RawFValueAllArrange.Count];

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBasePage, ref m_RectsList))
            {

            }
            m_Rectangle = new List<RectangleF>
            {
                new RectangleF(212, 333, 23, 24),
                new RectangleF(212, 303, 23, 24),
                new RectangleF(212, 271, 23, 24),
                new RectangleF(212, 239, 23, 24),
                new RectangleF(212, 115, 23, 24),

            };
            for (index = 0; index < 2; index++)
            {
                m_ChangingRect[index] = new NeedChangeLength(m_RectsList[34 + index*2], 16000f,
                    RectRiseDirection.上, false);
            }
            m_ChangingRect[2] = new NeedChangeLength(m_RectsList[39], 1100f, RectRiseDirection.上, false);
            for (index = 0; index < 8; index++)
            {
                m_ChangingRect[3 + index] = new NeedChangeLength(m_RectsList[41 + index*2], 100f,
                    RectRiseDirection.上, false);
            }
            m_ControlCollection = new List<CommonInnerControlBase>
            {
                new Line(new Point(81, 85), new Point(81, 433))
                {
                    Color = SrcObj.DMITitle.NightMode ? Color.Black : Color.White,
                    Tag = 1,
                    Visible = false
                },
                new Line(new Point(510, 39), new Point(510, 195))
                {
                    Color = SrcObj.DMITitle.NightMode ? Color.Black : Color.White,
                    Tag = 2,
                    Visible = false
                },
                new Line(new Point(510, 203), new Point(510, 435))
                {
                    Color = SrcObj.DMITitle.NightMode ? Color.Black : Color.White,
                    Tag = 3,
                    Visible = false
                }
            };

            SrcObj.DMITitle.MarshallModeChanged += m =>
            {
                m_ControlCollection[m_ControlCollection.FindIndex(f => f is Line && (int) f.Tag == 1)].Visible =
                    SrcObj.DMITitle.MarshallMode;
                m_ControlCollection[m_ControlCollection.FindIndex(f => f is Line && (int) f.Tag == 2)].Visible =
                    SrcObj.DMITitle.MarshallMode;
                m_ControlCollection[m_ControlCollection.FindIndex(f => f is Line && (int) f.Tag == 3)].Visible =
                    SrcObj.DMITitle.MarshallMode;

            };
        }

        public override void OnDraw(Graphics g)
        {
            PaintGroundImage(g);

            PaintState(g);

            PaintRectangles(g);
        }


        private void PaintRectangles(Graphics gp)
        {
            //接触网电压条
            for (int i = 0; i < (SrcObj.DMITitle.MarshallMode ? 2 : 1); i++)
            {
                if (m_FValue[i] > 0)
                {
                    gp.FillRectangle(SolidBrushsItems.BlueBrush1, m_RectsList[35 + i*2]);
                    gp.DrawRectangle(SrcObj.DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                        m_RectsList[35 + i*2].X, m_RectsList[35 + i*2].Y,
                        m_RectsList[35 + i*2].Width, m_RectsList[35 + i*2].Height);
                }
                if (m_FValue[i] >= 16000)
                {
                    m_ChangingRect[i].DrawRectangle(gp, m_FValue[i] - 16000, SolidBrushsItems.BlueBrush1,
                        SrcObj.DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////
            //网侧电流条
            m_ChangingRect[2].DrawRectangle(gp, m_FValue[2], SolidBrushsItems.BlueBrush1,
                SrcObj.DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
            //小三角位置
            if (SrcObj.OutBoolList[SrcObj.GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_600A)])
            {
                gp.DrawImage(MSImages.小三角_0_0, m_Rectangle[2]);
            }
            else if (SrcObj.OutBoolList[SrcObj.GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_最大)])
            {
                gp.DrawImage(MSImages.小三角_0_0, m_Rectangle[3]);
            }
            else if (SrcObj.OutBoolList[SrcObj.GetOutBoolIndex(OutBoolKeys.Oub网侧电流_300A)])
            {
                gp.DrawImage(MSImages.小三角_0_0, m_Rectangle[0]);
            }
            else if (SrcObj.OutBoolList[SrcObj.GetOutBoolIndex(OutBoolKeys.Oub网侧电流_400A)])
            {
                gp.DrawImage(MSImages.小三角_0_0, m_Rectangle[1]);
            }
            //牵引制动
            if (SrcObj.GetInBoolValue(InBoolKeys.Inb工况牵引) || SrcObj.GetInBoolValue(InBoolKeys.Inb工况制动))
            {
                for (int i = 0; i < (SrcObj.DMITitle.MarshallMode ? 8 : 4); i++)
                {
                    if (!(m_FValue[3 + i] >= 0))
                    {
                        continue;
                    }
                    m_ChangingRect[3 + i].DrawRectangle(gp, m_FValue[3 + i],
                        m_BValue[40] ? SolidBrushsItems.BlueBrush1 : SolidBrushsItems.YellowBrush1,
                        SrcObj.DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
                    gp.DrawImage(
                        i%2 == 0
                            ? (m_BValue[40] ? MSImages.小三角_0_0 : MSImages.小三角_1_0)
                            : (m_BValue[40] ? MSImages.小三角_0_1 : MSImages.小三角_1_1),
                        new RectangleF(m_RectsList[40 + i*2].X,
                            m_RectsList[40 + i*2].Y + m_RectsList[41 + i*2].Height -
                            (m_FValue[3 + i] > 100 ? 100 : m_FValue[3 + i])*(m_RectsList[41 + i*2].Height/100),
                            m_RectsList[40 + i*2].Width, m_RectsList[40 + i*2].Height));
                }
            }
            else
            {
                for (int i = 0; i < (SrcObj.DMITitle.MarshallMode ? 8 : 4); i++)
                {
                    gp.DrawImage(i%2 == 0 ? MSImages.小三角_0_0 : MSImages.小三角_0_1,
                        new RectangleF(m_RectsList[40 + i*2].X,
                            m_RectsList[40 + i*2].Y + m_RectsList[41 + i*2].Height,
                            m_RectsList[40 + i*2].Width, m_RectsList[40 + i*2].Height));
                }

            }
            m_ControlCollection.ForEach(g => g.OnDraw(gp));
        }

        private void PaintState(Graphics g)
        {
            g.DrawString("接触网电压", FontsItems.FontC11,
                SrcObj.DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[19], FontsItems.TheAlignment(FontRelated.靠左));
            g.DrawString(SrcObj.DMITitle.MarshallMode
                ? (SrcObj.DMITitle.TrainInSide ? "28-21   18-11" : "11-18   21-28")
                : (SrcObj.DMITitle.TrainInSide ? "18-11" : "11-18"), FontsItems.FontE12,
                SrcObj.DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[20], FontsItems.TheAlignment(FontRelated.靠左));
            g.DrawString("网侧电流", FontsItems.FontC11,
                SrcObj.DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[21], FontsItems.TheAlignment(FontRelated.靠左));
            g.DrawString("", FontsItems.FontC11,
                SrcObj.DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[19], FontsItems.TheAlignment(FontRelated.靠左));

            g.DrawString(SrcObj.DMITitle.MarshallMode
                ? (SrcObj.DMITitle.TrainInSide
                    ? "    27            22             17             12          "
                    : "    12            17             22             27    ")
                : (SrcObj.DMITitle.TrainInSide
                    ? "    17            12    "
                    : "    12            17    "), FontsItems.FontE12,
                SrcObj.DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[70], FontsItems.TheAlignment(FontRelated.靠左));

            g.DrawString(SrcObj.DMITitle.MarshallMode
                ? (SrcObj.DMITitle.TrainInSide
                    ? "28    26      23    21     18    16      13     11"
                    : "11    13      16    18     21    23      26     28")
                : (SrcObj.DMITitle.TrainInSide
                    ? " 18    16      13    11"
                    : " 11    13      16    18"), FontsItems.FontE12,
                SrcObj.DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[71], FontsItems.TheAlignment(FontRelated.靠左));

            ////////////////////////////////////////////////////////////////////////////////////////////////
            for (int i = 0; i < (SrcObj.DMITitle.MarshallMode ? 4 : 2); i++)
            {
                //受电弓
                if (m_BValue[2 + i*3]) //切除
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.受电弓切除_1 : MSImages.受电弓切除_0, m_RectsList[11 + i]);
                }
                else if (m_BValue[i*3]) //升弓
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.升弓_1 : MSImages.升弓_0, m_RectsList[11 + i]);
                }
                else if (m_BValue[1 + i*3]) //降弓
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.降弓_1 : MSImages.降弓_0, m_RectsList[11 + i]);
                }
                else
                {
                    g.DrawImage(MSImages.未知, m_RectsList[11 + i]);
                }

                //主断
                if (m_BValue[14 + i*3]) //切除
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.主断切除_1 : MSImages.主断切除_0, m_RectsList[15 + i]);
                }
                else if (m_BValue[12 + i*3]) //主断合
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.主断合_1 : MSImages.主断合_0, m_RectsList[15 + i]);
                }
                else if (m_BValue[13 + i*3]) //主断断
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.主断断_1 : MSImages.主断断_0, m_RectsList[15 + i]);
                }
                else
                {
                    g.DrawImage(MSImages.未知, m_RectsList[15 + i]);
                }

                //充电机1
                if (SrcObj.BoolList[m_DynamoDictionary1[i + 1]])
                {
                    g.DrawImage(CommonImages.StateUnkown, m_RectsList[3 + i]);
                }
                else if (m_BValue[24 + i*2])
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.xfk_1_0 : MSImages.xfk_0_0, m_RectsList[3 + i]);
                }
                else if (m_BValue[25 + i*2])
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.xfk_1_1 : MSImages.xfk_0_1, m_RectsList[3 + i]);
                }
                else
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.xfk_1_2 : MSImages.xfk_0_2, m_RectsList[3 + i]);
                }

                //充电机2
                if (SrcObj.BoolList[m_DynamoDictionary2[i + 1]])
                {
                    g.DrawImage(CommonImages.StateUnkown, m_RectsList[7 + i]);
                }
                else if (m_BValue[32 + i*2])
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.小方块_1_0 : MSImages.小方块_0_0, m_RectsList[7 + i]);
                }
                else if (m_BValue[33 + i*2])
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.小方块_1_1 : MSImages.小方块_0_1, m_RectsList[7 + i]);
                }
                else
                {
                    g.DrawImage(SrcObj.DMITitle.NightMode ? MSImages.小方块_1_2 : MSImages.小方块_0_2, m_RectsList[7 + i]);
                }
            }
        }

        private void PaintGroundImage(Graphics g)
        {
            if (SrcObj.DMITitle.NightMode)
            {
                g.DrawImage(SrcObj.DMITitle.MarshallMode ? MSImages.voltage_1_1 : MSImages.voltage_1_0, m_RectsList[0]);
                g.DrawImage(MSImages.powerCurrent_1, m_RectsList[1]);
                g.DrawImage(SrcObj.DMITitle.MarshallMode ? MSImages.牵引图_1_1 : MSImages.牵引图_1_0, m_RectsList[2]);
            }
            else
            {
                g.DrawImage(SrcObj.DMITitle.MarshallMode ? MSImages.voltage_0_1 : MSImages.voltage_0_0, m_RectsList[0]);
                g.DrawImage(MSImages.powerCurrent_0, m_RectsList[1]);
                g.DrawImage(SrcObj.DMITitle.MarshallMode ? MSImages.牵引图_0_1 : MSImages.牵引图_0_0, m_RectsList[2]);
            }

            g.DrawString("主页面", FontsItems.FontC11,
                SrcObj.DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[67], FontsItems.TheAlignment(FontRelated.靠左));
        }
    }
}