using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Control.Data
{
    /// <summary>
    /// 插件信息
    /// </summary>
    public class AddinInfo
    {
 
        public string FilePath { private set; get; }

        public string FileName { private set; get; }

        public Assembly Assembly { private set; get; }

        public List<Type> ObjectBaseTypeCollection { private set; get; }

        public AddinInfo(string filePath)
        {
            FilePath = filePath;
            if (File.Exists(filePath))
            {
                FileName = Path.GetFileName(filePath);
                try
                {
                    var rawAss = File.ReadAllBytes(filePath);
                    //var assembly = Assembly.LoadFile(file);
                    Assembly = Assembly.Load(rawAss);

                    SysLog.Info(string.Format("Load assembly success. Assembly: {0} .  File: {1}", Assembly, filePath));

                    ObjectBaseTypeCollection = FindIObjectBasees(Assembly);
                }
                catch (Exception e)
                {
                    SysLog.Fatal(e.Message);
                    SysLog.Fatal(e.StackTrace);
                }
            }
        }

        private List<Type> FindIObjectBasees(Assembly assembly)
        {
            var types = new List<Type>();
            try
            {
                types = assembly.GetTypes()
                                .ToList()
                                .FindAll(f => f.GetInterface(typeof(IObjectBase).FullName) != null);
            }
            catch (ReflectionTypeLoadException e)
            {
                SysLog.Fatal(e.Message);
                SysLog.Fatal(e.StackTrace);
                var sb = new StringBuilder();
                sb.Append("here is ther load exceptions: ");
                for (var i = 0; i < e.LoaderExceptions.Length; i++)
                {
                    var exception = e.LoaderExceptions[i];
                    sb.AppendFormat("{0}: {1}\r\n", i, exception.Message);
                }
                SysLog.Fatal(sb.ToString());
            }
            return types;
        }

    }
}