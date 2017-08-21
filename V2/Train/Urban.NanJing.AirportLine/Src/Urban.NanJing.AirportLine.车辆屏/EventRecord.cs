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
    public class EventRecord : baseClass
    {
        private List<IGridRect> m_EventRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "事件记录仪";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            int j = 0;
            m_EventRectList.Add(new DriveRect1(new Point(2, 150 + 22 * i++), "事件记录仪", null));
            m_EventRectList.Add(new DriveRect2(new Point(2, 150 + 22 * i++), "事件记录仪维护", null));
            m_EventRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "MVB通信故障", new List<int>() { 2001 + j++, 2001 + j++, 2001 + j++, 2001 + j++ }));
            m_EventRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "轻微故障", new List<int>() { 2001 + j++, 2001 + j++, 2001 + j++, 2001 + j++ }));
            m_EventRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "EVR电路断路器故障", new List<int>() { 2001 + j++, 2001 + j++, 2001 + j++, 2001 + j++ }));

            i += 2;
            m_EventRectList.Add(new DriveRect1(new Point(2, 150 + 22 * i++), "事件记录仪", null));
            m_EventRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "EVR在禁用状态", new List<int>() { 2001 + j++, 2001 + j++, 2001 + j++, 2001 + j++ }));
            m_EventRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "EVR在准备状态", new List<int>() { 2001 + j++, 2001 + j++, 2001 + j++, 2001 + j++ }));
            m_EventRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "EVR在记录状态", new List<int>() { 2001 + j++, 2001 + j++, 2001 + j++, 2001 + j++ }));
            m_EventRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "EVR在维护状态", new List<int>() { 2001 + j++, 2001 + j++, 2001 + j++, 2001 + j++ }));

            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_EventRectList)
            {
                v.OnDraw(g);
            }
        }
    }
}

