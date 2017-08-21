#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-9
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.3-获取故障信息
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace LightRail.Ethiopia.TMS.Pub
{
    /// <summary>
    /// 功能描述：公共组件-No.3-获取故障信息
    /// 创建人：唐林
    /// 创建时间：2014-7-9
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C2_GetValue : baseClass
    {

        public static Boolean[] DirectionValues = new Boolean[12];
        public static Boolean[] TrainLogicValues = new Boolean[12];
        public static Boolean[] SpeedLimitingValues = new Boolean[11];

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-获取故障";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            for (int i = 0; i < 12; i++)
            {
                DirectionValues[i] = BoolList[UIObj.InBoolList[0] + i];
                TrainLogicValues[i] = BoolList[UIObj.InBoolList[1] + i];
                if (i < 11) SpeedLimitingValues[i] = BoolList[UIObj.InBoolList[2] + i];
            }

            base.paint(dcGs);
        }
        #endregion
    }
}
