using System.Linq;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Common
{
    static class CRH2BaseClassExtension
    {
        /// <summary>
        /// 获得同个工程的 T 类型的 实例 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetSameProjectObjcect<T>(this CRH2BaseClass obj) where T : CRH2BaseClass
        {
            return ObjectManager.Instance.GetAllObjects(obj.ProjectName).OfType<T>().FirstOrDefault();
        }
    }
}
