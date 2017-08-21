using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommonUtil.Util;

namespace CRH2MMI.Common.Global
{
    class ObjectManager
    {
        public static ObjectManager Instance { private set; get; }

        private readonly List<string> m_ProjectNames;
        public ReadOnlyCollection<string> ProjectNames
        {
            get
            {
                return m_ProjectNames.AsReadOnly();
            }
        }

        static ObjectManager() { Instance = new ObjectManager(); }

        private ObjectManager()
        {
            m_ProjectNames = new List<string>();
            m_AllObjectDictionary = new Dictionary<string, List<CRH2BaseClass>>();
        }

        /// <summary>
        /// key : project name
        /// 
        /// </summary>
        private readonly Dictionary<string, List<CRH2BaseClass>> m_AllObjectDictionary;

        /// <summary>
        /// 获取所有工程的实例 
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<CRH2BaseClass> GetAllObjects()
        {
            return m_AllObjectDictionary.Values.SelectMany(s => s).ToList().AsReadOnly();
        }

        /// <summary>
        /// 获得同一个工程下的所有实例
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public ReadOnlyCollection<CRH2BaseClass> GetAllObjects(string projectName)
        {
            if (!m_AllObjectDictionary.ContainsKey(projectName))
            {
                LogMgr.Error(string.Format("Can not find project {0}", projectName));
                return ( new List<CRH2BaseClass>() ).AsReadOnly();
            }
            return m_AllObjectDictionary[projectName].AsReadOnly();
        }

        /// <summary>
        ///  记录实例
        /// </summary>
        /// <param name="obj"></param>
        public void RegistObject(CRH2BaseClass obj)
        {
            if (!m_AllObjectDictionary.ContainsKey(obj.ProjectName))
            {
                m_ProjectNames.Add(obj.ProjectName);
                m_AllObjectDictionary.Add(obj.ProjectName, new List<CRH2BaseClass>());
            }

            if (m_AllObjectDictionary[obj.ProjectName].Contains(obj))
            {
                LogMgr.Warn(string.Format("{0} of project {1} has registed into ObjectManager. Can not regist again.", obj.GetType(), obj.ProjectName));
                return;
            }

            m_AllObjectDictionary[obj.ProjectName].Add(obj);

        }
    }
}
