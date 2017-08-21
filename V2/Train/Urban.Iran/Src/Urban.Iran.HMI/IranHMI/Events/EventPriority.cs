using System.ComponentModel;

namespace Urban.Iran.HMI.Events
{
    public enum EventPriority
    {
        [Description("Unkown")]
        Unkown = 0,

        /// <summary>
        /// Handling errors that require the driver to perform a corrective action
        /// 全屏显示
        /// </summary>
        [Description("Info")]
        Information = 1,

        /// <summary>
        /// Fire alarm events from fire system
        /// </summary>
        [Description("Fire-alarm")]
        FireAlarm = 2,

        /// <summary>
        /// Faults that require the drivers' immediate attention and action
        /// </summary>
        [Description("A-alarm")]
        AAlarm = 3,

        /// <summary>
        /// Faults that require the drivers' attention and action but can wait some time.
        /// </summary>
        [Description("B-alarm")]
        BAlarm = 4,

        /// <summary>
        /// A fault that needs to be corrected by maintenance personnel but shall not be shown to the driver and crew on the train
        /// </summary>
        [Description("fault")]
        Fault = 5,

        /// <summary>
        /// Not used.
        /// </summary>
        [Description("ManualFault")]
        ManualFault = 6,

        /// <summary>
        /// Events (not faults) that have occurred on the train that is stored for information.
        /// 白色
        /// </summary>
        [Description("Event")]
        Event = 7,

    }
}