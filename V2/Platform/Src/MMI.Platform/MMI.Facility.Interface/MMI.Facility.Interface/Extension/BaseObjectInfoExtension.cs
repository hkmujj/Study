using ES.Facility.PublicModule.Memo;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class BaseObjectInfoExtension
    {
        #region ::::::::::::::::::::: function :::::::::::::::::::::::::::

        /// <summary>
        /// 初始化原始对象数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cPara"></param>
        /// <returns>初始化是否成功</returns>
        public static bool InitParaFromString(this baseObjectInfo obj, string cPara)
        {
            var tmpSouceStr = cPara;
            var tmpStr = string.Empty;

            //考虑到填表的人可能会用excel等工具转为文本，那么将以制表符进行分割
            tmpSouceStr = tmpSouceStr.Replace('\t', ' ');
            tmpSouceStr = tmpSouceStr.Trim();

            //引用类名
            if (StringHelper.findStringByKey(tmpSouceStr, string.Empty, " ", ref tmpStr))
            {
                obj. ClassName = tmpStr.Trim();

                if (obj.ClassName.Length <= 0)
                {
                    return false;
                }
            }
            else { return false; }

            //从属视图
            obj.ViewInfo = string.Empty;
            if (StringHelper.findStringByKey(tmpSouceStr, FirstKeyWord(ObjectKeyWord.form), EndKeyWord(ObjectKeyWord.form), ref tmpStr))
            {
                obj.ViewInfo = tmpStr.Trim();
            }

            //输入_Bool_对应表
            obj.InputBoolIndexs = string.Empty;
            if (StringHelper.findStringByKey(tmpSouceStr, FirstKeyWord(ObjectKeyWord.inBool), EndKeyWord(ObjectKeyWord.inBool), ref tmpStr))
            {
                obj.InputBoolIndexs = tmpStr.Trim();
            }

            //输入_Float_对应表
            obj.InputFloatIndexs = string.Empty;
            if (StringHelper.findStringByKey(tmpSouceStr, FirstKeyWord(ObjectKeyWord.inFloat), EndKeyWord(ObjectKeyWord.inFloat), ref tmpStr))
            {
                obj.InputFloatIndexs = tmpStr.Trim();
            }

            //输出_bool_对应表
            obj.OutputBoolIndexs = string.Empty;
            if (StringHelper.findStringByKey(tmpSouceStr, FirstKeyWord(ObjectKeyWord.outBool), EndKeyWord(ObjectKeyWord.outBool), ref tmpStr))
            {
                obj.OutputBoolIndexs = tmpStr.Trim();
            }

            //输出_Float_对应表
            obj.OutputFloatIndexs = string.Empty;
            if (StringHelper.findStringByKey(tmpSouceStr, FirstKeyWord(ObjectKeyWord.outFloat), EndKeyWord(ObjectKeyWord.outFloat), ref tmpStr))
            {
                obj.OutputFloatIndexs = tmpStr.Trim();
            }

            //参数表
            obj.ParaIndexs = string.Empty;
            if (StringHelper.findStringByKey(tmpSouceStr, FirstKeyWord(ObjectKeyWord.para), EndKeyWord(ObjectKeyWord.para), ref tmpStr))
            {
                obj.ParaIndexs = tmpStr.Trim();
            }

            //编号
            if (StringHelper.findStringByKey(tmpSouceStr, FirstKeyWord(ObjectKeyWord.index), EndKeyWord(ObjectKeyWord.index), ref tmpStr))
            {
                int idx;
                if (!int.TryParse(tmpStr, out idx))
                {
                    idx = -1;
                }
                obj.MainIndex = idx;
            }

            return true;
        }
        #endregion

        #region ::::::::::::::::::::: fuc string :::::::::::::::::::::::::
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nDkw"></param>
        /// <returns></returns>
        public static string GetFirstKeyWord(this baseObjectInfo obj, ObjectKeyWord nDkw)
        {
            return FirstKeyWord(nDkw);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nDkw"></param>
        /// <returns></returns>
        public static string FirstKeyWord(ObjectKeyWord nDkw)
        {
            switch (nDkw)
            {
                case ObjectKeyWord.form :
                    return "<";
                case ObjectKeyWord.inBool :
                    return "[#";
                case ObjectKeyWord.inFloat :
                    return "[*";
                case ObjectKeyWord.outBool :
                    return "[-";
                case ObjectKeyWord.outFloat :
                    return "[+";
                case ObjectKeyWord.para :
                    return "{";
                case ObjectKeyWord.index :
                    return "[~";
                case ObjectKeyWord.objectName :
                    return string.Empty;
                default :
                    return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nDkw"></param>
        /// <returns></returns>
        public static string GetEndKeyWord(this baseObjectInfo obj, ObjectKeyWord nDkw)
        {
            return EndKeyWord(nDkw);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nDkw"></param>
        /// <returns></returns>
        public static string EndKeyWord(ObjectKeyWord nDkw)
        {
            switch (nDkw)
            {
                case ObjectKeyWord.form :
                    return ">";
                case ObjectKeyWord.inBool :
                    return "#]";
                case ObjectKeyWord.inFloat :
                    return "*]";
                case ObjectKeyWord.outBool :
                    return "-]";
                case ObjectKeyWord.outFloat :
                    return "+]";
                case ObjectKeyWord.para :
                    return "}";
                case ObjectKeyWord.index :
                    return "~]";
                case ObjectKeyWord.objectName :
                    return " ";
                default :
                    return string.Empty;
            }
        }

        #endregion
    }
}
