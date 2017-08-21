using LightRail.HMI.SZLHLF.View.Buttons;
using LightRail.HMI.SZLHLF.View.Contents;
using LightRail.HMI.SZLHLF.View.Common;
using LightRail.HMI.SZLHLF.View.MaintainView;

namespace LightRail.HMI.SZLHLF.Constant
{
    /// <summary>
    /// 试图名称
    /// </summary>
    public class ViewNames
    {
        /// <summary>
        /// 运行界面
        /// </summary>
        public static readonly string OperationView = typeof (RootContentView).FullName;

        /// <summary>
        /// 按钮界面
        /// </summary>
        public static readonly string MainBottomButton = typeof (MainBottomButton).FullName;

        /// <summary>
        /// 空调界面
        /// </summary>
        public static readonly string AirConditionView = typeof (AirConditionView).FullName;

        /// <summary>
        /// 设置界面
        /// </summary>
        public static readonly string SettingView = typeof (SettingView).FullName;

        /// <summary>
        /// 帮助信息界面
        /// </summary>
        public static readonly string HelpView = typeof (HelpView).FullName;

        /// <summary>
        /// 故障信息界面
        /// </summary>
        public static readonly string MalfunctionInfoView = typeof (MalfunctionInfoView).FullName;

        /// <summary>
        /// 维护界面
        /// </summary>
        public static readonly string MaintainView = typeof (MaintainView).FullName;

        /// <summary>
        /// 软件版本界面
        /// </summary>
        public static readonly string SoftwareVersionsView = typeof(SoftwareVersionsView).FullName;

        /// <summary>
        /// 参数设置界面
        /// </summary>
        public static readonly string ParameterSettingView = typeof (ParameterSettingView).FullName;

        /// <summary>
        /// 测试界面
        /// </summary>
        public static readonly string TextView = typeof (TestView).FullName;

        /// <summary>
        /// 数据界面
        /// </summary>
        public static readonly string DataView = typeof (DataView).FullName;

        /// <summary>
        /// 网络拓扑界面
        /// </summary>
        public static readonly string NetworkTopologyView = typeof (NetworkTopologyView).FullName;

        /// <summary>
        /// 紧急广播界面
        /// </summary>
        public static readonly string EmergencyBroadcastView = typeof (EmergencyBroadcastView).FullName;

        /// <summary>
        /// 广播控制界面
        /// </summary>
        public static readonly string BroadcastControlView = typeof (BroadcastControlView).FullName;

        /// <summary>
        /// 语言改变
        /// </summary>
        public static readonly string LanguageChangeButton = typeof (LanguageChangeButton).FullName;

        /// <summary>
        /// 站点设置界面
        /// </summary>
        public static readonly string StationSetView = typeof(StationSetView).FullName;

        /// <summary>
        /// 维护登陆界面
        /// </summary>
        public static readonly string LoginView = typeof(LoginView).FullName;
    }
}
