using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommonUtil.Controls;
using CommonUtil.Model;

namespace Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches
{
    internal interface ICloseCouplerHatchesView : IInnerControl
    {
        event Action<ICouplerHatchesStateItem> CloseCouplerHatchesStateChanged;

        event Action<ICloseCouplerHatchesView> CurrentSelectedCloseCouplerHatchesUnitChanged;

        void ChangeAirconditionState(CouplerHatchesUnit unit, CouplerHatchesState state);

        ICouplerHatchesStateItem CurrentSelectedCloseCouplerHatchesUnit { get; }
        ReadOnlyCollection<ICouplerHatchesStateItem> AllCloseCouplerHatchesStateItemCollections { get; }
        void Select(Direction direction);

        void Reset(bool bPara);
    }

    internal interface ICouplerHatchesStateItem
    {
        CouplerHatchesUnit Unit { get; }

        CouplerHatchesState State { get; }
    }

    public enum CouplerHatchesUnit
    {
        None,
        All,
        Unit1,
        Unit2,
    }

    /// <summary>
    /// 枚举状态
    /// </summary>
    public enum CouplerHatchesState
    {
        [Description("关闭")] Close = 0,
        [Description("打开")] Open = 1,
        [Description("运动中")] Motion = 2,
        [Description("已联挂")] Coupled = 3,
        [Description("准备联挂")] PlanCoupl = 5,
        [Description("故障")] Breakdown = 7,
    }

}
