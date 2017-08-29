using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.GZYGDC.Model.State;
using LightRail.HMI.GZYGDC.Model.Units;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class RunningModel : NotificationObject
    {

        private ObservableCollection<DoorUnit> m_DoorUnits;


        private ObservableCollection<AssistUnit> m_AssistUnits;


        private ObservableCollection<BrakeUnit> m_BrakeUnits;


        private ObservableCollection<TractionUnit> m_TractionUnits;

        private ObservableCollection<PantographUnit> m_PantographUnits;

        private ObservableCollection<HSCBUnit> m_HSCBUnits;

        private ObservableCollection<BatteryUnit> m_BatteryUnits;

        private ObservableCollection<SpringUnit> m_SpringUnits;

        private TrainDirection m_CurTrainDirectionDirection = TrainDirection.None;


        private float m_TractionPercent;

        private float m_BrakePercent;

        private float m_ReferencePercent;


        private float m_LimitSpeed;


        private bool m_HeadTrainActive;
        private bool m_TailTrainActive;


        private WorkCondition m_CurWorkCondition = WorkCondition.None;



        public RunningModel()
        {
            DoorUnits = new ObservableCollection<DoorUnit>(GlobalParam.Instance.DoorUnits);
            AssistUnits = new ObservableCollection<AssistUnit>(GlobalParam.Instance.AssistUnits);
            BrakeUnits = new ObservableCollection<BrakeUnit>(GlobalParam.Instance.BrakeUnits);
            TractionUnits = new ObservableCollection<TractionUnit>(GlobalParam.Instance.TractionUnits);
            PantographUnits = new ObservableCollection<PantographUnit>(GlobalParam.Instance.PantographUnits);
            HSCBUnits = new ObservableCollection<HSCBUnit>(GlobalParam.Instance.HSCBUnits);
            BatteryUnits = new ObservableCollection<BatteryUnit>(GlobalParam.Instance.BatteryUnits);
            SpringUnits = new ObservableCollection<SpringUnit>(GlobalParam.Instance.SpringUnits);

            Car1BatteryInfo = new BatteryInfo();
            Car2BatteryInfo = new BatteryInfo();
            Car4BatteryInfo = new BatteryInfo();
        }



        /// <summary>
        /// 当前工况
        /// </summary>
        public WorkCondition CurWorkCondition
        {
            get { return m_CurWorkCondition; }
            set
            {
                if (value.Equals(m_CurWorkCondition))
                {
                    return;
                }

                m_CurWorkCondition = value;

                RaisePropertyChanged(() => CurWorkCondition);
            }
        }


        /// <summary>
        /// 司机室头车激活
        /// </summary>
        public bool HeadTrainActive
        {
            get { return m_HeadTrainActive; }
            set
            {
                if (value.Equals(m_HeadTrainActive))
                {
                    return;
                }

                m_HeadTrainActive = value;

                RaisePropertyChanged(() => HeadTrainActive);
            }
        }


        /// <summary>
        /// 司机室尾车激活
        /// </summary>
        public bool TailTrainActive
        {
            get { return m_TailTrainActive; }
            set
            {
                if (Equals(value,m_TailTrainActive))
                {
                    return;
                }

                m_TailTrainActive = value;

                RaisePropertyChanged(() => TailTrainActive);
            }
        }


        /// <summary>
        /// 所有门单元
        /// </summary>
        public ObservableCollection<DoorUnit> DoorUnits
        {
            get { return m_DoorUnits; }
            private set
            {
                if (Equals(value, m_DoorUnits))
                {
                    return;
                }
                m_DoorUnits = value;
                RaisePropertyChanged(() => DoorUnits);
                RaisePropertyChanged(() => Car1Door1);
                RaisePropertyChanged(() => Car1Door2);
                RaisePropertyChanged(() => Car1Door3);
                RaisePropertyChanged(() => Car2Door4);
                RaisePropertyChanged(() => Car2Door5);
                RaisePropertyChanged(() => Car3Door6);
                RaisePropertyChanged(() => Car3Door7);
                RaisePropertyChanged(() => Car4Door8);
                RaisePropertyChanged(() => Car4Door9);
                RaisePropertyChanged(() => Car4Door10);
            }
        }

        /// <summary>
        /// 1车1门
        /// </summary>
        public DoorUnit Car1Door1 { get { return DoorUnits.Where(w => w.Car == 1 && w.Location  == 1).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 1车2门
        /// </summary>
        public DoorUnit Car1Door2 { get { return DoorUnits.Where(w => w.Car == 1 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 1车3门
        /// </summary>
        public DoorUnit Car1Door3 { get { return DoorUnits.Where(w => w.Car == 1 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 2车4门
        /// </summary>
        public DoorUnit Car2Door4 { get { return DoorUnits.Where(w => w.Car == 2 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 2车5门
        /// </summary>
        public DoorUnit Car2Door5 { get { return DoorUnits.Where(w => w.Car == 2 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 3车6门
        /// </summary>
        public DoorUnit Car3Door6 { get { return DoorUnits.Where(w => w.Car == 3 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 3车7门
        /// </summary>
        public DoorUnit Car3Door7 { get { return DoorUnits.Where(w => w.Car == 3 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 4车8门
        /// </summary>
        public DoorUnit Car4Door8 { get { return DoorUnits.Where(w => w.Car == 4 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 4车9门
        /// </summary>
        public DoorUnit Car4Door9 { get { return DoorUnits.Where(w => w.Car == 4 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 4车10门
        /// </summary>
        public DoorUnit Car4Door10 { get { return DoorUnits.Where(w => w.Car == 4 && w.Location == 10).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 所有辅助电源单元
        /// </summary>
        public ObservableCollection<AssistUnit> AssistUnits
        {
            get { return m_AssistUnits; }
            private set
            {
                if (Equals(value, m_AssistUnits))
                {
                    return;
                }
                m_AssistUnits = value;
                RaisePropertyChanged(() => AssistUnits);
                RaisePropertyChanged(() => Car1Location1AssistUnit);
                RaisePropertyChanged(() => Car4Location1AssistUnit);

            }
        }



        /// <summary>
        /// 1车1位置
        /// </summary>
        public AssistUnit Car1Location1AssistUnit { get { return AssistUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 4车1位置
        /// </summary>
        public AssistUnit Car4Location1AssistUnit { get { return AssistUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 所有制动单元
        /// </summary>
        public ObservableCollection<BrakeUnit> BrakeUnits
        {
            get { return m_BrakeUnits; }
            private set
            {
                if (Equals(value, m_BrakeUnits))
                {
                    return;
                }
                m_BrakeUnits = value;
                RaisePropertyChanged(() => BrakeUnits);
                RaisePropertyChanged(() => Car1Location1BrakeUnit);
                RaisePropertyChanged(() => Car2Location1BrakeUnit);
                RaisePropertyChanged(() => Car3Location1BrakeUnit);
                RaisePropertyChanged(() => Car4Location1BrakeUnit);

            }
        }


        /// <summary>
        /// 1车1位置
        /// </summary>
        public BrakeUnit Car1Location1BrakeUnit { get { return BrakeUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 2车1位置
        /// </summary>
        public BrakeUnit Car2Location1BrakeUnit { get { return BrakeUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 3车1位置
        /// </summary>
        public BrakeUnit Car3Location1BrakeUnit { get { return BrakeUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }




        /// <summary>
        /// 4车1位置
        /// </summary>
        public BrakeUnit Car4Location1BrakeUnit { get { return BrakeUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }





        /// <summary>
        /// 所有牵引单元
        /// </summary>
        public ObservableCollection<TractionUnit> TractionUnits
        {
            get { return m_TractionUnits; }
            private set
            {
                if (Equals(value, m_TractionUnits))
                {
                    return;
                }
                m_TractionUnits = value;
                RaisePropertyChanged(() => TractionUnits);
                RaisePropertyChanged(() => Car1TractionUnit);
                RaisePropertyChanged(() => Car3TractionUnit);
                RaisePropertyChanged(() => Car4TractionUnit);

            }
        }


        /// <summary>
        /// 1车
        /// </summary>
        public TractionUnit Car1TractionUnit { get { return TractionUnits.Where(w => w.Car == 1).OrderBy(o => o.Car).FirstOrDefault(); } }




        /// <summary>
        /// 3车
        /// </summary>
        public TractionUnit Car3TractionUnit { get { return TractionUnits.Where(w => w.Car == 3).OrderBy(o => o.Car).FirstOrDefault(); } }




        /// <summary>
        /// 4车
        /// </summary>
        public TractionUnit Car4TractionUnit { get { return TractionUnits.Where(w => w.Car == 4).OrderBy(o => o.Car).FirstOrDefault(); } }




        /// <summary>
        /// 所有受电弓单元
        /// </summary>
        public ObservableCollection<PantographUnit> PantographUnits
        {
            get { return m_PantographUnits; }
            private set
            {
                if (Equals(value, m_PantographUnits))
                {
                    return;
                }
                m_PantographUnits = value;
                RaisePropertyChanged(() => PantographUnits);
                RaisePropertyChanged(() => Car2PantographUnit);

            }
        }


        /// <summary>
        /// 2车
        /// </summary>
        public PantographUnit Car2PantographUnit { get { return PantographUnits.Where(w => w.Car == 2).OrderBy(o => o.Car).FirstOrDefault(); } }




        /// <summary>
        /// 所有HSCB高速断路器单元
        /// </summary>
        public ObservableCollection<HSCBUnit> HSCBUnits
        {
            get { return m_HSCBUnits; }
            private set
            {
                if (Equals(value, m_HSCBUnits))
                {
                    return;
                }
                m_HSCBUnits = value;
                RaisePropertyChanged(() => HSCBUnits);
                RaisePropertyChanged(() => Car2HSCBUnit);

            }
        }


        /// <summary>
        /// 2车
        /// </summary>
        public HSCBUnit Car2HSCBUnit { get { return HSCBUnits.Where(w => w.Car == 2).OrderBy(o => o.Car).FirstOrDefault(); } }





        /// <summary>
        /// 所有电池单元
        /// </summary>
        public ObservableCollection<BatteryUnit> BatteryUnits
        {
            get { return m_BatteryUnits; }
            private set
            {
                if (Equals(value, m_BatteryUnits))
                {
                    return;
                }
                m_BatteryUnits = value;
                RaisePropertyChanged(() => BatteryUnits);
                RaisePropertyChanged(() => Car1BatteryUnit);
                RaisePropertyChanged(() => Car2BatteryUnit);
                RaisePropertyChanged(() => Car4BatteryUnit);

            }
        }


        /// <summary>
        /// 1车
        /// </summary>
        public BatteryUnit Car1BatteryUnit { get { return BatteryUnits.Where(w => w.Car == 1).OrderBy(o => o.Car).FirstOrDefault(); } }


        /// <summary>
        /// 2车
        /// </summary>
        public BatteryUnit Car2BatteryUnit { get { return BatteryUnits.Where(w => w.Car == 2).OrderBy(o => o.Car).FirstOrDefault(); } }



        /// <summary>
        /// 4车
        /// </summary>
        public BatteryUnit Car4BatteryUnit { get { return BatteryUnits.Where(w => w.Car == 4).OrderBy(o => o.Car).FirstOrDefault(); } }





        /// <summary>
        /// 所有弹簧单元
        /// </summary>
        public ObservableCollection<SpringUnit> SpringUnits
        {
            get { return m_SpringUnits; }
            private set
            {
                if (Equals(value, m_SpringUnits))
                {
                    return;
                }
                m_SpringUnits = value;
                RaisePropertyChanged(() => SpringUnits);
                RaisePropertyChanged(() => Car1Location1SpringUnit);
                RaisePropertyChanged(() => Car1Location2SpringUnit);
                //RaisePropertyChanged(() => Car2Location1SpringUnit);
                //RaisePropertyChanged(() => Car2Location2SpringUnit);
                RaisePropertyChanged(() => Car3Location1SpringUnit);
                RaisePropertyChanged(() => Car3Location2SpringUnit);
                RaisePropertyChanged(() => Car4Location1SpringUnit);
                RaisePropertyChanged(() => Car4Location2SpringUnit);

            }
        }


        /// <summary>
        /// 1车1级
        /// </summary>
        public SpringUnit Car1Location1SpringUnit { get { return SpringUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 1车2级
        /// </summary>
        public SpringUnit Car1Location2SpringUnit { get { return SpringUnits.Where(w => w.Car == 1 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }





        /// <summary>
        /// 2车1级
        /// </summary>
        //public SpringUnit Car2Location1SpringUnit { get { return SpringUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 2车2级
        /// </summary>
        //public SpringUnit Car2Location2SpringUnit { get { return SpringUnits.Where(w => w.Car == 2 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }





        /// <summary>
        /// 3车1级
        /// </summary>
        public SpringUnit Car3Location1SpringUnit { get { return SpringUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 3车2级
        /// </summary>
        public SpringUnit Car3Location2SpringUnit { get { return SpringUnits.Where(w => w.Car == 3 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }





        /// <summary>
        /// 4车1级
        /// </summary>
        public SpringUnit Car4Location1SpringUnit { get { return SpringUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 4车2级
        /// </summary>
        public SpringUnit Car4Location2SpringUnit { get { return SpringUnits.Where(w => w.Car == 4 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }



        /// <summary>
        /// 当前方向
        /// </summary>
        public TrainDirection CurTrainDirectionDirection
        {
            get { return m_CurTrainDirectionDirection; }
            set
            {
                if (value.Equals(m_CurTrainDirectionDirection))
                {
                    return;
                }

                m_CurTrainDirectionDirection = value;

                RaisePropertyChanged(() => CurTrainDirectionDirection);
            }
        }



        /// <summary>
        /// 1车电池信息
        /// </summary>
        public BatteryInfo Car1BatteryInfo { get; private set; }

        /// <summary>
        /// 2车电池信息
        /// </summary>
        public BatteryInfo Car2BatteryInfo { get; private set; }

        /// <summary>
        /// 4车电池信息
        /// </summary>
        public BatteryInfo Car4BatteryInfo { get; private set; }




        /// <summary>
        /// 牵引百分比
        /// </summary>
        public float TractionPercent
        {
            get { return m_TractionPercent; }
            set
            {
                if (value.Equals(m_TractionPercent))
                {
                    return;
                }

                m_TractionPercent = value;
                ReferencePercent = value;

                RaisePropertyChanged(() => TractionPercent);
            }
        }


        /// <summary>
        /// 制动百分比
        /// </summary>
        public float BrakePercent
        {
            get { return m_BrakePercent; }
            set
            {
                if (value.Equals(m_BrakePercent))
                {
                    return;
                }

                m_BrakePercent = value;
                ReferencePercent = -value;

                RaisePropertyChanged(() => BrakePercent);
            }
        }



        /// <summary>
        /// 参考值百分比，牵引百分比或制动百分比
        /// </summary>
        public float ReferencePercent
        {
            get { return m_ReferencePercent; }
            set
            {
                if (value.Equals(m_ReferencePercent))
                {
                    return;
                }

                m_ReferencePercent = value;

                RaisePropertyChanged(() => ReferencePercent);
            }
        }



        /// <summary>
        /// 限制速度
        /// </summary>
        public float LimitSpeed
        {
            get { return m_LimitSpeed; }
            set
            {
                if (value.Equals(m_LimitSpeed))
                {
                    return;
                }

                m_LimitSpeed = value;

                RaisePropertyChanged(() => LimitSpeed);
            }
        }







    }
}