namespace HXD1.DeepDomestic
{
    internal class FaultFacode
    {
        // ReSharper disable once InconsistentNaming
        private static readonly FaultFacode m_Instance = new FaultFacode();

        public static FaultFacode Instance
        {
            get { return m_Instance; }
        }

        public void AddFaultA(Fault fault)
        {
            CurrentFaultA.SortedFaultList.Add(fault);
            HisteryFault.CurrentFault.Add(fault);
            AllFalutA.SortedFaultList.Add(fault);
        }

        public void AddFaultB(Fault fault)
        {
            CurrentFaultB.SortedFaultList.Add(fault);
            HisteryFault.CurrentFault.Add(fault);
            AllFalutB.SortedFaultList.Add(fault);
        }

        /// <summary>
        /// 清除所有的故障
        /// </summary>
        public void Clear()
        {
            HisteryFault.CurrentFault.Clear();
            ClearFaultA();
            ClearFaultB();
        }

        private void ClearFaultA()
        {
            CurrentFaultA.SortedFaultList.Clear();
            AllFalutA.SortedFaultList.Clear();
        }

        public void ClearFaultB()
        {
            CurrentFaultB.SortedFaultList.Clear();
            AllFalutB.SortedFaultList.Clear();
        }
    }
}
