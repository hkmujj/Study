using System;
using System.Collections.Generic;
using CommonUtil.Util;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Dictionary<Type, Dictionary<string, object>> m_ServiceDictionary;

        public static ServiceManager Instance { private set; get; }

        static ServiceManager() { Instance = new ServiceManager(); }

        public ServiceManager()
        {
            m_ServiceDictionary = new Dictionary<Type, Dictionary<string, object>>();
        }

        public T GetService<T>() where T : IService
        {
            return GetService<T>(typeof (T).FullName);
        }

        public bool RegistService<T>(object service) where T : IService
        {
            return RegistService<T>(typeof (T).FullName, service);
        }

        public void UnregistService<T>() where T : IService 
        {
            UnregistService<T>(typeof(T).FullName);
        }

        public T GetService<T>(string key) where T : IService
        {
            var type = typeof(T);
            if (m_ServiceDictionary.ContainsKey(type) && m_ServiceDictionary[type].ContainsKey(key))
            {
                return (T)m_ServiceDictionary[type][key];
            }
            LogMgr.Warn(string.Format("There is no service of type {0}", type));
            return default(T);
        }

        public bool RegistService<T>(string key, object service) where T : IService
        {
            if (!m_ServiceDictionary.ContainsKey(typeof(T)))
            {
                m_ServiceDictionary.Add(typeof(T), new Dictionary<string, object>() { { key, service } });
                return true;
            }

            if (!m_ServiceDictionary[typeof(T)].ContainsKey(key))
            {
                m_ServiceDictionary[typeof(T)].Add(key, service);
                return true;
            }

            LogMgr.Warn(string.Format("you have registed service of type {0}, ignore this operation, if you want regist, please use UnregistService first .", typeof(T)));

            return false;
        }

        public void UnregistService<T>(string key) where T : IService
        {
            if (m_ServiceDictionary.ContainsKey(typeof(T)) && m_ServiceDictionary[typeof(T)].ContainsKey(key))
            {
                m_ServiceDictionary[typeof(T)].Remove(key);
                LogMgr.Debug(string.Format("has unregisted service of {0}", typeof(T)));
            }
        }
    }
}