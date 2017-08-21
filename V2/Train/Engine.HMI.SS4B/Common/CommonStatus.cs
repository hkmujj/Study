using System.Collections.Generic;

namespace SS4B_TMS.Common
{
    /// <summary>
    /// </summary>
    public class CommonStatus
    {
        /// <summary>
        ///     记录当前所在屏ID
        /// </summary>
        public static ViewState CurrentViewState = ViewState.BlackScreen;

        /// <summary>
        ///     记录当前界面名称
        /// </summary>
        public static string CurrentInterfaceName = "";

        /// <summary>
        ///     所有界面名称
        /// </summary>
        public static Dictionary<ViewState, string> InterfaceNameDicts = new Dictionary<ViewState, string>();

        /// <summary>
        ///     广播界面应该返回的页面，即记录按下广播按钮所在的界面
        /// </summary>
        public static ViewState BroadcastReturnViewState = ViewState.PISAutoModel;

        /// <summary>
        ///     故障界面应该返回的页面，即记录按下故障按钮所在的界面
        /// </summary>
        public static ViewState FaultReturnViewState = ViewState.MainInterface;

        /// <summary>
        ///     故障实时界面应该返回的页面
        /// </summary>
        public static ViewState FaultRtReturnViewState = ViewState.MainInterface;

        /// <summary>
        ///     PIS报站模式:自动模式
        /// </summary>
        public static int PISAutoModelFlag = 0;

        /// <summary>
        ///     PIS报站模式:半自动
        /// </summary>
        public static int PISSemiModelFlag = 0;

        /// <summary>
        ///     PIS报站模式:手动
        /// </summary>
        public static int PISManualModelFlag = 0;

        /// <summary>
        ///     向外发送bool数据时计数开始位
        /// </summary>
        public static readonly int SdBoolBaseNumber = 4800;

        /// <summary>
        ///     向外发送float数据时计数开始位
        /// </summary>
        public static readonly int SdFloatBaseNumber = 600;

        /// <summary>
        ///     车站列表
        /// </summary>
        public static SortedDictionary<int, string> StationList = new SortedDictionary<int, string>(); //车站列表

        /// <summary>
        ///     是否是黑屏
        /// </summary>
        public static bool IsBlackScreen = false;

        /// <summary>
        ///     初始化
        /// </summary>
        static CommonStatus()
        {
            InterfaceNameDicts.Add(ViewState.BlackScreen, "黑屏");

            InterfaceNameDicts.Add(ViewState.MainInterface, "主界面");

            InterfaceNameDicts.Add(ViewState.SecondInterface, "牵引");

            InterfaceNameDicts.Add(ViewState.BrakePage1, "制动");
            InterfaceNameDicts.Add(ViewState.BrakePage2, "制动");
            InterfaceNameDicts.Add(ViewState.BrakePage3, "制动");

            InterfaceNameDicts.Add(ViewState.Assist, "辅助");
            InterfaceNameDicts.Add(ViewState.AssistPage2, "辅助");
            InterfaceNameDicts.Add(ViewState.AirConditioner, "空调");
            InterfaceNameDicts.Add(ViewState.AirConditionerPage2, "空调");

            //InterfaceNameDicts.Add(ViewState.PIS, "乘客信息");
            InterfaceNameDicts.Add(ViewState.PISAutoModel, "乘客信息");
            InterfaceNameDicts.Add(ViewState.PISSemiAutoModel, "乘客信息");
            InterfaceNameDicts.Add(ViewState.PISManualModel, "乘客信息");
            InterfaceNameDicts.Add(ViewState.PISSpecialInfo, "特殊信息");

            InterfaceNameDicts.Add(ViewState.FaultInfo, "故障");
            InterfaceNameDicts.Add(ViewState.FaultInfoRealTime, "事件警告");

            InterfaceNameDicts.Add(ViewState.BypassInfo, "旁路");
            InterfaceNameDicts.Add(ViewState.BypassInfo2, "旁路");

            InterfaceNameDicts.Add(ViewState.WirelessInterface, "历史");

            InterfaceNameDicts.Add(ViewState.Maintenance, "维护");
            InterfaceNameDicts.Add(ViewState.MtTrainInfo, "列车信息");
            InterfaceNameDicts.Add(ViewState.MtNetWorkStatus, "网络状态");
            InterfaceNameDicts.Add(ViewState.MtSoftWareVersion, "软件版本");
            InterfaceNameDicts.Add(ViewState.MtInterfaceCheck, "接口检查");
            InterfaceNameDicts.Add(ViewState.MtEnergyConsumptionInfo, "能耗信息");
            InterfaceNameDicts.Add(ViewState.MtAccelerationTest, "加速度测试");
            InterfaceNameDicts.Add(ViewState.MtBrakeSelfChecking, "制动自检");
            InterfaceNameDicts.Add(ViewState.MtPasswordSetting, "密码设置");
            InterfaceNameDicts.Add(ViewState.MtAdjustBrightness, "亮度设置");
        }
    }
}