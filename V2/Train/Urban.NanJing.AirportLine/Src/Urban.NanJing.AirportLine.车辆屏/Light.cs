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
    public class Light : baseClass
    {
        private List<IGridRect> m_LightRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "灯";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            int j = 0;
            m_LightRectList.Add(new DefaultRect1(new Point(2, 150 + 22 * i++), "照明", null));
            m_LightRectList.Add(new DefaultRect2(new Point(2, 150 + 22 * i++), "照明维护", null));
            m_LightRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "常用照明指令故障", new List<int>() { 1969 + j++, 1969 + j++, -1, -1, -1, -1, -1, -1, -1, -1, 1969 + j++, 1969 + j++ }));
            m_LightRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "常用照明故障", new List<int>() { 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++ }));
            m_LightRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "应急照明故障", new List<int>() { 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++, 1969 + j++ }));

            m_LightRectList.Add(new DefaultRect2(new Point(2, 150 + 22 * i++), "ESG维护", null));
            m_LightRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "尾灯故障", new List<int>() { 1969 + j++, 1969 + j++, -1, -1, -1, -1, -1, -1, -1, -1, 1969 + j++, 1969 + j++ }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_LightRectList)
            {
                v.OnDraw(g);
            }
        }
    }
}

