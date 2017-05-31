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
    public class AxleBitModel : NotificationObject
    {
        public AxleBitModel()
        {
            CurrentCarAxleBitUnits = new ObservableCollection<AxleBitUnit>(GlobalParam.Instance.AxleBitUnit.Where(w => w.IsCurrent));
            OtherCarAxleBitUnits = new ObservableCollection<AxleBitUnit>(GlobalParam.Instance.AxleBitUnit.Where(w => !w.IsCurrent));
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<DataServiceDataChangedEvent>()
                .Subscribe(DataServiceDataChangedEventAction, ThreadOption.UIThread);
        }

        private void DataServiceDataChangedEventAction(DataServiceDataChangedEvent.Args obj)
        {
            CurrentCarAxleBitUnits.ForEach(f =>
            {
                f.BoolDataChanged(obj.DataChangedArgs.ChangedBools);
                f.FloatChanged(obj.DataChangedArgs.ChangedFloats);
            });
            OtherCarAxleBitUnits.ForEach(f =>
            {
                f.BoolDataChanged(obj.DataChangedArgs.ChangedBools);
                f.FloatChanged(obj.DataChangedArgs.ChangedFloats);
            });
        }

        public ObservableCollection<AxleBitUnit> CurrentCarAxleBitUnits { get; private set; }
        public ObservableCollection<AxleBitUnit> OtherCarAxleBitUnits { get; private set; }
    }
}
