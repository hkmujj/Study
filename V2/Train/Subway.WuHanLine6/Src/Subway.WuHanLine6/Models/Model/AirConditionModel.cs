using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.WuHanLine6.Controller;
using Subway.WuHanLine6.Models.States;
using Subway.WuHanLine6.Models.Units;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    ///     空调状态Model
    /// </summary>
    [Export]
    public class AirConditionModel : ModelBase
    {
        private ObservableCollection<ChanceUnit> m_AllOpenStatus;
        private double m_CarTemperatureF001;
        private double m_CarTemperatureF002;
        private double m_CarTemperatureF003;
        private double m_CarTemperatureF004;
        private double m_CarTemperatureF005;
        private double m_CarTemperatureF006;
        private AirControlModel m_ControlModelF001;
        private AirControlModel m_ControlModelF002;
        private AirControlModel m_ControlModelF003;
        private AirControlModel m_ControlModelF004;
        private AirControlModel m_ControlModelF005;
        private AirControlModel m_ControlModelF006;
        private AirRunModel m_RunModelF001;
        private AirRunModel m_RunModelF002;
        private AirRunModel m_RunModelF003;
        private AirRunModel m_RunModelF004;
        private AirRunModel m_RunModelF005;
        private AirRunModel m_RunModelF006;
        private double m_SettingValueF001;
        private double m_SettingValueF002;
        private double m_SettingValueF003;
        private double m_SettingValueF004;
        private double m_SettingValueF005;
        private double m_SettingValueF006;
        private bool m_F006Select;
        private bool m_F005Select;
        private bool m_F004Select;
        private bool m_F003Select;
        private bool m_F002Select;
        private bool m_F001Select;
        private bool m_AllTrainSelect;

        /// <summary>
        /// 随机给温度赋值
        /// </summary>
        private static readonly Random m_Random = new Random(1);

        /// <summary>
        ///     构造函数
        /// </summary>
        [ImportingConstructor]
        public AirConditionModel(AirConditionViewController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            AllOpenStatus = new ObservableCollection<ChanceUnit>(GlobalParams.Instance.ChanceUnits);
            SettingValueF001 = m_Random.Next(23, 25);
            SettingValueF002 = m_Random.Next(23, 25);
            SettingValueF003 = m_Random.Next(23, 25);
            SettingValueF004 = m_Random.Next(23, 25);
            SettingValueF005 = m_Random.Next(23, 25);
            SettingValueF006 = m_Random.Next(23, 25);
        }

        /// <summary>
        /// 全车选中
        /// </summary>
        public bool AllTrainSelect
        {
            get { return m_AllTrainSelect; }
            set
            {
                if (value == m_AllTrainSelect)
                {
                    return;
                }
                m_AllTrainSelect = value;
                RaisePropertyChanged(() => AllTrainSelect);
            }
        }

        /// <summary>
        /// 1车选中
        /// </summary>
        public bool F001Select
        {
            get { return m_F001Select; }
            set
            {
                if (value == m_F001Select)
                {
                    return;
                }
                m_F001Select = value;
                RaisePropertyChanged(() => F001Select);
            }
        }

        /// <summary>
        /// 1车选中
        /// </summary>
        public bool F002Select
        {
            get { return m_F002Select; }
            set
            {
                if (value == m_F002Select)
                {
                    return;
                }
                m_F002Select = value;
                RaisePropertyChanged(() => F002Select);
            }
        }

        /// <summary>
        /// 1车选中
        /// </summary>
        public bool F003Select
        {
            get { return m_F003Select; }
            set
            {
                if (value == m_F003Select)
                {
                    return;
                }
                m_F003Select = value;
                RaisePropertyChanged(() => F003Select);
            }
        }

        /// <summary>
        /// 1车选中
        /// </summary>
        public bool F004Select
        {
            get { return m_F004Select; }
            set
            {
                if (value == m_F004Select)
                {
                    return;
                }
                m_F004Select = value;
                RaisePropertyChanged(() => F004Select);
            }
        }

        /// <summary>
        /// 1车选中
        /// </summary>
        public bool F005Select
        {
            get { return m_F005Select; }
            set
            {
                if (value == m_F005Select)
                {
                    return;
                }
                m_F005Select = value;
                RaisePropertyChanged(() => F005Select);
            }
        }

        /// <summary>
        /// 1车选中
        /// </summary>
        public bool F006Select
        {
            get { return m_F006Select; }
            set
            {
                if (value == m_F006Select)
                {
                    return;
                }
                m_F006Select = value;
                RaisePropertyChanged(() => F006Select);
            }
        }

        /// <summary>
        /// 控制
        /// </summary>
        public AirConditionViewController Controller { get; private set; }

        /// <summary>
        ///     控制模式
        /// </summary>
        public AirControlModel ControlModelF001
        {
            get { return m_ControlModelF001; }
            set
            {
                if (value == m_ControlModelF001)
                {
                    return;
                }
                m_ControlModelF001 = value;
                RaisePropertyChanged(() => ControlModelF001);
            }
        }

        /// <summary>
        ///     控制模式
        /// </summary>
        public AirControlModel ControlModelF002
        {
            get { return m_ControlModelF002; }
            set
            {
                if (value == m_ControlModelF002)
                {
                    return;
                }
                m_ControlModelF002 = value;
                RaisePropertyChanged(() => ControlModelF002);
            }
        }

        /// <summary>
        ///     控制模式
        /// </summary>
        public AirControlModel ControlModelF003
        {
            get { return m_ControlModelF003; }
            set
            {
                if (value == m_ControlModelF003)
                {
                    return;
                }
                m_ControlModelF003 = value;
                RaisePropertyChanged(() => ControlModelF003);
            }
        }

        /// <summary>
        ///     控制模式
        /// </summary>
        public AirControlModel ControlModelF004
        {
            get { return m_ControlModelF004; }
            set
            {
                if (value == m_ControlModelF004)
                {
                    return;
                }
                m_ControlModelF004 = value;
                RaisePropertyChanged(() => ControlModelF004);
            }
        }

        /// <summary>
        ///     控制模式
        /// </summary>
        public AirControlModel ControlModelF005
        {
            get { return m_ControlModelF005; }
            set
            {
                if (value == m_ControlModelF005)
                {
                    return;
                }
                m_ControlModelF005 = value;
                RaisePropertyChanged(() => ControlModelF005);
            }
        }

        /// <summary>
        ///     控制模式
        /// </summary>
        public AirControlModel ControlModelF006
        {
            get { return m_ControlModelF006; }
            set
            {
                if (value == m_ControlModelF006)
                {
                    return;
                }
                m_ControlModelF006 = value;
                RaisePropertyChanged(() => ControlModelF006);
            }
        }

        /// <summary>
        ///     运行模式
        /// </summary>
        public AirRunModel RunModelF001
        {
            get { return m_RunModelF001; }
            set
            {
                if (value == m_RunModelF001)
                {
                    return;
                }
                m_RunModelF001 = value;
                RaisePropertyChanged(() => RunModelF001);
            }
        }

        /// <summary>
        ///     运行模式
        /// </summary>
        public AirRunModel RunModelF002
        {
            get { return m_RunModelF002; }
            set
            {
                if (value == m_RunModelF002)
                {
                    return;
                }
                m_RunModelF002 = value;
                RaisePropertyChanged(() => RunModelF002);
            }
        }

        /// <summary>
        ///     运行模式
        /// </summary>
        public AirRunModel RunModelF003
        {
            get { return m_RunModelF003; }
            set
            {
                if (value == m_RunModelF003)
                {
                    return;
                }
                m_RunModelF003 = value;
                RaisePropertyChanged(() => RunModelF003);
            }
        }

        /// <summary>
        ///     运行模式
        /// </summary>
        public AirRunModel RunModelF004
        {
            get { return m_RunModelF004; }
            set
            {
                if (value == m_RunModelF004)
                {
                    return;
                }
                m_RunModelF004 = value;
                RaisePropertyChanged(() => RunModelF004);
            }
        }

        /// <summary>
        ///     运行模式
        /// </summary>
        public AirRunModel RunModelF005
        {
            get { return m_RunModelF005; }
            set
            {
                if (value == m_RunModelF005)
                {
                    return;
                }
                m_RunModelF005 = value;
                RaisePropertyChanged(() => RunModelF005);
            }
        }

        /// <summary>
        ///     运行模式
        /// </summary>
        public AirRunModel RunModelF006
        {
            get { return m_RunModelF006; }
            set
            {
                if (value == m_RunModelF006)
                {
                    return;
                }
                m_RunModelF006 = value;
                RaisePropertyChanged(() => RunModelF006);
            }
        }

        /// <summary>
        ///     设置温度
        /// </summary>
        public double SettingValueF001
        {
            get { return m_SettingValueF001; }
            set
            {
                if (value.Equals(m_SettingValueF001))
                {
                    return;
                }
                m_SettingValueF001 = value;
                RaisePropertyChanged(() => SettingValueF001);
            }
        }

        /// <summary>
        ///     设置温度
        /// </summary>
        public double SettingValueF002
        {
            get { return m_SettingValueF002; }
            set
            {
                if (value.Equals(m_SettingValueF002))
                {
                    return;
                }
                m_SettingValueF002 = value;
                RaisePropertyChanged(() => SettingValueF002);
            }
        }

        /// <summary>
        ///     设置温度
        /// </summary>
        public double SettingValueF003
        {
            get { return m_SettingValueF003; }
            set
            {
                if (value.Equals(m_SettingValueF003))
                {
                    return;
                }
                m_SettingValueF003 = value;
                RaisePropertyChanged(() => SettingValueF003);
            }
        }

        /// <summary>
        ///     设置温度
        /// </summary>
        public double SettingValueF004
        {
            get { return m_SettingValueF004; }
            set
            {
                if (value.Equals(m_SettingValueF004))
                {
                    return;
                }
                m_SettingValueF004 = value;
                RaisePropertyChanged(() => SettingValueF004);
            }
        }

        /// <summary>
        ///     设置温度
        /// </summary>
        public double SettingValueF005
        {
            get { return m_SettingValueF005; }
            set
            {
                if (value.Equals(m_SettingValueF005))
                {
                    return;
                }
                m_SettingValueF005 = value;
                RaisePropertyChanged(() => SettingValueF005);
            }
        }

        /// <summary>
        ///     设置温度
        /// </summary>
        public double SettingValueF006
        {
            get { return m_SettingValueF006; }
            set
            {
                if (value.Equals(m_SettingValueF006))
                {
                    return;
                }
                m_SettingValueF006 = value;
                RaisePropertyChanged(() => SettingValueF006);
            }
        }

        /// <summary>
        ///     车内外温度
        /// </summary>
        public double CarTemperatureF001
        {
            get { return m_CarTemperatureF001; }
            set
            {
                if (value.Equals(m_CarTemperatureF001))
                {
                    return;
                }
                m_CarTemperatureF001 = value;
                RaisePropertyChanged(() => CarTemperatureF001);
            }
        }

        /// <summary>
        ///     车内外温度
        /// </summary>
        public double CarTemperatureF002
        {
            get { return m_CarTemperatureF002; }
            set
            {
                if (value.Equals(m_CarTemperatureF002))
                {
                    return;
                }
                m_CarTemperatureF002 = value;
                RaisePropertyChanged(() => CarTemperatureF002);
            }
        }

        /// <summary>
        ///     车内外温度
        /// </summary>
        public double CarTemperatureF003
        {
            get { return m_CarTemperatureF003; }
            set
            {
                if (value.Equals(m_CarTemperatureF003))
                {
                    return;
                }
                m_CarTemperatureF003 = value;
                RaisePropertyChanged(() => CarTemperatureF003);
            }
        }

        /// <summary>
        ///     车内外温度
        /// </summary>
        public double CarTemperatureF004
        {
            get { return m_CarTemperatureF004; }
            set
            {
                if (value.Equals(m_CarTemperatureF004))
                {
                    return;
                }
                m_CarTemperatureF004 = value;
                RaisePropertyChanged(() => CarTemperatureF004);
            }
        }

        /// <summary>
        ///     车内外温度
        /// </summary>
        public double CarTemperatureF005
        {
            get { return m_CarTemperatureF005; }
            set
            {
                if (value.Equals(m_CarTemperatureF005))
                {
                    return;
                }
                m_CarTemperatureF005 = value;
                RaisePropertyChanged(() => CarTemperatureF005);
            }
        }

        /// <summary>
        ///     车内外温度
        /// </summary>
        public double CarTemperatureF006
        {
            get { return m_CarTemperatureF006; }
            set
            {
                if (value.Equals(m_CarTemperatureF006))
                {
                    return;
                }
                m_CarTemperatureF006 = value;
                RaisePropertyChanged(() => CarTemperatureF006);
            }
        }

        /// <summary>
        ///     压缩机 冷凝机  通风机  课室加热 打开状态
        /// </summary>
        public ObservableCollection<ChanceUnit> AllOpenStatus
        {
            get { return m_AllOpenStatus; }
            set
            {
                if (Equals(value, m_AllOpenStatus))
                {
                    return;
                }
                m_AllOpenStatus = value;
                RaisePropertyChanged(() => AllOpenStatus);
                RaisePropertyChanged(() => CompressF001Status);
                RaisePropertyChanged(() => CompressF002Status);
                RaisePropertyChanged(() => CompressF003Status);
                RaisePropertyChanged(() => CompressF004Status);
                RaisePropertyChanged(() => CompressF005Status);
                RaisePropertyChanged(() => CompressF006Status);
                RaisePropertyChanged(() => CondenstateF001Status);
                RaisePropertyChanged(() => CondenstateF002Status);
                RaisePropertyChanged(() => CondenstateF003Status);
                RaisePropertyChanged(() => CondenstateF004Status);
                RaisePropertyChanged(() => CondenstateF005Status);
                RaisePropertyChanged(() => CondenstateF006Status);
                RaisePropertyChanged(() => VentlatestateF001Status);
                RaisePropertyChanged(() => VentlatestateF002Status);
                RaisePropertyChanged(() => VentlatestateF003Status);
                RaisePropertyChanged(() => VentlatestateF004Status);
                RaisePropertyChanged(() => VentlatestateF005Status);
                RaisePropertyChanged(() => VentlatestateF006Status);
                RaisePropertyChanged(() => HeatF001Status);
                RaisePropertyChanged(() => HeatF002Status);
                RaisePropertyChanged(() => HeatF003Status);
                RaisePropertyChanged(() => HeatF004Status);
                RaisePropertyChanged(() => HeatF005Status);
                RaisePropertyChanged(() => HeatF006Status);
            }
        }

        /// <summary>
        ///     压缩机
        /// </summary>
        public IEnumerable<ChanceUnit> CompressF001Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 1 && w.Type == ChanceType.Compress).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     压缩机
        /// </summary>
        public IEnumerable<ChanceUnit> CompressF002Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 2 && w.Type == ChanceType.Compress).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     压缩机
        /// </summary>
        public IEnumerable<ChanceUnit> CompressF003Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 3 && w.Type == ChanceType.Compress).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     压缩机
        /// </summary>
        public IEnumerable<ChanceUnit> CompressF004Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 4 && w.Type == ChanceType.Compress).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     压缩机
        /// </summary>
        public IEnumerable<ChanceUnit> CompressF005Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 5 && w.Type == ChanceType.Compress).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     压缩机
        /// </summary>
        public IEnumerable<ChanceUnit> CompressF006Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 6 && w.Type == ChanceType.Compress).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     冷凝机
        /// </summary>
        public IEnumerable<ChanceUnit> CondenstateF001Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 1 && w.Type == ChanceType.Condenstate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     冷凝机
        /// </summary>
        public IEnumerable<ChanceUnit> CondenstateF002Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 2 && w.Type == ChanceType.Condenstate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     冷凝机
        /// </summary>
        public IEnumerable<ChanceUnit> CondenstateF003Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 3 && w.Type == ChanceType.Condenstate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     冷凝机
        /// </summary>
        public IEnumerable<ChanceUnit> CondenstateF004Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 4 && w.Type == ChanceType.Condenstate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     冷凝机
        /// </summary>
        public IEnumerable<ChanceUnit> CondenstateF005Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 5 && w.Type == ChanceType.Condenstate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     冷凝机
        /// </summary>
        public IEnumerable<ChanceUnit> CondenstateF006Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 6 && w.Type == ChanceType.Condenstate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     通风机
        /// </summary>
        public IEnumerable<ChanceUnit> VentlatestateF001Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 1 && w.Type == ChanceType.Ventlate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     通风机
        /// </summary>
        public IEnumerable<ChanceUnit> VentlatestateF002Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 2 && w.Type == ChanceType.Ventlate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     通风机
        /// </summary>
        public IEnumerable<ChanceUnit> VentlatestateF003Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 3 && w.Type == ChanceType.Ventlate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     通风机
        /// </summary>
        public IEnumerable<ChanceUnit> VentlatestateF004Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 4 && w.Type == ChanceType.Ventlate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     通风机
        /// </summary>
        public IEnumerable<ChanceUnit> VentlatestateF005Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 5 && w.Type == ChanceType.Ventlate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     通风机
        /// </summary>
        public IEnumerable<ChanceUnit> VentlatestateF006Status
        {
            get
            {
                return AllOpenStatus.Where(w => w.Car == 6 && w.Type == ChanceType.Ventlate).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        ///     课室加热
        /// </summary>
        public IEnumerable<ChanceUnit> HeatF001Status
        {
            get { return AllOpenStatus.Where(w => w.Car == 1 && w.Type == ChanceType.Heat).OrderBy(o => o.Location); }
        }

        /// <summary>
        ///     课室加热
        /// </summary>
        public IEnumerable<ChanceUnit> HeatF002Status
        {
            get { return AllOpenStatus.Where(w => w.Car == 2 && w.Type == ChanceType.Heat).OrderBy(o => o.Location); }
        }

        /// <summary>
        ///     课室加热
        /// </summary>
        public IEnumerable<ChanceUnit> HeatF003Status
        {
            get { return AllOpenStatus.Where(w => w.Car == 3 && w.Type == ChanceType.Heat).OrderBy(o => o.Location); }
        }

        /// <summary>
        ///     课室加热
        /// </summary>
        public IEnumerable<ChanceUnit> HeatF004Status
        {
            get { return AllOpenStatus.Where(w => w.Car == 4 && w.Type == ChanceType.Heat).OrderBy(o => o.Location); }
        }

        /// <summary>
        ///     课室加热
        /// </summary>
        public IEnumerable<ChanceUnit> HeatF005Status
        {
            get { return AllOpenStatus.Where(w => w.Car == 5 && w.Type == ChanceType.Heat).OrderBy(o => o.Location); }
        }

        /// <summary>
        ///     课室加热
        /// </summary>
        public IEnumerable<ChanceUnit> HeatF006Status
        {
            get { return AllOpenStatus.Where(w => w.Car == 6 && w.Type == ChanceType.Heat).OrderBy(o => o.Location); }
        }
    }
}