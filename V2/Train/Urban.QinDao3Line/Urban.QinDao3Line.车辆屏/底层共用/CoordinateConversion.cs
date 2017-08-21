using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI.底层共用
{
    public static class Coordinate
    {
        public static bool RectangleFLists(ViewIDName className, ref List<RectangleF> rectanglesList)
        {
            if (!m_ClassIsInited) return false;
            else
            {
                switch (className)
                {
                    case ViewIDName.TitleScreen:
                        rectanglesList = m_TitleScreenArea;
                        return true;
                    case ViewIDName.TrainStartUpManagementScreen:
                        rectanglesList = m_TrainStartUpManagementScreenArea;
                        return true;
                    case ViewIDName.MainDrivingScreen:
                        rectanglesList = m_MainDrivingScreenArea;
                        return true;
                    case ViewIDName.ManualTrainStartUpManagementScreen:
                        rectanglesList = m_TrainStartUpManagementScreenArea;
                        return true;
                    case ViewIDName.EmergencyMessagesScreen:
                        rectanglesList = m_EmergencyMessagesScreenArea;
                        return true;
                    case ViewIDName.ParameterScreen:
                        rectanglesList = m_ParameterScreenArea;
                        return true;
                    case ViewIDName.HVACModeSelectionScreen:
                        rectanglesList = m_HVACModeSelectionScreenArea;
                        return true;
                    case ViewIDName.PassWordScreen:
                        rectanglesList = m_PassWordScreenArea;
                        return true;
                    case ViewIDName.DateAndTimeScreen:
                        rectanglesList = m_DateAndTimeScreenArea;
                        return true;
                    case ViewIDName.FaultInformationScreen:
                        rectanglesList = m_FaultInformationScreenArea;
                        return true;
                    default:
                        return false;
                }
            }
        }

        public static bool AddRectangle(ViewIDName className, ref List<RectangleF> rectanglesList, int index)
        {
            if (!m_ClassIsInited) return false;
            else
            {
                switch (className)
                {
                    case ViewIDName.TitleScreen:
                        rectanglesList.Add(m_TitleScreenArea[index]);
                        return true;
                    case ViewIDName.TrainStartUpManagementScreen:
                        rectanglesList.Add(m_TrainStartUpManagementScreenArea[index]);
                        return true;
                    case ViewIDName.MainDrivingScreen:
                        rectanglesList.Add(m_MainDrivingScreenArea[index]);
                        return true;
                    case ViewIDName.ManualTrainStartUpManagementScreen:
                        rectanglesList.Add(m_TrainStartUpManagementScreenArea[index]);
                        return true;
                    case ViewIDName.EmergencyMessagesScreen:
                        rectanglesList.Add(m_EmergencyMessagesScreenArea[index]);
                        return true;
                    case ViewIDName.ParameterScreen:
                        rectanglesList.Add(m_ParameterScreenArea[index]);
                        return true;
                    case ViewIDName.HVACModeSelectionScreen:
                        rectanglesList.Add(m_HVACModeSelectionScreenArea[index]);
                        return true;
                    case ViewIDName.PassWordScreen:
                        rectanglesList.Add(m_PassWordScreenArea[index]);
                        return true;
                    case ViewIDName.DateAndTimeScreen:
                        rectanglesList.Add(m_DateAndTimeScreenArea[index]);
                        return true;
                    default:
                        return false;
                }
            }
        }
        public static RectangleF TransformCoord(RectangleF rectangle)
        {
            return RectsMovemont(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static void InitData()
        {
            if (!m_ClassIsInited) Init();
        }

        static RectangleF RectsMovemont(float x, float y, float width, float hight)
        {
            return new RectangleF(
                        ((x + m_OffsetCoodinatesX) * m_Scaling),
                        ((y + m_OffsetCoodinatesY) * m_Scaling),
                        (width * m_Scaling),
                        (hight * m_Scaling));

        }

        private static void Init()
        {
            int i, j, m;
            #region::::::::::标题::::::::::::
            //0
            m_TitleScreenArea.Add(RectsMovemont(0, 0, 336, 75));
            //RESCUE /1
            m_TitleScreenArea.Add(RectsMovemont(11, 10, 58, 55));
            //版本信息 模式/2-3
            for (i = 0; i < 2; i++)
            {
                m_TitleScreenArea.Add(RectsMovemont(76 + 130 * i, 0, 130, 22));
            }
            //4个图标  /4-7
            for (i = 0; i < 4; i++)
            {
                m_TitleScreenArea.Add(RectsMovemont(90 + 64 * i, 22, 50, 50));
            }
            //8
            m_TitleScreenArea.Add(RectsMovemont(336, 0, 101, 75));
            //最大速度/9
            m_TitleScreenArea.Add(RectsMovemont(320, 10, 115, 15));
            //实际速度/10
            m_TitleScreenArea.Add(RectsMovemont(335, 30, 100, 44));
            //日期  时间/11-12
            for (i = 0; i < 2; i++)
            {
                m_TitleScreenArea.Add(RectsMovemont(437 + 148 * i, 0, 148, 21));
            }
            //下一站  终点站 /13-14
            for (i = 0; i < 2; i++)
            {
                m_TitleScreenArea.Add(RectsMovemont(437, 21 + 27 * i, 296, 27));
            }
            //15-18
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    m_TitleScreenArea.Add(RectsMovemont(439 + 113 * i, 27 + 27 * j, 113, 20));
                }
            }
            //电压/19
            m_TitleScreenArea.Add(RectsMovemont(733, 0, 65, 75));
            //20
            m_TitleScreenArea.Add(RectsMovemont(75, 21, 725, 54));
            //底图/21
            m_TitleScreenArea.Add(RectsMovemont(0, 0, 800, 620));
            //4个闪烁框/22-25
            for (i = 0; i < 4; i++)
            {
                m_TitleScreenArea.Add(RectsMovemont(92 + 64 * i, 25, 47, 45));
            }

            //底部从左往右，10个按钮区域 /26-35
            for (i = 0; i < 10; i++)
            {
                m_TitleScreenArea.Add(RectsMovemont(38 + 74 * i, 562, 48, 48));
            }

            //底部从左往右，10个按钮图片  /36-45
            for (i = 0; i < 10; i++)
            {
                m_TitleScreenArea.Add(RectsMovemont(33 + 74 * i, 557, 58, 58));
            }
            ////返回按钮/31
            //_titleScreenArea.Add(RectsMovemont(6,546,60,60));
            ////返回按钮图/32
            //_titleScreenArea.Add(RectsMovemont(8,548,56,56));

            #endregion

            #region::::::::::主驾驶界面::::::
            //车头1，2       /0-1
            for (i = 0; i < 2; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(0 + 767 * i, 84, 34, 76));
            }
            //驾驶室1,2      /2-3
            for (i = 0; i < 2; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(32 + 720 * i, 83, 16, 78));
            }
            //驾驶室1,2车门  /4-7
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    m_MainDrivingScreenArea.Add(RectsMovemont(34 + 720 * i, 88 + 45 * j, 12, 24));
                }
            }
            //6节车厢       /8-13
            for (i = 0; i < 6; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(51 + 117 * i, 83, 114, 78));
            }
            //车厢门        /14-61

            for (i = 0; i < 6; i++)
            {
                for (m = 0; m < 4; m++)
                {
                    for (j = 0; j < 2; j++)
                    {
                        m_MainDrivingScreenArea.Add(RectsMovemont(56 + 27 * m + 117 * i, 88 + 42 * j, 24, 26));
                    }
                }
            }
            //load of the car /62
            m_MainDrivingScreenArea.Add(RectsMovemont(77, 175, 643, 31));
            //                /63-68
            for (i = 0; i < 6; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(100 + 109 * i, 184, 61, 17));
            }
            //MRP1 MRP2总风      /69-70
            for (i = 0; i < 2; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(2 + 734 * i, 195, 59, 59));
            }
            //辅助变流器1,2  /71-72
            for (i = 0; i < 2; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(103 + 540 * i, 213, 44, 44));
            }
            //              /73-80
            for (i = 0; i < 8; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(195 + 50 * i, 213, 44, 44));
            }
            //             /81-98
            for (i = 0; i < 3; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(109 + 28 * i, 270, 26, 57));
            }
            for (i = 0; i < 3; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(210 + 28 * i, 270, 26, 57));
            }
            for (i = 0; i < 3; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(308 + 28 * i, 270, 26, 57));
            }
            for (i = 0; i < 3; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(405 + 28 * i, 270, 26, 57));
            }
            for (i = 0; i < 3; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(504 + 28 * i, 270, 26, 57));
            }
            for (i = 0; i < 3; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(604 + 28 * i, 270, 26, 57));
            }
            //Traction/Brake Error/99
            m_MainDrivingScreenArea.Add(RectsMovemont(77, 340, 645, 60));
            //牵引/制动力            100-103
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    m_MainDrivingScreenArea.Add(RectsMovemont(260 + 350 * i, 348 + 24 * j, 90, 22));
                }
            }
            //SB Pressure       /104
            m_MainDrivingScreenArea.Add(RectsMovemont(77, 431, 645, 37));
            //常用制动压力     /105-110
            for (i = 0; i < 6; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(96 + 101 * i, 438, 95, 26));
            }
            //网关阀车轮打滑 PCE车轮打滑/111-112
            for (i = 0; i < 2; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(243 + 255 * i, 481, 50, 50));
            }
            //驾驶室1,2旁路   /113-136
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 12; j++)
                {
                    m_MainDrivingScreenArea.Add(RectsMovemont(3 + 728 * i, 285 + 22 * j, 16, 16));
                }
            }
            //旁路框         /137-138
            for (i = 0; i < 2; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(2 + 724 * i, 281, 72, 262));
            }
            //字             /139-173
            m_MainDrivingScreenArea.Add(RectsMovemont(340, 167, 130, 20));
            m_MainDrivingScreenArea.Add(RectsMovemont(340, 333, 130, 20));
            m_MainDrivingScreenArea.Add(RectsMovemont(340, 423, 130, 20));
            //字MRP1和MRP2    /142-143
            for (i = 0; i < 2; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(2 + 732 * i, 171, 65, 30));
            }
            //牵引制动的施加需求   /144-147
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    m_MainDrivingScreenArea.Add(RectsMovemont(89 + 369 * i, 351 + 23 * j, 129, 20));
                }
            }
            for (i = 0; i < 2; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(2 + 726 * i, 264, 72, 18));
            }
            //旁路中24个字符
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 12; j++)
                {
                    m_MainDrivingScreenArea.Add(RectsMovemont(15 + 728 * i, 283 + 22 * j, 60, 22));
                }
            }
            //按钮框  /174
            m_MainDrivingScreenArea.Add(RectsMovemont(0, 543, 800, 66));
            //前4个按钮/175-178
            for (i = 0; i < 4; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(205 + 74 * i, 545, 60, 60));
            }
            //后3个按钮/179-181
            for (i = 0; i < 3; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(583 + 74 * i, 545, 60, 60));
            }
            //前4个按钮图/182-185
            for (i = 0; i < 4; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(207 + 74 * i, 547, 56, 56));
            }
            //后3个按钮图/186-188
            for (i = 0; i < 3; i++)
            {
                m_MainDrivingScreenArea.Add(RectsMovemont(585 + 74 * i, 547, 56, 56));
            }
            //LoadOfTheCar/189
            m_MainDrivingScreenArea.Add(RectsMovemont(67, 172, 665, 37));
            #endregion

            #region::::::::::列车启动管理界面:::::::::::
            //0-5
            for (i = 0; i < 6; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(109 + 107 * i, 110, 95, 27));
            }
            //6-15
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(109 + 201 * i, 137, 1, 370));
            }
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(203 + 13 * i, 137, 1, 137));
            }
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(203 + 13 * i, 323, 1, 25));
            }
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(203 + 13 * i, 373, 1, 134));
            }
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(109 + 107 * i, 506, 95, 1));
            }
            //16-17
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(324 + 107 * i, 137, 95, 370));
            }
            //18-27
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(537 + 201 * i, 137, 1, 370));
            }
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(631 + 13 * i, 137, 1, 137));
            }
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(631 + 13 * i, 323, 1, 25));
            }
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(631 + 13 * i, 373, 1, 134));
            }
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(537 + 107 * i, 506, 95, 1));
            }
            //110V 144V 7.3Bar   /28-33
            m_TrainStartUpManagementScreenArea.Add(RectsMovemont(182, 276, 59, 20));
            m_TrainStartUpManagementScreenArea.Add(RectsMovemont(182, 302, 59, 20));
            m_TrainStartUpManagementScreenArea.Add(RectsMovemont(182, 352, 59, 20));
            m_TrainStartUpManagementScreenArea.Add(RectsMovemont(610, 276, 59, 20));
            m_TrainStartUpManagementScreenArea.Add(RectsMovemont(610, 302, 59, 20));
            m_TrainStartUpManagementScreenArea.Add(RectsMovemont(610, 352, 59, 20));
            //辅助变流器  空气压缩机  /34-37
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    m_TrainStartUpManagementScreenArea.Add(RectsMovemont(128 + 545 * i, 274 + 62 * j, 51, 51));
                }
            }
            //HVAC设备      /38-43
            for (i = 0; i < 6; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(129 + 109 * i, 411, 51, 51));
            }
            //启动 停止按钮图片   /44-45
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(7, 248 + 60 * i, 58, 58));
            }
            //启动 停止按钮区域  /46-47
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(11, 252 + 60 * i, 50, 50));
            }

            //辅助变流器 HVAC按钮图片   /48-49
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(7, 288 + 120 * i, 62, 62));
            }
            //辅助变流器 HVAC按钮区域  /50-51
            for (i = 0; i < 2; i++)
            {
                m_TrainStartUpManagementScreenArea.Add(RectsMovemont(11, 292 + 120 * i, 54, 54));
            }

            #endregion

            #region::::::::::紧急消息界面:::::::::::::
            //Messages   /0
            m_EmergencyMessagesScreenArea.Add(RectsMovemont(364, 83, 88, 32));
            //消息     /1-9
            for (i = 0; i < 9; i++)
            {
                m_EmergencyMessagesScreenArea.Add(RectsMovemont(94, 115 + 45 * i, 600, 45));
            }
            //SEND 按钮   /10
            m_EmergencyMessagesScreenArea.Add(RectsMovemont(10, 450, 60, 60));
            //上翻 下翻按钮 /11-12
            for (i = 0; i < 2; i++)
            {
                m_EmergencyMessagesScreenArea.Add(RectsMovemont(10, 253 + 76 * i, 58, 58));
            }
            //SEND 按钮图  /13
            m_EmergencyMessagesScreenArea.Add(RectsMovemont(15, 455, 50, 50));
            //上翻 下翻按钮图 /14-15
            for (i = 0; i < 2; i++)
            {
                m_EmergencyMessagesScreenArea.Add(RectsMovemont(12, 256 + 76 * i, 50, 50));
            }

            #endregion

            #region::::::::::手动列车启动管理界面:::::::::

            #endregion

            #region::::::::::参数界面:::::::::::::::::
            //0
            m_ParameterScreenArea.Add(RectsMovemont(420, 99, 200, 44));
            //上、下按钮区域  /1-2
            for (i = 0; i < 2; i++)
            {
                m_ParameterScreenArea.Add(RectsMovemont(430, 170 + 95 * i, 58, 58));
            }
            //outside fire mode、air furification、锁按钮  /3-5
            for (i = 0; i < 2; i++)
            {
                m_ParameterScreenArea.Add(RectsMovemont(370, 365 + 84 * i, 176, 51));
            }
            m_ParameterScreenArea.Add(RectsMovemont(20, 432, 70, 70));
            //6
            m_ParameterScreenArea.Add(RectsMovemont(422, 101, 132, 40));
            //上、下按钮图 /7-8
            for (i = 0; i < 2; i++)
            {
                m_ParameterScreenArea.Add(RectsMovemont(435, 175 + 95 * i, 48, 48));
            }
            //outside fire mode、air furification、锁按钮图 /9-11
            for (i = 0; i < 2; i++)
            {
                m_ParameterScreenArea.Add(RectsMovemont(388, 367 + 84 * i, 140, 47));
            }
            m_ParameterScreenArea.Add(RectsMovemont(24, 437, 60, 60));
            //温度        /12
            m_ParameterScreenArea.Add(RectsMovemont(415, 228, 82, 40));
            //  13-15
            for (i = 0; i < 2; i++)
            {
                m_ParameterScreenArea.Add(RectsMovemont(240, 105 + 131 * i, 160, 31));
            }
            m_ParameterScreenArea.Add(RectsMovemont(5, 505, 100, 36));
            #endregion

            #region::::::::::空调模式选择::::::::::::
            //0-6
            for (i = 0; i < 7; i++)
            {
                m_HVACModeSelectionScreenArea.Add(RectsMovemont(10, 143 + 54 * i, 620, 54));
            }
            //HVAC mode selection   /7
            m_HVACModeSelectionScreenArea.Add(RectsMovemont(207, 80, 282, 48));
            //上、下、确定按钮   /8-10
            for (i = 0; i < 2; i++)
            {
                m_HVACModeSelectionScreenArea.Add(RectsMovemont(728, 223 + 69 * i, 60, 60));
            }
            m_HVACModeSelectionScreenArea.Add(RectsMovemont(648, 463, 146, 53));
            //上、下、确定按钮图   /11-13
            for (i = 0; i < 2; i++)
            {
                m_HVACModeSelectionScreenArea.Add(RectsMovemont(733, 228 + 69 * i, 50, 50));
            }
            m_HVACModeSelectionScreenArea.Add(RectsMovemont(650, 465, 142, 49));
            #endregion

            #region::::::::::密码界面:::::::::::::
            //enter password    /0
            m_PassWordScreenArea.Add(RectsMovemont(224, 186, 150, 26));
            //密码框位置      /1
            m_PassWordScreenArea.Add(RectsMovemont(450, 181, 132, 36));
            //六位密码  /2-7
            for (i = 0; i < 6; i++)
            {
                m_PassWordScreenArea.Add(RectsMovemont(453 + 21 * i, 184, 21, 30));
            }
            //密码键盘  /8-45
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    m_PassWordScreenArea.Add(RectsMovemont(186 + 53 * j, 240 + 53 * i, 50, 50));
                }
            }
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    m_PassWordScreenArea.Add(RectsMovemont(186 + 53 * j, 346 + 53 * i, 50, 50));
                }
                m_PassWordScreenArea.Add(RectsMovemont(610, 346 + 53 * i, 103, 50));
            }
            //密码键盘图  /46-83
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    m_PassWordScreenArea.Add(RectsMovemont(188 + 53 * j, 242 + 53 * i, 46, 46));
                }
            }
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    m_PassWordScreenArea.Add(RectsMovemont(188 + 53 * j, 348 + 53 * i, 46, 46));
                }
                m_PassWordScreenArea.Add(RectsMovemont(612, 348 + 53 * i, 99, 46));
            }
            #endregion

            #region::::::::::时间和日期::::::::::::
            //date and time /0
            m_DateAndTimeScreenArea.Add(RectsMovemont(76, 157, 162, 46));
            //1
            m_DateAndTimeScreenArea.Add(RectsMovemont(489, 159, 242, 33));
            //12位数字   /2-13
            for (i = 0; i < 4; i++)
            {
                m_DateAndTimeScreenArea.Add(RectsMovemont(494 + 15 * i, 163, 13, 26));
            }
            for (i = 0; i < 2; i++)
            {
                m_DateAndTimeScreenArea.Add(RectsMovemont(567 + 13 * i, 163, 13, 26));
            }
            for (i = 0; i < 2; i++)
            {
                m_DateAndTimeScreenArea.Add(RectsMovemont(612 + 13 * i, 163, 13, 26));
            }
            for (i = 0; i < 2; i++)
            {
                m_DateAndTimeScreenArea.Add(RectsMovemont(646 + 15 * i, 163, 13, 26));
            }
            for (i = 0; i < 2; i++)
            {
                m_DateAndTimeScreenArea.Add(RectsMovemont(689 + 15 * i, 163, 13, 26));
            }
            // - - :   /14-16
            m_DateAndTimeScreenArea.Add(RectsMovemont(552, 163, 13, 26));
            m_DateAndTimeScreenArea.Add(RectsMovemont(597, 163, 13, 26));
            m_DateAndTimeScreenArea.Add(RectsMovemont(674, 163, 13, 26));
            //确认键  /17
            m_DateAndTimeScreenArea.Add(RectsMovemont(732, 155, 39, 39));
            //确认键图 /18
            m_DateAndTimeScreenArea.Add(RectsMovemont(734, 157, 35, 35));
            //密码盘  //19-54
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    m_DateAndTimeScreenArea.Add(RectsMovemont(193 + 52 * j, 243 + 52 * i, 50, 50));
                }
            }
            //密码盘图  /55-90
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    m_DateAndTimeScreenArea.Add(RectsMovemont(195 + 52 * j, 245 + 52 * i, 46, 46));
                }
            }
            #endregion

            #region:::::::::故障信息::::::::::::
            //停车：不正确操作状态清单     0
            m_FaultInformationScreenArea.Add(RectsMovemont(50, 80, 162, 30));
            //停车：页码                   1
            m_FaultInformationScreenArea.Add(RectsMovemont(360, 80, 50, 30));
            //停车：类型                   2
            m_FaultInformationScreenArea.Add(RectsMovemont(640, 80, 50, 30));
            //停车：文字：说明             3
            m_FaultInformationScreenArea.Add(RectsMovemont(52, 445, 50, 30));
            //停车：文字：请检查断路器，或者需要进行维护操作  4
            m_FaultInformationScreenArea.Add(RectsMovemont(52, 495, 400, 30));
            //停车：说明框                 5
            m_FaultInformationScreenArea.Add(RectsMovemont(50, 440, 670, 105));
            //停车:两个对勾框图片              6-7
            for (i = 0; i < 2; i++)
            {
                m_FaultInformationScreenArea.Add(RectsMovemont(730, 415 + 68 * i, 60, 60));
            }
            //停车：左侧9个框             8-16
            for (i = 0; i < 9; i++)
            {
                m_FaultInformationScreenArea.Add(RectsMovemont(50, 105 + 35 * i, 610, 35));
            }
            //停车：右侧9个框            17-25
            for (i = 0; i < 9; i++)
            {
                m_FaultInformationScreenArea.Add(RectsMovemont(660, 105 + 35 * i, 60, 35));
            }
            //停车：左侧9个小图片        26-34
            for (i = 0; i < 9; i++)
            {
                m_FaultInformationScreenArea.Add(RectsMovemont(20,110+35*i,26,24));
            }
            //紧急制动图标               35
            m_FaultInformationScreenArea.Add(RectsMovemont(140,150,55,55));
            //确认图标                   36
            m_FaultInformationScreenArea.Add(RectsMovemont(20,250,58,58));
            //文字：施加紧急制动         37
            m_FaultInformationScreenArea.Add(RectsMovemont(200,160,180,35));
            //文字：紧急制动已施加       38
            m_FaultInformationScreenArea.Add(RectsMovemont(110, 250, 600, 25));
            //页面上、下翻图片           39-40
            for (i = 0; i < 2; i++)
            {
                m_FaultInformationScreenArea.Add(RectsMovemont(730,225+70*i,50,50));
            }
            //页面上、下翻图片           41-42
            for (i = 0; i < 2;i++ )
            {
                m_FaultInformationScreenArea.Add(RectsMovemont(725, 220 + 70 * i, 60, 60));
            }

            #endregion
                m_ClassIsInited = true;
        }
        #region::::::::::::::::::RectangleF::::::::::::::
        //TitleScreen 标题界面坐标
        static readonly List<RectangleF> m_TitleScreenArea = new List<RectangleF>();
        //MainDrivingScreen主驾驶界面坐标
        static readonly List<RectangleF> m_MainDrivingScreenArea = new List<RectangleF>();
        //TrainStartUpManagementScreen列车启动管理界面
        static readonly List<RectangleF> m_TrainStartUpManagementScreenArea = new List<RectangleF>();
        //ManualTrainStartUpManagementScreen手动列车启动管理界面
        static List<RectangleF> m_ManualTrainStartUpManagementScreenArea = new List<RectangleF>();
        //EmergencyMessagesScreen紧急消息界面坐标
        static readonly List<RectangleF> m_EmergencyMessagesScreenArea = new List<RectangleF>();
        //ParameterScreen参数界面坐标
        static readonly List<RectangleF> m_ParameterScreenArea = new List<RectangleF>();
        //HVACModeSelectionScreen空调模式选择坐标
        static readonly List<RectangleF> m_HVACModeSelectionScreenArea = new List<RectangleF>();
        //PassWordScreen密码界面坐标
        static readonly List<RectangleF> m_PassWordScreenArea = new List<RectangleF>();
        //DateAndTimeScreen日期和时间
        static readonly List<RectangleF> m_DateAndTimeScreenArea = new List<RectangleF>();
        //FaultInformationScreen故障信息页面
        static readonly List<RectangleF> m_FaultInformationScreenArea = new List<RectangleF>();
        #endregion

        private static bool m_ClassIsInited = false;

        /// <summary>
        /// 初始化完成
        /// </summary>
        public static bool ClassIsInited
        {
            get { return m_ClassIsInited; }
        }
        #region 屏大小偏移变化相关
        //偏移坐标X
        static readonly int m_OffsetCoodinatesX = 0;
        /// <summary>
        /// 偏移坐标X
        /// </summary>
        public static int OffsetCoodinatesX { get { return m_OffsetCoodinatesX; } }

        //偏移坐标Y
        static readonly int m_OffsetCoodinatesY = 0;
        /// <summary>
        /// 偏移坐标Y
        /// </summary>
        public static int OffsetCoodinatesY { get { return m_OffsetCoodinatesY; } }

        //缩放比例
        static readonly float m_Scaling = 1.0f;
        /// <summary>
        /// 整体缩放比例
        /// </summary>
        public static float Scaling { get { return m_Scaling; } }
        #endregion
    }
}
