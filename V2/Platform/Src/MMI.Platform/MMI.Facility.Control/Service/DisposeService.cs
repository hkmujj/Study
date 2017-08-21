using System;
using System.Collections.Generic;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class DisposeService : IDisposeService
    {
        private readonly List<IDisposable> m_RegisttedODisposableCollection;

        public DisposeService()
        {
            m_RegisttedODisposableCollection = new List<IDisposable>();
        }

        public void RegistDisposableObject(IDisposable disposable)
        {
            if (!m_RegisttedODisposableCollection.Contains(disposable))
            {
                m_RegisttedODisposableCollection.Add(disposable);
            }
        }

        public void DisposeRegisttedObjects()
        {
            foreach (var disposable in m_RegisttedODisposableCollection)
            {
                try
                {
                    disposable.Dispose();
                }
                catch (Exception e)
                {
                    SysLog.Error("Dispose {0} fail. {1}", disposable.GetType().FullName, e);
                }
            }
        }
    }
}