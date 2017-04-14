using System;
using System.Collections.Generic;
using System.Linq;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 启动模式
    /// </summary>
    public enum StartModel
    {
        /// <summary>
        /// 
        /// </summary>
        Normal,

        /// <summary>
        /// 
        /// </summary>
        Edit,

        /// <summary>
        /// 
        /// </summary>
        PTT,
    }

    /// <summary>
    /// 
    /// </summary>
    public class StartModelHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<StartModel> AllStartModelCollection { private set; get; }

        static StartModelHelper()
        {
            AllStartModelCollection = Enum.GetValues(typeof (StartModel)).Cast<StartModel>().ToList();
        }
    }

}
