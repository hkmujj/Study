using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "VVVF")]
    public class VVVFUnit : NotificationObject, ISetValueProvider, IStatusChanged, IDataClear
    {
        private VVVFStatus m_CurrentStaus = VVVFStatus.Offline;
        private VVVFStatus? m_Status;

        public VVVFUnit()
        {
            Status = null;
        }

        public VVVFStatus? Status
        {
            get { return m_Status; }
            private set
            {
                if (value == m_Status)
                {
                    return;
                }
                m_Status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        [ExcelField("Offline")]
        public string Offline { get; set; }

        [ExcelField("Defective")]
        public string Defective { get; set; }

        [ExcelField("Normal")]
        public string Normal { get; set; }

        [ExcelField("Train")]
        public int Train { get; set; }

        public void Clear()
        {
            Status = null;
        }

        public void Clear(object obj, Type type)
        {
        }

        //[ExcelField("Unit")]
        //public int Unit { get; set; }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Offline":
                    Offline = value;
                    break;

                case "Defective":
                    Defective = value;
                    break;

                case "Normal":
                    Normal = value;
                    break;

                case "Train":
                    Train = System.Convert.ToInt32(value);
                    break;

                case "Unit":
                    //  Unit = System.Convert.ToInt32(value);
                    break;
            }
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            var index1 = IndexConfigure.InBoolIndex[Offline];
            var index2 = IndexConfigure.InBoolIndex[Defective];
            var index3 = IndexConfigure.InBoolIndex[Normal];
          //  Status = m_CurrentStaus;
            if (args.Keys.Any(a => a == index1 || a == index2 || a == index3))
            {
                if (args.ContainsKey(index1) && args[index1])
                {
                    m_CurrentStaus = VVVFStatus.Offline;
                }
                if (args.ContainsKey(index2) && args[index2])
                {
                    m_CurrentStaus = VVVFStatus.Defective;
                }
                if (args.ContainsKey(index3) && args[index3])
                {
                    m_CurrentStaus = VVVFStatus.Normal;
                }
            }
            if (iPara != null && Train <= iPara)
            {
                Status = m_CurrentStaus;
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