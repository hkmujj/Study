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
    public class AirCondition : baseClass
    {
        private List<IGridRect> m_AirRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "空调";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            int j = 0;
            m_AirRectList.Add(new DefaultRect1(new Point(2, 150 + 22 * i++), "空调", null));
            m_AirRectList.Add(new DefaultRect2(new Point(2, 150 + 22 * i++), "空调操作", null));
            m_AirRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "空调可使用性", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "环境温度(C)", new List<int>() { 102, 103, 104, 105, 106, 107 }, 1));
            m_AirRectList.Add(new DefaultRect3(new Point(2, 150 + 22 * i++), "外部温度(C)", new List<int>() { 108 }, 1));
            m_AirRectList.Add(new DefaultRect3(new Point(2, 150 + 22 * i++), "能耗统计（Kwh）", new List<int>() { 109 }, 1));
            m_AirRectList.Add(new DefaultRect2(new Point(2, 150 + 22 * i++), "空调维护/第一页", null));
            m_AirRectList.Add(new BrakeRect(new Point(2, 150 + 22 * i++), "自助模式选择", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "空调自检结果", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new BrakeRect(new Point(2, 150 + 22 * i++), "单元状态", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new BrakeRect(new Point(2, 150 + 22 * i++), "压力故障", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new BrakeRect(new Point(2, 150 + 22 * i++), "蒸发器故障", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new BrakeRect(new Point(2, 150 + 22 * i++), "冷凝器故障", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new BrakeRect(new Point(2, 150 + 22 * i++), "压缩机故障", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new BrakeRect(new Point(2, 150 + 22 * i++), "低电压故障", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            m_AirRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "紧急逆变器故障", new List<int>() { 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++, 1753 + j++ }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_AirRectList)
            {
                v.OnDraw(g);
            }
        }
    }
}

