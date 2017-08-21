using System;
using System.Collections.Generic;

namespace ES.Facility.PublicModule.BaseData
{
    /// <summary>
    /// 版本说明接口
    /// </summary>
    [CLSCompliant(false)]
    public interface IVerHelper
    {
        /// <summary>
        /// ES体系中的接口版本编号
        /// </summary>
        /// <remarks>对应支持的接口单元</remarks>
        int EsInterfaceVerIndex { get;}

        /// <summary>
        /// 主分類
        /// </summary>
        int VerMainIndex { get;}

        /// <summary>
        /// 次分類
        /// </summary>
        int VerSubIndex { get;}

        /// <summary>
        /// 设置分类信息
        /// </summary>
        /// <param name="nVerMainIndex"></param>
        /// <param name="nVerSubIndex"></param>
        void setVerIndex(int nVerMainIndex, int nVerSubIndex);

        /// <summary>
        /// 获取版本编号
        /// </summary>
        /// <returns></returns>
        string getVerIndex();

        /// <summary>
        /// 获取版本信息
        /// </summary>
        string getVerInfo();

        /// <summary>
        /// 获取完整版本信息
        /// </summary>
        /// <returns></returns>
        string getVerAllInfo();
    }

    /// <summary>
    /// 帮助及附带信息接口
    /// </summary>
    public interface IMemoHelper
    {
        /// <summary>
        /// 获取该对象的备注信息
        /// </summary>
        string getMemoInfo();

        /// <summary>
        /// 获取该对象的帮助信息
        /// </summary>
        /// <returns></returns>
        string getHelpInfo();

        /// <summary>
        /// 获取该对象的参数帮助信息
        /// </summary>
        /// <returns></returns>
        string getConfigInfo();

        /// <summary>
        /// 主要用于字典中同类计数时使用
        /// </summary>
        int Count{ get; set;}

        /// <summary>
        /// 文件信息相关
        /// </summary>
        EsFileInfo FileInfo { get; set;}
    }

    /// <summary>
    /// 文件信息
    /// </summary>
    public class EsFileInfo
    {
        protected string theRecPath = @"c:\";
        /// <summary>
        /// 资源目录
        /// </summary>
        public string RecPath { get { return theRecPath; } set { theRecPath = value; } }

        public string theFileName = "test.dll";
        public string FileName { get { return theFileName; } set { theFileName = value; } }

        protected Dictionary<string, string> theParaConfig = new Dictionary<string, string>();
        public Dictionary<string, string> ParaConfig { get { return theParaConfig; } set { theParaConfig = value; } }

    }

    /// <summary>
    /// 版本实现类
    /// </summary>
    public class VerHelper
    {
        #region ::::::::::::::::::::::::::::::::  interface fun ::::::::::::::::::::::::::::

        //  ::: IVerHelper      版本说明接口 
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

        //  ::: IMemoHelper     帮助及附带信息接口
        public virtual string getMemoInfo() { return ""; }
        public virtual string getHelpInfo() { return "帮助信息"; }
        public virtual string getConfigInfo() { return ""; }

        protected int theCount = 0;
        public int Count { get { return theCount; } set { theCount = value; } }

        protected EsFileInfo theFileInfo = new EsFileInfo();
        public EsFileInfo FileInfo { get { return theFileInfo; } set { theFileInfo = value; } }

        #endregion
    }
}
