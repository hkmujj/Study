using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CommonUtil.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlAttributeUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputUri"></param>
        /// <param name="parentXPath"></param>
        /// <param name="entities"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Append<T>(string inputUri, string parentXPath,
            List<T> entities) where T : new()
        {
            return RepalceOrAppend(inputUri, parentXPath, entities, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputUri"></param>
        /// <param name="parentXPath"></param>
        /// <param name="entities"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Replace<T>(string inputUri, string parentXPath,
            List<T> entities) where T : new()
        {
            return RepalceOrAppend(inputUri, parentXPath, entities, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputUri"></param>
        /// <param name="parentXPath"></param>
        /// <param name="entities"></param>
        /// <param name="appendToLast"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool RepalceOrAppend<T>(string inputUri, string parentXPath,
            List<T> entities, bool appendToLast) where T : new()
        {
            if (!File.Exists(inputUri) || parentXPath.IsNullOrWhiteSpace()
                || entities == null || entities.Count == 0)
            {
                return false;
            }

            var document = new XmlDocument();
            document.Load(inputUri);
            var parentElement = document.DocumentElement.SelectSingleNode(parentXPath) as XmlElement;

            if (parentElement == null)
            {
                return false;
            }

            var elementContent = ConvertToString<T>(entities);
        
            if (appendToLast)
            {
                parentElement.InnerXml += elementContent;
            }
            else
            {
                parentElement.InnerXml = elementContent;
            }

            document.Save(inputUri);
            document = null;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ConvertToString<T>(List<T> entities) where T : new()
        {
            var elements = Convert<T>(entities);
            if (elements == null || elements.Count == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            elements.ForEach(p => builder.AppendLine(p.ToString()));

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<XElement> Convert<T>(List<T> entities) where T : new()
        {
            if (entities == null || entities.Count == 0)
            {
                return new List<XElement>();
            }

            var elements = new List<XElement>();
            XElement element;

            foreach (var entity in entities)
            {
                element = Convert<T>(entity);
                if (element == null)
                {
                    continue;
                }

                elements.Add(element);
            }

            return elements;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ConvertToString<T>(T entity) where T : new()
        {
            var element = Convert<T>(entity);
            return element == null ? string.Empty : element.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static XElement Convert<T>(T entity) where T : new()
        {
            if (entity == null)
            {
                return null;
            }

            var className = GetClassName<T>();
            var element = new XElement(className);

            var properties = typeof(T).GetProperties().ToList();
            var propertyName = string.Empty;
            object propertyValue = null;

            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    propertyName = GetPropertyName(property);
                    propertyValue = property.GetValue(entity, null);
                    if (property.PropertyType.Name == "Color")
                    {
                        propertyValue = ColorTranslator.ToHtml((Color)propertyValue);
                    }
                    element.SetAttributeValue(propertyName, propertyValue);
                }
            }

            return element;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputUri"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> ParseByDescendants<T>(string inputUri) where T : new()
        {
            if (!File.Exists(inputUri))
            { 
                return new List<T>();
            }

            var document = XDocument.Load(inputUri);

            return ParseByDescendants<T>(document);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> ParseByDescendants<T>(XDocument document) where T : new()
        {
            if (document == null)
            {
                return new List<T>();
            }

            var xName = GetClassName<T>();
            var elements = document.Descendants(xName);

            return ParseByDescendants<T>(elements);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elements"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> ParseByDescendants<T>(IEnumerable<XElement> elements) where T : new()
        {
            if (elements == null || elements.Count() == 0)
            {
                return new List<T>();
            }

            var entities = new List<T>();
       
            foreach (var element in elements)
            {
                entities.Add(AddEntity<T>(element.Attributes()));            
            }

            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributes"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T AddEntity<T>(IEnumerable<XAttribute> attributes) where T : new()
        {
            if (attributes == null || attributes.Count() == 0)
            {
                return default(T);
            }

            var entity = new T();
            var properties = typeof(T).GetProperties().ToList();

            foreach (var attribute in attributes)
            {
                SetPropertyValue<T>(properties, entity, attribute.Name.ToString(), attribute.Value);
            }

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputUri"></param>
        /// <param name="parentXPath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Parse<T>(string inputUri, string parentXPath) where T : new()
        {
            if (!File.Exists(inputUri) || parentXPath.IsNullOrWhiteSpace())
            {
                return new List<T>();
            }

            var document = new XmlDocument();
            document.Load(inputUri);

            return Parse<T>(document, parentXPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parentXPath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Parse<T>(XmlDocument document, string parentXPath) where T : new()
        {
            if (document == null || parentXPath.IsNullOrWhiteSpace())
            {
                return new List<T>();
            }

            var parentNode = document.DocumentElement.SelectSingleNode(parentXPath);

            if (parentNode == null)
            {
                return new List<T>();
            }

            return Parse<T>(parentNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Parse<T>(XmlNode parentNode) where T : new()
        {
            if (parentNode == null || !parentNode.HasChildNodes)
            {
                return new List<T>();
            }

            var nodeList = parentNode.ChildNodes;

            return Parse<T>(nodeList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeList"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Parse<T>(XmlNodeList nodeList) where T : new()
        {
            if (nodeList == null || nodeList.Count == 0)
            {
                return new List<T>();
            }

            var entities = new List<T>();
            AddEntities<T>(nodeList, entities);

            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputUri"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Parse<T>(string inputUri) where T : new()
        {
            if (!File.Exists(inputUri))
            {
                return new List<T>();
            }

            var settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

            var reader = XmlReader.Create(inputUri, settings);

            return Parse<T>(reader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Parse<T>(XmlReader reader) where T : new()
        {
            if (reader == null)
            {
                return new List<T>();
            }

            reader.ReadStartElement();
            var className = GetClassName<T>();     
            var properties = typeof(T).GetProperties().ToList();
            var entities = new List<T>();
            var entity = new T();

            while (!reader.EOF)
            {
                if (!string.Equals(reader.Name, className) || !reader.IsStartElement())
                {
                    reader.Read();
                    continue;
                }

                entity = new T();

                if (!reader.HasAttributes)
                {
                    entities.Add(entity);
                    reader.Read();
                    continue;
                }

                SetPropertyValue<T>(reader, properties, entity);
                entities.Add(entity);
                reader.Read();
            }

            reader.Close();

            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetClassName<T>() where T : new()
        {
            var className = typeof(T).Name;

            var attributes = typeof(T).GetCustomAttributes(
                typeof(ClassAttribute), false) as ClassAttribute[];

            if (attributes != null && attributes.Length > 0)
            {
                if (!attributes[0].Name.IsNullOrWhiteSpace())
                {
                    className = attributes[0].Name;
                }
            }

            return className;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetPropertyName(PropertyInfo property)
        {
            if (property == null)
            {
                return string.Empty;
            }

            var propertyName = property.Name;

            var attributes = property.GetCustomAttributes(typeof(PropertyAttribute),
                false) as PropertyAttribute[];

            if (attributes != null && attributes.Length > 0)
            {
                if (!attributes[0].Name.IsNullOrWhiteSpace())
                {
                    propertyName = attributes[0].Name;
                }
            }

            return propertyName;
        }

        private static void AddEntities<T>(XmlNodeList nodeList,
            List<T> entities) where T : new()
        {
            var className = GetClassName<T>();
            var properties = typeof(T).GetProperties().ToList();
            var entity = new T();

            foreach (XmlNode xmlNode in nodeList)
            {
                var element = xmlNode as XmlElement;
                if (element == null || !string.Equals(className, element.Name))
                {
                    continue;
                }

                entity = new T();
                if (!element.HasAttributes)
                {
                    entities.Add(entity);
                    continue;
                }

                var attributes = element.Attributes;
                foreach (XmlAttribute attribute in attributes)
                {
                    SetPropertyValue<T>(properties, entity, attribute.Name, attribute.Value);
                }

                entities.Add(entity);
            }
        }

        private static void SetPropertyValue<T>(XmlReader reader, 
            List<PropertyInfo> properties, T entity) where T : new()
        {
            while (reader.MoveToNextAttribute())
            {
                SetPropertyValue<T>(properties, entity, reader.Name, reader.Value);
            }
        }

        private static void SetPropertyValue<T>(List<PropertyInfo> properties,
            T entity, string name, string value) where T : new()
        {
            foreach (var property in properties)
            {
                if (!property.CanWrite)
                {
                    continue;
                }

                var propertyName = GetPropertyName(property);

                if (string.Equals(name, propertyName))
                {
                    //FuncDictionary action = new FuncDictionary();
                    var invokeResult = DynamicInvokeHelper.DynamicInvoke(property.PropertyType, value);

                    property.SetValue(entity, invokeResult, null);
                }
            }
        }

    }
}
