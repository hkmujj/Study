﻿using System;
using System.IO;
using System.Xml.Serialization;

namespace MMITool.Common.Util
{
    /// <summary>
    /// 数据序列化
    /// </summary>
    public static class DataSerialization
    {
        /// <summary>
        /// xml 序列化到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="file"></param>
        public static void SerializeToXmlFile<T>(T data, string file)
        {
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var ser = new XmlSerializer(typeof (T));
                    ser.Serialize(fs, data);
                }
                catch (Exception e)
                {
                    LogMgr.Error(e.ToString());
                }
            }
        }

        /// <summary>
        /// xml 序列化到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rootName">根结点的名字</param>
        /// <param name="data"></param>
        /// <param name="file"></param>
        public static void SerializeToXmlFile<T>(T data, string file,string rootName)
        {
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var ser = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));
                    ser.Serialize(fs, data);
                }
                catch (Exception e)
                {
                    LogMgr.Error(e.ToString());
                }
            }
        }



        /// <summary>
        /// 将xml文件反序列化到对象 
        /// </summary>
        /// <param name="file"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeFromXmlFile<T>(string file) 
        {
            try
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        var ser = new XmlSerializer(typeof (T));
                        return (T) ser.Deserialize(fs);
                    }
                    catch (Exception e)
                    {
                        LogMgr.Error(e.ToString());
                        return default(T);
                    }
                }
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Can DeserializeFromXmlFile file {0}, Inner exception : {1}", file, e));
                return default(T);
            }
        }


        /// <summary>
        /// 将xml文件反序列化到对象 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="rootName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeFromXmlFile<T>(string file, string rootName)
        {
            try
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        var ser = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));
                        return (T)ser.Deserialize(fs);
                    }
                    catch (Exception e)
                    {
                        LogMgr.Error(e.ToString());
                        return default(T);
                    }
                }
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Can DeserializeFromXmlFile file {0}, Inner exception : {1}", file, e));
                return default(T);
            }
        }
    }
}