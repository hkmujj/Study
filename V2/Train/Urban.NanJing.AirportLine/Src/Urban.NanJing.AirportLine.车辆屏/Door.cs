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
    public class Door : baseClass
    {
        private List<IGridRect> m_ATCRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "Door";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            int j = 0;
            m_ATCRectList.Add(new DefaultRect1(new Point(2, 150 + 22 * i++), "车门", null));
            m_ATCRectList.Add(new DefaultRect2(new Point(2, 150 + 22 * i++), "车门维护/第一页", null));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "安全回路状态异常", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "关门开关故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "关门故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "开门故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "门驱动电机回路故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "内部安全继电器故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "逻辑静态输出", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "位置传感器故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "门参数更新故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "关门输入故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "开门输入故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
            m_ATCRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "门释放输入故障", new List<int>() { 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++, 1609 + j++ }));
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
}