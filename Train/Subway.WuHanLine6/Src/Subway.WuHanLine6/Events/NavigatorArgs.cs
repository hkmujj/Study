using System;

namespace Subway.WuHanLine6.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class NavigatorArgs : EventArgs
    {
        /// <summary>
        /// 当前视图Key
        /// </summary>
        public string Current { get; set; }
        /// <summary>
        /// 导航视图Key
        /// </summary>
        public string Navigator { get; set; }
        /// <summary>
        /// 下一视图Key
        /// </summary>
        public string NextNavigator { get; set; }
    }
}