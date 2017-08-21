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
        /// <summary> �����ļ�������·��</summary>
        public String ConfigFullPath { get { return theFullPath; } set { theFullPath = value; } }

        protected String theConfigFileName = "config.ini";
        /// <summary> �����ļ����ļ�����</summary>
        public String ConfigFileName { get { return theConfigFileName; } set { theConfigFileName = value; } }

        String theConfigDir = string.Empty;
        /// <summary> �����ļ���Ŀ¼</summary>
        public String ConfigDir { get { return theConfigDir; } set { theConfigDir = value; } }

        #endregion

        #region ::::::::::::::::::::::::::::::::  message    :::::::::::::::::::::::::::::::
        
        #endregion
    }

    /// <summary>
    /// �����ļ�������
    /// </summary>
    public class IniConfigHelper : BaseConfig
    {
        #region ::::::::::::::::::::::::::::::::  function  dir     ::::::::::::::::::::::::::::::::

        /// <summary>
        /// ���������ļ����ڵ�Ŀ¼
        /// </summary>
        /// <param name="cDir"></param>
        public void setConfigDir(string cDir)
        {
            theFullPath = Path.GetFullPath(cDir + "//" + theConfigFileName);
        }

        /// <summary>
        /// ���ļ���ȡ������Ϣ
        /// </summary>
        /// <param name="cPath">����·��</param>
        /// <returns></returns>
        public virtual bool loadConfigFromFile(string cFullPath, ref string cExMsg)
        {
            return false;
        }

        /// <summary>
        /// ���ļ���ȡ������Ϣ
        /// </summary>
        /// <param name="pathMustRight">�����������ļ�·��</param>
        /// <param name="cExMsg"></param>
        /// <returns></returns>
        public bool loadConfigFromFile(bool pathMustRight, ref string cExMsg)
        {
            if (pathMustRight)
            {
                var tmpDir = Path.GetDirectoryName(theFullPath);
                if (!Directory.Exists(tmpDir))
                {
                    cExMsg = "�����ļ���Ŀ¼����"; return false;
                }

                if (!File.Exists(theFullPath))
                {
                    cExMsg = "�����ļ�������"; return false;
                }
            }

            return loadConfigFromFile(theFullPath, ref cExMsg);
        }

        /// <summary>
        /// ������Ŀ¼�ж�ȡ�����ļ�
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
