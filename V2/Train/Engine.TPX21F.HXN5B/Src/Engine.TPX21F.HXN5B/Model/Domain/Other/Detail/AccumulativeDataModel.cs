using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Other.Detail
{
    [Export]
    public class AccumulativeDataModel : NotificationObject
    {
        public Lazy<ReadOnlyCollection<AccumulativeDataItem>> PowerCollection { set; get; }
        public Lazy<ReadOnlyCollection<AccumulativeDataItem>> TimeCollection { set; get; }
        public Lazy<ReadOnlyCollection<AccumulativeDataItem>> TowCollection { set; get; }
        public Lazy<ReadOnlyCollection<AccumulativeDataItem>> BrakeCollection { set; get; }
    }
}