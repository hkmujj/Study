using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 网络拓扑适配
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class NetWorkModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            Model.NetWorkModel.AllSatte.ForEach(f => f.Changed(args));
        }
    }
}