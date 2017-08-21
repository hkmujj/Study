using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class DataChangeListenService : IDataChangeListenService
    {
        private readonly Dictionary<string, List<IDataListener>> m_DataListenerSupports;

        public Predicate<string> Filter;

        public DataChangeListenService()
        {
            m_DataListenerSupports = new Dictionary<string, List<IDataListener>>();
            Filter = s => true;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="appConfig"></param>
        public void RegistListener(IDataListener listener, IAppConfig appConfig)
        {
            if (!m_DataListenerSupports.ContainsKey(appConfig.AppName))
            {
                m_DataListenerSupports.Add(appConfig.AppName, new List<IDataListener>());
            }
            m_DataListenerSupports[appConfig.AppName].Add(listener);
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="appConfig"></param>
        public void UnregistListener(IDataListener listener, IAppConfig appConfig)
        {
            if (m_DataListenerSupports.ContainsKey(appConfig.AppName))
            {
                m_DataListenerSupports[appConfig.AppName].Remove(listener);
            }
        }

        /// <summary>返回一个循环访问集合的枚举器。</summary>
        /// <returns>可用于循环访问集合的 <see cref="T:System.Collections.Generic.IEnumerator`1" />。</returns>
        public IEnumerable<IDataListener> GetListeners(string appName)
        {
            if (Filter(appName) && m_DataListenerSupports.ContainsKey(appName))
            {
                return m_DataListenerSupports[appName];
            }

            return Enumerable.Empty<IDataListener>();
        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(string appName, object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            var lists = GetListeners(appName);
            foreach (var it in lists)
            {
                it.OnBoolChanged(sender, dataChangedArgs);
            }
        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(string appName, object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            var lists = GetListeners(appName);
            foreach (var it in lists)
            {
                it.OnFloatChanged(sender, dataChangedArgs);
            }
        }

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(string appName, object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            var lists = GetListeners(appName);
            foreach (var it in lists)
            {
                it.OnDataChanged(sender, dataChangedArgs);
            }
        }
    }
}