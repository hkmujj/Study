using MMI.Facility.Interface;

namespace CRH2MMI
{
    /// <summary>
    /// 封装 的 baseClass
    /// </summary>
    public class MMIBaseClass : baseClass
    {
        /// <summary>
        /// 获取当前工程的名字
        /// </summary>
        /// <returns></returns>
        public string GetProjectName()
        {

            return AppConfig.AppName;
        
        }

        /// <summary>
        ///  获得指定索引处的输入bool值 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool GetInBoolAt(int idx)
        {
            return BoolList[UIObj.InBoolList[idx]];
        }

        /// <summary>
        ///  获得指定索引处的输入Float值 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public float GetInFloatAt(int idx)
        {
            return FloatList[UIObj.InFloatList[idx]];
        }
    }
}
