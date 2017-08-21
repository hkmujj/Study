using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 牵引封锁
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class TractionLockModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            Model.TractionLocakModel.AllTractionLockUnits.ForEach(f => f.Changed(args));
        }
    }
}