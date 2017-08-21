using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public enum RaiseCommunicationDataChangedType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unkown,

        /// <summary>
        /// 用户手动调用方法触 发
        /// </summary>
        ByUserManul,

        /// <summary>
        /// 网络数据接收
        /// </summary>
        ByNetDataRevc,

        ///// <summary>
        ///// 用户通过调试界面点击
        ///// </summary>
        //ByUserDebug,
    }

    /// <summary>
    /// 数据变化事件参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommunicationDataChangedArgs<T> : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public new static readonly CommunicationDataChangedArgs<T> Empty = new CommunicationDataChangedArgs<T>();



        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValueValue"></param>
        /// <param name="raiseDataChangedType"></param>
        [DebuggerStepThrough]
        public CommunicationDataChangedArgs(IDictionary<int, T> oldValue, IDictionary<int, T> newValueValue,
            RaiseCommunicationDataChangedType raiseDataChangedType = RaiseCommunicationDataChangedType.Unkown)
        {
            NewValue = newValueValue;
            RaiseDataChangedType = raiseDataChangedType;
            OldValue = oldValue;
        }

        /// <summary>
        /// 
        /// </summary>
        [DebuggerStepThrough]
        public CommunicationDataChangedArgs(
            RaiseCommunicationDataChangedType raiseDataChangedType = RaiseCommunicationDataChangedType.ByNetDataRevc)
            : this(new Dictionary<int, T>(), new Dictionary<int, T>(), raiseDataChangedType)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public RaiseCommunicationDataChangedType RaiseDataChangedType { set; get; }

        /// <summary>
        /// 新值
        /// </summary>
        public IDictionary<int, T> NewValue { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<int, T> OldValue { private set; get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CommunicationDataChangedArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changedBools"></param>
        /// <param name="changedFloats"></param>
        /// <param name="raiseDataChangedType"></param>
        [DebuggerStepThrough]
        public CommunicationDataChangedArgs(CommunicationDataChangedArgs<bool> changedBools,
            CommunicationDataChangedArgs<float> changedFloats,
            RaiseCommunicationDataChangedType raiseDataChangedType = RaiseCommunicationDataChangedType.ByNetDataRevc)
        {
            ChangedBools = changedBools;
            ChangedFloats = changedFloats;
            RaiseDataChangedType = raiseDataChangedType;
        }

        /// <summary>
        /// 
        /// </summary>
        public RaiseCommunicationDataChangedType RaiseDataChangedType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public CommunicationDataChangedArgs<bool> ChangedBools { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public CommunicationDataChangedArgs<float> ChangedFloats { private set; get; }
    }
}
