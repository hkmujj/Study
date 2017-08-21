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
    [Export(typeof(EmergencyTalkViewModel))]
    public class EmergencyTalkViewModel : SubViewModelBase
    {
        public EmergencyTalkViewModel()
        {
            var tmp =
                ExcelParser.Parser<EmergencyTalkUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            EmergencyTalkcCollection = new ObservableCollection<EmergencyTalkUnit>(tmp);
        }
        public ObservableCollection<EmergencyTalkUnit> EmergencyTalkcCollection { get; set; }
        public IEnumerable<EmergencyTalkUnit> CarOneTop
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 1 && w.Index < 3);
            }
        }

        public IEnumerable<EmergencyTalkUnit> CarOneDown
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 1 && w.Index >= 3).OrderByDescending(o => o.Index);
            }
        }
        public IEnumerable<EmergencyTalkUnit> CarTwoTop
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 2 && w.Index < 3);
            }
        }

        public IEnumerable<EmergencyTalkUnit> CarTwoDown
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 2 && w.Index >= 3).OrderByDescending(o => o.Index);
            }
        }
        public IEnumerable<EmergencyTalkUnit> CarThreeTop
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 3 && w.Index < 3);
            }
        }

        public IEnumerable<EmergencyTalkUnit> CarThreeDown
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 3 && w.Index >= 3).OrderByDescending(o => o.Index);
            }
        }
        public IEnumerable<EmergencyTalkUnit> CarFourTop
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 4 && w.Index < 3);
            }
        }

        public IEnumerable<EmergencyTalkUnit> CarFourDown
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 4 && w.Index >= 3).OrderByDescending(o => o.Index);
            }
        }
        public IEnumerable<EmergencyTalkUnit> CarFiveDown
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 5 && w.Index < 3).OrderByDescending(o => o.Index);
            }
        }

        public IEnumerable<EmergencyTalkUnit> CarFiveTop
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 5 && w.Index >= 3);
            }
        }
        public IEnumerable<EmergencyTalkUnit> CarSixDown
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 6 && w.Index < 3).OrderByDescending(o => o.Index);
            }
        }

        public IEnumerable<EmergencyTalkUnit> CarSixTop
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 6 && w.Index >= 3);
            }
        }
        public IEnumerable<EmergencyTalkUnit> CarSevenDown
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 7 && w.Index < 3).OrderByDescending(o => o.Index);
            }
        }

        public IEnumerable<EmergencyTalkUnit> CarSevenTop
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 7 && w.Index >= 3);
            }
        }
        public IEnumerable<EmergencyTalkUnit> CarEightDown
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 8 && w.Index < 3).OrderByDescending(o => o.Index);
            }
        }

        public IEnumerable<EmergencyTalkUnit> CarEightTop
        {
            get
            {
                return EmergencyTalkcCollection.Where(w => w.Car == 8 && w.Index >= 3);
            }
        }
        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(sender, args);
            foreach (var unit in EmergencyTalkcCollection)
            {
                unit.Changed(sender, args);
            }
        }
    }
}