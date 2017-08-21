using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motor.ATP._300D
{
    class NotifyConstant
    {
        /// <summary>
        /// 不需要确认的通知的起始索引 350
        /// </summary>
        public const int NormalNotifyStartIndex = 350;

        /// <summary>
        /// 不需要确认的通知的个数
        /// </summary>
        public const int NormalNotifyCount = ConfirmNotifyStartIndex - NormalNotifyStartIndex;

        /// <summary>
        /// 需要确认的通知起始索引 380
        /// </summary>
        public const int ConfirmNotifyStartIndex = 380;

        /// <summary>
        /// 需要确认的通知的个数 20
        /// </summary>
        public const int ConfirmNotifyCount = 20;

    }
}
