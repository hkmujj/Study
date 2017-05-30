using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using DevExpress.Mvvm.Native;
using Engine.TCMS.Turkmenistan.Event;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class FaultCutModel : NotificationObject
    {
        private FaultCutUnit _selectItem;

        public FaultCutModel()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<DataServiceDataChangedEvent>().Subscribe(DataChanged, ThreadOption.UIThread);
            AllUnit = new ObservableCollection<FaultCutUnit>(GlobalParam.Instance.FaultCutUnit);
            SelectItem = AllUnit.FirstOrDefault();
            AllUnit.ForEach(f=>f.ChangedDispaly());
        }
        public ObservableCollection<FaultCutUnit> AllUnit { get; private set; }

        public FaultCutUnit SelectItem
        {
            get { return _selectItem; }
            set
            {
                if (Equals(value, _selectItem)) return;
                _selectItem = value;
                RaisePropertyChanged(() => SelectItem);
            }
        }

        private void DataChanged(DataServiceDataChangedEvent.Args obj)
        {
            AllUnit.ForEach(f => f.ChangedData(obj.DataChangedArgs.ChangedBools));
        }
    }
}