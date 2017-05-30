using System.Collections.Generic;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Engine.TCMS.Turkmenistan.Extension;

namespace Engine.TCMS.Turkmenistan.Model
{
    [ExcelLocation("故障切除选项.xls", "Sheet1")]
    public class FaultCutUnit : NotificationObject, ISetValueProvider
    {
        private string _dispalyText;
        private bool _isCut;

        [ExcelField("故障切除接口")]
        public string OutCutName { get; set; }
        [ExcelField("故障恢复切除接口")]
        public string OutRestCutName { get; set; }
        [ExcelField("故障当前状态接口")]
        public string InName { get; set; }
        [ExcelField("切除显示中文文字")]
        public string CutChineseName { get; set; }
        [ExcelField("未切除显示中文文字")]
        public string UnCutChineseName { get; set; }
        [ExcelField("切除显示土库曼斯坦文字")]
        public string CutThurkmenistanName { get; set; }
        [ExcelField("未切除显示土库曼斯坦文字")]
        public string UnCutThurkmenistanName { get; set; }

        public bool IsCut
        {
            get { return _isCut; }
            set
            {
                if (value == _isCut) return;
                _isCut = value;
                ChangedDispaly();
                RaisePropertyChanged(() => IsCut);
            }
        }

        public void ChangedData(CommunicationDataChangedArgs<bool> args)
        {
            args.UpdateIfContains(InName, b => IsCut = b);
        }
        public void ChangedDispaly()
        {
            DispalyText = GlobalParam.Instance.IsTurkmenistan
                ? (IsCut ? CutThurkmenistanName : UnCutThurkmenistanName)
                : (IsCut ? CutChineseName : UnCutChineseName);
        }
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

        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}