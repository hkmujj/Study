using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.�ײ㹲��;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.�ײ㹲��.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.�ƶ���Ч��
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBrakingPower : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        private Image m_BackImage;

        private List<string> m_BValueList;

        private int[] m_BValueIdArr;
        private List<int[]> m_BValueAllArrange;

        private bool[] m_BValue;

        private BrakingPower m_BrakePower;
        private List<CommonInnerControlBase> m_Collection;
        private bool m_Braking;

        private readonly string[] m_BtnStr = {"", "", "", "", "", "�������\n��", "", "�ϴ�����\n���", "", "�ƶ�\n״̬"};

        //2
        public override string GetInfo()
        {
            return "DMI�ƶ���";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2)
            {
                return;
            }

            if (DMITitle.BtnContentList.Count != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
                }
            }

            m_Braking = false;

            if (DMITitle.MarshallMode || GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                m_BValueIdArr = DMITitle.TrainInSide ? m_BValueAllArrange[2] : m_BValueAllArrange[0]; //��ʼ��16���ͻ��˻�ȡ���߼�λ
                // ReSharper disable once ConditionalTernaryEqualBranch
                m_BackImage = DMITitle.NightMode ? BrakeImages.�ƶ���Ч��16 : BrakeImages.�ƶ���Ч��16; //��ʼ������ͼ
            }
            else
            {
                m_BValueIdArr = DMITitle.TrainInSide ? m_BValueAllArrange[1] : m_BValueAllArrange[0];
                // ReSharper disable once ConditionalTernaryEqualBranch
                m_BackImage = DMITitle.NightMode ? BrakeImages.�ƶ���Ч��8 : BrakeImages.�ƶ���Ч��8;
            }

            m_BrakePower.ResetCalculate();

        }

        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);
        }

        private void GetValue()
        {
            ResponseBtnEvent();

            for (var i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[GetInBoolIndex(m_BValueList[m_BValueIdArr[i]])];
            }

            m_BrakePower.CalculateResult(m_BValue,
                (DMITitle.MarshallMode || GlobalParam.Instance.ProjectType == ProjectType.CRH380BL));
            if (m_BrakePower.EffectiveRateC == 100)
            {
                m_Braking = true;
            }
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0:
                        append_postCmd(CmdType.ChangePage, 21, 0, 0);
                        break;
                    case 11:
                        append_postCmd(CmdType.ChangePage, 236, 0, 0); //�������\n��
                        break;
                    case 13:
                        append_postCmd(CmdType.ChangePage, 238, 0, 0); //�ϴ�������
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 21, 0, 0); //
                        break;
                }
            }
        }


        private void Draw(Graphics g)
        {
            g.DrawString("�ƶ�����״̬", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.����));

            g.DrawImage(m_BackImage, m_RectsList[2]);

            if (!m_Braking)
            {
                g.FillRectangle(DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[3]);
                g.DrawString("���ƶ��ֱ����ڽ����ƶ�λ��", FontsItems.FontC12B,
                    DMITitle.NightMode ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush,
                    m_RectsList[3], FontsItems.TheAlignment(FontRelated.����));
            }

            g.DrawString(m_BrakePower.StratTime.ToString("yy.MM.dd"), FontsItems.FontC11,
                SolidBrushsItems.BlackBrush, m_RectsList[4], FontsItems.TheAlignment(FontRelated.����)); //����
            g.DrawString(m_BrakePower.StratTime.ToString("HH:mm:ss"), FontsItems.FontC11,
                SolidBrushsItems.BlackBrush, m_RectsList[5], FontsItems.TheAlignment(FontRelated.����)); //ʱ��
            g.DrawString(m_BrakePower.EffectiveRateA.ToString(), FontsItems.FontC11,
                SolidBrushsItems.BlackBrush, m_RectsList[6], FontsItems.TheAlignment(FontRelated.����)); //�ƶ���Ч��A
            g.DrawString(m_BrakePower.EffectiveRateB.ToString(), FontsItems.FontC11,
                SolidBrushsItems.BlackBrush, m_RectsList[7], FontsItems.TheAlignment(FontRelated.����)); //�ƶ���Ч��B
            g.DrawString(
                m_BrakePower.EffectiveRateC == 0
                    ? "X"
                    : m_BrakePower.EffectiveRateC.ToString(CultureInfo.InvariantCulture), FontsItems.FontC11,
                SolidBrushsItems.BlackBrush, m_RectsList[8], FontsItems.TheAlignment(FontRelated.����)); //�ƶ���Ч��C
            g.DrawString(((int) GetInFloatValue(InFloatKeys.Inf����)).ToString(), FontsItems.FontC11,
                SolidBrushsItems.BlackBrush, m_RectsList[9], FontsItems.TheAlignment(FontRelated.����)); //����
            g.DrawString(((int) GetInFloatValue(InFloatKeys.Inf����)).ToString(), FontsItems.FontC11,
                SolidBrushsItems.BlackBrush, m_RectsList[10], FontsItems.TheAlignment(FontRelated.����)); //����

            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                for (var i = 0; i < 16; i++)
                {
                    if (!m_BrakePower.BrakePowers0[i])
                    {
                        g.DrawImage(BrakeImages.��, m_RectsList[30 + i]);
                    }
                    if (m_BrakePower.BrakePowers1[i])
                    {
                        g.DrawImage(BrakeImages.��, m_RectsList[46 + i]);
                    }
                    g.DrawString(GetCarNumOfBl(i), FontsItems.FontC12B,
                        SolidBrushsItems.WhiteBrush, m_RectsList[14 + i], FontsItems.TheAlignment(FontRelated.����));
                }
            }
            else
            {
                for (var i = 0; i < (DMITitle.MarshallMode ? 16 : 8); i++)
                {
                    if (!m_BrakePower.BrakePowers0[i])
                    {
                        g.DrawImage(BrakeImages.��, m_RectsList[30 + i]);
                    }
                    if (m_BrakePower.BrakePowers1[i])
                    {
                        g.DrawImage(BrakeImages.��, m_RectsList[46 + i]);
                    }
                    g.DrawString(GetCarNum.GetNum(i, DMITitle.MarshallMode, DMITitle.TrainInSide), FontsItems.FontC12B,
                        SolidBrushsItems.WhiteBrush, m_RectsList[14 + i], FontsItems.TheAlignment(FontRelated.����));
                }
            }

            m_Collection.ForEach(f => f.OnPaint(g));
        }

        private string GetCarNumOfBl(int i)
        {
            return DMITitle.TrainInSide ? (16 - i).ToString("00") : (i + 1).ToString("00");
        }

        private void InitData()
        {
            m_BValueList = new List<string>
            {
                InBoolKeys.Inb�ƶ���Ч����Ч1D,
                InBoolKeys.Inb�ƶ���Ч����Ч2D,
                InBoolKeys.Inb�ƶ���Ч����Ч3D,
                InBoolKeys.Inb�ƶ���Ч����Ч4D,
                InBoolKeys.Inb�ƶ���Ч����Ч5D,
                InBoolKeys.Inb�ƶ���Ч����Ч6D,
                InBoolKeys.Inb�ƶ���Ч����Ч7D,
                InBoolKeys.Inb�ƶ���Ч����Ч8D,
                InBoolKeys.Inb�ƶ���Ч����Ч9D,
                InBoolKeys.Inb�ƶ���Ч����Ч10D,
                InBoolKeys.Inb�ƶ���Ч����Ч11D,
                InBoolKeys.Inb�ƶ���Ч����Ч12D,
                InBoolKeys.Inb�ƶ���Ч����Ч13D,
                InBoolKeys.Inb�ƶ���Ч����Ч14D,
                InBoolKeys.Inb�ƶ���Ч����Ч15D,
                InBoolKeys.Inb�ƶ���Ч����Ч16D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�1D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�2D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�3D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�4D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�5D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�6D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�7D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�8D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�9D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�10D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�11D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�12D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�13D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�14D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�15D,
                InBoolKeys.Inb�ƶ���Ч����Ч�ر�16D,

            };

            m_BValueAllArrange = new List<int[]>
            {
                //����8��16��
                new[]
                {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    8,
                    9,
                    10,
                    11,
                    12,
                    13,
                    14,
                    15,
                    16,
                    17,
                    18,
                    19,
                    20,
                    21,
                    22,
                    23,
                    24,
                    25,
                    26,
                    27,
                    28,
                    29,
                    30,
                    31
                },
                //8������
                new[]
                {
                    7,
                    6,
                    5,
                    4,
                    3,
                    2,
                    1,
                    0,
                    15,
                    14,
                    13,
                    12,
                    11,
                    10,
                    9,
                    8,
                    23,
                    22,
                    21,
                    20,
                    19,
                    18,
                    17,
                    16,
                    31,
                    30,
                    29,
                    28,
                    27,
                    26,
                    25,
                    24
                },
                //16������
                new[]
                {
                    15,
                    14,
                    13,
                    12,
                    11,
                    10,
                    9,
                    8,
                    7,
                    6,
                    5,
                    4,
                    3,
                    2,
                    1,
                    0,
                    31,
                    30,
                    29,
                    28,
                    27,
                    26,
                    25,
                    24,
                    23,
                    22,
                    21,
                    20,
                    19,
                    18,
                    17,
                    16
                }
            };
            m_BValueIdArr = m_BValueAllArrange[0];

            m_BValue = new bool[m_BValueList.Count];


            m_BackImage = BrakeImages.�ƶ���Ч��8;

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakingPower, ref m_RectsList))
            {

            }

            var color = Color.White;
            var linePen = new Pen(Color.White, 1);
            const int length = 305;
            m_Collection = new List<CommonInnerControlBase>();
            var line1 = new Line(new Point(25, 375), new Point(478, 375)) {LinePen = linePen, Tag = 1};
            var line2 = new Line(new Point(line1.StartPoint.X, line1.StartPoint.Y + 100),
                new Point(line1.EndPoint.X, line1.EndPoint.Y + 100)) {LinePen = linePen, Tag = 1};
            var line3 = new Line(new Point(line1.StartPoint.X + 152, line1.StartPoint.Y - 75),
                new Point(line1.StartPoint.X + 152, line1.StartPoint.Y + 100)) {LinePen = new Pen(color, 2f), Tag = 1};
            for (var i = 1; i < 8; i++)
            {
                m_Collection.Add(new Line(new Point(line3.StartPoint.X + i*38, line1.StartPoint.Y - 20),
                    new Point(line3.StartPoint.X + i*38, line1.StartPoint.Y + 100)) {LinePen = linePen, Tag = 1});
            }

            var line4 = GlobalParam.Instance.ProjectType == ProjectType.CRH380BL
                ? new Line(new Point(line3.StartPoint.X + 38*8, line1.StartPoint.Y - 20),
                    new Point(line3.StartPoint.X + 8*38, line1.StartPoint.Y + 100)) {LinePen = linePen}
                : new Line(new Point(line3.StartPoint.X + 38*8, line1.StartPoint.Y - 75),
                    new Point(line3.StartPoint.X + 8*38, line1.StartPoint.Y + 100))
                {
                    LinePen = new Pen(color, 2f),
                    Tag = 1
                };
            // line4.Visible = DMITitle.MarshallMode;
            for (var i = 1; i < 8; i++)
            {
                m_Collection.Add(new Line(new Point(line4.StartPoint.X + i*38, line1.StartPoint.Y - 20),
                    new Point(line4.StartPoint.X + i*38, line1.StartPoint.Y + 100))
                {
                    LinePen = linePen,
                    Visible = DMITitle.MarshallMode,
                    Tag = 2
                });
            }

            var line5 = GlobalParam.Instance.ProjectType == ProjectType.CRH380BL
                ? new Line(new Point(line4.StartPoint.X + 38*8, line1.StartPoint.Y - 20),
                    new Point(line4.StartPoint.X + 8*38, line1.StartPoint.Y + 100)) {LinePen = linePen}
                : new Line(new Point(line4.StartPoint.X + 38*8, line1.StartPoint.Y - 75),
                    new Point(line4.StartPoint.X + 8*38, line1.StartPoint.Y + 100))
                {
                    LinePen = new Pen(color, 2f),
                    Tag = 2
                };
            var line6 = new Line(line1.EndPoint, new Point(line1.EndPoint.X + length, line1.EndPoint.Y))
            {
                LinePen = linePen,
                Tag = 2
            };
            var line7 = new Line(line2.EndPoint, new Point(line2.EndPoint.X + length, line2.EndPoint.Y))
            {
                LinePen = linePen,
                Tag = 2
            };
            var line8 = new Line(new Point(line3.StartPoint.X, line1.StartPoint.Y + 50),
                new Point(line1.EndPoint.X, line1.EndPoint.Y + 50)) {LinePen = linePen, Tag = 1};
            var line9 = new Line(line8.EndPoint, new Point(line8.EndPoint.X + length, line8.EndPoint.Y))
            {
                LinePen = linePen,
                Tag = 2
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
            var size = new Size(150, 30);
            m_Collection.Add(new GDIRectText
            {
                Text = "�����ƶ�",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextColor = color,
                OutLineRectangle = new Rectangle(line1.StartPoint.X, line1.StartPoint.Y - 50, size.Width, size.Height)
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "������",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextColor = color,
                TextFormat = FontsItems.TheAlignment(FontRelated.������),
                OutLineRectangle = new Rectangle(line1.StartPoint.X, line1.StartPoint.Y + 5, size.Width, size.Height)
            });
            m_Collection.Add(new GDIRectText
            {
                Text = "�ر�",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextColor = color,
                TextFormat = FontsItems.TheAlignment(FontRelated.������),
                OutLineRectangle = new Rectangle(line2.StartPoint.X, line2.StartPoint.Y - 30, size.Width, size.Height)
            });

            m_Collection.Add(new GDIRectText
            {
                Text = "������ 1",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextColor = color,
                TextFormat = FontsItems.TheAlignment(FontRelated.������),
                OutLineRectangle =
                    new Rectangle(line3.StartPoint.X + 1, line3.StartPoint.Y + 5, size.Width, size.Height),
                Tag = 2,
            });

            m_Collection.Add(new GDIRectText
            {
                Text = "������ 2",
                NeedDarwOutline = false,
                BackColorVisible = false,
                TextColor = color,
                TextFormat = FontsItems.TheAlignment(FontRelated.������),
                OutLineRectangle =
                    new Rectangle(line4.StartPoint.X + 1, line4.StartPoint.Y + 5, size.Width, size.Height),
                Tag = 2,
            });

            SetTrainUnit2Visible();

            m_BrakePower = new BrakingPower(this);

        }

        private void SetTrainUnit2Visible()
        {
            var tmp = m_Collection.FindAll(f => f.Tag != null && (int) f.Tag == 2);
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                tmp.ForEach(e => e.Visible = true);
            }
            else
            {
                tmp.ForEach(e => e.Visible = DMITitle.MarshallMode);
                DMITitle.MarshallModeChanged += m => { tmp.ForEach(e => e.Visible = DMITitle.MarshallMode); };
            }
        }
    }
}