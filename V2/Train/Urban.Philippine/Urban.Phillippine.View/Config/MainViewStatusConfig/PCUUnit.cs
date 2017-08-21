using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "PCU")]
    public class PcuUnit : NotificationObject, IUnit
    {
        private PcuStatus m_CurrentStatus = PcuStatus.Offline;
        private PcuStatus? m_Status;

        public PcuUnit()
        {
            Status = null;
        }

        public PcuStatus? Status
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

        [ExcelField("Fault")]
        public string Fault { get; set; }

        [ExcelField("Normal")]
        public string Normal { get; set; }

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

                case "Fault":
                    Fault = value;
                    break;

                case "Normal":
                    Normal = value;
                    break;
            }
        }

        public void Clear()
        {
            Status = null;
        }

        public void Clear(object obj, Type type)
        {
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            var index1 = IndexConfigure.InBoolIndex[Offline];
            var index2 = IndexConfigure.InBoolIndex[Fault];
            var index3 = IndexConfigure.InBoolIndex[Normal];
           // Status = m_CurrentStatus;
            if (args.Keys.Any(a => a == index1 || a == index2 || a == index3))
            {
                if (args.ContainsKey(index1) && args[index1])
                {
                    m_CurrentStatus = PcuStatus.Offline;
                }
                if (args.ContainsKey(index2) && args[index2])
                {
                    m_CurrentStatus = PcuStatus.Fault;
                }
                if (args.ContainsKey(index3) && args[index3])
                {
                    m_CurrentStatus = PcuStatus.Normal;
                }
            }
            if (iPara != null && Train <= iPara)
            {
                Status = m_CurrentStatus;
            }
            else
            {
                Status = null;
            }
        }

        public void Changed(IDictionary<int, float> args, int? ipara = null)
        {
        }
    }
}