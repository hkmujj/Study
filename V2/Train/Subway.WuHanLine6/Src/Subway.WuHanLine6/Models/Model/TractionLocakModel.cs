using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.WuHanLine6.Models.Units;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 牵引封锁界面
    /// </summary>
    [Export]
    public class TractionLocakModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TractionLocakModel()
        {
            AllTractionLockUnits = new ObservableCollection<TractionLockUnit>(GlobalParams.Instance.TractionLockUnits);
        }

        /// <summary>
        /// 所有牵引封锁状态
        /// </summary>
        public ObservableCollection<TractionLockUnit> AllTractionLockUnits { get; set; }

        /// <summary>
        /// 列1状态
        /// </summary>
        public IEnumerable<TractionLockUnit> ColumOneUnit
        {
            get
            {
                return AllTractionLockUnits.Where(w => w.Columm == 1).OrderBy(o => o.Row);
            }
        }

        /// <summary>
        /// 列2状态
        /// </summary>
        public IEnumerable<TractionLockUnit> ColumTwoUnit
        {
            get
            {
                return AllTractionLockUnits.Where(w => w.Columm == 2).OrderBy(o => o.Row);
            }
        }

        /// <summary>
        /// 列3状态
        /// </summary>
        public IEnumerable<TractionLockUnit> ColumThreeUnit
        {
            get
            {
                return AllTractionLockUnits.Where(w => w.Columm == 3).OrderBy(o => o.Row);
            }
        }
    }
}