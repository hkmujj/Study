using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.黑屏;

namespace Motor.HMI.CRH1A.Common.Config
{
    /// <summary>
    /// 视图配置
    /// </summary>
    public enum ViewConfig
    {
        // TODO  ADD
        /// <summary>
        /// 主界面 
        /// </summary>
        MainView = 1,

        /// <summary>
        /// 登录界面
        /// </summary>
        Login = 2,

        /// <summary>
        /// 运行
        /// </summary>
        Running = 3,

        /// <summary>
        /// 车站
        /// </summary>
        Station = 5,

        /// <summary>
        /// 系统/ 舒适度
        /// </summary>
        SystemComfort = 7,

        /// <summary>
        /// 系统/ 污物
        /// </summary>
        SystemSanitary = 8,

        /// <summary>
        /// 系统/ 门
        /// </summary>
        SystemDoor = 9,

        /// <summary>
        /// 系统/ 轴温
        /// </summary>
        SystemRoller = 10,


        /// <summary>
        /// 系统/ 火灾
        /// </summary>
        SystemFire = 11,


        /// <summary>
        /// 系统/ 前端
        /// </summary>
        SystemFont = 12,

        /// <summary>
        /// 系统/ 制动
        /// </summary>
        SystemBrake = 13,

        /// <summary>
        /// 系统/  高压
        /// </summary>
        SystemHightVol = 14,

        /// <summary>
        /// 系统/   电源供电 
        /// </summary>
        SystemPowerSupply = 15,

        /// <summary>
        /// 系统/  供风
        /// </summary>
        SystemAirSupply = 16,

        /// <summary>
        /// 系统/  牵引
        /// </summary>
        SystemTow = 17,

        /// <summary>
        /// 报警记录
        /// </summary>
        AlarmRecord = 23,

        /// <summary>
        /// 报警报告
        /// </summary>
        AlarmReport = 24,

        /// <summary>
        /// 报警总况
        /// </summary>
        AlarmAll = 25,

        /// <summary>
        /// 测试/ 制动测试
        /// </summary>
        TestBrakeTest=30,

        /// <summary>
        /// 测试/ 驾驶控制
        /// </summary>
        TestDrivControl=31,

        /// <summary>
        /// 测试/ 灯测试
        /// </summary>
        TestLightTest = 32,

        /// <summary>
        /// 黑屏
        /// </summary>
        BlackScreen = 34,

        /// <summary>
        /// 弹出性的故障视图
        /// </summary>
        FaultPopup = 42,


        /// <summary>
        /// 启动界面
        /// </summary>
        Start = 43,
    }
}
