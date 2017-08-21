using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.开关
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMIFrontWindowHeating : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        private int m_Mode;

        private readonly SolidBrush[] m_RectBrushArr =
        {
            SolidBrushsItems.BlackBrush,
            SolidBrushsItems.BlackBrush,
            SolidBrushsItems.BlackBrush
        };

        private readonly SolidBrush[] m_StrBrushArr =
        {
            SolidBrushsItems.WhiteBrush,
            SolidBrushsItems.WhiteBrush,
            SolidBrushsItems.WhiteBrush
        };

        private readonly string[] m_BtnStr =
        {
            "关闭",
            "",
            "常用\n模式",
            "",
            "解冻\n模式",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly string[] m_ContentStrs = {"关闭", "常用模式", "解冻模式"};

        //2
        public override string GetInfo()
        {
            return "DMI前窗加热";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                UpdateBrush(true);
            }

        }

        public override void Paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        private void Draw(Graphics e)
        {
            PaintRectangles(e);
        }

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 190, 0, 0);
                    break;
                case 6: //关闭
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道1), 1, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道2), 0, 0);

                    m_Mode = 1;
                    UpdateBrush(false);
                    break;
                case 8: //常用模式
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道1), 0, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道2), 0, 0);
                    m_Mode = 0;
                    UpdateBrush(false);
                    break;
                case 10: //解冻模式
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道1), 0, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道2), 1, 0);
                    m_Mode = 2;
                    UpdateBrush(false);
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void UpdateStrBrush()
        {
            switch (m_Mode)
            {
                case 1:
                    m_RectBrushArr[0] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[0] = SolidBrushsItems.BlackBrush;
                    DMITitle.BtnContentList[0].BtnStr = string.Empty;
                    break;
                case 0:
                    m_RectBrushArr[1] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[1] = SolidBrushsItems.BlackBrush;
                    DMITitle.BtnContentList[2].BtnStr = string.Empty;
                    break;
                case 2:
                    m_RectBrushArr[2] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[2] = SolidBrushsItems.BlackBrush;
                    DMITitle.BtnContentList[4].BtnStr = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// 更改Brush
        /// </summary>
        /// <param name="isInit"></param>
        private void UpdateBrush(bool isInit)
        {
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
            for (int i = 0; i < 3; i++)
            {
                m_RectBrushArr[i] = SolidBrushsItems.BlackBrush;
                m_StrBrushArr[i] = SolidBrushsItems.WhiteBrush;
            }

            if (isInit)
            {
                if (OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道1)])
                {
                    m_Mode = 1;
                }
                else if (OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道2)])
                {
                    m_Mode = 2;
                }
                else
                {
                    m_Mode = 0;
                }
            }

            UpdateStrBrush();
        }

        /// <summary>
        /// 设置画图区域和字符串的Brush
        /// </summary>
        /// <param name="theBrush">Brush</param>
        /// <returns>返回的Brush</returns>
        private static SolidBrush SetRectAndStrBrush(SolidBrush theBrush)
        {
            return theBrush == SolidBrushsItems.BlackBrush ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush;
        }

        /// <summary>
        /// 画字符串以及图形区域
        /// </summary>
        /// <param name="g">图形</param>
        private void PaintRectangles(Graphics g)
        {
            g.DrawString("开关;前窗加热", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            g.DrawString("前窗加热:", FontsItems.FontC24B,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[13], FontsItems.TheAlignment(FontRelated.靠左));

            for (int i = 0; i < m_ContentStrs.Length; i++)
            {
                g.FillRectangle(DMITitle.NightMode ? SetRectAndStrBrush(m_RectBrushArr[i]) : m_RectBrushArr[i],
                    m_RectsList[15 + i]);
                g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                    DMITitle.NightMode ? SetRectAndStrBrush(m_StrBrushArr[i]) : m_StrBrushArr[i],
                    m_RectsList[15 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }
        }

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }
            Coordinate.RectangleFLists(ViewIDName.DMIFrontWindowHeating, ref m_RectsList);
        }
    }
}