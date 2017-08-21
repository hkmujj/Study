using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH5G.底层共用;

namespace Motor.HMI.CRH5G.下载数据故障
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DownloadDataFault : CRH5GBase
    {
        public override string GetInfo()
        {
            return "下载数据故障视图";
        }

        public override bool Initalize()
        {
            return Init();
        }

        public override void Paint(Graphics g)
        {
            DrawOn(g);

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 11://上翻
                    if (m_SelectedIdx > 0)
                    {
                        m_SelectedIdx = (--m_SelectedIdx) % 3;
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 12://下翻
                    m_SelectedIdx = (++m_SelectedIdx) % 3;
                    ButtonsScreen.BtnState.Press();
                    break;
            }
        }

        private void DrawOn(Graphics e)
        {
            int index = 0;
            for (index = 0; index < m_NameArr.Length; index++)
            {
                e.DrawString(m_NameArr[index], FontsItems.Fonts_Regular( FontName .宋体 ,14f,false ), SolidBrushsItems.WhiteBrush,
                    m_RectsList[index], FontsItems.TheAlignment(FontRelated.居中));
               // e.DrawRectangle(PenItems.WhitePen, Rectangle.Round(_rectsList[index]));
             
            }


            if (m_SelectedIdx != -1)
            {

                e.DrawRectangle(new Pen(Color.Red,3), m_RectsList[m_SelectedIdx].X - 40, m_RectsList[m_SelectedIdx].Y,
                    m_RectsList[m_SelectedIdx].Width + 80, m_RectsList[m_SelectedIdx].Height);
                
            }


        }

        private bool Init()
        {

            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.DownloadDataFault);
            return true;

        }

        public override bool OnMouseDown(Point nPoint)
        {


            for (int i = 0; i < m_RectsList.Count; i++)
            {
                if (m_RectsList[i].Contains(nPoint))
                {
                    m_SelectedIdx = i;
                    break;
                }
            }


            return true;
        }

        public override bool OnMouseUp(Point nPoint)
        {

            return true;
        }


        private int m_SelectedIdx;

       
        private List<RectangleF> m_RectsList;

        private string[] m_NameArr =
        {
            "USB预驱动下载故障", "向USB下载辅助故障"  ,"强迫FTP故障下载"          
        };
    }

}
