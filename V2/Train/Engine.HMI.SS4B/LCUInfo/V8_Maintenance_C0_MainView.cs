/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：维护-No.0-主界面
 *
 *-------------------------------------------------------------------------------------------------*/

using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using System.Drawing;

namespace SS4B_TMS.LCUInfo
{
    /// <summary>
    ///     功能描述：维护-No.0-主界面
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V8MaintenanceC0MainView : baseClass
    {
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private readonly Font m_ChineseFont = new Font("宋体", 12);

        //private Button[] _btns;//按钮列表
        private string[] m_BtnNames;

        private int m_I;

        private readonly Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Pen m_WhiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.0f);

        private Rectangle[] m_FrameRects;
        private string[] m_FrameStrs;
        private Rectangle[] m_FrameStrRects;

        private Point[] m_HPStarts;
        private Point[] m_HPEnds;

        private Point[] m_VPStarts;
        private Point[] m_VPEnds;

        private Rectangle[] m_FixedRects;
        private string[] m_FixedStrs;

        private Rectangle[] m_ChangeableRects;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-主界面";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_HPStarts = new Point[19];
            m_HPEnds = new Point[19];
            m_HPStarts[0] = new Point(12, 20);
            m_HPEnds[0] = new Point(752, 20);

            for (m_I = 1; m_I < 17; m_I++)
            {
                m_HPStarts[m_I] = new Point(12, 56 + 26 * (m_I - 1));
                m_HPEnds[m_I] = new Point(752, 56 + 26 * (m_I - 1));
            }

            for (m_I = 17; m_I < 19; m_I++)
            {
                m_HPStarts[m_I] = new Point(12, 486 + 40 * (m_I - 17));
                m_HPEnds[m_I] = new Point(752, 486 + 40 * (m_I - 17));
            }

            m_VPStarts = new Point[8];
            m_VPEnds = new Point[8];
            m_VPStarts[0] = new Point(12, 20);
            m_VPEnds[0] = new Point(12, 526);

            m_VPStarts[1] = new Point(92, 446);
            m_VPEnds[1] = new Point(92, 526);

            m_VPStarts[2] = new Point(262, 20);
            m_VPEnds[2] = new Point(262, 446);

            m_VPStarts[3] = new Point(322, 20);
            m_VPEnds[3] = new Point(322, 446);

            m_VPStarts[4] = new Point(382, 20);
            m_VPEnds[4] = new Point(382, 446);

            m_VPStarts[5] = new Point(632, 20);
            m_VPEnds[5] = new Point(632, 446);

            m_VPStarts[6] = new Point(692, 20);
            m_VPEnds[6] = new Point(692, 446);

            m_VPStarts[7] = new Point(752, 20);
            m_VPEnds[7] = new Point(752, 526);

            m_FixedStrs = new string[37]
            {
                "        名称",
                "功补隔离开关572QS", "4位电机故障隔离开关49QS",
                "3位电机故障隔离开关39QS", "2位电机故障隔离开关29QS", "1位电机故障隔离开关19QS",
                "牵引风速1隔离开关573QS", "一端入库转换开关20QP", "二端入库转换开关50QP",
                "劈相机2隔离开关583QS", "制动风速1隔离开关589QS", "自起劈相机隔离开关591QS",
                "牵引风速2隔离开关574QS", "主断隔离开关586QS", "欠压隔离开关236QS",
                "A车故障", "B车故障",
                "A车", "B车",
                "         名称",
                "制动风机2隔离开关582QS", "制动风机1隔离开关581QS", "牵引风机2隔离开关576QS",
                "牵引风机1隔离开关575QS", "劈相机隔离开关2位242QS", "劈相机隔离开关1位242QS",
                "备用", "备用", "备用", "备用", "备用",
                "变压器风机隔离开关599QS", "油泵隔离开关584QS", "制动机风速2隔离开关", "压缩机隔离开关579QS",
                "A车", "B车"
            };

