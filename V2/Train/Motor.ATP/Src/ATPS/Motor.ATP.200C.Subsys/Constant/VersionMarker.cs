using System;

namespace Motor.ATP._200C.Subsys.Constant
{
    public static class VersionMarker
    {
        public static readonly Version V0 = new Version();

        /// <summary>
        /// V1 点击 数据->司机号/车次号  后弹出一个取消的对话框。
        /// </summary>
        public static readonly Version V1 = Version.Parse("1.0");

    }
}