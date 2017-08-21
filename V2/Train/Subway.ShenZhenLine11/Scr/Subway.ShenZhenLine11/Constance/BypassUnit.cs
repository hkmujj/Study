using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.Constance
{

    [ExcelLocation("主界面各个子界面接口.xls", "Bypass")]
    public class BypassUnit : NotificationObject, IStatusChanged, ISetValueProvider
    {
        private string m_Name;
        private bool m_CarA2Bypass;
        private bool m_CarA1Bypass;

        public bool CarA1Bypass
        {
            get { return m_CarA1Bypass; }
            set
            {
                if (value == m_CarA1Bypass)
                {
                    return;
                }
                m_CarA1Bypass = value;
                RaisePropertyChanged(() => CarA1Bypass);
            }
        }

        public bool CarA2Bypass
        {
            get { return m_CarA2Bypass; }
            set
            {
                if (value == m_CarA2Bypass)
                {
                    return;
                }
                m_CarA2Bypass = value;
                RaisePropertyChanged(() => CarA2Bypass);
            }
        }

        [ExcelField("Name")]
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value == m_Name)
                {
                    return;
                }
                m_Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        [ExcelField("Car1Logic")]
        public string Car1Logic { get; set; }
        [ExcelField("Car2Logic")]
        public string Car2Logic { get; set; }

        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            var idnex1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Car1Logic];
            if (args.NewValue.ContainsKey(idnex1))
            {
                CarA1Bypass = args.NewValue[idnex1];
            }
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[Car2Logic];
            if (args.NewValue.ContainsKey(index2))
            {
                CarA2Bypass = args.NewValue[index2];
            }
        }

        public void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {

        }

        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}