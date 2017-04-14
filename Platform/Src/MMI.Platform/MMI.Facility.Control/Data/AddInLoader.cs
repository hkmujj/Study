using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CommonUtil.Util;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Addins;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Control.Data
{
    /// <summary>
    /// 插件对象加载
    /// </summary>
    public class AddInLoader : IAddInLoader
    {
        private Dictionary<string, Type> m_AddinTypeDic;

        private static List<AddinInfo> m_AddInInfoBuffer;

        /// <summary>
        /// 保存类对象类型 ,key :文件名+类名 value: type
        /// </summary>
        private Dictionary<string, Type> AddinTypeDic
        {
            set
            {
                m_AddinTypeDic = value;
                AddinTypeDictionary = value.AsReadOnlyDictionary();
            }
            get { return m_AddinTypeDic; }
        }

        public IReadOnlyDictionary<string, Type> AddinTypeDictionary { get; private set; }

        /// <summary>
        /// 工程下的插件实例
        /// key : 工程名   key : 查找类型的索引,文件名+类名
        /// </summary>
        public Dictionary<string, Dictionary<string, IObjectBase>> ProjetAddinInstanceDic { get; private set; }


        [DebuggerStepThrough]
        internal AddInLoader()
        {
            ProjetAddinInstanceDic = new Dictionary<string, Dictionary<string, IObjectBase>>();
        }

        private IEnumerable<AddinInfo> GetAddinInfoBuffer(IAppConfig appConfig)
        {
            if (m_AddInInfoBuffer == null)
            {
                m_AddInInfoBuffer = Directory.GetFiles(appConfig.ParentConfig.SystemDicrectory.AddinDirectory, "*dll").Select(s => new AddinInfo(s)).ToList();
            }

            return m_AddInInfoBuffer;
        }

        private Dictionary<string, Type> LoadAddinType(IAppConfig appConfig)
        {
            var outCount = 0;
            var dic = new Dictionary<string, Type>();
            var uiobjs = appConfig.AppObjcetConfig.UIObjects;

            var addins = GetAddinInfoBuffer(appConfig);

            foreach (var addin in addins)
            {
                try
                {
                    foreach (var type in addin.ObjectBaseTypeCollection)
                    {
                        if (type.GetInterface(typeof(IObjectBase).FullName) != null)
                        {
                            var tmpObjs = type.GetCustomAttributes(typeof(GksDataTypeAttribute), false);

                            if (tmpObjs.Length > 0)
                            {
                                var uiobj = uiobjs.FindAll(f => f.ClassName == type.FullName);

                                // 解决有相同名字的两个dll, 使用 full name .
                                if (uiobj.Any())
                                {
                                    // 更新type 字典 
                                    outCount = uiobj.Aggregate(outCount, (current, o) => UpdateAddinDic(dic, o, type, current, addin.FileName));
                                }
                                else
                                {
                                    uiobj = uiobjs.FindAll(f => f.ClassName == type.Name);
                                    outCount = uiobj.Aggregate(outCount, (current, o) => UpdateAddinDic(dic, o, type, current, addin.FileName));
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    SysLog.Fatal(e.Message);
                    SysLog.Fatal(e.ToString());
                    SysLog.Fatal(e.StackTrace);
                }
            }
            return dic;
        }

        private Dictionary<string, IObjectBase> LoadAddinInstance(IAppConfig appConfig,  Dictionary<string, Type> dic)
        {
            var proejctName = appConfig.AppName;
            var insDic = new Dictionary<string, IObjectBase>();
            // must be has not , if has , the logic is error
            //ProjetAddinInstanceDic.Add(proejctName, insDic);
            foreach (var kvp in dic)
            {
                try
                {
                    var ins = (IObjectBase)Activator.CreateInstance(kvp.Value);
                    ins.ProjectName = proejctName;
                    ins.UIObj = appConfig.AppObjcetConfig.UIObjects.Find(f => f.ClassName == kvp.Value.Name || f.ClassName == kvp.Value.FullName).Clone() as IUIObject;
                    insDic.Add(kvp.Key, ins);
                }
                catch (Exception)
                {
                    SysLog.Error(string.Format("Can not create instance of type={0}, there has not deafult constructor of the type", kvp.Value));
                }
            }
            return insDic;
        }

        /// <summary>
        /// 加载所有的文件
        /// </summary>
        /// <param name="config"></param>
        public void LoadAddin(IConfig config)
        {
            foreach (var appConfig in config.AppConfigs.Where(w => w.SubsystemType == SubsystemType.Addin))
            {

                AddinTypeDic = LoadAddinType(appConfig);
                var insDic = LoadAddinInstance(appConfig, AddinTypeDic);
                // must be not have, if have , the logic is error
                ProjetAddinInstanceDic.Add(appConfig.AppName, insDic);
            }

        }


        private IEnumerable<Type> FindIObjectBasees(Assembly assembly)
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

        private int UpdateAddinDic(Dictionary<string, Type> dic,IUIObject obj, Type type, int outCount, string file)
        {
            var tmpIob = (IObjectBase)Activator.CreateInstance(type);
            obj.DllName = file;

            var objKey = obj.ObjectKey;
            if (!dic.ContainsKey(objKey))
            {
                dic.Add(objKey, type);

                //记录信息
                var tmpMsg = string.Format("{0}\t文件名：{1}\t 类名：{2}\t描述信息：{3}", outCount, file, tmpIob.GetType(),
                    tmpIob.GetInfo());

                AppLog.Debug(tmpMsg);

                outCount++;
            }
            else
            {
                var tmpMsg = string.Format("Error : 该对象已经存在, {0}\t{1}\t{2}", file, tmpIob.GetType(), tmpIob.GetInfo());
                SysLog.Error(tmpMsg);
            }
            return outCount;
        }
    }
}
