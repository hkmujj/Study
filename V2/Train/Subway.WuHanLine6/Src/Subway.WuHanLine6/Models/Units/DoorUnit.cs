using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using Subway.WuHanLine6.Configs;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    ///
    /// </summary>
    [ExcelLocation("界面接口表.xls", "DoorUnit")]

    public class DoorUnit : UnitBase
    {
        private DoorState m_State;

        /// <summary>
        /// 门状态
        /// </summary>0
        public DoorState State
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

        private List<DoorPriority> m_AllDoorState;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DoorUnit()
        {
            m_AllDoorState = new List<DoorPriority>();
        }

        /// <summary>
        /// 设置门状态
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        public void SetState(DoorState state, bool value)
        {
            if (value)
            {
                var tmp = GlobalParams.Instance.DoorPriorityConfig.DoorPriorities.FirstOrDefault(f => f.State == state);
                if (tmp != null)
                {
                    m_AllDoorState.Add(tmp);
                }

            }
            else
            {
                m_AllDoorState.Remove(m_AllDoorState.FirstOrDefault(f => f.State == state));
            }
            var firstOrDefault = m_AllDoorState.OrderBy(o => o.Priority).FirstOrDefault();
            if (firstOrDefault != null)
            {
                var tmp1 = firstOrDefault.State;
                //if (tmp1 != null)
                {
                    State = (DoorState)tmp1;
                }
            }
        }
        /// <summary>
        /// 门完全关闭
        /// </summary>
        [ExcelField("门完全关闭")]
        public string Closed { get; set; }

        /// <summary>
        /// 门未完全关闭
        /// </summary>
        [ExcelField("门未完全关闭")]
        public string UnClosed { get; set; }

        /// <summary>
        /// 防挤压过程中
        /// </summary>
        [ExcelField("防挤压过程中")]
        public string Checked { get; set; }

        /// <summary>
        /// 门隔离
        /// </summary>
        [ExcelField("门隔离")]
        public string Isolated { get; set; }

        /// <summary>
        /// Emergency
        /// </summary>
        [ExcelField("门紧急解锁")]
        public string Emergency { get; set; }

        /// <summary>
        /// 门故障
        /// </summary>
        [ExcelField("门故障")]
        public string Fault { get; set; }

        /// <summary>
        /// 门无效
        /// </summary>
        [ExcelField("门无效")]
        public string Invalid { get; set; }

        /// <summary>
        /// 门通信异常
        /// </summary>
        [ExcelField("门通信异常")]
        public string Communication { get; set; }

        /// <summary>
        /// 门
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
            //if (m_DoorStatePrio.ClosedPrio > m_DoorStatePrio.UnClosePrio && )
            //args[1029] = true;
            args.UpdateIfContain(Closed, b => SetState(DoorState.Closed, b));
            args.UpdateIfContain(UnClosed, b => SetState(DoorState.UnClose, b));
            args.UpdateIfContain(Checked, b => SetState(DoorState.Checked, b));
            args.UpdateIfContain(Isolated, b => SetState(DoorState.Isolated, b));
            args.UpdateIfContain(Emergency, b => SetState(DoorState.Emergency, b));
            args.UpdateIfContain(Fault, b => SetState(DoorState.Fault, b));
            args.UpdateIfContain(Invalid, b => SetState(DoorState.Invalid, b));
            args.UpdateIfContain(Communication, b => SetState(DoorState.Communication, b));
        }
    }
}