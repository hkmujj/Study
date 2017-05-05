using System;
using System.Windows;
using CommonUtil.Util;

namespace Engine.TCMS.Turkmenistan.Extension
{
    public static class ResourceExtention
    {
        public static T GetResourceOfKey<T>(this string key)
        {
            try
            {
                return (T)Application.Current.FindResource(key);

            }
            catch (Exception)
            {

                AppLog.Error($"Key {key} Not Found!");
            }
            return default(T);
        }
    }
}