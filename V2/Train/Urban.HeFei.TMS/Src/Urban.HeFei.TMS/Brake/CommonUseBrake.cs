using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Brake
{
    /// <summary>
    /// 常用制动
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class CommonUseBrake : baseClass
    {
        public const int DiffValue = 73;
        private readonly Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private int[] m_ServiceBrakingPointXs;
        private int[] m_ServiceBrakingPointYs;
        private Rectangle m_FrameRectangle;
        private Rectangle[] m_CyzdRects;
        private bool[] m_CyzdFlags;
        private readonly List<Image> m_ResourceImage = new List<Image>();//图片资源
        private readonly Font m_ChineseFont = new Font("宋体", 20, FontStyle.Regular);
        private readonly Brush m_BlackBrush = new SolidBrush(Color.Black);
        public override string GetInfo()
        {
            return "常用制动";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameRectangle = new Rectangle(12, 262 + DiffValue, 674, 96);
            m_ServiceBrakingPointXs = new int[6];
            for (int i = 0; i < 6; i++)
            {
                m_ServiceBrakingPointXs[i] = 175 + i * 86;
            }
            m_ServiceBrakingPointYs = new int[5];
            m_ServiceBrakingPointYs[0] = 268 + DiffValue;
            m_ServiceBrakingPointYs[1] = 312 + DiffValue;
            m_ServiceBrakingPointYs[2] = 366 + DiffValue;
            m_ServiceBrakingPointYs[3] = 416 + DiffValue;
            m_ServiceBrakingPointYs[4] = 465 + DiffValue;
            m_CyzdRects = new Rectangle[12];
            m_CyzdFlags = new bool[12];
            for (int i = 0; i < 6; i++)
            {
                m_CyzdRects[2 * i] = new Rectangle(m_ServiceBrakingPointXs[i], m_ServiceBrakingPointYs[0], 60, 40);
                m_CyzdRects[2 * i + 1] = new Rectangle(m_ServiceBrakingPointXs[i], m_ServiceBrakingPointYs[1], 60, 40);
                m_CyzdFlags[2 * i] = false;
                m_CyzdFlags[2 * i + 1] = false;
            }
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

            for (int j = 0; j < 6; j++)
            {
                m_CyzdFlags[2 * j] = false;
                m_CyzdFlags[2 * j + 1] = false;
                for (int i = 0; i < 4; i++)//常用制动缓解
                {
                    if (BoolList[UIObj.InBoolList[j] + i])
                    {
                        m_CyzdFlags[2 * j] = true;
                        g.DrawImage(m_ResourceImage[9 + i], m_CyzdRects[2 * j]);
                    }
                    if (BoolList[UIObj.InBoolList[j] + 4 + i])
                    {
                        m_CyzdFlags[2 * j + 1] = true;
                        g.DrawImage(m_ResourceImage[9 + i], m_CyzdRects[2 * j + 1]);
                    }
                }
            }
            for (int j = 0; j < 6; j++)
            {
                if (m_CyzdFlags[2 * j] == false)
                {
                    g.DrawImage(m_ResourceImage[13], m_CyzdRects[2 * j]);
                }
                if (m_CyzdFlags[2 * j + 1] == false)
                {
                    g.DrawImage(m_ResourceImage[13], m_CyzdRects[2 * j + 1]);
                }
            }
            g.DrawRectangle(m_BlackLinePen, m_FrameRectangle);
            g.DrawString("常用制动", m_ChineseFont, m_BlackBrush, m_FrameRectangle, FontInfo.SfLc);
            base.paint(g);
        }

    }
}