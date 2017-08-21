using System;
using System.Collections.Generic;
using System.Linq;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    internal class FaultTypeHelper
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<FaultType, FaultShowAttribute> m_FaultShowAttributeDictionary;

        static FaultTypeHelper()
        {
            m_FaultShowAttributeDictionary = new Dictionary<FaultType, FaultShowAttribute>();

            var fields = typeof(FaultType).GetFields();

            foreach (var field in fields)
            {
                var attrs = field.GetCustomAttributes(typeof (FaultShowAttribute), false);

                FaultType value;

                if (Enum.TryParse(field.Name, out value))
                {
                    m_FaultShowAttributeDictionary.Add(value, attrs.FirstOrDefault() as FaultShowAttribute);
                }
            }
        }

        public static FaultShowAttribute GetFaultShowAttribute(FaultType type)
        {
            return m_FaultShowAttributeDictionary[type];
        }

    }
}