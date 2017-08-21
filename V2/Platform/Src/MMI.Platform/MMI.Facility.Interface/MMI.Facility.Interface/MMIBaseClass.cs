using System.Drawing;
using System.IO;
using System.Linq;

namespace MMI.Facility.Interface
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
        /// 获得资源图片
        /// return UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(_recPath, s))).ToArray();
        /// </summary>
        /// <returns></returns>
        public Image[] GetImages()
        {
            return UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToArray();
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
