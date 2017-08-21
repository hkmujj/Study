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
    public class PowerSupply : baseClass
    {
        private List<IGridRect> m_PowerRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "中压供电";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            int j = 0;
            m_PowerRectList.Add(new DriveRect1(new Point(2, 150 + 22 * i++), "中压供电", null));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "辅助逆变器状态", null));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "高压值（V）", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "直流输出电压（V）", new List<int>() { 90, 91 }, 1));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "直流输出电流（A）", new List<int>() { 92, 93 }, 1));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * (i++)), "直流电池电流（A）", new List<int>() { 94, 95 }, 1));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * (i++)), "交流电压（V）", new List<int>() { 96, 97 }, 1));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * (i++)), "交流电流（A）", new List<int>() { 98, 99 }, 1));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "蓄电池温度状态", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect2(new Point(2, 150 + 22 * i++), "中压供电维护", null));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "辅助逆变器", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "辅助逆变器", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "蓄电池充电故障", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "蓄电池接地故障", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "蓄电池熔断器故障", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "永久低压侦测故障", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "准备低压分布故障", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            m_PowerRectList.Add(new DriveRect(new Point(2, 150 + 22 * i++), "永久低压分布故障", new List<int>() { 1557 + j++, 1557 + j++, 1557 + j++, 1557 + j++ }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_PowerRectList)
            {
                v.OnDraw(g);
            }
        }

    }
}
