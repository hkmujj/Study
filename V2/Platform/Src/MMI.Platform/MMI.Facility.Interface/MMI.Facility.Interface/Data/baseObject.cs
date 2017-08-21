using System;
using MMI.Facility.Interface.Data.Config;

// ReSharper disable All

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 图元对象数据信息
    /// 该对象用于存储原始数据
    /// </summary>
    [Serializable]
    public class baseObjectInfo : IAppBaseObjectInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public baseObjectInfo()
        {
            OutputBoolIndexs = string.Empty;
            InputBoolIndexs = string.Empty;
            MainIndex = 0;
            ParaIndexs = string.Empty;
            InputFloatIndexs = string.Empty;
            OutputFloatIndexs = string.Empty;
            ViewInfo = string.Empty;
            ClassName = string.Empty;
        }

        /// <summary>
        /// 引用的类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 从属视图信息
        /// </summary>
        public string ViewInfo { get; set; }

        /// <summary>
        /// 输入bool对应信息
        /// </summary>
        public string InputBoolIndexs { get; set; }

        /// <summary>
        /// 输出bool对应信息
        /// </summary>
        public string OutputBoolIndexs { get; set; }

        /// <summary>
        /// 输入float对应信息
        /// </summary>
        public string InputFloatIndexs { get; set; }

        /// <summary>
        /// 输出float对应信息
        /// </summary>
        public string OutputFloatIndexs { get; set; }

        /// <summary>
        /// 参数对应信息
        /// </summary>
        public string ParaIndexs { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int MainIndex { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ObjectKeyWord
    {
        /// <summary>
        /// UI名称
        /// </summary>
        objectName,

        /// <summary>
        /// 视图
        /// </summary>
        form,

        /// <summary>
        /// 输入bool
        /// </summary>
        inBool,

        /// <summary>
        /// 输入float
        /// </summary>
        inFloat,

        /// <summary>
        /// 输出bool
        /// </summary>
        outBool,

        /// <summary>
        /// 输出float
        /// </summary>
        outFloat,

        /// <summary>
        /// 参数队列
        /// </summary>
        para,
        
        /// <summary>
        /// 编号
        /// </summary>
        index,
    }
   
}
