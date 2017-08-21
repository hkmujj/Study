using Engine.Dial.Angola.Annotations;
using Engine.Dial.Angola.Config;
using Engine.Dial.Angola.Reourse;
using MMI.Facility.Interface.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using MMI.Facility.Interface.Service;

namespace Engine.Dial.Angola.ViewModel
{
    [Export(typeof(AngolaViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AngolaViewModel : INotifyPropertyChanged, IDataListener
    {
        public AngolaViewModel()
        {
        }

        private double m_Fornecimento;
        private double m_Reservatono;
        private double m_VBPValue;
        private double m_BPValue;
        private double m_ERValue;
        private double m_BCValue;
        private double m_MRValue;

        public double MRValue
        {
            get { return m_MRValue; }

            set
            {
                if (value.Equals(m_MRValue))
                {
                    return;
                }
                m_MRValue = value;
                OnPropertyChanged(nameof(MRValue));
            }
        }

        public double BCValue
        {
            get { return m_BCValue; }

            set
            {
                if (value.Equals(m_BCValue))
                {
                    return;
                }
                m_BCValue = value;
                OnPropertyChanged(nameof(BCValue));
            }
        }

        public double ERValue
        {
            get { return m_ERValue; }

            set
            {
                if (value.Equals(m_ERValue))
                {
                    return;
                }
                m_ERValue = value;
                OnPropertyChanged(nameof(ERValue));
            }
        }

        public double BPValue
        {
            get { return m_BPValue; }

            set
            {
                if (value.Equals(m_BPValue))
                {
                    return;
                }
                m_BPValue = value;
                OnPropertyChanged(nameof(BPValue));
            }
        }

        public double VBPValue
        {
            get { return m_VBPValue; }

            set
            {
                if (value.Equals(m_VBPValue))
                {
                    return;
                }
                m_VBPValue = value;
                OnPropertyChanged(nameof(VBPValue));
            }
        }

        public double Reservatono
        {
            get { return m_Reservatono; }

            set
            {
                if (value.Equals(m_Reservatono))
                {
                    return;
                }
                m_Reservatono = value;
                OnPropertyChanged(nameof(Reservatono));
            }
        }

        public double Fornecimento
        {
            get { return m_Fornecimento; }

            set
            {
                if (value.Equals(m_Fornecimento))
                {
                    return;
                }
                m_Fornecimento = value;
                OnPropertyChanged(nameof(Fornecimento));
            }
        }

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
            args.UpdateIfContain(InFloatKeys.InF表1白针, ( f => BCValue = f ));
            args.UpdateIfContain(InFloatKeys.InF表1红针, ( f => MRValue = f ));
            args.UpdateIfContain(InFloatKeys.InF表2白针, ( f => BPValue = f ));
            args.UpdateIfContain(InFloatKeys.InF表2红针, ( f => ERValue = f ));
            args.UpdateIfContain(InFloatKeys.InF表3白针, ( f => VBPValue = f ));
            args.UpdateIfContain(InFloatKeys.InF左侧LED数字, ( f => Reservatono = f ));
            args.UpdateIfContain(InFloatKeys.InF右侧LED数字, ( f => Fornecimento = f ));
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
                action?.Invoke(data.NewValue[index]);
            }
        }
    }
}