using System.Collections.Generic;
using MMI.Facility.DataType;

namespace HXD1D
{
    public static class FindImgClass
    {
        /// <summary>
        /// 查找图片
        /// </summary>
        /// <param name="type">类型 0代表牵引 1代表 制动</param>
        /// <param name="predictIndex">预定牵引、制动百分比索引</param>
        /// <param name="currentIndex">实际牵引、制动百分比索引</param>
        /// <param name="objeClass">需要查找的类</param>
        /// <param name="floatId">输入Floaf的索引集合</param>
        /// <returns></returns>
        public static int FindImgIndex(int type, int predictIndex, int currentIndex, baseClass objeClass,List<int> floatId)
        {
            int imgIndex = 0;
            switch (type)
            {
                case 0:
                    imgIndex = objeClass.FloatList[floatId[predictIndex]] > objeClass.FloatList[floatId[currentIndex]] ? 2 : 3;
                    break;
                case 1:
                    imgIndex = objeClass.FloatList[floatId[predictIndex]] > objeClass.FloatList[floatId[currentIndex]] ? 5 : 4;
                    break;
            }
            return imgIndex;
        }
    }
}
