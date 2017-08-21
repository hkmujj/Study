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
    public class Drive : baseClass
    {
        private List<IGridRect> m_DriveRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "驾驶";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            int j = 0;
            m_DriveRectList.Add(new DriveRect1(new Point(2, 150 + 22 * i++), "驾驶", null));
            m_DriveRectList.Add(new DriveRect2(new Point(2, 150 + 22 * i++), "驾驶 维护", null));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "主控器断路器跳开", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "列车供电接触器开路", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect3(new Point(2, 150 + 22 * i++), "司机室选择故障", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect3(new Point(2, 150 + 22 * i++), "驾驶模式选择故障", new List<int>() { 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "轮缘润滑选择故障", new List<int>() { 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "洗车模式继电器冲突", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "限速模式冲突", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "自由模式冲突", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "ATO模式继电器冲突", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "司机室占用继电器冲突", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            m_DriveRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "唤醒延时继电器故障", new List<int>() { 1313 + j++, 1313 + j++, 1313 + j++, 1313 + j++ }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_DriveRectList)
            {
                v.OnDraw(g);
            }
        }

    }

    public class DriveRect : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;
        private List<int> m_BoolIndexList;
        private int m_Index;
        public List<RectText> m_RectText = new List<RectText>();

        public DriveRect(Point point, string label, List<int> boolIndexList, int index = 0)
        {
            m_InitialPoint = point;
            m_Label = label;
            m_BoolIndexList = boolIndexList;
            m_Index = index;

            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 550, 22), m_Label, 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 550, m_InitialPoint.Y, 65, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 615, m_InitialPoint.Y, 65, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
        }
        public void OnDraw(Graphics g)
        {

            if (m_BoolIndexList != null)
            {
                for (int i = 0; i < m_BoolIndexList.Count; i++)
                {
                    if (m_BoolIndexList[i] > 0)
                    {
                        if (m_Index == 0)
                        {
                            if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                            {
                                m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_StateColorImageArray[i % 2];
                            }
                        }
                        else if (m_Index == 1)
                        {
                            m_RectText[i + 1].Text = BasicClass.m_FloatSortedList[m_BoolIndexList[i]].ToString();
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

    public class DriveRect1 : DriveRect
    {
        public DriveRect1(Point point, string label, List<int> boolIndexList, int index = 0)
            : base(point, label, boolIndexList, index)
        {
            m_RectText[0].AlignmentTextFormat = 1;
            m_RectText[0].FontTextStyle = FontStyle.Bold;

            m_RectText[1].Text = "A1";
            m_RectText[1].FontTextStyle = FontStyle.Bold;
            m_RectText[1].IsImmutable = true;

            m_RectText[2].Text = "A2";
            m_RectText[2].FontTextStyle = FontStyle.Bold;
            m_RectText[2].IsImmutable = true;
        }
    }

    public class DriveRect2 : DriveRect
    {
        public DriveRect2(Point point, string label, List<int> boolIndexList, int index = 0)
            : base(point, label, boolIndexList, index)
        {
            m_RectText[0].AlignmentTextFormat = 1;
            m_RectText[0].FontTextStyle = FontStyle.Bold;
        }
    }

    public class DriveRect3 : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;
        private List<int> m_BoolIndexList;
        private int m_Index;
        public List<RectText> m_RectText = new List<RectText>();

        public DriveRect3(Point point, string label, List<int> boolIndexList,int index=0)
        {
            m_Index = index;
            m_InitialPoint = point;
            m_Label = label;
            m_BoolIndexList = boolIndexList;
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 550, 22), m_Label, 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 550, m_InitialPoint.Y, 130, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));

        }
        public void OnDraw(Graphics g)
        {
            if (m_BoolIndexList != null)
            {
                for (int i = 0; i < m_BoolIndexList.Count; i++)
                {
                    if (m_BoolIndexList[i] > 0)
                    {
                        if (m_Index == 0)
                        {
                            if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                            {
                                m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_StateColorImageArray[i % 2];
                            }
                        }
                        else if (m_Index == 1)
                        {
                            m_RectText[i + 1].Text = BasicClass.m_FloatSortedList[m_BoolIndexList[i]].ToString();
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
