using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common.Control;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Brake
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MasterFonVoltage : baseClass
    {

        private Font m_Font;//字体
        private Button m_BtnCheck;//自检按钮
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 14);
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private Brush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font m_DigitFont = new Font("Arial", 14, FontStyle.Regular);
        private Rectangle m_FrameRect = new Rectangle(12, 465, 674, 44);
        private string m_FrameStr = "主风管压力";
        private Rectangle[] m_ChildrenRects;
        private Rectangle[] m_ChildrenStrRects;
        private int m_I = 0;
        private string[] m_CurrentStrs;
        public override string GetInfo()
        {
            return "主风管压力";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Font = new Font("宋体", 10.5f, FontStyle.Bold);

            m_ChildrenRects = new Rectangle[2];
            m_ChildrenStrRects = new Rectangle[2];
            m_CurrentStrs = new string[2] { "", "" };

            m_ChildrenRects[0] = new Rectangle(262, 469, 60, 37);
            m_ChildrenRects[1] = new Rectangle(520, 469, 60, 37);


            m_ChildrenStrRects[0] = new Rectangle(263, 470, 59, 36);
            m_ChildrenStrRects[1] = new Rectangle(521, 470, 59, 36);


            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawRectangle(m_BlackLinePen, m_FrameRect);
            g.DrawString(m_FrameStr, m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);

            for (m_I = 0; m_I < 2; m_I++)
            {
                m_CurrentStrs[m_I] = FloatList[UIObj.InFloatList[m_I]].ToString("0");
                g.DrawRectangle(m_BlackLinePen, m_ChildrenRects[m_I]);
                g.FillRectangle(m_RectBrush, m_ChildrenStrRects[m_I]);
                g.DrawString(m_CurrentStrs[m_I], m_DigitFont, m_BlackBrush, m_ChildrenRects[m_I], FontInfo.SfCc);
            }
            base.paint(g);
        }
    }
}