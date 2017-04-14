using System;
using System.Collections.Generic;

namespace ES.Facility.PublicModule.BaseData
{
    /// <summary>
    /// �汾˵���ӿ�
    /// </summary>
    [CLSCompliant(false)]
    public interface IVerHelper
    {
        /// <summary>
        /// ES��ϵ�еĽӿڰ汾���
        /// </summary>
        /// <remarks>��Ӧ֧�ֵĽӿڵ�Ԫ</remarks>
        int EsInterfaceVerIndex { get;}

        /// <summary>
        /// �����
        /// </summary>
        int VerMainIndex { get;}

        /// <summary>
        /// �η��
        /// </summary>
        int VerSubIndex { get;}

        /// <summary>
        /// ���÷�����Ϣ
        /// </summary>
        /// <param name="nVerMainIndex"></param>
        /// <param name="nVerSubIndex"></param>
        void setVerIndex(int nVerMainIndex, int nVerSubIndex);

        /// <summary>
        /// ��ȡ�汾���
        /// </summary>
        /// <returns></returns>
        string getVerIndex();

        /// <summary>
        /// ��ȡ�汾��Ϣ
        /// </summary>
        string getVerInfo();

        /// <summary>
        /// ��ȡ�����汾��Ϣ
        /// </summary>
        /// <returns></returns>
        string getVerAllInfo();
    }

    /// <summary>
    /// ������������Ϣ�ӿ�
    /// </summary>
    public interface IMemoHelper
    {
        /// <summary>
        /// ��ȡ�ö���ı�ע��Ϣ
        /// </summary>
        string getMemoInfo();

        /// <summary>
        /// ��ȡ�ö���İ�����Ϣ
        /// </summary>
        /// <returns></returns>
        string getHelpInfo();

        /// <summary>
        /// ��ȡ�ö���Ĳ���������Ϣ
        /// </summary>
        /// <returns></returns>
        string getConfigInfo();

        /// <summary>
        /// ��Ҫ�����ֵ���ͬ�����ʱʹ��
        /// </summary>
        int Count{ get; set;}

        /// <summary>
        /// �ļ���Ϣ���
        /// </summary>
        EsFileInfo FileInfo { get; set;}
    }

    /// <summary>
    /// �ļ���Ϣ
    /// </summary>
    public class EsFileInfo
    {
        protected string theRecPath = @"c:\";
        /// <summary>
        /// ��ԴĿ¼
        /// </summary>
        public string RecPath { get { return theRecPath; } set { theRecPath = value; } }

        public string theFileName = "test.dll";
        public string FileName { get { return theFileName; } set { theFileName = value; } }

        protected Dictionary<string, string> theParaConfig = new Dictionary<string, string>();
        public Dictionary<string, string> ParaConfig { get { return theParaConfig; } set { theParaConfig = value; } }

    }

    /// <summary>
    /// �汾ʵ����
    /// </summary>
    public class VerHelper
    {
        #region ::::::::::::::::::::::::::::::::  interface fun ::::::::::::::::::::::::::::

        //  ::: IVerHelper      �汾˵���ӿ� 
        protected int theEsInterfaceVer = 0;
        public int EsInterfaceVerIndex { get { return theEsInterfaceVer; } }

        protected int theVerMainIndex = 0;
        public int VerMainIndex { get { return theVerMainIndex; } }

        protected int theVerSubIndex = 0;
        public int VerSubIndex { get { return theVerSubIndex; } }

        public void setVerIndex(int nVerMainIndex, int nVerSubIndex) { theVerMainIndex = nVerMainIndex; theVerSubIndex = nVerSubIndex; }
        public virtual string getVerIndex() { return VerMainIndex.ToString() + "." + VerSubIndex.ToString(); }
        public virtual string getVerInfo() { return "Test Class"; }
        public string getVerAllInfo() { return getVerInfo() + " " + getVerIndex(); }

        //  ::: IMemoHelper     ������������Ϣ�ӿ�
        public virtual string getMemoInfo() { return ""; }
        public virtual string getHelpInfo() { return "������Ϣ"; }
        public virtual string getConfigInfo() { return ""; }

        protected int theCount = 0;
        public int Count { get { return theCount; } set { theCount = value; } }

        protected EsFileInfo theFileInfo = new EsFileInfo();
        public EsFileInfo FileInfo { get { return theFileInfo; } set { theFileInfo = value; } }

        #endregion
    }
}
