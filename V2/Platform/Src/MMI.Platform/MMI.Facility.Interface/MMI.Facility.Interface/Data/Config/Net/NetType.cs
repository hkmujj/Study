using System.Collections.Generic;

namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 
    /// </summary>
    public enum NetType
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// 
        /// </summary>
        B,

        /// <summary>
        /// 
        /// </summary>
        C,

        /// <summary>
        /// PTT2D网络
        /// </summary>
        PTT2D,
    }

    /// <summary>
    /// 
    /// </summary>
    public class NetTypeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public const string NoneTypeSTring = "无网络";

        /// <summary>
        /// 
        /// </summary>
        public const string BTypeString = "老网络";

        /// <summary>
        /// 
        /// </summary>
        public const string CTypeString = "新网络";

        /// <summary>
        /// 
        /// </summary>
        public const string PTT2DTypeString = "PTT2D";

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<NetType, string> AllNetTypeDictionary { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<string> AllNetTypeStringCollection
        {
            get { return AllNetTypeDictionary.Values; }
        }

        static NetTypeHelper()
        {
            AllNetTypeDictionary = new Dictionary<NetType, string>()
            {
                {NetType.None, NoneTypeSTring},
                {NetType.B, BTypeString},
                {NetType.C, CTypeString},
                {NetType.PTT2D, PTT2DTypeString},
            };
        }
    }
}
