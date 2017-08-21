using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommonUtil.Controls;
using CommonUtil.Model;

namespace Motor.HMI.CRH3C380B.Base.开关.Lighting
{
    internal interface ILightingView : IInnerControl
    {
        event Action<ILightStateItem> LightingStateChanged;

        event Action<ILightingView> CurrentSelectedLightingUnitChanged;

        void ChangeLightingState(LightingUnit unit, LightState state);
        ILightStateItem CurrentSelectedLightingUnit { get; }

        ReadOnlyCollection<ILightStateItem> AllLightStateItemCollection { get; }


        void Select(Direction direction);
    }

    internal interface ILightStateItem
    {
        LightingUnit Unit { get; }

        LightState State { get; }
    }

    public enum LightingUnit
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

    public enum LightState
    {
        [Description("照明 0")] Light0,

        [Description("照明 1/3")] Light1P3,

        [Description("照明 1")] Light1,
    }
}