using System.Diagnostics;
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
    [ExcelLocation("主界面各个子界面接口.xls", "Traction")]
    public class TractionUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {
        private Traction m_Status;

        public Traction Status
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
        [ExcelField("Fault")]
        public string Fault { get; set; }
        [ExcelField("Warn")]
        public string Warn { get; set; }
        [ExcelField("Active")]
        public string Active { get; set; }
        [ExcelField("Closed")]
        public string Closed { get; set; }

        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Fault];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Warn];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Active];
            var index4 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Closed];
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4))
            {
                Status = Traction.Normal;
                if (args.ContainKeyTrue(index1))
                {
                    Status = Traction.Fault;
                }
                if (args.ContainKeyTrue(index2))
                {
                    Status = Traction.Warn;
                }
                if (args.ContainKeyTrue(index3))
                {
                    Status = Traction.Active;
                }
                if (args.ContainKeyTrue(index4))
                {
                    Status = Traction.Off;
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