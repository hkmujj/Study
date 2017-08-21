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
    [ExcelLocation("主界面各个子界面接口.xls", "Pantograph")]
    public class PantographUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {
        private Pantograph m_Status;

        public Pantograph Status
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
        [ExcelField("ConnectPower")]
        public string ConnectPower { get; set; }
        [ExcelField("ConnectUnPower")]
        public string ConnectUnPower { get; set; }
        [ExcelField("PantographUpFault")]
        public string PantographUpFault { get; set; }
        [ExcelField("PantographDownFault")]
        public string PantographDownFault { get; set; }
        [ExcelField("PantographUp")]
        public string PantographUp { get; set; }
        [ExcelField("PantographDown")]
        public string PantographDown { get; set; }
        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[ConnectPower];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[ConnectUnPower];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[PantographUpFault];
            var index4 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[PantographDownFault];
            var index5 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[PantographUp];
            var index6 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[PantographDown];

            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4 || a == index5 || a == index6))
            {
                Status = Pantograph.Normal;
                if (args.ContainKeyTrue(index1))
                {
                    Status = Pantograph.ConnectPower;
                }
                if (args.ContainKeyTrue(index2))
                {
                    Status = Pantograph.ConnectUnPower;
                }
                if (args.ContainKeyTrue(index3))
                {
                    Status = Pantograph.PantographUpFault;
                }
                if (args.ContainKeyTrue(index4))
                {
                    Status = Pantograph.PantographDownFault;
                }
                if (args.ContainKeyTrue(index5))
                {
                    Status = Pantograph.PantographUp;
                }
                if (args.ContainKeyTrue(index6))
                {
                    Status = Pantograph.PantographDown;
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

            }
        }
    }
}