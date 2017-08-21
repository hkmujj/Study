using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Enum;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.Constance
{
    [ExcelLocation("主界面各个子界面接口.xls", "AssistPower")]
    public class AssistPowerUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {

        public AssistPowerUnit()
        {
            ACStatus = AssistPowerAC.Off;
            DCStatus = AssistPowerDC.Off;
        }
        private AssistPowerDC m_DCStatus;
        private AssistPowerAC m_ACStatus;

        public AssistPowerAC ACStatus
        {
            get { return m_ACStatus; }
            set
            {
                if (value == m_ACStatus)
                {
                    return;
                }
                m_ACStatus = value;
                RaisePropertyChanged(() => ACStatus);
            }
        }

        public AssistPowerDC DCStatus
        {
            get { return m_DCStatus; }
            set
            {
                if (value == m_DCStatus)
                {
                    return;
                }
                m_DCStatus = value;
                RaisePropertyChanged(() => DCStatus);
            }
        }
        [ExcelField("Car")]
        public int Car { get; set; }
        [ExcelField("Type")]
        public string Type { get; set; }
        [ExcelField("Fault")]
        public string Fault { get; set; }
        [ExcelField("Warn")]
        public string Warn { get; set; }
        [ExcelField("Run")]
        public string Run { get; set; }
        [ExcelField("Closed")]
        public string Off { get; set; }


        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {

            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Fault];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Warn];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Run];
            var index4 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Off];

            if (Type.Equals("AC"))
            {
                ChangedAC(args, index1, index2, index3, index4);
            }
            else if (Type.Equals("DC"))
            {
                ChangedDC(args, index1, index2, index3, index4);
            }
        }

        private void ChangedAC(CommunicationDataChangedArgs<bool> args, int index1, int index2, int index3, int index4)
        {
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4))
            {
                ACStatus = AssistPowerAC.Normal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    ACStatus = AssistPowerAC.Fault;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    ACStatus = AssistPowerAC.Warn;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    ACStatus = AssistPowerAC.Run;
                }
                if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    ACStatus = AssistPowerAC.Off;
                }
            }
        }
        private void ChangedDC(CommunicationDataChangedArgs<bool> args, int index1, int index2, int index3, int index4)
        {
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2 || a == index3 || a == index4))
            {
                DCStatus = AssistPowerDC.Normal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    DCStatus = AssistPowerDC.Fault;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    DCStatus = AssistPowerDC.Warn;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    DCStatus = AssistPowerDC.Run;
                }
                if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    DCStatus = AssistPowerDC.Off;
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
                case "Type":
                    Type = value;
                    break;
                case "Fault":
                    Fault = value;
                    break;
                case "Warn":
                    Warn = value;
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