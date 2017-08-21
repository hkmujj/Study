using System.Collections.Generic;
using Excel.Interface;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    /// 制动系统
    /// </summary>
    [ExcelLocation("界面接口表.xls", "BrakeUnit")]
    public class BrakeUnit : UnitBase
    {
        private BrakeState m_State;

        /// <summary>
        /// 当前制动状态
        /// </summary>
        public BrakeState State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                {
                    return;
                }
                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }

        /// <summary>
        /// 停放制动施加
        /// </summary>
        [ExcelField("停放制动施加")]
        public string Parking { get; set; }

        /// <summary>
        /// 制动切除
        /// </summary>
        [ExcelField("制动切除")]
        public string Cut { get; set; }

        /// <summary>
        /// 制动自检激活
        /// </summary>
        [ExcelField("制动自检激活")]
        public string AutoCheck { get; set; }

        /// <summary>
        /// 制动故障
        /// </summary>
        [ExcelField("制动故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 制动警告
        /// </summary>
        [ExcelField("制动警告")]
        public string Warn { get; set; }

        /// <summary>
        /// 常用制动施加
        /// </summary>
        [ExcelField("常用制动施加")]
        public string Infliction { get; set; }

        /// <summary>
        /// 常用制动缓解
        /// </summary>
        [ExcelField("常用制动缓解")]
        public string Remission { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [ExcelField("位置")]
        public int Location { get; set; }

        /// <summary>
        /// 车辆
        /// </summary>
        [ExcelField("车辆")]
        public int Car { get; set; }

        /// <summary>
        /// Bool值改变
        /// </summary>
        /// <param name="args"></param>
        public override void Changed(IDictionary<int, bool> args)
        {
            args.UpdateIfContainWhereTrue(Parking, (b => State = BrakeState.Parking));
            args.UpdateIfContainWhereTrue(Cut, (b => State = BrakeState.Cut));
            args.UpdateIfContainWhereTrue(AutoCheck, (b => State = BrakeState.AutoCheck));
            args.UpdateIfContainWhereTrue(Fault, b => State = BrakeState.Fault);
            args.UpdateIfContainWhereTrue(Warn, (b => State = BrakeState.Warn));
            args.UpdateIfContainWhereTrue(Infliction, b => State = BrakeState.Infliction);
            args.UpdateIfContainWhereTrue(Remission, (b => State = BrakeState.Remission));
        }
    }
}