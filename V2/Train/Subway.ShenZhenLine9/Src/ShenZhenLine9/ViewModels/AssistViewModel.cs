using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class AssistViewModel : ViewModelBase
    {
        private ObservableCollection<AssistUnit> m_AssistUnits;
        private Generatrix m_Generatrix3;
        private Generatrix m_Generatrix2;
        private Generatrix m_Generatrix1;
        private bool m_PowerGrid;

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

        public Generatrix Generatrix1
        {
            get { return m_Generatrix1; }
            set
            {
                if (value == m_Generatrix1)
                    return;

                m_Generatrix1 = value;
                RaisePropertyChanged(() => Generatrix1);
            }
        }

        public Generatrix Generatrix2
        {
            get { return m_Generatrix2; }
            set
            {
                if (value == m_Generatrix2)
                    return;

                m_Generatrix2 = value;
                RaisePropertyChanged(() => Generatrix2);
            }
        }

        public Generatrix Generatrix3
        {
            get { return m_Generatrix3; }
            set
            {
                if (value == m_Generatrix3)
                    return;

                m_Generatrix3 = value;
                RaisePropertyChanged(() => Generatrix3);
            }
        }
        /// <summary>
        /// 并网供电
        /// </summary>
        public bool PowerGrid
        {
            get { return m_PowerGrid; }
            set
            {
                if (value == m_PowerGrid)
                    return;

                m_PowerGrid = value;
                RaisePropertyChanged(() => PowerGrid);
            }
        }
    }
}