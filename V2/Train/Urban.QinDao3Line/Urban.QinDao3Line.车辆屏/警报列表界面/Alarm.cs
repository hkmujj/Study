using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Alarm:NewQBaseclass
    {
        private List<string> m_String;
        private bool[] IsBtndown = new bool[4];
        public override bool init(ref int nErrorObjectIndex)
        {
            AlarmData.InitData();
            IntiData();
            m_String = new List<string>()
            {
                string.Empty, 
                "RIOM R1  -  TC1",                              //1         
                ResourceFacade.MaintenanceResoursRIOMSelect,    //2
                "L_LSWpKbK_Tc1",                                //3
                "L_MushPressed_Tc1",
                "L_DSDPushBut_T_Tc1",
                "L_LocCabActive_Tc1",
                "L_PBkCockStus_Tc1",
                "L_AirCockStus_B2_Tc1",
                "L_AirCociStus_B1_Tc1",
                "L_PsgAIm_Tc1",
                "L_OBCUActive_Tc1",
                "L_IswATP_Tc1",
                "L_IswDCL_Tc1",
                "L_ReqPBkTL_Tc1",
                "L_FireAIm_Tc1",
                "L_FirePreAIm_Tc1",
                "L_FireFall_Tc1",
                "L_PBkApplied_Tc1",
                
                "L_IswCabDoor_Tc1",
                "L_IswMRP_Tc1",
                "L_IsEBLoop2_Tc1",
                "L_IsEBLoop_Tc1",
                "L_SwBackup_Tc1",
                "L_IswDSD_Tc1",
                "L_ShuntMode_Tc1",
                "L_ManualMode_1_Tc1",
                "L_16_M3_R1_Tc1",
                "L_EBRelay_Tc1",
                "L_14_M3_R1_Tc1",
                "L_13_M3_R1_Tc1",
                "L_IswBkNRIsd_Tc1",
                "L_BatDiscReg_Tc1",
                "L_ZeroSpeedTL_Tc1",
                "L_WashMode_Tc1",
                
                "LO_06_R1_Tc1",
                "LO_05_R1_Tc1",
                "LO_04_R1_Tc1",
                "LO_SpeedThres_Tc1",
                "LO_RstFAS_Tc1",
                "LO_StartAirComp_1_Tc1",
                
                //41
                "A_DCHVoltPosit1_Tc1",
                "AO_CatVolt_Tc1",
                "A_DCHVoltMax1_Tc1",
                "AO_02_M1_R1_Tc1"
            };
            return base.init(ref nErrorObjectIndex);
        }
        public override void paint(Graphics g)
        {
            FillRects(g);
            DrawRects(g);
            DrawImgs(g);
            DrawWords(g);
        }
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 4; index++)
            {
                if (AlarmData.Regions[index].IsVisible(point))
                {
                    IsBtndown[index] = true;
                }
            }
                    return base.mouseDown(point);
        }
        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 4; index++)
            {
                if (AlarmData.Regions[index].IsVisible(point))
                {
                    IsBtndown[index] = false;
                }
            }
            return base.mouseUp(point);
        }
        private void FillRects(Graphics e)
        {
            //上方的表格
            for(int i=0;i<46;i++)
            {
                e.FillRectangle(Common.m_WhiteBrush, AlarmData.Rects[48 + i]);
            }
        }
        private void DrawRects(Graphics e)
        {
            //上方的表格
            for (int i = 0; i < 46; i++)
            {
                e.DrawRectangle(Common.m_BlackPen , AlarmData.Rects[48 + i]);
            }
        }
        private void DrawImgs(Graphics e)
        {
            //指示灯
            for (int i = 0; i < 38; i++)
            {
                e.DrawImage(m_Imgs[4], AlarmData.Rects[10 + i]);
            }
            //按钮
            for (int i = 0; i < 4; i++)
            {
                if (IsBtndown[i])
                {
                    e.DrawImage(m_Imgs[8], AlarmData.Rects[1 + i]);
                }
                else
                {
                    e.DrawImage(m_Imgs[7], AlarmData.Rects[1 + i]);
                }
                e.DrawImage(m_Imgs[i], AlarmData.Rects[5 + i]);
            }
        }
        private void DrawWords(Graphics e)
        {
            e.DrawString(m_String[1], Common.m_Font14B, Common.m_BlackBrush, AlarmData.Rects[0],
               Common.m_MFormat);
            for (int i = 0; i < 38; i++)
            {
                e.DrawString(m_String[3+i], Common.m_Font11, Common.m_BlackBrush, AlarmData.Rects[48+i],
                              Common.m_LeftCenterFormat);
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[41 + i], Common.m_Font11, Common.m_BlackBrush, AlarmData.Rects[88 + i],
                              Common.m_LeftCenterFormat);
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[43 + i], Common.m_Font11, Common.m_BlackBrush, AlarmData.Rects[92 + i],
                              Common.m_LeftCenterFormat);
            }
            //RIOM 选择
            e.DrawString(m_String[2], Common.m_Font14, Common.m_BlackBrush, AlarmData.Rects[9],
                   Common.m_LeftCenterFormat);
        }
    }
}
