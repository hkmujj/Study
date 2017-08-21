namespace SS4B_TMS.Common
{
    /// <summary>
    ///     功能描述：视图状态枚举
    /// </summary>
    public enum ViewState
    {
        /// <summary>
        ///     黑屏
        /// </summary>
        BlackScreen = 0,

        /// <summary>
        ///     主界面
        /// </summary>
        MainInterface = 1,

        /// <summary>
        ///     旁路
        /// </summary>
        BypassInfo = 101,

        /// <summary>
        ///     旁路界面2
        /// </summary>
        BypassInfo2 = 102,

        /// <summary>
        ///     故障
        /// </summary>
        FaultInfo = 111,

        /// <summary>
        ///     故障--实时故障信息
        /// </summary>
        FaultInfoRealTime = 112,

        /// <summary>
        ///     故障
        ///     换页向上按钮
        /// </summary>
        FiUpBtn = 1110,

        /// <summary>
        ///     故障
        ///     换页向下按钮
        /// </summary>
        FiDownBtn = 1111,

        /// <summary>
        ///     故障
        ///     故障页面返回按钮
        /// </summary>
        FiReturnArrow = 1112,

        /// <summary>
        ///     故障--实时故障信息
        ///     确认按钮
        /// </summary>
        FiRtConfirmBtn = 1120,

        /// <summary>
        ///     二级界面
        /// </summary>
        SecondInterface = 2,

        /// <summary>
        ///     二级界面
        ///     数字键1
        /// </summary>
        SiDigitOne = 201,

        /// <summary>
        ///     二级界面
        ///     数字键2
        /// </summary>
        SiDigitTwo = 202,

        /// <summary>
        ///     二级界面
        ///     数字键3
        /// </summary>
        SiDigitThree = 203,

        /// <summary>
        ///     二级界面
        ///     数字键4
        /// </summary>
        SiDigitFour = 204,

        /// <summary>
        ///     二级界面
        ///     数字键5
        /// </summary>
        SiDigitFive = 205,

        /// <summary>
        ///     二级界面
        ///     数字键6
        /// </summary>
        SiDigitSix = 206,

        /// <summary>
        ///     二级界面
        ///     数字键7
        /// </summary>
        SiDigitSeven = 207,

        /// <summary>
        ///     二级界面
        ///     数字键8
        /// </summary>
        SiDigitEight = 208,

        /// <summary>
        ///     二级界面
        ///     数字键9
        /// </summary>
        SiDigitNine = 209,

        /// <summary>
        ///     二级界面
        ///     数字键0
        /// </summary>
        SiDigitZero = 210,

        /// <summary>
        ///     二级界面
        ///     返回按钮
        /// </summary>
        SiBtnBack = 2001,

        /// <summary>
        ///     二级界面
        ///     上移
        /// </summary>
        SiBtnShitUp = 2002,

        /// <summary>
        ///     二级界面
        ///     下移
        /// </summary>
        SiBtnShitDown = 2003,

        /// <summary>
        ///     二级界面
        ///     左移
        /// </summary>
        SiBtnShitLeft = 2004,

        /// <summary>
        ///     二级界面
        ///     右移
        /// </summary>
        SiBtnShitRight = 2005,

        /// <summary>
        ///     二级界面
        ///     确认按钮
        /// </summary>
        SiBtnConfirm = 2006,

        /// <summary>
        ///     制动机页面1
        /// </summary>
        BrakePage1 = 3,

        /// <summary>
        ///     制动机页面2
        /// </summary>
        BrakePage2 = 301,

        /// <summary>
        ///     制动机页面3
        /// </summary>
        BrakePage3 = 302,

        /// <summary>
        ///     辅助
        /// </summary>
        Assist = 4,

        /// <summary>
        ///     辅助页面2
        /// </summary>
        AssistPage2 = 401,

        /// <summary>
        ///     空调
        /// </summary>
        AirConditioner = 5,

        /// <summary>
        ///     空调界面2
        /// </summary>
        AirConditionerPage2 = 501,

        /// <summary>
        ///     空调界面1
        ///     自动
        /// </summary>
        AirConditionP1Auto = 5010,

        /// <summary>
        ///     空调界面1
        ///     手动
        /// </summary>
        AirConditionP1Manual = 5011,

        /// <summary>
        ///     空调界面1
        ///     通风
        /// </summary>
        AirConditionP1Air = 5012,

        /// <summary>
        ///     空调界面1
        ///     停止
        /// </summary>
        AirConditionP1Stop = 5013,

        /// <summary>
        ///     空调界面2
        ///     关闭按钮
        /// </summary>
        AirConditionP2NewAirClose = 5014,

        ///// <summary>
        ///// PIS,乘客信息
        ///// </summary>
        //PIS = 6,

        /// <summary>
        ///     PIS-自动模式
        /// </summary>
        PISAutoModel = 6,

        /// <summary>
        ///     PIS-半自动模式
        /// </summary>
        PISSemiAutoModel = 602,

        /// <summary>
        ///     PIS-手动模式 manual
        /// </summary>
        PISManualModel = 603,

        /// <summary>
        ///     PIS-旅程设置route
        /// </summary>
        PISRouteSet = 604,

        /// <summary>
        ///     PIS-特殊信息
        /// </summary>
        PISSpecialInfo = 605,

        /// <summary>
        ///     PIS选择按钮-下一站
        ///     手动模式界面
        /// </summary>
        PISMSelectNextStation = 6030,

        /// <summary>
        ///     PIS选择按钮-终点站
        ///     手动模式界面
        /// </summary>
        PISMSelectEndStation = 6031,

        /// <summary>
        ///     PIS发车按钮
        ///     手动模式界面
        /// </summary>
        PISMDepartBtn = 6032,

        /// <summary>
        ///     PIS下一站按钮
        ///     手动模式界面
        /// </summary>
        PISMNextStationBtn = 6033,

        /// <summary>
        ///     PIS到达站按钮
        ///     手动模式界面
        /// </summary>
        PISMArriveStationBtn = 6034,

        /// <summary>
        ///     PIS 选择按钮-下一站
        ///     旅程设置界面
        /// </summary>
        PISRSelectNextStation = 6040,

        /// <summary>
        ///     PIS选择按钮-终点站
        ///     旅程设置界面
        /// </summary>
        PISRSelectEndStation = 6041,

        /// <summary>
        ///     PIS-修改按钮
        ///     旅程设置界面
        /// </summary>
        PISRModifyBtn = 6042,

        /// <summary>
        ///     PIS-确认按钮
        ///     旅程设置界面
        /// </summary>
        PISRConfrimBtn = 6043,

        /// <summary>
        ///     PIS-取消按钮
        ///     旅程设置界面
        /// </summary>
        PISRCancelBtn = 6044,

        /// <summary>
        ///     PIS-单次播放
        ///     特殊信息界面
        /// </summary>
        PISSSingleBtn = 6050,

        /// <summary>
        ///     PIS-循环播放
        ///     特殊信息界面
        /// </summary>
        PISSCycleBtn = 6051,

        /// <summary>
        ///     PIS-向上翻页按钮
        ///     特殊信息界面
        /// </summary>
        PISSUpBtn = 6052,

        /// <summary>
        ///     PIS-向下翻页按钮
        ///     特殊信息界面
        /// </summary>
        PISSDownBtn = 6053,

        /// <summary>
        ///     PIS-返回箭头按钮
        ///     特殊信息界面
        /// </summary>
        PISSReturnArrow = 6054,

        /// <summary>
        ///     无线界面
        /// </summary>
        WirelessInterface = 7,

        /// <summary>
        ///     无线界面
        ///     显示状态按钮
        /// </summary>
        WiBtnShowStatus = 71001,

        /// <summary>
        ///     移除编组
        /// </summary>
        WiBtnRemoveBz = 704,

        /// <summary>
        ///     编组设置
        /// </summary>
        WiBtnBzSetting = 706,

        /// <summary>
        ///     编组设置
        /// </summary>
        WiBtnBzDiagnoseInfo = 708,

        /// <summary>
        ///     制动状态
        /// </summary>
        WiBtnBrakeStatus = 710101,

        /// <summary>
        ///     版本信息
        /// </summary>
        WiBtnVersionInfo = 710103,

        /// <summary>
        ///     故障指示灯
        /// </summary>
        WiBtnFaultLamp = 710104,

        /// <summary>
        ///     IO
        /// </summary>
        WiBtnIo = 710105,

        /// <summary>
        ///     无线界面
        ///     编组
        /// </summary>
        WiBtnMarshalling = 71002,

        /// <summary>
        ///     无线界面
        ///     空白
        /// </summary>
        WiBtnBlankThree = 71003,

        /// <summary>
        ///     无线界面
        ///     解除编组
        /// </summary>
        WiBtnRemoveMarshalling = 71004,

        /// <summary>
        ///     无线界面
        ///     空白
        /// </summary>
        WiBtnBlankFive = 71005,

        /// <summary>
        ///     无线界面
        ///     编组设置
        /// </summary>
        WiBtnSetMarshalling = 71006,

        /// <summary>
        ///     无线界面
        ///     改端
        /// </summary>
        WiBtnChangeTerminal = 71007,

        /// <summary>
        ///     无线界面
        ///     编组诊断
        /// </summary>
        WiBtnDiagnoseMarshalling = 71008,

        /// <summary>
        ///     无线界面
        ///     空白
        /// </summary>
        WiBtnBlankEight = 71009,

        /// <summary>
        ///     无线界面
        ///     返回
        /// </summary>
        WiBtnBack = 71010,

        /// <summary>
        ///     向上翻页按钮
        ///     历史界面
        /// </summary>
        HsUpBtn = 7001,

        /// <summary>
        ///     向上翻页按钮
        ///     历史界面
        /// </summary>
        HsDownBtn = 7002,

        /// <summary>
        ///     维护
        /// </summary>
        Maintenance = 8,

        /// <summary>
        ///     列车信息
        /// </summary>
        MtTrainInfo = 801,

        /// <summary>
        ///     网络状态
        /// </summary>
        MtNetWorkStatus = 802,

        /// <summary>
        ///     软件版本
        /// </summary>
        MtSoftWareVersion = 803,

        /// <summary>
        ///     接口检查
        /// </summary>
        MtInterfaceCheck = 804,

        /// <summary>
        ///     输入按钮
        ///     接口检查界面
        /// </summary>
        MtIInputBtn = 8040,

        /// <summary>
        ///     输出按钮
        ///     接口检查界面
        /// </summary>
        MtIOutputBtn = 8041,

        /// <summary>
        ///     选择按钮
        ///     接口检查界面
        /// </summary>
        MtISelectBtn = 8042,

        /// <summary>
        ///     页面向上按钮
        ///     接口检查界面
        /// </summary>
        MtIPageBtnUp = 8043,

        /// <summary>
        ///     页面向下按钮
        ///     接口检查界面
        /// </summary>
        MtIPageBtnDown = 8044,

        /// <summary>
        ///     能耗信息
        /// </summary>
        MtEnergyConsumptionInfo = 805,

        /// <summary>
        ///     加速度测试
        /// </summary>
        MtAccelerationTest = 806,

        /// <summary>
        ///     加速度测试
        ///     初始化按钮
        /// </summary>
        MtAtInitalBtn = 8060,

        /// <summary>
        ///     加速度测试
        ///     20按钮
        /// </summary>
        MtAtTwentyBtn = 8061,

        /// <summary>
        ///     加速度测试
        ///     40按钮
        /// </summary>
        MtAtFortyBtn = 8062,

        /// <summary>
        ///     加速度测试
        ///     60按钮
        /// </summary>
        MtAtSixtyBtn = 8063,

        /// <summary>
        ///     加速度测试
        ///     80按钮
        /// </summary>
        MtAtEghityBtn = 8064,

        /// <summary>
        ///     制动自检
        /// </summary>
        MtBrakeSelfChecking = 807,

        /// <summary>
        ///     自检按钮
        ///     制动自检界面
        /// </summary>
        MtBscCheckBtn = 8070,

        /// <summary>
        ///     密码设置
        /// </summary>
        MtPasswordSetting = 808,

        /// <summary>
        ///     确认按钮
        ///     密码设置界面
        /// </summary>
        MtPdsCConfirmBtn = 80800,

        /// <summary>
        ///     取消按钮
        ///     密码设置界面
        /// </summary>
        MtPdsCCancelBtn = 808001,

        /// <summary>
        ///     7
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitSeven = 808002,

        /// <summary>
        ///     8
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitEight = 808003,

        /// <summary>
        ///     9
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitNine = 808004,

        /// <summary>
        ///     4
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitFour = 808005,

        /// <summary>
        ///     5
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitFive = 808006,

        /// <summary>
        ///     6
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitSix = 808007,

        /// <summary>
        ///     1
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitOne = 808008,

        /// <summary>
        ///     2
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitTwo = 808009,

        /// <summary>
        ///     3
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitThree = 808010,

        /// <summary>
        ///     0
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitZero = 808011,

        /// <summary>
        ///     返回
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitBack = 808012,

        /// <summary>
        ///     确认
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitConfirm = 808013,

        /// <summary>
        ///     取消
        ///     密码设置界面
        ///     数字键盘
        /// </summary>
        MtPdsDigitCancel = 808014,

        /// <summary>
        ///     亮度调节
        /// </summary>
        MtAdjustBrightness = 809,

        /// <summary>
        ///     亮度调节
        ///     亮度减少
        /// </summary>
        MtAbReduceBtn = 8090,

        /// <summary>
        ///     亮度调节
        ///     亮度增加
        /// </summary>
        MtAbAddBtn = 8091,

        /// <summary>
        ///     维护功能按钮
        ///     返回按钮
        /// </summary>
        MtCommonBackBtn = 8000
    }
}