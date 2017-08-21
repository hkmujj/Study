using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 旁路ViewModel
    /// </summary>
    [Export]
    public class BypassViewModel : ViewModelBase
    {
        private ObservableCollection<BypassUnit> m_AllUnits;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BypassViewModel()
        {
            AllUnits = new ObservableCollection<BypassUnit>(GlobalParam.Instance.AllBypassUnits);
        }
        /// <summary>
        /// 所有旁路单元
        /// </summary>
        public ObservableCollection<BypassUnit> AllUnits
        {
            get { return m_AllUnits; }
            set
            {
                if (Equals(value, m_AllUnits))
                {
                    return;
                }
                m_AllUnits = value;
                RaisePropertyChanged(() => AllUnits);
                RaisePropertyChanged(() => LeftUnit);
                RaisePropertyChanged(() => RightUnit);
                RaisePropertyChanged(() => BrakeCutUnit);
            }
        }

        /// <summary>
        /// 左边的旁路
        /// </summary>
        public IEnumerable<BypassUnit> LeftUnit { get { return AllUnits.Where(w => w.Index <= 7 && w.Type == BypassType.Type1); } }
        /// <summary>
        /// 右边的旁路
        /// </summary>
        public IEnumerable<BypassUnit> RightUnit { get { return AllUnits.Where(w => w.Index > 7 && w.Type == BypassType.Type1); } }
        /// <summary>
        /// 整车制动切除
        /// </summary>
        public IEnumerable<BypassUnit> BrakeCutUnit { get { return AllUnits.Where(w => w.Index <= 6 && w.Type == BypassType.Type2); } }
    }
}