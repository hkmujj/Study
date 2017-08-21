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
    [Export(typeof(BrakeViewModel))]
    public class BrakeViewModel : SubViewModelBase
    {
        public BrakeViewModel()
        {
            var tmp = ExcelParser.Parser<BrakeUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            BrakeCollection = new ObservableCollection<BrakeUnit>(tmp);
        }
        public ObservableCollection<BrakeUnit> BrakeCollection { get; set; }

        public List<BrakeUnit> LocationOne
        {
            get
            {
                return BrakeCollection.Where(w => w.Location == 1).OrderBy(o => o.Car).ToList();
            }
        }
        public List<BrakeUnit> LocationTwo
        {
            get
            {
                return BrakeCollection.Where(w => w.Location == 2).OrderBy(o => o.Car).ToList();
            }
        }
        public List<BrakeUnit> LocationThree
        {
            get
            {
                return BrakeCollection.Where(w => w.Location == 3).OrderBy(o => o.Car).ToList();
            }
        }

        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(sender, args);
            foreach (var unit in BrakeCollection)
            {
                unit.Changed(sender, args);
            }
        }
    }
}