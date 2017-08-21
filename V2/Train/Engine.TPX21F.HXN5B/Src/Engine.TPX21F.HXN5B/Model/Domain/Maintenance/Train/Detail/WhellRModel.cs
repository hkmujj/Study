using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.ViewSource.DesignData;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train.Detail
{
    [Export]
    public class WhellRModel : NotificationObject
    {
        public WhellRModel()
        {
            WhellRItems =
                new Lazy<ObservableCollection<WhellRItem>>(() => DesignWhellRModel.Instance.Items);
        }

        public Lazy<ObservableCollection<WhellRItem>> WhellRItems { private set; get; }
    }
}