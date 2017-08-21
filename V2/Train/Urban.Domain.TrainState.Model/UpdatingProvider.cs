using System.Collections.Generic;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model
{
    /// <summary>
    /// 更新数据的方法。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdatingProvider<T> : ListeningModelProviderBase, IStateUpdatable, IUpdatingEventProvider<T> where T : UpdatingProvider<T>
    {

        public event UpdateEvent<T> Updating;

        public UpdatingProvider()
        {
            ListeningModelCollection = new List<ListeningModel>();
        }

        public virtual void Update(IUpdateParam updateParam)
        {
            OnUpdating((T)this, updateParam);
        }

        protected virtual void OnUpdating(T target, IUpdateParam updateParam)
        {
            UpdateEvent<T> handler = Updating;
            if (handler != null)
            {
                handler(target, updateParam);
            }
        }
    }
}