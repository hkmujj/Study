#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图107-运行-线路条件-No.0
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NC_TMS.运行
{
    /// <summary>
    /// 功能描述：视图107-运行-线路条件-No.0
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V107_Run_SpeedLimiting_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private String[] _str_SpeedLimitingList;//限速条件文本列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行视图-限速条件";
        }

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            this._str_SpeedLimitingList = new String[] { 
            "限速3km/h",
            "限速12km/h",
            "限速30km/h",
            "限速40km/h",
            "限速45km/h",
            "限速50km/h",
            "限速60km/h",
            "限速70km/h",
            "限速75km/h",
            "----",
            "--"};

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
            this.paint_Frame(dcGs);
            this.paint_Value(dcGs);
            base.paint(dcGs);
        }

        /// <summary>
        /// 线框与标题绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawString("限速条件提示", new Font("宋体", 11), Brushs.WhiteBrush, new RectangleF(312, 155, 102, 24), FontInfo.SF_CC);
            for (int i = 0; i < 11; i++)
            {
                dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(65 + (i % 2) * 329, 236 + 32 * (i / 2), 262, 32));
            }
        }

        /// <summary>
        /// 状态绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Value(Graphics dcGs)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (i == 1 && j == 5) continue;
                    if (VC_C2_GetValue.SpeedLimitingValues[j + i * 6])
                    {
                        dcGs.FillRectangle(Brushs.RedBrush, new Rectangle(65 + i * 329 + 1, 236 + 32 * j + 2, 261, 30));
                    }
                    dcGs.DrawString(_str_SpeedLimitingList[j + i * 6], new Font("宋体", 11), Brushs.WhiteBrush, new RectangleF(65 + i * 329, 236 + 32 * j + 3, 262, 32), FontInfo.SF_CC);
                }
            }
        }
        #endregion
    }
}
