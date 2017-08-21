using System;
using System.Collections.Generic;
using Subway.WuHanLine6.GIS.Models;

namespace Subway.WuHanLine6.GIS.Extention
{
    public static class CommunicationExtention
    {
        public static void UpdateIfContanin(this IDictionary<int, bool> args, string key, Action<bool> action)
        {
            if (GlobalParams.Instance.InitParam != null && GlobalParams.Instance.IndexDescription.InBoolDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParams.Instance.IndexDescription.InBoolDescriptionDictionary[key];
                if (args.ContainsKey(index))
                {
                    action?.Invoke(args[index]);
                }
            }
        }
        public static void UpdateIfContanin(this IDictionary<int, float> args, string key, Action<float> action)
        {
            if (GlobalParams.Instance.InitParam != null && GlobalParams.Instance.IndexDescription.InFloatDescriptionDictionary.ContainsKey(key))
            {
                var index = GlobalParams.Instance.IndexDescription.InFloatDescriptionDictionary[key];
                if (args.ContainsKey(index))
                {
                    action?.Invoke(args[index]);
                }
            }
        }
    }
}