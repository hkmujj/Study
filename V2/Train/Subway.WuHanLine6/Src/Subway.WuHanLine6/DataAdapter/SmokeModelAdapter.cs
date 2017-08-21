using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models.States;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 烟火报警
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class SmokeModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var smoke = Model.SmokeModel;
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>()
            {
                {InBoolKeys.InBoTC1车烟火报警烟火探头故障,target=> {smoke.SmokeF001=SmokeState.Fault;} },
                {InBoolKeys.InBoTC1车烟火报警烟火探头正常表示未检测到烟火,target=> {smoke.SmokeF001=SmokeState.Nomal;} },
                {InBoolKeys.InBoTC1车烟火报警烟火探头正常表示警报,target=> {smoke.SmokeF001=SmokeState.Smoke;} },
                {InBoolKeys.InBoTC1车烟火报警烟火探头警告,target=> {smoke.SmokeF001=SmokeState.Warn;} },
            }, (() => smoke.SmokeF001 = SmokeState.Nomal));
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>()
            {
                {InBoolKeys.InBoMP1车烟火报警烟火探头故障,target=> {smoke.SmokeF002=SmokeState.Fault;} },
                {InBoolKeys.InBoMP1车烟火报警烟火探头正常表示未检测到烟火,target=> {smoke.SmokeF002=SmokeState.Nomal;} },
                {InBoolKeys.InBoMP1车烟火报警烟火探头正常表示警报,target=> {smoke.SmokeF002=SmokeState.Smoke;} },
                {InBoolKeys.InBoMP1车烟火报警烟火探头警告,target=> {smoke.SmokeF002=SmokeState.Warn;} },
            }, (() => smoke.SmokeF002 = SmokeState.Nomal));
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>()
            {
                {InBoolKeys.InBoM1车烟火报警烟火探头故障,target=> {smoke.SmokeF003=SmokeState.Fault;} },
                {InBoolKeys.InBoM1车烟火报警烟火探头正常表示未检测到烟火,target=> {smoke.SmokeF003=SmokeState.Nomal;} },
                {InBoolKeys.InBoM1车烟火报警烟火探头正常表示警报,target=> {smoke.SmokeF003=SmokeState.Smoke;} },
                {InBoolKeys.InBoM1车烟火报警烟火探头警告,target=> {smoke.SmokeF003=SmokeState.Warn;} },
            }, (() => smoke.SmokeF003 = SmokeState.Nomal));
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>()
            {
                {InBoolKeys.InBoM2车烟火报警烟火探头故障,target=> {smoke.SmokeF004=SmokeState.Fault;} },
                {InBoolKeys.InBoM2车烟火报警烟火探头正常表示未检测到烟火,target=> {smoke.SmokeF004=SmokeState.Nomal;} },
                {InBoolKeys.InBoM2车烟火报警烟火探头正常表示警报,target=> {smoke.SmokeF004=SmokeState.Smoke;} },
                {InBoolKeys.InBoM2车烟火报警烟火探头警告,target=> {smoke.SmokeF004=SmokeState.Warn;} },
            }, (() => smoke.SmokeF004 = SmokeState.Nomal));
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>()
            {
                {InBoolKeys.InBoMP2车烟火报警烟火探头故障,target=> {smoke.SmokeF005=SmokeState.Fault;} },
                {InBoolKeys.InBoMP2车烟火报警烟火探头正常表示未检测到烟火,target=> {smoke.SmokeF005=SmokeState.Nomal;} },
                {InBoolKeys.InBoMP2车烟火报警烟火探头正常表示警报,target=> {smoke.SmokeF005=SmokeState.Smoke;} },
                {InBoolKeys.InBoMP2车烟火报警烟火探头警告,target=> {smoke.SmokeF005=SmokeState.Warn;} },
            }, (() => smoke.SmokeF005 = SmokeState.Nomal));
            args.UpdateAllTrue(new Dictionary<string, Action<bool>>()
            {
                {InBoolKeys.InBoTC2车烟火报警烟火探头故障,target=> {smoke.SmokeF006=SmokeState.Fault;} },
                {InBoolKeys.InBoTC2车烟火报警烟火探头正常表示未检测到烟火,target=> {smoke.SmokeF006=SmokeState.Nomal;} },
                {InBoolKeys.InBoTC2车烟火报警烟火探头正常表示警报,target=> {smoke.SmokeF006=SmokeState.Smoke;} },
                {InBoolKeys.InBoTC2车烟火报警烟火探头警告,target=> {smoke.SmokeF006=SmokeState.Warn;} },
            }, (() => smoke.SmokeF006 = SmokeState.Nomal));
        }
    }
}