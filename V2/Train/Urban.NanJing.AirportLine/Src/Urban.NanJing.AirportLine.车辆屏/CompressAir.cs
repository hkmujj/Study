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
    public class CompressAir : baseClass
    {
        private List<IGridRect> m_DraughtRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "压缩空气";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            int j = 0;
            m_DraughtRectList.Add(new DriveRect1(new Point(2, 150 + 22 * i++), "压缩空气", null));
            m_DraughtRectList.Add(new DriveRect2(new Point(2, 150 + 22 * i++), "压缩空气操作", null));
            m_DraughtRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "主风缸压力（bar）", new List<int>() { 88, 89 }, 1));
            m_DraughtRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "本地压缩空气状态", new List<int>() { 1533 + j++, 1533 + j++, 1533 + j++, 1533 + j++ }));
            m_DraughtRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "LMRG状态", new List<int>() { 1533 + j++, 1533 + j++, 1533 + j++, 1533 + j++ }));
            m_DraughtRectList.Add(new CompressAirRect(new Point(2, 150 + 22 * (i++)), "压缩空气维护"));
            i++;
            m_DraughtRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "空压机三相供电故障", new List<int>() { 1533 + j++, 1533 + j++, 1533 + j++, 1533 + j++ }));
            m_DraughtRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "空压机过流", new List<int>() { 1533 + j++, 1533 + j++, 1533 + j++, 1533 + j++ }));
            m_DraughtRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "空压机启动故障", new List<int>() { 1533 + j++, 1533 + j++, 1533 + j++, 1533 + j++ }));
            m_DraughtRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "空气干燥器故障", new List<int>() { 1533 + j++, 1533 + j++, 1533 + j++, 1533 + j++ }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_DraughtRectList)
            {
                v.OnDraw(g);
            }
        }


    }

    public class CompressAirRect : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;

        public List<RectText> m_RectText = new List<RectText>();

        public CompressAirRect(Point point, string label)
        {
            m_InitialPoint = point;
            m_Label = label;

            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 550, 44), m_Label, 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 550, m_InitialPoint.Y, 65, 44), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 615, m_InitialPoint.Y, 65, 44), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));

            m_RectText[0].LineAlignmentTextFormat = 2;
            m_RectText[0].FontTextStyle = FontStyle.Bold;
        }
        public void OnDraw(Graphics g)
        {

            foreach (var v in m_RectText)
            {
                v.OnDraw(g);
            }
        }
    }
}
