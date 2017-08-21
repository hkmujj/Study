using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Infrasturcture.Model.Fault
{
    /// <summary>
    /// 雷达信息
    /// </summary>
    public class RadarInfo : NotificationObject
    {

        private ObservableCollection<RadarTarget> m_allRadarTargets;


        public RadarInfo()
        {
            AllRadarTargets = new ObservableCollection<RadarTarget>();
        }

        /// <summary>
        /// 所有雷达目标集合
        /// </summary>
        public ObservableCollection<RadarTarget> AllRadarTargets
        {
            get { return m_allRadarTargets; }
            set
            {
                if (Equals(value, m_allRadarTargets))
                    return;
                m_allRadarTargets = value;
                RaisePropertyChanged(() => AllRadarTargets);
            }
        }
    }
}
