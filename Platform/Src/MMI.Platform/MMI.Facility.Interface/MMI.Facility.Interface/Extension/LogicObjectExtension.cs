using System.Collections.Generic;
using System.Globalization;
using ES.Facility.PublicModule.Memo;
using ES.Facility.PublicModule.TypeTransition;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class LogicObjectExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStringPara(this IAppLogicConfig obj)
        {
            var tmpSourceStr = string.Empty;

            //index
            string tmpStr = "{";
            tmpStr += obj.Index.ToString(CultureInfo.InvariantCulture);
            tmpStr += "}";
            tmpSourceStr += tmpStr;

            //运算参与位
            tmpStr = "[~";
            tmpStr += string.Join(" ", obj.LeftDataList);
            tmpStr += "~]";
            tmpSourceStr += tmpStr;
            if (obj.LeftDataList.Count <= 0)
            {
                return string.Empty;
            }

            //运算输出位
            tmpStr = "[_";
            tmpStr += string.Join(" ", obj.RightDataList);
            tmpStr += "_]";
            tmpSourceStr += tmpStr;
            if (obj.RightDataList.Count <= 0)
            {
                return string.Empty;
            }

            //运算方式
            tmpStr = "<@";
            tmpStr += obj.LogicRunType.ToString();
            tmpStr += "@>";
            tmpSourceStr += tmpStr;

            //允许计算
            tmpStr = "<!";
            tmpStr += obj.Enable ? "1" : "0";
            tmpStr += "!>";
            tmpSourceStr += tmpStr;

            //备注信息
            tmpStr = "<#";
            tmpStr += obj.Memo;
            tmpStr += "#>";
            tmpSourceStr += tmpStr;

            return tmpSourceStr;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nType"></param>
        /// <param name="nPara"></param>
        /// <returns></returns>
        public static string GetParaStringByType(int nType, string nPara)
        {
            if (nType == 2)
            {
                return "[~" + nPara + "~]";
            }
            else if (nType == 4)
            {
                return "[_" + nPara + "_]";
            }
            else if (nType == 3)
            {
                return "<@" + nPara + "@>";
            }
            else if (nType == 1)
            {
                return "<!" + nPara + "!>";
            }
            else if (nType == 5)
            {
                return "<#" + nPara + "#>";
            }
            else if (nType == 0)
            {
                return "{" + nPara + "}";
            }

            return string.Empty;
        }

        /// <summary>
        /// 按字符初始化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cPara"></param>
        /// <returns></returns>
        public static bool InitParaFromString(this LogicObject obj, string cPara)
        {
            var tmpSourceStr = cPara;
            tmpSourceStr = tmpSourceStr.Replace('\t', ' ');
            tmpSourceStr = tmpSourceStr.Trim();

            var tmpStr = string.Empty;

            List<string> tmpStringList;

            int tmpInt;
            var tmpRightIndex = 0;

            //运算参与位
            obj.LeftDataList.Clear();
            if (StringHelper.findStringByKey(tmpSourceStr, "[~", "~]", ref tmpStr))
            {
                tmpStringList = StringHelper.getValueBySpace(tmpStr.Trim(), " ");

                foreach (var tmpstr in tmpStringList)
                {
                    if (int.TryParse(tmpstr, out tmpInt))
                    {
                        obj.LeftDataList.Add(tmpInt);
                    }
                }

                tmpRightIndex++;
            }

            //运算输出位
            obj.RightDataList.Clear();
            if (StringHelper.findStringByKey(tmpSourceStr, "[_", "_]", ref tmpStr))
            {
                tmpStringList = StringHelper.getValueBySpace(tmpStr.Trim(), " ");

                foreach (var tmpstr in tmpStringList)
                {
                    if (int.TryParse(tmpstr, out tmpInt))
                    {
                        obj.RightDataList.Add(tmpInt);
                    }
                }

                tmpRightIndex++;
            }

            //运算方式
            if (StringHelper.findStringByKey(tmpSourceStr, "<@", "@>", ref tmpStr))
            {
                tmpStringList = StringHelper.getValueBySpace(tmpStr.Trim(), " ");
                if (tmpStringList.Count > 0)
                {
                    var et = new EnumTransition();
                    try
                    {
                        obj.LogicRunType = et.strToEnum<LogicType>(tmpStringList[0]);
                        tmpRightIndex++;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            //允许计算
            if (StringHelper.findStringByKey(tmpSourceStr, "<!", "!>", ref tmpStr))
            {
                tmpStringList = StringHelper.getValueBySpace(tmpStr.Trim(), " ");
                if (tmpStringList.Count > 0)
                {
                    if (int.TryParse(tmpStringList[0], out tmpInt))
                    {
                        obj.Enable = tmpInt == 1;
                        tmpRightIndex++;
                    }
                }
            }

            //备注信息
            if (StringHelper.findStringByKey(tmpSourceStr, "{", "}", ref tmpStr))
            {
                if (int.TryParse(tmpStr, out tmpInt))
                {
                    obj.Index = tmpInt;
                    tmpRightIndex++;
                }
            }

            //备注信息
            if (StringHelper.findStringByKey(tmpSourceStr, "<#", "#>", ref tmpStr))
            {
                obj.Memo = tmpStr.Trim();
            }
            if (tmpRightIndex == 5)
            {
                return true;
            }

            return false;


        }
    }
}
