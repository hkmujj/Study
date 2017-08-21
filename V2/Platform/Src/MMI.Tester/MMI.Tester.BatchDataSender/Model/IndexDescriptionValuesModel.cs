using System;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Bars;
using MMI.Facility.Interface.IndexDescription;

namespace MMI.Tester.BatchDataSender.Model
{
    public class IndexDescriptionValuesModel<TIndex, TValue> : IndexDescriptionModel<TIndex>
        where TIndex : IEquatable<TIndex>
    {
        public IndexDescriptionValuesModel(TIndex index, string description) : base(index, description)
        {
            ValueCollection = new GalleryCollection<TValue>();
        }

        public ObservableCollection<TValue> ValueCollection { set; get; }
    }
}