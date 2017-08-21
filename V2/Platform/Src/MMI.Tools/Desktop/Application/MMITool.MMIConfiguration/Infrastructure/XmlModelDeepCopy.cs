using System.IO;
using System.Xml.Serialization;

namespace MMITool.Addin.MMIConfiguration.Infrastructure
{
    internal class XmlModelDeepCopy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Copy<T>(T obj) where T : class
        {
            using (var ms = new MemoryStream())
            {
                var bf = new XmlSerializer(typeof (T));
                bf.Serialize(ms, obj);
                ms.Position = 0;
                return bf.Deserialize(ms) as T;
            }
        }
    }
}