            m_FixedRects = new Rectangle[37];
            m_FixedRects[0] = new Rectangle(12, 20, 120, 36);
            for (m_I = 1; m_I < 15; m_I++)
            {
                m_FixedRects[m_I] = new Rectangle(12, 56 + 26 * (m_I - 1), 250, 26);
            }
            m_FixedRects[15] = new Rectangle(12, 446, 80, 40);
            m_FixedRects[16] = new Rectangle(12, 486, 80, 40);

            m_FixedRects[17] = new Rectangle(262, 20, 60, 36);
            m_FixedRects[18] = new Rectangle(322, 20, 60, 36);
            m_FixedRects[19] = new Rectangle(382, 20, 120, 36);

            for (m_I = 0; m_I < 15; m_I++)
            {
                m_FixedRects[20 + m_I] = new Rectangle(382, 56 + 26 * m_I, 250, 26);
            }

            m_FixedRects[35] = new Rectangle(632, 20, 60, 36);
            m_FixedRects[36] = new Rectangle(692, 20, 60, 36);

            m_ChangeableRects = new Rectangle[58];

            for (m_I = 0; m_I < 14; m_I++)
            {
                m_ChangeableRects[2 * m_I] = new Rectangle(283, 60 + m_I * 26, 18, 18);
                m_ChangeableRects[2 * m_I + 1] = new Rectangle(343, 60 + m_I * 26, 18, 18);
            }
            for (m_I = 0; m_I < 15; m_I++)
            {
                m_ChangeableRects[28 + 2 * m_I] = new Rectangle(653, 60 + m_I * 26, 18, 18);
                m_ChangeableRects[2 * m_I + 29] = new Rectangle(713, 60 + m_I * 26, 18, 18);
            }

            return true;
        }

        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            for (m_I = 0; m_I < 19; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_HPStarts[m_I], m_HPEnds[m_I]);
            }
            for (m_I = 0; m_I < 8; m_I++)
            {
                dcGs.DrawLine(m_WhiteLinePen, m_VPStarts[m_I], m_VPEnds[m_I]);
            }

            for (m_I = 0; m_I < 37; m_I++)
            {
                dcGs.DrawString(m_FixedStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_FixedRects[m_I], FontInfo.SfLc);
            }

            OnPaintChange(dcGs);

            base.paint(dcGs);
        }

        //private string[]
        /// <summary>
        ///     onPaintChange
        /// </summary>
        /// <param name="dcGs"></param>
        private void OnPaintChange(Graphics dcGs)
        {
            for (m_I = 0; m_I < 14; m_I++)
            {
                if (BoolList[UIObj.InBoolList[0] + m_I])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, m_ChangeableRects[2 * m_I]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, m_ChangeableRects[2 * m_I]);
                }
                if (BoolList[UIObj.InBoolList[1] + m_I])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, m_ChangeableRects[2 * m_I + 1]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, m_ChangeableRects[2 * m_I + 1]);
                }
            }

            for (m_I = 0; m_I < 6; m_I++)
            {
                if (BoolList[UIObj.InBoolList[2] + m_I])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, m_ChangeableRects[28 + 2 * m_I]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, m_ChangeableRects[28 + 2 * m_I]);
                }
                if (BoolList[UIObj.InBoolList[3] + m_I])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, m_ChangeableRects[28 + 2 * m_I + 1]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, m_ChangeableRects[28 + 2 * m_I + 1]);
                }
            }

            for (m_I = 0; m_I < 5; m_I++)
            {
                dcGs.FillEllipse(Brushs.GreenBrush, m_ChangeableRects[40 + 2 * m_I]);
                dcGs.FillEllipse(Brushs.GreenBrush, m_ChangeableRects[40 + 2 * m_I + 1]);
            }

            for (m_I = 0; m_I < 4; m_I++)
            {
                if (BoolList[UIObj.InBoolList[2] + m_I + 6])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, m_ChangeableRects[50 + 2 * m_I]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, m_ChangeableRects[50 + 2 * m_I]);
                }
                if (BoolList[UIObj.InBoolList[3] + m_I + 6])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, m_ChangeableRects[50 + 2 * m_I + 1]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, m_ChangeableRects[50 + 2 * m_I + 1]);
                }
            }
        }
    }
}