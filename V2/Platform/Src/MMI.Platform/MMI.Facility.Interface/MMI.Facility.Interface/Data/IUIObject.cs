using System;
using System.Collections.Generic;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUIObject : ICloneable
    {

        /// <summary>
        /// 
        /// </summary>
        string ObjectKey { get; }

        /// <summary>
        /// 文件名
        /// </summary>
        string DllName { set; get; }

        /// <summary>
        /// 类名
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// 
        /// </summary>
        int MainIndex { get; }

        /// <summary>
        /// 视图列表
        /// </summary>
        List<int> ViewList { get; }

        /// <summary>
        /// 输入bool列表
        /// </summary>
        List<int> InBoolList { get; }

        /// <summary>
        /// 输出bool列表
        /// </summary>
        List<int> OutBoolList { get; }

        /// <summary>
        /// 输入float列表
        /// </summary>
        List<int> InFloatList { get; }

        /// <summary>
        /// 输出float列表
        /// </summary>
        List<int> OutFloatList { get; }

        /// <summary>
        /// 参数列表
        /// </summary>
        List<string> ParaList { get; }

    }
}
