namespace Motor.HMI.CRH3C380B.Base.底层共用
{
    /// <summary>
    /// 视图id对应的对象名
    /// </summary>
    public enum ViewIDName
    {
        /// <summary>
        /// 黑屏视图
        /// </summary>
        DMIBlackScreen,

        /// <summary>
        /// 课程结束视图
        /// </summary>
        DMIClassOverScreen,

        /// <summary>
        /// 按钮
        /// </summary>
        DMIButton,

        /// <summary>
        /// 标题
        /// </summary>
        DMITitle,

        /// <summary>
        /// 信息盒
        /// </summary>
        DMIInfoBox,

        /// <summary>
        /// 故障纵览
        /// </summary>
        DMIFault,

        /// <summary>
        /// 基本页
        /// </summary>
        DMIBasePage,

        #region ::: 维修 :::

        /// <summary>
        /// DMI维修
        /// </summary>
        DMIMaintenance,

        /// <summary>
        /// 手动输入错误图页面
        /// </summary>
        DMIManualEntryOfFaults,

        /// <summary>
        /// DMI远程数据传输页面
        /// </summary>
        DMIRemoteDataTransfer,

        /// <summary>
        /// DMI中间直流环节
        /// </summary>
        DMITractionDcLink,

        /// <summary>
        /// 车辆的版本
        /// </summary>
        DMIVersionsCar,

        /// <summary>
        /// DMI运行速度设定检测
        /// </summary>
        DmivSetpointSensorValue,

        /// <summary>
        /// DMI牵引手柄检测
        /// </summary>
        DMITractiveEffortControllerSensorSetpoint,

        /// <summary>
        /// DMI能量计量
        /// </summary>
        DMIEnergyCounter,

        #endregion

        #region ::: 系统 :::

        /// <summary>
        /// DMI系统
        /// </summary>
        DMISystem,

        /// <summary>
        /// DMI列车配置显示
        /// </summary>
        DMITrainConfigurationDisplay,

        /// <summary>
        /// DMI蓄电池电压110V
        /// </summary>
        DMIBatteryVoltage110V,

        /// <summary>
        /// DMI轮轴温度12
        /// </summary>
        DMIWheelShaftT12,

        /// <summary>
        /// DMI轮轴温度34
        /// </summary>
        DMIwheelShaftT34,

        /// <summary>
        /// 解钩
        /// </summary>
        DMIDecoupling,

        /// <summary>
        /// 连挂
        /// </summary>
        DMICoupling,

        /// <summary>
        /// 关闭后车钩罩
        /// </summary>
        DMICloseCouplerHatches,

        #endregion

        /// <summary>
        /// 车门
        /// </summary>
        DMIDoors,

        #region ::: ATP系统 :::

        /// <summary>
        /// ATP系统
        /// </summary>
        DMIATPSystem,

        /// <summary>
        /// DMI更换显示
        /// </summary>
        DMIDMIReplace,

        /// <summary>
        /// EVC更换显示
        /// </summary>
        DMIEVCReplace,

        #endregion

        #region ::: 紧急 :::

        /// <summary>
        /// DMI紧急
        /// </summary>
        DMIEmergency,

        #endregion

        #region ::: 准备/停放 :::

        /// <summary>
        /// 准备/停放
        /// </summary>
        DMIPreparingStabling,

        /// <summary>
        /// 列车编号和YD编号页面
        /// </summary>
        DMITrainNoAndTDNo,

        /// <summary>
        /// 开始停放操作页面
        /// </summary>
        DMIStartStablingOperation,

        /// <summary>
        /// 停放结束页面
        /// </summary>
        DMIEndOfStabling,

        #endregion

        /// <summary>
        /// 传动/制动屏幕
        /// </summary>
        DMIDrivingBraking,

        /// <summary>
        /// 自动速度控制屏幕
        /// </summary>
        DMIASC,

        #region ::: 开关 :::

        /// <summary>
        /// DMI开关
        /// </summary>
        DMISwitching,

        /// <summary>
        /// DMI网侧电流限制
        /// </summary>
        DMILineCurrentLimit,

        /// <summary>
        /// DMI风扇
        /// </summary>
        DMIFans,

        /// <summary>
        /// DMI牵引
        /// </summary>
        DMITraction,

        /// <summary>
        /// 空调页面
        /// </summary>
        DMIAirCondition,

        /// <summary>
        /// DMI照明
        /// </summary>
        DMILighting,

        /// <summary>
        /// DMI冲洗运行
        /// </summary>
        DMIWashRun,

        /// <summary>
        /// DMI前窗加热
        /// </summary>
        DMIFrontWindowHeating,

        #endregion

        /// <summary>
        /// DMI制动状态
        /// </summary>
        DMIBrakeStatus,

        #region ::: 制动有效率 :::

        /// <summary>
        /// DMI制动力
        /// </summary>
        DMIBrakingPower,

        /// <summary>
        /// DMI制动图表
        /// </summary>
        DMIBrakeSheet,

        /// <summary>
        /// DMI最高限速表
        /// </summary>
        DMIVmaxTable,

        /// <summary>
        /// DMI上次试验结果
        /// </summary>
        DMIResultOfLastBrakeTest,

        #endregion

        /// <summary>
        /// DMI制动功能状态
        /// </summary>
        DMIStatusOfBrakeFunctions,

        #region ::: 制动试验 :::

        /// <summary>
        /// DMI制动试验
        /// </summary>
        DMIBrakeTest,

        /// <summary>
        /// DMI直接制动试验
        /// </summary>
        DMITestOfDirectBrake,

        /// <summary>
        /// DMI紧急制动试验页面
        /// </summary>
        DMIEmergencyBrakeTest,

        /// <summary>
        /// DMIMRP连续试验页面
        /// </summary>
        DMIMRPContinuityTest,

        /// <summary>
        /// DMIBP泄漏试验页面
        /// </summary>
        DMIBPLeakageTest,

        /// <summary>
        /// DMI间接制动试验页面
        /// </summary>
        DMITestOfIndirectBrake,

        /// <summary>
        /// DMIBP连续试验页面
        /// </summary>
        DMIBPContinuityTest,

        #endregion

        /// <summary>
        /// DMI停放制动
        /// </summary>
        DMIParkingBrakes,

        /// <summary>
        /// DMI总风管
        /// </summary>
        DMIMainReservoirPipe,


    }

    /// <summary>
    /// 字体名称
    /// </summary>
    public enum FontName
    {
        宋体,
        黑体,
    }

    /// <summary>
    /// 位置格式
    /// </summary>
    public enum FontRelated
    {
        居中,
        靠左,
        靠右,
        靠左上,
        靠中上,
        靠右上,
        靠左下,
        靠中下,
        靠右下
    }

    /// <summary>
    /// 数值条增长方向
    /// </summary>
    public enum RectRiseDirection
    {
        上,
        下,
        左,
        右,
    }

    public enum TextType
    {
        默认中文黑,
        默认英文黑,
        默认中文白,
        默认英文白,
    }
}