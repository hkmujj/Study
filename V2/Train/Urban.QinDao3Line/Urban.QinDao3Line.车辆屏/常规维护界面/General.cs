using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class General : NewQBaseclass
    {
        /// <summary>
        /// 读取配置文件的路径
        /// </summary>

        private List<string> m_Resources;

        private readonly GeneralData m_General = new GeneralData();


        public override bool init(ref int nErrorObjectIndex)
        {
            m_Resources = new List<string>
            {

                // to make usefull index start at 1.
                string.Empty,                                                             //0
                ResourceFacade.MaintenanceBasicResourceMVBcommunication,                  //1
                ResourceFacade.MaintenanceBasicResourceOperational,                       //2
                ResourceFacade.MaintenanceBasicResourceEffortDemand,                      //3
                ResourceFacade.MaintenanceBasicResourceEffortAchieved,                    //4 
                ResourceFacade.MaintenanceBasicResourceEffortPossible,                    //5
                ResourceFacade.MaintenanceBasicResourceMPU,                               //6
                ResourceFacade.MaintenanceBasicResourcePCE1,                              //7
                ResourceFacade.MaintenanceBasicResourcePCE2,                              //8
                ResourceFacade.MaintenanceBasicResourcePCE3,                              //9
                ResourceFacade.MaintenanceBasicResourcePCE4,                              //10
                ResourceFacade.MaintenanceBasicResourceMPU,                               //11 
                "BCU1",                             //12
                "BCU2",                              //13
                "BCU3",                              //14
                "BCU4",                              //15
                ResourceFacade.MaintenanceBasicResourcekN,                                //16
                ResourceFacade.MaintenanceBasicResourceTOTAL,                             //17
                ResourceFacade.MaintenanceBasicResourceEnergyConsumed,                    //18
                ResourceFacade.MaintenanceBasicResourceEnergyRegenerated,                 //19
                ResourceFacade.MaintenanceBasicResourcekWh,                               //20
                ResourceFacade.MaintenanceResourceMaintenanceBaseInformation                         //21
            };

            m_General.RectData();

            IntiData();

            return true;
        }

        public override void paint(Graphics g)
        {
            FillBackruond(g);

            DrawRects(g);

            DrawWords(g);

            DrawImgs(g);

            DrawFloatState(g);
        }

        private void DrawRects(Graphics g)
        {
            for (int i = 0; i < 60; i++)
            {
                g.DrawRectangle(Common.m_BlackPen, GeneralData.Rects[i]);
            }
        }

        private void DrawWords(Graphics g)
        {
            //上排左侧标题
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(m_Resources[i + 1], Common.m_Font11B, Common.m_BlackBrush, GeneralData.Rects[i], Common.m_LeftCenterFormat);
            }
            //下排左侧标题
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(m_Resources[i + 1], Common.m_Font11B, Common.m_BlackBrush,
                    GeneralData.Rects[i + 4], Common.m_LeftCenterFormat);
            }
            //上排横标题
            for (int i = 0; i < 5; i++)
            {
                g.DrawString(m_Resources[6 + i], Common.m_Font12B, Common.m_BlackBrush, GeneralData.Rects[8 + i],
                    Common.m_MFormat);
            }
            //下排横标题
            for (int i = 0; i < 5; i++)
            {
                g.DrawString(m_Resources[11 + i], Common.m_Font12B, Common.m_BlackBrush, GeneralData.Rects[33 + i],
                    Common.m_MFormat);
            }
            //总计、已消耗能量、已再生能量
            for (int i = 0; i < 3; i++)
            {
                g.DrawString(m_Resources[17 + i], Common.m_Font12B, Common.m_BlackBrush, GeneralData.Rects[60 + i]);
            }
            //维护：基本信息
            g.DrawString(m_Resources[21], Common.m_Font12B, Common.m_BlackBrush, GeneralData.Rects[85]);
        }

        /// <summary>
        /// 底图背景颜色
        /// </summary>
        /// <param name="g"></param>
        private void FillBackruond(Graphics g)
        {
            for (int i = 0; i < 60; i++)
            {
                g.FillRectangle(Common.m_GreyWhite, GeneralData.Rects[i]);
            }

        }

        private void DrawImgs(Graphics g)
        {
            g.DrawImage(m_Imgs[0], GeneralData.Rects[63]);
            for (int i = 0; i < 5; i++)
            {
                //PCE MVB通信故障
                if (BoolList[m_BoolIds[i]])
                {
                    g.DrawImage(m_Imgs[1], GeneralData.Rects[64 + i]);
                }
                else
                {
                    if(BoolList[m_BoolIds[20+i]])
                        g.DrawImage(m_Imgs[2], GeneralData.Rects[64 + i]);
                    else
                        g.DrawImage(m_Imgs[3], GeneralData.Rects[64 + i]);
                }
                //GTW MVB通信故障
                if (BoolList[m_BoolIds[5 + i]])
                {
                    g.DrawImage(m_Imgs[1], GeneralData.Rects[74 + i]);
                }
                else
                {
                    if (BoolList[m_BoolIds[25 + i]])
                        g.DrawImage(m_Imgs[2], GeneralData.Rects[74 + i]);
                    else
                        g.DrawImage(m_Imgs[3], GeneralData.Rects[74 + i]);
                }
                //PCE 运行故障
                if (BoolList[m_BoolIds[10 + i]])
                {
                    g.DrawImage(m_Imgs[1], GeneralData.Rects[69 + i]);
                }
                else
                {
                    if (BoolList[m_BoolIds[30 + i]])
                        g.DrawImage(m_Imgs[2], GeneralData.Rects[69 + i]);
                    else
                        g.DrawImage(m_Imgs[3], GeneralData.Rects[69 + i]);
                }
                //GTW 运行故障
                if (BoolList[m_BoolIds[15 + i]])
                {
                    g.DrawImage(m_Imgs[1], GeneralData.Rects[79 + i]);
                }
                else
                {
                    if (BoolList[m_BoolIds[35 + i]])
                        g.DrawImage(m_Imgs[2], GeneralData.Rects[79 + i]);
                    else
                        g.DrawImage(m_Imgs[3], GeneralData.Rects[79 + i]);
                }
            }
        }

        private void DrawFloatState(Graphics g)
        {
            //上排信息
                g.DrawString(FloatList[m_FoolatIds[0]].ToString("0.0") + " kN", Common.m_Font12, Common.m_BlackBrush, GeneralData.Rects[23],
                    Common.m_MFormat);
                for (int i = 0; i < 4; i++)
                {
                    g.DrawString(FloatList[m_FoolatIds[2 + 2 * i]].ToString("0") + " %", Common.m_Font12, Common.m_BlackBrush, GeneralData.Rects[24 + i],
                                       Common.m_MFormat);
                    g.DrawString(FloatList[m_FoolatIds[3 + 2 * i]].ToString("0.0") + " kN", Common.m_Font12, Common.m_BlackBrush, GeneralData.Rects[29 + i],
                                       Common.m_MFormat);
                }
            
            //下排信息
                g.DrawString(FloatList[m_FoolatIds[1]].ToString("0.0") + " kN", Common.m_Font12, Common.m_BlackBrush, GeneralData.Rects[48],
                       Common.m_MFormat);
                for (int i = 0; i < 4; i++)
                {
                    g.DrawString(FloatList[m_FoolatIds[10 + 2 * i]].ToString("0") + " %", Common.m_Font12, Common.m_BlackBrush, GeneralData.Rects[49 + i],
                                       Common.m_MFormat);
                    g.DrawString(FloatList[m_FoolatIds[11 + 2 * i]].ToString("0.0") + " kN", Common.m_Font12, Common.m_BlackBrush, GeneralData.Rects[54 + i],
                                       Common.m_MFormat);
                }
            for (int i = 0; i < 2; i++)
            {
                g.DrawString(FloatList[m_FoolatIds[18+i]].ToString("0.00"), Common.m_Font16B, Common.m_BlackBrush, GeneralData.Rects[58 + i],
                 Common.m_MFormat);
                g.DrawString("kWh", Common.m_Font16B, Common.m_BlackBrush, GeneralData.Rects[58 + i], Common.m_RightCenterFormat);
            }
            g.DrawString(FloatList[m_FoolatIds[20]].ToString("0.0"), Common.m_Font12B, Common.m_BlackBrush, GeneralData.Rects[84],
                 Common.m_MFormat);
        }
    }
}
