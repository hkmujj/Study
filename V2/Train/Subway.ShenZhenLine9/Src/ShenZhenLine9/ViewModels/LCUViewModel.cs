using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.ViewModels
{
    [Export]
    public class LCUViewModel : ViewModelBase
    {
        public LCUViewModel()
        {
            AllUnit = new List<LCUUnit>(GlobalParam.Instance.LCUUnit);
        }
        private List<LCUUnit> m_AllUnit;

        public List<LCUUnit> AllUnit
        {
            get { return m_AllUnit; }
            set
            {
                if (Equals(value, m_AllUnit))
                    return;

                m_AllUnit = value;
                RaisePropertyChanged(() => AllUnit);
            }
        }

        public IEnumerable<LCUUnit> Car1Unit { get { return AllUnit.Where(w => w.Car == 1).OrderBy(o => o.Location); } }
        public IEnumerable<LCUUnit> Car2Unit { get { return AllUnit.Where(w => w.Car == 2).OrderBy(o => o.Location); } }
        public IEnumerable<LCUUnit> Car3Unit { get { return AllUnit.Where(w => w.Car == 3).OrderBy(o => o.Location); } }
        public IEnumerable<LCUUnit> Car4Unit { get { return AllUnit.Where(w => w.Car == 4).OrderBy(o => o.Location); } }
        public IEnumerable<LCUUnit> Car5Unit { get { return AllUnit.Where(w => w.Car == 5).OrderBy(o => o.Location); } }
        public IEnumerable<LCUUnit> Car6Unit { get { return AllUnit.Where(w => w.Car == 6).OrderBy(o => o.Location); } }
    }
}
