using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Extension;
using Engine.TCMS.Turkmenistan.Model.State;
using Engine.TCMS.Turkmenistan.Resources.Keys;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class MainModel : NotificationObject
    {
        public MainModel()
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Start();
            time.Tick += (sender, args) =>
            {
                CurrentTime = DateTime.Now;
                Date = CurrentTime.ToString("yyyy-MM-dd");
                Time = CurrentTime.ToString("hh:mm:ss");
            };
            MasterCurrentFlag = 7200;
            MasterVoltageFlag = 960;
            OilPressureFlag = 70;
            FuelPressureFlag = 150;
            CoolingWaterTemperatureFlag = 880;
            OILTEngineOilTemperatureFlag = 880;
            DieselEngineSpeedFalg = 1050;
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<DataServiceDataChangedEvent>().Subscribe(DataChanged, ThreadOption.UIThread);
        }

        private void DataChanged(DataServiceDataChangedEvent.Args obj)
        {
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB牵引工况, b =>
            {
                if (b)
                {
                    WorkModel = WorkModel.Traction;
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB制动工况, b =>
            {
                if (b)
                {
                    WorkModel = WorkModel.Brake;
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB惰转工况, b =>
            {
                if (b)
                {
                    WorkModel = WorkModel.Coasting;
                }
            });
            obj.DataChangedArgs.ChangedBools.UpdateIfContains(InBoolKeys.InB自负荷工况, b =>
            {
                if (b)
                {
                    WorkModel = WorkModel.SlefLoading;
                }
            });

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车主电源, f => MasterCurrent = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车主电压, f => MasterVoltage = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车滑油压力, f => CurrentOilPressure = OilPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车滑油压力, f => OtherOilPressure = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车燃油压力, f => CurrentFuelPressure = FuelPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车燃油压力, f => OtherFuelPressure = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车冷却水温, f => CurrentCoolingWaterTemperature = CoolingWaterTemperature = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车冷却水温, f => OtherCoolingWaterTemperature = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车机油温度, f => CurrentOILTEngineOilTemperature = OILTEngineOilTemperature = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车机油温度, f => CurrentOILTEngineOilTemperature = OILTEngineOilTemperature = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车机车速度, f => EngineSpeed = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车柴油机转速, f => CurrentDieselEngineSpeed = DieselEngineSpeed = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车柴油机转速, f => OtherDieselEngineSpeed = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车功率环温1, f => CurrentPowerTemperature1 = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车功率环温1, f => OtherPowerTemperature1 = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车功率环温2, f => CurrentPowerTemperature2 = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车功率环温2, f => OtherPowerTemperature2 = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车制动风缸压力, f => OtherBrakePress = f);



        }


        private DateTime m_CurrentTime;
        private string m_Date;
        private string m_Time;
        private double m_OtherBrakePress;
        private double m_CurrentBrakePress;
        private double m_OtherPowerTemperature2;
        private double m_CurrentPowerTemperature2;
        private double m_OtherPowerTemperature1;
        private double m_CurrentPowerTemperature1;
        private double m_OtherOILTEngineOilTemperature;
        private double m_CurrentOILTEngineOilTemperature;
        private double m_OtherFuelPressure;
        private double m_CurrentFuelPressure;
        private double m_OtherCoolingWaterTemperature;
        private double m_CurrentCoolingWaterTemperature;
        private double m_OtherOilPressure;
        private double m_CurrentOilPressure;
        private double m_OtherDieselEngineSpeed;
        private double m_CurrentDieselEngineSpeed;
        private double m_DieselEngineSpeed;
        private double m_EngineSpeed;
        private WorkModel m_WorkModel;
        private double m_DieselEngineSpeedFalg;
        private double m_OILTEngineOilTemperatureFlag;
        private double m_CoolingWaterTemperatureFlag;
        private double m_FuelPressureFlag;
        private double m_OilPressureFlag;
        private double m_MasterCurrentFlag;
        private double m_MasterVoltageFlag;
        private double m_OILTEngineOilTemperature;
        private double m_CoolingWaterTemperature;
        private double m_FuelPressure;
        private double m_OilPressure;
        private double m_MasterCurrent;
        private double m_MasterVoltage;

        public DateTime CurrentTime
        {
            get { return m_CurrentTime; }
            private set
            {
                if (value.Equals(m_CurrentTime))
                    return;
                m_CurrentTime = value;
                RaisePropertyChanged(() => CurrentTime);
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public string Date
        {
            get { return m_Date; }
            private set
            {
                if (value == m_Date)
                    return;
                m_Date = value;
                RaisePropertyChanged(() => Date);
            }
        }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time
        {
            get { return m_Time; }
            private set
            {
                if (value == m_Time)
                    return;
                m_Time = value;
                RaisePropertyChanged(() => Time);
            }
        }

        /// <summary>
        /// 主电压
        /// </summary>
        public double MasterVoltage
        {
            get { return m_MasterVoltage; }
            set
            {
                if (value.Equals(m_MasterVoltage))
                    return;
                m_MasterVoltage = value;
                RaisePropertyChanged(() => MasterVoltage);
            }
        }

        /// <summary>
        /// 主电流
        /// </summary>
        public double MasterCurrent
        {
            get { return m_MasterCurrent; }
            set
            {
                if (value.Equals(m_MasterCurrent))
                    return;
                m_MasterCurrent = value;
                RaisePropertyChanged(() => MasterCurrent);
            }
        }

        /// <summary>
        /// 滑油压力
        /// </summary>
        public double OilPressure
        {
            get { return m_OilPressure; }
            set
            {
                if (value.Equals(m_OilPressure))
                    return;
                m_OilPressure = value;
                RaisePropertyChanged(() => OilPressure);
            }
        }

        /// <summary>
        /// 燃油压力
        /// </summary>
        public double FuelPressure
        {
            get { return m_FuelPressure; }
            set
            {
                if (value.Equals(m_FuelPressure))
                    return;
                m_FuelPressure = value;
                RaisePropertyChanged(() => FuelPressure);
            }
        }

        /// <summary>
        /// 冷却水温
        /// </summary>
        public double CoolingWaterTemperature
        {
            get { return m_CoolingWaterTemperature; }
            set
            {
                if (value.Equals(m_CoolingWaterTemperature))
                    return;
                m_CoolingWaterTemperature = value;
                RaisePropertyChanged(() => CoolingWaterTemperature);
            }
        }

        /// <summary>
        /// 机油温度
        /// </summary>
        public double OILTEngineOilTemperature
        {
            get { return m_OILTEngineOilTemperature; }
            set
            {
                if (value.Equals(m_OILTEngineOilTemperature))
                    return;
                m_OILTEngineOilTemperature = value;
                RaisePropertyChanged(() => OILTEngineOilTemperature);
            }
        }

        /// <summary>
        /// 主电压Falg
        /// </summary>
        public double MasterVoltageFlag
        {
            get { return m_MasterVoltageFlag; }
            set
            {
                if (value.Equals(m_MasterVoltageFlag))
                    return;
                m_MasterVoltageFlag = value;
                RaisePropertyChanged(() => MasterVoltageFlag);
            }
        }

        /// <summary>
        /// 主电流Falg
        /// </summary>
        public double MasterCurrentFlag
        {
            get { return m_MasterCurrentFlag; }
            set
            {
                if (value.Equals(m_MasterCurrentFlag))
                    return;
                m_MasterCurrentFlag = value;
                RaisePropertyChanged(() => MasterCurrentFlag);
            }
        }

        /// <summary>
        /// 滑油压力Falg
        /// </summary>
        public double OilPressureFlag
        {
            get { return m_OilPressureFlag; }
            set
            {
                if (value.Equals(m_OilPressureFlag))
                    return;
                m_OilPressureFlag = value;
                RaisePropertyChanged(() => OilPressureFlag);
            }
        }

        /// <summary>
        /// 燃油压力Falg
        /// </summary>
        public double FuelPressureFlag
        {
            get { return m_FuelPressureFlag; }
            set
            {
                if (value.Equals(m_FuelPressureFlag))
                    return;
                m_FuelPressureFlag = value;
                RaisePropertyChanged(() => FuelPressureFlag);
            }
        }

        /// <summary>
        /// 冷却水温Falg
        /// </summary>
        public double CoolingWaterTemperatureFlag
        {
            get { return m_CoolingWaterTemperatureFlag; }
            set
            {
                if (value.Equals(m_CoolingWaterTemperatureFlag))
                    return;
                m_CoolingWaterTemperatureFlag = value;
                RaisePropertyChanged(() => CoolingWaterTemperatureFlag);
            }
        }

        /// <summary>
        /// 机油温度Falg
        /// </summary>
        public double OILTEngineOilTemperatureFlag
        {
            get { return m_OILTEngineOilTemperatureFlag; }
            set
            {
                if (value.Equals(m_OILTEngineOilTemperatureFlag))
                    return;
                m_OILTEngineOilTemperatureFlag = value;
                RaisePropertyChanged(() => OILTEngineOilTemperatureFlag);
            }
        }

        /// <summary>
        /// 柴油机转速Falg
        /// </summary>
        public double DieselEngineSpeedFalg
        {
            get { return m_DieselEngineSpeedFalg; }
            set
            {
                if (value.Equals(m_DieselEngineSpeedFalg))
                    return;
                m_DieselEngineSpeedFalg = value;
                RaisePropertyChanged(() => DieselEngineSpeedFalg);
            }
        }

        /// <summary>
        /// 工况
        /// </summary>
        public WorkModel WorkModel
        {
            get { return m_WorkModel; }
            set
            {
                if (value == m_WorkModel)
                    return;
                m_WorkModel = value;
                RaisePropertyChanged(() => WorkModel);
            }
        }

        /// <summary>
        /// 机车速度
        /// </summary>
        public double EngineSpeed
        {
            get { return m_EngineSpeed; }
            set
            {
                if (value.Equals(m_EngineSpeed))
                    return;
                m_EngineSpeed = value;
                RaisePropertyChanged(() => EngineSpeed);
            }
        }

        /// <summary>
        /// 柴油机转速
        /// </summary>
        public double DieselEngineSpeed
        {
            get { return m_DieselEngineSpeed; }
            set
            {
                if (value.Equals(m_DieselEngineSpeed))
                    return;
                m_DieselEngineSpeed = value;
                RaisePropertyChanged(() => DieselEngineSpeed);
            }
        }

        /// <summary>
        /// 本车柴油机转速
        /// </summary>
        public double CurrentDieselEngineSpeed
        {
            get { return m_CurrentDieselEngineSpeed; }
            set
            {
                if (value.Equals(m_CurrentDieselEngineSpeed))
                    return;
                m_CurrentDieselEngineSpeed = value;
                RaisePropertyChanged(() => CurrentDieselEngineSpeed);
            }
        }

        /// <summary>
        /// 他车柴油机转速
        /// </summary>
        public double OtherDieselEngineSpeed
        {
            get { return m_OtherDieselEngineSpeed; }
            set
            {
                if (value.Equals(m_OtherDieselEngineSpeed))
                    return;
                m_OtherDieselEngineSpeed = value;
                RaisePropertyChanged(() => OtherDieselEngineSpeed);
            }
        }

        /// <summary>
        /// 本车滑油压力
        /// </summary>
        public double CurrentOilPressure
        {
            get { return m_CurrentOilPressure; }
            set
            {
                if (value.Equals(m_CurrentOilPressure))
                    return;
                m_CurrentOilPressure = value;
                RaisePropertyChanged(() => CurrentOilPressure);
            }
        }

        /// <summary>
        /// 他车滑油压力
        /// </summary>
        public double OtherOilPressure
        {
            get { return m_OtherOilPressure; }
            set
            {
                if (value.Equals(m_OtherOilPressure))
                    return;
                m_OtherOilPressure = value;
                RaisePropertyChanged(() => OtherOilPressure);
            }
        }

        /// <summary>
        /// 本车冷却水温
        /// </summary>
        public double CurrentCoolingWaterTemperature
        {
            get { return m_CurrentCoolingWaterTemperature; }
            set
            {
                if (value.Equals(m_CurrentCoolingWaterTemperature))
                    return;
                m_CurrentCoolingWaterTemperature = value;
                RaisePropertyChanged(() => CurrentCoolingWaterTemperature);
            }
        }

        /// <summary>
        /// 他车冷却水温
        /// </summary>
        public double OtherCoolingWaterTemperature
        {
            get { return m_OtherCoolingWaterTemperature; }
            set
            {
                if (value.Equals(m_OtherCoolingWaterTemperature))
                    return;
                m_OtherCoolingWaterTemperature = value;
                RaisePropertyChanged(() => OtherCoolingWaterTemperature);
            }
        }

        /// <summary>
        /// 本车燃油压力
        /// </summary>
        public double CurrentFuelPressure
        {
            get { return m_CurrentFuelPressure; }
            set
            {
                if (value.Equals(m_CurrentFuelPressure))
                    return;
                m_CurrentFuelPressure = value;
                RaisePropertyChanged(() => CurrentFuelPressure);
            }
        }

        /// <summary>
        /// 他车燃油压力
        /// </summary>
        public double OtherFuelPressure
        {
            get { return m_OtherFuelPressure; }
            set
            {
                if (value.Equals(m_OtherFuelPressure))
                    return;
                m_OtherFuelPressure = value;
                RaisePropertyChanged(() => OtherFuelPressure);
            }
        }

        /// <summary>
        /// 本车机油温度
        /// </summary>
        public double CurrentOILTEngineOilTemperature
        {
            get { return m_CurrentOILTEngineOilTemperature; }
            set
            {
                if (value.Equals(m_CurrentOILTEngineOilTemperature))
                    return;
                m_CurrentOILTEngineOilTemperature = value;
                RaisePropertyChanged(() => CurrentOILTEngineOilTemperature);
            }
        }

        /// <summary>
        /// 他车机油温度
        /// </summary>
        public double OtherOILTEngineOilTemperature
        {
            get { return m_OtherOILTEngineOilTemperature; }
            set
            {
                if (value.Equals(m_OtherOILTEngineOilTemperature))
                    return;
                m_OtherOILTEngineOilTemperature = value;
                RaisePropertyChanged(() => OtherOILTEngineOilTemperature);
            }
        }

        /// <summary>
        /// 本车功率环温1
        /// </summary>
        public double CurrentPowerTemperature1
        {
            get { return m_CurrentPowerTemperature1; }
            set
            {
                if (value.Equals(m_CurrentPowerTemperature1))
                    return;
                m_CurrentPowerTemperature1 = value;
                RaisePropertyChanged(() => CurrentPowerTemperature1);
            }
        }

        /// <summary>
        /// 他车功率环温1
        /// </summary>
        public double OtherPowerTemperature1
        {
            get { return m_OtherPowerTemperature1; }
            set
            {
                if (value.Equals(m_OtherPowerTemperature1))
                    return;
                m_OtherPowerTemperature1 = value;
                RaisePropertyChanged(() => OtherPowerTemperature1);
            }
        }

        /// <summary>
        /// 本车功率环温2
        /// </summary>
        public double CurrentPowerTemperature2
        {
            get { return m_CurrentPowerTemperature2; }
            set
            {
                if (value.Equals(m_CurrentPowerTemperature2))
                    return;
                m_CurrentPowerTemperature2 = value;
                RaisePropertyChanged(() => CurrentPowerTemperature2);
            }
        }

        /// <summary>
        /// 他车功率环温2
        /// </summary>
        public double OtherPowerTemperature2
        {
            get { return m_OtherPowerTemperature2; }
            set
            {
                if (value.Equals(m_OtherPowerTemperature2))
                    return;
                m_OtherPowerTemperature2 = value;
                RaisePropertyChanged(() => OtherPowerTemperature2);
            }
        }

        /// <summary>
        /// 车制动风缸压力
        /// </summary>
        public double CurrentBrakePress
        {
            get { return m_CurrentBrakePress; }
            set
            {
                if (value.Equals(m_CurrentBrakePress))
                    return;
                m_CurrentBrakePress = value;
                RaisePropertyChanged(() => CurrentBrakePress);
            }
        }

        /// <summary>
        /// 他车制动风缸压力
        /// </summary>
        public double OtherBrakePress
        {
            get { return m_OtherBrakePress; }
            set
            {
                if (value.Equals(m_OtherBrakePress))
                    return;
                m_OtherBrakePress = value;
                RaisePropertyChanged(() => OtherBrakePress);
            }
        }
    }
}
