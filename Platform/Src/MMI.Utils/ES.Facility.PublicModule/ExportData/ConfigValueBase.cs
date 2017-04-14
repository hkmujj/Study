using System;
using System.Collections.Generic;

namespace ES.Facility.PublicModule.ExportData
{
    /// <summary>
    /// ���û��ʽ����Ϣ�Ļ�����
    /// </summary>

    public class ConfigValueBase<TParaKey, TValueType> : IConfigValueBase<TParaKey,TValueType>
    {
        #region ::::::::::::::::::::::::::::::::  attrible  ::::::::::::::::::::::::::::::::
        [field: CLSCompliant(false)]
        protected List<String> _stringList = new List<string>(50);
        /// <summary>
        /// �洢ԭʼ��������Ϣ
        /// </summary>
        public List<String> StringList { get { return _stringList; } set { _stringList = value; } }

        [field: CLSCompliant(false)]
        protected Dictionary<TParaKey, TValueType> _paraObjList = new Dictionary<TParaKey, TValueType>(50);
        /// <summary>
        /// �洢�����Ĳ���������Ϣ
        /// </summary>
        public Dictionary<TParaKey, TValueType> ParaObjList { get { return _paraObjList; } set { _paraObjList = value; } }
        #endregion

        [field: CLSCompliant(false)]
        protected string _ignoreKey = ";";      //�ֺ���Ϊ���Թؼ���

        #region ::::::::::::::::::::::::::::::::  function   ::::::::::::::::::::::::::::::::

        /// <summary>
        /// �ֽ�������
        /// </summary>
        /// <param name="cString"></param>
        /// <param name="tmpErrorIndex"></param>
        /// <returns></returns>
        public virtual bool decodeFromString(string cString, ref int tmpErrorIndex)
        {
            return false;
        }

        /// <summary>
        /// ��ʼ��������Ϣ��������������Ϣ�����е�����������Ϣ
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
