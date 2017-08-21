using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CRH2MMI.PowerClassify.PowerStates
{
    [DebuggerDisplay("DataType={DataType} IsPowerOn={IsPowerOn}  State={State}")]
    class PowerStateData 
    {
#if DEBUG

        public enum PowerStateDataType
        {
            MTR,
            ACK1,
            ACKBKK,
            ACK,
            BKK,
            APU,
        }

        public PowerStateDataType DataType { set; get; }

#endif

        public PowerStateData(List<IPowerUnit> contents)
        {
            Contents = contents;
        }

        public PowerStateData(PowerStateData other)
        {
            Contents = new List<IPowerUnit>(other.Contents);
            State = other.State;
            //IsPowerOn = other.IsPowerOn;
        }

        // ReSharper disable once InconsistentNaming
        public Action<PowerStateData> RefreshAction;

        public bool IsPowerOn { get { return Contents.Any(a => a.IsPowerOn); } }

        /// <summary>
        /// 状态
        /// </summary>
        public PowerFrom State { set; get; }

        public bool CanRefreshState()
        {
            return State == PowerFrom.Null && IsPowerOn;
        }

        /// <summary>
        /// 调用 RefreshAction
        /// </summary>
        public void Refresh()
        {
            if (RefreshAction != null)
            {
                RefreshAction(this);
            }
        }

        /// <summary>
        /// 刷新自己状态 
        /// </summary>
        public void RefreshState(PowerStateData data)
        {
            this.State = data.State;
        }


        public virtual void Reset()
        {
            State = PowerFrom.Null;
        }

        /// <summary>
        /// 里面的小单元
        /// </summary>
        public List<IPowerUnit> Contents { private set; get; }

    }
}
