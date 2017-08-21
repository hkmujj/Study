using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 紧急对讲
    /// </summary>
    [Export]
    public class EmergencyTalkViewModel : ViewModelBase
    {
        private ObservableCollection<EnmergencyTalkUnit> m_EnmergencyTalkUnits;

        /// <summary>
        /// 
        /// </summary>
        public EmergencyTalkViewModel()
        {
            EnmergencyTalkUnits = new ObservableCollection<EnmergencyTalkUnit>(GlobalParam.Instance.EnmergencyTalkUnits);
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<EnmergencyTalkUnit> EnmergencyTalkUnits
        {
            get { return m_EnmergencyTalkUnits; }
            private set
            {
                if (Equals(value, m_EnmergencyTalkUnits))
                {
                    return;
                }
                m_EnmergencyTalkUnits = value;
                RaisePropertyChanged(() => EnmergencyTalkUnits);
                RaisePropertyChanged(() => Car1Top);
                RaisePropertyChanged(() => Car1Buttom);
                RaisePropertyChanged(() => Car2Top);
                RaisePropertyChanged(() => Car2Buttom);
                RaisePropertyChanged(() => Car3Top);
                RaisePropertyChanged(() => Car3Buttom);
                RaisePropertyChanged(() => Car4Top);
                RaisePropertyChanged(() => Car4Buttom);
                RaisePropertyChanged(() => Car5Top);
                RaisePropertyChanged(() => Car5Buttom);
                RaisePropertyChanged(() => Car6Top);
                RaisePropertyChanged(() => Car6Buttom);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car1Top
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 1 && w.Location % 2 == 0).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car1Buttom
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 1 && w.Location % 2 != 0).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car2Top
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 2 && w.Location % 2 == 0).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car2Buttom
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 2 && w.Location % 2 != 0).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car3Top
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 3 && w.Location % 2 == 0).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car3Buttom
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 3 && w.Location % 2 != 0).OrderBy(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car4Top
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 4 && w.Location % 2 != 0).OrderByDescending(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car4Buttom
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 4 && w.Location % 2 == 0).OrderByDescending(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car5Top
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 5 && w.Location % 2 != 0).OrderByDescending(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car5Buttom
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 5 && w.Location % 2 == 0).OrderByDescending(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car6Top
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 6 && w.Location % 2 != 0).OrderByDescending(o => o.Location);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<EnmergencyTalkUnit> Car6Buttom
        {
            get
            {
                return EnmergencyTalkUnits.Where(w => w.Car == 6 && w.Location % 2 == 0).OrderByDescending(o => o.Location);
            }
        }
    }
}