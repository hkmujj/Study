using System.Collections.Generic;

namespace ATP200C.FunctionButton
{
    /// <summary>
    /// �๦�ܲ˵�(���κ�����)
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