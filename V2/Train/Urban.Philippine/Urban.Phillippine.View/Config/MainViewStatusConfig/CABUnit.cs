using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "CAB")]
    public class CabUnit : NotificationObject, IUnit
    {
        private CabStatus m_Status;

        public CabStatus Status
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

        [ExcelField("Active")]
        public string Active { get; set; }

        [ExcelField("UnActive")]
        public string UnActive { get; set; }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Train":
                    Train = System.Convert.ToInt32(value);
                    break;

                case "Active":
                    Active = value;
                    break;

                case "UnActive":
                    UnActive = value;
                    break;
            }
        }

        public void Clear()
        {
            Status = default(CabStatus);
        }

        public void Clear(object obj, Type type)
        {
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            if (iPara != null && Train <= iPara)
            {
                var index1 = IndexConfigure.InBoolIndex[Active];
                var index2 = IndexConfigure.InBoolIndex[UnActive];
                if (args.Keys.Any(a => a == index1 || a == index2))
                {
                    Status = CabStatus.UnActive;
                    if (args.ContainsKey(index1) && args[index1])
                    {
                        Status = CabStatus.Active;
                    }
                    if (args.ContainsKey(index2) && args[index2])
                    {
                        Status = CabStatus.UnActive;
                    }
                }
            }
        }

        public void Changed(IDictionary<int, float> args, int? ipara = null)
        {
        }
    }
}