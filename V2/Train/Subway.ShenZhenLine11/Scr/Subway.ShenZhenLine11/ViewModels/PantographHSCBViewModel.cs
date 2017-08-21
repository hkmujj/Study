using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(PantographHSCBViewModel))]
    public class PantographHSCBViewModel : SubViewModelBase
    {
        public PantographHSCBViewModel()
        {
            var path = GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory;
            var tmp1 = ExcelParser.Parser<HightSpeedUnit>(path);
            var tmp2 = ExcelParser.Parser<PantographUnit>(path);
            HightSpeedcCollection = new ObservableCollection<HightSpeedUnit>(tmp1);
            PantographCollection = new ObservableCollection<PantographUnit>(tmp2);
            OnePantograph = new ObservableCollection<PantographUnit>(PantographCollection.Where(w => w.Location == 1).OrderBy(o => o.Car));
            TwoPantograph = new ObservableCollection<PantographUnit>(PantographCollection.Where(w => w.Location == 2).OrderBy(o => o.Car));
        }
        public ObservableCollection<HightSpeedUnit> HightSpeedcCollection { get; set; }
        public ObservableCollection<PantographUnit> PantographCollection { get; set; }

        public ObservableCollection<PantographUnit> OnePantograph { get; set; }

        public ObservableCollection<PantographUnit> TwoPantograph { get; set; }

        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(sender, args);
            foreach (var unit in HightSpeedcCollection)
            {
                unit.Changed(sender, args);
            }
            foreach (var unit in PantographCollection)
            {
                unit.Changed(sender, args);
            }
        }
    }
}