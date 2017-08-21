#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图0-黑屏-关闭时候
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace LightRail.Ethiopia.TMS.Pub
{
    /// <summary>
    /// 功能描述：视图0-黑屏-关闭时候
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C5_BlackView : baseClass
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
            return true;
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            if (!BoolList[UIObj.InBoolList[0]])
                append_postCmd(CmdType.ChangePage, 0, 0, 0);

            base.paint(dcGs);
        }
        #endregion
    }
}
