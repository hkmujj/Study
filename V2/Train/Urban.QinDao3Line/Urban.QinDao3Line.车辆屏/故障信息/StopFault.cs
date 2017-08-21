using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;
using Urban.QingDao3Line.MMI.底层共用;
using Excel.Interface;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class StopFault : NewQBaseclass
    {
        private List<string> m_Resources;
        private List<Region> m_Region = new List<Region>();
        private List<RectangleF> m_RectsList;
        private List<FaultInfo> m_FaultInfos;          //所有的故障信息
        private List<FaultInfo> m_Fault = new List<FaultInfo>();               //需显示的故障信息
        private FlashTimer m_FlashTimer;
        private bool[] IsDown = new bool[2];
        private int totalPage = 1;                     //总页面
        private int curPage = 1;                       //当前页
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            Init();
            m_Resources = new List<string>()
            {
                "不正确操作状态清单",
                "/",
                "类型",
                "说明",
                "请检查断路器。或者需要进行维护操作。"
            };
            return true;
        }
        public override void paint(Graphics g)
        {
            DrawWords(g);
            DrawRectangles(g);
        }
        private void DrawWords(Graphics e)
        {
            e.DrawString(m_Resources[0],
                                 Common.m_Font10B, SolidBrushsItems.BlackBrush,
                                 m_RectsList[0], Common.m_MFormat);
            e.DrawString(curPage + m_Resources[1] + totalPage,
                                 Common.m_Font10B, SolidBrushsItems.BlackBrush,
                                 m_RectsList[1], Common.m_MFormat);
            e.DrawString(m_Resources[2],
                                 Common.m_Font10B, SolidBrushsItems.BlackBrush,
                                 m_RectsList[2], Common.m_MFormat);
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Resources[3 + i],
                Common.m_Font9B, SolidBrushsItems.BlackBrush,
                m_RectsList[3 + i], Common.m_LeftCenterFormat);
            }
        }
        private void DrawRectangles(Graphics e)
        {
            int index = 0;
            m_Fault.Clear();
            //两个打钩图片
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(m_Imgs[i], m_RectsList[6 + i]);
            }
            //说明框
            e.DrawRectangle(Common.m_BlackPen1, m_RectsList[5].X, m_RectsList[5].Y, m_RectsList[5].Width, m_RectsList[5].Height);
            foreach (FaultInfo info in m_FaultInfos)
            {
                if (BoolList[info.Logic])
                {
                    m_Fault.Add(info);
                    index++;
                }
            }
            totalPage = m_Fault.Count / 9 + 1;
            for (int i = 0; i < m_Fault.Count; i++)
            {
                if (i >= (curPage - 1) * 9 && i < curPage * 9)
                {
                    e.FillRectangle(Common.m_WhiteBrush, m_RectsList[i % 9 + 8]);
                    e.DrawRectangle(Common.m_BlackPen, m_RectsList[i % 9 + 8].X, m_RectsList[i % 9 + 8].Y, m_RectsList[i % 9 + 8].Width, m_RectsList[i % 9 + 8].Height);
                    e.DrawString(m_Fault[i].Context,
                                 Common.m_Font11, SolidBrushsItems.BlackBrush,
                                 m_RectsList[i % 9 + 8], Common.m_LeftMidumFormat);
                    e.FillRectangle(Common.m_WhiteBrush, m_RectsList[i % 9 + 17]);
                    e.DrawRectangle(Common.m_BlackPen, m_RectsList[i % 9 + 17].X, m_RectsList[i % 9 + 17].Y, m_RectsList[i % 9 + 17].Width, m_RectsList[i % 9 + 17].Height);
                    e.DrawString(m_Fault[i].FaultType,
                                 Common.m_Font11, SolidBrushsItems.BlackBrush,
                                 m_RectsList[i % 9 + 17], Common.m_MFormat);
                }
            }
            if (index > 9)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (IsDown[i])
                    {
                        e.DrawImage(m_Imgs[8], m_RectsList[41 + i]);
                        if (curPage < totalPage && i == 1)
                            curPage++;
                        if (curPage > 1 && i == 0)
                            curPage--;
                    }
                    else
                        e.DrawImage(m_Imgs[7], m_RectsList[41 + i]);
                    e.DrawImage(m_Imgs[5 + i], m_RectsList[39 + i]);
                }
            }
            if (m_FlashTimer.IsNeedFlash())
            {
                for (int i = 0; i < m_Fault.Count; i++)
                {
                    if (i >= (curPage - 1) * 9 && i < curPage * 9)
                    {
                        e.DrawImage(GetLog(m_Fault[i].Log), m_RectsList[i % 9 + 26]);
                    }
                }
            }
        }
        public override bool mouseDown(Point point)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_Region[i].IsVisible(point))
                    IsDown[i] = true;
            }
            return base.mouseDown(point);
        }
        public override bool mouseUp(Point point)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_Region[i].IsVisible(point))
                    IsDown[i] = false;
            }
            return base.mouseUp(point);
        }
        private Image GetLog(string name)
        {
            switch (name)
            {
                case "清客图标":
                    return m_Imgs[2];
                case "日运行结束":
                    return m_Imgs[3];
                case "线路终点":
                    return m_Imgs[4];
                default:
                    break;
            }
            return null;
        }
        private void Init()
        {
            m_FaultInfos = ExcelParser.Parser<FaultInfo>(AppConfig.AppPaths.ConfigDirectory).ToList();
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            Coordinate.RectangleFLists(ViewIDName.FaultInformationScreen, ref m_RectsList);
            m_FlashTimer = new FlashTimer(1);
            for (int i = 0; i < 2; i++)
            {
                m_Region.Add(new Region(m_RectsList[39 + i]));
            }
        }
    }
    [ExcelLocation("故障信息.xls", "FaultInfo")]
    public class FaultInfo : ISetValueProvider
    {
        [ExcelField("LogicNumber")]
        public int Logic { get; set; }
        [ExcelField("Log")]
        public string Log { get; set; }
        [ExcelField("Context")]
        public string Context { get; set; }
        [ExcelField("FaultType")]
        public string FaultType { get; set; }
        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Logic":
                    Logic = Convert.ToInt32(value);
                    break;
                case "Context":
                    Context = value;
                    break;
                case "Log":
                    Log = value;
                    break;
                case "FaultType":
                    FaultType = value;
                    break;
                default:
                    break;
            }
        }
    }
}
