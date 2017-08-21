using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动状态
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBrakeStatus : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr = {"", "", "制动\n有效率", "制动力\n状态", "制动\n试验", "停放\n制动", "", "总风管", "", ""};

        private readonly string[] m_ConStrArr = {"比例制动", "雪天制动", "正常操作"};

        private readonly string[] m_StateIndexs = {InBoolKeys.Inb1168, InBoolKeys.Inb1169, InBoolKeys.Inb1170,};

        public override string GetInfo()
        {
            return "DMI制动状态";
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
            ResponseBtnEvent();

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintRectangles(g);
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 8:
                        append_postCmd(CmdType.ChangePage, 230, 0, 0); //制动有效率
                        break;
                    case 9:
                        append_postCmd(CmdType.ChangePage, 240, 0, 0); //制动力状态
                        break;
                    case 10:
                        append_postCmd(CmdType.ChangePage, 250, 0, 0); //制动试验
                        break;
                    case 11:
                        append_postCmd(CmdType.ChangePage, 260, 0, 0); //停放制动
                        break;
                    case 13:
                        append_postCmd(CmdType.ChangePage, 280, 0, 0); //总风管
                        break;
                    case 20:
                        append_postCmd(CmdType.ChangePage, 2, 0, 0); //信息盒
                        break;
                }
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("制动状态", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            for (int i = 0; i < 3; i++)
            {
                if (GetInBoolValue(m_StateIndexs[i]))
                {
                    g.DrawString(m_ConStrArr[i], FontsItems.FontC12,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_RectsList[19], FontsItems.TheAlignment(FontRelated.靠左));
                }
            }
        }

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakeStatus, ref m_RectsList))
            {

            }
        }
    }
}