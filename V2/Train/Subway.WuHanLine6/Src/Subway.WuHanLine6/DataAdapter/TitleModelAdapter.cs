using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 标题视图数据转换
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class TitleModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变化
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, float> args)
        {
            var title = Model.TitleModel;
            args.UpdateIfContain(InFloatKeys.InF网压, f => title.NetVoltage = f);
            args.UpdateIfContain(InFloatKeys.InF网流, f => title.NetCurrent = f);
            args.UpdateIfContain(InFloatKeys.InF速度, f => title.Speed = f);
            args.UpdateIfContain(InFloatKeys.InF限速值, f => title.LimitSpeed = f);

            args.UpdateIfContain(InFloatKeys.InF当前站, f => title.CurrentStation = title.StationManager.GetStatioName(f.ToInt()));
            args.UpdateIfContain(InFloatKeys.InF下一站, f => title.NextStation = title.StationManager.GetStatioName(f.ToInt()));
            args.UpdateIfContain(InFloatKeys.InF终点站, f => title.EndStation = title.StationManager.GetStatioName(f.ToInt()));
        }
    }
}