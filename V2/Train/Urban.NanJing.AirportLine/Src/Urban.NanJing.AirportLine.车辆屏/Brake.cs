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
    public class Brake : baseClass
    {
        private List<IGridRect> m_BrakeRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "制动";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            m_BrakeRectList.Add(new DefaultRect1(new Point(2, 150), "制动", null));
            m_BrakeRectList.Add(new DefaultRect2(new Point(2, 172), "制动操作", null));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 194), "主", new List<int>() { 1425 + i++, 1425 + i++, -1, -1, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, -1, -1, 1425 + i++, 1425 + i++ }));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 216), "制动缓解", new List<int>() { 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++ }));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 238), "停放制动缓解", new List<int>() { 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++ }));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 260), "打滑侦测", null));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 282), "常用自动隔离（车级别）", new List<int>() { 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++ }));
            m_BrakeRectList.Add(new BrakeRect(new Point(2, 304), "常用制动隔离（转向架级别）", new List<int>() { 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++ }));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 326), "转向架1摩擦制动压力", new List<int>() { 70, 71, 72, 73, 74, 75 }, 1));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 348), "转向架2摩擦制动压力", new List<int>() { 76, 77, 78, 79, 80, 81 }, 1));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 370), "载重（Kg）", new List<int>() { 82, 83, 84, 85, 86, 87 }, 1));
            m_BrakeRectList.Add(new DefaultRect2(new Point(2, 392), "制动维护", null));

            m_BrakeRectList.Add(new DefaultRect(new Point(2, 414), "BCE 状态", new List<int>() { 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++ }));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 436), "BCE 自检状态", new List<int>() { 1425 + i++, 1425 + i++, -1, -1, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, -1, -1, 1425 + i++, 1425 + i++ }));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 458), "制动网络状态", new List<int>() { 1425 + i++, 1425 + i++, -1, -1, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, -1, -1, 1425 + i++, 1425 + i++ }));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 480), "制动远程缓解", null));
            m_BrakeRectList.Add(new DefaultRect(new Point(2, 502), "紧急制动可用", new List<int>() { 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++, 1425 + i++ }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_BrakeRectList)
            {
                v.OnDraw(g);
            }
        }
    }

    public class BrakeRect : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;
        private List<int> m_BoolIndexList;
        public List<RectText> m_RectText = new List<RectText>();

        public BrakeRect(Point point, string label, List<int> boolIndexList)
        {
            m_InitialPoint = point;
            m_Label = label;
            m_BoolIndexList = boolIndexList;

            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 380, 22), m_Label, 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 380, m_InitialPoint.Y, 25, 25), "", 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 405, m_InitialPoint.Y, 25, 25), "", 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 430, m_InitialPoint.Y, 25, 25), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 455, m_InitialPoint.Y, 25, 25), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 480, m_InitialPoint.Y, 25, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 505, m_InitialPoint.Y, 25, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 530, m_InitialPoint.Y, 25, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 555, m_InitialPoint.Y, 25, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 580, m_InitialPoint.Y, 25, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 605, m_InitialPoint.Y, 25, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 630, m_InitialPoint.Y, 25, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 655, m_InitialPoint.Y, 25, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));

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
}
