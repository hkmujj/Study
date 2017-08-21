using System;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;

namespace KumM_MMI
{
    internal static class BaseClassExtension
    {
        private static readonly IDateTimeInterpreter DateTimeInterpreter =
            DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime); 

        public static DateTime NowTime(this baseClass obj)
        {
            if (obj.IConfig.SystemConfig.StartModel == StartModel.Edit)
            {
                return DateTime.Now;
            }

            return DateTimeInterpreter.Interpreter(obj.FloatList[193], obj.FloatList[194]);
        }
    }
}