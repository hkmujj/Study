using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Extension;
using Engine.TCMS.Turkmenistan.Resources.Keys;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class RunParamModel : NotificationObject
    {
        private double m_OtherReducedPowerLoopTemperature2;
        private double m_OtherReducedPowerLoopTemperature1;
        private double m_OtherAfterTurbochargerSpeed;
        private double m_OtherFrontTurbochargerSpeed;
        private double m_OtherEarthLeakageCurrent;
        private double m_OtherMainGeneratorExciterCurrent;
        private double m_OtherExciterCurrent;
        private double m_OtherCoolingFanSpeed2;
        private double m_OtherCoolingFanSpeed1;
        private double m_OtherMotorSpeedDifference;
        private double m_OtherSpeed6D;
        private double m_OtherSpeed5D;
        private double m_OtherSpeed4D;
        private double m_OtherSpeed3D;
        private double m_OtherSpeed2D;
        private double m_OtherSpeed1D;
        private double m_OtherCurrentDistributionFactor;
        private double m_OtherMotorCurrentDifference;
        private double m_OtherElectricCurrent6D;
        private double m_OtherElectricCurrent5D;
        private double m_OtherElectricCurrent4D;
        private double m_OtherElectricCurrent3D;
        private double m_OtherElectricCurrent2D;
        private double m_OtherElectricCurrent1D;
        private double m_OtherSystemEndingPressure;
        private double m_OtherFuelPumpOutletPressure;
        private double m_OtherFilterInletPressure;
        private double m_OtherAfterTurbochargerInletPressure;
        private double m_OtherFrontTurbochargerInletPressure;
        private double m_OtherOutletPressureOfDieselEngine;
        private double m_OtherInletPressureOfDieselEngine;
        private double m_OtherWaterTemperatureOfHeatExchanger;
        private double m_OtherOilOutletTemperature;
        private double m_OtherOilInletTemperature;
        private double m_OtherColdWaterSystemOfDieselEngineIn;
        private double m_OtherColdWaterSystemOfDieselEngineOut;
        private double m_OtherHighWaterTemperatureSystemOfDieselEngineIn;
        private double m_OtherHighWaterTemperatureSystemOfDieselEngineOut;
        private double m_ReducedPowerLoopTemperature2;
        private double m_ReducedPowerLoopTemperature1;
        private double m_AfterTurbochargerSpeed;
        private double m_FrontTurbochargerSpeed;
        private double m_EarthLeakageCurrent;
        private double m_MainGeneratorExciterCurrent;
        private double m_ExciterCurrent;
        private double m_CoolingFanSpeed2;
        private double m_CoolingFanSpeed1;
        private double m_MotorSpeedDifference;
        private double m_Speed6D;
        private double m_Speed5D;
        private double m_Speed4D;
        private double m_Speed3D;
        private double m_Speed2D;
        private double m_Speed1D;
        private double m_CurrentDistributionFactor;
        private double m_MotorCurrentDifference;
        private double m_ElectricCurrent6D;
        private double m_ElectricCurrent5D;
        private double m_ElectricCurrent4D;
        private double m_ElectricCurrent3D;
        private double m_ElectricCurrent2D;
        private double m_ElectricCurrent1D;
        private double m_SystemEndingPressure;
        private double m_FuelPumpOutletPressure;
        private double m_FilterInletPressure;
        private double m_AfterTurbochargerInletPressure;
        private double m_FrontTurbochargerInletPressure;
        private double m_OutletPressureOfDieselEngine;
        private double m_InletPressureOfDieselEngine;
        private double m_WaterTemperatureOfHeatExchanger;
        private double m_OilOutletTemperature;
        private double m_OilInletTemperature;
        private double m_ColdWaterSystemOfDieselEngineIn;
        private double m_ColdWaterSystemOfDieselEngineOut;
        private double m_HighWaterTemperatureSystemOfDieselEngineIn;
        private double m_HighWaterTemperatureSystemOfDieselEngineOut;

        public RunParamModel()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<DataServiceDataChangedEvent>()
                .Subscribe(DataChangedMethod, ThreadOption.UIThread);
        }

        private void DataChangedMethod(DataServiceDataChangedEvent.Args obj)
        {
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车功率环温1, f => ReducedPowerLoopTemperature1 = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车功率环温2, f => ReducedPowerLoopTemperature2 = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车1D电流, f => ElectricCurrent1D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车2D电流, f => ElectricCurrent2D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车3D电流, f => ElectricCurrent3D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车4D电流, f => ElectricCurrent4D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车5D电流, f => ElectricCurrent5D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车6D电流, f => ElectricCurrent6D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车电流分配系数, f => CurrentDistributionFactor = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车1D转速, f => Speed1D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车2D转速, f => Speed2D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车3D转速, f => Speed3D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车4D转速, f => Speed4D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车5D转速, f => Speed5D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车6D转速, f => Speed6D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车电机转速差, f => MotorSpeedDifference = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车高温水进口温度, f => HighWaterTemperatureSystemOfDieselEngineIn = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车高温水出口温度, f => HighWaterTemperatureSystemOfDieselEngineOut = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车中冷水进口温度, f => ColdWaterSystemOfDieselEngineIn = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车中冷水出口温度, f => ColdWaterSystemOfDieselEngineOut = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车机油进口温度, f => OilInletTemperature = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车机油出口温度, f => OilOutletTemperature = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车热交换器水温, f => WaterTemperatureOfHeatExchanger = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车柴油机进口压力, f => InletPressureOfDieselEngine = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车柴油机出口压力, f => OutletPressureOfDieselEngine = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车前增压器进口压力, f => FrontTurbochargerInletPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车后增压器进口压力, f => AfterTurbochargerInletPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车滤清器进口压力, f => FilterInletPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车燃油泵出口压力, f => FuelPumpOutletPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车燃油系统末端压力, f => SystemEndingPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车_1冷风扇转速, f => CoolingFanSpeed1 = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车_2冷风扇转速, f => CoolingFanSpeed2 = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车励磁机励磁电流, f => ExciterCurrent = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车主发电机励磁电流, f => MainGeneratorExciterCurrent = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车接地漏电流, f => EarthLeakageCurrent = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车前增压器转速, f => FrontTurbochargerSpeed = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车后增压器转速, f => AfterTurbochargerSpeed = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF本车电机电流差, f => MotorCurrentDifference = f);


            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车功率环温1, f => OtherReducedPowerLoopTemperature1 = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车功率环温2, f => OtherReducedPowerLoopTemperature2 = f);

            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车1D电流, f => OtherElectricCurrent1D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车2D电流, f => OtherElectricCurrent2D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车3D电流, f => OtherElectricCurrent3D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车4D电流, f => OtherElectricCurrent4D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车5D电流, f => OtherElectricCurrent5D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车6D电流, f => OtherElectricCurrent6D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车电流分配系数, f => OtherCurrentDistributionFactor = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车1D转速, f => OtherSpeed1D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车2D转速, f => OtherSpeed2D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车3D转速, f => OtherSpeed3D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车4D转速, f => OtherSpeed4D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车5D转速, f => OtherSpeed5D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车6D转速, f => OtherSpeed6D = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车电机转速差, f => OtherMotorSpeedDifference = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车高温水进口温度, f => OtherHighWaterTemperatureSystemOfDieselEngineIn = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车高温水出口温度, f => OtherHighWaterTemperatureSystemOfDieselEngineOut = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车中冷水进口温度, f => OtherColdWaterSystemOfDieselEngineIn = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车中冷水出口温度, f => OtherColdWaterSystemOfDieselEngineOut = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车热交换器水温, f => OtherOilInletTemperature = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车柴油机进口压力, f => OtherOilOutletTemperature = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车柴油机出口压力, f => OtherWaterTemperatureOfHeatExchanger = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车前增压器进口压力, f => OtherFrontTurbochargerInletPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车后增压器进口压力, f => OtherAfterTurbochargerInletPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车滤清器进口压力, f => OtherFilterInletPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车燃油泵出口压力, f => OtherFuelPumpOutletPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车燃油系统末端压力, f => OtherSystemEndingPressure = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车_1冷风扇转速, f => OtherCoolingFanSpeed1 = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车_2冷风扇转速, f => OtherCoolingFanSpeed2 = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车励磁机励磁电流, f => OtherExciterCurrent = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车主发电机励磁电流, f => OtherMainGeneratorExciterCurrent = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车接地漏电流, f => OtherEarthLeakageCurrent = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车前增压器转速, f => OtherFrontTurbochargerSpeed = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车后增压器转速, f => OtherAfterTurbochargerSpeed = f);
            obj.DataChangedArgs.ChangedFloats.UpdateIfContains(InFloatKeys.InF他车电机电流差, f => OtherMotorCurrentDifference = f);

        }

        /// <summary>
        /// 柴油机高温水系统出口温度
        /// </summary>
        public double HighWaterTemperatureSystemOfDieselEngineOut
        {
            get { return m_HighWaterTemperatureSystemOfDieselEngineOut; }
            set
            {
                if (value.Equals(m_HighWaterTemperatureSystemOfDieselEngineOut))
                    return;
                m_HighWaterTemperatureSystemOfDieselEngineOut = value;
                RaisePropertyChanged(() => HighWaterTemperatureSystemOfDieselEngineOut);
            }
        }

        /// <summary>
        /// 柴油机高温水系统进口温度
        /// </summary>
        public double HighWaterTemperatureSystemOfDieselEngineIn
        {
            get { return m_HighWaterTemperatureSystemOfDieselEngineIn; }
            set
            {
                if (value.Equals(m_HighWaterTemperatureSystemOfDieselEngineIn))
                    return;
                m_HighWaterTemperatureSystemOfDieselEngineIn = value;
                RaisePropertyChanged(() => HighWaterTemperatureSystemOfDieselEngineIn);
            }
        }

        /// <summary>
        /// 柴油机中冷水系统出口温度
        /// </summary>
        public double ColdWaterSystemOfDieselEngineOut
        {
            get { return m_ColdWaterSystemOfDieselEngineOut; }
            set
            {
                if (value.Equals(m_ColdWaterSystemOfDieselEngineOut))
                    return;
                m_ColdWaterSystemOfDieselEngineOut = value;
                RaisePropertyChanged(() => ColdWaterSystemOfDieselEngineOut);
            }
        }

        /// <summary>
        /// 柴油机中冷水系统进口温度
        /// </summary>
        public double ColdWaterSystemOfDieselEngineIn
        {
            get { return m_ColdWaterSystemOfDieselEngineIn; }
            set
            {
                if (value.Equals(m_ColdWaterSystemOfDieselEngineIn))
                    return;
                m_ColdWaterSystemOfDieselEngineIn = value;
                RaisePropertyChanged(() => ColdWaterSystemOfDieselEngineIn);
            }
        }

        /// <summary>
        /// 机油进口温度
        /// </summary>

        public double OilInletTemperature
        {
            get { return m_OilInletTemperature; }
            set
            {
                if (value.Equals(m_OilInletTemperature))
                    return;
                m_OilInletTemperature = value;
                RaisePropertyChanged(() => OilInletTemperature);
            }
        }

        /// <summary>
        /// 机油出口温度
        /// </summary>

        public double OilOutletTemperature
        {
            get { return m_OilOutletTemperature; }
            set
            {
                if (value.Equals(m_OilOutletTemperature))
                    return;
                m_OilOutletTemperature = value;
                RaisePropertyChanged(() => OilOutletTemperature);
            }
        }

        /// <summary>
        /// 热交换机水温
        /// </summary>

        public double WaterTemperatureOfHeatExchanger
        {
            get { return m_WaterTemperatureOfHeatExchanger; }
            set
            {
                if (value.Equals(m_WaterTemperatureOfHeatExchanger))
                    return;
                m_WaterTemperatureOfHeatExchanger = value;
                RaisePropertyChanged(() => WaterTemperatureOfHeatExchanger);
            }
        }

        /// <summary>
        /// 柴油机进口压力
        /// </summary>
        public double InletPressureOfDieselEngine
        {
            get { return m_InletPressureOfDieselEngine; }
            set
            {
                if (value.Equals(m_InletPressureOfDieselEngine))
                    return;
                m_InletPressureOfDieselEngine = value;
                RaisePropertyChanged(() => InletPressureOfDieselEngine);
            }
        }

        /// <summary>
        /// 柴油机出口压力
        /// </summary>
        public double OutletPressureOfDieselEngine
        {
            get { return m_OutletPressureOfDieselEngine; }
            set
            {
                if (value.Equals(m_OutletPressureOfDieselEngine))
                    return;
                m_OutletPressureOfDieselEngine = value;
                RaisePropertyChanged(() => OutletPressureOfDieselEngine);
            }
        }

        /// <summary>
        /// 前增压器进口压力
        /// </summary>
        public double FrontTurbochargerInletPressure
        {
            get { return m_FrontTurbochargerInletPressure; }
            set
            {
                if (value.Equals(m_FrontTurbochargerInletPressure))
                    return;
                m_FrontTurbochargerInletPressure = value;
                RaisePropertyChanged(() => FrontTurbochargerInletPressure);
            }
        }

        /// <summary>
        /// 后增压器进口压力
        /// </summary>
        public double AfterTurbochargerInletPressure
        {
            get { return m_AfterTurbochargerInletPressure; }
            set
            {
                if (value.Equals(m_AfterTurbochargerInletPressure))
                    return;
                m_AfterTurbochargerInletPressure = value;
                RaisePropertyChanged(() => AfterTurbochargerInletPressure);
            }
        }

        /// <summary>
        /// 滤清器进口压力
        /// </summary>
        public double FilterInletPressure
        {
            get { return m_FilterInletPressure; }
            set
            {
                if (value.Equals(m_FilterInletPressure))
                    return;
                m_FilterInletPressure = value;
                RaisePropertyChanged(() => FilterInletPressure);
            }
        }

        /// <summary>
        /// 燃油泵出口压力
        /// </summary>
        public double FuelPumpOutletPressure
        {
            get { return m_FuelPumpOutletPressure; }
            set
            {
                if (value.Equals(m_FuelPumpOutletPressure))
                    return;
                m_FuelPumpOutletPressure = value;
                RaisePropertyChanged(() => FuelPumpOutletPressure);
            }
        }

        /// <summary>
        /// 系统末端
        /// </summary>
        public double SystemEndingPressure
        {
            get { return m_SystemEndingPressure; }
            set
            {
                if (value.Equals(m_SystemEndingPressure))
                    return;
                m_SystemEndingPressure = value;
                RaisePropertyChanged(() => SystemEndingPressure);
            }
        }

        /// <summary>
        /// 1D电流
        /// </summary>
        public double ElectricCurrent1D
        {
            get { return m_ElectricCurrent1D; }
            set
            {
                if (value.Equals(m_ElectricCurrent1D))
                    return;
                m_ElectricCurrent1D = value;
                RaisePropertyChanged(() => ElectricCurrent1D);
            }
        }

        /// <summary>
        /// 2D电流
        /// </summary>
        public double ElectricCurrent2D
        {
            get { return m_ElectricCurrent2D; }
            set
            {
                if (value.Equals(m_ElectricCurrent2D))
                    return;
                m_ElectricCurrent2D = value;
                RaisePropertyChanged(() => ElectricCurrent2D);
            }
        }

        /// <summary>
        /// 3D电流
        /// </summary>
        public double ElectricCurrent3D
        {
            get { return m_ElectricCurrent3D; }
            set
            {
                if (value.Equals(m_ElectricCurrent3D))
                    return;
                m_ElectricCurrent3D = value;
                RaisePropertyChanged(() => ElectricCurrent3D);
            }
        }

        /// <summary>
        /// 4D电流
        /// </summary>
        public double ElectricCurrent4D
        {
            get { return m_ElectricCurrent4D; }
            set
            {
                if (value.Equals(m_ElectricCurrent4D))
                    return;
                m_ElectricCurrent4D = value;
                RaisePropertyChanged(() => ElectricCurrent4D);
            }
        }

        /// <summary>
        /// 5D电流
        /// </summary>
        public double ElectricCurrent5D
        {
            get { return m_ElectricCurrent5D; }
            set
            {
                if (value.Equals(m_ElectricCurrent5D))
                    return;
                m_ElectricCurrent5D = value;
                RaisePropertyChanged(() => ElectricCurrent5D);
            }
        }

        /// <summary>
        /// 6D电流
        /// </summary>
        public double ElectricCurrent6D
        {
            get { return m_ElectricCurrent6D; }
            set
            {
                if (value.Equals(m_ElectricCurrent6D))
                    return;
                m_ElectricCurrent6D = value;
                RaisePropertyChanged(() => ElectricCurrent6D);
            }
        }

        /// <summary>
        /// 电机电流差
        /// </summary>
        public double MotorCurrentDifference
        {
            get { return m_MotorCurrentDifference; }
            set
            {
                if (value.Equals(m_MotorCurrentDifference))
                    return;
                m_MotorCurrentDifference = value;
                RaisePropertyChanged(() => MotorCurrentDifference);
            }
        }

        /// <summary>
        /// 电流分配系数
        /// </summary>
        public double CurrentDistributionFactor
        {
            get { return m_CurrentDistributionFactor; }
            set
            {
                if (value.Equals(m_CurrentDistributionFactor))
                    return;
                m_CurrentDistributionFactor = value;
                RaisePropertyChanged(() => CurrentDistributionFactor);
            }
        }

        /// <summary>
        /// 转速1D
        /// </summary>
        public double Speed1D
        {
            get { return m_Speed1D; }
            set
            {
                if (value.Equals(m_Speed1D))
                    return;
                m_Speed1D = value;
                RaisePropertyChanged(() => Speed1D);
            }
        }

        /// <summary>
        /// 转速2D
        /// </summary>
        public double Speed2D
        {
            get { return m_Speed2D; }
            set
            {
                if (value.Equals(m_Speed2D))
                    return;
                m_Speed2D = value;
                RaisePropertyChanged(() => Speed2D);
            }
        }

        /// <summary>
        /// 转速3D
        /// </summary>
        public double Speed3D
        {
            get { return m_Speed3D; }
            set
            {
                if (value.Equals(m_Speed3D))
                    return;
                m_Speed3D = value;
                RaisePropertyChanged(() => Speed3D);
            }
        }

        /// <summary>
        /// 转速4D
        /// </summary>
        public double Speed4D
        {
            get { return m_Speed4D; }
            set
            {
                if (value.Equals(m_Speed4D))
                    return;
                m_Speed4D = value;
                RaisePropertyChanged(() => Speed4D);
            }
        }

        /// <summary>
        /// 转速5D
        /// </summary>
        public double Speed5D
        {
            get { return m_Speed5D; }
            set
            {
                if (value.Equals(m_Speed5D))
                    return;
                m_Speed5D = value;
                RaisePropertyChanged(() => Speed5D);
            }
        }

        /// <summary>
        /// 转速6D
        /// </summary>
        public double Speed6D
        {
            get { return m_Speed6D; }
            set
            {
                if (value.Equals(m_Speed6D))
                    return;
                m_Speed6D = value;
                RaisePropertyChanged(() => Speed6D);
            }
        }

        /// <summary>
        /// 电机转速差
        /// </summary>
        public double MotorSpeedDifference
        {
            get { return m_MotorSpeedDifference; }
            set
            {
                if (value.Equals(m_MotorSpeedDifference))
                    return;
                m_MotorSpeedDifference = value;
                RaisePropertyChanged(() => MotorSpeedDifference);
            }
        }

        /// <summary>
        /// 冷却风扇转速1
        /// </summary>
        public double CoolingFanSpeed1
        {
            get { return m_CoolingFanSpeed1; }
            set
            {
                if (value.Equals(m_CoolingFanSpeed1))
                    return;
                m_CoolingFanSpeed1 = value;
                RaisePropertyChanged(() => CoolingFanSpeed1);
            }
        }

        /// <summary>
        /// 冷却风扇转速2
        /// </summary>
        public double CoolingFanSpeed2
        {
            get { return m_CoolingFanSpeed2; }
            set
            {
                if (value.Equals(m_CoolingFanSpeed2))
                    return;
                m_CoolingFanSpeed2 = value;
                RaisePropertyChanged(() => CoolingFanSpeed2);
            }
        }

        /// <summary>
        /// 励磁机励磁电流
        /// </summary>
        public double ExciterCurrent
        {
            get { return m_ExciterCurrent; }
            set
            {
                if (value.Equals(m_ExciterCurrent))
                    return;
                m_ExciterCurrent = value;
                RaisePropertyChanged(() => ExciterCurrent);
            }
        }

        /// <summary>
        /// 主发电机励磁电流
        /// </summary>
        public double MainGeneratorExciterCurrent
        {
            get { return m_MainGeneratorExciterCurrent; }
            set
            {
                if (value.Equals(m_MainGeneratorExciterCurrent))
                    return;
                m_MainGeneratorExciterCurrent = value;
                RaisePropertyChanged(() => MainGeneratorExciterCurrent);
            }
        }

        /// <summary>
        /// 接地漏电流
        /// </summary>
        public double EarthLeakageCurrent
        {
            get { return m_EarthLeakageCurrent; }
            set
            {
                if (value.Equals(m_EarthLeakageCurrent))
                    return;
                m_EarthLeakageCurrent = value;
                RaisePropertyChanged(() => EarthLeakageCurrent);
            }
        }

        /// <summary>
        /// 前增压器转速
        /// </summary>
        public double FrontTurbochargerSpeed
        {
            get { return m_FrontTurbochargerSpeed; }
            set
            {
                if (value.Equals(m_FrontTurbochargerSpeed))
                    return;
                m_FrontTurbochargerSpeed = value;
                RaisePropertyChanged(() => FrontTurbochargerSpeed);
            }
        }

        /// <summary>
        /// 后增压器转速
        /// </summary>
        public double AfterTurbochargerSpeed
        {
            get { return m_AfterTurbochargerSpeed; }
            set
            {
                if (value.Equals(m_AfterTurbochargerSpeed))
                    return;
                m_AfterTurbochargerSpeed = value;
                RaisePropertyChanged(() => AfterTurbochargerSpeed);
            }
        }

        /// <summary>
        /// 降功环温1
        /// </summary>
        public double ReducedPowerLoopTemperature1
        {
            get { return m_ReducedPowerLoopTemperature1; }
            set
            {
                if (value.Equals(m_ReducedPowerLoopTemperature1))
                    return;
                m_ReducedPowerLoopTemperature1 = value;
                RaisePropertyChanged(() => ReducedPowerLoopTemperature1);
            }
        }

        /// <summary>
        /// 降功环温2
        /// </summary>
        public double ReducedPowerLoopTemperature2
        {
            get { return m_ReducedPowerLoopTemperature2; }
            set
            {
                if (value.Equals(m_ReducedPowerLoopTemperature2))
                    return;
                m_ReducedPowerLoopTemperature2 = value;
                RaisePropertyChanged(() => ReducedPowerLoopTemperature2);
            }
        }

        /// <summary>
        /// 柴油机高温水系统出口温度
        /// </summary>
        public double OtherHighWaterTemperatureSystemOfDieselEngineOut
        {
            get { return m_OtherHighWaterTemperatureSystemOfDieselEngineOut; }
            set
            {
                if (value.Equals(m_OtherHighWaterTemperatureSystemOfDieselEngineOut))
                    return;
                m_OtherHighWaterTemperatureSystemOfDieselEngineOut = value;
                RaisePropertyChanged(() => OtherHighWaterTemperatureSystemOfDieselEngineOut);
            }
        }

        /// <summary>
        /// 柴油机高温水系统进口温度
        /// </summary>
        public double OtherHighWaterTemperatureSystemOfDieselEngineIn
        {
            get { return m_OtherHighWaterTemperatureSystemOfDieselEngineIn; }
            set
            {
                if (value.Equals(m_OtherHighWaterTemperatureSystemOfDieselEngineIn))
                    return;
                m_OtherHighWaterTemperatureSystemOfDieselEngineIn = value;
                RaisePropertyChanged(() => OtherHighWaterTemperatureSystemOfDieselEngineIn);
            }
        }

        /// <summary>
        /// 柴油机中冷水系统出口温度
        /// </summary>
        public double OtherColdWaterSystemOfDieselEngineOut
        {
            get { return m_OtherColdWaterSystemOfDieselEngineOut; }
            set
            {
                if (value.Equals(m_OtherColdWaterSystemOfDieselEngineOut))
                    return;
                m_OtherColdWaterSystemOfDieselEngineOut = value;
                RaisePropertyChanged(() => OtherColdWaterSystemOfDieselEngineOut);
            }
        }

        /// <summary>
        /// 柴油机中冷水系统进口温度
        /// </summary>
        public double OtherColdWaterSystemOfDieselEngineIn
        {
            get { return m_OtherColdWaterSystemOfDieselEngineIn; }
            set
            {
                if (value.Equals(m_OtherColdWaterSystemOfDieselEngineIn))
                    return;
                m_OtherColdWaterSystemOfDieselEngineIn = value;
                RaisePropertyChanged(() => OtherColdWaterSystemOfDieselEngineIn);
            }
        }

        /// <summary>
        /// 机油进口温度
        /// </summary>

        public double OtherOilInletTemperature
        {
            get { return m_OtherOilInletTemperature; }
            set
            {
                if (value.Equals(m_OtherOilInletTemperature))
                    return;
                m_OtherOilInletTemperature = value;
                RaisePropertyChanged(() => OtherOilInletTemperature);
            }
        }

        /// <summary>
        /// 机油出口温度
        /// </summary>

        public double OtherOilOutletTemperature
        {
            get { return m_OtherOilOutletTemperature; }
            set
            {
                if (value.Equals(m_OtherOilOutletTemperature))
                    return;
                m_OtherOilOutletTemperature = value;
                RaisePropertyChanged(() => OtherOilOutletTemperature);
            }
        }

        /// <summary>
        /// 热交换机水温
        /// </summary>

        public double OtherWaterTemperatureOfHeatExchanger
        {
            get { return m_OtherWaterTemperatureOfHeatExchanger; }
            set
            {
                if (value.Equals(m_OtherWaterTemperatureOfHeatExchanger))
                    return;
                m_OtherWaterTemperatureOfHeatExchanger = value;
                RaisePropertyChanged(() => OtherWaterTemperatureOfHeatExchanger);
            }
        }

        /// <summary>
        /// 柴油机进口压力
        /// </summary>
        public double OtherInletPressureOfDieselEngine
        {
            get { return m_OtherInletPressureOfDieselEngine; }
            set
            {
                if (value.Equals(m_OtherInletPressureOfDieselEngine))
                    return;
                m_OtherInletPressureOfDieselEngine = value;
                RaisePropertyChanged(() => OtherInletPressureOfDieselEngine);
            }
        }

        /// <summary>
        /// 柴油机出口压力
        /// </summary>
        public double OtherOutletPressureOfDieselEngine
        {
            get { return m_OtherOutletPressureOfDieselEngine; }
            set
            {
                if (value.Equals(m_OtherOutletPressureOfDieselEngine))
                    return;
                m_OtherOutletPressureOfDieselEngine = value;
                RaisePropertyChanged(() => OtherOutletPressureOfDieselEngine);
            }
        }

        /// <summary>
        /// 前增压器进口压力
        /// </summary>
        public double OtherFrontTurbochargerInletPressure
        {
            get { return m_OtherFrontTurbochargerInletPressure; }
            set
            {
                if (value.Equals(m_OtherFrontTurbochargerInletPressure))
                    return;
                m_OtherFrontTurbochargerInletPressure = value;
                RaisePropertyChanged(() => OtherFrontTurbochargerInletPressure);
            }
        }

        /// <summary>
        /// 后增压器进口压力
        /// </summary>
        public double OtherAfterTurbochargerInletPressure
        {
            get { return m_OtherAfterTurbochargerInletPressure; }
            set
            {
                if (value.Equals(m_OtherAfterTurbochargerInletPressure))
                    return;
                m_OtherAfterTurbochargerInletPressure = value;
                RaisePropertyChanged(() => OtherAfterTurbochargerInletPressure);
            }
        }

        /// <summary>
        /// 滤清器进口压力
        /// </summary>
        public double OtherFilterInletPressure
        {
            get { return m_OtherFilterInletPressure; }
            set
            {
                if (value.Equals(m_OtherFilterInletPressure))
                    return;
                m_OtherFilterInletPressure = value;
                RaisePropertyChanged(() => OtherFilterInletPressure);
            }
        }

        /// <summary>
        /// 燃油泵出口压力
        /// </summary>
        public double OtherFuelPumpOutletPressure
        {
            get { return m_OtherFuelPumpOutletPressure; }
            set
            {
                if (value.Equals(m_OtherFuelPumpOutletPressure))
                    return;
                m_OtherFuelPumpOutletPressure = value;
                RaisePropertyChanged(() => OtherFuelPumpOutletPressure);
            }
        }

        /// <summary>
        /// 系统末端
        /// </summary>
        public double OtherSystemEndingPressure
        {
            get { return m_OtherSystemEndingPressure; }
            set
            {
                if (value.Equals(m_OtherSystemEndingPressure))
                    return;
                m_OtherSystemEndingPressure = value;
                RaisePropertyChanged(() => OtherSystemEndingPressure);
            }
        }

        /// <summary>
        /// 1D电流
        /// </summary>
        public double OtherElectricCurrent1D
        {
            get { return m_OtherElectricCurrent1D; }
            set
            {
                if (value.Equals(m_OtherElectricCurrent1D))
                    return;
                m_OtherElectricCurrent1D = value;
                RaisePropertyChanged(() => OtherElectricCurrent1D);
            }
        }

        /// <summary>
        /// 2D电流
        /// </summary>
        public double OtherElectricCurrent2D
        {
            get { return m_OtherElectricCurrent2D; }
            set
            {
                if (value.Equals(m_OtherElectricCurrent2D))
                    return;
                m_OtherElectricCurrent2D = value;
                RaisePropertyChanged(() => OtherElectricCurrent2D);
            }
        }

        /// <summary>
        /// 3D电流
        /// </summary>
        public double OtherElectricCurrent3D
        {
            get { return m_OtherElectricCurrent3D; }
            set
            {
                if (value.Equals(m_OtherElectricCurrent3D))
                    return;
                m_OtherElectricCurrent3D = value;
                RaisePropertyChanged(() => OtherElectricCurrent3D);
            }
        }

        /// <summary>
        /// 4D电流
        /// </summary>
        public double OtherElectricCurrent4D
        {
            get { return m_OtherElectricCurrent4D; }
            set
            {
                if (value.Equals(m_OtherElectricCurrent4D))
                    return;
                m_OtherElectricCurrent4D = value;
                RaisePropertyChanged(() => OtherElectricCurrent4D);
            }
        }

        /// <summary>
        /// 5D电流
        /// </summary>
        public double OtherElectricCurrent5D
        {
            get { return m_OtherElectricCurrent5D; }
            set
            {
                if (value.Equals(m_OtherElectricCurrent5D))
                    return;
                m_OtherElectricCurrent5D = value;
                RaisePropertyChanged(() => OtherElectricCurrent5D);
            }
        }

        /// <summary>
        /// 6D电流
        /// </summary>
        public double OtherElectricCurrent6D
        {
            get { return m_OtherElectricCurrent6D; }
            set
            {
                if (value.Equals(m_OtherElectricCurrent6D))
                    return;
                m_OtherElectricCurrent6D = value;
                RaisePropertyChanged(() => OtherElectricCurrent6D);
            }
        }

        /// <summary>
        /// 电机电流差
        /// </summary>
        public double OtherMotorCurrentDifference
        {
            get { return m_OtherMotorCurrentDifference; }
            set
            {
                if (value.Equals(m_OtherMotorCurrentDifference))
                    return;
                m_OtherMotorCurrentDifference = value;
                RaisePropertyChanged(() => OtherMotorCurrentDifference);
            }
        }

        /// <summary>
        /// 电流分配系数
        /// </summary>
        public double OtherCurrentDistributionFactor
        {
            get { return m_OtherCurrentDistributionFactor; }
            set
            {
                if (value.Equals(m_OtherCurrentDistributionFactor))
                    return;
                m_OtherCurrentDistributionFactor = value;
                RaisePropertyChanged(() => OtherCurrentDistributionFactor);
            }
        }

        /// <summary>
        /// 转速1D
        /// </summary>
        public double OtherSpeed1D
        {
            get { return m_OtherSpeed1D; }
            set
            {
                if (value.Equals(m_OtherSpeed1D))
                    return;
                m_OtherSpeed1D = value;
                RaisePropertyChanged(() => OtherSpeed1D);
            }
        }

        /// <summary>
        /// 转速2D
        /// </summary>
        public double OtherSpeed2D
        {
            get { return m_OtherSpeed2D; }
            set
            {
                if (value.Equals(m_OtherSpeed2D))
                    return;
                m_OtherSpeed2D = value;
                RaisePropertyChanged(() => OtherSpeed2D);
            }
        }

        /// <summary>
        /// 转速3D
        /// </summary>
        public double OtherSpeed3D
        {
            get { return m_OtherSpeed3D; }
            set
            {
                if (value.Equals(m_OtherSpeed3D))
                    return;
                m_OtherSpeed3D = value;
                RaisePropertyChanged(() => OtherSpeed3D);
            }
        }

        /// <summary>
        /// 转速4D
        /// </summary>
        public double OtherSpeed4D
        {
            get { return m_OtherSpeed4D; }
            set
            {
                if (value.Equals(m_OtherSpeed4D))
                    return;
                m_OtherSpeed4D = value;
                RaisePropertyChanged(() => OtherSpeed4D);
            }
        }

        /// <summary>
        /// 转速5D
        /// </summary>
        public double OtherSpeed5D
        {
            get { return m_OtherSpeed5D; }
            set
            {
                if (value.Equals(m_OtherSpeed5D))
                    return;
                m_OtherSpeed5D = value;
                RaisePropertyChanged(() => OtherSpeed5D);
            }
        }

        /// <summary>
        /// 转速6D
        /// </summary>
        public double OtherSpeed6D
        {
            get { return m_OtherSpeed6D; }
            set
            {
                if (value.Equals(m_OtherSpeed6D))
                    return;
                m_OtherSpeed6D = value;
                RaisePropertyChanged(() => OtherSpeed6D);
            }
        }

        /// <summary>
        /// 电机转速差
        /// </summary>
        public double OtherMotorSpeedDifference
        {
            get { return m_OtherMotorSpeedDifference; }
            set
            {
                if (value.Equals(m_OtherMotorSpeedDifference))
                    return;
                m_OtherMotorSpeedDifference = value;
                RaisePropertyChanged(() => OtherMotorSpeedDifference);
            }
        }

        /// <summary>
        /// 冷却风扇转速1
        /// </summary>
        public double OtherCoolingFanSpeed1
        {
            get { return m_OtherCoolingFanSpeed1; }
            set
            {
                if (value.Equals(m_OtherCoolingFanSpeed1))
                    return;
                m_OtherCoolingFanSpeed1 = value;
                RaisePropertyChanged(() => OtherCoolingFanSpeed1);
            }
        }

        /// <summary>
        /// 冷却风扇转速2
        /// </summary>
        public double OtherCoolingFanSpeed2
        {
            get { return m_OtherCoolingFanSpeed2; }
            set
            {
                if (value.Equals(m_OtherCoolingFanSpeed2))
                    return;
                m_OtherCoolingFanSpeed2 = value;
                RaisePropertyChanged(() => OtherCoolingFanSpeed2);
            }
        }

        /// <summary>
        /// 励磁机励磁电流
        /// </summary>
        public double OtherExciterCurrent
        {
            get { return m_OtherExciterCurrent; }
            set
            {
                if (value.Equals(m_OtherExciterCurrent))
                    return;
                m_OtherExciterCurrent = value;
                RaisePropertyChanged(() => OtherExciterCurrent);
            }
        }

        /// <summary>
        /// 主发电机励磁电流
        /// </summary>
        public double OtherMainGeneratorExciterCurrent
        {
            get { return m_OtherMainGeneratorExciterCurrent; }
            set
            {
                if (value.Equals(m_OtherMainGeneratorExciterCurrent))
                    return;
                m_OtherMainGeneratorExciterCurrent = value;
                RaisePropertyChanged(() => OtherMainGeneratorExciterCurrent);
            }
        }

        /// <summary>
        /// 接地漏电流
        /// </summary>
        public double OtherEarthLeakageCurrent
        {
            get { return m_OtherEarthLeakageCurrent; }
            set
            {
                if (value.Equals(m_OtherEarthLeakageCurrent))
                    return;
                m_OtherEarthLeakageCurrent = value;
                RaisePropertyChanged(() => OtherEarthLeakageCurrent);
            }
        }

        /// <summary>
        /// 前增压器转速
        /// </summary>
        public double OtherFrontTurbochargerSpeed
        {
            get { return m_OtherFrontTurbochargerSpeed; }
            set
            {
                if (value.Equals(m_OtherFrontTurbochargerSpeed))
                    return;
                m_OtherFrontTurbochargerSpeed = value;
                RaisePropertyChanged(() => OtherFrontTurbochargerSpeed);
            }
        }

        /// <summary>
        /// 后增压器转速
        /// </summary>
        public double OtherAfterTurbochargerSpeed
        {
            get { return m_OtherAfterTurbochargerSpeed; }
            set
            {
                if (value.Equals(m_OtherAfterTurbochargerSpeed))
                    return;
                m_OtherAfterTurbochargerSpeed = value;
                RaisePropertyChanged(() => OtherAfterTurbochargerSpeed);
            }
        }

        /// <summary>
        /// 降功环温1
        /// </summary>
        public double OtherReducedPowerLoopTemperature1
        {
            get { return m_OtherReducedPowerLoopTemperature1; }
            set
            {
                if (value.Equals(m_OtherReducedPowerLoopTemperature1))
                    return;
                m_OtherReducedPowerLoopTemperature1 = value;
                RaisePropertyChanged(() => OtherReducedPowerLoopTemperature1);
            }
        }

        /// <summary>
        /// 降功环温2
        /// </summary>
        public double OtherReducedPowerLoopTemperature2
        {
            get { return m_OtherReducedPowerLoopTemperature2; }
            set
            {
                if (value.Equals(m_OtherReducedPowerLoopTemperature2))
                    return;
                m_OtherReducedPowerLoopTemperature2 = value;
                RaisePropertyChanged(() => OtherReducedPowerLoopTemperature2);
            }
        }
    }
}
