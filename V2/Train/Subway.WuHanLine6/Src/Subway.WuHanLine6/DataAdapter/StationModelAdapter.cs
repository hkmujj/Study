using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class StationModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变化
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            args.UpdateIfContain(InFloatKeys.InF当前设定起始站, f => Model.StationModel.CurrentStartStation = Model.TitleModel.StationManager.GetStatioName(f.ToInt()));
            args.UpdateIfContain(InFloatKeys.InF当前设定终点站, f => Model.StationModel.CurrentEndStation = Model.TitleModel.StationManager.GetStatioName(f.ToInt()));
        }
    }
}