using System.Collections.Generic;
using Excel.Interface;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    /// 空调系统
    /// </summary>
    [ExcelLocation("界面接口表.xls", "AirConditionUnit")]
    public class AirConditionUnit : UnitBase
    {
        private AirConditionState m_State;

        /// <summary>
        /// 状态
        /// </summary>
        public AirConditionState State
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
        /// 空调故障
        /// </summary>
        [ExcelField("空调故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 空调警告
        /// </summary>
        [ExcelField("空调警告")]
        public string Warn { get; set; }

        /// <summary>
        /// 紧急通风
        /// </summary>
        [ExcelField("紧急通风")]
        public string EmergencyFan { get; set; }

        /// <summary>
        /// 通风
        /// </summary>
        [ExcelField("通风")]
        public string Fan { get; set; }

        /// <summary>
        /// 限制制冷
        /// </summary>
        [ExcelField("限制制冷")]
        public string Limit { get; set; }

        /// <summary>
        /// 空调运行
        /// </summary>
        [ExcelField("空调运行")]
        public string Run { get; set; }

        /// <summary>
        /// 空调断开
        /// </summary>
        [ExcelField("空调断开")]
        public string Off { get; set; }

        /// <summary>
        /// 车
        /// </summary>
        [ExcelField("车")]
        public int Car { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [ExcelField("位置")]
        public int Location { get; set; }

        /// <summary>
        /// Bool值改变
        /// </summary>
        /// <param name="args"></param>
        public override void Changed(IDictionary<int, bool> args)
        {
            args.UpdateIfContainWhereTrue(Fault, (b => State = AirConditionState.Fault));
            args.UpdateIfContainWhereTrue(Warn, (b => State = AirConditionState.Warn));
            args.UpdateIfContainWhereTrue(EmergencyFan, (b => State = AirConditionState.EmergencyFan));
            args.UpdateIfContainWhereTrue(Fan, (b => State = AirConditionState.Fan));
            args.UpdateIfContainWhereTrue(Limit, (b => State = AirConditionState.Limit));
            args.UpdateIfContainWhereTrue(Run, (b => State = AirConditionState.Run));
            args.UpdateIfContainWhereTrue(Off, b => State = AirConditionState.Off);
        }
    }
}