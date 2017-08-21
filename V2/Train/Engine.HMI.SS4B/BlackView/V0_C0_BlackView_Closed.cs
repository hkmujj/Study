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
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Drawing;

namespace SS4B_TMS.BlackView
{
    /// <summary>
    ///     功能描述：视图0-黑屏-关闭时候
    ///     创建人：lih
    ///     创建时间：2015-8-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V0C0BlackViewClosed : baseClass
    {
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

        private int m_ShowTimes;

        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            if (GetInBoolValue(InBoolKeys.InB黑屏标志))
            {
                if (10 >= m_ShowTimes++)
                {
                    CommonStatus.IsBlackScreen = true;
                    dcGs.DrawImage(ImageResource.start, 0, 0, 800, 600);
                }
                else
                {
                    m_ShowTimes = 0;
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                }
            }

            base.paint(dcGs);
        }
    }
}