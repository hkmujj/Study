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
    internal class DMILineCurrentLimit : CRH3C380BBase
    {
        private bool m_IsBl;
        private int Step;
        private List<RectangleF> m_RectsList;

        private int m_Mode;

        private readonly SolidBrush[] m_RectBrushArr =
        {
            SolidBrushsItems.BlackBrush, SolidBrushsItems.BlackBrush,
            SolidBrushsItems.BlackBrush, SolidBrushsItems.BlackBrush, SolidBrushsItems.BlackBrush
        };

        private readonly SolidBrush[] m_StrBrushArr =
        {
            SolidBrushsItems.WhiteBrush, SolidBrushsItems.WhiteBrush,
            SolidBrushsItems.WhiteBrush, SolidBrushsItems.WhiteBrush, SolidBrushsItems.WhiteBrush
        };

        private string[] m_BtnStr; //= { "300A", "400A", "500A", "600A", "最大",
        //     "", "", "", "", "主页面" };
        private string[] m_ContentStrs; //= { "300A", "400A", "500A", "600A", "最大" };

        //2
        public override string GetInfo()
        {
            return "DMI网侧电流限制";
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

            PaintRectangles(g);
        }

        private void SendLineCurrentLimitData(int index)
        {
            switch (index)
            {
                case 0: //最大
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_600A), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_最大), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_300A), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_400A), 0, 1);
                    break;
                case 1: //300A
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_600A), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_最大), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_300A), 1, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_400A), 0, 1);
                    break;
                case 2: //400A
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_600A), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_最大), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_300A), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_400A), 1, 1);
                    break;
                case 3: //500A
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_600A), 1, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_最大), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_300A), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_400A), 0, 1);
                    break;
                case 4: //600A
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_600A), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_最大), 1, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_300A), 0, 1);
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub网侧电流_400A), 0, 1);
                    break;
            }
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
                case 6: //300A或者500A
                    if (GetInBoolValue(InBoolKeys.Inb922))
                    {
                        if (m_IsBl)
                        {
                            SendLineCurrentLimitData(3);
                            m_Mode = 0;
                        }
                        else
                        {
                            SendLineCurrentLimitData(1);
                            m_Mode = 0;
                        }
                        UpdateBrush(false);
                    }
                    break;
                case 7: //400A
                    if (GetInBoolValue(InBoolKeys.Inb922))
                    {
                        SendLineCurrentLimitData(2);
                        m_Mode = 1;
                        UpdateBrush(false);
                    }
                    break;

                case 8: //500A
                    if (GetInBoolValue(InBoolKeys.Inb922))
                    {
                        if (m_IsBl)
                        {
                            SendLineCurrentLimitData(4);
                            m_Mode = 1;
                        }
                        else
                        {
                            SendLineCurrentLimitData(3);
                            m_Mode = 2;
                        }
                        UpdateBrush(false);
                    }
                    break;
                case 9: //600A
                    if (GetInBoolValue(InBoolKeys.Inb922))
                    {
                        SendLineCurrentLimitData(4);
                        m_Mode = 3;
                        UpdateBrush(false);
                    }
                    break;
                case 10: //最大
                    if (GetInBoolValue(InBoolKeys.Inb922))
                    {
                        SendLineCurrentLimitData(0);
                        m_Mode = m_IsBl ? 2 : 4;
                        UpdateBrush(false);
                    }
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
                case 0:
                    m_RectBrushArr[0] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[0] = SolidBrushsItems.BlackBrush;
                    DMITitle.BtnContentList[0].BtnStr = string.Empty;
                    break;
                case 1:
                    m_RectBrushArr[1] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[1] = SolidBrushsItems.BlackBrush;
                    if (m_IsBl)
                    {
                        DMITitle.BtnContentList[2].BtnStr = string.Empty;
                    }
                    else
                    {
                        DMITitle.BtnContentList[1].BtnStr = string.Empty;
                    }
                    break;
                case 2:
                    m_RectBrushArr[2] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[2] = SolidBrushsItems.BlackBrush;
                    if (m_IsBl)
                    {
                        DMITitle.BtnContentList[4].BtnStr = string.Empty;
                    }
                    else
                    {
                        DMITitle.BtnContentList[2].BtnStr = string.Empty;
                    }

                    break;
                case 3:
                    m_RectBrushArr[3] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[3] = SolidBrushsItems.BlackBrush;
                    DMITitle.BtnContentList[3].BtnStr = string.Empty;
                    break;
                case 4:
                    m_RectBrushArr[4] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[4] = SolidBrushsItems.BlackBrush;
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
            for (int i = 0; i < 5; i++)
            {
                m_RectBrushArr[i] = SolidBrushsItems.BlackBrush;
                m_StrBrushArr[i] = SolidBrushsItems.WhiteBrush;
            }

            if (isInit)
            {
                if (OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_600A)])
                {
                    m_Mode = m_IsBl ? 0 : 2;
                }
                else if (OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub网侧电流限制_最大)])
                {
                    m_Mode = m_IsBl ? 1 : 3;
                }
                else if (OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub网侧电流_300A)])
                {
                    m_Mode = 0;
                }
                else if (OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub网侧电流_400A)])
                {
                    m_Mode = 1;
                }
                else
                {
                    m_Mode = m_IsBl ? 2 : 4;
                }
            }

            UpdateStrBrush();
        }

        private static SolidBrush SetRectAndStrBrush(SolidBrush theBrush)
        {
            return theBrush == SolidBrushsItems.BlackBrush ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush;
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("开关;网侧电流限制", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            for (int i = 0; i < m_ContentStrs.Length; i++)
            {
                g.FillRectangle(DMITitle.NightMode ? SetRectAndStrBrush(m_RectBrushArr[i]) : m_RectBrushArr[i],
                    m_RectsList[13 + i*Step]);
                g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                    DMITitle.NightMode ? SetRectAndStrBrush(m_StrBrushArr[i]) : m_StrBrushArr[i],
                    m_RectsList[13 + i*Step], FontsItems.TheAlignment(FontRelated.靠左));
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMILineCurrentLimit, ref m_RectsList))
            {

            }
            m_IsBl = GlobalParam.Instance.ProjectType == ProjectType.CRH380BL;
            if (m_IsBl)
            {
                m_BtnStr = new[]
                {
                    "500A", "", "600A", "", "最大",
                    "", "", "", "", "主页面"
                };
                m_ContentStrs = new[] {"500A", "600A", "最大"};
            }
            else
            {
                m_BtnStr = new[]
                {
                    "300A", "400A", "500A", "600A", "最大",
                    "", "", "", "", "主页面"
                };
                m_ContentStrs = new[] {"300A", "400A", "500A", "600A", "最大"};
            }
            Step = m_IsBl ? 3 : 2;
        }
    }
}
