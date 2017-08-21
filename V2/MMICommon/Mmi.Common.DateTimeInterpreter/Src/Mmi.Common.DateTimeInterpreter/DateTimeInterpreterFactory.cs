using System;
using System.Collections.Generic;
using CommonUtil.Util;

namespace Mmi.Common.DateTimeInterpreter
{
    public class DateTimeInterpreterFactory
    {
        public static readonly DateTimeInterpreterFactory Instance = new DateTimeInterpreterFactory();

        private readonly Dictionary<RawDataType, IDateTimeInterpreter> m_DateTimeInterpreterDictionary;

        public DateTimeInterpreterFactory()
        {
            m_DateTimeInterpreterDictionary = new Dictionary<RawDataType, IDateTimeInterpreter>()
            {
                { RawDataType.DateTime, new DefaultDateTimeInterpreter()},
                { RawDataType.Tick, new TickDateTimeInterpreter()},
                { RawDataType.Second, new SecondDateTimeInterpreter()}
            };
        }

        public IDateTimeInterpreter GetInterpreter(RawDataType dataType)
        {
            if (m_DateTimeInterpreterDictionary.ContainsKey(dataType))
            {
                return m_DateTimeInterpreterDictionary[dataType];
            }
            var msg = string.Format("Can not found IDateTimeInterpreter where type={0}", dataType);
            LogMgr.Error(msg);
            throw new KeyNotFoundException(msg);
        }
    }
}