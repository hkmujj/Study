using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Models.Units;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class DCModel : ModelBase
    {
        private ObservableCollection<DCDevice1Unit> m_DCDevice1Unit;
        private ObservableCollection<DCDevice2Unit> m_DCDevice2Unit;
        private ObservableCollection<DCDevice3Unit> m_DCDevice3Unit;
        private float m_DCVoltage51;
        private float m_DCVoltage52;
        private float m_DCVoltage41;
        private float m_DCVoltage42;
        private float m_DCVoltage;
        private float m_ElectronFlow51;
        private float m_ElectronFlow52;
        private float m_ElectronFlow41;
        private float m_ElectronFlow42;
        

        [ImportingConstructor]
        public DCModel()
        {
            DCDevice1Unit = new ObservableCollection<DCDevice1Unit>(GlobalParam.Instance.DCDevice1Units.OrderBy(o => o.Num));
            DCDevice2Unit = new ObservableCollection<DCDevice2Unit>(GlobalParam.Instance.DCDevice2Units.OrderBy(o => o.Num));
            DCDevice3Unit = new ObservableCollection<DCDevice3Unit>(GlobalParam.Instance.DCDevice3Units.OrderBy(o => o.Num));
        }

        /// <summary>
        /// 所有设备1状态
        /// </summary>
        public ObservableCollection<DCDevice1Unit> DCDevice1Unit
        {
            get { return m_DCDevice1Unit; }
            set
            {
                if (Equals(value, m_DCDevice1Unit))
                {
                    return;
                }
                m_DCDevice1Unit = value;
                RaisePropertyChanged(() => DCDevice1Unit);
                RaisePropertyChanged(() => DCDevice1Unit51);
                RaisePropertyChanged(() => DCDevice1Unit52);
                RaisePropertyChanged(() => DCDevice1Unit41);
                RaisePropertyChanged(() => DCDevice1Unit42);
            }
        }

        /// <summary>
        /// 5号车位置1设备1状态
        /// </summary>
        public DCDevice1Unit DCDevice1Unit51 { get { return DCDevice1Unit.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5号车位置2设备1状态
        /// </summary>
        public DCDevice1Unit DCDevice1Unit52 { get { return DCDevice1Unit.Where(w => w.Car == 5 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4号车位置1设备1状态
        /// </summary>
        public DCDevice1Unit DCDevice1Unit41 { get { return DCDevice1Unit.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4号车位置2设备1状态
        /// </summary>
        public DCDevice1Unit DCDevice1Unit42 { get { return DCDevice1Unit.Where(w => w.Car == 4 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 所有设备2状态
        /// </summary>
        public ObservableCollection<DCDevice2Unit> DCDevice2Unit
        {
            get { return m_DCDevice2Unit; }
            set
            {
                if (Equals(value, m_DCDevice2Unit))
                {
                    return;
                }
                m_DCDevice2Unit = value;
                RaisePropertyChanged(() => DCDevice2Unit);
                RaisePropertyChanged(() => DCDevice2Unit51);
                RaisePropertyChanged(() => DCDevice2Unit52);
                RaisePropertyChanged(() => DCDevice2Unit41);
                RaisePropertyChanged(() => DCDevice2Unit42);
            }
        }

        /// <summary>
        /// 5号车位置1设备1状态
        /// </summary>
        public DCDevice2Unit DCDevice2Unit51 { get { return DCDevice2Unit.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5号车位置2设备1状态
        /// </summary>
        public DCDevice2Unit DCDevice2Unit52 { get { return DCDevice2Unit.Where(w => w.Car == 5 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4号车位置1设备1状态
        /// </summary>
        public DCDevice2Unit DCDevice2Unit41 { get { return DCDevice2Unit.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4号车位置2设备1状态
        /// </summary>
        public DCDevice2Unit DCDevice2Unit42 { get { return DCDevice2Unit.Where(w => w.Car == 4 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }


        /// <summary>
        /// 所有设备3状态
        /// </summary>
        public ObservableCollection<DCDevice3Unit> DCDevice3Unit
        {
            get { return m_DCDevice3Unit; }
            set
            {
                if (Equals(value, m_DCDevice3Unit))
                {
                    return;
                }
                m_DCDevice3Unit = value;
                RaisePropertyChanged(() => DCDevice3Unit);
                RaisePropertyChanged(() => DCDevice3Unit51);
                RaisePropertyChanged(() => DCDevice3Unit52);
                RaisePropertyChanged(() => DCDevice3Unit41);
                RaisePropertyChanged(() => DCDevice3Unit42);
            }
        }

        /// <summary>
        /// 5号车位置1设备1状态
        /// </summary>
        public DCDevice3Unit DCDevice3Unit51 { get { return DCDevice3Unit.Where(w => w.Car == 5 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5号车位置2设备1状态
        /// </summary>
        public DCDevice3Unit DCDevice3Unit52 { get { return DCDevice3Unit.Where(w => w.Car == 5 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4号车位置1设备1状态
        /// </summary>
        public DCDevice3Unit DCDevice3Unit41 { get { return DCDevice3Unit.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4号车位置2设备1状态
        /// </summary>
        public DCDevice3Unit DCDevice3Unit42 { get { return DCDevice3Unit.Where(w => w.Car == 4 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 5号车位置1电压
        /// </summary>
        public float DCVoltage51
        {
            get { return m_DCVoltage51; }
            set
            {
                if (value == m_DCVoltage51)
                {
                    return;
                }
                m_DCVoltage51 = value;
                RaisePropertyChanged(() => DCVoltage51);
            }
        }

        /// <summary>
        /// 5号车位置2电压
        /// </summary>
        public float DCVoltage52
        {
            get { return m_DCVoltage52; }
            set
            {
                if (value == m_DCVoltage52)
                {
                    return;
                }
                m_DCVoltage52 = value;
                RaisePropertyChanged(() => DCVoltage52);
            }
        }

        /// <summary>
        /// 4号车位置1电压
        /// </summary>
        public float DCVoltage41
        {
            get { return m_DCVoltage41; }
            set
            {
                if (value == m_DCVoltage41)
                {
                    return;
                }
                m_DCVoltage41 = value;
                RaisePropertyChanged(() => DCVoltage41);
            }
        }

        /// <summary>
        /// 4号车位置2电压
        /// </summary>
        public float DCVoltage42
        {
            get { return m_DCVoltage42; }
            set
            {
                if (value == m_DCVoltage42)
                {
                    return;
                }
                m_DCVoltage42 = value;
                RaisePropertyChanged(() => DCVoltage42);
            }
        }

        /// <summary>
        /// 平均电压
        /// </summary>
        public float DCVoltage
        {
            get { return m_DCVoltage; }
            set
            {
                if (value == m_DCVoltage)
                {
                    return;
                }
                m_DCVoltage = value;
                RaisePropertyChanged(() => DCVoltage);
            }
        }

        /// <summary>
        /// 5号车位置1电流
        /// </summary>
        public float ElectronFlow51
        {
            get { return m_ElectronFlow51; }
            set
            {
                if (value == m_ElectronFlow51)
                {
                    return;
                }
                m_ElectronFlow51 = value;
                RaisePropertyChanged(() => ElectronFlow51);
            }
        }

        /// <summary>
        /// 5号车位置2电流
        /// </summary>
        public float ElectronFlow52
        {
            get { return m_ElectronFlow52; }
            set
            {
                if (value == m_ElectronFlow52)
                {
                    return;
                }
                m_ElectronFlow52 = value;
                RaisePropertyChanged(() => ElectronFlow52);
            }
        }

        /// <summary>
        /// 4号车位置1电流
        /// </summary>
        public float ElectronFlow41
        {
            get { return m_ElectronFlow41; }
            set
            {
                if (value == m_ElectronFlow41)
                {
                    return;
                }
                m_ElectronFlow41 = value;
                RaisePropertyChanged(() => ElectronFlow41);
            }
        }

        /// <summary>
        /// 4号车位置2电流
        /// </summary>
        public float ElectronFlow42
        {
            get { return m_ElectronFlow42; }
            set
            {
                if (value == m_ElectronFlow42)
                {
                    return;
                }
                m_ElectronFlow42 = value;
                RaisePropertyChanged(() => ElectronFlow42);
            }
        }
    }
}