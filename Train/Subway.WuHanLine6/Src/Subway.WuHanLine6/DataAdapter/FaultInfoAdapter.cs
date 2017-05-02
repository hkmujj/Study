using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.WuHanLine6.Interfaces;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 故障
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class FaultInfoAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var manager = Model.FaultInfoModel.Controller.FaultInfoManager;
            if (args.Keys.Any(a => manager.AllDate.ContainsKey(a)))
            {
                foreach (var source in args.Where(w => manager.AllDate.ContainsKey(w.Key)))
                {
                    if (source.Value)
                    {
                        manager.Add(source.Key);
                    }
                    else
                    {
                        manager.Remove(source.Key);
                    }
                }
            }
        }
    }
}