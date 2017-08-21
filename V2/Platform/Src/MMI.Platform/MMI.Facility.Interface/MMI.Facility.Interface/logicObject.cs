using System.Collections.Generic;
using MMI.Facility.Interface.Data.Config;

// ReSharper disable All

namespace MMI.Facility.Interface
{
    /// <summary>
    /// �߼�����
    /// </summary>
    public class LogicObject : IAppLogicConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public LogicObject()
        {
            Memo = string.Empty;
            Enable = false;
            LogicRunType = LogicType.AND;
            RightDataList = new List<int>();
            LeftDataList = new List<int>();
            RecPath = string.Empty;
            Index = 0;
        }


        /// <summary>
        /// 
        /// </summary>
        public IAppConfig ParentConfig { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// ��Դ·��
        /// </summary>
        public string RecPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> LeftDataList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> RightDataList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LogicType LogicRunType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Memo { get; set; }
       

    }
}
