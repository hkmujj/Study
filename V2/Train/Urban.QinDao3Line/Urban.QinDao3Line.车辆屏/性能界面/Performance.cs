using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI.参数界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Performance : NewQBaseclass
    {
        private readonly List<Region> m_BtnArea = new List<Region>();
        private bool m_BtnIsDown;
        private int[] index = new int[2];
        private List<string> m_string;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            m_string = new List<string>()
            {
                string.Empty,           //0
                ResourceFacade.MaintenceDragAndBrake,
                //加速测试
                ResourceFacade.MaintenceAccelerateTest,
                ResourceFacade.MaintenceTargetSpeed,
                ResourceFacade.MaintenceAverageAcceleration,

                //减速测试
                ResourceFacade.MaintenceReduceTest,  //5
                ResourceFacade.MaintenceTargetSpeed,
                ResourceFacade.MaintenceAverageReduce,
                ResourceFacade.MaintenceBrakeDistance,
                ResourceFacade.MaintenanceBrakeResourceSTART,
                "kph",            //10
                "0.1m/s",
                "kph","0.1m/s",
                "m"
            };
            PerformanceData.InitData();
            return true;
        }

        public override bool mouseDown(Point point)
        {
            for (int i = 0; i < 3; i++)
            {
                if (PerformanceData.Regions[i].IsVisible(point))
                {
                    switch (i)
                    {
                        case 0:
                            if (index[0] == 0)
                                index[0] = 1;
                            else
                                index[0] = 0;
                            break;
                        case 1:
                            if (index[1] == 0)
                                index[1] = 1;
                            else
                                index[1] = 0;
                            break;
                        case 2:
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 1, 0);
                            m_BtnIsDown = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            for (int i = 0; i < 3; i++)
            {
                if (PerformanceData.Regions[i].IsVisible(point))
                {

                    switch (i)
                    {
                        case 0:
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], index[0], 0);
                            break;
                        case 1:
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], index[1], 0);
                            break;
                        case 2:
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 0, 0);
                            m_BtnIsDown = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            PaintState(g);
            DrawRect(g);
            DrawWord(g);
        }
        private void DrawRect(Graphics e)
        {
            for (int i = 1; i < 15; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, PerformanceData.Rects[i]);
            }
        }
        private void DrawWord(Graphics e)
        {
            e.DrawString(m_string[1], Common.m_Font14B, Common.m_BlackBrush, PerformanceData.Rects[0], Common.m_MFormat);
            for (int i = 0; i < 7; i++)
            {
                e.DrawString(m_string[2 + i], Common.m_Font12B, Common.m_BlackBrush, PerformanceData.Rects[1 + i], Common.m_MFormat);
            }
            e.DrawString(index[0] == 0 ? "NOK" : "OK", Common.m_Font12, Common.m_BlackBrush, PerformanceData.Rects[8], Common.m_MFormat);
            e.DrawString(index[1] == 0 ? "NOK" : "OK", Common.m_Font12, Common.m_BlackBrush, PerformanceData.Rects[11], Common.m_MFormat);
            e.DrawString(m_string[9], Common.m_Font11B, Common.m_BlackBrush, PerformanceData.Rects[15], Common.m_MFormat);
            //数值及单位
            for (int i = 0; i < 5; i++)
            {
                if (i < 2)
                    e.DrawString(FloatList[m_FoolatIds[i]].ToString("0"), Common.m_Font11, Common.m_BlackBrush, PerformanceData.Rects[9 + i], Common.m_MFormat);
                else
                    e.DrawString(FloatList[m_FoolatIds[i]].ToString("0"), Common.m_Font11, Common.m_BlackBrush, PerformanceData.Rects[10 + i], Common.m_MFormat);
                e.DrawString(m_string[10 + i], Common.m_Font11, Common.m_BlackBrush, PerformanceData.Rects[16 + i], Common.m_MFormat);
            }
        }
        private void PaintState(Graphics e)
        {
            //开始按钮
            if (m_BtnIsDown)
                e.DrawImage(m_Imgs[1], PerformanceData.Rects[15]);
            else
                e.DrawImage(m_Imgs[0], PerformanceData.Rects[15]);
            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(index[i] == 0 ? Common.m_RedBrush : Common.m_GreenBrush, PerformanceData.Rects[8 + 3 * i]);
            }
        }
    }
}
