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
    public class PassengerInfo : baseClass
    {
        private List<IGridRect> m_PassengerRectList = new List<IGridRect>();
        private int m_CurrentPage = 0;

        public override string GetInfo()
        {
            return "乘客状态";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            int j = 0;
            m_PassengerRectList.Add(new DefaultRect1(new Point(2, 150 + 22 * i++), "乘客信息系统", null));
            m_PassengerRectList.Add(new DefaultRect2(new Point(2, 150 + 22 * i++), "乘客信息系统/第一页", null));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "广播视频单元（ACSU）故障", new List<int>() { 1205 + j++, 1205 + j++, -1, -1, -1, -1, -1, -1, -1, -1, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "司机语言控制单元（DACU）故障", new List<int>() { 1205 + j++, 1205 + j++, -1, -1, -1, -1, -1, -1, -1, -1, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "客室广播视频控制单元（PACU）故障", new List<int>() { 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "车载硬盘录像机故障", new List<int>() { 1205 + j++, 1205 + j++, -1, -1, -1, -1, -1, -1, -1, -1, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "乘客紧急对讲装置1故障", new List<int>() { 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "乘客紧急对讲装置2故障", new List<int>() { 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "内部显示单元1冲突", new List<int>() { 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "内部显示单元2故障", new List<int>() { 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "前端显示单元故障", new List<int>() { 1205 + j++, 1205 + j++, -1, -1, -1, -1, -1, -1, -1, -1, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "一端摄像头", new List<int>() { 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "二端摄像头", new List<int>() { 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++ }));
            m_PassengerRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "二端摄像头", new List<int>() { 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++, -1, -1, -1, -1, 1205 + j++, 1205 + j++, 1205 + j++, 1205 + j++ }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_PassengerRectList)
            {
                v.OnDraw(g);
            }
        }


    }
}
