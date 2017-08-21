using System.ComponentModel.Composition;
using System.Globalization;
using MMI.Tester.BatchDataSender.Model;
using MMI.Tester.BatchDataSender.ValidationRules;

namespace MMI.Tester.BatchDataSender.ViewModel
{
    [Export]
    public class SelectTemplateDataViewModel
    {
        [ImportingConstructor]
        public SelectTemplateDataViewModel()
        {
            var vali = new TemplateDataFileValidationRule();

            BoolViewModel = new TempldateDataFileItemViewModel(SendDataType.InBool)
            {
                File =
                    vali.Validate(BatchSenderParam.Instance.Param.BoolFile, CultureInfo.CurrentCulture).IsValid
                        ? BatchSenderParam.Instance.Param.BoolFile
                        : null
            };
            FloatViewModel = new TempldateDataFileItemViewModel(SendDataType.InFloat)
            {
                File =
                    vali.Validate(BatchSenderParam.Instance.Param.FloatFile, CultureInfo.CurrentCulture).IsValid
                        ? BatchSenderParam.Instance.Param.FloatFile
                        : null
            };
        }

        public TempldateDataFileItemViewModel BoolViewModel { private set; get; }

        public TempldateDataFileItemViewModel FloatViewModel { private set; get; }

    }
}