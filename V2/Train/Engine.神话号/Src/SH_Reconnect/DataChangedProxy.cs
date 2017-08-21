using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommonUtil.Util;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace SH_Reconnect
{
    public class DataChangedProxy : IDataListener
    {
        public static readonly DataChangedProxy Instance = new DataChangedProxy();

        private readonly List<IDataChangedListener> m_ChangedLiteners;

        public DataChangedProxy()
        {
            m_ChangedLiteners = new List<IDataChangedListener>();
        }

        public void Regist(IDataChangedListener listener)
        {
            if (!m_ChangedLiteners.Contains(listener))
            {
                m_ChangedLiteners.Add(listener);
            }
            else
            {
                AppLog.Info("Data listener {0} has registed! ignore regist twice.", listener.GetType());
            }
        }

        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> args)
        {
            foreach (var it in args.NewValue)
            {
                var item = it;
                for (int i = 0; i < m_ChangedLiteners.Count; i++)
                {
                    var listener = m_ChangedLiteners[i];
                    listener.OnBoolItemChanged(ref item);
                }
            }
        }

        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> args)
        {
            foreach (var it in args.NewValue)
            {
                var item = it;
                for (int i = 0; i < m_ChangedLiteners.Count; i++)
                {
                    var listener = m_ChangedLiteners[i];
                    listener.OnFloatItemChanged(ref item);
                }
            }
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }
    }
}