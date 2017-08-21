using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.维修
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIMaintenance : CRH3C380BBase
    {
        private bool m_LastModel;

        private static bool m_ServiceModel;

        public static bool ServiceModel
        {
            get { return m_ServiceModel; }
        }

        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr =
        {
            "维护\n状态开启", "维护\n状态关闭", "", "远程\n数据传输", "中间\n直流环节",
            "1号车\n版本", "运行速度\n设定检测", "牵引手柄\n检测", "能量\n计量", "主页面"
        };

        private readonly string[] m_ContentStrs =
        {
            "1 维护模式开启", "2 维护模式关闭", "", "4 远程数据传输", "5 中间直流环节",
            "6 软件和硬件版本", "7 运行速度设定检测", "8 牵引手柄检测", "9 能量计量", "0 主页面"
        };

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIMaintenance, ref m_RectsList))
            {

            }
        }

        //2
        public override string GetInfo()
        {
            return "维修视图";
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
            if (m_ServiceModel)
            {
                DMITitle.BtnContentList[0].BtnStr = string.Empty;
            }
            else
            {
                DMITitle.BtnContentList[1].BtnStr = string.Empty;
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
            m_ServiceModel = GetOutBoolValue(OutBoolKeys.Oub维护0为关闭1为开启);
            if (m_LastModel != m_ServiceModel)
            {
                DMITitle.BtnContentList[0].BtnStr = m_ServiceModel ? string.Empty : "维护\n状态开启";
                DMITitle.BtnContentList[1].BtnStr = m_ServiceModel ? "维护\n状态关闭" : string.Empty;
                m_LastModel = m_ServiceModel;
            }

            ResponseBtnEvent();
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //C键
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                    case 6:
                        if (!m_ServiceModel)
                        {
                            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub维护0为关闭1为开启), 1, 0); //维护
                        }
                        break;
                    case 7:
                        if (m_ServiceModel)
                        {
                            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub维护0为关闭1为开启), 0, 0);
                                //维护关闭
                        }
                        break;
                    case 8:
                        break;
                    case 9: //远程数据传输
                        append_postCmd(CmdType.ChangePage, 114, 0, 0);
                        break;
                    case 10: //中间直流环节
                        append_postCmd(CmdType.ChangePage, 115, 0, 0);
                        break;
                    case 11: //1号车版本
                        append_postCmd(CmdType.ChangePage, 1161, 0, 0);
                        break;
                    case 12: //运行速度设定检测
                        append_postCmd(CmdType.ChangePage, 117, 0, 0);
                        break;
                    case 13: //牵引手柄检测
                        append_postCmd(CmdType.ChangePage, 118, 0, 0);
                        break;
                    case 14: //能量计量
                        append_postCmd(CmdType.ChangePage, 119, 0, 0);
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("维护", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
            g.FillRectangle(DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_ServiceModel ? m_RectsList[12] : m_RectsList[13]);
            for (int i = 0; i < 10; i++)
            {
                if (i >= 2)
                {
                    //中间内容
                    g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));
                }
                else
                {
                    if (i == 0)
                    {
                        //中间内容
                        g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                            (DMITitle.NightMode && !m_ServiceModel || !DMITitle.NightMode && m_ServiceModel)
                                ? SolidBrushsItems.BlackBrush
                                : SolidBrushsItems.WhiteBrush,
                            m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));

                    }
                    else
                    {
                        //中间内容
                        g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                            (DMITitle.NightMode && m_ServiceModel || !DMITitle.NightMode && !m_ServiceModel)
                                ? SolidBrushsItems.BlackBrush
                                : SolidBrushsItems.WhiteBrush,
                            m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));
                    }
                }
            }
        }
    }
}
