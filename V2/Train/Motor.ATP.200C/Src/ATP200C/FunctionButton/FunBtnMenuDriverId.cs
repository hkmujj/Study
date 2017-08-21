using System.Collections.Generic;

namespace ATP200C.FunctionButton
{
    /// <summary>
    /// 多功能菜单(司机号输入)
    /// </summary>
    public class FunBtnMenuDriverId : FunBtnMenuCommon
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="funNames"></param>
        public FunBtnMenuDriverId(int menuId, IList<FunBtnStruct> funNames)
            : base(menuId, funNames)
        {
        }
    }
}