using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectBaseExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetSerializteString(this IObjectBase obj)
        {
            var tmpString = string.Empty;
            var isRightIndex = 0;
            tmpString += '\t';
            //类型名
            tmpString += obj.GetTypeName() + '\t';

            //idnex
            tmpString += BaseObjectInfoExtension.FirstKeyWord(ObjectKeyWord.index);
            tmpString += obj.MainIndex;
            tmpString += BaseObjectInfoExtension.EndKeyWord(ObjectKeyWord.index);
            tmpString += '\t';

            //从属视图
            List<int> tmpListIntValue;
            {
                tmpListIntValue = obj.UIObj.ViewList;
                isRightIndex++;
                tmpString += BaseObjectInfoExtension.FirstKeyWord(ObjectKeyWord.form);
                tmpString = tmpListIntValue.Aggregate(tmpString, (current, tempInt) => current + ( " " + tempInt.ToString(CultureInfo.InvariantCulture) + " " ));
                tmpString += BaseObjectInfoExtension.EndKeyWord(ObjectKeyWord.form);
            }
            tmpString += '\t';

            //输入_Bool_对应表
            {
                tmpListIntValue = obj.UIObj.InBoolList;
                isRightIndex++;
                tmpString += BaseObjectInfoExtension.FirstKeyWord(ObjectKeyWord.inBool);
                tmpString = tmpListIntValue.Aggregate(tmpString, (current, tempInt) => current + (" " + tempInt.ToString() + " "));
                tmpString += BaseObjectInfoExtension.EndKeyWord(ObjectKeyWord.inBool);
            }
            tmpString += '\t';


            //输入_Float_对应表
            {
                tmpListIntValue = obj.UIObj.InFloatList;
                isRightIndex++;
                tmpString += BaseObjectInfoExtension.FirstKeyWord(ObjectKeyWord.inFloat);
                tmpString = tmpListIntValue.Aggregate(tmpString, (current, tempInt) => current + ( " " + tempInt.ToString() + " " ));
                tmpString += BaseObjectInfoExtension.EndKeyWord(ObjectKeyWord.inFloat);
            }
            tmpString += '\t';

            //输出_bool_对应表
            {
                tmpListIntValue = obj.UIObj.OutBoolList;
                isRightIndex++;
                tmpString += BaseObjectInfoExtension.FirstKeyWord(ObjectKeyWord.outBool);
                tmpString = tmpListIntValue.Aggregate(tmpString, (current, tempInt) => current + ( " " + tempInt.ToString() + " " ));
                tmpString += BaseObjectInfoExtension.EndKeyWord(ObjectKeyWord.outBool);
            }
            tmpString += '\t';

            //输出_Float_对应表
            {
                tmpListIntValue = obj.UIObj.OutFloatList;
                isRightIndex++;
                tmpString += BaseObjectInfoExtension.FirstKeyWord(ObjectKeyWord.outFloat);
                tmpString = tmpListIntValue.Aggregate(tmpString, (current, tempInt) => current + ( " " + tempInt.ToString() + " " ));
                tmpString += BaseObjectInfoExtension.EndKeyWord(ObjectKeyWord.outFloat);
            }
            tmpString += '\t';

            //参数表
            {
                var tmpListStrValue = obj.UIObj.ParaList;
                isRightIndex++;
                tmpString += BaseObjectInfoExtension.FirstKeyWord(ObjectKeyWord.para);
                tmpString = tmpListStrValue.Aggregate(tmpString, (current, tempStr) => current + (" " + tempStr.Trim() + " "));
                tmpString += BaseObjectInfoExtension.EndKeyWord(ObjectKeyWord.para);
            }
            return isRightIndex == 6 ? tmpString : string.Empty;
        }
    }
}
