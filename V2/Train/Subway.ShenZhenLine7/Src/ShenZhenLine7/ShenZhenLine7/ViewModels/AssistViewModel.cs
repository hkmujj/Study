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
    public class AssistViewModel : ViewModelBase
    {
        private ObservableCollection<AssistUnit> m_AssistUnits;

        /// <summary>
        /// 
        /// </summary>
        public AssistViewModel()
        {
            AssistUnits = new ObservableCollection<AssistUnit>(GlobalParam.Instance.AssistUnits);
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<AssistUnit> AssistUnits
        {
            get { return m_AssistUnits; }
            set
            {
                if (Equals(value, m_AssistUnits))
                {
                    return;
                }
                m_AssistUnits = value;
                RaisePropertyChanged(() => AssistUnits);
                RaisePropertyChanged(() => Car1AC);
                RaisePropertyChanged(() => Car1DC);
                RaisePropertyChanged(() => Car3AC);
                RaisePropertyChanged(() => Car4AC);
                RaisePropertyChanged(() => Car6AC);
                RaisePropertyChanged(() => Car6DC);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AssistUnit Car1AC { get { return AssistUnits.FirstOrDefault(w => w.Car == 1 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public AssistUnit Car1DC { get { return AssistUnits.FirstOrDefault(w => w.Car == 1 && w.Location == 2); } }
        /// <summary>
        /// 
        /// </summary>
        public AssistUnit Car3AC { get { return AssistUnits.FirstOrDefault(w => w.Car == 3 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public AssistUnit Car4AC { get { return AssistUnits.FirstOrDefault(w => w.Car == 4 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public AssistUnit Car6AC { get { return AssistUnits.FirstOrDefault(w => w.Car == 6 && w.Location == 1); } }
        /// <summary>
        /// 
        /// </summary>
        public AssistUnit Car6DC { get { return AssistUnits.FirstOrDefault(w => w.Car == 6 && w.Location == 2); } }

    }
}