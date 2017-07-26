using System.Collections.Specialized;
using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.StateProvider
{
    public class F4DataUpStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void UpdateState()
        {
            UpdateEnable();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.Message.CanNavigateUp;
        }

        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);

            ATP.Message.PropertyChanged += MessageOnPropertyChanged;
            ATP.Message.MessageCollection.CollectionChanged += MessageCollectionOnCollectionChanged;
        }

        private void MessageCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            UpdateEnable();
        }

        private void MessageOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateEnable();
        }
    }
}