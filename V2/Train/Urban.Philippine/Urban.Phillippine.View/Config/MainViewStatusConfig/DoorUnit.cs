using System;
using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "Door")]
    public class DoorUnit : NotificationObject, IDataClear, ISetValueProvider, IStatusChanged
    {
        private DoorStatus m_CurrentStatus = DoorStatus.Offline;
        private DoorStatus? m_Status;

        public DoorUnit()
        {
            Status = null;
        }

        public DoorStatus? Status
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

        [ExcelField("Offline")]
        public string Offline { get; set; }

        [ExcelField("Isolated")]
        public string Isolated { get; set; }

        [ExcelField("Defective")]
        public string Defective { get; set; }

        [ExcelField("Emergency")]
        public string Emergency { get; set; }

        [ExcelField("Obstade")]
        public string Obstade { get; set; }

        [ExcelField("DoorOpen")]
        public string Open { get; set; }

        [ExcelField("DoorClose")]
        public string Close { get; set; }

        [ExcelField("Train")]
        public int Train { get; set; }

        [ExcelField("Door")]
        public int Door { get; set; }

        public void Clear()
        {
            Status = default(DoorStatus);
        }

        public void Clear(object obj, Type type)
        {
        }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Offline":
                    Offline = value;
                    break;

                case "Isolated":
                    Isolated = value;
                    break;

                case "Defective":
                    Defective = value;
                    break;

                case "Emergency":
                    Emergency = value;
                    break;

                case "Obstade":
                    Obstade = value;
                    break;

                case "Open":
                    Open = value;
                    break;

                case "Close":
                    Close = value;
                    break;

                case "Train":
                    Train = System.Convert.ToInt32(value);
                    break;

                case "Door":
                    Door = System.Convert.ToInt32(value);
                    break;
            }
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            GetTrainName(args);
            if (iPara != null && Train <= iPara)
            {
                Status = m_CurrentStatus;
            }
            else
            {
                Status = null;
            }
        }

        private void GetTrainName(IDictionary<int, bool> args)
        {
            var index1 = IndexConfigure.InBoolIndex[Offline];
            var index2 = IndexConfigure.InBoolIndex[Isolated];
            var index3 = IndexConfigure.InBoolIndex[Defective];
            var index4 = IndexConfigure.InBoolIndex[Emergency];
            var index5 = IndexConfigure.InBoolIndex[Obstade];
            var index6 = IndexConfigure.InBoolIndex[Open];
            var index7 = IndexConfigure.InBoolIndex[Close];
            // Status = m_CurrentStatus;
            if (Contain(args, index1, index2, index3, index4, index5, index6, index7))
            {
                GetTrainValue(args, index1, index2, index3, index4, index5, index6, index7);
            }
        }

        private void GetTrainValue(IDictionary<int, bool> args, int index1, int index2, int index3, int index4, int index5, int index6,
            int index7)
        {

            if (GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(index1))
            {
                m_CurrentStatus = DoorStatus.Offline;
            }
            if (GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(index2))
            {
                m_CurrentStatus = DoorStatus.Isolated;
            }
            if (GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(index3))
            {
                m_CurrentStatus = DoorStatus.Defective;
            }
            if (GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(index4))
            {
                m_CurrentStatus = DoorStatus.Emergency;
            }
            if (GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(index5))
            {
                m_CurrentStatus = DoorStatus.Obstade;
            }
            if (GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(index6))
            {
                m_CurrentStatus = DoorStatus.Open;
            }
            if (GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(index7))
            {
                m_CurrentStatus = DoorStatus.Close;
            }
            //if (args.ContainsKey(index1) && args[index1])
            //{
            //    m_CurrentStatus = DoorStatus.Offline;
            //}
            //if (args.ContainsKey(index2) && args[index2])
            //{
            //    m_CurrentStatus = DoorStatus.Isolated;
            //}
            //if (args.ContainsKey(index3) && args[index3])
            //{
            //    m_CurrentStatus = DoorStatus.Defective;
            //}
            //if (args.ContainsKey(index4) && args[index4])
            //{
            //    m_CurrentStatus = DoorStatus.Emergency;
            //}
            //if (args.ContainsKey(index5) && args[index5])
            //{
            //    m_CurrentStatus = DoorStatus.Obstade;
            //}
            //if (args.ContainsKey(index6) && args[index6])
            //{
            //    m_CurrentStatus = DoorStatus.Open;
            //}
            //if (args.ContainsKey(index7) && args[index7])
            //{
            //    m_CurrentStatus = DoorStatus.Close;
            //}
        }

        private static bool Contain(IDictionary<int, bool> args, int index1, int index2, int index3, int index4, int index5, int index6, int index7)
        {
            return args.Keys.Any(
                a =>
                    a == index1 || a == index2 || a == index3 || a == index4 || a == index5 || a == index6 ||
                    a == index7);
        }

        public void Changed(IDictionary<int, float> args, int? ipara = null)
        {
        }
    }
}