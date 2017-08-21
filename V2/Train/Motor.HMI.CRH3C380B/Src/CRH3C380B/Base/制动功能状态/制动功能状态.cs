using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动功能状态
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMIStatusOfBrakeFunctions : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        /// <summary>
        /// 10个按钮
        /// </summary>
        private readonly string[] m_BtnStr =
        {
            "空压机\n切断", "空压机\n接通", "",
            "BP充风\n切断", "BP充风\n接通", "",
            "沙干燥\n切断", "沙干燥\n工作", "",
            "制动\n状态"
        };

        /// <summary>
        /// 19行内容
        /// </summary>
        private readonly string[] m_ContentStrs =
        {
            "", "", "", "空压机切断", "空压机接通",
            "", "", "", "", "列车管(BP)充风切断", "列车管(BP)充风接通",
            "", "", "", "", "沙干燥切断", "沙干燥工作",
            "", ""
        };

        /// <summary>
        /// 夜间模式
        /// </summary>
        private bool m_NightMode;

        private readonly SolidBrush m_BlackBrush = SolidBrushsItems.BlackBrush;
        private readonly SolidBrush m_WhiteBrush = SolidBrushsItems.WhiteBrush;

        /// <summary>
        /// 19行文字的颜色
        /// </summary>
        private SolidBrush[] m_StrBrushArr;

        /// <summary>
        /// 19行文字哪些需要填充
        /// </summary>
        private bool[] m_FillRectBrushArr;

        /// <summary>
        /// 按钮状态改变
        /// </summary>
        private bool m_BtnStateChanged;

        //2
        public override string GetInfo()
        {
            return "DMI制动功能状态";
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
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
            BtnStateUpdata();
            m_NightMode = DMITitle.NightMode;
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);
        }


        private void Draw(Graphics g)
        {
            PaintRectangles(g);
        }

        private void BtnStateUpdata()
        {
            DMITitle.BtnContentList[0].BtnStr = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5144)]
                ? String.Empty
                : "空压机\n切断";
            DMITitle.BtnContentList[1].BtnStr = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5144)]
                ? "空压机\n接通"
                : String.Empty;

            DMITitle.BtnContentList[3].BtnStr = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5145)]
                ? String.Empty
                : "BP充风\n切断";
            DMITitle.BtnContentList[4].BtnStr = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5145)]
                ? "BP充风\n接通"
                : String.Empty;

            DMITitle.BtnContentList[6].BtnStr = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5146)]
                ? String.Empty
                : "沙干燥\n切断";
            DMITitle.BtnContentList[7].BtnStr = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5146)]
                ? "沙干燥\n工作"
                : String.Empty;

            m_FillRectBrushArr[3] = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5144)];
            m_FillRectBrushArr[4] = !OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5144)];

            m_FillRectBrushArr[9] = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5145)];
            m_FillRectBrushArr[10] = !OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5145)];

            m_FillRectBrushArr[15] = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5146)];
            m_FillRectBrushArr[16] = !OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub5146)];

            for (int i = 0; i < m_StrBrushArr.Length; i++)
            {
                if (m_FillRectBrushArr[i])
                {
                    m_StrBrushArr[i] = m_NightMode ? m_WhiteBrush : m_BlackBrush;
                }
                else
                {
                    m_StrBrushArr[i] = m_NightMode ? m_BlackBrush : m_WhiteBrush;
                }

            }
        }

        private void GetValue()
        {
            if (m_BtnStateChanged)
            {
                BtnStateUpdata();
                m_BtnStateChanged = false;
            }

            ResponseBtnEvent();
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }
            switch (DMIButton.BtnUpList[0])
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 21, 0, 0); //制动状态
                    break;
                case 6: //空压机切断
                    if (!string.IsNullOrEmpty(DMITitle.BtnContentList[0].BtnStr))
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub5144), 1, 0);
                        m_BtnStateChanged = true;
                    }
                    break;
                case 7: //空压机接通
                    if (!string.IsNullOrEmpty(DMITitle.BtnContentList[1].BtnStr))
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub5144), 0, 0);
                        m_BtnStateChanged = true;
                    }
                    break;
                case 9: //列车管充风切断
                    if (!string.IsNullOrEmpty(DMITitle.BtnContentList[3].BtnStr))
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub5145), 1, 0);
                        m_BtnStateChanged = true;
                    }
                    break;
                case 10: //列车管充风接通
                    if (!string.IsNullOrEmpty(DMITitle.BtnContentList[4].BtnStr))
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub5145), 0, 0);
                        m_BtnStateChanged = true;
                    }
                    break;
                case 12: //沙干燥切断
                    if (!string.IsNullOrEmpty(DMITitle.BtnContentList[6].BtnStr))
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub5146), 1, 0);
                        m_BtnStateChanged = true;
                    }
                    break;
                case 13: //沙干燥工作
                    if (!string.IsNullOrEmpty(DMITitle.BtnContentList[7].BtnStr))
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub5146), 0, 0);
                        m_BtnStateChanged = true;
                    }
                    break;
                case 15: //制动状态
                    append_postCmd(CmdType.ChangePage, 21, 0, 0);
                    break;
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("制动功能状态", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
            for (int i = 0; i < 19; i++)
            {
                if (string.IsNullOrEmpty(m_ContentStrs[i]))
                {
                    continue;
                }
                if (m_FillRectBrushArr[i])
                {
                    g.FillRectangle(m_NightMode ? m_BlackBrush : m_WhiteBrush, m_RectsList[2 + i]);
                }
                g.DrawString(m_ContentStrs[i], FontsItems.FontC11, m_StrBrushArr[i],
                    m_RectsList[2 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }
        }

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIStatusOfBrakeFunctions, ref m_RectsList))
            {

            }
            m_StrBrushArr = new[]
            {
                m_BlackBrush, m_BlackBrush, m_BlackBrush, m_WhiteBrush, m_BlackBrush,
                m_BlackBrush, m_BlackBrush, m_BlackBrush, m_BlackBrush, m_WhiteBrush, m_BlackBrush,
                m_BlackBrush, m_BlackBrush, m_BlackBrush, m_BlackBrush, m_WhiteBrush, m_BlackBrush,
                m_BlackBrush, m_BlackBrush
            };
            m_FillRectBrushArr = new[]
            {
                false, false, false, false, true,
                false, false, false, false, false, true,
                false, false, false, false, false, true,
                false, false
            };
        }
    }
}
