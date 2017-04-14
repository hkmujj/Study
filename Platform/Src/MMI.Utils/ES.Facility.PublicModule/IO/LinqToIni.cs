using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ES.Facility.PublicModule.IO
{
    /// <summary>
    /// 基于LINQ实现对ini文件的读写
    /// </summary>
    public class LinqToIni
    {
        /// <summary>
        /// ini文件内容结点结构
        /// </summary>
        [DebuggerDisplay("Section={Section} KeyName={KeyName} KeyValue={KeyValue}")]
        private struct IniNode
        {
            /// <summary>
            /// 分区名
            /// </summary>
            public String Section;

            /// <summary>
            /// 键名
            /// </summary>
            public String KeyName;

            /// <summary>
            /// 键值
            /// </summary>
            public String KeyValue;
        }
        
        /// <summary>
        /// ini文件内容结点列表
        /// </summary>
        private List<IniNode> m_IniElement = null;                    
        
        /// <summary>
        /// 读取ini文件并序列化，以供LINQ查询
        /// </summary>
        /// <param name="iniFile">ini文件名</param>
        /// <param name="isQueryOnly">是否只做查询操作。缺省为true</param>
        /// <param name="fileEncoding">文件编码，缺省为null，使用Unicode编码</param>
        /// <returns>
        ///     true：成功
        ///     false：失败
        /// </returns>     
        /// <remarks>
        /// 如果只做查询操作，则序列化时去掉空行结点和注释行结点
        /// </remarks>
        public Boolean Load(String iniFile, Boolean isQueryOnly = true, Encoding fileEncoding = null)
        {
            if (fileEncoding == null)
            {   // 默认使用Unicode编码
                fileEncoding = Encoding.Unicode;
            }

            try
            {   // 自动检测文件编码
                using (var sr = new StreamReader(iniFile,  false))
                {
                    if (m_IniElement == null)
                    {
                        m_IniElement = new List<IniNode>();
                    }
                    else
                    {
                        m_IniElement.Clear();
                    }
                    
                    String section = null;
                    while (true)
                    {
                        var source = sr.ReadLine();
                        if (source == null) break;

                        source = source.Trim();
                        if (source == String.Empty)
                        {   // 空行
                            if (!isQueryOnly)
                            {
                                m_IniElement.Add(new IniNode { Section = "\u000A", KeyName = null, KeyValue = null });
                            }
                        }
                        else if (source[0] == '#' || source[0] == ';')
                        {   // 注释行
                            if (!isQueryOnly)
                            {
                                m_IniElement.Add(new IniNode { Section = "\u000B", KeyName = null, KeyValue = source });
                            }
                        }
                        else if (source[0] == '[')
                        {   // 分区名
                            var rightSquareBracketIndex = source.IndexOf(']');
                            if (rightSquareBracketIndex != -1)
                            {
                                section = source.Substring(1, rightSquareBracketIndex - 1).Trim();
                                if (section != String.Empty)
                                {
                                    m_IniElement.Add(new IniNode { Section = section, KeyName = String.Empty, KeyValue = null });
                                }
                            }
                        }
                        else
                        {   // 键名键值对
                            if (!String.IsNullOrEmpty(section))
                            {
                                var equalsSignIndex = source.IndexOf('=');
                                if (equalsSignIndex != -1)
                                {   // 提取键名
                                    var keyName = source.Substring(0, equalsSignIndex).Trim();
                                    if (keyName != String.Empty)
                                    {   // 提取键值
                                        var keyValue = source.Substring(equalsSignIndex + 1).Trim();
                                        if(keyValue.Length >= 2)
                                        {   // 判断是否有双引号
                                            if (keyValue[0] == '\u0022' && keyValue[keyValue.Length - 1] == '\u0022')
                                            {   
                                                keyValue = keyValue.Substring(1, keyValue.Length - 2);
                                            }
                                        }

                                        m_IniElement.Add(new IniNode { Section = section, KeyName = keyName, KeyValue = keyValue });
                                    }
                                }
                            }
                        }
                    }

                    sr.Close();

                    return true;
                }
            }

            catch (Exception e)
            {
                return false;
            }            
        }

        /// <summary>
        /// 存储ini文件
        /// </summary>
        /// <param name="iniFile">要存储的文件名</param>
        /// <param name="fileEncoding">文件编码。缺省为null，使用Unicode编码</param>
        /// <returns>
        ///     true：成功
        ///     false：失败
        /// </returns>
        /// <remarks>注意：只有调用此函数，才能保存最终数据</remarks>
        public Boolean Save(String iniFile, Encoding fileEncoding = null)
        {
            if (m_IniElement == null)
            {   // 抛出异常：无效的数据源
                throw new ApplicationException("Invalid Data Source!");
            }
            
            if (fileEncoding == null)
            {   // 默认使用Unicode编码
                fileEncoding = Encoding.Unicode;
            }

            try
            {                
                using (var sw = new StreamWriter(iniFile, false, fileEncoding))
                {
                    foreach (var node in m_IniElement)
                    {
                        if (node.KeyName == null)
                        {
                            if (node.Section == "\u000A")
                            {   // 空行
                                sw.WriteLine();
                            }
                            else if (node.Section == "\u000B")
                            {   // 注释行
                                sw.WriteLine(node.KeyValue);
                            }
                        }
                        else
                        {
                            if (node.KeyName == String.Empty)
                            {   // 分区
                                sw.WriteLine("[" + node.Section + "]");
                            }
                            else
                            {   // 键名键值对
                                if (node.KeyValue.IndexOf(' ') == -1)
                                {   // 键值中没有空格
                                    sw.WriteLine(node.KeyName + "=" + node.KeyValue);
                                }
                                else
                                {   // 键值中包含空格，需在键值两端加上引号
                                    sw.WriteLine(node.KeyName + "=\u0022" + node.KeyValue + "\u0022");
                                }
                            }
                        }
                    }

                    sw.Close();
                    return true;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 提取键名对应的键值
        /// </summary>
        /// <param name="section">分区名。如果为null，则提取所有的分区名</param>
        /// <param name="keyName">键名。如果为null，则提取分区所有的键名键值对</param>
        /// <param name="defaultString">缺省键值</param>
        /// <returns>键值</returns>
        public String[] GetProfileString(String section, String keyName, String defaultString)
        {
            if (m_IniElement == null)
            {   // 抛出异常：无效的数据源
                throw new ApplicationException("Invalid Data Source!");
            }

            if(section == null)
            {   // 获取所有的分区名
                return (from node in m_IniElement where (node.KeyName == String.Empty) select node.Section).ToArray();
            }
            else if (keyName == null)
            {   // 获取指定分区所有的键名及键值
                return
                    ( from node in m_IniElement
                      where ( String.Compare(node.Section, section, true) == 0 && !String.IsNullOrEmpty(node.KeyName) )
                      select ( node.KeyName + "=" + node.KeyValue ) ).ToArray();
            }
            else
            {   // 获取键值
                var ValueQuery =
                    ( from node in m_IniElement
                      where ( String.Compare(node.Section, section, true) == 0 && String.Compare(node.KeyName, keyName, true) == 0 )
                      select node.KeyValue ).ToArray();
                if (ValueQuery.Length == 0)
                {
                    return new String[] { defaultString };
                }
                else
                {
                    return ValueQuery;
                }
            }
        }

        /// <summary>
        /// 提取键名对应的键值（整数值）
        /// </summary>
        /// <param name="section">分区名</param>
        /// <param name="keyName">键名</param>
        /// <param name="defaultValue">缺省键值</param>
        /// <returns>键值（整数值）</returns>
        public Int32 GetProfileInt(String section, String keyName, Int32 defaultValue)
        {
            if (m_IniElement == null)
            {   // 抛出异常：无效的数据源
                throw new ApplicationException("Invalid Data Source!");
            }

            if (String.IsNullOrEmpty(section) || String.IsNullOrEmpty(keyName))
            {
                return defaultValue;
            }

            // 获取键值
            var ValueQuery = (from node in m_IniElement where (String.Compare(node.Section, section, true) == 0 && String.Compare(node.KeyName, keyName, true) == 0) select node.KeyValue).ToArray();
            if (ValueQuery.Length == 0)
            {
                return defaultValue;
            }
            else
            {
                if (ValueQuery[0] == String.Empty)
                {
                    return defaultValue;
                }
                else
                {   // 将字符串转换为整数值（注意：可能会抛出异常）
                    return Convert.ToInt32(ValueQuery[0]);
                }
            }
        }

        /// <summary>
        /// 获取分区所有的键名键值对
        /// </summary>
        /// <param name="section">分区名</param>
        /// <returns>键名键值对数组</returns>
        public String[] GetProfileSection(String section)
        {
            if (m_IniElement == null)
            {   // 抛出异常：无效的数据源
                throw new ApplicationException("Invalid Data Source!");
            }

            if (String.IsNullOrEmpty(section))
            {
                return null;
            }
            else
            {   // 获取指定分区所有的键名及键值
                return (from node in m_IniElement where (String.Compare(node.Section, section, true) == 0 && !String.IsNullOrEmpty(node.KeyName)) select (node.KeyName + "=" + node.KeyValue)).ToArray();
            }
        }

        /// <summary>
        /// 获取所有的分区名
        /// </summary>
        /// <returns>分区名数组</returns>
        public String[] GetProfileSectionNames()
        {
            if (m_IniElement == null)
            {   // 抛出异常：无效的数据源
                throw new ApplicationException("Invalid Data Source!");
            }

            // 获取所有的分区名
            return (from node in m_IniElement where (String.Compare(node.KeyName, String.Empty, true) == 0) select node.Section).ToArray();
        }

        /// <summary>
        /// 增加或更新分区名、键名或者键值
        /// </summary>
        /// <param name="section">分区名</param>
        /// <param name="keyName">键名。如果为null或者空串，则删除整个分区</param>
        /// <param name="keyValue">键值。如果为null，则删除该键</param>
        /// <returns>
        ///     true：成功
        ///     false：失败
        /// </returns>
        public Boolean WriteProfileString(String section, String keyName, String keyValue)
        {
            if (String.IsNullOrEmpty(section))
            {
                return false;
            }

            if (m_IniElement == null)
            {   // 初始化ini结点列表
                m_IniElement = new List<IniNode>();
            }

            if (String.IsNullOrEmpty(keyName))
            {   // 删除整个分区（关键：要从后往前删）
                for (var i = m_IniElement.Count - 1; i >= 0; i--)
                {
                    if (String.Compare(m_IniElement[i].Section, section, true) == 0 && m_IniElement[i].KeyName != null)
                    {
                        m_IniElement.RemoveAt(i);
                    }
                }
            }
            else
            {   // 更新键值
                var InsertIndex = -1;
                for (var i = m_IniElement.Count - 1; i >= 0; i--)
                {
                    if (String.Compare(m_IniElement[i].Section, section, true) == 0)
                    {       
                        if (String.Compare(m_IniElement[i].KeyName, keyName, true) == 0)
                        {   // 删除该键
                            m_IniElement.RemoveAt(i);
                            if (keyValue != null)
                            {   // 更新该键
                                if (keyValue.Length >= 2)
                                {   // 判断是否有双引号
                                    if (keyValue[0] == '\u0022' && keyValue[keyValue.Length - 1] == '\u0022')
                                    {
                                        keyValue = keyValue.Substring(1, keyValue.Length - 2);
                                    }
                                }

                                // 插入更新后的键名键值对
                                m_IniElement.Insert(i, new IniNode { Section = section, KeyName = keyName, KeyValue = keyValue });
                            }

                            // 直接返回
                            return true;
                        }

                        if (InsertIndex == -1)
                        {   // 将分区末尾做为插入点
                            InsertIndex = i + 1;
                        }
                    }
                }

                if (InsertIndex == -1)
                {   // 分区不存在，首先增加新的分区名
                    m_IniElement.Add(new IniNode { Section = section, KeyName = String.Empty, KeyValue = null });
                    
                    // 再增加新的键名键值对
                    m_IniElement.Add(new IniNode { Section = section, KeyName = keyName, KeyValue = keyValue });
                }
                else
                {   // 分区存在，在分区末尾增加新的键名键值对
                    m_IniElement.Insert(InsertIndex, new IniNode { Section = section, KeyName = keyName, KeyValue = keyValue });
                }
            }

            return true;
        }

        /// <summary>
        /// 替换分区的键名键值对
        /// </summary>
        /// <param name="section">分区名</param>
        /// <param name="keyValueSet">要替换的键名键值对。如果为null，则删除整个分区</param>
        /// <returns>
        ///     true：成功
        ///     false：失败
        /// </returns>
        public Boolean WriteProfileSection(String section, String[] keyValueSet)
        {
            if (String.IsNullOrEmpty(section))
            {
                return false;
            }

            if (m_IniElement == null)
            {   // 初始化ini结点列表
                m_IniElement = new List<IniNode>();
            }

            // 删除整个分区
            var InsertIndex = m_IniElement.Count;
            for (var i = m_IniElement.Count - 1; i >= 0; i--)
            {
                if (String.Compare(m_IniElement[i].Section, section, true) == 0 && m_IniElement[i].KeyName != null)
                {
                    m_IniElement.RemoveAt(i);
                    InsertIndex = i;
                }
            }      

            if (keyValueSet != null)
            {   // 写入分区名
                m_IniElement.Insert(InsertIndex++, new IniNode {Section = section, KeyName = String.Empty, KeyValue = null });
                
                // 写入键名键值对
                foreach (var s in keyValueSet)
                {
                    var EqualsSignIndex = s.IndexOf('=');
                    if (EqualsSignIndex != -1)
                    {
                        var KeyName = s.Substring(0, EqualsSignIndex).Trim();
                        if (KeyName != String.Empty)
                        {
                            var KeyValue = s.Substring(EqualsSignIndex + 1).Trim();
                            if (KeyValue.Length >= 2)
                            {   // 判断是否有双引号
                                if (KeyValue[0] == '\u0022' && KeyValue[KeyValue.Length - 1] == '\u0022')
                                {
                                    KeyValue = KeyValue.Substring(1, KeyValue.Length - 2);
                                }
                            }

                            m_IniElement.Insert(InsertIndex++, new IniNode { Section = section, KeyName = KeyName, KeyValue = KeyValue });
                        }
                    }
                }           
            }

            return true;
        }

        /// <summary>
        /// 将结构数据写入键值
        /// </summary>
        /// <param name="section">分区名</param>
        /// <param name="keyName">键名</param>
        /// <param name="data">数据</param>
        /// <returns>
        ///     true：成功
        ///     false：失败
        /// </returns>
        public Boolean WriteProfileStruct(String section, String keyName, Byte[] data)
        {
            if (String.IsNullOrEmpty(section) || String.IsNullOrEmpty(keyName))
            {
                return false;
            }

            if (data == null)
            {
                return WriteProfileString(section, keyName, null);
            }

            // 将字节数组转换成16进制字符串
            var sb = new StringBuilder((data.Length + 1) << 1);
            var CheckSum = 0;
            foreach(var b in data)
            {
                CheckSum += b;
                sb.Append(b.ToString("X2"));
            }

            // 写入校验和
            sb.Append(((Byte)CheckSum).ToString("X2"));

            return WriteProfileString(section, keyName, sb.ToString());
        }

        /// <summary>
        /// 提取键值，并转化为字节数组
        /// </summary>
        /// <param name="section">分区名</param>
        /// <param name="keyName">键名</param>
        /// <returns>键值对应的字节数组</returns>
        public Byte[] GetProfileStruct(String section, String keyName)
        {
            if (m_IniElement == null)
            {   // 抛出异常：无效的数据源
                throw new ApplicationException("Invalid Data Source!");
            }

            if (String.IsNullOrEmpty(section) || String.IsNullOrEmpty(keyName))
            {
                return null;
            }

            // 获取键值
            var ValueQuery = (from node in m_IniElement where (String.Compare(node.Section, section, true) == 0 && String.Compare(node.KeyName, keyName, true) == 0) select node.KeyValue).ToArray();
            if (ValueQuery.Length != 1)
            {
                return null;
            }

            // 将16进制字符串转换成字节数组
            var s = ValueQuery[0];
            if (String.IsNullOrEmpty(s) || (s.Length % 2 != 0))
            {
                return null;
            }

            try
            {
                var Num = s.Length / 2 - 1;
                var ValueArray = new Byte[Num];
                var CheckSum = 0;
                for (var i = 0; i < Num; i++)
                {
                    CheckSum += ValueArray[i] = Convert.ToByte(s.Substring(i << 1, 2), 16);
                }

                // 检测校验和
                if (Convert.ToByte(s.Substring(Num << 1, 2), 16) == CheckSum)
                {
                    return ValueArray;
                }
                else
                {   // 校验失败
                    return null;
                }
            }

            catch (Exception)
            {   // 无效字符串
                return null;
            }
        }
    }
}
