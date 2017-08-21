using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.传动制动
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMIDrivingBraking : CRH3C380BBase
    {

        private static readonly string[] TowBrakeNames = new[]
        {
            InFloatKeys.Inf牵引制动百分比1,
            InFloatKeys.Inf牵引制动百分比2,
            InFloatKeys.Inf牵引制动百分比3,
            InFloatKeys.Inf牵引制动百分比4,
            InFloatKeys.Inf牵引制动百分比5,
            InFloatKeys.Inf牵引制动百分比6,
            InFloatKeys.Inf牵引制动百分比7,
            InFloatKeys.Inf牵引制动百分比8,
        };

        // 充电机
        private List<ChargingGeneratorControl> m_ChargingGeneratorControls;

        private List<RectangleF> m_RectsList;

        private readonly NeedChangeLength[] m_ChangingRect = new NeedChangeLength[18];

        private readonly string[] m_BtnStr = {"", "", "", "", "", "", "", "", "", "主页面"};

        private List<CommonInnerControlBase> m_Infos;

        private void SetVisible(IEnumerable<CommonInnerControlBase> lst)
        {
            foreach (var t in lst)
            {
                if (t is Line && t.Tag != null && (int) t.Tag == 2)
                {
                    t.Visible = DMITitle.MarshallMode;
                    continue;
                }

                if (t is GDIRectText && t.Tag != null)
                {
                    switch ((int) t.Tag)
                    {
                        case 1:
                            t.Visible = !DMITitle.MarshallMode;
                            break;
                        case 2:
                            t.Visible = DMITitle.MarshallMode;
                            break;
                    }
                }
            }
        }

        private void SetLocation(Point defatPoint)
        {
            if ((((Line) m_Infos[0]).StartPoint == defatPoint) && DMITitle.MarshallMode)
            {
                foreach (var t in m_Infos)
                {
                    var line = t as Line;
                    if (line != null)
                    {
                        line.StartPoint = new Point(line.StartPoint.X + 30, line.StartPoint.Y);
                        line.EndPoint = new Point(line.EndPoint.X + 30, line.EndPoint.Y);
                    }
                    else
                    {
                        t.Location = new Point(t.Location.X + 30, t.Location.Y);
                    }

                }
            }
            else if ((((Line) m_Infos[0]).StartPoint != defatPoint) && !DMITitle.MarshallMode)
            {
                foreach (var t in m_Infos)
                {
                    var line = t as Line;
                    if (line != null)
                    {
                        line.StartPoint = new Point(line.StartPoint.X - 30, line.StartPoint.Y);
                        line.EndPoint = new Point(line.EndPoint.X - 30, line.EndPoint.Y);
                    }
                    else
                    {
                        t.Location = new Point(t.Location.X - 30, t.Location.Y);
                    }

                }
            }


        }

        //2
        public override string GetInfo()
        {
            return "DMI传动制动";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

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
        }

        public override void Paint(Graphics g)
        {
            ResponseBtnEvent();

            m_Infos.ForEach(e => e.OnPaint(g));

            PaintGroundImage(g);

            DrawVolTitle(g);

            PaintRectangles(g);

            m_ChargingGeneratorControls.ForEach(e => e.OnPaint(g));
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0 && DMIButton.BtnUpList[0] == 15)
            {
                append_postCmd(CmdType.ChangePage, 11, 0, 0);
            }
        }

        private void PaintGroundImage(Graphics g)
        {
            //MSImages.
            //底图
            g.DrawImage(DMITitle.NightMode ? MSImages._3C主界面_1 : MSImages._3C主界面_0, m_RectsList[2]);
            //接触网电压
            if (DMITitle.MarshallMode)
            {
                g.DrawImage(DMITitle.NightMode ? MSImages.voltage_1_1 : MSImages.voltage_0_1, m_RectsList[3]);
            }
            else
            {
                g.DrawImage(DMITitle.NightMode ? MSImages.voltage_1_0 : MSImages.voltage_0_0, m_RectsList[3]);
            }

            //标题
            g.DrawString("牵引/制动", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void DrawVolTitle(Graphics g)
        {
            g.DrawString("接触网电压", FontsItems.FontE12,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[4], FontsItems.TheAlignment(FontRelated.靠左));
            g.DrawString(DMITitle.MarshallMode
                ? (DMITitle.TrainInSide ? "动车组 2" : " 动车组 1")
                : (DMITitle.TrainInSide ? " 8- 1" : " 1-8 "), FontsItems.FontE12,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[5], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void PaintRectangles(Graphics g)
        {
            //接触网电压条
            DrawVoltage(g, GetInFloatValue(InFloatKeys.Inf动车组1接触网电压), 0);
            ////////////////////////////////////////////////////////////////////////////////////////////////

            //牵引制动
            for (int i = 0; i < (DMITitle.MarshallMode ? 8 : 4); i++)
            {
                var tmp = GetValidatedTowBrake(i);

                if (GetInBoolValue(InBoolKeys.Inb工况牵引) || GetInBoolValue(InBoolKeys.Inb工况制动))
                {
                    (!DMITitle.MarshallMode ? m_ChangingRect[2 + i] : m_ChangingRect[10 + i]).DrawRectangle(g,
                        tmp,
                        GetInBoolValue(InBoolKeys.Inb工况牵引)
                            ? SolidBrushsItems.BlueBrush1
                            : SolidBrushsItems.YellowBrush1,
                        DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
                    g.DrawImage(
                        i%2 == 0
                            ? (GetInBoolValue(InBoolKeys.Inb工况牵引) ? MSImages.小三角_0_0 : MSImages.小三角_1_0)
                            : (GetInBoolValue(InBoolKeys.Inb工况牵引) ? MSImages.小三角_0_1 : MSImages.小三角_1_1),
                        !DMITitle.MarshallMode
                            ? new RectangleF(m_RectsList[20 + i*2].X,
                                m_RectsList[20 + i*2].Y + m_RectsList[21 + i*2].Height -
                                tmp*(m_RectsList[21 + i*2].Height/100),
                                m_RectsList[20 + i*2].Width, m_RectsList[20 + i*2].Height)
                            : new RectangleF(m_RectsList[36 + i*2].X,
                                m_RectsList[36 + i*2].Y + m_RectsList[37 + i*2].Height -
                                tmp*(m_RectsList[37 + i*2].Height/100),
                                m_RectsList[36 + i*2].Width, m_RectsList[36 + i*2].Height));
                }
                else
                {
                    g.DrawImage(i%2 == 0 ? MSImages.小三角_0_0 : MSImages.小三角_0_1,
                        !DMITitle.MarshallMode
                            ? new RectangleF(m_RectsList[20 + i*2].X,
                                m_RectsList[20 + i*2].Y + m_RectsList[21 + i*2].Height -
                                tmp*(m_RectsList[21 + i*2].Height/100),
                                m_RectsList[20 + i*2].Width, m_RectsList[20 + i*2].Height)
                            : new RectangleF(m_RectsList[36 + i*2].X,
                                m_RectsList[36 + i*2].Y + m_RectsList[37 + i*2].Height -
                                tmp*(m_RectsList[37 + i*2].Height/100),
                                m_RectsList[36 + i*2].Width, m_RectsList[36 + i*2].Height));
                }
            }
        }

        private float GetValidatedTowBrake(int carNo)
        {
            var tmp = GetInFloatValue(TowBrakeNames[carNo]);

            if (tmp > 100)
            {
                tmp = 100;
            }
            else if (tmp < 0)
            {
                tmp = 0;
            }
            return tmp;
        }

        private void DrawVoltage(Graphics g, float vol, int groupNo)
        {
            if (vol > 0)
            {
                g.FillRectangle(SolidBrushsItems.BlueBrush1, m_RectsList[7 + groupNo*2]);
                g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                    Rectangle.Round(m_RectsList[7 + groupNo*2]));
            }
            if (vol >= 16000)
            {
                m_ChangingRect[groupNo].DrawRectangle(g, vol - 16000,
                    SolidBrushsItems.BlueBrush1,
                    DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen);
            }
        }

        private void InitData()
        {
            InitalizeBackgroud();

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIDrivingBraking, ref m_RectsList))
            {
            }

            InitalizeChangingRect();

            InitalizeChargingGeneratorControl();
        }

        private void InitalizeBackgroud()
        {
            var tmpColor = Color.White;
            var loction = new Point(186, 59);
            const int length1 = 377;
            const int length2 = 373;
            const int height = 28;
            const int length3 = 145;
            const int length5 = 36;
            const int length4 = length2 - length3;
            const int length6 = length2 - length3 - length5;
            var size = new Size(130, height);
            m_Infos = new List<CommonInnerControlBase>();

            var line1 = new Line(loction, new Point(loction.X, loction.Y + length1)) {Tag = 1, Color = tmpColor};
            m_Infos.Add(line1);

            // 动车组 2 左边竖线
            var line2 = new Line(new Point(333, loction.Y), new Point(333, loction.Y + length1))
            {
                Tag = 2,
                Color = tmpColor
            };
            m_Infos.Add(line2);

            AddTrainGroups(line1, tmpColor, line2);


            var tempEnd = line1.EndPoint;

            for (int i = 0; i < 11; i++)
            {
                var changge = (i == 10 ? 3 : i == 5 ? 1 : 0);
                var tmpLine1 = new Line(new Point(tempEnd.X, tempEnd.Y - height*i + changge),
                    new Point(tempEnd.X + (i%5 == 0 ? length4 : length6), tempEnd.Y - height*i + changge))
                {
                    Tag = 1,
                    Color = tmpColor
                };
                var tmpLine2 = new Line(tmpLine1.EndPoint, new Point(tmpLine1.EndPoint.X + length3, tmpLine1.EndPoint.Y))
                {
                    Tag = 2,
                    Color = tmpColor
                };
                m_Infos.Add(tmpLine1);
                m_Infos.Add(tmpLine2);

                if (i%5 == 0)
                {
                    AddDegrees(i, tmpLine1, size, FontsItems.TheAlignment(FontRelated.靠右下), tmpLine2);
                }
            }

            SetVisible(m_Infos);

            SetLocation(loction);

            DMITitle.MarshallModeChanged += m =>
            {
                SetVisible(m_Infos);
                SetLocation(loction);
            };
        }

        private void AddTrainGroups(Line line1, Color tmpColor, Line line2)
        {
            var txtSize = new Size(116, 39);
            m_Infos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(line1.StartPoint, txtSize),
                TextColor = tmpColor,
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 2,
                TextFormat = FontsItems.TheAlignment(FontRelated.靠左上),
                Text = "动车组 1",
            });
            m_Infos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(line2.StartPoint, txtSize),
                TextColor = tmpColor,
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 2,
                TextFormat = FontsItems.TheAlignment(FontRelated.靠左上),
                Text = "动车组 2",
            });
        }

        private void AddDegrees(int i, Line tmpLine1, Size size, StringFormat format, Line tmpLine2)
        {
            switch (i)
            {
                case 0:
                    AddDegree0(tmpLine1, size, format, tmpLine2);
                    break;
                case 5:
                    AddDegree50(tmpLine1, size, format, tmpLine2);
                    break;
                default:
                    AddDegree100(tmpLine1, size, format, tmpLine2);
                    break;
            }
        }

        private void AddDegree0(Line tmpLine1, Size size, StringFormat format, Line tmpLine2)
        {
            var tmp = new GDIRectText
            {
                Text = "0",
                OutLineRectangle =
                    new Rectangle(
                        new Point(tmpLine1.EndPoint.X - size.Width, tmpLine1.EndPoint.Y - size.Height), size),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 1,
                TextFormat = format
            };
            var tmp1 = new GDIRectText
            {
                Text = "%",
                OutLineRectangle =
                    new Rectangle(new Point(tmpLine1.EndPoint.X - size.Width, tmpLine1.EndPoint.Y), size),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 1,
                TextFormat = FontsItems.TheAlignment(FontRelated.靠右上)
            };
            var tmp2 = new GDIRectText
            {
                Text = "0",
                OutLineRectangle =
                    new Rectangle(
                        new Point(tmpLine2.EndPoint.X - size.Width, tmpLine2.EndPoint.Y - size.Height), size),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 2,
                TextFormat = format
            };
            var tmp3 = new GDIRectText
            {
                Text = "%",
                OutLineRectangle =
                    new Rectangle(new Point(tmpLine2.EndPoint.X - size.Width, tmpLine2.EndPoint.Y), size),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 2,
                TextFormat = FontsItems.TheAlignment(FontRelated.靠右上)
            };
            m_Infos.Add(tmp);
            m_Infos.Add(tmp1);
            m_Infos.Add(tmp2);
            m_Infos.Add(tmp3);
        }

        private void AddDegree50(Line tmpLine1, Size size, StringFormat format, Line tmpLine2)
        {
            var tempText1 = new GDIRectText
            {
                Text = "50",
                OutLineRectangle =
                    new Rectangle(
                        new Point(tmpLine1.EndPoint.X - size.Width, tmpLine1.EndPoint.Y - size.Height), size),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 1,
                TextFormat = format
            };
            var tempText = new GDIRectText
            {
                Text = "50",
                OutLineRectangle =
                    new Rectangle(
                        new Point(tmpLine2.EndPoint.X - size.Width, tmpLine2.EndPoint.Y - size.Height), size),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 2,
                TextFormat = format
            };
            m_Infos.Add(tempText);
            m_Infos.Add(tempText1);
        }

        private void AddDegree100(Line tmpLine1, Size size, StringFormat format, Line tmpLine2)
        {
            var tempText1 = new GDIRectText
            {
                Text = "100",
                OutLineRectangle =
                    new Rectangle(
                        new Point(tmpLine1.EndPoint.X - size.Width, tmpLine1.EndPoint.Y - size.Height), size),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 1,
                TextFormat = format
            };
            var tempText = new GDIRectText
            {
                Text = "100",
                OutLineRectangle =
                    new Rectangle(
                        new Point(tmpLine2.EndPoint.X - size.Width, tmpLine2.EndPoint.Y - size.Height), size),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 2,
                TextFormat = format
            };
            m_Infos.Add(tempText);
            m_Infos.Add(tempText1);
        }

        private void InitalizeChargingGeneratorControl()
        {
            var first = new Rectangle(240, 455, 36, 36);
            m_ChargingGeneratorControls = new List<ChargingGeneratorControl>();

            // 8车时第一个
            var x0On8 = (int) m_ChangingRect[3 + 2*0].TheRectangleF.X + 13;


            for (int i = 0; i < 4; i++)
            {
                var contorl = new ChargingGeneratorControl(this)
                {
                    StateNames =
                        new[]
                        {
                            new Tuple<string, ChargingGeneratorControl.ChargingGeneratorState>(
                                string.Format("充电机{0}-状态未知", i + 1),
                                ChargingGeneratorControl.ChargingGeneratorState.Unkown),
                            new Tuple<string, ChargingGeneratorControl.ChargingGeneratorState>(
                                string.Format("充电机{0}-蓝", i + 1), ChargingGeneratorControl.ChargingGeneratorState.Blue),
                            new Tuple<string, ChargingGeneratorControl.ChargingGeneratorState>(
                                string.Format("充电机{0}-黄", i + 1), ChargingGeneratorControl.ChargingGeneratorState.Yellow),
                        },
                    OutLineRectangle =
                        new Rectangle((int) m_ChangingRect[3 + 2*i].TheRectangleF.X + 13, first.Y, first.Width,
                            first.Height),
                };
                m_ChargingGeneratorControls.Add(contorl);
            }

            UpdateChanginItemLocationAndVisble(x0On8);
            DMITitle.MarshallModeChanged += title =>
            {
                UpdateChanginItemLocationAndVisble(x0On8);
            };

            DMITitle.TrainInsideChanged += title =>
            {
                var visItem = m_ChargingGeneratorControls.Where(w => w.Visible).ToList();

                var orderBy = visItem.OrderBy(o => o.OutLineRectangle.X).Select(s => s.OutLineRectangle.X).ToList();
                if (title.TrainInSide)
                {
                    orderBy =
                        visItem.OrderByDescending(o => o.OutLineRectangle.X).Select(s => s.OutLineRectangle.X).ToList();
                }
                for (int i = 0; i < visItem.Count; i++)
                {
                    visItem[i].OutLineRectangle = new Rectangle(orderBy[i],
                        visItem[i].OutLineRectangle.Y,
                        visItem[i].OutLineRectangle.Width,
                        visItem[i].OutLineRectangle.Height);
                }
            };
        }

        private void UpdateChanginItemLocationAndVisble(int x0On8)
        {
            const int offset = 30;
            var off = 0;
            var x0 = m_ChargingGeneratorControls.Min(m => m.OutLineRectangle.X);
            if (DMITitle.MarshallMode)
            {
                if (x0 < x0On8)
                {
                    off = offset;
                }

                m_ChargingGeneratorControls.ForEach(e => e.Visible = true);
            }
            else
            {
                if (x0 >= x0On8)
                {
                    off = -offset;
                }

                foreach (var c in m_ChargingGeneratorControls.Skip(2))
                {
                    c.Visible = false;
                }
            }

            m_ChargingGeneratorControls.ForEach(e => e.Location = new Point(e.Location.X + off, e.Location.Y));
        }

        private void InitalizeChangingRect()
        {
            for (var i = 0; i < 2; i++)
            {
                m_ChangingRect[i] = new NeedChangeLength(m_RectsList[6 + i*2], 16000f, RectRiseDirection.上,
                    false);
            }
            for (var i = 0; i < 8; i++)
            {
                m_ChangingRect[2 + i] = new NeedChangeLength(m_RectsList[21 + i*2], 100f, RectRiseDirection.上,
                    false);
            }
            for (var i = 0; i < 8; i++)
            {
                m_ChangingRect[10 + i] = new NeedChangeLength(m_RectsList[37 + i*2], 100f, RectRiseDirection.上,
                    false);
            }
        }
    }
}