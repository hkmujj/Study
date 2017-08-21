using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(BypassViewModel))]
    public class BypassViewModel : SubViewModelBase
    {
        public BypassViewModel()
        {
            var tmp = ExcelParser.Parser<BypassUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            BypassUnits = new ObservableCollection<BypassUnit>(tmp);
        }

        public ObservableCollection<BypassUnit> BypassUnits { get; private set; }
        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            foreach (var unit in BypassUnits)
            {
                unit.Changed(sender, args);
            }
        }
    }
}