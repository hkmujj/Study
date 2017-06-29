namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 折返类型
    /// </summary>
    public enum ReturnState
    {
        /// <summary>
        /// 无显示 
        /// </summary>
        None,

        /// <summary>
        ///显示自动折返图标
        /// </summary>
        AutoReturn,

        /// <summary>
        /// 无人折返操作激活
        /// </summary>
        AutoReturnActived,

        /// <summary>
        /// 显示人工折返图标
        /// </summary>
        ManuanlReturn,

        /// <summary>
        /// 人工折返图标激活
        /// </summary>
        ManuanlReturnActived,

        /// <summary>
        /// 显示自动、人工折返图标
        /// </summary>
        ManuanlOrAutoReturn,

        /// <summary>
        /// 人工折返图标激活
        /// </summary>
        ManuanlOrAutoReturnActived,
    }
}