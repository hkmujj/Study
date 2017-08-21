using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.底层共用
{
    //信息盒
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIInfoBox : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly List<string> m_Maintain = new List<string>();
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly List<string> m_CommonInfo = new List<string>();

        private float[] m_FValue;

        private int m_MenuID;
        private int m_PageID;

        private readonly string[] m_BtnStr =
        {
            "维护\n信息",
            "一般\n信息",
            "硬件\n  ",
            "左显示器\n层次",
            "右显示器\n层次",
            "列车员显\n示器层次",
            "可操作\n项",
            "不可操作\n项",
            "状态条\n  ",
            "返回\n  "
        };

        private readonly string[] m_TitleStr =
        {
            "信息; 维护信息",
            "信息; 一般信息",
            "信息; 硬件",
            "信息; 左显示器层次",
            "信息; 右显示器层次",
            "信息; 列车员显示器层次",
            "信息; 可操作项",
            "信息; 不可操作项",
            "信息; 状态条",
            "信息; 维护信息"
        };

        private string m_TitleName = "信息;维护信息";

        //2
        public override string GetInfo()
        {
            return "信息盒";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);
        }


        private void Draw(Graphics g)
        {
            PaintGroundImage(g);
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

            DMITitle.BtnContentList[0].BtnStr = string.Empty;
        }

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 1:
                        if (m_PageID > 0)
                        {
                            m_PageID--;
                        }
                        break;
                    case 2:
                        if (m_MenuID == 6 && m_PageID < 1)
                        {
                            m_PageID++;
                        }
                        else if (m_MenuID == 7 && m_PageID < 6)
                        {
                            m_PageID++;
                        }
                        break;
                    default:
                        if (DMIButton.BtnUpList[0] > 5 && DMIButton.BtnUpList[0] < 16)
                        {
                            m_MenuID = DMIButton.BtnUpList[0] - 6;
                            m_TitleName = m_TitleStr[DMIButton.BtnUpList[0] - 6];

                            for (int i = 0; i < 10; i++)
                            {
                                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
                            }

                            DMITitle.BtnContentList[DMIButton.BtnUpList[0] - 6].BtnStr = string.Empty;

                            m_PageID = 0;

                            if (DMIButton.BtnUpList[0] == 15)
                            {
                                append_postCmd(CmdType.ChangePage, 11, 0, 0);
                                m_MenuID = 0;
                                m_TitleName = m_TitleStr[0];
                                m_PageID = 0;
                            }
                        }

                        break;
                }
            }

            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }


        }

        private void PaintGroundImage(Graphics g)
        {
            if (DMITitle.NightMode)
            {

            }
            else
            {
                g.DrawString(m_TitleName, FontsItems.FontC11,
                    SolidBrushsItems.WhiteBrush, m_RectsList[56], FontsItems.TheAlignment(FontRelated.靠左));
                switch (m_MenuID)
                {
                    case 0: //维护信息
                        if (m_Maintain.Count == 0)
                        {
                            g.DrawImage(MSImages.小三角_0_0, m_RectsList[20]);
                            g.DrawImage(MSImages.小三角_0_1, m_RectsList[21]);
                            g.DrawString("无有用信息", FontsItems.FontC11,
                                SolidBrushsItems.WhiteBrush, m_RectsList[2], FontsItems.TheAlignment(FontRelated.靠左));
                            g.DrawString("第 1 行 共 1 行", FontsItems.FontC11,
                                SolidBrushsItems.WhiteBrush, m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠右));
                        }
                        break;
                    case 1: //一般信息
                        if (m_CommonInfo.Count == 0)
                        {
                            g.DrawImage(MSImages.小三角_0_0, m_RectsList[20]);
                            g.DrawImage(MSImages.小三角_0_1, m_RectsList[21]);
                            g.DrawString("无有用信息", FontsItems.FontC11,
                                SolidBrushsItems.WhiteBrush, m_RectsList[2], FontsItems.TheAlignment(FontRelated.靠左));
                            g.DrawString("第 1 行 共 1 行", FontsItems.FontC11,
                                SolidBrushsItems.WhiteBrush, m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠右));
                        }
                        break;
                    case 2: //硬件
                        g.DrawImage(MsgBoxImages.Btn_3, m_RectsList[0]);
                        break;
                    case 3: //左显示器层次
                        g.DrawImage(MsgBoxImages.Btn_4, m_RectsList[0]);
                        break;
                    case 4: //右显示器层次
                        g.DrawImage(MsgBoxImages.Btn_5, m_RectsList[0]);
                        break;
                    case 5: //列车员显示器层次
                        g.DrawImage(MsgBoxImages.Btn_6, m_RectsList[0]);
                        break;
                    case 6: //可操作项
                        switch (m_PageID)
                        {
                            case 0:
                                g.DrawImage(MsgBoxImages.Btn_7_1, m_RectsList[0]);
                                break;
                            case 1:
                                g.DrawImage(MsgBoxImages.Btn_7_2, m_RectsList[0]);
                                break;
                        }

                        break;
                    case 7: //不可操作项                        
                        switch (m_PageID)
                        {
                            case 0:
                                g.DrawImage(MsgBoxImages.Btn_8_1, m_RectsList[0]);
                                break;
                            case 1:
                                g.DrawImage(MsgBoxImages.Btn_8_2, m_RectsList[0]);
                                break;
                            case 2:
                                g.DrawImage(MsgBoxImages.Btn_8_3, m_RectsList[0]);
                                break;
                            case 3:
                                g.DrawImage(MsgBoxImages.Btn_8_4, m_RectsList[0]);
                                break;
                            case 4:
                                g.DrawImage(MsgBoxImages.Btn_8_5, m_RectsList[0]);
                                break;
                            case 5:
                                g.DrawImage(MsgBoxImages.Btn_8_6, m_RectsList[0]);
                                break;
                            case 6:
                                g.DrawImage(MsgBoxImages.Btn_8_7, m_RectsList[0]);
                                break;
                        }

                        break;
                    case 8: //状态条
                        g.DrawImage(MsgBoxImages.Btn_9, m_RectsList[0]);
                        break;
                }
            }
        }

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIInfoBox, ref m_RectsList))
            {

            }

        }
    }
}