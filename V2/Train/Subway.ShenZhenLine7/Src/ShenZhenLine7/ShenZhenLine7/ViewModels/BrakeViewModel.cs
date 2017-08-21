using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.ShenZhenLine7.Models;
using Subway.ShenZhenLine7.Models.Units;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class BrakeViewModel : ViewModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public BrakeViewModel()
        {
            BrakeUnits = new ObservableCollection<BrakeUnit>(GlobalParam.Instance.BrakeUnits);
        }
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<BrakeUnit> BrakeUnits { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car1Top { get { return BrakeUnits.Where(w => w.Car == 1 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car2Top { get { return BrakeUnits.Where(w => w.Car == 2 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car3Top { get { return BrakeUnits.Where(w => w.Car == 3 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car4Top { get { return BrakeUnits.Where(w => w.Car == 4 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car5Top { get { return BrakeUnits.Where(w => w.Car == 5 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car6Top { get { return BrakeUnits.Where(w => w.Car == 6 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car1Buttom { get { return BrakeUnits.Where(w => w.Car == 1 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car2Buttom { get { return BrakeUnits.Where(w => w.Car == 2 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car3Buttom { get { return BrakeUnits.Where(w => w.Car == 3 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car4Buttom { get { return BrakeUnits.Where(w => w.Car == 4 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car5Buttom { get { return BrakeUnits.Where(w => w.Car == 5 && w.Location != 1); } }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BrakeUnit> Car6Buttom { get { return BrakeUnits.Where(w => w.Car == 6 && w.Location != 1); } }
    }
}