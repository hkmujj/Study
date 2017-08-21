using System.ComponentModel;

namespace Urban.Iran.HMI.HVAC
{
    public enum HVACWorkingModel
    {
        [Description("Cooling")]
        Cooling,

        [Description("Heation")]
        Heation,

        [Description("Ventilation")]
        Ventilation,

        [Description("Em.Vent.")]
        EmVent,

        [Description("Test")]
        Test,
    }
}