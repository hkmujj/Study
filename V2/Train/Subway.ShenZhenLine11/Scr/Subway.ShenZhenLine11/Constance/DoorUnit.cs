using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Enum;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.Constance
{
    [ExcelLocation("主界面各个子界面接口.xls", "Door")]
    public class DoorUnit : NotificationObject, ISetValueProvider, IStatusChanged
    {
        public DoorUnit()
        {
            DoorStatus = Door.Close;
        }
        private Door m_DoorStatus;

        [ExcelField("Car")]
        public int Car { get; set; }
        [ExcelField("DoorIndex")]
        public int DoorIndex { get; set; }
        [ExcelField("Emergency")]
        public string Emergency { get; set; }
        [ExcelField("Cut")]
        public string Cut { get; set; }
        [ExcelField("GraveFault")]
        public string GraveFault { get; set; }
        [ExcelField("MediumFault")]
        public string MediumFault { get; set; }
        [ExcelField("Check")]
        public string Check { get; set; }
        [ExcelField("Opened")]
        public string Opened { get; set; }
        [ExcelField("Closed")]
        public string Close { get; set; }

        public Door DoorStatus
        {
            get { return m_DoorStatus; }
            set
            {
                if (value == m_DoorStatus)
                {
                    return;
                }
                m_DoorStatus = value;
                RaisePropertyChanged(() => DoorStatus);
            }
        }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Car":
                    Car = value.ToInt();
                    break;
                case "DoorIndex":
                    DoorIndex = value.ToInt();
                    break;
                case "Emergency":
                    Emergency = value;
                    break;
                case "Cut":
                    Cut = value;
                    break;
                case "GraveFault":
                    GraveFault = value;
                    break;
                case "MediumFault":
                    MediumFault = value;
                    break;
                case "Check":
                    Check = value;
                    break;
                case "Opened":
                    Opened = value;
                    break;
                case "Close":
                    Close = value;
                    break;
            }
        }

        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Emergency];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Cut];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[GraveFault];
            var index4 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[MediumFault];
            var index5 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Check];
            var index6 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Opened];
            var index7 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Close];
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4 || a == index5 || a == index6 || a == index7))
            {
                DoorStatus = Door.Normal;
                if (ContainsTure(args.NewValue, index1))
                {
                    DoorStatus = Door.Emergency;
                }
                if (ContainsTure(args.NewValue, index2))
                {
                    DoorStatus = Door.Cut;
                }
                if (ContainsTure(args.NewValue, index3))
                {
                    DoorStatus = Door.SeverityFault;
                }
                if (ContainsTure(args.NewValue, index4))
                {
                    DoorStatus = Door.MediumFaulr;
                }
                if (ContainsTure(args.NewValue, index5))
                {
                    DoorStatus = Door.Check;
                }
                if (ContainsTure(args.NewValue, index6))
                {
                    DoorStatus = Door.Open;
                }
                if (ContainsTure(args.NewValue, index7))
                {
                    DoorStatus = Door.Close;
                }
            }
        }

        private bool ContainsTure(IDictionary<int, bool> dic, int index)
        {
            return dic.ContainsKey(index) && dic[index];
        }
        public void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {

        }
    }
}