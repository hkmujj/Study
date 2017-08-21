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
    class EmergencyAlarm : NewQBaseclass
    {
        private List<string> m_Resources;
        private List<RectangleF> m_RectsList;
        private List<FaultInfo> m_FaultInfos;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            Init();
            m_Resources = new List<string>()
            {
                "施加紧急制动",
                "紧急制动已施加"
            };
            return true;
        }
        public override void paint(Graphics g)
        {
            DrawImage(g);
            DrawRectangles(g);
            DrawWords(g);
        }
        private void DrawImage(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(m_Imgs[i], m_RectsList[35+i]);
            }   
        }
        private void DrawWords(Graphics e)
        {
            e.DrawString(m_Resources[0], Common.m_Font14B, Common.m_BlackBrush, m_RectsList[37],Common.m_LeftCenterFormat);
            e.DrawString(m_Resources[1], Common.m_Font10B, Common.m_BlackBrush, m_RectsList[38], Common.m_LeftCenterFormat);
        }
        private void DrawRectangles(Graphics e)
        {
            e.DrawRectangle(Common.m_BlackPen, m_RectsList[38].X, m_RectsList[38].Y, m_RectsList[38].Width, m_RectsList[38].Height);
        }
        private void Init()
        {
            m_FaultInfos = ExcelParser.Parser<FaultInfo>(AppConfig.AppPaths.ConfigDirectory).ToList();
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            Coordinate.RectangleFLists(ViewIDName.FaultInformationScreen, ref m_RectsList);
        }
    }
}
