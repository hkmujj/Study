/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：维护-No.8-密码设置
 *
 *-------------------------------------------------------------------------------------------------*/

using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using System.Drawing;

namespace SS4B_TMS.LCUInfo
{
    /// <summary>
    ///     功能描述：维护-No.8-密码设置
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V808MaintenancePasswordSetting : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
        }

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-密码设置";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }
    }
}