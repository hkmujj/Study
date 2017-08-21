using System.ComponentModel;

namespace Motor.ATP._200H.ConfigModel
{
    public enum DisplayType
    {
        /// <summary>
        /// 1级常用制动显示一条横杠,4级显示2条,7级显示3条
        /// </summary>
        [Description("正常显示")]
        Normal,
        /// <summary>
        /// 只要制动就显示三条横杠
        /// </summary>
        [Description("只要制动就显示三条横杠")]
        Brake7
    }
}