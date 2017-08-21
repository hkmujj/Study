using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.ATP系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIATPSystem : CRH3C380BBase
    {

        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr =
        {
            "",
            "EVC替换",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly string[] m_ContentStrs = {"", "2 EVC替换显示", "", "", "", "", "", "", "", "0 主页面"};

        public override string GetInfo()
        {
            return "DMIATP系统";
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
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0: //C键
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
                case 6:
                    break;
                case 7: //EVC替换

                    if (BoolList[IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.InbEVC可以切换标志]])
                    {
                        append_postCmd(CmdType.ChangePage, 142, 0, 0);
                    }
                    break;
                case 8:
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("ATP系统", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            for (int i = 0; i < 10; i++)
            {
                g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIATPSystem, ref m_RectsList))
            {

            }
        }
    }
}