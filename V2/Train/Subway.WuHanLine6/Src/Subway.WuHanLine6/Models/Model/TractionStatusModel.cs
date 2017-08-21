using System.ComponentModel.Composition;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 制动状态Model
    /// </summary>
    [Export]
    public class TractionStatusModel : ModelBase
    {
        private bool m_LineBrakerF0052;
        private bool m_LineBrakerF0042;
        private bool m_LineBrakerF0032;
        private bool m_LineBrakerF0022;
        private bool m_LineBrakerF0051;
        private bool m_LineBrakerF0041;
        private bool m_LineBrakerF0031;
        private bool m_LineBrakerF0021;
        private double m_ResistanceBrakeF005;
        private double m_ResistanceBrakeF004;
        private double m_ResistanceBrakeF003;
        private double m_ResistanceBrakeF002;
        private double m_FliterF005;
        private double m_FliterF004;
        private double m_FliterF003;
        private double m_FliterF002;
        private double m_ElectricityNetF005;
        private double m_ElectricityNetF004;
        private double m_ElectricityNetF003;
        private double m_ElectricityNetF002;
        private double m_RebrithBrakeF005;
        private double m_RebrithBrakeF004;
        private double m_RebrithBrakeF003;
        private double m_RebrithBrakeF002;
        private double m_TractionEnergyF005;
        private double m_TractionEnergyF004;
        private double m_TractionEnergyF003;
        private double m_TractionEnergyF002;
        private double m_ElectricityMachineF005;
        private double m_ElectricityMachineF004;
        private double m_ElectricityMachineF003;
        private double m_ElectricityMachineF002;
        private double m_TarctionF005;
        private double m_TarctionF004;
        private double m_TarctionF003;
        private double m_TarctionF002;
        private double m_ActualElectricityF005;
        private double m_ActualElectricityF004;
        private double m_ActualElectricityF003;
        private double m_ActualElectricityF002;
        private bool m_TractionExceptionF005;
        private bool m_TractionExceptionF004;
        private bool m_TractionExceptionF003;
        private bool m_TractionExceptionF002;

        /// <summary>
        /// 牵引系统状态F002
        /// </summary>
        public bool TractionExceptionF002
        {
            get { return m_TractionExceptionF002; }
            set
            {
                if (value == m_TractionExceptionF002)
                {
                    return;
                }
                m_TractionExceptionF002 = value;
                RaisePropertyChanged(() => TractionExceptionF002);
            }
        }

        /// <summary>
        /// 牵引系统状态F003
        /// </summary>
        public bool TractionExceptionF003
        {
            get { return m_TractionExceptionF003; }
            set
            {
                if (value == m_TractionExceptionF003)
                {
                    return;
                }
                m_TractionExceptionF003 = value;
                RaisePropertyChanged(() => TractionExceptionF003);
            }
        }

        /// <summary>
        /// 牵引系统状态F004
        /// </summary>
        public bool TractionExceptionF004
        {
            get { return m_TractionExceptionF004; }
            set
            {
                if (value == m_TractionExceptionF004)
                {
                    return;
                }
                m_TractionExceptionF004 = value;
                RaisePropertyChanged(() => TractionExceptionF004);
            }
        }

        /// <summary>
        /// 牵引系统状态F005
        /// </summary>
        public bool TractionExceptionF005
        {
            get { return m_TractionExceptionF005; }
            set
            {
                if (value == m_TractionExceptionF005)
                {
                    return;
                }
                m_TractionExceptionF005 = value;
                RaisePropertyChanged(() => TractionExceptionF005);
            }
        }

        /// <summary>
        /// 实际电制力
        /// </summary>
        public double ActualElectricityF002
        {
            get { return m_ActualElectricityF002; }
            set
            {
                if (value.Equals(m_ActualElectricityF002))
                {
                    return;
                }
                m_ActualElectricityF002 = value;
                RaisePropertyChanged(() => ActualElectricityF002);
            }
        }

        /// <summary>
        /// 实际电制力
        /// </summary>
        public double ActualElectricityF003
        {
            get { return m_ActualElectricityF003; }
            set
            {
                if (value.Equals(m_ActualElectricityF003))
                {
                    return;
                }
                m_ActualElectricityF003 = value;
                RaisePropertyChanged(() => ActualElectricityF003);
            }
        }

        /// <summary>
        /// 实际电制力
        /// </summary>
        public double ActualElectricityF004
        {
            get { return m_ActualElectricityF004; }
            set
            {
                if (value.Equals(m_ActualElectricityF004))
                {
                    return;
                }
                m_ActualElectricityF004 = value;
                RaisePropertyChanged(() => ActualElectricityF004);
            }
        }

        /// <summary>
        /// 实际电制力
        /// </summary>
        public double ActualElectricityF005
        {
            get { return m_ActualElectricityF005; }
            set
            {
                if (value.Equals(m_ActualElectricityF005))
                {
                    return;
                }
                m_ActualElectricityF005 = value;
                RaisePropertyChanged(() => ActualElectricityF005);
            }
        }

        /// <summary>
        /// 牵引力
        /// </summary>
        public double TarctionF002
        {
            get { return m_TarctionF002; }
            set
            {
                if (value.Equals(m_TarctionF002))
                {
                    return;
                }
                m_TarctionF002 = value;
                RaisePropertyChanged(() => TarctionF002);
            }
        }

        /// <summary>
        /// 牵引力
        /// </summary>
        public double TarctionF003
        {
            get { return m_TarctionF003; }
            set
            {
                if (value.Equals(m_TarctionF003))
                {
                    return;
                }
                m_TarctionF003 = value;
                RaisePropertyChanged(() => TarctionF003);
            }
        }

        /// <summary>
        /// 牵引力
        /// </summary>
        public double TarctionF004
        {
            get { return m_TarctionF004; }
            set
            {
                if (value.Equals(m_TarctionF004))
                {
                    return;
                }
                m_TarctionF004 = value;
                RaisePropertyChanged(() => TarctionF004);
            }
        }

        /// <summary>
        /// 牵引力
        /// </summary>
        public double TarctionF005
        {
            get { return m_TarctionF005; }
            set
            {
                if (value.Equals(m_TarctionF005))
                {
                    return;
                }
                m_TarctionF005 = value;
                RaisePropertyChanged(() => TarctionF005);
            }
        }

        /// <summary>
        /// 电机电流
        /// </summary>
        public double ElectricityMachineF002
        {
            get { return m_ElectricityMachineF002; }
            set
            {
                if (value.Equals(m_ElectricityMachineF002))
                {
                    return;
                }
                m_ElectricityMachineF002 = value;
                RaisePropertyChanged(() => ElectricityMachineF002);
            }
        }

        /// <summary>
        /// 电机电流
        /// </summary>
        public double ElectricityMachineF003
        {
            get { return m_ElectricityMachineF003; }
            set
            {
                if (value.Equals(m_ElectricityMachineF003))
                {
                    return;
                }
                m_ElectricityMachineF003 = value;
                RaisePropertyChanged(() => ElectricityMachineF003);
            }
        }

        /// <summary>
        /// 电机电流
        /// </summary>
        public double ElectricityMachineF004
        {
            get { return m_ElectricityMachineF004; }
            set
            {
                if (value.Equals(m_ElectricityMachineF004))
                {
                    return;
                }
                m_ElectricityMachineF004 = value;
                RaisePropertyChanged(() => ElectricityMachineF004);
            }
        }

        /// <summary>
        /// 电机电流
        /// </summary>
        public double ElectricityMachineF005
        {
            get { return m_ElectricityMachineF005; }
            set
            {
                if (value.Equals(m_ElectricityMachineF005))
                {
                    return;
                }
                m_ElectricityMachineF005 = value;
                RaisePropertyChanged(() => ElectricityMachineF005);
            }
        }

        /// <summary>
        /// 牵引能耗
        /// </summary>
        public double TractionEnergyF002
        {
            get { return m_TractionEnergyF002; }
            set
            {
                if (value.Equals(m_TractionEnergyF002))
                {
                    return;
                }
                m_TractionEnergyF002 = value;
                RaisePropertyChanged(() => TractionEnergyF002);
            }
        }

        /// <summary>
        /// 牵引能耗
        /// </summary>
        public double TractionEnergyF003
        {
            get { return m_TractionEnergyF003; }
            set
            {
                if (value.Equals(m_TractionEnergyF003))
                {
                    return;
                }
                m_TractionEnergyF003 = value;
                RaisePropertyChanged(() => TractionEnergyF003);
            }
        }

        /// <summary>
        /// 牵引能耗
        /// </summary>
        public double TractionEnergyF004
        {
            get { return m_TractionEnergyF004; }
            set
            {
                if (value.Equals(m_TractionEnergyF004))
                {
                    return;
                }
                m_TractionEnergyF004 = value;
                RaisePropertyChanged(() => TractionEnergyF004);
            }
        }

        /// <summary>
        /// 牵引能耗
        /// </summary>
        public double TractionEnergyF005
        {
            get { return m_TractionEnergyF005; }
            set
            {
                if (value.Equals(m_TractionEnergyF005))
                {
                    return;
                }
                m_TractionEnergyF005 = value;
                RaisePropertyChanged(() => TractionEnergyF005);
            }
        }

        /// <summary>
        /// 再生制动
        /// </summary>
        public double RebrithBrakeF002
        {
            get { return m_RebrithBrakeF002; }
            set
            {
                if (value.Equals(m_RebrithBrakeF002))
                {
                    return;
                }
                m_RebrithBrakeF002 = value;
                RaisePropertyChanged(() => RebrithBrakeF002);
            }
        }

        /// <summary>
        /// 再生制动
        /// </summary>
        public double RebrithBrakeF003
        {
            get { return m_RebrithBrakeF003; }
            set
            {
                if (value.Equals(m_RebrithBrakeF003))
                {
                    return;
                }
                m_RebrithBrakeF003 = value;
                RaisePropertyChanged(() => RebrithBrakeF003);
            }
        }

        /// <summary>
        /// 再生制动
        /// </summary>
        public double RebrithBrakeF004
        {
            get { return m_RebrithBrakeF004; }
            set
            {
                if (value.Equals(m_RebrithBrakeF004))
                {
                    return;
                }
                m_RebrithBrakeF004 = value;
                RaisePropertyChanged(() => RebrithBrakeF004);
            }
        }

        /// <summary>
        /// 再生制动
        /// </summary>
        public double RebrithBrakeF005
        {
            get { return m_RebrithBrakeF005; }
            set
            {
                if (value.Equals(m_RebrithBrakeF005))
                {
                    return;
                }
                m_RebrithBrakeF005 = value;
                RaisePropertyChanged(() => RebrithBrakeF005);
            }
        }

        /// <summary>
        /// 电网电压
        /// </summary>
        public double ElectricityNetF002
        {
            get { return m_ElectricityNetF002; }
            set
            {
                if (value.Equals(m_ElectricityNetF002))
                {
                    return;
                }
                m_ElectricityNetF002 = value;
                RaisePropertyChanged(() => ElectricityNetF002);
            }
        }

        /// <summary>
        /// 电网电压
        /// </summary>
        public double ElectricityNetF003
        {
            get { return m_ElectricityNetF003; }
            set
            {
                if (value.Equals(m_ElectricityNetF003))
                {
                    return;
                }
                m_ElectricityNetF003 = value;
                RaisePropertyChanged(() => ElectricityNetF003);
            }
        }

        /// <summary>
        /// 电网电压
        /// </summary>
        public double ElectricityNetF004
        {
            get { return m_ElectricityNetF004; }
            set
            {
                if (value.Equals(m_ElectricityNetF004))
                {
                    return;
                }
                m_ElectricityNetF004 = value;
                RaisePropertyChanged(() => ElectricityNetF004);
            }
        }

        /// <summary>
        /// 电网电压
        /// </summary>
        public double ElectricityNetF005
        {
            get { return m_ElectricityNetF005; }
            set
            {
                if (value.Equals(m_ElectricityNetF005))
                {
                    return;
                }
                m_ElectricityNetF005 = value;
                RaisePropertyChanged(() => ElectricityNetF005);
            }
        }

        /// <summary>
        /// 滤波器电容电压
        /// </summary>
        public double FliterF002
        {
            get { return m_FliterF002; }
            set
            {
                if (value.Equals(m_FliterF002))
                {
                    return;
                }
                m_FliterF002 = value;
                RaisePropertyChanged(() => FliterF002);
            }
        }

        /// <summary>
        /// 滤波器电容电压
        /// </summary>
        public double FliterF003
        {
            get { return m_FliterF003; }
            set
            {
                if (value.Equals(m_FliterF003))
                {
                    return;
                }
                m_FliterF003 = value;
                RaisePropertyChanged(() => FliterF003);
            }
        }

        /// <summary>
        /// 滤波器电容电压
        /// </summary>
        public double FliterF004
        {
            get { return m_FliterF004; }
            set
            {
                if (value.Equals(m_FliterF004))
                {
                    return;
                }
                m_FliterF004 = value;
                RaisePropertyChanged(() => FliterF004);
            }
        }

        /// <summary>
        /// 滤波器电容电压
        /// </summary>
        public double FliterF005
        {
            get { return m_FliterF005; }
            set
            {
                if (value.Equals(m_FliterF005))
                {
                    return;
                }
                m_FliterF005 = value;
                RaisePropertyChanged(() => FliterF005);
            }
        }

        /// <summary>
        /// 电阻制动
        /// </summary>
        public double ResistanceBrakeF002
        {
            get { return m_ResistanceBrakeF002; }
            set
            {
                if (value.Equals(m_ResistanceBrakeF002))
                {
                    return;
                }
                m_ResistanceBrakeF002 = value;
                RaisePropertyChanged(() => ResistanceBrakeF002);
            }
        }

        /// <summary>
        /// 电阻制动
        /// </summary>
        public double ResistanceBrakeF003
        {
            get { return m_ResistanceBrakeF003; }
            set
            {
                if (value.Equals(m_ResistanceBrakeF003))
                {
                    return;
                }
                m_ResistanceBrakeF003 = value;
                RaisePropertyChanged(() => ResistanceBrakeF003);
            }
        }

        /// <summary>
        /// 电阻制动
        /// </summary>
        public double ResistanceBrakeF004
        {
            get { return m_ResistanceBrakeF004; }
            set
            {
                if (value.Equals(m_ResistanceBrakeF004))
                {
                    return;
                }
                m_ResistanceBrakeF004 = value;
                RaisePropertyChanged(() => ResistanceBrakeF004);
            }
        }

        /// <summary>
        /// 电阻制动
        /// </summary>
        public double ResistanceBrakeF005
        {
            get { return m_ResistanceBrakeF005; }
            set
            {
                if (value.Equals(m_ResistanceBrakeF005))
                {
                    return;
                }
                m_ResistanceBrakeF005 = value;
                RaisePropertyChanged(() => ResistanceBrakeF005);
            }
        }

        /// <summary>
        /// 线路断路器
        /// </summary>
        public bool LineBrakerF0021
        {
            get { return m_LineBrakerF0021; }
            set
            {
                if (value == m_LineBrakerF0021)
                {
                    return;
                }
                m_LineBrakerF0021 = value;
                RaisePropertyChanged(() => LineBrakerF0021);
            }
        }

        /// <summary>
        /// 线路断路器
        /// </summary>
        public bool LineBrakerF0031
        {
            get { return m_LineBrakerF0031; }
            set
            {
                if (value == m_LineBrakerF0031)
                {
                    return;
                }
                m_LineBrakerF0031 = value;
                RaisePropertyChanged(() => LineBrakerF0031);
            }
        }

        /// <summary>
        /// 线路断路器
        /// </summary>
        public bool LineBrakerF0041
        {
            get { return m_LineBrakerF0041; }
            set
            {
                if (value == m_LineBrakerF0041)
                {
                    return;
                }
                m_LineBrakerF0041 = value;
                RaisePropertyChanged(() => LineBrakerF0041);
            }
        }

        /// <summary>
        /// 线路断路器
        /// </summary>
        public bool LineBrakerF0051
        {
            get { return m_LineBrakerF0051; }
            set
            {
                if (value == m_LineBrakerF0051)
                {
                    return;
                }
                m_LineBrakerF0051 = value;
                RaisePropertyChanged(() => LineBrakerF0051);
            }
        }

        /// <summary>
        /// 线路断路器
        /// </summary>
        public bool LineBrakerF0022
        {
            get { return m_LineBrakerF0022; }
            set
            {
                if (value == m_LineBrakerF0022)
                {
                    return;
                }
                m_LineBrakerF0022 = value;
                RaisePropertyChanged(() => LineBrakerF0022);
            }
        }

        /// <summary>
        /// 线路断路器
        /// </summary>
        public bool LineBrakerF0032
        {
            get { return m_LineBrakerF0032; }
            set
            {
                if (value == m_LineBrakerF0032)
                {
                    return;
                }
                m_LineBrakerF0032 = value;
                RaisePropertyChanged(() => LineBrakerF0032);
            }
        }

        /// <summary>
        /// 线路断路器
        /// </summary>
        public bool LineBrakerF0042
        {
            get { return m_LineBrakerF0042; }
            set
            {
                if (value == m_LineBrakerF0042)
                {
                    return;
                }
                m_LineBrakerF0042 = value;
                RaisePropertyChanged(() => LineBrakerF0042);
            }
        }

        /// <summary>
        /// 线路断路器
        /// </summary>
        public bool LineBrakerF0052
        {
            get { return m_LineBrakerF0052; }
            set
            {
                if (value == m_LineBrakerF0052)
                {
                    return;
                }
                m_LineBrakerF0052 = value;
                RaisePropertyChanged(() => LineBrakerF0052);
            }
        }
    }
}