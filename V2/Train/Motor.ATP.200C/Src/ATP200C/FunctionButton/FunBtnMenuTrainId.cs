using System.Collections.Generic;

namespace ATP200C.FunctionButton
{
    /// <summary>
    /// 多功能菜单(车次号输入)
    /// </summary>
    public class FunBtnMenuTrainId : FunBtnMenuCommon
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="funNames"></param>
        public FunBtnMenuTrainId(int menuId, IList<FunBtnStruct> funNames)
            : base(menuId, funNames)
        {
        }
    }
}