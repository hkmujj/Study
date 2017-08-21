using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Doors : NewQBaseclass
    {
        
        private List<RectangleF> m_RectsList = new List<RectangleF>();
        public override bool init(ref int nErrorObjectIndex)
        {
            Init();
            IntiData();
            return true;
        }
        private int CurrentViewId = 0;
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                CurrentViewId = nParaB;
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }
        public override void paint(Graphics g)
        {
            DrawDoors(g);
            DrawDoorState(g);
            DrawWord(g);
            if (CurrentViewId == 20)
            {
                DrawChooseCar(g);
            }
        }
        public void DrawDoors(Graphics e)
        {

            //车头
            for (int i = 0; i < 8; i++)
            {
                if (BoolList[m_BoolIds[344 + i]])
                {
                    e.DrawImage(m_Imgs[0 + i], i < 4 ? m_RectsList[0] : m_RectsList[1]);
                }
            }
            if (!BoolList[m_BoolIds[344]] && !BoolList[m_BoolIds[345]]
                && !BoolList[m_BoolIds[346]] && !BoolList[m_BoolIds[347]])
            {
                e.DrawImage(m_Imgs[2], m_RectsList[0]);
            }
            if (!BoolList[m_BoolIds[348]] && !BoolList[m_BoolIds[349]]
               && !BoolList[m_BoolIds[350]] && !BoolList[m_BoolIds[351]])
            {
                e.DrawImage(m_Imgs[6], m_RectsList[1]);
            }
            //驾驶室
            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(SolidBrushsItems.SoftYellow, m_RectsList[2 + i]);
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[2 + i].X, m_RectsList[2 + i].Y, m_RectsList[2 + i].Width,
                    m_RectsList[2 + i].Height);
            }
            for (int i = 0; i < 4; i++)
            {
                e.DrawImage(m_Imgs[10], m_RectsList[4 + i]);
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[m_BoolIds[352 + j + i * 3]])
                    {
                        e.DrawImage(m_Imgs[8 + j], m_RectsList[4 + i]);
                    }
                }
            }

            //车厢
            for (int i = 0; i < 6; i++)
            {
                e.FillRectangle(SolidBrushsItems.SoftYellow, m_RectsList[8 + i]);
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[8 + i].X, m_RectsList[8 + i].Y, m_RectsList[8 + i].Width,
                    m_RectsList[8 + i].Height);
            }
        }
        private void DrawWord(Graphics e)
        {
            for (int i = 1; i < 7; i++)
            {
                e.DrawString(i.ToString(), FontItems.Fonts_Regular(FontName.Arial, 11, true), Common.m_BlackBrush,
                                   m_RectsList[i + 7], Common.m_MFormat);
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    e.DrawString(j.ToString(), FontItems.Fonts_Regular(FontName.Arial, 11, true), Common.m_BlackBrush,
                                                      m_RectsList[13+j+8*i], Common.m_MFormat);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j <9; j++)
                {
                    e.DrawString((9 - j).ToString(), FontItems.Fonts_Regular(FontName.Arial, 11, true), Common.m_BlackBrush,
                                                      m_RectsList[37 + j + 8 * i], Common.m_MFormat);
                }
            }
        }
        private void DrawDoorState(Graphics e)
        {
            for (int i = 0; i < 48; i++)
            {
                e.DrawImage(m_Imgs[18], m_RectsList[14 + i]);
            }

            //门的状态
            for (int i = 0; i < 48; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (BoolList[m_BoolIds[8 + j + i * 7]])
                    {
                        e.DrawImage(m_Imgs[18 + j], m_RectsList[14 + i]);
                    }
                }
            }
        }

        private void DrawChooseCar(Graphics e)
        {
            e.DrawRectangle(new Pen(Color.FromArgb(0, 255, 0), 4), m_RectsList[8 + DoorsMaintenance.CarChoose].X - 1,
                m_RectsList[8 + DoorsMaintenance.CarChoose].Y-1, m_RectsList[8 + DoorsMaintenance.CarChoose].Width+3,
                    m_RectsList[8 + DoorsMaintenance.CarChoose].Height+3);
        }
        private void Init()
        {
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            Coordinate.RectangleFLists(ViewIDName.MainDrivingScreen, ref m_RectsList);
        }

    }
}
