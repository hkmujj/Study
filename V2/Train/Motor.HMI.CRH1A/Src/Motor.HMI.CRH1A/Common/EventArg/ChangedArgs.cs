using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH1A.Common.EventArg
{
    /// <summary>
    /// 变化参数的模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ChangedArgs<T>
    {
        [DebuggerStepThrough]
        public ChangedArgs(T oldValue, T newValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }

        /// <summary>
        /// 老的值
        /// </summary>
        public T OldValue { private set; get; }

        /// <summary>
        /// 新的值
        /// </summary>
        public T NewValue { private set; get; }

    }
}
