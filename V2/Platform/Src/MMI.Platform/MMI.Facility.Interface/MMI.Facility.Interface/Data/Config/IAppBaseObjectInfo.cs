namespace MMI.Facility.Interface.Data.Config
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAppBaseObjectInfo
    {

        /// <summary>
        /// 引用的类名
        /// </summary>
        string ClassName { get; set; }

        /// <summary>
        /// 从属视图信息
        /// </summary>
        string ViewInfo { get; set; }

        /// <summary>
        /// 输入bool对应信息
        /// </summary>
        string InputBoolIndexs { get; set; }

        /// <summary>
        /// 输出bool对应信息
        /// </summary>
        string OutputBoolIndexs { get; set; }

        /// <summary>
        /// 输入float对应信息
        /// </summary>
        string InputFloatIndexs { get; set; }

        /// <summary>
        /// 输出float对应信息
        /// </summary>
        string OutputFloatIndexs { get; set; }

        /// <summary>
        /// 参数对应信息
        /// </summary>
        string ParaIndexs { get; set; }


        /// <summary>
        /// 
        /// </summary>
        int MainIndex { get; set; }

    }
}
