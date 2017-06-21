using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
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
            DispatcherTimer time = new DispatcherTimer(TimeSpan.FromSeconds(2), DispatcherPriority.Normal, ChangedAxle, Application.Current.Dispatcher);
            time.Start();

        }
        private static Random m_Random = new Random(100);
        private string m_OtherDisplayT2;
        private string m_OtherDisplayT1;
        private string m_OtherDisplayAxle2;
        private string m_OtherDisplayAxle1;
        private string m_DisplayT3;
        private string m_DisplayT2;
        private string m_DisplayT1;
        private string m_DisplayAxle3;
        private string m_DisplayAxle2;
        private string m_DisplayAxle1;

        private void ChangedAxle(object sender, EventArgs e)
        {
            var index1 = m_Random.Next(0, CurrentCarAxleBitUnits.Count);
            DisplayAxle1 = CurrentCarAxleBitUnits[index1].Axle.ToString() + CurrentCarAxleBitUnits[index1].Bit;
            DisplayT1 = CurrentCarAxleBitUnits[index1].AxleBitValue.ToString("F0");
            index1 = m_Random.Next(0, CurrentCarAxleBitUnits.Count);
            DisplayAxle2 = CurrentCarAxleBitUnits[index1].Axle.ToString() + CurrentCarAxleBitUnits[index1].Bit;
            DisplayT2 = CurrentCarAxleBitUnits[index1].AxleBitValue.ToString("F0");
            index1 = m_Random.Next(0, CurrentCarAxleBitUnits.Count);
            DisplayAxle3 = CurrentCarAxleBitUnits[index1].Axle.ToString() + CurrentCarAxleBitUnits[index1].Bit;
            DisplayT3 = CurrentCarAxleBitUnits[index1].AxleBitValue.ToString("F0");

            index1 = m_Random.Next(0, OtherCarAxleBitUnits.Count);
            OtherDisplayAxle1 = OtherCarAxleBitUnits[index1].Axle.ToString() + OtherCarAxleBitUnits[index1].Bit;
            OtherDisplayT1 = OtherCarAxleBitUnits[index1].AxleBitValue.ToString("F0");
            index1 = m_Random.Next(0, OtherCarAxleBitUnits.Count);
            OtherDisplayAxle2 = OtherCarAxleBitUnits[index1].Axle.ToString() + OtherCarAxleBitUnits[index1].Bit;
            OtherDisplayT2 = OtherCarAxleBitUnits[index1].AxleBitValue.ToString("F0");
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

        public string DisplayAxle1
        {
            get { return m_DisplayAxle1; }
            set
            {
                if (value == m_DisplayAxle1)
                    return;
                m_DisplayAxle1 = value;
                RaisePropertyChanged(() => DisplayAxle1);
            }
        }

        public string DisplayAxle2
        {
            get { return m_DisplayAxle2; }
            set
            {
                if (value == m_DisplayAxle2)
                    return;
                m_DisplayAxle2 = value;
                RaisePropertyChanged(() => DisplayAxle2);
            }
        }

        public string DisplayAxle3
        {
            get { return m_DisplayAxle3; }
            set
            {
                if (value == m_DisplayAxle3)
                    return;
                m_DisplayAxle3 = value;
                RaisePropertyChanged(() => DisplayAxle3);
            }
        }

        public string DisplayT1
        {
            get { return m_DisplayT1; }
            set
            {
                if (value == m_DisplayT1)
                    return;
                m_DisplayT1 = value;
                RaisePropertyChanged(() => DisplayT1);
            }
        }

        public string DisplayT2
        {
            get { return m_DisplayT2; }
            set
            {
                if (value == m_DisplayT2)
                    return;
                m_DisplayT2 = value;
                RaisePropertyChanged(() => DisplayT2);
            }
        }

        public string DisplayT3
        {
            get { return m_DisplayT3; }
            set
            {
                if (value == m_DisplayT3)
                    return;
                m_DisplayT3 = value;
                RaisePropertyChanged(() => DisplayT3);
            }
        }

        public string OtherDisplayAxle1
        {
            get { return m_OtherDisplayAxle1; }
            set
            {
                if (value == m_OtherDisplayAxle1)
                    return;
                m_OtherDisplayAxle1 = value;
                RaisePropertyChanged(() => OtherDisplayAxle1);
            }
        }

        public string OtherDisplayAxle2
        {
            get { return m_OtherDisplayAxle2; }
            set
            {
                if (value == m_OtherDisplayAxle2)
                    return;
                m_OtherDisplayAxle2 = value;
                RaisePropertyChanged(() => OtherDisplayAxle2);
            }
        }

        public string OtherDisplayT1
        {
            get { return m_OtherDisplayT1; }
            set
            {
                if (value == m_OtherDisplayT1)
                    return;
                m_OtherDisplayT1 = value;
                RaisePropertyChanged(() => OtherDisplayT1);
            }
        }

        public string OtherDisplayT2
        {
            get { return m_OtherDisplayT2; }
            set
            {
                if (value == m_OtherDisplayT2)
                    return;
                m_OtherDisplayT2 = value;
                RaisePropertyChanged(() => OtherDisplayT2);
            }
        }
    }
}
