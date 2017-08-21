using System;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommunicationDataReadService : IDisposable, IService
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, bool> ReadOnlyBoolDictionary { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, float> ReadOnlyFloatDictionary { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, bool> ReadOnlyBoolOldDictionary { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyDictionary<int, float> ReadOnlyFloatOldDictionary { get; }

        ///// <summary>
        ///// 模拟量发生变化
        ///// </summary>
        //event EventHandler<CommunicationDataChangedArgs<float>> FloatChanged;

        ///// <summary>
        ///// 数字量发生变化
        ///// </summary>
        //event EventHandler<CommunicationDataChangedArgs<bool>> BoolChanged;

        ///// <summary>
        ///// 数据发生变化
        ///// </summary>
        //event EventHandler<CommunicationDataChangedArgs> DataChanged;

        /// <summary>
        /// 
        /// </summary>
        bool GetBoolAt(int index);

        /// <summary>
        /// 
        /// </summary>
        float GetFloatAt(int index);

        /// <summary>
        /// 引起所有的数据变化事件
        /// </summary>
        void RaiseAllDataChanged();
    }
}
