using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Infomation;

namespace Urban.Domain.TrainState.Model.Infomation
{
    public class InfomationCollection : ReadOnlyCollection<IInfomationItem>, IInfomationCollection
    {

        public InfomationCollection()
            : base(new ObservableCollection<IInfomationItem>())
        {
            ((ObservableCollection<IInfomationItem>) Items).CollectionChanged += RaiseCollectionChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void RaiseCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}