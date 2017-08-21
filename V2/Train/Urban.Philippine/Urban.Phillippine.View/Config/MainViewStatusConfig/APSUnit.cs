using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "APS")]
    public class APSUnit : NotificationObject, ISetValueProvider, IDataClear, IStatusChanged
    {
        private APSStatus m_CurrentAPSStatus = APSStatus.Offline;
        private APSStatus? m_Status;

        public APSUnit()
        {
            Status = null;
        }

        public APSStatus? Status
        {
            get { return m_Status; }
            set
            {
                if (value == m_Status)
                {
                    return;
                }
                m_Status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        [ExcelField("Train")]
        public int Train { get; set; }

        [ExcelField("Offline")]
        public string Offline { get; set; }

        [ExcelField("MajorFault")]
        public string MajorFault { get; set; }

        [ExcelField("MinorFault")]
        public string MinorFault { get; set; }

        [ExcelField("Normal")]
        public string Normal { get; set; }

        public void Clear()
        {
            Status = null;
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

                case "Offline":
                    Offline = value;
                    break;

                case "MajorFault":
                    MajorFault = value;
                    break;

                case "MinorFault":
                    MinorFault = value;
                    break;

                case "Normal":
                    Normal = value;
                    break;
            }
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            GetTrainValue(args);
            if (iPara != null && Train <= iPara)
            {
                Status = m_CurrentAPSStatus;
            }
            else
            {
                Status = null;
            }
        }

        private void GetTrainValue(IDictionary<int, bool> args)
        {
            var index1 = IndexConfigure.InBoolIndex[Offline];
            var index2 = IndexConfigure.InBoolIndex[MajorFault];
            var index3 = IndexConfigure.InBoolIndex[MinorFault];
            var index4 = IndexConfigure.InBoolIndex[Normal];
            if (args.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4))
            {
                if (args.ContainsKey(index1) && args[index1])
                {
                    m_CurrentAPSStatus = APSStatus.Offline;
                }
                if (args.ContainsKey(index2) && args[index2])
                {
                    m_CurrentAPSStatus = APSStatus.MajorFault;
                }
                if (args.ContainsKey(index3) && args[index3])
                {
                    m_CurrentAPSStatus = APSStatus.MinorFault;
                }
                if (args.ContainsKey(index4) && args[index4])
                {
                    m_CurrentAPSStatus = APSStatus.Normal;
                }
            }
        }

        public void Changed(IDictionary<int, float> args, int? ipara = null)
        {
        }
    }
}