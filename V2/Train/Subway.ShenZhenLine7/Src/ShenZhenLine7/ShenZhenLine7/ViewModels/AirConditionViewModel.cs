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
    public class AirConditionViewModel : ViewModelBase
    {
        private ObservableCollection<AirConditionUnit> m_AllAirConditionUnits;

        /// <summary>
        /// 
        /// </summary>
        public AirConditionViewModel()
        {
            AllAirConditionUnits = new ObservableCollection<AirConditionUnit>(GlobalParam.Instance.AirConditionUnits);
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<AirConditionUnit> AllAirConditionUnits
        {
            get { return m_AllAirConditionUnits; }
            set
            {
                if (Equals(value, m_AllAirConditionUnits))
                {
                    return;
                }
                m_AllAirConditionUnits = value;
                RaisePropertyChanged(() => AllAirConditionUnits);
                RaisePropertyChanged(() => Car1AirConditionUnits);
                RaisePropertyChanged(() => Car2AirConditionUnits);
                RaisePropertyChanged(() => Car3AirConditionUnits);
                RaisePropertyChanged(() => Car4AirConditionUnits);
                RaisePropertyChanged(() => Car5AirConditionUnits);
                RaisePropertyChanged(() => Car6AirConditionUnits);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AirConditionUnit> Car1AirConditionUnits
        {
            get
            {
                return AllAirConditionUnits.Where(w => w.Car == 1).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AirConditionUnit> Car2AirConditionUnits
        {
            get
            {
                return AllAirConditionUnits.Where(w => w.Car == 2).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AirConditionUnit> Car3AirConditionUnits
        {
            get
            {
                return AllAirConditionUnits.Where(w => w.Car == 3).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AirConditionUnit> Car4AirConditionUnits
        {
            get
            {
                return AllAirConditionUnits.Where(w => w.Car == 4).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AirConditionUnit> Car5AirConditionUnits
        {
            get
            {
                return AllAirConditionUnits.Where(w => w.Car == 5).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AirConditionUnit> Car6AirConditionUnits
        {
            get
            {
                return AllAirConditionUnits.Where(w => w.Car == 6).OrderBy(o => o.Location);
            }
        }

    }
}