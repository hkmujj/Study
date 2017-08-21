using System.Text;
using ES.Facility.PublicModule.Win32;

namespace ES.Facility.PublicModule.IO
{
    /// <summary>
    /// INI文件操作类
    /// </summary>
    public class IniHelper : IoHelper
    {
        #region ::::::::::::::::::::::::::::::::  attribute  ::::::::::::::::::::::::::::::::

        /// <summary>
        /// 能存储的最大字符长度
        /// </summary>
        public static int MaxLength { get; set; }

        #endregion

        #region ::::::::::::::::::::::::::::::::  structure  ::::::::::::::::::::::::::::::::
        public IniHelper() { }

        /// <summary>
        /// 写时需要使用
        /// </summary>
        /// <param name="paraStr"></param>
        public IniHelper(string paraStr) { LinkPath = paraStr; }

        static IniHelper()
        {
            MaxLength = 6000;
        }

        #endregion

        #region ::::::::::::::::::::::::::::::::   override  ::::::::::::::::::::::::::::::::

        public override T Select<T>(string keyStr, string keyName, string defaultValue = "NULL")
        {
            var strInfo = new StringBuilder();

            var i = Kernel.GetPrivateProfileString(keyStr, keyName, defaultValue, strInfo, MaxLength, LinkPath);

            return (T)((object)strInfo.ToString());
        }

        public override int Updata(string keyStr, string keyName, string value)
        {
            return Kernel.WritePrivateProfileString(keyStr, keyName, value, LinkPath);
        }

        public override int Inset(string keyStr, string keyName, string value)
        {
            return Kernel.WritePrivateProfileString(keyStr, keyName, value, LinkPath);
        }


        #endregion

    }
}
