using System.Collections.Generic;
using System.Drawing;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.底层共用
{
    public static class Coordinate
    {
        public static bool RectangleFLists(ViewIDName className, ref List<RectangleF> rectanglesList)
        {
            if (!m_ClassIsInited)
            {
                return false;
            }

            switch (className)
            {
                case ViewIDName.DMIBlackScreen:
                    rectanglesList = DMIBlackScreenAreaList;
                    return true;
                case ViewIDName.DMIButton:
                    rectanglesList = DMIButtonAreaList;
                    return true;
                case ViewIDName.DMITitle:
                    rectanglesList = DMITitleAreaList;
                    return true;
                case ViewIDName.DMIFault:
                    rectanglesList = DMIFaultAreaList;
                    return true;
                case ViewIDName.DMIInfoBox:
                    rectanglesList = DMIInfoBoxAreaList;
                    return true;
                case ViewIDName.DMIBasePage:
                    rectanglesList = DMIBasePageAreaList;
                    return true;
                case ViewIDName.DMIMaintenance:
                    rectanglesList = DMIMaintenanceAreaList;
                    return true;
                case ViewIDName.DMIRemoteDataTransfer:
                    rectanglesList = DMIRemoteDataTransferAreaList;
                    return true;
                case ViewIDName.DMITractionDcLink:
                    rectanglesList = DMITractionDcLinkAreaList;
                    return true;
                case ViewIDName.DMIVersionsCar:
                    rectanglesList = DMIVersionsCarAreaList;
                    return true;
                case ViewIDName.DmivSetpointSensorValue:
                    rectanglesList = DmivSetpointSensorValueAreaList;
                    return true;
                case ViewIDName.DMITractiveEffortControllerSensorSetpoint:
                    rectanglesList = DmivSetpointSensorValueAreaList;
                    return true;
                case ViewIDName.DMIEnergyCounter:
                    rectanglesList = DMIEnergyCounterAreaList;
                    return true;
                case ViewIDName.DMISystem:
                    rectanglesList = DMISystemAreaList;
                    return true;
                case ViewIDName.DMITrainConfigurationDisplay:
                    rectanglesList = DMITrainConfigurationDisplayAreaList;
                    return true;
                case ViewIDName.DMIBatteryVoltage110V:
                    rectanglesList = DMIBatteryVoltage110VAreaList;
                    return true;
                case ViewIDName.DMIDoors:
                    rectanglesList = DMIDoorsAreaList;
                    return true;
                case ViewIDName.DMIATPSystem:
                    rectanglesList = DmiatpSystemAreaList;
                    return true;
                case ViewIDName.DMIDMIReplace:
                    rectanglesList = DmidmiReplaceAreaList;
                    return true;
                case ViewIDName.DMIEVCReplace:
                    rectanglesList = DmievcReplaceAreaList;
                    return true;
                case ViewIDName.DMIEmergency:
                    rectanglesList = DMIEmergencyAreaList;
                    return true;
                case ViewIDName.DMIPreparingStabling:
                    rectanglesList = DMIPreparingStablingAreaList;
                    return true;
                case ViewIDName.DMITrainNoAndTDNo:
                    rectanglesList = DMITrainNoAndTdNoAreaList;
                    return true;
                case ViewIDName.DMIStartStablingOperation:
                    rectanglesList = DMIStartStablingOperationAreaList;
                    return true;
                case ViewIDName.DMIDrivingBraking:
                    rectanglesList = DMIDrivingBrakingAreaList;
                    return true;
                case ViewIDName.DMIASC:
                    rectanglesList = DmiascAreaList;
                    return true;
                case ViewIDName.DMISwitching:
                    rectanglesList = DMISwitchingAreaList;
                    return true;
                case ViewIDName.DMILineCurrentLimit:
                    rectanglesList = DMILineCurrentLimitAreaList;
                    return true;
                case ViewIDName.DMIFans:
                    rectanglesList = DMIFansAreaList;
                    return true;
                case ViewIDName.DMITraction:
                    rectanglesList = DMITractionAreaList;
                    return true;
                case ViewIDName.DMILighting:
                    rectanglesList = DMILightingAreaList;
                    return true;
                case ViewIDName.DMIWashRun:
                    rectanglesList = DMIWashRunAreaList;
                    return true;
                case ViewIDName.DMIFrontWindowHeating:
                    rectanglesList = DMIFrontWindowHeatingAreaList;
                    return true;
                case ViewIDName.DMIBrakeStatus:
                    rectanglesList = DMIBrakeStatusAreaList;
                    return true;
                case ViewIDName.DMIBrakingPower:
                    rectanglesList = DMIBrakingPowerAreaList;
                    return true;
                case ViewIDName.DMIVmaxTable:
                    rectanglesList = DMIVmaxTableAreaList;
                    return true;
                case ViewIDName.DMIStatusOfBrakeFunctions:
                    rectanglesList = DMIStatusOfBrakeFunctionsAreaList;
                    return true;
                case ViewIDName.DMIBrakeTest:
                    rectanglesList = DMIBrakeTestAreaList;
                    return true;
                case ViewIDName.DMIParkingBrakes:
                    rectanglesList = DMIParkingBrakesAreaList;
                    return true;
                case ViewIDName.DMIResultOfLastBrakeTest:
                    rectanglesList = DMIResultOfLastBrakeTestAreaList;
                    return true;
                case ViewIDName.DMICloseCouplerHatches:
                    rectanglesList = DMICloseCouplerHatchesAreaList;
                    return true;
                default:
                    return false;
            }
        }

        public static RectangleF TransformCoord(RectangleF rectangle)
        {
            return RectsMovemont(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static void InitData()
        {
            if (!m_ClassIsInited)
            {
                Init();
            }
        }

        private static RectangleF RectsMovemont(float x, float y, float width, float hight)
        {
            return new RectangleF(
                ((x + _offsetCoodinates_X)*m_Scaling),
                ((y + _offsetCoodinates_Y)*m_Scaling),
                (width*m_Scaling),
                (hight*m_Scaling));
        }

        private static void Init()
        {
            #region ::::::::::::::::: 公共界面相关坐标 :::::::::::::::::::::::::

            /////////////////////////////////////////////////////////////////////////////
            ////////黑屏视图
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIBlackScreen :::::::::::::::::::

            DMIBlackScreenAreaList.Add(RectsMovemont(0, 0, 800, 600));

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////课程结束视图
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIClassOverScreen :::::::::::::::::::

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////按钮界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIButton ::::::::::::

            //0     /上侧底图
            DMIButtonAreaList.Add(RectsMovemont(0, 0 - 67, 800, 67));
            //1     /下侧底图
            DMIButtonAreaList.Add(RectsMovemont(0, 667 - 67, 800, 67));
            //2     /右侧底图
            DMIButtonAreaList.Add(RectsMovemont(800, 0 - 67, 86, 600));
            //3-8 /右层6个按钮
            for (int i = 0; i < 6; i++)
            {
                DMIButtonAreaList.Add(RectsMovemont(812, i*72, 57, 57));
            }
            //9-18  /下层10个按钮
            for (int i = 0; i < 10; i++)
            {
                DMIButtonAreaList.Add(RectsMovemont(25 + i*76, 672 - 67, 57, 57));
            }
            //19-27   /上层9个按钮
            for (int i = 0; i < 5; i++)
            {
                DMIButtonAreaList.Add(RectsMovemont(25 + i*76, 5 - 67, 57, 57));
            }

            DMIButtonAreaList.Add(RectsMovemont(405, 5 - 67, 132, 57));
            for (int i = 0; i < 3; i++)
            {
                DMIButtonAreaList.Add(RectsMovemont(557 + i*76, 5 - 67, 57, 57));
            }
            //底色变换  /28
            DMIButtonAreaList.Add(RectsMovemont(0, 0, 800, 600));

            //29-38
            for (int i = 0; i < 10; i++)
            {
                DMIButtonAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////标题界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMITitle ::::::::::::

            //头标题     /0
            DMITitleAreaList.Add(RectsMovemont(25, 0, 750, 25));
            //中下标题     /1
            DMITitleAreaList.Add(RectsMovemont(25, 505, 750, 38));
            //下标题     /2
            DMITitleAreaList.Add(RectsMovemont(25, 550, 750, 50));
            //头标题第1格  /3
            DMITitleAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /4
            DMITitleAreaList.Add(RectsMovemont(175, 0, 450, 25));
            //头标题第3、4格 /5,6
            for (int i = 0; i < 2; i++)
            {
                DMITitleAreaList.Add(RectsMovemont(625 + i*75, 0, 75, 25));
            }
            //中标题    /7-8
            for (int i = 0; i < 2; i++)
            {
                DMITitleAreaList.Add(RectsMovemont(27 + i*36, 507, 36, 36));
            }
            //中标题    /9-20
            for (int i = 0; i < 12; i++)
            {
                DMITitleAreaList.Add(RectsMovemont(101 + i*38, 507, 36, 36));
            }
            //中标题    /21
            DMITitleAreaList.Add(RectsMovemont(557, 507, 216, 36));
            //下标题    /22-31
            for (int i = 0; i < 10; i++)
            {
                DMITitleAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////信息盒
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIInfoBox :::::::::::::::::::

            //背景图    /0
            DMIInfoBoxAreaList.Add(RectsMovemont(0, 0, 800, 600));
            //当前行号  /1
            DMIInfoBoxAreaList.Add(RectsMovemont(25, 30, 750, 20));
            //18行信息  /2-19
            for (int i = 0; i < 18; i++)
            {
                DMIInfoBoxAreaList.Add(RectsMovemont(25, 55 + i*24, 750, 24));
            }
            //两侧18个三角位置  /20-55
            for (int i = 0; i < 18; i++)
            {
                DMIInfoBoxAreaList.Add(RectsMovemont(0, 55 + i*24, 23, 24));
                DMIInfoBoxAreaList.Add(RectsMovemont(777, 55 + i*24, 23, 24));
            }
            //标题      /56
            DMIInfoBoxAreaList.Add(RectsMovemont(175, 0, 450, 25));

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////故障纵览
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIFault :::::::::::::::::::

            //背景图    /0
            DMIFaultAreaList.Add(RectsMovemont(25, 25, 750, 475));
            //头标题     /1
            DMIFaultAreaList.Add(RectsMovemont(25, 0, 750, 25));
            //头标题第1格  /2
            DMIFaultAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /3
            DMIFaultAreaList.Add(RectsMovemont(185, 0, 440, 25));
            //头标题第3、4格 /4,5
            for (int i = 0; i < 2; i++)
            {
                DMIFaultAreaList.Add(RectsMovemont(625 + i*75, 0, 75, 25));
            }
            //当前行号  /6
            DMIFaultAreaList.Add(RectsMovemont(25, 35, 750, 25));
            //20行信息  /7-126
            for (int i = 0; i < 20; i++)
            {
                //7 /框
                DMIFaultAreaList.Add(RectsMovemont(25, 75 + i*21, 750, 20));
                //8 /第一列
                DMIFaultAreaList.Add(RectsMovemont(25, 75 + i*21, 100, 20));
                //9    /第二列
                DMIFaultAreaList.Add(RectsMovemont(125, 75 + i*21, 70, 20));
                //10    /第三列
                DMIFaultAreaList.Add(RectsMovemont(205, 75 + i*21, 435, 20));
                //11    /第四列
                DMIFaultAreaList.Add(RectsMovemont(640, 75 + i*21, 55, 20));
                //12    /第五列
                DMIFaultAreaList.Add(RectsMovemont(695, 75 + i*21, 80, 20));

            }
            //两侧20个三角位置  /127-146
            for (int i = 0; i < 20; i++)
            {
                DMIFaultAreaList.Add(RectsMovemont(0, 75 + i*21, 800, 20));
            }
            //右侧故障  /147
            DMIFaultAreaList.Add(RectsMovemont(557, 507, 216, 36));

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////行驶期间显示校正措施
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: MovingCalibrationScreen :::::::::::::::::::

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////静止时显示校正措施
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: StopCalibrationScreen :::::::::::::::::::

            #endregion

            #endregion

            #region ::::::::::::::::: 列车信息相关坐标 :::::::::::::::::::::::::

            /////////////////////////////////////////////////////////////////////////////
            ////////主屏视图
            /////////////////////////////////////////////////////////////////////////////
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380B ||
                GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                #region :::::::::::: DMIBasePageD1 :::::::::::::::

                //接触网电压        /0
                DMIBasePageAreaList.Add(RectsMovemont(18, 50, 300, 400));
                //网侧电流          /1
                DMIBasePageAreaList.Add(RectsMovemont(208, 57, 141, 400));
                //受电主断牵引制动  /2
                DMIBasePageAreaList.Add(RectsMovemont(360, 35, 420, 465));
                //小方块--电压      /3-6
                for (int i = 0; i < 4; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(25 + i*36, 455, 36, 36));
                }
                //小方块--牵引电制力    /7-10
                for (int i = 0; i < 4; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(378 + i*76, 455, 36, 36));
                }
                //受电弓-主断       /11-18
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        DMIBasePageAreaList.Add(RectsMovemont(380 + j*70, 70 + i*63, 35, 46));
                    }
                }
                //接触网文字/车编组号   /19-20
                for (int i = 0; i < 2; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(30, 60 + i*25, 150, 25));
                }
                //网侧电流文字      /21
                DMIBasePageAreaList.Add(RectsMovemont(215, 60, 100, 25));
                //居中对齐      /22-25
                for (int i = 0; i < 4; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(380 + i*70, 40, 35, 20));
                }
                //左对齐        /26-33
                for (int i = 0; i < 8; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(370 + i*35, 205, 35, 20));
                }
                //接触网电压数值条  /34-37
                for (int i = 0; i < 2; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(28 + i*60, 50 + 75, 40, 288));
                    DMIBasePageAreaList.Add(RectsMovemont(28 + i*60, 50 + 377, 40, 5));
                }
                //网侧电流小三角    /38
                DMIBasePageAreaList.Add(RectsMovemont(212, 57 + 58, 23, 24));
                //网侧电流条        /39
                DMIBasePageAreaList.Add(RectsMovemont(214, 57 + 39, 40, 341));
                //
                for (int i = 0; i < 4; i++)
                {
                    //牵引制动力双三角 左  /40、44、48、52
                    DMIBasePageAreaList.Add(RectsMovemont(367 + i*74, 213, 23, 24));
                    //牵引制动力数值条 左 /41、45、49、53
                    DMIBasePageAreaList.Add(RectsMovemont(367 + i*74, 225, 30, 210));
                    //牵引制动力双三角 右  /42、46、50、54
                    DMIBasePageAreaList.Add(RectsMovemont(409 + i*74, 213, 23, 24));
                    //牵引制动力数值条 右 /43、47、51、55
                    DMIBasePageAreaList.Add(RectsMovemont(402 + i*74, 225, 30, 210));
                }
                //下标题    /56-65
                for (int i = 0; i < 10; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
                }
                //头标题第1格  /66
                DMIBasePageAreaList.Add(RectsMovemont(25, 0, 150, 25));
                //头标题第2格   /67
                DMIBasePageAreaList.Add(RectsMovemont(175, 0, 450, 25));

                //网侧电流小三角    /68
                DMIBasePageAreaList.Add(RectsMovemont(212, 57 + 182, 23, 24));
                //网侧电流小三角    /69
                DMIBasePageAreaList.Add(RectsMovemont(212, 57 + 214, 23, 24));
                //车辆编号，主断受电弓/70
                DMIBasePageAreaList.Add(RectsMovemont(365, 40, 300, 25));
                //车辆编号，牵引制动/71
                DMIBasePageAreaList.Add(RectsMovemont(365, 200, 350, 25));

                #endregion
            }
            else
            {
                #region :::::::::::: DMIBasePageD1 :::::::::::::::

                //头标题第1格  /0
                DMIBasePageAreaList.Add(RectsMovemont(25, 0, 150, 25));
                //头标题第2格   /1
                DMIBasePageAreaList.Add(RectsMovemont(175, 0, 450, 25));
                //接触网电压        /2
                DMIBasePageAreaList.Add(RectsMovemont(20, 26, 304, 463));
                //网侧电流          /3
                DMIBasePageAreaList.Add(RectsMovemont(622, 57, 141, 400));
                //小方块--电压      /4-7
                for (int i = 0; i < 4; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(28 + i*36, 455, 36, 36));
                }
                //接触网文字/车编组号   /8-9
                for (int i = 0; i < 2; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(35, 60 + i*25, 150, 25));
                }
                //网侧电流文字      /10
                DMIBasePageAreaList.Add(RectsMovemont(629, 60, 100, 25));
                //接触网电压数值条  /11-14
                for (int i = 0; i < 2; i++)
                {
                    DMIBasePageAreaList.Add(RectsMovemont(29 + i*124, 132, 64, 283));
                    DMIBasePageAreaList.Add(RectsMovemont(29 + i*124, 429, 64, 10));
                }
                //网侧电流条        /15
                DMIBasePageAreaList.Add(RectsMovemont(628, 57 + 39, 40, 341));
                //网侧电流小三角    /16
                DMIBasePageAreaList.Add(RectsMovemont(626, 57 + 58, 23, 24));
                //网侧电流小三角    /17
                DMIBasePageAreaList.Add(RectsMovemont(626, 57 + 182, 23, 24));
                //网侧电流小三角    /18
                DMIBasePageAreaList.Add(RectsMovemont(626, 57 + 214, 23, 24));

                #endregion
            }

            #endregion

            #region ::::::::::::::::::::::::::: 维护

            /////////////////////////////////////////////////////////////////////////////
            ////////维修界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIMaintenance :::::::::::::::::::

            //头标题第1格  /0
            DMIMaintenanceAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIMaintenanceAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题    /2-11
            for (int i = 0; i < 10; i++)
            {
                DMIMaintenanceAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容  /12-21
            for (int i = 0; i < 10; i++)
            {
                DMIMaintenanceAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////远程数据传输界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIRemoteDataTransfer :::::::::::::::::::

            //头标题第1格  /0
            DMIRemoteDataTransferAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIRemoteDataTransferAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题    /2-11
            for (int i = 0; i < 10; i++)
            {
                DMIRemoteDataTransferAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容  /12
            DMIRemoteDataTransferAreaList.Add(RectsMovemont(250, 180, 300, 40));

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////中间直流环节界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: LinkOfDC_DragScreen :::::::::::::::::::

            //头标题第1格  /0
            DMITractionDcLinkAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMITractionDcLinkAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题    /2-11
            for (int i = 0; i < 10; i++)
            {
                DMITractionDcLinkAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //底图  /12
            DMITractionDcLinkAreaList.Add(RectsMovemont(90, 103, 616, 321));
            //16根可变矩形条    /13-28
            for (int i = 0; i < 16; i++)
            {
                DMITractionDcLinkAreaList.Add(RectsMovemont(106 + i*35, 245, 26, 139));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////车辆的版本界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: Edition_Car_1Screen :::::::::::::::::::

            //头标题第1格  /0
            DMIVersionsCarAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIVersionsCarAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题    /2-11
            for (int i = 0; i < 10; i++)
            {
                DMIVersionsCarAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //底图  /12
            DMIVersionsCarAreaList.Add(RectsMovemont(0, 0, 800, 600));

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////运行速度设定检测/牵引手柄检测界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: ValueOfV_SetScreen :::::::::::::::::::

            //头标题第1格  /0
            DmivSetpointSensorValueAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DmivSetpointSensorValueAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题    /2-11
            for (int i = 0; i < 10; i++)
            {
                DmivSetpointSensorValueAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //底图  /12
            DmivSetpointSensorValueAreaList.Add(RectsMovemont(0, 0, 800, 600));
            //通道切断接通  /13-14
            for (int i = 0; i < 2; i++)
            {
                DmivSetpointSensorValueAreaList.Add(RectsMovemont(190 + i*150, 430, 24, 24));
            }
            //2个通道   /15-16
            for (int i = 0; i < 2; i++)
            {
                DmivSetpointSensorValueAreaList.Add(RectsMovemont(177 + i*150, 162, 55, 250));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////能量计量界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIEnergyCounter :::::::::::::::::::

            //头标题第1格  /0
            DMIEnergyCounterAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIEnergyCounterAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题    /2-11
            for (int i = 0; i < 10; i++)
            {
                DMIEnergyCounterAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //左排文字  /12-22
            for (int i = 0; i < 11; i++)
            {
                DMIEnergyCounterAreaList.Add(RectsMovemont(175, 45 + i*40, 100, 40));
            }
            //右排上部  /23-26
            for (int i = 0; i < 4; i++)
            {
                DMIEnergyCounterAreaList.Add(RectsMovemont(460, 85 + i*40, 200, 40));
            }
            //右排下部  /27-30
            for (int i = 0; i < 4; i++)
            {
                DMIEnergyCounterAreaList.Add(RectsMovemont(460, 325 + i*40, 200, 40));
            }

            #endregion

            #endregion

            #region ::::::::::::::::::::::::::::: 系统

            /////////////////////////////////////////////////////////////////////////////
            ////////系统界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMISystem :::::::::::::::::::

            //头标题第1格  /0
            DMISystemAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMISystemAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMISystemAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DMISystemAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////列车配置显示页面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMITrainConfigurationDisplay :::::::::::::::::::

            //头标题第1格  /0
            DMITrainConfigurationDisplayAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMITrainConfigurationDisplayAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 /2-11
            for (int i = 0; i < 10; i++)
            {
                DMITrainConfigurationDisplayAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //图片  /12
            DMITrainConfigurationDisplayAreaList.Add(RectsMovemont(20, 40, 770, 425));

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////蓄电池电压显示页面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: Battery_VoltageScreen :::::::::::::::::::

            //头标题第1格  /0
            DMIBatteryVoltage110VAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIBatteryVoltage110VAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 /2-11
            for (int i = 0; i < 10; i++)
            {
                DMIBatteryVoltage110VAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //图片  /12
            DMIBatteryVoltage110VAreaList.Add(RectsMovemont(20, 60, 481, 440));
            //矩形条    /13-14
            for (int i = 0; i < 2; i++)
            {
                DMIBatteryVoltage110VAreaList.Add(RectsMovemont(25 + i*75, 155, 50, 320));
            }
            //矩形条    /15-16
            for (int i = 0; i < 2; i++)
            {
                DMIBatteryVoltage110VAreaList.Add(RectsMovemont(250 + i*75, 155, 50, 320));
            }
            //车辆号    /17-18
            for (int i = 0; i < 2; i++)
            {
                DMIBatteryVoltage110VAreaList.Add(RectsMovemont(25 + i*75, 155, 50, 320));
            }
            //车辆号    /19-20
            for (int i = 0; i < 2; i++)
            {
                DMIBatteryVoltage110VAreaList.Add(RectsMovemont(250 + i*75, 155, 50, 320));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////关闭后车钩罩
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMITrainConfigurationDisplay :::::::::::::::::::

            //头标题第1格  /0
            DMICloseCouplerHatchesAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMICloseCouplerHatchesAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 /2-11
            for (int i = 0; i < 10; i++)
            {
                DMICloseCouplerHatchesAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //图片  /12
            DMICloseCouplerHatchesAreaList.Add(RectsMovemont(0, 25, 800, 480));

            #endregion

            #endregion

            #region :::::::::::::::::::::::::: 门

            /////////////////////////////////////////////////////////////////////////////
            ////////车门
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIDoors :::::::::::::::::::

            //头标题第1格  /0
            DMIDoorsAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIDoorsAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 /2-11
            for (int i = 0; i < 10; i++)
            {
                DMIDoorsAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //图片  /12
            DMIDoorsAreaList.Add(RectsMovemont(319, 40, 449, 148));

            //车门16个车 /13-76
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    DMIDoorsAreaList.Add(RectsMovemont(180 + j*37, 370 + i*25, 32, 24));
                }
            }
            //16个车厢门状态    /77-92
            for (int i = 0; i < 16; i++)
            {
                DMIDoorsAreaList.Add(RectsMovemont(183 + i*37, 330, 25, 25));
            }
            //两侧门 /93-124
            for (int i = 0; i < 16; i++)
            {
                DMIDoorsAreaList.Add(RectsMovemont(180 + i*37, 320, 37, 3));
                DMIDoorsAreaList.Add(RectsMovemont(180 + i*37, 360, 37, 3));
            }
            //大门图/125
            DMIDoorsAreaList.Add(RectsMovemont(85, 300, 51, 51));
            //门已禁用/126
            DMIDoorsAreaList.Add(RectsMovemont(180, 260, 170, 25));
            //车厢号/127-142
            for (int i = 0; i < 16; i++)
            {
                DMIDoorsAreaList.Add(RectsMovemont(180 + i*37, 293, 37, 25));
            }

            #endregion

            #endregion

            #region ::::::::::::::::::::::::::::: ATP系统

            /////////////////////////////////////////////////////////////////////////////
            ////////ATP系统界面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIATPSystem :::::::::::::::::::

            //头标题第1格  /0
            DmiatpSystemAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DmiatpSystemAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DmiatpSystemAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DmiatpSystemAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            #endregion

            #region ::::::::::::::::::::::::::::: 紧急

            /////////////////////////////////////////////////////////////////////////////
            ////////紧急
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: Drive_BrakeScreen :::::::::::::::::::

            //头标题第1格  /0
            DMIEmergencyAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIEmergencyAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMIEmergencyAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DMIEmergencyAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            #endregion

            #region ::::::::::::::::::::::::::::: 准备/整备

            /////////////////////////////////////////////////////////////////////////////
            ////////准备/停放
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: Drive_ServiceScreen :::::::::::::::::::

            //头标题第1格  /0
            DMIPreparingStablingAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIPreparingStablingAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMIPreparingStablingAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DMIPreparingStablingAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            #endregion

            #region :::::::::::::::::::::: 牵引制动

            //头标题第1格  /0
            DMIDrivingBrakingAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIDrivingBrakingAreaList.Add(RectsMovemont(175, 0, 450, 25));
            //底图  /2
            DMIDrivingBrakingAreaList.Add(RectsMovemont(30, 30, 760, 469));
            //接触网电压        /3
            DMIDrivingBrakingAreaList.Add(RectsMovemont(28, 41, 300, 400));
            //接触网文字/车编组号   /4-5
            for (int i = 0; i < 2; i++)
            {
                DMIDrivingBrakingAreaList.Add(RectsMovemont(40, 51 + i*25, 150, 25));
            }
            //接触网电压数值条  /6-9
            for (int i = 0; i < 2; i++)
            {
                DMIDrivingBrakingAreaList.Add(RectsMovemont(37 + i*124, 116, 64, 288));
                DMIDrivingBrakingAreaList.Add(RectsMovemont(37 + i*124, 413, 64, 10));
            }
            //牵引制动底图  /10-11
            DMIDrivingBrakingAreaList.Add(RectsMovemont(185, 59, 382, 440));
            DMIDrivingBrakingAreaList.Add(RectsMovemont(215, 59, 382, 440));
            //小方块--牵引电制力    /12-19
            for (int i = 0; i < 4; i++)
            {
                DMIDrivingBrakingAreaList.Add(RectsMovemont(199 + i*75, 455, 36, 36));
                DMIDrivingBrakingAreaList.Add(RectsMovemont(229 + i*75, 455, 36, 36));
            }
            //位置改变前的牵引制动  /20-35
            for (int i = 0; i < 4; i++)
            {
                //牵引制动力双三角 左  /20、24、28、32
                DMIDrivingBrakingAreaList.Add(RectsMovemont(186 + i*74, 147, 23, 24));
                //牵引制动力数值条 左 /21、25、29、33
                DMIDrivingBrakingAreaList.Add(RectsMovemont(186 + i*74, 159, 30, 276));
                //牵引制动力双三角 右  /22、26、30、34
                DMIDrivingBrakingAreaList.Add(RectsMovemont(228 + i*74, 147, 23, 24));
                //牵引制动力数值条 右 /23、27、31、35
                DMIDrivingBrakingAreaList.Add(RectsMovemont(220 + i*74, 159, 30, 276));
            }
            //位置改变后的牵引制动  /36-51
            for (int i = 0; i < 4; i++)
            {
                //牵引制动力双三角 左  /36、40、44、48
                DMIDrivingBrakingAreaList.Add(RectsMovemont(216 + i*74, 147, 23, 24));
                //牵引制动力数值条 左 /37、41、45、49
                DMIDrivingBrakingAreaList.Add(RectsMovemont(216 + i*74, 159, 30, 276));
                //牵引制动力双三角 右  /38、42、46、50
                DMIDrivingBrakingAreaList.Add(RectsMovemont(258 + i*74, 147, 23, 24));
                //牵引制动力数值条 右 /39、43、47、51
                DMIDrivingBrakingAreaList.Add(RectsMovemont(250 + i*74, 159, 30, 276));
            }

            #endregion

            #region ::::::::::::::::::::::::::::: 自动速度控制

            /////////////////////////////////////////////////////////////////////////////
            ////////自动速度控制
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIASC :::::::::::::::::::

            //头标题第1格  /0
            DmiascAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DmiascAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DmiascAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DmiascAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            #endregion

            #region ::::::::::::::::::::::::::::: 开关

            /////////////////////////////////////////////////////////////////////////////
            ////////开关
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMISwitching :::::::::::::::::::

            //头标题第1格  /0
            DMISwitchingAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMISwitchingAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMISwitchingAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DMISwitchingAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////网侧电流限制页面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMILineCurrentLimit :::::::::::::::::::

            //头标题第1格  /0
            DMILineCurrentLimitAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMILineCurrentLimitAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMILineCurrentLimitAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-23
            for (int i = 0; i < 12; i++)
            {
                DMILineCurrentLimitAreaList.Add(RectsMovemont(115, 40f + (i*37.5f), 435, 45));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////风扇页面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIFans :::::::::::::::::::

            //头标题第1格  /0
            DMIFansAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIFansAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMIFansAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DMIFansAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////牵引页面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMITraction :::::::::::::::::::

            //头标题第1格  /0
            DMITractionAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMITractionAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMITractionAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //12    /框线
            DMITractionAreaList.Add(RectsMovemont(0, 0, 800, 600));
            //13    /蓝色块
            DMITractionAreaList.Add(RectsMovemont(30, 36, 52, 52));
            //14    /25kV
            DMITractionAreaList.Add(RectsMovemont(30, 100, 140, 25));
            //15-19 /标题(受电弓、主断路器、车顶隔离开关、牵引变流器、辅助供电单元)
            for (int i = 0; i < 5; i++)
            {
                DMITractionAreaList.Add(RectsMovemont(30, 125 + i*63, 149, 28));
            }
            //20-35 /车厢号
            for (int i = 0; i < 16; i++)
            {
                DMITractionAreaList.Add(RectsMovemont(180 + i*37, 100, 37, 25));
            }
            //36-99     /三行图标位置
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    DMITractionAreaList.Add(RectsMovemont(181 + j*37, 132 + i*63, 35, 46));
                }
            }
            //100-131   /辅助供电单元
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    DMITractionAreaList.Add(RectsMovemont(180 + j*37, 380 + i*42, 36, 38));
                }
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////洗车运行页面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIFans :::::::::::::::::::

            //头标题第1格  /0
            DMIWashRunAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIWashRunAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMIWashRunAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DMIWashRunAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            /////////////////////////////////////////////////////////////////////////////
            ////////前窗加热页面
            /////////////////////////////////////////////////////////////////////////////

            #region :::::::::::: DMIFrontWindowHeating :::::::::::::::::::

            //头标题第1格  /0
            DMIFrontWindowHeatingAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIFrontWindowHeatingAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMIFrontWindowHeatingAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容 12-21
            for (int i = 0; i < 10; i++)
            {
                DMIFrontWindowHeatingAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }

            #endregion

            #endregion

            #region ::::::::::::::::::::::::::::: 制动有效率

            //头标题第1格  /0
            DMIBrakingPowerAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIBrakingPowerAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //底图      /2
            DMIBrakingPowerAreaList.Add(RectsMovemont(21, 45, 773, 446));
            //提示信息  /3
            DMIBrakingPowerAreaList.Add(RectsMovemont(226, 50, 364, 25));
            //日期  /4
            DMIBrakingPowerAreaList.Add(RectsMovemont(178, 113, 76, 24));
            //时间  /5
            DMIBrakingPowerAreaList.Add(RectsMovemont(319, 113, 75, 24));
            //制动有效率分子    /6
            DMIBrakingPowerAreaList.Add(RectsMovemont(177, 164, 48, 22));
            //制动有效率分母    /7
            DMIBrakingPowerAreaList.Add(RectsMovemont(177, 188, 48, 22));
            //制动有效率值  /8
            DMIBrakingPowerAreaList.Add(RectsMovemont(267, 175, 41, 22));
            //轴数  /9
            DMIBrakingPowerAreaList.Add(RectsMovemont(727, 184, 41, 25));
            //车长  /10
            DMIBrakingPowerAreaList.Add(RectsMovemont(727, 220, 41, 25));
            //动车组    /11
            DMIBrakingPowerAreaList.Add(RectsMovemont(32, 300, 76, 25));
            //动车组    /12
            DMIBrakingPowerAreaList.Add(RectsMovemont(178, 300, 76, 25));
            //动车组    /13
            DMIBrakingPowerAreaList.Add(RectsMovemont(478, 300, 76, 25));
            //车厢号 14-29  /不可用 30-45   /关闭 46-61
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    DMIBrakingPowerAreaList.Add(RectsMovemont(185 + j*38, 336 + i*50, 23, 29));
                }
            }

            #endregion

            #region ::::::::::::::::::::::::::::: 最高限速表

            //头标题第1格  /0
            DMIVmaxTableAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIVmaxTableAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //图片  /2
            DMIVmaxTableAreaList.Add(RectsMovemont(190, 75, 309, 305));

            #endregion

            #region ::::::::::::::::::::::::::::: 制动功能状态

            //头标题第1格  /0
            DMIStatusOfBrakeFunctionsAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIStatusOfBrakeFunctionsAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //内容  /2-20
            for (int index = 0; index < 19; index++)
            {
                DMIStatusOfBrakeFunctionsAreaList.Add(RectsMovemont(178, 25 + index*25, 300, 25));
            }

            #endregion

            #region ::::::::::::::::::::::::::::: 上次试验结果

            //头标题第1格  /0
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //图片位置  /2
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(25, 50, 752, 219));
            //动车组1   /3
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(26, 80, 80, 25));
            //日期 时间 /4
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(128, 100, 125, 24));
            //时间  /5
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(204, 100, 49, 24));
            //状态1 /6
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(128, 130, 404, 25));
            //状态2 /7
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(128, 160, 404, 103));
            //状态2中的值坐标   /8-17
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(128 + i*202, 160 + j*20, 202, 20));
                }
            }
            //动车组1   /18
            DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(553, 91, 80, 25));
            //试验时间  /19-24
            for (int i = 0; i < 6; i++)
            {
                DMIResultOfLastBrakeTestAreaList.Add(RectsMovemont(642, 123 + i*22.5f, 125, 22.5f));
            }

            #endregion

            #region ::::::::::::::::::::::::::::: 制动试验

            //头标题第1格  /0
            DMIBrakeTestAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIBrakeTestAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题    /2-11
            for (int i = 0; i < 10; i++)
            {
                DMIBrakeTestAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //内容  /12-21
            for (int i = 0; i < 10; i++)
            {
                DMIBrakeTestAreaList.Add(RectsMovemont(115, 40 + i*45, 435, 45));
            }
            //22
            DMIBrakeTestAreaList.Add(RectsMovemont(5, 40, 790, 45));
            //直接制动试验,紧急制动试验,BP泄漏试验,间接制动试验 /23    
            DMIBrakeTestAreaList.Add(RectsMovemont(178, 40, 450, 200));
            //MRP贯通性试验 /24
            DMIBrakeTestAreaList.Add(RectsMovemont(500, 120, 300, 250));
            //BP贯通性试验  /25
            DMIBrakeTestAreaList.Add(RectsMovemont(350, 120, 450, 200));

            #endregion

            #region ::::::::::::::::::::::::::::: 右屏基本画面

            //头标题第1格  /0
            DMIBrakeStatusAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIBrakeStatusAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMIBrakeStatusAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //列车管/12-13
            DMIBrakeStatusAreaList.Add(RectsMovemont(25, 43, 68, 442));
            DMIBrakeStatusAreaList.Add(RectsMovemont(25, 143, 30, 325));
            //总风管/14-15
            DMIBrakeStatusAreaList.Add(RectsMovemont(712, 43, 68, 442));
            DMIBrakeStatusAreaList.Add(RectsMovemont(712, 83, 30, 385));
            //16-18
            DMIBrakeStatusAreaList.Add(RectsMovemont(100, 331, 39, 39));
            DMIBrakeStatusAreaList.Add(RectsMovemont(100, 423, 39, 39));
            DMIBrakeStatusAreaList.Add(RectsMovemont(100, 180, 48, 321));
            //正常操作  /19
            DMIBrakeStatusAreaList.Add(RectsMovemont(148, 186, 100, 40));

            //车号  /20-35
            for (int i = 0; i < 16; i++)
            {
                DMIBrakeStatusAreaList.Add(RectsMovemont(150 + i*35, 290, 29, 20));
            }
            //36-67,68-99
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    //大矩形
                    DMIBrakeStatusAreaList.Add(RectsMovemont(150 + j*35, 320 + i*92, 29, 33));
                    //小矩形
                    DMIBrakeStatusAreaList.Add(RectsMovemont(150 + j*35, 357 + i*92, 29, 15));
                }
            }
            //总风管 100
            DMIBrakeStatusAreaList.Add(RectsMovemont(174, 42, 316, 450));
            //总风管101-108
            for (int i = 0; i < 4; i++)
            {
                DMIBrakeStatusAreaList.Add(RectsMovemont(175 + i*75, 117, 30, 350));
                DMIBrakeStatusAreaList.Add(RectsMovemont(175 + i*75, 85, 30, 30));
            }
            //列车管 109
            DMIBrakeStatusAreaList.Add(RectsMovemont(25, 43, 314, 451));
            //列车管110-117
            for (int i = 0; i < 4; i++)
            {
                DMIBrakeStatusAreaList.Add(RectsMovemont(26 + i*75, 143, 30, 325));
                DMIBrakeStatusAreaList.Add(RectsMovemont(26 + i*75, 59, 30, 30));
            }
            //D /118
            DMIBrakeStatusAreaList.Add(RectsMovemont(101, 422, 39, 39));

            //车号  /119-135
            for (int i = 0; i < 16; i++)
            {
                DMIBrakeStatusAreaList.Add(RectsMovemont(150 + i*35, 380, 29, 20));
            }

            #endregion

            #region ::::::::::::::::::::::::::::: 停放制动画面

            //头标题第1格  /0
            DMIParkingBrakesAreaList.Add(RectsMovemont(25, 0, 150, 25));
            //头标题第2格   /1
            DMIParkingBrakesAreaList.Add(RectsMovemont(178, 0, 450, 25));
            //下标题 2-11
            for (int i = 0; i < 10; i++)
            {
                DMIParkingBrakesAreaList.Add(RectsMovemont(26 + i*75, 551, 73, 48));
            }
            //列车管/12-13
            DMIParkingBrakesAreaList.Add(RectsMovemont(25, 43, 68, 442));
            DMIParkingBrakesAreaList.Add(RectsMovemont(25, 143, 30, 325));
            //总风管/14-15
            DMIParkingBrakesAreaList.Add(RectsMovemont(712, 43, 68, 442));
            DMIParkingBrakesAreaList.Add(RectsMovemont(712, 83, 30, 385));
            //16-18
            DMIParkingBrakesAreaList.Add(RectsMovemont(100, 331, 39, 39));
            DMIParkingBrakesAreaList.Add(RectsMovemont(100, 423, 39, 39));
            DMIParkingBrakesAreaList.Add(RectsMovemont(100, 180, 48, 321));
            //停放制动  /19
            DMIParkingBrakesAreaList.Add(RectsMovemont(148, 186, 40, 100));

            //车号  /20-35
            for (int i = 0; i < 16; i++)
            {
                DMIParkingBrakesAreaList.Add(RectsMovemont(150 + i*35, 375, 29, 20));
            }
            //36-67
            for (int i = 0; i < 16; i++)
            {
                //大矩形
                DMIParkingBrakesAreaList.Add(RectsMovemont(150 + i*35, 412, 29, 33));
                //小矩形
                DMIParkingBrakesAreaList.Add(RectsMovemont(150 + i*35, 449, 29, 15));
            }

            #endregion

            m_ClassIsInited = true;
        }

        #region :::::::::::::::::::::: RectangleF :::::::::::::::::::::::::::::

        /// <summary>
        /// DMIButton 按钮界面坐标
        /// </summary>
        private static readonly List<RectangleF> DMIBlackScreenAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIButton 按钮界面坐标
        /// </summary>
        private static readonly List<RectangleF> DMIButtonAreaList = new List<RectangleF>();

        /// <summary>
        /// DMITitle 标题界面坐标
        /// </summary>
        private static readonly List<RectangleF> DMITitleAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIInfoBox 信息和界面坐标
        /// </summary>
        private static readonly List<RectangleF> DMIInfoBoxAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIFault 故障相关界面坐标
        /// </summary>
        private static readonly List<RectangleF> DMIFaultAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIBasePageD1 基本页
        /// </summary>
        private static readonly List<RectangleF> DMIBasePageAreaList = new List<RectangleF>();

        #region ::: 维修 :::

        /// <summary>
        /// DMIMaintenance 维修视图坐标
        /// </summary>
        private static readonly List<RectangleF> DMIMaintenanceAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIRemoteDataTransfer 远程数据传输视图坐标
        /// </summary>
        private static readonly List<RectangleF> DMIRemoteDataTransferAreaList = new List<RectangleF>();

        /// <summary>
        /// DMITractionDC_Link 中间直流环节
        /// </summary>
        private static readonly List<RectangleF> DMITractionDcLinkAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIVersionsCar 车辆1的版本
        /// </summary>
        private static readonly List<RectangleF> DMIVersionsCarAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIV_SetpointSensorValue 速度运行设定检测界面坐标
        /// </summary>
        private static readonly List<RectangleF> DmivSetpointSensorValueAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIEnergyCounter 能量计量界面坐标
        /// </summary>
        private static readonly List<RectangleF> DMIEnergyCounterAreaList = new List<RectangleF>();

        #endregion

        #region ::: 系统 :::

        /// <summary>
        /// DMISystem 系统屏幕
        /// </summary>
        private static readonly List<RectangleF> DMISystemAreaList = new List<RectangleF>();

        /// <summary>
        /// DMITrainConfigurationDisplay 列车配置显示页面
        /// </summary>
        private static readonly List<RectangleF> DMITrainConfigurationDisplayAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIBatteryVoltage110V 蓄电池电压页面
        /// </summary>
        private static readonly List<RectangleF> DMIBatteryVoltage110VAreaList = new List<RectangleF>();

        /// <summary>
        /// 关闭后车钩罩
        /// </summary>
        private static readonly List<RectangleF> DMICloseCouplerHatchesAreaList = new List<RectangleF>();

        #endregion

        /// <summary>
        /// DMIDoors 车门画面
        /// </summary>
        private static readonly List<RectangleF> DMIDoorsAreaList = new List<RectangleF>();

        #region ::: ATP系统 :::

        /// <summary>
        /// DMIATPSystem ATP系统屏幕
        /// </summary>
        private static readonly List<RectangleF> DmiatpSystemAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIDMIReplace DMI更换显示页面
        /// </summary>
        private static readonly List<RectangleF> DmidmiReplaceAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIEVCReplace EVC更换显示页面
        /// </summary>
        private static readonly List<RectangleF> DmievcReplaceAreaList = new List<RectangleF>();

        #endregion

        #region ::: 紧急 :::

        /// <summary>
        /// DMIEmergency 紧急屏幕
        /// </summary>
        private static readonly List<RectangleF> DMIEmergencyAreaList = new List<RectangleF>();

        #endregion

        #region ::: 准备/停放 :::

        /// <summary>
        /// DMIPreparingStabling 准备/停放屏幕画面
        /// </summary>
        private static readonly List<RectangleF> DMIPreparingStablingAreaList = new List<RectangleF>();

        /// <summary>
        /// DMITrainNoAndTDNo 列车编号和YD编号页面
        /// </summary>
        private static readonly List<RectangleF> DMITrainNoAndTdNoAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIStartStablingOperation 开始停放操作页面
        /// </summary>
        private static readonly List<RectangleF> DMIStartStablingOperationAreaList = new List<RectangleF>();

        #endregion

        /// <summary>
        /// DMIDrivingBraking 传动/制动屏幕
        /// </summary>
        private static readonly List<RectangleF> DMIDrivingBrakingAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIASC 自动速度控制屏幕
        /// </summary>
        private static readonly List<RectangleF> DmiascAreaList = new List<RectangleF>();

        #region ::: 开关 :::

        /// <summary>
        /// DMISwitching 开关屏幕
        /// </summary>
        private static readonly List<RectangleF> DMISwitchingAreaList = new List<RectangleF>();

        /// <summary>
        /// DMILineCurrentLimit 网侧电流限制页面
        /// </summary>
        private static readonly List<RectangleF> DMILineCurrentLimitAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIFans 风扇页面
        /// </summary>
        private static readonly List<RectangleF> DMIFansAreaList = new List<RectangleF>();

        /// <summary>
        /// DMITraction 牵引页面
        /// </summary>
        private static readonly List<RectangleF> DMITractionAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIAirCondition 空调页面
        /// </summary>
        private static readonly List<RectangleF> DMIAirConditionAreaList = new List<RectangleF>();

        /// <summary>
        /// DMILighting 照明页面
        /// </summary>
        private static readonly List<RectangleF> DMILightingAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIWashRun 冲洗运行页面
        /// </summary>
        private static readonly List<RectangleF> DMIWashRunAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIFrontWindowHeating 前窗加热页面
        /// </summary>
        private static readonly List<RectangleF> DMIFrontWindowHeatingAreaList = new List<RectangleF>();

        #endregion

        /// <summary>
        /// DMIBrakeStatus DMI制动状态
        /// </summary>
        private static readonly List<RectangleF> DMIBrakeStatusAreaList = new List<RectangleF>();

        #region ::: 制动有效率 :::

        /// <summary>
        /// DMIBrakingPower DMI制动力
        /// </summary>
        private static readonly List<RectangleF> DMIBrakingPowerAreaList = new List<RectangleF>();


        /// <summary>
        /// DMIBrakeSheet DMI制动图表
        /// </summary>
        private static readonly List<RectangleF> DMIBrakeSheetAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIVmaxTable DMI最高限速表
        /// </summary>
        private static readonly List<RectangleF> DMIVmaxTableAreaList = new List<RectangleF>();

        /// <summary>
        /// DMIResultOfLastBrakeTest DMI上次试验结果
        /// </summary>
        private static readonly List<RectangleF> DMIResultOfLastBrakeTestAreaList = new List<RectangleF>();

        #endregion

        /// <summary>
        /// DMIStatusOfBrakeFunctions DMI制动功能状态
        /// </summary>
        private static readonly List<RectangleF> DMIStatusOfBrakeFunctionsAreaList = new List<RectangleF>();

        #region ::: 制动试验 :::

        /// <summary>
        /// DMIBrakeTest DMI制动试验
        /// </summary>
        private static readonly List<RectangleF> DMIBrakeTestAreaList = new List<RectangleF>();

        #endregion

        /// <summary>
        /// DMIParkingBrakes DMI停放制动
        /// </summary>
        private static readonly List<RectangleF> DMIParkingBrakesAreaList = new List<RectangleF>();

        #endregion

        private static bool m_ClassIsInited;

        /// <summary>
        /// 初始化完成
        /// </summary>
        public static bool ClassIsInited
        {
            get { return m_ClassIsInited; }
        }

        #region 屏大小偏移变化相关

        //偏移坐标X
        // ReSharper disable once InconsistentNaming
        private const int _offsetCoodinates_X = 0;

        /// <summary>
        /// 偏移坐标X
        /// </summary>
        public static int OffsetCoodinatesX
        {
            get { return _offsetCoodinates_X; }
        }

        //偏移坐标Y
        // ReSharper disable once InconsistentNaming
        //private const int _offsetCoodinates_Y = 67;
        private const int _offsetCoodinates_Y = 0;

        /// <summary>
        /// 偏移坐标Y
        /// </summary>
        public static int OffsetCoodinatesY
        {
            get { return _offsetCoodinates_Y; }
        }

        //缩放比例
        private static readonly float m_Scaling = 1.0f;

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