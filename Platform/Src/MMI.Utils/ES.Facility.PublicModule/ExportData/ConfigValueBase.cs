using System;
using System.Collections.Generic;

namespace ES.Facility.PublicModule.ExportData
{
    /// <summary>
    /// 配置或格式化信息的基础类
    /// </summary>

    public class ConfigValueBase<TParaKey, TValueType> : IConfigValueBase<TParaKey,TValueType>
    {
        #region ::::::::::::::::::::::::::::::::  attrible  ::::::::::::::::::::::::::::::::
        [field: CLSCompliant(false)]
        protected List<String> _stringList = new List<string>(50);
        /// <summary>
        /// 存储原始配置行信息
        /// </summary>
        public List<String> StringList { get { return _stringList; } set { _stringList = value; } }

        [field: CLSCompliant(false)]
        protected Dictionary<TParaKey, TValueType> _paraObjList = new Dictionary<TParaKey, TValueType>(50);
        /// <summary>
        /// 存储解码后的参数对象信息
        /// </summary>
        public Dictionary<TParaKey, TValueType> ParaObjList { get { return _paraObjList; } set { _paraObjList = value; } }
        #endregion

        [field: CLSCompliant(false)]
        protected string _ignoreKey = ";";      //分号作为忽略关键字

        #region ::::::::::::::::::::::::::::::::  function   ::::::::::::::::::::::::::::::::

        /// <summary>
        /// 分解配置行
        /// </summary>
        /// <param name="cString"></param>
        /// <param name="tmpErrorIndex"></param>
        /// <returns></returns>
        public virtual bool decodeFromString(string cString, ref int tmpErrorIndex)
        {
            return false;
        }

        /// <summary>
        /// 初始化配置信息，即解析配置信息容器中的所有配置信息
        /// </summary>
        /// <param name="tmpErrorIndex"></param>
        /// <returns></returns>
        public virtual bool initParaListFromString(ref int tmpErrorIndex)
        {
            return false;
        }

        public virtual void clear()
        {
            _stringList.Clear();
            _paraObjList.Clear();
        }

        #endregion

        #region ::::::::::::::::::::::::::::::::  default   ::::::::::::::::::::::::::::::::

        public ConfigValueBase() { }
        public ConfigValueBase(string nIgnoreKey) : this() { _ignoreKey = nIgnoreKey; }

        #endregion
    }
}
