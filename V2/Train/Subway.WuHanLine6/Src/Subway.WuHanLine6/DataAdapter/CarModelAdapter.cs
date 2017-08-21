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
    ///
    /// </summary>
    [Export(typeof(IModelAdapter))]
    public class CarModelAdapter : Adapterbase
    {
        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected override void DataChanged(IDictionary<int, bool> args)
        {
            var carModel = Model.CarModel;

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车受电弓动作中, b => { carModel.PantographStateOne = PantographState.Motion; });
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车受电弓升弓, b => Model.CarModel.PantographStateOne = PantographState.Up);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车受电弓降弓, b => carModel.PantographStateOne = PantographState.Down);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP1车受电弓通信正常, b => carModel.PantographStateOne = PantographState.UnUsual);

            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车受电弓动作中, b => { carModel.PantographStateTwo = PantographState.Motion; });
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车受电弓升弓, b => Model.CarModel.PantographStateTwo = PantographState.Up);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车受电弓降弓, b => carModel.PantographStateTwo = PantographState.Down);
            args.UpdateIfContainWhereTrue(InBoolKeys.InBoMP2车受电弓通信正常, b => carModel.PantographStateTwo = PantographState.UnUsual);

            args.UpdateIfContain(InBoolKeys.InBoTC1司机室激活, b =>
            {
                carModel.CABStateOne = b ? CABState.Active : CABState.Normal;
            });
            args.UpdateIfContain(InBoolKeys.InBoTC2司机室激活, b =>
            {
                carModel.CABStateTwo = b ? CABState.Active : CABState.Normal;
            });
            args.UpdateIfContain(InBoolKeys.InBoTC1车逃生门正常, b =>
            {
                if (b)
                {
                    carModel.EscapeDoorStateOne = EscapeDoorState.Normal;
                }
            });
            args.UpdateIfContain(InBoolKeys.InBoTC1车逃生门故障, b =>
            {
                if (b)
                {
                    carModel.EscapeDoorStateOne = EscapeDoorState.Fault;
                }
            });
            args.UpdateIfContain(InBoolKeys.InBoTC2车逃生门正常, b =>
            {
                if (b)
                {
                    carModel.EscapeDoorStateTwo = EscapeDoorState.Normal;
                }
            });
            args.UpdateIfContain(InBoolKeys.InBoTC2车逃生门故障, b =>
            {
                if (b)
                {
                    carModel.EscapeDoorStateTwo = EscapeDoorState.Fault;
                }
            });
            var tmp = new Dictionary<string, Action<bool>>();
            tmp.Add(InBoolKeys.InBo工况牵引, b => carModel.WorkState = WorkState.Traction);
            tmp.Add(InBoolKeys.InBo工况制动, b => carModel.WorkState = WorkState.Brake);
            tmp.Add(InBoolKeys.InBo工况惰行, b => carModel.WorkState = WorkState.Lazy);
            args.UpdateAllTrue(tmp, () => carModel.WorkState = WorkState.Normal);

            var dir = new Dictionary<string, Action<bool>>();
            dir.Add(InBoolKeys.InBo列车运行方向左, b => carModel.Direction = Direction.Left);
            dir.Add(InBoolKeys.InBo列车运行方向右, b => carModel.Direction = Direction.Right);
            args.UpdateAllTrue(dir, () => carModel.Direction = Direction.Normal);
        }
    }
}