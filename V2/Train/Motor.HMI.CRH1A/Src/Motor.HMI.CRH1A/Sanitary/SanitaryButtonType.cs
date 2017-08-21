using System.ComponentModel;

namespace Motor.HMI.CRH1A.Sanitary
{
    enum SanitaryButtonType
    {
        /// <summary>
        /// 排空单个水箱
        /// </summary>
        [Description("排空水箱")]
        ClearSigle,

        /// <summary>
        /// 排空所有水箱
        /// </summary>
        [Description("排空所有水箱")]
        ClearAll,
    }
}