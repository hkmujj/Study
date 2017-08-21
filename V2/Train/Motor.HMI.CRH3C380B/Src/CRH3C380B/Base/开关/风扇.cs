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
    public class DMIFans : CRH3C380BBase
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
            "风扇\n最小",
            "",
            "风扇\n自动",
            "",
            "风扇\n最大",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly string[] m_ContentStrs = {"最小", "自动", "最大"};

        //2
        public override string GetInfo()
        {
            return "DMI风扇";
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

        private void Draw(Graphics g)
        {
            PaintRectangles(g);
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
                case 6: //最小
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub风扇_最小), 1, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub风扇_最大), 0, 0);
                    m_Mode = 1;
                    UpdateBrush(false);
                    break;
                case 8: //自动
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub风扇_最小), 0, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub风扇_最大), 0, 0);
                    m_Mode = 0;
                    UpdateBrush(false);
                    break;
                case 10: //最大
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub风扇_最小), 0, 0);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub风扇_最大), 1, 0);
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
                if (OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub风扇_最小)])
                {
                    m_Mode = 1;
                }
                else if (OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub风扇_最大)])
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

        private SolidBrush SetRectAndStrBrush(SolidBrush theBrush)
        {
            return theBrush == SolidBrushsItems.BlackBrush ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush;
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("开关;风扇", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            g.DrawString("风扇:", FontsItems.FontC24B,
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

            if (Coordinate.RectangleFLists(ViewIDName.DMIFans, ref m_RectsList))
            {

            }
        }
    }
}