using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.GZYGDC.Model.Units;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class BroadcastControlModel : NotificationObject
    {
        private ObservableCollection<EnmergencyTalkUnit> m_EnmergencyTalkUnits;


        public BroadcastControlModel()
        {

            EnmergencyTalkUnits = new ObservableCollection<EnmergencyTalkUnit>(GlobalParam.Instance.EnmergencyTalkUnits);

            //默认值
            

            
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



    }
}