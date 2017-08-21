using System;
using System.Collections.Generic;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Interface;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "Temperature")]
    public class TemperatureUnit : NotificationObject, IDataClear, IStatusChanged, ISetValueProvider
    {
        private double m_CurrentTemerature;
        private double? m_Temerature;

        public TemperatureUnit()
        {
            Temerature = null;
        }

        [ExcelField("Train")]
        public int Train { get; set; }

        [ExcelField("Temperature")]
        public string Temperature { get; set; }

        public double? Temerature
        {
            get { return m_Temerature; }
            set
            {
                if (value.Equals(m_Temerature))
                {
                    return;
                }
                m_Temerature = value;
                RaisePropertyChanged(() => Temerature);
            }
        }

        public void Clear()
        {
            Temerature = null;
        }

        public void Clear(object obj, Type type)
        {
        }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Train":
                    Train = System.Convert.ToInt32(value);
                    break;

                case "Temperature":
                    Temperature = value;
                    break;
            }
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
        }

        public void Changed(IDictionary<int, float> args, int? ipara = null)
        {
            var index = IndexConfigure.IntFloatIndex[Temperature];
          //  Temerature = m_CurrentTemerature;
            if (args.ContainsKey(index))
            {
                m_CurrentTemerature = args[index];
            }
            if (ipara != null && Train <= ipara)
            {
                Temerature = m_CurrentTemerature;
            }
            else
            {
                Temerature = null;
            }
        }
    }
}