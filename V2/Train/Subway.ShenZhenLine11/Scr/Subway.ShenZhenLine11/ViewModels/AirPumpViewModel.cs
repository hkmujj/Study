using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(AirPumpViewModel))]
    public class AirPumpViewModel : SubViewModelBase
    {
        public AirPumpViewModel()
        {
            var tmp = ExcelParser.Parser<AirPumpUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            AirPumpCollection = new ObservableCollection<AirPumpUnit>(tmp);
        }
        public ObservableCollection<AirPumpUnit> AirPumpCollection { get; set; }
        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(sender, args);
            foreach (var unit in AirPumpCollection)
            {
                unit.Changed(sender, args);
            }
        }
    }
}