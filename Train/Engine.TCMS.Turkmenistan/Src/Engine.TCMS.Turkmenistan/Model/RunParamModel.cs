using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class RunParamModel : NotificationObject
    {
        /// <summary>
        /// 柴油机高温水系统出口温度
        /// </summary>
        public double HighWaterTemperatureSystemOfDieselEngineOut { get; set; }
        /// <summary>
        /// 柴油机高温水系统进口温度
        /// </summary>
        public double HighWaterTemperatureSystemOfDieselEngineIn { get; set; }
        /// <summary>
        /// 柴油机中冷水系统出口温度
        /// </summary>
        public double ColdWaterSystemOfDieselEngineOut { get; set; }
        /// <summary>
        /// 柴油机中冷水系统进口温度
        /// </summary>
        public double ColdWaterSystemOfDieselEngineIn { get; set; }
        /// <summary>
        /// 机油进口温度
        /// </summary>

        public double OilInletTemperature { get; set; }
        /// <summary>
        /// 机油出口温度
        /// </summary>

        public double OilOutletTemperature { get; set; }
        /// <summary>
        /// 热交换机水温
        /// </summary>

        public double WaterTemperatureOfHeatExchanger { get; set; }
        /// <summary>
        /// 柴油机进口压力
        /// </summary>
        public double InletPressureOfDieselEngine { get; set; }
        /// <summary>
        /// 柴油机出口压力
        /// </summary>
        public double OutletPressureOfDieselEngine { get; set; }
        /// <summary>
        /// 前增压器进口压力
        /// </summary>
        public double FrontTurbochargerInletPressure { get; set; }
        /// <summary>
        /// 后增压器进口压力
        /// </summary>
        public double AfterTurbochargerInletPressure { get; set; }
        /// <summary>
        /// 滤清器进口压力
        /// </summary>
        public double FilterInletPressure { get; set; }
        /// <summary>
        /// 燃油泵出口压力
        /// </summary>
        public double FuelPumpOutletPressure { get; set; }
        /// <summary>
        /// 系统末端
        /// </summary>
        public double SystemEndingPressure { get; set; }
        /// <summary>
        /// 1D电流
        /// </summary>
        public double ElectricCurrent1D { get; set; }
        /// <summary>
        /// 2D电流
        /// </summary>
        public double ElectricCurrent2D { get; set; }
        /// <summary>
        /// 3D电流
        /// </summary>
        public double ElectricCurrent3D { get; set; }
        /// <summary>
        /// 4D电流
        /// </summary>
        public double ElectricCurrent4D { get; set; }
        /// <summary>
        /// 5D电流
        /// </summary>
        public double ElectricCurrent5D { get; set; }
        /// <summary>
        /// 6D电流
        /// </summary>
        public double ElectricCurrent6D { get; set; }
        /// <summary>
        /// 电机电流差
        /// </summary>
        public double MotorCurrentDifference { get; set; }
        /// <summary>
        /// 电流分配系数
        /// </summary>
        public double CurrentDistributionFactor { get; set; }
        /// <summary>
        /// 转速1D
        /// </summary>
        public double Speed1D { get; set; }
        /// <summary>
        /// 转速2D
        /// </summary>
        public double Speed2D { get; set; }
        /// <summary>
        /// 转速3D
        /// </summary>
        public double Speed3D { get; set; }
        /// <summary>
        /// 转速4D
        /// </summary>
        public double Speed4D { get; set; }
        /// <summary>
        /// 转速5D
        /// </summary>
        public double Speed5D { get; set; }
        /// <summary>
        /// 转速6D
        /// </summary>
        public double Speed6D { get; set; }
        /// <summary>
        /// 电机转速差
        /// </summary>
        public double MotorSpeedDifference { get; set; }
        /// <summary>
        /// 冷却风扇转速1
        /// </summary>
        public double CoolingFanSpeed1 { get; set; }
        /// <summary>
        /// 冷却风扇转速2
        /// </summary>
        public double CoolingFanSpeed2 { get; set; }
        /// <summary>
        /// 励磁机励磁电流
        /// </summary>
        public double ExciterCurrent { get; set; }
        /// <summary>
        /// 主发电机励磁电流
        /// </summary>
        public double MainGeneratorExciterCurrent { get; set; }
        /// <summary>
        /// 接地漏电流
        /// </summary>
        public double EarthLeakageCurrent { get; set; }
        /// <summary>
        /// 前增压器转速
        /// </summary>
        public double FrontTurbochargerSpeed { get; set; }
        /// <summary>
        /// 后增压器转速
        /// </summary>
        public double AfterTurbochargerSpeed { get; set; }
        /// <summary>
        /// 降功环温1
        /// </summary>
        public double ReducedPowerLoopTemperature1 { get; set; }
        /// <summary>
        /// 降功环温2
        /// </summary>
        public double ReducedPowerLoopTemperature2 { get; set; }
        /// <summary>
        /// 柴油机高温水系统出口温度
        /// </summary>
        public double OtherHighWaterTemperatureSystemOfDieselEngineOut { get; set; }
        /// <summary>
        /// 柴油机高温水系统进口温度
        /// </summary>
        public double OtherHighWaterTemperatureSystemOfDieselEngineIn { get; set; }
        /// <summary>
        /// 柴油机中冷水系统出口温度
        /// </summary>
        public double OtherColdWaterSystemOfDieselEngineOut { get; set; }
        /// <summary>
        /// 柴油机中冷水系统进口温度
        /// </summary>
        public double OtherColdWaterSystemOfDieselEngineIn { get; set; }
        /// <summary>
        /// 机油进口温度
        /// </summary>

        public double OtherOilInletTemperature { get; set; }
        /// <summary>
        /// 机油出口温度
        /// </summary>

        public double OtherOilOutletTemperature { get; set; }
        /// <summary>
        /// 热交换机水温
        /// </summary>

        public double OtherWaterTemperatureOfHeatExchanger { get; set; }
        /// <summary>
        /// 柴油机进口压力
        /// </summary>
        public double OtherInletPressureOfDieselEngine { get; set; }
        /// <summary>
        /// 柴油机出口压力
        /// </summary>
        public double OtherOutletPressureOfDieselEngine { get; set; }
        /// <summary>
        /// 前增压器进口压力
        /// </summary>
        public double OtherFrontTurbochargerInletPressure { get; set; }
        /// <summary>
        /// 后增压器进口压力
        /// </summary>
        public double OtherAfterTurbochargerInletPressure { get; set; }
        /// <summary>
        /// 滤清器进口压力
        /// </summary>
        public double OtherFilterInletPressure { get; set; }
        /// <summary>
        /// 燃油泵出口压力
        /// </summary>
        public double OtherFuelPumpOutletPressure { get; set; }
        /// <summary>
        /// 系统末端
        /// </summary>
        public double OtherSystemEndingPressure { get; set; }
        /// <summary>
        /// 1D电流
        /// </summary>
        public double OtherElectricCurrent1D { get; set; }
        /// <summary>
        /// 2D电流
        /// </summary>
        public double OtherElectricCurrent2D { get; set; }
        /// <summary>
        /// 3D电流
        /// </summary>
        public double OtherElectricCurrent3D { get; set; }
        /// <summary>
        /// 4D电流
        /// </summary>
        public double OtherElectricCurrent4D { get; set; }
        /// <summary>
        /// 5D电流
        /// </summary>
        public double OtherElectricCurrent5D { get; set; }
        /// <summary>
        /// 6D电流
        /// </summary>
        public double OtherElectricCurrent6D { get; set; }
        /// <summary>
        /// 电机电流差
        /// </summary>
        public double OtherMotorCurrentDifference { get; set; }
        /// <summary>
        /// 电流分配系数
        /// </summary>
        public double OtherCurrentDistributionFactor { get; set; }
        /// <summary>
        /// 转速1D
        /// </summary>
        public double OtherSpeed1D { get; set; }
        /// <summary>
        /// 转速2D
        /// </summary>
        public double OtherSpeed2D { get; set; }
        /// <summary>
        /// 转速3D
        /// </summary>
        public double OtherSpeed3D { get; set; }
        /// <summary>
        /// 转速4D
        /// </summary>
        public double OtherSpeed4D { get; set; }
        /// <summary>
        /// 转速5D
        /// </summary>
        public double OtherSpeed5D { get; set; }
        /// <summary>
        /// 转速6D
        /// </summary>
        public double OtherSpeed6D { get; set; }
        /// <summary>
        /// 电机转速差
        /// </summary>
        public double OtherMotorSpeedDifference { get; set; }
        /// <summary>
        /// 冷却风扇转速1
        /// </summary>
        public double OtherCoolingFanSpeed1 { get; set; }
        /// <summary>
        /// 冷却风扇转速2
        /// </summary>
        public double OtherCoolingFanSpeed2 { get; set; }
        /// <summary>
        /// 励磁机励磁电流
        /// </summary>
        public double OtherExciterCurrent { get; set; }
        /// <summary>
        /// 主发电机励磁电流
        /// </summary>
        public double OtherMainGeneratorExciterCurrent { get; set; }
        /// <summary>
        /// 接地漏电流
        /// </summary>
        public double OtherEarthLeakageCurrent { get; set; }
        /// <summary>
        /// 前增压器转速
        /// </summary>
        public double OtherFrontTurbochargerSpeed { get; set; }
        /// <summary>
        /// 后增压器转速
        /// </summary>
        public double OtherAfterTurbochargerSpeed { get; set; }
        /// <summary>
        /// 降功环温1
        /// </summary>
        public double OtherReducedPowerLoopTemperature1 { get; set; }
        /// <summary>
        /// 降功环温2
        /// </summary>
        public double OtherReducedPowerLoopTemperature2 { get; set; }
    }
}
