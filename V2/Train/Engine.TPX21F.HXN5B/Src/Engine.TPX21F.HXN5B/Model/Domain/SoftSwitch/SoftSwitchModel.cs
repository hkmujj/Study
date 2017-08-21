using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Model.Domain.SoftSwitch.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.SoftSwitch
{
    [Export]
    public class SoftSwitchModel : NotificationObject
    {
        public Lazy<ReadOnlyCollection<SoftSwitchItem>> SoftSwitchItemCollection { set; get; }

        public Lazy<ReadOnlyCollection<SoftSwitchItem>> Page1
        {
            get { return new Lazy<ReadOnlyCollection<SoftSwitchItem>>(() => SoftSwitchItemCollection.Value.Take(10).ToList().AsReadOnly()); }
        }

        public Lazy<ReadOnlyCollection<SoftSwitchItem>> Page2
        {
            get { return new Lazy<ReadOnlyCollection<SoftSwitchItem>>(() => SoftSwitchItemCollection.Value.Skip(10).Take(9).ToList().AsReadOnly()); }
        }

        public Lazy<ReadOnlyCollection<SoftSwitchItem>> Page3
        {
            get { return new Lazy<ReadOnlyCollection<SoftSwitchItem>>(() => SoftSwitchItemCollection.Value.Skip(19).ToList().AsReadOnly()); }
        }
    }
}