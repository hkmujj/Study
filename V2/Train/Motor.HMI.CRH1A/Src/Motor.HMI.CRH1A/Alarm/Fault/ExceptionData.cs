using System;
using CommonUtil.Util;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    [Serializable]
    public class ExceptionData
    {
        public FaultSenderType SenderType { set; get; }
        
        /// <summary>
        /// 状态变化 
        /// </summary>
        [NonSerialized]
        public EventHandler StateChanged;

        private FaultState m_State;

        /// <summary>
        /// 故障的逻辑位
        /// </summary>
        public int ExLogNo { get; set; }
        /// <summary>
        /// 故障所属单元
        /// </summary>            
        public int ExUnit { get; set; }
        /// <summary>
        /// 故障车厢号
        /// </summary>
        public int ExCarId { get; set; }

        /// <summary>
        /// 故障车厢号所属单元
        /// </summary>
        public int ExCarUnit { get; set; }

        /// <summary>
        /// 故障代码(编号)
        /// </summary>
        public int ExId { get; set; }

        /// <summary>
        /// 故障信息
        /// </summary>
        public string ExText { get; set; }
        /// <summary>
        /// 故障类型(等级)
        /// </summary>
        public FaultType ExType { get; set; }

        public string ExTypeInfo
        {
            get
            {
                switch (ExType)
                {
                    case FaultType.OperError:
                        return "信息";
                    case FaultType.A:
                        return "A - 警报";
                    case FaultType.E:
                        return "信息";
                    case FaultType.B:
                        return "B - 警报";
                    default:
                        return "故障";
                }
            }
        }

        /// <summary>
        /// 故障提示/帮助内容的编号
        /// </summary>
        public int ExSuggestId { get; set; }
        /// <summary>
        /// 故障发生的日期
        /// </summary>
        public DateTime Startdate { get; set; }
        /// <summary>
        /// 故障结束时间
        /// </summary>
        public DateTime Enddate { get; set; }


        /// <summary>
        /// 故障所属类别号
        /// </summary>
        public int PartId { get; set; }


        /// <summary>
        /// 故障状态
        /// </summary>
        public FaultState State
        {
            set
            {
                m_State = value;
                HandleUtil.OnHandle(StateChanged, this, null);
            }
            get { return m_State; }
        }


        /// <summary>
        /// 是否被确认(ture表示被确认 false表示未确认)
        /// </summary>
        public bool IsConfirm
        {
            set
            {
                if (value)
                {
                    State |= FaultState.Sure;
                }
                else
                {
                    State &= ~FaultState.Sure;
                }
            }
            get { return (State & FaultState.Sure) == FaultState.Sure; }
        }

        /// <summary>
        /// 是否已解决
        /// </summary>
        public bool IsFixed
        {
            set
            {
                if (value)
                {
                    State |= FaultState.Fixed;
                }
                else
                {
                    State &= ~FaultState.Fixed;
                }
            }
            get { return (State & FaultState.Fixed) == FaultState.Fixed; }
        }

        public ExceptionData()
        {
            State = FaultState.New;
            IsConfirm = false;

        }

    }
}