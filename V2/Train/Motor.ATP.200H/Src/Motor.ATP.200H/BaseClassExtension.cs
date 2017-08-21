using System;
using System.Collections.Generic;
using CommonUtil.Util;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    internal static class BaseClassExtension
    {
        private static readonly IDateTimeInterpreter DateTimeInterpreter =
            DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);

        private static readonly Dictionary<string, IProjectIndexDescriptionConfig> AppProjectIndexDescriptionConfigDictionary = new Dictionary<string, IProjectIndexDescriptionConfig>();

        internal static IProjectIndexDescriptionConfig IndexDescriptionConfig(this baseClass obj)
        {
            if (!AppProjectIndexDescriptionConfigDictionary.ContainsKey(obj.AppConfig.AppName))
            {
                AppProjectIndexDescriptionConfigDictionary.Add(obj.AppConfig.AppName,
                    obj.IConfig.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                        new CommunicationDataKey(obj.AppConfig)));
            }
            return AppProjectIndexDescriptionConfigDictionary[obj.AppConfig.AppName];
        }

        internal static DateTime CurrenTime(this baseClass obj)
        {
            if (obj.DataPackage.Config.SystemConfig.StartModel != StartModel.Edit)
            {
                return
                    DateTimeInterpreter.Interpreter(
                        obj.FloatList[obj.IndexDescriptionConfig().InFloatDescriptionDictionary[InFloatKeys.InfShowingDate]
                            ],
                        obj.FloatList[obj.IndexDescriptionConfig().InFloatDescriptionDictionary[InFloatKeys.InfShowingTime]
                            ]);

            }
            return DateTime.Now;
        }

        internal static int GetInboolIndex(this baseClass obj,string name)
        {
            if (obj.IndexDescriptionConfig().InBoolDescriptionDictionary.ContainsKey(name))
            {
                return obj.IndexDescriptionConfig().InBoolDescriptionDictionary[name];
            }
            AppLog.Error("Can not found inbool index where name={0}", name);
            return int.MaxValue;
        }


        internal static int GetInFloatIndex(this baseClass obj, string name)
        {
            if (obj.IndexDescriptionConfig().InFloatDescriptionDictionary.ContainsKey(name))
            {
                return obj.IndexDescriptionConfig().InFloatDescriptionDictionary[name];
            }
            AppLog.Error("Can not found in float index where name={0}", name);
            return int.MaxValue;
        }

        internal static int GetOutBoolIndex(this baseClass obj, string name)
        {
            if (obj.IndexDescriptionConfig().OutBoolDescriptionDictionary.ContainsKey(name))
            {
                return obj.IndexDescriptionConfig().OutBoolDescriptionDictionary[name];
            }
            AppLog.Error("Can not found out bool index where name={0}", name);
            return int.MaxValue;
        }

        internal static int GetOutFloatIndex(this baseClass obj, string name)
        {
            if (obj.IndexDescriptionConfig().OutFloatDescriptionDictionary.ContainsKey(name))
            {
                return obj.IndexDescriptionConfig().OutFloatDescriptionDictionary[name];
            }
            AppLog.Error("Can not found out float index where name={0}", name);
            return int.MaxValue;
        }
    }
}