using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.维修
{
    [GksDataType(DataType.isMMIObjectClass)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DMITractionDC_Link : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        private float[] m_FValue;

        private readonly NeedChangeLength[] m_ChangingRect = new NeedChangeLength[16];

        private readonly string[] m_BtnStr =
        {
            "",
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

        private Dictionary<int, int> m_Index;
        private readonly int[] m_FloatIndex = {1, 3, 6, 8, 9, 11, 14, 16};

        private List<CommonInnerControlBase> m_Collection;

        //2
        public override string GetInfo()
        {
            return "DMI中间直流环节";
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
            GetValue();
            Draw(g);
            for (int i = 0; i < m_ChangingRect.Length; i++)
            {
                m_ChangingRect[i].DrawRectangle(g, m_FValue[i], SolidBrushsItems.BlueBrush1, PenItems.WhiltPen);
            }
        }

        private void Draw(Graphics g)
        {
            PaintState(g);

            m_Collection.ForEach(f => f.OnDraw(g));
        }

        private void GetValue()
        {
            ResponseBtnEvent();

            foreach (var key in m_Index.Keys)
            {
                m_FValue[key - 1] = FloatList[m_Index[key]];
                //m_ChangingRect[key - 1].TmpStrpLength = FloatList[m_Index[key]];
            }
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0:
                        append_postCmd(CmdType.ChangePage, 110, 0, 0); //维护
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0); //
                        break;
                }
            }
        }

        private void PaintState(Graphics g)
        {
            g.DrawString("维护; 中间直流环节", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void InitData()
        {
            m_FValue = new float[16];
            m_Index =
                (m_FloatIndex.ToDictionary(f => f,
                    f => GetInFloatIndex(string.Format("中间直流环节电压---{0}", f.ToString("00")))))
                ;
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMITractionDcLink, ref m_RectsList))
            {

            }
            m_Collection = new List<CommonInnerControlBase>
            {
                new GDIRectText
                {
                    Text = "中间直流环节电压",
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    OutLineRectangle = new Rectangle(100, 110, 200, 30)
                }
            };
            for (int i = 0; i < 16; i++)
            {
                m_ChangingRect[i] = new NeedChangeLength(m_RectsList[13 + i], 4000f, RectRiseDirection.上, false);
                if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
                {
                    m_Collection.Add(new GDIRectText
                    {
                        Text = (i + 1).ToString("00"),
                        NeedDarwOutline = false,
                        BackColorVisible = false,
                        OutLineRectangle =
                            new Rectangle((int) m_RectsList[13 + i].X, 205, (int) m_RectsList[13 + i].Width,
                                (int) m_RectsList[13 + i].Width),
                        TextFormat = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        }
                    });
                }
                else
                {
                    m_Collection.Add(new GDIRectText
                    {
                        Text =
                            GlobalParam.Instance.ProjectType == ProjectType.CRH3C
                                ? (i + 1 + (i > 7 ? 2 : 0)).ToString("0")
                                : (i + 11 + (i > 7 ? 2 : 0)).ToString("00"),
                        NeedDarwOutline = false,
                        Tag = i >= 8 ? 2 : 1,
                        BackColorVisible = false,
                        OutLineRectangle =
                            new Rectangle((int) m_RectsList[13 + i].X, 205, (int) m_RectsList[13 + i].Width,
                                (int) m_RectsList[13 + i].Width),
                        TextFormat = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        }
                    });
                }

            }

            InitLine();
        }

        /// <summary>
        /// 初始化线条
        /// </summary>
        private void InitLine()
        {
            var lcPoint = new Point(90, 110);
            const int lcLength = 275;
            const int lcLeng2 = 310;
            const int lcLength3 = 600 - lcLeng2;
            const int heghts = 35;
            const int length4 = 211;
            var line1 = new Line(lcPoint, new Point(lcPoint.X, lcPoint.Y + lcLength));
            var line2 = new Line(line1.EndPoint, new Point(line1.EndPoint.X + lcLeng2, line1.EndPoint.Y));
            var line3 = new Line(new Point(line2.StartPoint.X, line2.StartPoint.Y - heghts),
                new Point(line2.EndPoint.X, line2.EndPoint.Y - heghts));
            var line4 = new Line(new Point(line3.StartPoint.X, line3.StartPoint.Y - heghts),
                new Point(line3.EndPoint.X, line3.EndPoint.Y - heghts));
            var line5 = new Line(new Point(line4.StartPoint.X, line4.StartPoint.Y - heghts),
                new Point(line4.EndPoint.X, line4.EndPoint.Y - heghts));
            var line6 = new Line(new Point(line5.StartPoint.X, line5.StartPoint.Y - heghts),
                new Point(line5.EndPoint.X, line5.EndPoint.Y - heghts));
            var line7 = new Line(line2.EndPoint, new Point(line2.EndPoint.X + lcLength3, line2.EndPoint.Y))
            {
                Visible = DMITitle.MarshallMode,
            };
            var line8 = new Line(line3.EndPoint, new Point(line3.EndPoint.X + lcLength3, line3.EndPoint.Y))
            {
                Visible = DMITitle.MarshallMode,
            };
            var line9 = new Line(line4.EndPoint, new Point(line4.EndPoint.X + lcLength3, line4.EndPoint.Y))
            {
                Visible = DMITitle.MarshallMode,
            };
            var line10 = new Line(line5.EndPoint, new Point(line5.EndPoint.X + lcLength3, line5.EndPoint.Y))
            {
                Visible = DMITitle.MarshallMode,
            };
            var line11 = new Line(line6.EndPoint, new Point(line6.EndPoint.X + lcLength3, line6.EndPoint.Y))
            {
                Visible = DMITitle.MarshallMode,
            };
            var line12 = new Line(new Point(line1.EndPoint.X + lcLeng2 - 20, line1.EndPoint.Y),
                new Point(line1.EndPoint.X + lcLeng2 - 20, line1.EndPoint.Y - length4))
            {
                Visible = DMITitle.MarshallMode,
            };
            m_Collection.Add(line1);
            m_Collection.Add(line2);
            m_Collection.Add(line3);
            m_Collection.Add(line4);
            m_Collection.Add(line5);
            m_Collection.Add(line6);
            m_Collection.Add(line7);
            m_Collection.Add(line8);
            m_Collection.Add(line9);
            m_Collection.Add(line10);
            m_Collection.Add(line11);
            m_Collection.Add(line12);
            InitNum(line6, line5, line4, line3, line2, line11, line10, line9, line8, line7);
            if (GlobalParam.Instance.ProjectType != ProjectType.CRH380BL)
            {
                InitLineVisible(line7, line8, line9, line10, line11, line12);
            }
            else
            {
                line7.Visible = true;
                line8.Visible = true;
                line9.Visible = true;
                line10.Visible = true;
                line11.Visible = true;
                var tmp = m_Collection.FindAll(f => f is GDIRectText && f.Tag != null && (int) f.Tag == 3);
                tmp.ForEach(f => f.Visible = false);
            }
        }

        /// <summary>
        /// 设置右边的数字
        /// </summary>
        /// <param name="line6"></param>
        /// <param name="line5"></param>
        /// <param name="line4"></param>
        /// <param name="line3"></param>
        /// <param name="line2"></param>
        /// <param name="line11"></param>
        /// <param name="line10"></param>
        /// <param name="line9"></param>
        /// <param name="line8"></param>
        /// <param name="line7"></param>
        private void InitNum(Line line6, Line line5, Line line4, Line line3, Line line2, Line line11, Line line10,
            Line line9,
            Line line8, Line line7)
        {
            var size = new Size(130, 35);
            var format = FontsItems.TheAlignment(FontRelated.靠右下);
            m_Collection.Add(new GDIRectText
            {
                Text = "3",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = format,
                OutLineRectangle = new Rectangle(new Point(line6.EndPoint.X - size.Width, line6.EndPoint.Y), size),
                Tag = 3
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "2",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = format,
                OutLineRectangle = new Rectangle(new Point(line5.EndPoint.X - size.Width, line5.EndPoint.Y), size),
                Tag = 3
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "1",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = format,
                OutLineRectangle = new Rectangle(new Point(line4.EndPoint.X - size.Width, line4.EndPoint.Y), size),
                Tag = 3
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "0",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = format,
                OutLineRectangle = new Rectangle(new Point(line3.EndPoint.X - size.Width, line3.EndPoint.Y), size),
                Tag = 3
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "kv",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = FontsItems.TheAlignment(FontRelated.靠右上),
                OutLineRectangle = new Rectangle(new Point(line2.EndPoint.X - size.Width, line2.EndPoint.Y), size),
                Tag = 3
            });


            m_Collection.Add(new GDIRectText
            {
                Text = "3",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = format,
                OutLineRectangle = new Rectangle(new Point(line11.EndPoint.X - size.Width, line11.EndPoint.Y), size),
                Tag = 4
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "2",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = format,
                OutLineRectangle = new Rectangle(new Point(line10.EndPoint.X - size.Width, line10.EndPoint.Y), size),
                Tag = 4
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "1",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = format,
                OutLineRectangle = new Rectangle(new Point(line9.EndPoint.X - size.Width, line9.EndPoint.Y), size),
                Tag = 4
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "0",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = format,
                OutLineRectangle = new Rectangle(new Point(line8.EndPoint.X - size.Width, line8.EndPoint.Y), size),
                Tag = 4
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "kv",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextFormat = FontsItems.TheAlignment(FontRelated.靠右上),
                OutLineRectangle = new Rectangle(new Point(line7.EndPoint.X - size.Width, line7.EndPoint.Y), size),
                Tag = 4
            });
        }

        /// <summary>
        /// CRH380B/3C设置部分线条和数字的显示
        /// </summary>
        /// <param name="line7"></param>
        /// <param name="line8"></param>
        /// <param name="line9"></param>
        /// <param name="line10"></param>
        /// <param name="line11"></param>
        /// <param name="line12"></param>
        private void InitLineVisible(Line line7, Line line8, Line line9, Line line10, Line line11, Line line12)
        {
            var temp =
                m_Collection.FindAll(f => f is GDIRectText && f.Tag != null && ((int) f.Tag == 2 || (int) f.Tag == 4));
            temp.ForEach(e => e.Visible = DMITitle.MarshallMode);
            var temp1 =
                m_Collection.FindAll(
                    f => f is GDIRectText && f.Tag != null && ((int) f.Tag == 3));
            temp1.ForEach(e => e.Visible = !DMITitle.MarshallMode);
            DMITitle.MarshallModeChanged += m =>
            {
                line7.Visible = DMITitle.MarshallMode;
                line8.Visible = DMITitle.MarshallMode;
                line9.Visible = DMITitle.MarshallMode;
                line10.Visible = DMITitle.MarshallMode;
                line11.Visible = DMITitle.MarshallMode;
                line12.Visible = DMITitle.MarshallMode;
                var tmp =
                    m_Collection.FindAll(
                        f => f is GDIRectText && f.Tag != null && ((int) f.Tag == 2 || (int) f.Tag == 4));
                tmp.ForEach(e => e.Visible = DMITitle.MarshallMode);
                var tmp1 =
                    m_Collection.FindAll(
                        f => f is GDIRectText && f.Tag != null && ((int) f.Tag == 3));
                tmp1.ForEach(e => e.Visible = !DMITitle.MarshallMode);
            };
        }
    }
}
