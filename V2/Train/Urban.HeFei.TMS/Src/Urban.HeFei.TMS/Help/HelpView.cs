using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Urban.HeFei.TMS.Help
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HelpView : baseClass
    {
        public override string GetInfo()
        {
            return "帮助页面";
        }

        private List<Image> m_ImageResource;
        public static bool IsDisplay;
        private int m_PageIndex = 1;
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                m_PageIndex = 1;
            }


            base.setRunValue(nParaA, nParaB, nParaC);
        }

        public override bool mouseDown(Point point)
        {
            if (m_ButtonOne.Contains(point))
            {
                IsDisplay = false;
                m_PageIndex = 1;
            }
            else if (m_ButtonTwo.Contains(point))
            {

                m_PageIndex = m_PageIndex == 1 ? 2 : 1;
            }
            return true;
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_ImageResource = UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToList();
            return true;
        }
        private readonly Rectangle m_ImageRec = new Rectangle(150, 100, 500, 370);
        private Rectangle m_ButtonOne = new Rectangle(315, 415, 70, 30);
        private Rectangle m_ButtonTwo = new Rectangle(412, 415, 70, 30);
        public override void paint(Graphics g)
        {
            if (IsDisplay)
            {
                g.DrawImage(m_ImageResource[m_PageIndex - 1], m_ImageRec);
            }
        }
    }
}