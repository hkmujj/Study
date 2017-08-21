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
    [Export(typeof(AirConditionViewModel))]
    public class AirConditionViewModel : NotificationObject, IStatusChanged
    {
        [ImportingConstructor]
        public AirConditionViewModel()
        {
            var tmp =
                ExcelParser.Parser<AirConditionUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            AirConditioncCollection = new ObservableCollection<AirConditionUnit>(tmp);
        }
        public ObservableCollection<AirConditionUnit> AirConditioncCollection { get; set; }

        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            foreach (var unit in AirConditioncCollection)
            {
                unit.Changed(sender, args);
            }
        }

        public void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {

        }
    }
}