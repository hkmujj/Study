using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Motor.HMI.CRH1A.Common.Train
{
    /// <summary>
    /// CommonTrain 的选中变化时传递的参数
    /// </summary>
    public class TrainSelectedChangedArgs : EventArgs
    {
        [DebuggerStepThrough]
        public TrainSelectedChangedArgs(ChangedFrom from, ReadOnlyCollection<bool> oldSelected, ReadOnlyCollection<bool> newSelected)
        {
            NewSelected = newSelected;
            From = from;
            OldSelected = oldSelected;
        }

        /// <summary>
        /// 老的选中状态
        /// </summary>
        public ReadOnlyCollection<bool> OldSelected { get; private set; }

        /// <summary>
        /// 新的选中状态
        /// </summary>
        public ReadOnlyCollection<bool> NewSelected { private set; get; }

        /// <summary>
        /// 事件来源
        /// </summary>
        public ChangedFrom From { private set; get; }

        public enum ChangedFrom
        {
            /// <summary>
            /// 手动, 即用户设置的
            /// </summary>
            Manaul,
             
            /// <summary>
            /// 界面点击
            /// </summary>
            UiClick,
        }

    }
}
