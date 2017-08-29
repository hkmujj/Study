using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Util;
using LightRail.HMI.GZYGDC.Model.ConfigModel;
using LightRail.HMI.GZYGDC.Model.State;
using LightRail.HMI.GZYGDC.Model.Units;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class AirConditionModel : NotificationObject
    {
        private ObservableCollection<AirConditionUnit> m_AirConditionUnits;

        private AirConditionInfo m_ConcentrateAirConditionInfo;

        private WindSpeedMode m_CabWindSpeedMode;

        private AirConditionInfo m_AirConditionInfoCar1;

        private AirConditionInfo m_AirConditionInfoCar2;

        private AirConditionInfo m_AirConditionInfoCar3;

        private AirConditionInfo m_AirConditionInfoCar4;


        public AirConditionModel()
        {
            AirConditionUnits = new ObservableCollection<AirConditionUnit>(GlobalParam.Instance.AirConditionUnits);

            ConcentrateAirConditionInfo = new AirConditionInfo();
            AirConditionInfoCar1 = new AirConditionInfo();
            AirConditionInfoCar2 = new AirConditionInfo();
            AirConditionInfoCar3 = new AirConditionInfo();
            AirConditionInfoCar4 = new AirConditionInfo();
        }


        /// <summary>
        /// 所有空调单元
        /// </summary>
        public ObservableCollection<AirConditionUnit> AirConditionUnits
        {
            get { return m_AirConditionUnits; }
            private set
            {
                if (Equals(value, m_AirConditionUnits))
                {
                    return;
                }
                m_AirConditionUnits = value;
                RaisePropertyChanged(() => AirConditionUnits);
                RaisePropertyChanged(() => Car1Location1AirConditionUnit);
                RaisePropertyChanged(() => Car2Location1AirConditionUnit);
                RaisePropertyChanged(() => Car3Location1AirConditionUnit);
                RaisePropertyChanged(() => Car4Location1AirConditionUnit);

            }
        }


        /// <summary>
        /// 1车1位置
        /// </summary>
        public AirConditionUnit Car1Location1AirConditionUnit { get { return AirConditionUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 2车1位置
        /// </summary>
        public AirConditionUnit Car2Location1AirConditionUnit { get { return AirConditionUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 3车1位置
        /// </summary>
        public AirConditionUnit Car3Location1AirConditionUnit { get { return AirConditionUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 4车1位置
        /// </summary>
        public AirConditionUnit Car4Location1AirConditionUnit { get { return AirConditionUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 集中控制空调信息
        /// </summary>
        public AirConditionInfo ConcentrateAirConditionInfo
        {
            get { return m_ConcentrateAirConditionInfo; }
            set
            {
                if (Equals(value, m_ConcentrateAirConditionInfo))
                {
                    return;
                }

                m_ConcentrateAirConditionInfo = value;

                //子属性改变，通知父属性
                m_ConcentrateAirConditionInfo.PropertyChanged +=
                    (sender, args) => RaisePropertyChanged(() => ConcentrateAirConditionInfo);

                RaisePropertyChanged(() => ConcentrateAirConditionInfo);
            }
        }



        /// <summary>
        /// 集中控制司机室风速模式
        /// </summary>
        public WindSpeedMode CabWindSpeedMode
        {
            get { return m_CabWindSpeedMode; }
            set
            {
                if (Equals(value, m_CabWindSpeedMode))
                {
                    return;
                }
                m_CabWindSpeedMode = value;

                RaisePropertyChanged(() => CabWindSpeedMode);
            }
        }


        /// <summary>
        /// 1车空调信息
        /// </summary>
        public AirConditionInfo AirConditionInfoCar1
        {
            get { return m_AirConditionInfoCar1; }
            set
            {
                if (Equals(value, m_AirConditionInfoCar1))
                {
                    return;
                }
                m_AirConditionInfoCar1 = value;
                RaisePropertyChanged(() => AirConditionInfoCar1);
            }
        }


        /// <summary>
        /// 2车空调信息
        /// </summary>
        public AirConditionInfo AirConditionInfoCar2
        {
            get { return m_AirConditionInfoCar2; }
            set
            {
                if (Equals(value, m_AirConditionInfoCar2))
                {
                    return;
                }
                m_AirConditionInfoCar2 = value;
                RaisePropertyChanged(() => AirConditionInfoCar2);
            }
        }


        /// <summary>
        /// 3车空调信息
        /// </summary>
        public AirConditionInfo AirConditionInfoCar3
        {
            get { return m_AirConditionInfoCar3; }
            set
            {
                if (Equals(value, m_AirConditionInfoCar3))
                {
                    return;
                }
                m_AirConditionInfoCar3 = value;
                RaisePropertyChanged(() => AirConditionInfoCar3);
            }
        }


        /// <summary>
        /// 4车空调信息
        /// </summary>
        public AirConditionInfo AirConditionInfoCar4
        {
            get { return m_AirConditionInfoCar4; }
            set
            {
                if (Equals(value, m_AirConditionInfoCar4))
                {
                    return;
                }
                m_AirConditionInfoCar4 = value;
                RaisePropertyChanged(() => AirConditionInfoCar4);
            }
        }

    }
}