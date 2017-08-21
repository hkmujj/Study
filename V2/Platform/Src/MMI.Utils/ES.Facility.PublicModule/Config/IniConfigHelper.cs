using System;
using System.IO;
using System.Windows.Forms;
using ES.Facility.PublicModule.IO;

namespace ES.Facility.PublicModule.Config
{
    public class BaseConfig
    {
        #region ::::::::::::::::::::::::::::::::  function  dir     ::::::::::::::::::::::::::::::::

        public void setDirInfo(string cDir)
        {
            setDirInfo(cDir, theConfigFileName);
        }
        
        public void setDirInfo(string cDir, string cFileName)
        {
            theConfigDir = cDir;
            theConfigFileName = cFileName;

            theFullPath = cDir + "\\" + cFileName;
        }
        #endregion


        #region ::::::::::::::::::::::::::::::::  variable   :::::::::::::::::::::::::::::::

        protected String theFullPath = string.Empty;
        /// <summary> 配置文件的完整路径</summary>
        public String ConfigFullPath { get { return theFullPath; } set { theFullPath = value; } }

        protected String theConfigFileName = "config.ini";
        /// <summary> 配置文件的文件名称</summary>
        public String ConfigFileName { get { return theConfigFileName; } set { theConfigFileName = value; } }

        String theConfigDir = string.Empty;
        /// <summary> 配置文件的目录</summary>
        public String ConfigDir { get { return theConfigDir; } set { theConfigDir = value; } }

        #endregion

        #region ::::::::::::::::::::::::::::::::  message    :::::::::::::::::::::::::::::::
        
        #endregion
    }

    /// <summary>
    /// 配置文件操作类
    /// </summary>
    public class IniConfigHelper : BaseConfig
    {
        #region ::::::::::::::::::::::::::::::::  function  dir     ::::::::::::::::::::::::::::::::

        /// <summary>
        /// 设置配置文件所在的目录
        /// </summary>
        /// <param name="cDir"></param>
        public void setConfigDir(string cDir)
        {
            theFullPath = Path.GetFullPath(cDir + "//" + theConfigFileName);
        }

        /// <summary>
        /// 从文件读取配置信息
        /// </summary>
        /// <param name="cPath">完整路径</param>
        /// <returns></returns>
        public virtual bool loadConfigFromFile(string cFullPath, ref string cExMsg)
        {
            return false;
        }

        /// <summary>
        /// 从文件读取配置信息
        /// </summary>
        /// <param name="pathMustRight">必须检查配置文件路径</param>
        /// <param name="cExMsg"></param>
        /// <returns></returns>
        public bool loadConfigFromFile(bool pathMustRight, ref string cExMsg)
        {
            if (pathMustRight)
            {
                var tmpDir = Path.GetDirectoryName(theFullPath);
                if (!Directory.Exists(tmpDir))
                {
                    cExMsg = "配置文件的目录错误"; return false;
                }

                if (!File.Exists(theFullPath))
                {
                    cExMsg = "配置文件不存在"; return false;
                }
            }

            return loadConfigFromFile(theFullPath, ref cExMsg);
        }

        /// <summary>
        /// 从运行目录中读取配置文件
        /// </summary>
        /// <returns></returns>
        public bool loadConfigFromDefualtFile(ref string cExMsg)
        {
            setConfigDir(Path.GetDirectoryName(Application.ExecutablePath));
            return loadConfigFromFile(theFullPath, ref cExMsg);
        }

        protected virtual void initConfigFile(string cFullPath)
        {

        }

        protected bool getConfigValueByName(string cFullPath, string cMainName, string cName, ref string cOutValue)
        {
            var ih = new IniHelper(cFullPath);
            cOutValue = ih.Select<string>(cMainName, cName);
            return cOutValue == "NULL" ? false : true;
        }

        #endregion

        #region ::::::::::::::::::::::::::::::::  variable   :::::::::::::::::::::::::::::::

        #endregion
    }
}
