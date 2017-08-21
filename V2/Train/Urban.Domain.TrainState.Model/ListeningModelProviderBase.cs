using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model
{
    public abstract class ListeningModelProviderBase : NotificationObject, IListeningModelProvider
    {
        private List<ListeningModel> m_ListeningModelCollection;
        private ReadOnlyCollection<IListeningModel> m_IListeningModelCollection;

        public List<ListeningModel> ListeningModelCollection
        {
            set
            {
                m_ListeningModelCollection = value ?? ( new List<ListeningModel>() );
                m_IListeningModelCollection = m_ListeningModelCollection.Cast<IListeningModel>().ToList().AsReadOnly();
            }
            get { return m_ListeningModelCollection; }
        }

        ReadOnlyCollection<IListeningModel> IListeningModelProvider.ListeningModelCollection
        {
            get { return m_IListeningModelCollection; }
        }

        protected ListeningModelProviderBase()
        {
            ListeningModelCollection = new List<ListeningModel>();
        }
    }
}