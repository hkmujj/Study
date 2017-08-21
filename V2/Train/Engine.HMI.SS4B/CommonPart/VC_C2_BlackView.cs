/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图0-黑屏-关闭时候
 *
 *-------------------------------------------------------------------------------------------------*/

using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using SS4B_TMS.Resource;
using System.Drawing;

namespace SS4B_TMS.CommonPart
{
    /// <summary>
    ///     功能描述：视图0-黑屏-关闭时候
    ///     创建人：lih
    ///     创建时间：2015-8-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VCC2BlackView : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            if (!GetInBoolValue(InBoolKeys.InB黑屏标志))
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
            }

            base.paint(dcGs);
        }

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "黑屏";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }
    }
}