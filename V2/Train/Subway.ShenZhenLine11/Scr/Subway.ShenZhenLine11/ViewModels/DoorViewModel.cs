using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(DoorViewModel))]
    public class DoorViewModel : NotificationObject, IStatusChanged
    {
        [ImportingConstructor]
        public DoorViewModel()
        {
            var tmp = ExcelParser.Parser<DoorUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            DoorCollection = new ObservableCollection<DoorUnit>(tmp);
        }
        public ObservableCollection<DoorUnit> DoorCollection { get; set; }
        public IEnumerable<DoorUnit> DoorOneTop { get { return DoorCollection.Where(w => w.Car == 1 && w.DoorIndex % 2 == 0); } }
        public IEnumerable<DoorUnit> DoorTwoTop { get { return DoorCollection.Where(w => w.Car == 2 && w.DoorIndex % 2 == 0); } }
        public IEnumerable<DoorUnit> DoorThreeTop { get { return DoorCollection.Where(w => w.Car == 3 && w.DoorIndex % 2 == 0); } }
        public IEnumerable<DoorUnit> DoorFourTop { get { return DoorCollection.Where(w => w.Car == 4 && w.DoorIndex % 2 == 0); } }
        public IEnumerable<DoorUnit> DoorFiveTop { get { return DoorCollection.Where(w => w.Car == 5 && w.DoorIndex % 2 != 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> DoorSixTop { get { return DoorCollection.Where(w => w.Car == 6 && w.DoorIndex % 2 != 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> DoorSevenTop { get { return DoorCollection.Where(w => w.Car == 7 && w.DoorIndex % 2 != 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> DoorEightTop { get { return DoorCollection.Where(w => w.Car == 8 && w.DoorIndex % 2 != 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> DoorOneBottom { get { return DoorCollection.Where(w => w.Car == 1 && w.DoorIndex % 2 != 0); } }
        public IEnumerable<DoorUnit> DoorTwoBottom { get { return DoorCollection.Where(w => w.Car == 2 && w.DoorIndex % 2 != 0); } }
        public IEnumerable<DoorUnit> DoorThreeBottom { get { return DoorCollection.Where(w => w.Car == 3 && w.DoorIndex % 2 != 0); } }
        public IEnumerable<DoorUnit> DoorFourBottom { get { return DoorCollection.Where(w => w.Car == 4 && w.DoorIndex % 2 != 0); } }
        public IEnumerable<DoorUnit> DoorFiveBottom { get { return DoorCollection.Where(w => w.Car == 5 && w.DoorIndex % 2 == 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> DoorSixBottom { get { return DoorCollection.Where(w => w.Car == 6 && w.DoorIndex % 2 == 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> DoorSevenBottom { get { return DoorCollection.Where(w => w.Car == 7 && w.DoorIndex % 2 == 0).OrderByDescending(o => o.DoorIndex); } }
        public IEnumerable<DoorUnit> DoorEightBottom { get { return DoorCollection.Where(w => w.Car == 8 && w.DoorIndex % 2 == 0).OrderByDescending(o => o.DoorIndex); } }

        public void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            foreach (var unit in DoorCollection)
            {
                unit.Changed(sender, args);
            }
        }

        public void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {

        }
    }
}