using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;
namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BlackBoxButton : NewQBaseclass
    {
        private List<string> m_String;
        private bool[] IsBtnDown = new bool[3];
        private List<Region> m_Regions = new List<Region>();
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            BlackBoxData.InitData();
            m_String = new List<string>() 
            {
                string.Empty,                                                  //0
                ResourceFacade.MaintenanceAuxiliaryResourceFaultEvent,         //1     故障事件
                ResourceFacade.MaintenanceBrakeResourceTrainLines,             //2     列车线
                ResourceFacade.MaintenanceBlackBoxResourceData                 //3     数据
            };
            for (int i = 0; i < 3; i++)
            {
                m_Regions.Add(new Region(BlackBoxData.Rects[4 + i]));
            }
            return true;
        }
        public override void paint(Graphics g)
        {
            DrawBtnState(g);
        }
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (m_Regions[index].IsVisible(point))
                {
                    IsBtnDown[index] = true;
                }
            }
            return true;
        }
        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (m_Regions[index].IsVisible(point))
                {
                    BtnValueSet(index);
                    switch (index)
                    {
                        case 0:
                            append_postCmd(CmdType.ChangePage, 26, 1, 0);
                            break;
                        case 1:
                            append_postCmd(CmdType.ChangePage, 52, 1, 0);
                            break;
                        case 2:
                            append_postCmd(CmdType.ChangePage, 53, 1, 0);
                            break;
                    }
                }
            }
            return true;
        }
        private void DrawBtnState(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (IsBtnDown[i])
                {
                    e.DrawImage(m_Imgs[1], BlackBoxData.Rects[4 + i]);
                }
                else
                {
                    e.DrawImage(m_Imgs[0], BlackBoxData.Rects[4 + i]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(m_String[1 + i], Common.m_Font10, Common.m_BlackBrush, BlackBoxData.Rects[1 + i],
               Common.m_MFormat);
            }
        }
        private void BtnValueSet(int a)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == a)
                {
                    IsBtnDown[i] = true;
                }
                else
                    IsBtnDown[i] = false;
            }
        }
    }
}


