using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.ShenZhenLine7.Models;
using Subway.ShenZhenLine7.Models.Units;

namespace Subway.ShenZhenLine7.ViewModels
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
            }
        }

        /// <summary>
        /// 左边的旁路
        /// </summary>
        public IEnumerable<BypassUnit> LeftUnit { get { return AllUnits.Where(w => w.Index <= 6); } }
        /// <summary>
        /// 右边的旁路
        /// </summary>
        public IEnumerable<BypassUnit> RightUnit { get { return AllUnits.Where(w => w.Index > 6); } }
    }
}