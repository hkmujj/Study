using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [ExcelLocation("紧急原因界面内容和接口.xls", "Sheet1")]
    public class EnmergencyCauseUnit : NotificationObject, ISetValueProvider
    {
        private bool m_IsEmergency;

        [ExcelField("Name")]
        public string Name { get; set; }
        [ExcelField("LogicName")]
        public string LogicName { get; set; }

        public bool IsEmergency
        {
            get { return m_IsEmergency; }
            set
            {
                if (value == m_IsEmergency)
                {
                    return;
                }
                m_IsEmergency = value;
                RaisePropertyChanged(() => IsEmergency);
            }
        }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Name":
                    Name = value;
                    break;
                case "LogicName":
                    LogicName = value;
                    break;
            }
        }
    }
}