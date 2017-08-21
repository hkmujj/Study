using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.View.Train;
using CRH2MMI.PowerClassify;
using CRH2MMI.PowerClassify.PowerStates;
using CommonUtil.Controls;

namespace CRH2MMI.Common.Util
{
    /// <summary>
    /// 基础单元
    /// </summary>
    abstract class UnitBase : CommonInnerControlBase, IUnit, IPowerUnit
    {
        protected UnitBase()
        {
            TextVisible = true;
            InputPowerUnits = new List<IPowerUnit>();
        }

        /// <summary>
        /// 车厢号
        /// </summary>
        public virtual int CarNo { get; set; }

        public int UnitNo { get; set; }

        /// <summary>
        /// 实际的轮廓
        /// </summary>
        public virtual Rectangle ActureOutLine
        {
            protected set { OutLineRectangle = value; }
            get { return OutLineRectangle; }
        }

        /// <summary>
        /// 文本是否可见
        /// </summary>
        public bool TextVisible { set; get; }

        public TrainUnit Unit { get; set; }

        public virtual bool IsPowerOn { get; set; }

        public Color Color { get; set; }

        public PowerFrom PowerFrom { get; set; }

        public List<IPowerUnit> InputPowerUnits { set; get; }

        public abstract void RefreshByState(PowerFrom powerFrom);

        public virtual void Reset()
        {
            IsPowerOn = false;
            RefreshByState(PowerFrom.Null);
        }

        public virtual void RefreshState()
        {
            if (!this.IsPowerOn)
            {
                RefreshByState(PowerFrom.Null);
                return;
            }

            var inputs = InputPowerUnits.Where(s => s.IsPowerOn).ToList();
            inputs.Sort((a, b) => a.PowerFrom.CompareTo(b.PowerFrom));
            foreach (var inputPowerUnit in inputs.Where(inputPowerUnit => inputPowerUnit.IsPowerOn))
            {
                IsPowerOn = true;
                RefreshByState(inputPowerUnit.PowerFrom);
                return;
            }
            RefreshByState(PowerFrom.Null);
        }

    }
}
