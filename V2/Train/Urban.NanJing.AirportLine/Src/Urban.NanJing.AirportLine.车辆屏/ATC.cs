using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]

    public class ATC : baseClass
    {
        private List<IGridRect> m_ATCRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "ATC";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            m_ATCRectList.Add(new ATCRect1(new Point(40, 250 + 22 * i++), "ATC", null));
            m_ATCRectList.Add(new ATCRect2(new Point(40, 250 + 22 * i++), "ATC操作", null));
            m_ATCRectList.Add(new ATCRect(new Point(40, 250 + 22 * i++), "ATC", new List<int> { 1597, 1598 }));
            m_ATCRectList.Add(new ATCRect2(new Point(40, 250 + 22 * i++), "ATC维护", null));
            m_ATCRectList.Add(new ATCRect(new Point(40, 250 + 22 * i++), "ATO故障", new List<int> { 1600, 1601 }));
            m_ATCRectList.Add(new ATCRect(new Point(40, 250 + 22 * (i++)), "ATP故障", new List<int> { 1603, 1604 }));
            m_ATCRectList.Add(new ATCRect(new Point(40, 250 + 22 * i++), "A1车VOBC CB5状态", new List<int> { 1605, 1606 }));
            m_ATCRectList.Add(new ATCRect(new Point(40, 250 + 22 * i++), "A1车VOBC Cb6状态", new List<int> { 1607, 1608 }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_ATCRectList)
            {
                v.OnDraw(g);
            }
        }
    }

    public class ATCRect : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;

        public List<RectText> m_RectText = new List<RectText>();

        private List<int> m_BoolIndexList;

        public ATCRect(Point point, string label, List<int> boolIndexList)
        {
            m_InitialPoint = point;
            m_Label = label;
            m_BoolIndexList = boolIndexList;

            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 550, 22), m_Label, 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 550, m_InitialPoint.Y, 65, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
        }
        public void OnDraw(Graphics g)
        {
            if (m_BoolIndexList != null)
            {
                for (int i = 0; i < m_BoolIndexList.Count; i++)
                {
                    if (m_BoolIndexList[i] > 0)
                    {
                        if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                        {
                            m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_StateColorImageArray[i % 2];
                        }
                    }
                }
            }

            foreach (var v in m_RectText)
            {
                v.OnDraw(g);
            }
        }
    }

    public class ATCRect1 : ATCRect
    {
        public ATCRect1(Point point, string label, List<int> boolIndexList)
            : base(point, label, boolIndexList)
        {
            m_RectText[0].AlignmentTextFormat = 1;
            m_RectText[0].FontTextStyle = FontStyle.Bold;

            m_RectText[1].Text = "A1";
            m_RectText[1].FontTextStyle = FontStyle.Bold;
            m_RectText[1].IsImmutable = true;
        }
    }

    public class ATCRect2 : ATCRect
    {
        public ATCRect2(Point point, string label, List<int> boolIndexList)
            : base(point, label, boolIndexList)
        {
            m_RectText[0].AlignmentTextFormat = 1;
            m_RectText[0].FontTextStyle = FontStyle.Bold;
        }
    }
}
