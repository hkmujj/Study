using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "HSCB")]
    public class HscbUnit : NotificationObject, ISetValueProvider, IDataClear, IStatusChanged
    {
        private HscbStatus m_CurrentStatus = HscbStatus.Close;
        private HscbStatus? m_Status;

        public HscbUnit()
        {
            Status = null;
        }

        public HscbStatus? Status
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

        [ExcelField("HSCBClose")]
        public string Close { get; set; }

        [ExcelField("HSCBOpen")]
        public string Open { get; set; }

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

                case "Close":
                    Close = value;
                    break;

                case "Open":
                    Open = value;
                    break;
            }
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            var index1 = IndexConfigure.InBoolIndex[Open];
            var index2 = IndexConfigure.InBoolIndex[Close];
         //   Status = m_CurrentStatus;
            if (args.Keys.Any(a => a == index1 || a == index2))
            {
                if (args.ContainsKey(index1) && args[index1])
                {
                    m_CurrentStatus = HscbStatus.Open;
                }
                if (args.ContainsKey(index2) && args[index2])
                {
                    m_CurrentStatus = HscbStatus.Close;
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