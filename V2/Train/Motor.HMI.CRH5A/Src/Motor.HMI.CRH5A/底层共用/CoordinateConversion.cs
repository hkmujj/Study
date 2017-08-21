using System.Collections.Generic;
using System.Drawing;

namespace Motor.HMI.CRH5A.底层共用
{
    public static class Coordinate
    {
        public static List<RectangleF> RectangleFLists(ViewIDName className)
        {
            int i, j;
            switch (className)
            {
                #region ::::::::::::::: 黑屏视图 :::::::::::::::

                case ViewIDName.BlackScreen:
                    return m_TheRectFListlackScreen;

                #endregion

                #region ::::::::::::::: 课程结束 :::::::::::::::

                case ViewIDName.ClassOverScreen:
                    return m_TheRectFListClassOverScreen;

                #endregion

                #region ::::::::::::::: 故障接收 :::::::::::::::

                case ViewIDName.FaultReceive:
                    return m_TheRectFListFaultReceive;

                #endregion

                #region ::::::::::::::: 标题 :::::::::::::::

                case ViewIDName.TitleScreen:
                    if (m_TheRectFListTitleScreen.Count!=0)
                    {
                        return m_TheRectFListTitleScreen;
                    }
                    //0-3 /4个框
                    m_TheRectFListTitleScreen.Add(RectsMovemont(1, 1, 400, 50));
                    m_TheRectFListTitleScreen.Add(RectsMovemont(401, 1, 130, 50));
                    m_TheRectFListTitleScreen.Add(RectsMovemont(531, 1, 130, 50));
                    m_TheRectFListTitleScreen.Add(RectsMovemont(661, 1, 139, 50));
                    //4-5 /设定速度
                    m_TheRectFListTitleScreen.Add(RectsMovemont(400, 2, 130, 20));
                    m_TheRectFListTitleScreen.Add(RectsMovemont(400, 25, 130, 30));
                    //6-15 /下方按钮栏
                    for (i = 0; i < 10; i++)
                    {
                        m_TheRectFListTitleScreen.Add(RectsMovemont(i * 80, 555, 80, 35));
                    }

                    //16 /类似1/16
                    m_TheRectFListTitleScreen.Add(RectsMovemont(20, 85, 50, 25));
                    //17 /车
                    m_TheRectFListTitleScreen.Add(RectsMovemont(22, 71, 763, 67));
                    //18-33 //三角
                    for (i = 0; i < 16; i++)
                    {
                        m_TheRectFListTitleScreen.Add(RectsMovemont(131 + i * 41, 57, 13, 10));
                    }
                    //34-35    /8车*
                    for (i = 0; i < 2; i++)
                    {
                        m_TheRectFListTitleScreen.Add(RectsMovemont(160 + i * 560, 80, 11, 12));
                    }
                    //36-37     /16车*
                    for (i = 0; i < 2; i++)
                    {
                        m_TheRectFListTitleScreen.Add(RectsMovemont(140 + i * 600, 80, 11, 12));
                    }
                    //38-41 /->
                    m_TheRectFListTitleScreen.Add(RectsMovemont(95 + 41 * 4, 90, 13, 8));
                    m_TheRectFListTitleScreen.Add(RectsMovemont(95 + 41 * 12, 90, 13, 8));
                    m_TheRectFListTitleScreen.Add(RectsMovemont(95 + 82 * 4, 75, 19, 13));
                    m_TheRectFListTitleScreen.Add(RectsMovemont(95 + 82 * 4, 75, 19, 13));
                    //42    />>
                    m_TheRectFListTitleScreen.Add(RectsMovemont(752, 92, 20, 15));
                    //43    /数字
                    m_TheRectFListTitleScreen.Add(RectsMovemont(770, 92, 20, 15));
                    return m_TheRectFListTitleScreen;

                #endregion

                #region ::::::::::::::: 状态 :::::::::::::::

                case ViewIDName.MenuScreen:
                    if (m_TheRectFListMenuScreen.Count != 0) return m_TheRectFListMenuScreen;
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListMenuScreen.Add(RectsMovemont(10, 155 + 36 * i, 94, 35));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-98
                            m_TheRectFListMenuScreen.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListMenuScreen.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListMenuScreen;
                /*
             11, 12, 13, 14, 15, 16, 17, 18,
             19, 20, 21, 22, 23, 24, 25, 26,
             27, 28, 29, 30, 31, 32, 33, 34,
             35, 36, 37, 38, 39, 40, 41, 42,
             43, 44, 45, 46, 47, 48, 49, 50,
             51, 52, 53, 54, 55, 56, 57, 58,
             59, 60, 61, 62, 63, 64, 65, 66,
             67, 68, 69, 70, 71, 72, 73, 74,
             75, 76, 77, 78, 79, 80, 81, 82,
             83, 84, 85, 86, 87, 88, 89, 90,
             91, 92, 93, 94, 95, 96, 97, 98
             */

                /*
             99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114,
            115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130,
            131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146,
            147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162,
            163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178,
            179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194,
            195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210,
            211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226,
            227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242,
            243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258,
            259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274
             */

                #endregion

                #region ::::::::::::::: 仪表试图1 :::::::::::::::

                case ViewIDName.InstrumentScreen1:
                    m_TheRectFListInstrumentScreen1.Add(RectsMovemont(15, 135, 430, 430));
                    m_TheRectFListInstrumentScreen1.Add(RectsMovemont(30, 150, 400, 400));
                    for (i = 0; i < 2; i++)
                    {
                        m_TheRectFListInstrumentScreen1.Add(RectsMovemont(470, 58 + i * 249, 244, 244));
                    }


                    return m_TheRectFListInstrumentScreen1;

                #endregion

                #region ::::::::::::::: 仪表试图2 :::::::::::::::

                case ViewIDName.InstrumentScreen2:
                    m_TheRectFListInstrumentScreen2.Add(RectsMovemont(155, 105, 430, 430));
                    m_TheRectFListInstrumentScreen2.Add(RectsMovemont(170, 100, 420, 420));
                    for (i = 0; i < 2; i++)
                    {
                        m_TheRectFListInstrumentScreen2.Add(RectsMovemont(470, 58 + i * 249, 244, 244));
                    }


                    return m_TheRectFListInstrumentScreen2;

                #endregion

                #region :::::::: 按钮视图 ::::::::::::::::

                case ViewIDName.ButtonsScreen:
                    //上
                    int btnX = -124;
                    int btnY = -131;
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(0 + btnX, 0 + btnY, 1043, 874));
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(148 + btnX, 60 + btnY, 357, 37)); //i
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(533 + btnX, 60 + btnY, 57, 40)); //F
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(611 + btnX, 60 + btnY, 57, 40)); //D
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(690 + btnX, 60 + btnY, 57, 40)); //I
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(770 + btnX, 60 + btnY, 57, 40)); //
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(849 + btnX, 60 + btnY, 57, 40)); //

                    //左
                    for (i = 0; i < 2; i++)
                    {
                        m_TheRectFListuttonsScreen.Add(RectsMovemont(50 + btnX, 149 + i * 203 + btnY, 44, 181));
                    }
                    for (i = 0; i < 2; i++)
                    {
                        m_TheRectFListuttonsScreen.Add(RectsMovemont(50 + btnX, 554 + i * 77 + btnY, 44, 56));
                    }

                    //右
                    for (i = 0; i < 3; i++)
                    {
                        m_TheRectFListuttonsScreen.Add(RectsMovemont(950 + btnX, 223 + i * 78 + btnY, 44, 56));
                    }
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(950 + btnX, 455 + btnY, 44, 181));
                    m_TheRectFListuttonsScreen.Add(RectsMovemont(950 + btnX, 658 + btnY, 44, 56));


                    //下
                    for (i = 0; i < 10; i++)
                    {
                        m_TheRectFListuttonsScreen.Add(RectsMovemont(140 + i * 78 + btnX, 760 + btnY, 60, 45));
                    }

                    return m_TheRectFListuttonsScreen;

                #endregion

                #region :::::::::::::电子仪器图1::::::::::::::::

                case ViewIDName.TestOneScreen:
                    if (m_TheRectFListTestOneScreen.Count != 0) return m_TheRectFListTestOneScreen;
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListTestOneScreen.Add(RectsMovemont(10, 155 + 36 * i, 94, 35));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListTestOneScreen.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListTestOneScreen.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListTestOneScreen;

                #endregion

                #region  :::::::::::::电子仪器图2::::::::::::::::

                case ViewIDName.TestTwoScreen:
                    if (m_TheRectFListTestTwoScreen.Count != 0) return m_TheRectFListTestTwoScreen;
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListTestTwoScreen.Add(RectsMovemont(10, 155 + 36 * i, 94, 35));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListTestTwoScreen.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListTestTwoScreen.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListTestTwoScreen;

                #endregion

                #region  :::::::::::::电子仪器图3::::::::::::::::

                case ViewIDName.TestThreeScreen:
                    if (m_TheRectFListTestThreeScreen.Count != 0) return m_TheRectFListTestThreeScreen;
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListTestThreeScreen.Add(RectsMovemont(10, 155 + 36 * i, 94, 35));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListTestThreeScreen.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListTestThreeScreen.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListTestThreeScreen;

                #endregion

                #region  :::::::::::::主屏图::::::::::::::::

                case ViewIDName.HomeScreen:
                    for (i = 0; i < 8; i++)
                    {
                        //0-7
                        m_TheRectFListHomeScreen.Add(RectsMovemont(50, 80 + i * 45, 140, 30));
                    }
                    return m_TheRectFListHomeScreen;

                #endregion

                #region :::::::::::::制动试验图1::::::::::::::::

                case ViewIDName.BrakeOneTest:
                    if (m_TheRectFListrakeOneTest.Count != 0) return m_TheRectFListrakeOneTest;
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListrakeOneTest.Add(RectsMovemont(10, 155 + 36 * i, 150, 35));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListrakeOneTest.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListrakeOneTest.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListrakeOneTest;

                #endregion

                #region :::::::::::::制动试验图2::::::::::::::::

                case ViewIDName.BrakeTwoTest:
                    if (m_TheRectFListrakeTwoTest.Count != 0) return m_TheRectFListrakeTwoTest;
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListrakeTwoTest.Add(RectsMovemont(10, 155 + 36 * i, 200, 35));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListrakeTwoTest.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListrakeTwoTest.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListrakeTwoTest;

                #endregion

                #region :::::::::::::制动试验图3::::::::::::::::

                case ViewIDName.BrakeThreeTest:
                    if (m_TheRectFListrakeThreeTest.Count != 0) return m_TheRectFListrakeThreeTest;
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListrakeThreeTest.Add(RectsMovemont(10, 155 + 36 * i, 200, 35));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListrakeThreeTest.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListrakeThreeTest.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListrakeThreeTest;

                #endregion

                #region :::::::::::::系统设置图1::::::::::::::::

                case ViewIDName.SystemOneSettings:
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListSystemOneSettings.Add(RectsMovemont(125, 200 + 99 * i, 550, 300));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListSystemOneSettings.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListSystemOneSettings.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListSystemOneSettings;

                #endregion

                #region :::::::::::::系统设置图2::::::::::::::::

                case ViewIDName.SystemTwoSettings:
                    for (i = 0; i < 5; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListSystemTwoSettings.Add(RectsMovemont(100, 200 + 50 * i, 150, 49));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListSystemTwoSettings.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListSystemTwoSettings.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListSystemTwoSettings;

                #endregion

                #region :::::::::::::下载数据故障图::::::::::::::::

                case ViewIDName.DownloadDataFault:
                    for (i = 0; i < 3; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListDownloadDataFault.Add(RectsMovemont(100, 200 + 70 * i, 200, 59));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListDownloadDataFault.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListDownloadDataFault.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListDownloadDataFault;

                #endregion

                #region  :::::::::::::指令图::::::::::::::::

                case ViewIDName.Command:
                    if (m_TheRectFListCommand.Count != 0) return m_TheRectFListCommand;
                    for (i = 0; i < 11; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListCommand.Add(RectsMovemont(10, 155 + 36 * i, 94, 35));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-98
                            m_TheRectFListCommand.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListCommand.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListCommand;

                #endregion

                #region  :::::::::::::维护::::::::::::::::

                case ViewIDName.MaintainScreen:
                    for (i = 0; i < 7; i++)
                    {
                        //0-10  /标题
                        m_TheRectFListMaintainScreen.Add(RectsMovemont(100, 180 + 50 * i, 600, 40));
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            //11-124
                            m_TheRectFListMaintainScreen.Add(RectsMovemont(124 + j * 82, 155 + i * 36, 74, 30));
                        }
                    }
                    for (i = 0; i < 11; i++)
                    {
                        for (j = 0; j < 16; j++)
                        {
                            //99-274
                            m_TheRectFListMaintainScreen.Add(RectsMovemont(124 + j * 41, 155 + i * 36, 37, 30));
                        }
                    }
                    return m_TheRectFListMaintainScreen;

                #endregion

                #region:::::::::故障确认:::::::::::::::::::

                case ViewIDName.FaultEnsure:
                    //底图 /0
                    m_TheRectFListFaultEnture.Add(RectsMovemont(0, 0, 800, 600));
                    //车辆号/1
                    m_TheRectFListFaultEnture.Add(RectsMovemont(90, 85, 30, 15));
                    //故障总数 /2
                    m_TheRectFListFaultEnture.Add(RectsMovemont(30, 107, 30, 15));
                    //当前故障数/3
                    m_TheRectFListFaultEnture.Add(RectsMovemont(143, 107, 30, 15));
                    //设备/4
                    m_TheRectFListFaultEnture.Add(RectsMovemont(35, 180, 450, 80));
                    //描述/5
                    m_TheRectFListFaultEnture.Add(RectsMovemont(35, 295, 450, 80));
                    //操作者指南/6
                    m_TheRectFListFaultEnture.Add(RectsMovemont(35, 404, 670, 130));

                    return m_TheRectFListFaultEnture;

                #endregion

                #region:::::::::::::密码:::::::::::::::::::::::

                case ViewIDName.PassWord:
                    //边框   /0
                    m_TheRectFListPassWord.Add(RectsMovemont(200, 200, 400, 300));
                    //提示框  /1
                    m_TheRectFListPassWord.Add(RectsMovemont(300, 250, 200, 50));
                    //5个密码框  /2-6
                    for (i = 0; i < 5; i++)
                    {
                        m_TheRectFListPassWord.Add(RectsMovemont(320 + 35 * i, 320, 30, 30));
                    }


                    return m_TheRectFListPassWord;

                #endregion

                #region:::::::::::::故障历史:::::::::::::::::::::::

                case ViewIDName.FaultHistory:
                    //底图   /0
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(0, 0, 800, 600));
                    //排序方式 /1
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(625, 60, 155, 25));
                    //第几页/共几页 /2,3
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(645, 279, 32, 20));
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(729, 279, 32, 20));
                    //车辆/设备名/故障时间 /4,5,6
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(25, 330, 100, 30));
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(215, 330, 170, 30));
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(445, 330, 200, 30));
                    //故障描述 /7
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(25, 380, 700, 50));
                    //操作者指南 /8
                    m_TheRectFListFaultHistroy.Add(RectsMovemont(25, 450, 700, 100));
                    //
                    for (i = 0; i < 10; i++)
                    {
                        //游标 /9,15,21,27,33,39,45,51,57,63
                        m_TheRectFListFaultHistroy.Add(RectsMovemont(10, 85 + i * 18, 40, 18));
                        //编号 /10,16,22,28,34,40,46,52,58,64
                        m_TheRectFListFaultHistroy.Add(RectsMovemont(50, 85 + i * 18, 40, 18));
                        //日期时间 /11,17,23,29,35,41,47,53,59,65
                        m_TheRectFListFaultHistroy.Add(RectsMovemont(90, 85 + i * 18, 200, 18));
                        //车辆 /12,18,24,30,36,42,48,54,60,66
                        m_TheRectFListFaultHistroy.Add(RectsMovemont(290, 85 + i * 18, 40, 18));
                        //设备 /13,19,25,31,37,43,49,55,61,67
                        m_TheRectFListFaultHistroy.Add(RectsMovemont(330, 85 + i * 18, 160, 18));
                        //故障名 /14,20,26,32,38,44,50,56,62,68
                        m_TheRectFListFaultHistroy.Add(RectsMovemont(490, 85 + i * 18, 300, 18));
                    }
                    return m_TheRectFListFaultHistroy;

                #endregion


            }
            return m_TheRectangleFList;
        }

        public static RectangleF TransformCoord(RectangleF rectangle)
        {
            return RectsMovemont(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        private static RectangleF RectsMovemont(float x, float y, float width, float hight)
        {
            return new RectangleF(
                ((x + m_OffsetCoodinatesX) * m_Scaling),
                ((y + m_OffsetCoodinatesY) * m_Scaling),
                (width * m_Scaling),
                (hight * m_Scaling));
        }

        private static bool m_ClassIsInited = false;

        /// <summary>
        /// 初始化完成
        /// </summary>
        public static bool ClassIsInited
        {
            get { return m_ClassIsInited; }
        }

        private static List<RectangleF> m_TheRectangleFList = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListlackScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListClassOverScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListFaultReceive = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListTitleScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListMenuScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListInstrumentScreen1 = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListInstrumentScreen2 = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListuttonsScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListTestOneScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListTestTwoScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListTestThreeScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListHomeScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListrakeOneTest = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListrakeTwoTest = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListrakeThreeTest = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListSystemOneSettings = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListSystemTwoSettings = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListDownloadDataFault = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListCommand = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListMaintainScreen = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListFaultEnture = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListPassWord = new List<RectangleF>();
        private static List<RectangleF> m_TheRectFListFaultHistroy = new List<RectangleF>();

        #region 屏大小偏移变化相关

        //偏移坐标X
        //static int _offsetCoodinates_X = 124;
        private static int m_OffsetCoodinatesX = 0;

        /// <summary>
        /// 偏移坐标X
        /// </summary>
        public static int OffsetCoodinatesX
        {
            get { return m_OffsetCoodinatesX; }
        }

        //偏移坐标Y
        //static int _offsetCoodinates_Y = 131;
        private static int m_OffsetCoodinatesY = 0;

        /// <summary>
        /// 偏移坐标Y
        /// </summary>
        public static int OffsetCoodinatesY
        {
            get { return m_OffsetCoodinatesY; }
        }

        //缩放比例
        private static float m_Scaling = 1.0f;

        /// <summary>
        /// 整体缩放比例
        /// </summary>
        public static float Scaling
        {
            get { return m_Scaling; }
        }

        #endregion

    }
}
