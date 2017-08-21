using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using System.Linq;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Model.BroadcastControlModel
{
    [Export]
    public class EnmergencyTalkModel : ModelBase
    {
        private ObservableCollection<EnmergencyTalkUnit> m_EnmergencyTalkUnits;
        
        private bool m_LastStation;
        private bool m_NextStation;
        private bool m_ArriveStationBroadcast;
        private bool m_LeaveStationBroadcast;
        private bool m_CancelStationBroadcast;

        public EnmergencyTalkModel()
        {
            EnmergencyTalkUnits = new ObservableCollection<EnmergencyTalkUnit>(GlobalParam.Instance.EnmergencyTalkUnits);
        }

        /// <summary>
        /// 所有紧急对讲单元
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
                RaisePropertyChanged(() => Car1Location1EnmergencyTalkUnit);
                RaisePropertyChanged(() => Car1Location2EnmergencyTalkUnit);
                RaisePropertyChanged(() => Car2Location1EnmergencyTalkUnit);
                RaisePropertyChanged(() => Car2Location2EnmergencyTalkUnit);
                RaisePropertyChanged(() => Car3Location1EnmergencyTalkUnit);
                RaisePropertyChanged(() => Car3Location2EnmergencyTalkUnit);
                RaisePropertyChanged(() => Car4Location1EnmergencyTalkUnit);
                RaisePropertyChanged(() => Car4Location2EnmergencyTalkUnit);
            }
        }
        
        /// <summary>
        /// 1车1位置
        /// </summary>
        public EnmergencyTalkUnit Car1Location1EnmergencyTalkUnit { get { return EnmergencyTalkUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 1车2位置
        /// </summary>
        public EnmergencyTalkUnit Car1Location2EnmergencyTalkUnit { get { return EnmergencyTalkUnits.Where(w => w.Car == 1 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 2车1位置
        /// </summary>
        public EnmergencyTalkUnit Car2Location1EnmergencyTalkUnit { get { return EnmergencyTalkUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 2车2位置
        /// </summary>
        public EnmergencyTalkUnit Car2Location2EnmergencyTalkUnit { get { return EnmergencyTalkUnits.Where(w => w.Car == 2 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车1位置
        /// </summary>
        public EnmergencyTalkUnit Car3Location1EnmergencyTalkUnit { get { return EnmergencyTalkUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 3车2位置
        /// </summary>
        public EnmergencyTalkUnit Car3Location2EnmergencyTalkUnit { get { return EnmergencyTalkUnits.Where(w => w.Car == 3 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 4车1位置
        /// </summary>
        public EnmergencyTalkUnit Car4Location1EnmergencyTalkUnit { get { return EnmergencyTalkUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }
        
        /// <summary>
        /// 4车2位置
        /// </summary>
        public EnmergencyTalkUnit Car4Location2EnmergencyTalkUnit { get { return EnmergencyTalkUnits.Where(w => w.Car == 4 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 上一站
        /// </summary>
        public bool LastStaion
        {
            get { return m_LastStation; }
            set
            {
                if (value == m_LastStation)
                {
                    return;
                }

                m_LastStation = value;
                RaisePropertyChanged(() => LastStaion);
            }
        }

        /// <summary>
        /// 下一站
        /// </summary>
        public bool NextStation
        {
            get { return m_NextStation; }
            set
            {
                if (value == m_NextStation)
                {
                    return;
                }

                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        /// <summary>
        /// 进站广播
        /// </summary>
        public bool ArriveStationBroadcast
        {
            get { return m_ArriveStationBroadcast; }
            set
            {
                if (value == m_ArriveStationBroadcast)
                {
                    return;
                }

                m_ArriveStationBroadcast = value;
                RaisePropertyChanged(() => ArriveStationBroadcast);
            }
        }

        /// <summary>
        /// 离站广播
        /// </summary>
        public bool LeaveStationBroadcast
        {
            get { return m_LeaveStationBroadcast; }
            set
            {
                if (value == m_LeaveStationBroadcast)
                {
                    return;
                }

                m_LeaveStationBroadcast = value;
                RaisePropertyChanged(() => LeaveStationBroadcast);
            }
        }

        /// <summary>
        /// 报站取消
        /// </summary>
        public bool CancelStationBroadcast
        {
            get { return m_CancelStationBroadcast; }
            set
            {
                if (value == m_CancelStationBroadcast)
                {
                    return;
                }

                m_CancelStationBroadcast = value;
                RaisePropertyChanged(() => CancelStationBroadcast);
            }
        }
    }
}
