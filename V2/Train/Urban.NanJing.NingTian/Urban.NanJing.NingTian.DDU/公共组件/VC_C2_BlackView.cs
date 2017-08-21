#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图0-黑屏-关闭时候
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Drawing;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.NanJing.NingTian.DDU.公共组件
{
    /// <summary>
    /// 功能描述：视图0-黑屏-关闭时候
    /// 创建人：唐林
    /// 创建时间：2014-7-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class VC_C2_BlackView : baseClass
    {
        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "黑屏";
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);
            return true;
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            if (!BoolList[UIObj.InBoolList[0]])
                append_postCmd(CmdType.ChangePage, 0, 0, 0);

            base.paint(g);
        }
        #endregion
    }
}
