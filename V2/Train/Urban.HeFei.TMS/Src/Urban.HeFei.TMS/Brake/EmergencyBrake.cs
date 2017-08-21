using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Brake
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class EmergencyBrake : baseClass
    {
        private Font m_ChineseFont = new Font("宋体", 14);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle m_FrameNameRect;
        private Rectangle m_FrameRects;
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Rectangle[] m_EmergeRects;
        public override string GetInfo()
        {
            return "紧急制动继电器";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameNameRect = new Rectangle(12, 466, 674, 45);
            m_FrameRects = new Rectangle(12, 466, 674, 45);
            m_EmergeRects = new Rectangle[4];
            m_EmergeRects[0] = new Rectangle(175, 469, 28, 38);
            m_EmergeRects[1] = new Rectangle(207, 469, 28, 38);
            m_EmergeRects[2] = new Rectangle(605, 469, 28, 38);
            m_EmergeRects[3] = new Rectangle(637, 469, 28, 38);
            //导入图片资源
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });
            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawRectangle(m_BlackLinePen, m_FrameRects);
            g.DrawString("紧急制动继电器", m_ChineseFont, m_BlackBrush, m_FrameNameRect, FontInfo.SfLc);
            for (int i = 0; i < 2; i++)//紧急制动继电器
            {
                if (BoolList[UIObj.InBoolList[i]])
                {
                    g.DrawImage(m_ResourceImage[0], m_EmergeRects[2 * i]);
                    g.DrawImage(m_ResourceImage[0], m_EmergeRects[2 * i + 1]);
                }
                else if (BoolList[UIObj.InBoolList[i] + 1])
                {
                    g.DrawImage(m_ResourceImage[1], m_EmergeRects[2 * i]);
                    g.DrawImage(m_ResourceImage[1], m_EmergeRects[2 * i + 1]);
                }
                else
                {
                    g.DrawImage(m_ResourceImage[2], m_EmergeRects[2 * i]);
                    g.DrawImage(m_ResourceImage[2], m_EmergeRects[2 * i + 1]);
                }
            }
            base.paint(g);
        }
    }
}