using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection
{
    public class SpeedCurve : NotificationObject, ISpeedCurve
    {
        public SpeedCurve()
        {
            CurvePointCollection = new ObservableCollection<DistanceSpeedPoint>();
            CurvePointCollection.CollectionChanged += CurvePointCollectionOnCollectionChanged;
        }

        private void CurvePointCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            RaisePropertyChanged(() => CurvePointCollection);
        }

        public event EventHandler CurvePointCollectionChanged;

        public ObservableCollection<DistanceSpeedPoint> CurvePointCollection { get; private set; }

        public void RaiseCurvePointCollectionChanged()
        {
            OnCurvePointCollectionChanged();
        }

        protected virtual void OnCurvePointCollectionChanged()
        {
            var handler = CurvePointCollectionChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
