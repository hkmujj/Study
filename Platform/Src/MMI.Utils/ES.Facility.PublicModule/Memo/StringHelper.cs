using System.Collections.Generic;

namespace ES.Facility.PublicModule.Memo
{
    public class StringHelper
    {
        /// <summary>
        /// 获取指定2种字符之间的字符信息
        /// </summary>
        /// <param name="cSoure">原始字符</param>
        /// <param name="cFirst">起点字符</param>
        /// <param name="cEnd">结束字符</param>
        /// <returns></returns>
        public static bool findStringByKey(string cSoure, string cFirst, string cEnd, ref string outStr)
        {
            var beginIndex = 0;                 //记录起点位置
            var endIndex = cSoure.Length - 1;   //记录结束位置

            if (cFirst != string.Empty)
            {
                //有起点字符的情况下 否则认为从0位开始
                beginIndex = cSoure.IndexOf(cFirst);
                if (beginIndex < 0) return false;       //不存在该起点字符

                beginIndex += cFirst.Length;
                if (beginIndex > cSoure.Length - 1) return false;   //该字符已经是最后一位了
            }

            if (cEnd != string.Empty)
            {
                //有结束字符的情况下 否则认为是末尾
                endIndex = cSoure.IndexOf(cEnd, beginIndex);
                if (endIndex < 0) return false;             //不存在该结束字符
            }

            if (beginIndex == endIndex)
            {
                //头字符即是尾字符
                return false;
            }

            outStr = cSoure.Substring(beginIndex, endIndex - beginIndex);

            if (outStr != null && outStr != string.Empty) return true;

            return false;
        }

        /// <summary>
        /// 按key分割出数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cSoure"></param>
        /// <param name="outList"></param>
        /// <returns></returns>
        public static List<string> getValueBySpace(string cSoure, string cKey)
        {
            var outList = new List<string>();

            var tmpStr = cSoure.Trim();
            var tmpValue = string.Empty;

            var beginIndex = 0;
            var endIndex = tmpStr.Length - 1;           

            while (beginIndex < tmpStr.Length)
            {
                endIndex = tmpStr.IndexOf(cKey);

                if (endIndex < 0)
                {
                    //没有空格存在，即该字符都为有效字符
                    outList.Add(tmpStr);
                    break;
                }

                //获取指定区间的数据
                tmpValue = tmpStr.Substring(beginIndex, endIndex);
                outList.Add(tmpValue);

                //获取剩余区间的数据
                tmpStr = tmpStr.Substring(endIndex);
                tmpStr = tmpStr.Trim();
                beginIndex = 0;
                endIndex = tmpStr.Length;

            }

            return outList;
        }
    }
}
