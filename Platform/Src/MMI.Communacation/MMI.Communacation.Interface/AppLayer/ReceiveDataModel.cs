using System.Collections.Generic;

namespace MMI.Communacation.Interface.AppLayer
{
    public class ReceiveDataModel<T>
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> DataList { set; get; }

        /// <summary>
        /// 数据起始地址
        /// </summary>
        public int StartIndex { set; get; }
        
    }
}
