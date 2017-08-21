#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-10
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图7-公共视图-线框
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图7-公共视图-线框
    /// 创建人：唐林
    /// 创建时间：2014-7-10
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V7_Check_Common_Frame : baseClass
    {
        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-公共组件-线框";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
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
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(-1, 96, 801, 454));
            base.paint(dcGs);
        }
        #endregion
    }
}
