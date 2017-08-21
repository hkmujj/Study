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
    [ExcelLocation("主界面各个子界面接口.xls", "AirCondition")]
    public class AirConditionUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {
        public AirConditionUnit()
        {
            Status = AirCondition.Off;
        }
        private AirCondition m_Status;

        public AirCondition Status
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
        [ExcelField("Car")]
        public int Car { get; set; }
        [ExcelField("Location")]
        public int Location { get; set; }
        [ExcelField("Fault")]
        public string Fault { get; set; }
        [ExcelField("Warn")]
        public string Warn { get; set; }
        [ExcelField("Emergency")]
        public string Emergency { get; set; }
        [ExcelField("Air")]
        public string Air { get; set; }
        [ExcelField("Run")]
        public string Run { get; set; }
        [ExcelField("Closed")]
        public string Off { get; set; }



        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Fault];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Warn];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Emergency];
            var index4 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Air];
            var index5 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Run];
            var index6 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Off];
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4 || a == index5 || a == index6))
            {
                Status = AirCondition.Normal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Status = AirCondition.Falut;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Status = AirCondition.Warn;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Status = AirCondition.EmergencyAir;
                }
                if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    Status = AirCondition.Air;
                }
                if (args.NewValue.ContainsKey(index5) && args.NewValue[index5])
                {
                    Status = AirCondition.Run;
                }
                if (args.NewValue.ContainsKey(index6) && args.NewValue[index6])
                {
                    Status = AirCondition.Off;
                }
            }
        }

        public void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {

        }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Car":
                    Car = value.ToInt();
                    break;
                case "Location":
                    Location = value.ToInt();
                    break;
                case "Fault":
                    Fault = value;
                    break;
                case "Warn":
                    Warn = value;
                    break;
                case "Emergency":
                    Emergency = value;
                    break;
                case "Air":
                    Air = value;
                    break;
                case "Run":
                    Run = value;
                    break;
                case "Off":
                    Off = value;
                    break;
            }
        }
    }
}