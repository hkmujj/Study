using System.Collections.Generic;
using System.Diagnostics;

namespace CRH2MMI.PowerClassify.PowerStates
{
    [DebuggerDisplay("State={StateData.State} ChirldrenCount={Chirldren.Count}")]
    class PowerStateTree 
    {
        /// <summary>
        /// 子节点 
        /// </summary>
        public List<PowerStateTree> Chirldren { set; get; }

        /// <summary>
        /// 节点数据
        /// </summary>
        public PowerStateData StateData { set; get; }

        public PowerStateTree()
        {
            Chirldren = new List<PowerStateTree>();
        }

        public PowerStateTree(PowerStateTree other)
        {
            StateData = other.StateData;
            Chirldren = new List<PowerStateTree>(other.Chirldren);
        }

        public void RefreshState()
        {
            RefreshTreeState(this);
        }

        public void Reset()
        {
            //State = PowerFrom.Null;
            StateData.Reset();
            Chirldren.ForEach(e => e.Reset());
        }

        private void RefreshTreeState(PowerStateTree tree)
        {
            foreach (var chirld in tree.Chirldren)
            {
                if (chirld.StateData.CanRefreshState())
                {
                    chirld.StateData.RefreshState(tree.StateData);
                    RefreshTreeState(chirld);
                }
            }
        }
    }
}
