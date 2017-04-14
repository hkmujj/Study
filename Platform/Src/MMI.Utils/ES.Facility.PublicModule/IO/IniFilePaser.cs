using System;
using System.IO;
using System.Linq;
using ES.Facility.PublicModule.Util;

namespace ES.Facility.PublicModule.IO
{
    class IniFilePaserTest
    {
        internal class ProjectConfig
        {
            [IniField(Section = "工程配置信息", KeyName = "运行目录")]
            public string RunDir { set; get; }

        }

        internal class FmsConfig
        {
            [IniField(Section = "FmsConfig", KeyName = "程序版本")]
            public string AppVersion { set; get; }
        }


        [IniField(Section = "工程配置信息", MemberType = IniFieldAttribute.Type.Section)]
        public ProjectConfig Project { set; get; }

        [IniField(Section = "工程配置信息", MemberType = IniFieldAttribute.Type.Section)]
        public FmsConfig Fms { set; get; }

        public static void Teset()
        {
            var data = IniFilePaser.Parse<IniFilePaserTest>(@"D:\Work\Yunda\MMI\Platform\Development outputs\config.ini");

            IniFilePaser.DeParse("d:\\a.ini", data);

        }
    }

    /// <summary>
    ///  IniFilePaser,解析 ini 文件成对象 
    /// </summary>
    public class IniFilePaser
    {

        /// <summary>
        /// 解析 ini 文件成对象 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public static T Parse<T>(string file, params string[] propertyNames) where T : new()
        {
            IniFieldAttributeCollection members;
            if (!Validate(file, typeof(T), propertyNames, out members))
            {
                return new T();
            }

            if (members == null)
            {
                var msg = string.Format("Can not load file {0} or load file error.", file);
                throw new FileLoadException(msg);
            }
            if (members.Count == 0)
            {
                var msg = string.Format("There is no any info in file {0}", file);
                throw new Exception(msg);
            }


            // 只有两层， 要么 分区 要么字段
            var isSection = members[0].MemberType == IniFieldAttribute.Type.Section;

            var ini = new IniHelper(file);
            if (isSection)
            {
                var entity = Activator.CreateInstance<T>();

                foreach (var member in members)
                {
                    GenerateSection(member, ini, entity);
                }
                return entity;
            }
            else
            {
                var entity = Activator.CreateInstance<T>();
                return GenerateKeyValues(entity, ini, members);
            }
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="data"></param>
        public static void DeParse<T>(string file, T data)
        {
            var properties = data.GetType().GetProperties();
            var ini = new IniHelper(file);
            foreach (var propertyInfo in properties)
            {
                var attr = propertyInfo.GetCustomAttributes(true).OfType<IniFieldAttribute>().FirstOrDefault();
                // TODO GetLastError 
                if (attr != null)
                {
                    if (!string.IsNullOrEmpty(attr.KeyName))
                    {
                        ini.Inset(attr.Section, attr.KeyName, propertyInfo.GetValue(data, null).ToString());
                    }
                    if (attr.MemberType == IniFieldAttribute.Type.Section)
                    {
                        var memValue = propertyInfo.GetValue(data, null);
                        var members = new IniFieldAttributeCollection(propertyInfo.PropertyType);
                        foreach (var member in members)
                        {
                            ini.Inset(member.Section, member.KeyName, member.PropertyInfo.GetValue(memValue, null).ToString());
                        }
                    }
                }
            }
        }

        private static void GetOrCreateFile<T>(string file)
        {
            if (!File.Exists(file))
            {
                try
                {
                    using (File.Create(file))
                    {
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        private static void GenerateSection<T>(IniFieldAttribute member, IniHelper ini, T entity) where T : new()
        {
            var type = member.PropertyInfo.PropertyType;
            if (!member.PropertyInfo.PropertyType.IsClass)
            {
                if (member.InstanceType == null)
                {
                    throw new ArgumentException("can not careate typeof interface or abstract class, you must set the InstanceType");
                }
                type = member.InstanceType;
            }
            var chirldMembers = new IniFieldAttributeCollection(type);
            var memberValue = Activator.CreateInstance(type);
            GenerateKeyValues(memberValue, ini, chirldMembers);
            member.PropertyInfo.SetValue(entity, memberValue, null);
        }

        private static bool Validate(string file, Type type, string[] propertyNames, out IniFieldAttributeCollection members)
        {
            if (!File.Exists(file))
            {
                members = null;
                return true;
            }

            members = new IniFieldAttributeCollection(type, propertyNames);

            if (members.Count < 1)
            {
                return false;
            }
            return true;
        }


        private static T GenerateKeyValues<T>(T entity, IniHelper ini, IniFieldAttributeCollection members)
        {
            foreach (var member in members)
            {
                var propertyValue = ini.Select<string>(member.Section, member.KeyName, member.DefaultValue);
                var result = DynamicInvokeHelper.DynamicInvoke(member.PropertyInfo.PropertyType, propertyValue);
                member.PropertyInfo.SetValue(entity, result, null);
            }
            return entity;
        }

    }
}
