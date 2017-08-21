using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Motor.HMI.CRH1A.Common.Train
{
    /// <summary>
    /// 基本列车单元
    /// </summary>
    public class BaseTrainUnit
    {
        private static List<List<int>> _allUnit;
        public static int CurrentUnit { get; set; }
        static BaseTrainUnit()
        {
            _allUnit = new List<List<int>>()
            {
                new List<int>() {0, 1, 2},
                new List<int>() {3, 4},
                new List<int>() {5, 6, 7},
            };
        }

        public static ReadOnlyCollection<int> GetUnitOfIndex(int idx)
        {
            Debug.Assert(idx >= 0 && idx <= 7);
            CurrentUnit = _allUnit.FindIndex(f => f.Contains(idx))+1;
            return _allUnit.Find(f => f.Contains(idx)).AsReadOnly();
        }

    }
}
