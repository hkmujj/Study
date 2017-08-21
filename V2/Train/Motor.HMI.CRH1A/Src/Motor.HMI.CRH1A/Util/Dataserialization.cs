using System;
using System.IO;
using System.Xml.Serialization;

namespace Motor.HMI.CRH1A.Util
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
                    Console.WriteLine(e);
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
                    Console.WriteLine(e);
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
                        Console.WriteLine(e);
                        return default(T);
                    }
                }
            }
            catch (Exception )
            {
                // TODO record file not found
                //throw new FileNotFoundException(file);
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
                        Console.WriteLine(e);
                        return default(T);
                    }
                }
            }
            catch (Exception )
            {
                // TODO record file not found
                //throw new FileNotFoundException(file);
                return default(T);
            }
        }
    }
}
