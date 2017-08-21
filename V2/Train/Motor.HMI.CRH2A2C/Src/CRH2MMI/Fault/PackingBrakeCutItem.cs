using System.Diagnostics;

namespace CRH2MMI.Fault
{
    class PackingBrakeCutItem
    {
        [DebuggerStepThrough]
        public PackingBrakeCutItem(string indexName, int logicIndex, int carIndex)
        {
            IndexName = indexName;
            LogicIndex = logicIndex;
            CarIndex = carIndex;
            State = PackingBrakeCutState.Normal;
        }

        public string IndexName { private set; get; }

        public int LogicIndex { private set; get; }

        public int CarIndex { private set; get; }

        public PackingBrakeCutState State { set; get; }
    }
}