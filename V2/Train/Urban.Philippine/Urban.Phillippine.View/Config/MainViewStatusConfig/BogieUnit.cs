using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "Bogie")]
    public class BogieUnit : NotificationObject, ISetValueProvider, IDataClear, IStatusChanged
    {
        private BogieStatus m_CurrentStatus = BogieStatus.BcuOffline;
        private BogieStatus? m_Status;

        public BogieUnit()
        {
            Status = null;
        }

        public BogieStatus? Status
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

        [ExcelField("Bogie")]
        public string Bogie { get; set; }

        [ExcelField("BCUOffline")]
        public string BcuOffline { get; set; }

        [ExcelField("Separate")]
        public string Separate { get; set; }

        [ExcelField("Fault")]
        public string Fault { get; set; }

        [ExcelField("MechanicalBrakeApply")]
        public string MechanicalBrakeApply { get; set; }

        [ExcelField("MechanicalBrakeRelease")]
        public string MechanicalBrakeRelease { get; set; }

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

                case "Bogie":
                    Bogie = value;
                    break;

                case "BCUOffline":
                    BcuOffline = value;
                    break;

                case "Separate":
                    Separate = value;
                    break;

                case "Fault":
                    Fault = value;
                    break;

                case "MechanicalBrakeApply":
                    MechanicalBrakeApply = value;
                    break;

                case "MechanicalBrakeRelease":
                    MechanicalBrakeRelease = value;
                    break;
            }
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            GetTrainValue(args);
            if (iPara != null && Train <= iPara)
            {
                Status = m_CurrentStatus;
            }
            else
            {
                Status = null;
            }
        }

        private void GetTrainValue(IDictionary<int, bool> args)
        {
            var index1 = IndexConfigure.InBoolIndex[BcuOffline];
            var index2 = IndexConfigure.InBoolIndex[Separate];
            var index3 = IndexConfigure.InBoolIndex[Fault];
            var index4 = IndexConfigure.InBoolIndex[MechanicalBrakeApply];
            var index5 = IndexConfigure.InBoolIndex[MechanicalBrakeRelease];
            // Status = m_CurrentStatus;
            if (args.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4 || a == index5))
            {
                if (args.ContainsKey(index1) && args[index1])
                {
                    m_CurrentStatus = BogieStatus.BcuOffline;
                }
                if (args.ContainsKey(index2) && args[index2])
                {
                    m_CurrentStatus = BogieStatus.Separate;
                }
                if (args.ContainsKey(index3) && args[index3])
                {
                    m_CurrentStatus = BogieStatus.Fault;
                }
                if (args.ContainsKey(index4) && args[index4])
                {
                    m_CurrentStatus = BogieStatus.MechanicalBrakeApply;
                }
                if (args.ContainsKey(index5) && args[index5])
                {
                    m_CurrentStatus = BogieStatus.MechanicalBrakeRelease;
                }
            }
        }

        public void Changed(IDictionary<int, float> args, int? ipara = null)
        {
        }
    }
}