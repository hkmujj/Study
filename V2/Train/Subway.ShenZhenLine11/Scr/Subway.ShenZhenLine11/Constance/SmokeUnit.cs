using System.Linq;
using CommonUtil.Annotations;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Enum;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.Constance
{
    [ExcelLocation("主界面各个子界面接口.xls", "Smoke")]
    public class SmokeUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {
        private Smoke m_Status;

        public Smoke Status
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
        [ExcelField("Smoke")]
        public string Smoke { get; set; }
        [ExcelField("Fault")]
        public string Fault { get; set; }
        [ExcelField("NoSmoke")]
        public string NoSmoke { get; set; }
        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Smoke];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Fault];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[NoSmoke];
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3))
            {
                Status = Enum.Smoke.Normal;
                if (args.ContainKeyTrue(index1))
                {
                    Status = Enum.Smoke.Smoke;
                }
                if (args.ContainKeyTrue(index2))
                {
                    Status = Enum.Smoke.Fault;
                }
                if (args.ContainKeyTrue(index3))
                {
                    Status = Enum.Smoke.NoSmoke;
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
            }
        }
    }
}