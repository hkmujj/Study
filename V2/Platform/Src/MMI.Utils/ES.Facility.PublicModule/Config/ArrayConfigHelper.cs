using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ES.Facility.PublicModule.IO;

namespace ES.Facility.PublicModule.Config
{
    /// <summary>
    /// 不定长配置文件
    /// </summary>
    public class ArrayConfigHelper : BaseConfig
    {
        #region ::::::::::::::::::::::::::::::::  function  dir     ::::::::::::::::::::::::::::::::

        public int readCustFile() { return readCustFile(theFullPath); }

        public int readCustFile(string cPath) { return readCustFile(cPath, Encoding.Default); }

        /// <summary>
        /// 读取指定的文件
        /// </summary>
        /// <param name="cPath"></param>
        /// <param name="nEncode"></param>
        /// <returns>-1: 文件不存在, -10:初始化失败, >0:读取有效行数</returns>
        public int readCustFile(string cPath, Encoding nEncode)
        {
            if (!FileHelper.GetCurFileA(cPath))
            {
                return -1;      //文件不存在
            }
            else
            {
                if (!assReadBeforeInit()) return -10;   //初始化失败

                string[] tmpStrArry;
                var tmpString = string.Empty;

                tmpStrArry = File.ReadAllLines(cPath, nEncode);
                var deCodeOut = new List<string>(); //解码输出使用的数组，存储按空格或者关键子区分的数据

                var tmpIndex = 0;
                for (var i = 0; i < tmpStrArry.Length; i++)
                {
                    tmpString = tmpStrArry[i].Replace('\t', ' ');
                    tmpString = tmpString.Trim();

                    deCodeOut.Clear();

                    if (tmpString.Length > 0 && (tmpString[0] != theNeglectKey[0]))
                    {
                        if (deCode(tmpString, ref tmpIndex, ref deCodeOut)) append_deCodeConfig(deCodeOut);
                    }
                }

                return 0;
            }
        }

        /// <summary>
        /// 在读取数据前，初始化调用
        /// </summary>
        /// <returns></returns>
        public virtual bool assReadBeforeInit() { return true; }

        public static bool deCodeFromString(string cStr, ref int pCount, ref List<String> cCodeOut)
        {
            cCodeOut.Clear();
            pCount = 0;
            var pevIsNotSpace = true;      //需要过滤掉连续分割符

            var tmpStr = "";
            var index = 0;
            var nkey = 32;

            var slength = cStr.ToCharArray().Length;
            if (slength > 0)
            {
                tmpStr = "";
                index = 0;

                for (var i = 0; i < slength; i++)
                {
                    //if (cStr[i] == 9) cStr[i] = nkey;       //过滤制表符
                    if (cStr[i] != nkey && i < slength - 1)
                    {
                        tmpStr += cStr[i].ToString();
                        pevIsNotSpace = true;
                    }
                    else if (cStr[i] == nkey || i == slength - 1)
                    {
                        if (!(i == 0 && cStr[i] == nkey) && pevIsNotSpace)
                        {
                            if (cStr[i] != nkey) tmpStr += cStr[i].ToString();

                            if (cCodeOut.Count >= (pCount + 1)) cCodeOut[pCount] = tmpStr; else cCodeOut.Add(tmpStr);

                            tmpStr = "";
                            index++; pCount++;
                            pevIsNotSpace = cStr[i] == nkey ? false : true;
                        }
                        else if (i == slength - 1)
                        {
                            if (cStr[i] != nkey && pevIsNotSpace)
                            {
                                tmpStr += cStr[i].ToString();
                            }
                            else if (cStr[i] != nkey && (!pevIsNotSpace))
                            {
                                tmpStr = "";
                                tmpStr += cStr[i].ToString();
                                if (cCodeOut.Count >= (pCount + 1)) cCodeOut[pCount] = tmpStr; else cCodeOut.Add(tmpStr);

                                index++; pCount++; pevIsNotSpace = true;
                            }
                        }
                    }
                }

                if (!pevIsNotSpace) pCount--;

                if (pCount > 0) return true;
            }

            return false;
        }
        public virtual bool deCode(string cStr, ref int pCount, ref List<String> cCodeOut)
        {
            return deCodeFromString(cStr, ref pCount, ref cCodeOut);            
        }

        #endregion

        #region ::::::::::::::::::::::::::::::::  message    :::::::::::::::::::::::::::::::
        public delegate void deCodeConfig(List<String> deCodeArr);
        public event deCodeConfig OnDeCodeConfig;
        protected void append_deCodeConfig(List<String> deCodeArr)
        {
            if (OnDeCodeConfig != null) OnDeCodeConfig(deCodeArr);
        }

        #endregion


        #region ::::::::::::::::::::::::::::::::  variable   :::::::::::::::::::::::::::::::

        private string theNeglectKey = "#";
        /// <summary>
        /// 忽略字,该字一般放置在行头
        /// </summary>
        public string NeglectKey { get { return theNeglectKey; } set { theNeglectKey = value; } }
        
        #endregion

        #region ::::::::::::::::::::::::::::::::  define     :::::::::::::::::::::::::::::::
        public ArrayConfigHelper()
        {
            theConfigFileName = "config.gks";
        }
        #endregion
    }
}
