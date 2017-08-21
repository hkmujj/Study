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
    [ExcelLocation("主界面各个子界面接口.xls", "Brake")]
    public class BrakeUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {
        private Brake m_Status;

        public Brake Status
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
        [ExcelField("Park")]
        public string Park { get; set; }
        [ExcelField("Cut")]
        public string Cut { get; set; }
        [ExcelField("SeverityFault")]
        public string SeverityFault { get; set; }
        [ExcelField("MediumFaulr")]
        public string MediumFaulr { get; set; }
        [ExcelField("BrakeInfliction")]
        public string BrakeInfliction { get; set; }
        [ExcelField("BarkeRemit")]
        public string BarkeRemit { get; set; }



        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Park];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Cut];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[SeverityFault];
            var index4 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[MediumFaulr];
            var index5 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[BrakeInfliction];
            var index6 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[BarkeRemit];
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4 || a == index5 || a == index6))
            {
                Status = Brake.Normal;
                if (ContainKeyTrue(args, index1))
                {
                    Status = Brake.Park;
                }
                else if (ContainKeyTrue(args, index2))
                {
                    Status = Brake.Cut;
                }
                else if (ContainKeyTrue(args, index3))
                {
                    Status = Brake.SeverityFault;
                }
                else if (ContainKeyTrue(args, index4))
                {
                    Status = Brake.MediumFaulr;
                }
                else if (ContainKeyTrue(args, index5))
                {
                    Status = Brake.BrakeInfliction;
                }
                else if (ContainKeyTrue(args, index6))
                {
                    Status = Brake.BarkeRemit;
                }
            }
        }

        private bool ContainKeyTrue(CommunicationDataChangedArgs<bool> args, int index)
        {
            return args.NewValue.ContainsKey(index) && args.NewValue[index];
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

            }
        }
    }
}