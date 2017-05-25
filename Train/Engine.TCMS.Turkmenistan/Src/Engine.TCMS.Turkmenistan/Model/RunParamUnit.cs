using Engine.TCMS.Turkmenistan.Extension;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.Turkmenistan.Model
{
    [ExcelLocation("运行参数表格.xls", "Sheet1")]
    public class RunParamUnit : NotificationObject, ISetValueProvider
    {
        private string _dispalyText;
        [ExcelField("中文字符串")]
        public string ChineseFormat { get; set; }
        [ExcelField("俄语字符串")]
        public string RussianFormat { get; set; }

        public string DispalyText
        {
            get { return _dispalyText; }
            set
            {
                if (value == _dispalyText) return;
                _dispalyText = value;
                RaisePropertyChanged(() => DispalyText);
            }
        }
        [ExcelField("对应接口")]
        public string LogicName { get; set; }
        [ExcelField("本车他车")]
        public bool IsCurrent { get; set; }
        public void SetValue(string propertyOrFieldName, string value)
        {

        }

        public void UpdateLanguage()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                DispalyText = string.Format(GlobalParam.Instance.IsTurkmenistan ? RussianFormat : ChineseFormat, GlobalParam.Instance.InitParam.CommunicationDataService.GetInBoolOf(LogicName));
            }
        }
    }
}