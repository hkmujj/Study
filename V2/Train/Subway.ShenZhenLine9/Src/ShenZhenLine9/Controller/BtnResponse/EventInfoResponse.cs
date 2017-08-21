using Subway.ShenZhenLine9.Resource.Keys;

namespace Subway.ShenZhenLine9.Controller.BtnResponse
{
    /// <summary>
    /// 事件信息按钮响应
    /// </summary>
    public class EventInfoResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public override void MouseUp()
        {
            Response(StateKeys.事件信息);
        }
    }
    /// <summary>
    /// 设置按钮响应
    /// </summary>
    public class SettingResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public override void MouseUp()
        {
            Response(StateKeys.设置);
        }
    }
    /// <summary>
    /// 维护按钮响应
    /// </summary>
    public class MaininstanceResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public override void MouseUp()
        {
            Response(StateKeys.维护);
        }
    }
    /// <summary>
    /// 旁路按钮响应
    /// </summary>
    public class BypassResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public override void MouseUp()
        {
            Response(StateKeys.旁路信息);
        }
    }
    /// <summary>
    /// 牵引封锁按钮响应
    /// </summary>
    public class TractionLockResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public override void MouseUp()
        {
            Response(StateKeys.牵引封锁);
        }
    }
    /// <summary>
    /// 广播按钮响应
    /// </summary>
    public class BorderCastResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public override void MouseUp()
        {
            Response(StateKeys.广播);
        }
    }
    /// <summary>
    /// 广播按钮响应
    /// </summary>
    public class HelpResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public override void MouseUp()
        {
            Response(StateKeys.帮助);
        }
    }

    /// <summary>
    /// 主页按钮响应
    /// </summary>
    public class MasterResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮弹起
        /// </summary>
        public override void MouseUp()
        {
            Response(StateKeys.主页面);
        }
    }
}