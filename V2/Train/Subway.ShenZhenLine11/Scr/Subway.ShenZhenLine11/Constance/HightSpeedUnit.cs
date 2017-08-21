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
    [ExcelLocation("主界面各个子界面接口.xls", "HightSpeed")]
    public class HightSpeedUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {
        private HighSpeed m_Status;

        public HighSpeed Status
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
        [ExcelField("Closed")]
        public string Closed { get; set; }
        [ExcelField("Opened")]
        public string Opened { get; set; }
        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var idnex1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Closed];
            var idnex2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Opened];
            if (args.NewValue.Keys.Any(a => a == idnex1 || a == idnex2))
            {
                Status = HighSpeed.Normal;
                if (args.ContainKeyTrue(idnex1))
                {
                    Status = HighSpeed.Close;
                }
                if (args.ContainKeyTrue(idnex2))
                {
                    Status = HighSpeed.Open;
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