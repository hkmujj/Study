using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class CarComfortModel : ModelBase
    {
        private float m_OutTemperature;
        private float m_Temperature0;
        private float m_Temperature7;
        private float m_Temperature6;
        private float m_Temperature5;
        private float m_Temperature4;
        private float m_Temperature3;
        private float m_Temperature2;
        private float m_Temperature1;
        private float m_SetTemperature0;
        private float m_SetTemperature7;
        private float m_SetTemperature6;
        private float m_SetTemperature5;
        private float m_SetTemperature4;
        private float m_SetTemperature3;
        private float m_SetTemperature2;
        private float m_SetTemperature1;
        private bool m_AddTemperatureIsEnable;
        private bool m_SubTemperatureIsEnable;
        
        private ObservableCollection<CarComfortUnit> m_CarComfortUnits;

        [ImportingConstructor]
        public CarComfortModel(CarComfortController carComfortController)
        {
            TrainModel = new TrainModel();

            CarComfortUnits = new ObservableCollection<CarComfortUnit>(GlobalParam.Instance.CarComfortUnits.OrderBy(o => o.Num));

            CarComfortController = carComfortController;
            CarComfortController.ViewModel = this;
            CarComfortController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public CarComfortController CarComfortController { get; set; }
        
        /// <summary>
        /// 车辆集合
        /// </summary>
        public TrainModel TrainModel { get; private set; }
        
        /// <summary>
        /// 空调状态单元
        /// </summary>
        public ObservableCollection<CarComfortUnit> CarComfortUnits
        {
            get { return m_CarComfortUnits; }
            private set
            {
                if (Equals(value, m_CarComfortUnits))
                {
                    return;
                }
                m_CarComfortUnits = value;
                RaisePropertyChanged(() => CarComfortUnits);
                RaisePropertyChanged(() => CarComfort0);
                RaisePropertyChanged(() => CarComfort7);
                RaisePropertyChanged(() => CarComfort6);
                RaisePropertyChanged(() => CarComfort5);
                RaisePropertyChanged(() => CarComfort4);
                RaisePropertyChanged(() => CarComfort3);
                RaisePropertyChanged(() => CarComfort2);
                RaisePropertyChanged(() => CarComfort1);
            }
        }
        /// <summary>
        /// 车0空调状态
        /// </summary>
        public CarComfortUnit CarComfort0 { get { return CarComfortUnits.Where(w => w.Car == 0 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车7空调状态
        /// </summary>
        public CarComfortUnit CarComfort7 { get { return CarComfortUnits.Where(w => w.Car == 7 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车6空调状态
        /// </summary>
        public CarComfortUnit CarComfort6 { get { return CarComfortUnits.Where(w => w.Car == 6 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车5空调状态
        /// </summary>
        public CarComfortUnit CarComfort5 { get { return CarComfortUnits.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车4空调状态
        /// </summary>
        public CarComfortUnit CarComfort4 { get { return CarComfortUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车3空调状态
        /// </summary>
        public CarComfortUnit CarComfort3 { get { return CarComfortUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车2空调状态
        /// </summary>
        public CarComfortUnit CarComfort2 { get { return CarComfortUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 车1空调状态
        /// </summary>
        public CarComfortUnit CarComfort1 { get { return CarComfortUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 外部温度
        /// </summary>
        public float OutTemperature
        {
            get { return m_OutTemperature; }
            set
            {
                if (value == m_OutTemperature)
                {
                    return;
                }
                m_OutTemperature = value;
                RaisePropertyChanged(() => OutTemperature);
            }
        }

      /// <summary>
      /// 车0温度
      /// </summary>
      public float Temperature0
      {
          get { return m_Temperature0; }
          set
          {
              if (value == m_Temperature0)
              {
                  return;
              }
              m_Temperature0 = value;
              RaisePropertyChanged(() => Temperature0);
          }
      }

      /// <summary>
      /// 车7温度
      /// </summary>
      public float Temperature7
      {
          get { return m_Temperature7; }
          set
          {
              if (value == m_Temperature7)
              {
                  return;
              }
              m_Temperature7 = value;
              RaisePropertyChanged(() => Temperature7);
          }
      }

      /// <summary>
      /// 车6温度
      /// </summary>
      public float Temperature6
      {
          get { return m_Temperature6; }
          set
          {
              if (value == m_Temperature6)
              {
                  return;
              }
              m_Temperature6 = value;
              RaisePropertyChanged(() => Temperature6);
          }
      }

      /// <summary>
      /// 车5温度
      /// </summary>
      public float Temperature5
      {
          get { return m_Temperature5; }
          set
          {
              if (value == m_Temperature5)
              {
                  return;
              }
              m_Temperature5 = value;
              RaisePropertyChanged(() => Temperature5);
          }
      }

      /// <summary>
      /// 车4温度
      /// </summary>
      public float Temperature4
      {
          get { return m_Temperature4; }
          set
          {
              if (value == m_Temperature4)
              {
                  return;
              }
              m_Temperature4 = value;
              RaisePropertyChanged(() => Temperature4);
          }
      }

      /// <summary>
      /// 车3温度
      /// </summary>
      public float Temperature3
      {
          get { return m_Temperature3; }
          set
          {
              if (value == m_Temperature3)
              {
                  return;
              }
              m_Temperature3 = value;
              RaisePropertyChanged(() => Temperature3);
          }
      }

      /// <summary>
      /// 车2温度
      /// </summary>
      public float Temperature2
      {
          get { return m_Temperature2; }
          set
          {
              if (value == m_Temperature2)
              {
                  return;
              }
              m_Temperature2 = value;
              RaisePropertyChanged(() => Temperature2);
          }
      }

      /// <summary>
      /// 车1温度
      /// </summary>
      public float Temperature1
      {
          get { return m_Temperature1; }
          set
          {
              if (value == m_Temperature1)
              {
                  return;
              }
              m_Temperature1 = value;
              RaisePropertyChanged(() => Temperature1);
          }
      }

      /// <summary>
      /// 车0设定温度
      /// </summary>
      public float SetTemperature0
      {
          get { return m_SetTemperature0; }
          set
          {
              if (value == m_SetTemperature0)
              {
                  return;
              }
              m_SetTemperature0 = value;
              RaisePropertyChanged(() => SetTemperature0);
          }
      }

      /// <summary>
      /// 车7设定温度
      /// </summary>
      public float SetTemperature7
      {
          get { return m_SetTemperature7; }
          set
          {
              if (value == m_SetTemperature7)
              {
                  return;
              }
              m_SetTemperature7 = value;
              RaisePropertyChanged(() => SetTemperature7);
          }
      }

      /// <summary>
      /// 车6设定温度
      /// </summary>
      public float SetTemperature6
      {
          get { return m_SetTemperature6; }
          set
          {
              if (value == m_SetTemperature6)
              {
                  return;
              }
              m_SetTemperature6 = value;
              RaisePropertyChanged(() => SetTemperature6);
          }
      }

      /// <summary>
      /// 车5设定温度
      /// </summary>
      public float SetTemperature5
      {
          get { return m_SetTemperature5; }
          set
          {
              if (value == m_SetTemperature5)
              {
                  return;
              }
              m_SetTemperature5 = value;
              RaisePropertyChanged(() => SetTemperature5);
          }
      }

      /// <summary>
      /// 车4设定温度
      /// </summary>
      public float SetTemperature4
      {
          get { return m_SetTemperature4; }
          set
          {
              if (value == m_SetTemperature4)
              {
                  return;
              }
              m_SetTemperature4 = value;
              RaisePropertyChanged(() => SetTemperature4);
          }
      }

      /// <summary>
      /// 车3设定温度
      /// </summary>
      public float SetTemperature3
      {
          get { return m_SetTemperature3; }
          set
          {
              if (value == m_SetTemperature3)
              {
                  return;
              }
              m_SetTemperature3 = value;
              RaisePropertyChanged(() => SetTemperature3);
          }
      }

      /// <summary>
      /// 车2设定温度
      /// </summary>
      public float SetTemperature2
      {
          get { return m_SetTemperature2; }
          set
          {
              if (value == m_SetTemperature2)
              {
                  return;
              }
              m_SetTemperature2 = value;
              RaisePropertyChanged(() => SetTemperature2);
          }
      }

      /// <summary>
      /// 车1设定温度
      /// </summary>
      public float SetTemperature1
      {
          get { return m_SetTemperature1; }
          set
          {
              if (value == m_SetTemperature1)
              {
                  return;
              }
              m_SetTemperature1 = value;
              RaisePropertyChanged(() => SetTemperature1);
          }
      }

      /// <summary>
      /// 温度增加
      /// </summary>
      public bool AddTemperatureIsEnable
      {
          get { return m_AddTemperatureIsEnable; }
          set
          {
              if (value == m_AddTemperatureIsEnable)
              {
                  return;
              }
              m_AddTemperatureIsEnable = value;
              RaisePropertyChanged(() => AddTemperatureIsEnable);
          }
      }

      /// <summary>
      /// 温度减少
      /// </summary>
      public bool SubTemperatureIsEnable
      {
          get { return m_SubTemperatureIsEnable; }
          set
          {
              if (value == m_SubTemperatureIsEnable)
              {
                  return;
              }
              m_SubTemperatureIsEnable = value;
              RaisePropertyChanged(() => SubTemperatureIsEnable);
          }
      }
    }
}