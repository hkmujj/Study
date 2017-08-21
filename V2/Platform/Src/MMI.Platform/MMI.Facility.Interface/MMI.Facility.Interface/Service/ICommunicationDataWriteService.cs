using System;
using System.Collections.Generic;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommunicationDataWriteService : IDisposable, IService
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
        /// 改变bool 值
        /// </summary>
        /// <param name="changedBools"></param>
        /// <param name="notifyDataChangedEvent"></param>
        bool ChangeBools(IDictionary<int, bool> changedBools, bool notifyDataChangedEvent = false);

        /// <summary>
        /// 改变bool 值
        /// </summary>
        /// <param name="changedBools"></param>
        /// <param name="changed"></param>
        bool ChangeBools(IDictionary<int, bool> changedBools, out CommunicationDataChangedArgs<bool> changed);

        /// <summary>
        /// 改变bool 值
        /// </summary>
        /// <param name="changedBools"></param>
        /// <param name="startIndex"></param>
        bool ChangeBools(List<bool> changedBools, int startIndex);

        /// <summary>
        /// 改变bool 值
        /// </summary>
        /// <param name="changedBools"></param>
        /// <param name="startIndex"></param>
        /// <param name="changed"></param>
        bool ChangeBools(List<bool> changedBools, int startIndex, out CommunicationDataChangedArgs<bool> changed);

        /// <summary>
        /// 改变bool 值
        /// </summary>
        bool ChangeBool(int index, bool value, bool notifyDataChangedEvent = false);

        /// <summary>
        /// 改变bool 值
        /// </summary>
        bool ChangeBool(int index, bool value, out CommunicationDataChangedArgs<bool> changed);

        /// <summary>
        /// 改变 float 值
        /// </summary>
        /// <param name="changedFloats"></param>
        bool ChangeFloats(IDictionary<int, float> changedFloats, bool notifyDataChangedEvent = false);

        /// <summary>
        /// 改变 float 值
        /// </summary>
        /// <param name="changedFloats"></param>
        /// <param name="changed"></param>
        bool ChangeFloats(IDictionary<int, float> changedFloats, out CommunicationDataChangedArgs<float> changed);

        /// <summary>
        /// 改变 float 值
        /// </summary>
        /// <param name="changedFloats"></param>
        /// <param name="startIndex"></param>
        bool ChangeFloats(List<float> changedFloats, int startIndex);

        /// <summary>
        /// 改变 float 值
        /// </summary>
        /// <param name="changedFloats"></param>
        /// <param name="startIndex"></param>
        /// <param name="changed"></param>
        bool ChangeFloats(List<float> changedFloats, int startIndex, out  CommunicationDataChangedArgs<float> changed);

        /// <summary>
        /// 改变 float 值
        /// </summary>
        bool ChangeFloat(int index, float value, bool notifyDataChangedEvent = false);

        /// <summary>
        /// 改变 float 值
        /// </summary>
        bool ChangeFloat(int index, float value, out CommunicationDataChangedArgs<float> changed);
    }
}
