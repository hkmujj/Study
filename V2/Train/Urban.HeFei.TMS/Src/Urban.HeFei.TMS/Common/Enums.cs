namespace Urban.HeFei.TMS.Common
{
    /// <summary>
    /// 功能描述：视图状态枚举
    /// </summary>
    public enum ViewState
    {
        /// <summary>
        /// 黑屏
        /// </summary>
        BlackScreen = 0,

        /// <summary>
        /// 主界面
        /// </summary>
        MainInterface = 1,

        /// <summary>
        /// 旁路
        /// </summary>
        BypassInfo = 101,

        /// <summary>
        /// 旁路界面2
        /// </summary>
        BypassInfo2 = 102,


        /// <summary>
        /// 故障
        /// </summary>
        FaultInfo = 111,

        /// <summary>
        /// 故障--实时故障信息
        /// </summary>
        FaultInfoRealTime = 112,

        /// <summary>
        /// 故障
        /// 换页向上按钮
        /// </summary>
        FiUpBtn = 1110,

        /// <summary>
        /// 故障
        /// 换页向下按钮
        /// </summary>
        FiDownBtn = 1111,

        /// <summary>
        /// 故障
        /// 故障页面返回按钮
        /// </summary>
        FiReturnArrow = 1112,

        /// <summary>
        /// 故障--实时故障信息
        /// 确认按钮
        /// </summary>
        FiRtConfirmBtn = 1120,

        /// <summary>
        /// 牵引
        /// </summary>
        Traction = 2,

        /// <summary>
        /// 制动机页面1
        /// </summary>
        BrakePage1 = 3,

        /// <summary>
        /// 制动机页面2
        /// </summary>
        BrakePage2 = 301,

        /// <summary>
        /// 制动机页面3
        /// </summary>
        BrakePage3 = 302,

        /// <summary>
        /// 辅助
        /// </summary>
        Assist = 4,

        /// <summary>
        /// 辅助页面2
        /// </summary>
        AssistPage2 = 401,

        /// <summary>
        /// 空调
        /// </summary>
        AirConditioner = 5,

        /// <summary>
        /// 空调界面2
        /// </summary>
        AirConditionerPage2 = 501,

        /// <summary>
        /// 空调界面1
        /// 自动
        /// </summary>
        AirConditionP1Auto = 5010,

        /// <summary>
        /// 空调界面1
        /// 手动
        /// </summary>
        AirConditionP1Manual = 5011,

        /// <summary>
        /// 空调界面1
        /// 通风
        /// </summary>
        AirConditionP1Air = 5012,

        /// <summary>
        /// 空调界面1
        /// 停止
        /// </summary>
        AirConditionP1Stop = 5013,

        /// <summary>
        /// 空调界面2
        /// 关闭按钮
        /// </summary>
        AirConditionP2NewAirClose = 5015,
        /// <summary>
        /// 空调界面1
        /// 仅TC车按钮
        /// </summary>
        AirConditionOnlyTc = 5014,
        ///// <summary>
        ///// PIS,乘客信息
        ///// </summary>
        //PIS = 6,

        /// <summary>
        /// PIS-自动模式
        /// </summary>
        PISAutoModel = 6,

        /// <summary>
        /// PIS-半自动模式
        /// </summary>
        PISSemiAutoModel = 602,

        /// <summary>
        /// PIS-手动模式 manual
        /// </summary>
        PISManualModel = 603,

        /// <summary>
        /// PIS-旅程设置route
        /// </summary>
        PISRouteSet = 604,

        /// <summary>
        /// PIS-特殊信息
        /// </summary>
        PISSpecialInfo = 605,

        /// <summary>
        /// PIS选择按钮-下一站
        /// 手动模式界面
        /// </summary>
        PismSelectNextStation = 6030,

        /// <summary>
        /// PIS选择按钮-终点站
        /// 手动模式界面
        /// </summary>
        PismSelectEndStation = 6031,

        /// <summary>
        /// PIS发车按钮
        /// 手动模式界面
        /// </summary>
        PismDepartBtn = 6032,

        /// <summary>
        /// PIS下一站按钮
        /// 手动模式界面
        /// </summary>
        PismNextStationBtn = 6033,

        /// <summary>
        /// PIS到达站按钮
        /// 手动模式界面
        /// </summary>
        PismArriveStationBtn = 6034,

        /// <summary>
        /// PIS 选择按钮-下一站
        /// 旅程设置界面
        /// </summary>
        PisrSelectNextStation = 6040,

        /// <summary>
        /// PIS选择按钮-终点站
        /// 旅程设置界面
        /// </summary>
        PisrSelectEndStation = 6041,

        /// <summary>
        /// PIS-修改按钮
        /// 旅程设置界面
        /// </summary>
        PisrModifyBtn = 6042,

        /// <summary>
        /// PIS-确认按钮
        /// 旅程设置界面
        /// </summary>
        PisrConfrimBtn = 6043,

        /// <summary>
        /// PIS-取消按钮
        /// 旅程设置界面
        /// </summary>
        PisrCancelBtn = 6044,

        /// <summary>
        /// PIS-单次播放
        /// 特殊信息界面
        /// </summary>
        PissSingleBtn = 6050,

        /// <summary>
        /// PIS-循环播放
        /// 特殊信息界面
        /// </summary>
        PissCycleBtn = 6051,

        /// <summary>
        /// PIS-向上翻页按钮
        /// 特殊信息界面
        /// </summary>
        PissUpBtn = 6052,

        /// <summary>
        ///  PIS-向下翻页按钮
        ///  特殊信息界面
        /// </summary>
        PissDownBtn = 6053,

        /// <summary>
        /// PIS-返回箭头按钮
        /// 特殊信息界面
        /// </summary>
        PissReturnArrow = 6054,

        /// <summary>
        /// 历史
        /// </summary>
        History = 7,

        /// <summary>
        /// 向上翻页按钮
        /// 历史界面
        /// </summary>
        HsUpBtn = 7001,

        /// <summary>
        /// 向上翻页按钮
        /// 历史界面
        /// </summary>
        HsDownBtn = 7002,

        /// <summary>
        /// 维护
        /// </summary>
        Maintenance = 8,

        /// <summary>
        /// 列车信息
        /// </summary>
        MtTrainInfo = 801,

        /// <summary>
        /// 网络状态
        /// </summary>
        MtNetWorkStatus = 802,

        /// <summary>
        /// 软件版本
        /// </summary>
        MtSoftWareVersion = 803,

        /// <summary>
        /// 接口检查
        /// </summary>
        MtInterfaceCheck = 804,

        /// <summary>
        /// 输入按钮
        /// 接口检查界面
        /// </summary>
        MtIInputBtn = 8040,

        /// <summary>
        /// 输出按钮
        /// 接口检查界面
        /// </summary>
        MtIOutputBtn = 8041,

        /// <summary>
        /// 选择按钮
        /// 接口检查界面
        /// </summary>
        MtISelectBtn = 8042,

        /// <summary>
        /// 页面向上按钮
        /// 接口检查界面
        /// </summary>
        MtIPageBtnUp = 8043,

        /// <summary>
        /// 页面向下按钮
        /// 接口检查界面
        /// </summary>
        MtIPageBtnDown = 8044,

        /// <summary>
        /// 能耗信息
        /// </summary>
        MtEnergyConsumptionInfo = 805,

        /// <summary>
        /// 加速度测试
        /// </summary>
        MtAccelerationTest = 806,

        /// <summary>
        /// 加速度测试
        /// 初始化按钮
        /// </summary>
        MtAtInitalBtn = 8060,

        /// <summary>
        /// 加速度测试
        /// 20按钮
        /// </summary>
        MtAtTwentyBtn = 8061,

        /// <summary>
        /// 加速度测试
        /// 40按钮
        /// </summary>
        MtAtFortyBtn = 8062,

        /// <summary>
        /// 加速度测试
        /// 60按钮
        /// </summary>
        MtAtSixtyBtn = 8063,

        /// <summary>
        /// 加速度测试
        /// 80按钮
        /// </summary>
        MtAtEghityBtn = 8064,

        /// <summary>
        /// 制动自检
        /// </summary>
        MtBrakeSelfChecking = 807,

        /// <summary>
        /// 自检按钮
        /// 制动自检界面
        /// </summary>
        MtBscCheckBtn = 8070,

        /// <summary>
        /// 密码设置
        /// </summary>
        MtPasswordSetting = 808,

        /// <summary>
        /// 确认按钮
        /// 密码设置界面
        /// </summary>
        MtPdsCConfirmBtn = 80800,

        /// <summary>
        /// 取消按钮
        /// 密码设置界面
        /// </summary>
        MtPdsCCancelBtn = 808001,

        /// <summary>
        /// 7
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitSeven = 808002,

        /// <summary>
        /// 8
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitEight = 808003,

        /// <summary>
        /// 9
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitNine = 808004,

        /// <summary>
        /// 4
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitFour = 808005,

        /// <summary>
        /// 5
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitFive = 808006,

        /// <summary>
        /// 6
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitSix = 808007,
        /// <summary>
        /// 1
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitOne = 808008,

        /// <summary>
        /// 2
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitTwo = 808009,

        /// <summary>
        /// 3
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitThree = 808010,
        /// <summary>
        /// 0
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitZero = 808011,
        /// <summary>
        /// 返回
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitBack = 808012,
        /// <summary>
        /// 确认
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitConfirm = 808013,
        /// <summary>
        /// 取消
        /// 密码设置界面
        /// 数字键盘
        /// </summary>
        MtPdsDigitCancel = 808014,

        /// <summary>
        /// 亮度调节
        /// </summary>
        MtAdjustBrightness = 809,

        /// <summary>
        /// 亮度调节
        /// 亮度减少
        /// </summary>
        MtAbReduceBtn = 8090,

        /// <summary>
        /// 亮度调节
        /// 亮度增加
        /// </summary>
        MtAbAddBtn = 8091,

        /// <summary>
        /// 维护功能按钮
        /// 返回按钮
        /// </summary>
        MtCommonBackBtn = 8000,
        /// <summary>
        /// 密码输入界面
        /// </summary>
        PawsswordSetView = 810,
        /// <summary>
        /// 系统复位 811
        /// </summary>
        SystemReset = 811,

    }
}
