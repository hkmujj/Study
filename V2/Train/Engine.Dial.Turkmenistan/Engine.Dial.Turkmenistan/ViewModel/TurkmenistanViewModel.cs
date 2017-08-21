using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using CommonUtil.Util;
using Engine.Dial.Angola.Annotations;
using Engine.Dial.Turkmenistan.Config;
using Engine.Dial.Turkmenistan.Reourse;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Engine.Dial.Turkmenistan.ViewModel
{
    [Export(typeof(TurkmenistanViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TurkmenistanViewModel : INotifyPropertyChanged, IDataListener
    {
        public TurkmenistanViewModel()
        {
            Config = DataSerialization.DeserializeFromXmlFile<TurkmenistanDialConfig>(Path.Combine(
                GlobalConfigParam.Instance.InitParam.AppConfig.AppPaths.ConfigDirectory, TurkmenistanDialConfig.FileName));
        }

        private double m_LedValue;
        private double m_TwoDialBlackPointer;
        private double m_TwoDialRedPointer;
        private double m_OneDialBlackPointer;
        private double m_OneDialRedPointer;
        public TurkmenistanDialConfig Config { get; private set; }
        public double OneDialRedPointer
        {
            get { return m_OneDialRedPointer; }

            set
            {
                if (value.Equals(m_OneDialRedPointer))
                {
                    return;
                }
                m_OneDialRedPointer = value;
                OnPropertyChanged("OneDialRedPointer");
            }
        }

        public double OneDialBlackPointer
        {
            get { return m_OneDialBlackPointer; }

            set
            {
                if (value.Equals(m_OneDialBlackPointer))
                {
                    return;
                }
                m_OneDialBlackPointer = value;
                OnPropertyChanged("OneDialBlackPointer");
            }
        }

        public double TwoDialRedPointer
        {
            get { return m_TwoDialRedPointer; }

            set
            {
                if (value.Equals(m_TwoDialRedPointer))
                {
                    return;
                }
                m_TwoDialRedPointer = value;
                OnPropertyChanged("TwoDialRedPointer");
            }
        }

        public double TwoDialBlackPointer
        {
            get { return m_TwoDialBlackPointer; }

            set
            {
                if (value.Equals(m_TwoDialBlackPointer))
                {
                    return;
                }
                m_TwoDialBlackPointer = value;
                OnPropertyChanged("TwoDialBlackPointer");
            }
        }



        public double LEDValue
        {
            get { return m_LedValue; }

            set
            {
                if (value.Equals(m_LedValue))
                {
                    return;
                }
                m_LedValue = value;
                OnPropertyChanged("LEDValue");
            }
        }


        public double DialOneLeft { get; private set; }
        public double DialOneTop { get; private set; }
        public double DialTwoLeft { get; private set; }
        public double DialTwoTop { get; private set; }
        public double DialOneWidth { get; set; }
        public double DialOneHeight { get; set; }
        public double DialTwoWidth { get; set; }
        public double DialTwoHeight { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> args)
        {
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> args)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                args.UpdateIfContain(InFloatKeys.InF表1黑针, (f => OneDialBlackPointer = f));
                args.UpdateIfContain(InFloatKeys.InF表1红针, (f => OneDialRedPointer = f));
                args.UpdateIfContain(InFloatKeys.InF表2黑针, (f => TwoDialBlackPointer = f));
                args.UpdateIfContain(InFloatKeys.InF表2红针, (f => TwoDialRedPointer = f));
                args.UpdateIfContain(InFloatKeys.InFLED数字, (f => LEDValue = f));
            }));
        
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }
    }

    public static class Extention
    {
        public static void UpdateIfContain(this CommunicationDataChangedArgs<float> data, string indexName, Action<float> action)
        {
            var index = GlobalConfigParam.Instance.IndexConfig.InFloatDescriptionDictionary[indexName];

            if (data.NewValue.ContainsKey(index))
            {
                if (action!=null)
                {
                    action.Invoke(data.NewValue[index]);
                }
                
            }
        }
    }
}