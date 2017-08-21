using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommonUtil.Controls;
using CommonUtil.Model;

namespace Motor.HMI.CRH3C380B.Base.开关.AirConditioning
{
    internal interface IAirconditionView : IInnerControl
    {
        event Action<IAirconditionStateItem> AirconditionStateChanged;

        event Action<IAirconditionView> CurrentSelectedAirconditionUnitChanged;

        void ChangeAirconditionState(AirconditionLevelUnit unit, AirConditionState state);

        IAirconditionStateItem CurrentSelectedAirconditionUnit { get; }

        ReadOnlyCollection<IAirconditionStateItem> AllAirconditionStateItemCollections { get; }


        void Select(Direction direction);
    }

    internal interface IAirconditionStateItem
    {
        AirconditionLevelUnit Unit { get; }

        AirConditionState State { get; }
    }

    internal enum AirconditionLevelUnit
    {
        None,

        All,

        Unit1,

        Unit2,

        Car11 = 11,
        Car12,
        Car13,
        Car14,
        Car15,
        Car16,
        Car17,
        Car18,
        Car19,
        Car20,
        Car21,
        Car22,
        Car23,
        Car24,
        Car25,
        Car26,
        Car27,
        Car28,
    }

    internal enum AirConditionState
    {
        [Description("空调关(防冻)")] Close = 0,

        [Description("空调开")] Open = 1,

        [Description("温度设定值")] SetTemperature = 2,

        [Description("空调紧急关")] EmergaceClose = 3,

        [Description("空调关(防冻)")] Default = Close,

        [Description("自动开启空调")] AutoOpen = 4,

        [Description("手动开启空调")] ManualOpen = 5,
    }
}
