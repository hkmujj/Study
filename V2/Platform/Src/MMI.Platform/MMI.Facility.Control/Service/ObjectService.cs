using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Control.Data;
using MMI.Facility.DataType;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    class ObjectService : IObjectService
    {

        /// <summary>
        /// 工程下的插件实例
        /// key : 工程名   key : 查找类型的索引,文件名+类名
        /// </summary>
        readonly Dictionary<string, Dictionary<string, IObjectBase>> m_ProjetAddinInstanceDic;


        public ObjectService(AddInLoader addInLoader)
        {
            m_ProjetAddinInstanceDic = addInLoader.ProjetAddinInstanceDic;

        }

        public T[] GetObject<T>(string appName) where T : baseClass
        {
            if (m_ProjetAddinInstanceDic.ContainsKey(appName))
            {
                return m_ProjetAddinInstanceDic[appName].Values.Where(w => w is T).Cast<T>().ToArray();
            }
            SysLog.Error(string.Format("Can not find the app name ({0}) in addin loader", appName));
            return null;
        }

        public ReadOnlyCollection<IObjectBase> GetAllObject(string appName)
        {
            if (m_ProjetAddinInstanceDic.ContainsKey(appName))
            {
                return m_ProjetAddinInstanceDic[appName].Values.ToList().AsReadOnly();
            }
            SysLog.Error(string.Format("Can not find the app name ({0}) in addin loader", appName));
            return null;
        }
    }
}
