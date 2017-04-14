using ES.Facility.PublicModule.Memo;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class UIObjectExtension
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cPara"></param>
        /// <returns></returns>
        public static bool InitParaFromString(this UIObject obj, string cPara)
        {
            if (!obj.Info.InitParaFromString(cPara))
            {
                return false;
            }     //转为基本数据的时候已经失败

            int tmpInt;

            //转视图
            obj.ViewList.Clear();
            var tmpStringList = StringHelper.getValueBySpace(obj.Info.ViewInfo, " ");
            foreach (var tmpstr in tmpStringList)
            {
                int.TryParse(tmpstr, out tmpInt);
                obj.ViewList.Add(tmpInt);
            }

            //转输入Bool
            obj.InBoolList.Clear();
            tmpStringList = StringHelper.getValueBySpace(obj.Info.InputBoolIndexs, " ");
            foreach (var tmpstr in tmpStringList)
            {
                int.TryParse(tmpstr, out tmpInt);
                obj.InBoolList.Add(tmpInt);
            }

            //转输出Bool
            obj.OutBoolList.Clear();
            tmpStringList = StringHelper.getValueBySpace(obj.Info.OutputBoolIndexs, " ");
            foreach (var tmpstr in tmpStringList)
            {
                int.TryParse(tmpstr, out tmpInt);
                obj.OutBoolList.Add(tmpInt);
            }

            //转输入Float
            obj.InFloatList.Clear();
            tmpStringList = StringHelper.getValueBySpace(obj.Info.InputFloatIndexs, " ");
            foreach (var tmpstr in tmpStringList)
            {
                int.TryParse(tmpstr, out tmpInt);
                obj.InFloatList.Add(tmpInt);
            }

            //转输出Float
            obj.OutFloatList.Clear();
            tmpStringList = StringHelper.getValueBySpace(obj.Info.OutputFloatIndexs, " ");
            foreach (var tmpstr in tmpStringList)
            {
                int.TryParse(tmpstr, out tmpInt);
                obj.OutFloatList.Add(tmpInt);
            }

            //
            obj.ParaList.Clear();
            tmpStringList = StringHelper.getValueBySpace(obj.Info.ParaIndexs, " ");
            foreach (var tmpstr in tmpStringList)
            {
                obj.ParaList.Add(tmpstr);
            }

            obj.MainIndex = obj.Info.MainIndex;
            obj.ClassName = obj.Info.ClassName;

            return true;
        }

    }
}
