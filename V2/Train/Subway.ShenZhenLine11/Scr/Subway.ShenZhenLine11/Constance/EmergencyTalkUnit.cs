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
    [ExcelLocation("主界面各个子界面接口.xls", "EmergencyTalk")]
    public class EmergencyTalkUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {
        private EmergncyTalk m_Status;

        public EmergncyTalk Status
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
        [ExcelField("Index")]
        public int Index { get; set; }
        [ExcelField("Falut")]
        public string Falut { get; set; }
        [ExcelField("FareActive")]
        public string FareActive { get; set; }
        [ExcelField("DriverActive")]
        public string DriverActive { get; set; }
        [ExcelField("UnActive")]
        public string UnActive { get; set; }


        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Falut];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[FareActive];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[DriverActive];
            var index4 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[UnActive];
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4))
            {
                Status = EmergncyTalk.Normal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Status = EmergncyTalk.Falut;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Status = EmergncyTalk.FareActive;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Status = EmergncyTalk.DriverActive;
                }
                if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    Status = EmergncyTalk.UnActive;
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
                case "Index":
                    Index = value.ToInt();
                    break;
                case "Falut":
                    Falut = value;
                    break;
                case "FareActive":
                    FareActive = value;
                    break;
                case "DriverActive":
                    DriverActive = value;
                    break;
                case "UnActive":
                    UnActive = value;
                    break;
            }
        }
    }
}