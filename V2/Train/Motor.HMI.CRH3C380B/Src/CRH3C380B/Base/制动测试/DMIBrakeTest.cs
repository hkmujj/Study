using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动测试
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBrakeTest : CRH3C380BBase
    {
        //2

        private List<RectangleF> m_RectsList;

        private bool m_TheBrakeOn;

        private readonly string[] m_BtnStr =
        {
            "",
            "直接制动\n试验",
            "紧急制动\n试验",
            "MRP\n贯通性",
            "BP\n泄漏",
            "间接制动\n试验",
            "BP\n贯通性",
            "上次试验\n结果",
            "",
            "制动\n状态"
        };

        private readonly string[] m_ContentStrs =
        {
            "",
            "2 直接制动试验",
            "3 紧急制动试验",
            "4 总风管(MRP)贯通性试验",
            "5 列车管(BP)泄漏试验",
            "6 间接制动试验",
            "7 列车管(BP)贯通性试验",
            "8 上次制动试验结果",
            "",
            "0 制动状态"
        };

        private readonly string[] m_BrakeIndexs =
        {

            InBoolKeys.Inb停放制动施加2,
            InBoolKeys.Inb停放制动施加7,
            InBoolKeys.Inb停放制动施加10,
            InBoolKeys.Inb停放制动施加15,
        };

        private readonly string[] m_BtnStr1 = {"", "", "", "", "", "", "", "", "", "制动\n状态"};

        public override string GetInfo()
        {
            return "DMI制动试验";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_TheBrakeOn ? m_BtnStr[i] : m_BtnStr1[i];
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
            m_TheBrakeOn = m_BrakeIndexs.Any(GetInBoolValue);

            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            if (DMIButton.BtnUpList[0] == 15 || DMIButton.BtnUpList[0] == 0)
            {
                append_postCmd(CmdType.ChangePage, 21, 0, 0); //制动状态
            }
            else if (m_TheBrakeOn)
            {
                ResponseBtnEvent();
            }
        }

        private void ResponseBtnEvent()
        {
            switch (DMIButton.BtnUpList[0])
            {
                case 7: //直接制动试验
                    append_postCmd(CmdType.ChangePage, 252, 0, 0);
                    break;
                case 8: //紧急制动试验
                    append_postCmd(CmdType.ChangePage, 253, 0, 0);
                    break;
                case 9: //总风管贯通性试验
                    append_postCmd(CmdType.ChangePage, 254, 0, 0);
                    break;
                case 10: //列车管泄漏试验
                    append_postCmd(CmdType.ChangePage, 255, 0, 0);
                    break;
                case 11: //间接制动试验
                    append_postCmd(CmdType.ChangePage, 256, 0, 0);
                    break;
                case 12: //列车管贯通性试验
                    append_postCmd(CmdType.ChangePage, 257, 0, 0);
                    break;
                case 13: //上次制动试验结果
                    append_postCmd(CmdType.ChangePage, 238, 0, 0);
                    break;
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("制动试验", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
            if (!m_TheBrakeOn)
            {
                g.FillRectangle(SolidBrushsItems.RedBrush1, m_RectsList[22]);
                g.DrawString("施加停放制动！", FontsItems.FontC24B,
                    SolidBrushsItems.WhiteBrush, m_RectsList[22], FontsItems.TheAlignment(FontRelated.居中));
            }
            for (int i = 0; i < 10; i++)
            {
                //中间内容
                g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }
        }

        private void InitData()
        {

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakeTest, ref m_RectsList))
            {

            }
        }
    }
}