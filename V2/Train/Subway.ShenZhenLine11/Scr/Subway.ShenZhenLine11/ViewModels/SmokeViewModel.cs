using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using CommonUtil.Annotations;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(SmokeViewModel))]
    public class SmokeViewModel : SubViewModelBase
    {
        public SmokeViewModel()
        {
            var tmp = ExcelParser.Parser<SmokeUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            SmokeCollection = new ObservableCollection<SmokeUnit>(tmp);
        }
        public ObservableCollection<SmokeUnit> SmokeCollection { get; set; }
        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(sender, args);
            foreach (var unit in SmokeCollection)
            {
                unit.Changed(sender, args);
            }
        }
    }
}