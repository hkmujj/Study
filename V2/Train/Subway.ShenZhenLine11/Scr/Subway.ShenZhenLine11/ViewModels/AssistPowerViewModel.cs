using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(AssistPowerViewModel))]
    public class AssistPowerViewModel : NotificationObject, IStatusChanged
    {
        [ImportingConstructor]
        public AssistPowerViewModel()
        {
            var tmp =
                ExcelParser.Parser<AssistPowerUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            AssistPowerCollection = new ObservableCollection<AssistPowerUnit>(tmp);
        }
        public ObservableCollection<AssistPowerUnit> AssistPowerCollection { get; set; }
        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            foreach (var unit in AssistPowerCollection)
            {
                unit.Changed(sender, args);
            }
        }

        public void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {

        }
    }
}