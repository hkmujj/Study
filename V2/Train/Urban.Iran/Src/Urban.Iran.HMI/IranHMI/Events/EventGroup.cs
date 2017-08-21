using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.Events
{
    public enum EventGroup
    {
        [Description("DCU2M")]
        [DisplayLocation(Horizontal = 0, Vertical = 0)]
        DCU2M,

        [Description("Auxiliary & Battery")]
        [DisplayLocation(Horizontal = 0, Vertical = 1)]
        AuxiliaryAndBattery,

        [Description("Control & Communication")]
        [DisplayLocation(Horizontal = 0, Vertical = 2)]
        ControlAndCommunication,

        [Description("Doors")]
        [DisplayLocation(Horizontal = 0, Vertical = 3)]
        Doors,

        [Description("Propulsion")]
        [DisplayLocation(Horizontal = 1, Vertical = 0)]
        Propulsion,

        [Description("Brakes/Air")]
        [DisplayLocation(Horizontal = 1, Vertical = 1)]
        BrakesAir,

        [Description("Train Operation")]
        [DisplayLocation(Horizontal = 1, Vertical = 2)]
        TrainOperation,

        [Description("HVAC")]
        [DisplayLocation(Horizontal = 1, Vertical = 3)]
        HVAC,

        [Description("DCU2A")]
        [DisplayLocation(Horizontal = 2, Vertical = 0)]
        DCU2A,

        [Description("Fire")]
        [DisplayLocation(Horizontal = 2, Vertical = 1)]
        Fire,

        [Description("ATC")]
        [DisplayLocation(Horizontal = 2, Vertical = 2)]
        ATC,

        [Description("PIS")]
        [DisplayLocation(Horizontal = 2, Vertical = 3)]
        PIS,
    }

    public static class EventGroupExtension
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<EventGroup, string> m_DescriptionDictionary =
            AllEventGroups
                .ToDictionary(kvp => kvp, kvp => EnumUtil.GetDescription(kvp).First());

        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<EventGroup, DisplayLocationAttribute> m_DisplayLocationDictionary = AllEventGroups
            .ToDictionary(kvp => kvp,
                kvp =>
                    (DisplayLocationAttribute)
                        kvp.GetType()
                            .GetFields()
                            .First(f => f.Name == kvp.ToString())
                            .GetCustomAttributes(typeof(DisplayLocationAttribute), false)
                            .First());

        private static List<EventGroup> m_AllEventGroups;

        public static List<EventGroup> AllEventGroups
        {
            get
            {
                return m_AllEventGroups ?? (m_AllEventGroups = Enum
                    .GetNames(typeof(EventGroup))
                    .Select(s => (EventGroup)Enum.Parse(typeof(EventGroup), s)).ToList());
            }
        }

        public static string GetDescription(this EventGroup eventGroup)
        {
            if (m_DescriptionDictionary.ContainsKey(eventGroup))
            {
                return m_DescriptionDictionary[eventGroup];
            }
            LogMgr.Error(string.Format("Can not found the description of EventGroup.{0}", eventGroup));
            return string.Empty;
        }

        public static DisplayLocationAttribute GetDisplayLocation(this EventGroup eventGroup)
        {
            if (m_DisplayLocationDictionary.ContainsKey(eventGroup))
            {
                return m_DisplayLocationDictionary[eventGroup];
            }

            LogMgr.Error(string.Format("Can not found the DisplayLocationAttribute of EventGroup.{0}", eventGroup));
            return null;
        }
    }
}