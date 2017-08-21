using System;
using System.Drawing;
using System.Linq;
using MMI.Facility.DataType.Config;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.DataType.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class DebugWindowConfigExtension
    {
        /// <summary>
        /// 获取第一个与类型相符的 Rectangle
        /// </summary>
        /// <param name="debugConfig"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Rectangle FirstOrDefaultRectangle(this IDebugWindowConfig debugConfig, Type type)
        {
            return FirstOrDefaultRectangle(debugConfig, type.FullName);
        }
        /// <summary>
        /// 获取第一个与类型相符的 Rectangle
        /// </summary>
        /// <param name="debugConfig"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static Rectangle FirstOrDefaultRectangle(this IDebugWindowConfig debugConfig, string fullName)
        {
            var config = debugConfig.UserDebugWindownConfigCollection.FirstOrDefault(f => f.TypeName == fullName);
            return config != null ? config.Rectangle : Rectangle.Empty;
        }

        /// <summary>
        /// 设置或者更新区域
        /// </summary>
        /// <param name="debugConfig"></param>
        /// <param name="type"></param>
        /// <param name="rectangle"></param>
        public static void SetOrUpdateRectangle(this DebugWindowConfig debugConfig, Type type, Rectangle rectangle)
        {
            SetOrUpdateRectangle(debugConfig, type.FullName, rectangle);
        }

        public static void SetOrUpdateRectangle(DebugWindowConfig debugConfig, string fullName, Rectangle rectangle)
        {
            var config = debugConfig.UserDebugWindownConfigCollection.FirstOrDefault(f => f.TypeName == fullName);
            if (config == null)
            {
                config = new UserDebugWindownConfig() { TypeName = fullName };
                debugConfig.UserDebugWindownConfigCollection.Add(config);
            }
            config.Rectangle = rectangle;
        }
    }
}