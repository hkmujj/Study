using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMISystem : CRH3C380BBase
    {
        //2
        public override string GetInfo()
        {
            return "DMI系统屏幕";
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
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //C键
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                    case 6:
                        break;
                    case 7: //列车配置
                        if (DMITitle.BtnContentList[1].BtnStr != string.Empty)
                        {
                            append_postCmd(CmdType.ChangePage, 122, 0, 0);
                        }
                        break;
                    case 8: //蓄电池
                        if (DMITitle.BtnContentList[2].BtnStr != string.Empty)
                        {
                            append_postCmd(CmdType.ChangePage, 123, 0, 0);
                        }
                        break;
                    case 9: //轮轴温度 
                        if (DMITitle.BtnContentList[3].BtnStr != string.Empty)
                        {
                            append_postCmd(CmdType.ChangePage, 1241, 0, 0);
                        }
                        break;
                    case 10: //解编
                        if (DMITitle.BtnContentList[4].BtnStr != string.Empty)
                        {
                            append_postCmd(CmdType.ChangePage, 125, 0, 0);
                        }
                        break;
                    case 11: //编组联挂
                        if (DMITitle.BtnContentList[5].BtnStr != string.Empty)
                        {
                            append_postCmd(CmdType.ChangePage, 126, 0, 0);
                        }
                        break;
                    case 12:
                        break;
                    case 13: //关闭车钩罩
                        if (DMITitle.BtnContentList[7].BtnStr != string.Empty)
                        {
                            if (GlobalParam.Instance.ProjectType == ProjectType.CRH3C)
                            {
                                append_postCmd(CmdType.ChangePage, 128, 0, 0);
                            }
                            else
                            {
                                if (DMITitle.CurrentMMIName == MMIType.左侧MMI屏)
                                {
                                    append_postCmd(CmdType.ChangePage, 128, 0, 0);
                                }
                            }
                        }
                        break;
                    case 14: //齿轮箱温度
                        if (DMITitle.BtnContentList[8].BtnStr != string.Empty)
                        {
                            append_postCmd(CmdType.ChangePage, 322, 0, 0);
                        }
                        break;
                    case 15: //主页面
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }

            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
        }

        private void PaintRectangles(Graphics e)
        {
            e.DrawString("系统", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            for (int i = 0; i < 10; i++)
            {
                e.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];

            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                for (int i = 4; i < 9; i++)
                {
                    m_BtnStr[i] = string.Empty;
                    m_ContentStrs[i] = string.Empty;
                }
            }

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMISystem, ref m_RectsList))
            {

            }
        }

        private List<RectangleF> m_RectsList;

        private float[] m_FValue;

        private readonly string[] m_BtnStr =
        {
            "",
            "列车\n配置",
            "蓄电池\n电压",
            "轮轴温度",
            "解编",
            "编组\n联挂",
            "",
            "关闭\n车钩罩",
            "齿轮箱\n温度",
            "主页面"
        };

        private readonly string[] m_ContentStrs =
        {
            "",
            "2 列车配置显示",
            "3 蓄电池电压110V",
            "4 轮轴温度",
            "5 解编",
            "6 编组联挂",
            "",
            "8 关闭车钩罩",
            "9 齿轮箱/牵引电机 温度",
            "0 主页面"
        };
    }
}