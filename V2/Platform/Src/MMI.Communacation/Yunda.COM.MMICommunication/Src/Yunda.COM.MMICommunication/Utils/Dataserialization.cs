using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Yunda.COM.MMICommunication.Utils
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
                    SerializeToStream(data, fs);
                }
                catch (Exception e)
                {
                    //LogMgr.Error(string.Format("SerializeToXmlFile:{0} error..", file));
                    //LogMgr.Error(e.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        public static string SerializeToString<T>(T data, Encoding encoding)
        {
            using (var ms = new MemoryStream())
            {
                SerializeToStream(data, ms);
                return encoding.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="rootName"></param>
        /// <typeparam name="T"></typeparam>
        public static string SerializeToString<T>(T data, Encoding encoding, string rootName)
        {
            using (var ms = new MemoryStream())
            {
                SerializeToStream(data, rootName, ms);
                return encoding.GetString(ms.ToArray());
            }
        }

        /// <summary>
        ///  使用 Encoding.Default
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public static string SerializeToString<T>(T data)
        {
            return SerializeToString(data, Encoding.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fs"></param>
        /// <typeparam name="T"></typeparam>
        public static void SerializeToStream<T>(T data, Stream fs)
        {
            var ser = new XmlSerializer(typeof (T));
            ser.Serialize(fs, data);
        }

        /// <summary>
        /// xml 序列化到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rootName">根结点的名字</param>
        /// <param name="data"></param>
        /// <param name="file"></param>
        public static void SerializeToXmlFile<T>(T data, string file, string rootName)
        {
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    SerializeToStream(data, rootName, fs);
                }
                catch (Exception e)
                {
                    //LogMgr.Error(e.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rootName"></param>
        /// <param name="fs"></param>
        /// <typeparam name="T"></typeparam>
        public static void SerializeToStream<T>(T data, string rootName, Stream fs)
        {
            var ser = new XmlSerializer(typeof (T), new XmlRootAttribute(rootName));
            ser.Serialize(fs, data);
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
                    return DeserializeFromStream<T>(fs);
                }
            }
            catch (Exception)
            {
                //LogMgr.Error(string.Format("DeserializeFromXmlFile{0} error.. file not found.", file));
                // TODO record file not found
                //throw new FileNotFoundException(file);
                return default(T);
            }
        }

        /// <summary>
        /// 将xml文件反序列化到对象 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeFromString<T>(string content, Encoding encoding)
        {
            try
            {
                using (var ms = new MemoryStream(encoding.GetBytes(content)))
                {
                    return DeserializeFromStream<T>(ms);
                }
            }
            catch (Exception)
            {
                //LogMgr.Error(string.Format("DeserializeFromXmlFile{0} error.. file not found.", content));
                // TODO record file not found
                //throw new FileNotFoundException(file);
                return default(T);
            }
        }

        /// <summary>
        /// 将xml文件反序列化到对象 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <param name="rootName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeFromString<T>(string content, Encoding encoding, string rootName)
        {
            try
            {
                using (var ms = new MemoryStream(encoding.GetBytes(content)))
                {
                    return DeserializeFromStream<T>(ms, rootName);
                }
            }
            catch (Exception)
            {
                //LogMgr.Error(string.Format("DeserializeFromXmlFile{0} error.. file not found.", content));
                // TODO record file not found
                //throw new FileNotFoundException(file);
                return default(T);
            }
        }

        /// <summary>
        /// 将xml文件反序列化到对象 
        /// </summary>
        /// <param name="content"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeFromString<T>(string content)
        {
            return DeserializeFromString<T>(content, Encoding.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static T DeserializeFromStream<T>(Stream fs)
        {
            try
            {
                var ser = new XmlSerializer(typeof (T));
                return (T) ser.Deserialize(fs);
            }
            catch (Exception e)
            {
                //LogMgr.Error(e.ToString());
                return default(T);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fs"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static T DeserializeFromStream<T>(Stream fs, string rootName)
        {
            try
            {
                var ser = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));
                return (T)ser.Deserialize(fs);
            }
            catch (Exception e)
            {
                //LogMgr.Error(e.ToString());
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
                        return DeserializeFromStream<T>(fs, rootName);
                    }
                    catch (Exception e)
                    {
                        //LogMgr.Error(string.Format("DeserializeFromXmlFile{0} error..", file));
                        //LogMgr.Error(e.ToString());
                        return default(T);
                    }
                }
            }
            catch (Exception)
            {
                //LogMgr.Error(string.Format("DeserializeFromXmlFile{0} error.. file not found.", file));
                // TODO record file not found
                //throw new FileNotFoundException(file);
                return default(T);
            }
        }

        class TypeBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Assembly ass = Assembly.GetExecutingAssembly();
                return ass.GetType(typeName);
            }
        }
    }
}
