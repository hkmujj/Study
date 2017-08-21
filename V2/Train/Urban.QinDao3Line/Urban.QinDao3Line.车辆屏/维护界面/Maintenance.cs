using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Maintenance : NewQBaseclass
    {
        private readonly MaintanceData m_Maindata = new MaintanceData();

        private List<int> m_TargetViewIndexCollection;

        private List<GDIButton> m_ControlBtns;

        public override bool init(ref int nErrorObjectIndex)
        {
            m_TargetViewIndexCollection = new List<int>()
            {
                11,             //基本信息
                12,             //制动
                18,             //空压机
                14,             //牵引
                15,             //辅助逆变器
                17,             //蓄电池
                25,             //软件版本
                19,             //空调
                21,             //车门列车线
                26,             //黑匣子
                22,             //烟火检测
                51,             //乘客信息系统
                23,             //RIOM
                24,             //MVB网络
                20,             //车门
                28              //性能
            };


            m_Maindata.Init();
            IntiData();

            // 从左到右， 从上到下
            m_ControlBtns =
                Enumerable.Range(0, 16)
                    .Select(
                        s =>
                            new GDIButton()
                            {
                                OutLineRectangle = m_Maindata.m_Rects[s],
                                //BackImage = m_Imgs[s],
                                Tag = m_TargetViewIndexCollection[s],
                                ButtonUpEvent = ButtonUpEvent
                            })
                    .ToList();
            return true;
        }

        private void ButtonUpEvent(object sender, EventArgs eventArgs)
        {
            ChangePageTo(((GDIButton)sender).Tag);
        }

        private void ChangePageTo(object o)
        {
            var index = (int)o;
            append_postCmd(CmdType.ChangePage, index, 0, 0);
        }

        public override void paint(Graphics g)
        {
            DrawWords(g);
            DrawButton(g);
            //m_ControlBtns.ForEach(e=> e.OnDraw(g));
        }

        public override bool mouseUp(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseUp(point));
        }

        private void DrawWords(Graphics g)
        {
            g.DrawString(ResourceFacade.MaintenanceResourceMaintenancefunctionselector, Common.m_Font12B,
                Common.m_BlackBrush, m_Maindata.m_Rects[16]);
            g.DrawString(ResourceFacade.MaintenanceResourceAccessToSoftwareConfigurationInformation,
                Common.m_Font12B, Common.m_BlackBrush, m_Maindata.m_Rects[17]);
        }

        private void DrawButton(Graphics g)
        {
            for (int i = 0; i < 16; i++)
            {
                g.DrawImage(m_Imgs[i], m_Maindata.m_Rects[i]);
                
            }
            for (int i = 0; i < 16; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    g.DrawImage(m_Imgs[16+i], m_Maindata.m_Rects[i]);
                }
                else
                {
                    if (BoolList[m_BoolIds[16 + i]])
                    {
                        g.DrawImage(m_Imgs[32+i], m_Maindata.m_Rects[i]);
                    }
                    else
                    g.DrawImage(m_Imgs[i], m_Maindata.m_Rects[i]);
                }
                
            }
        }
    }
}
