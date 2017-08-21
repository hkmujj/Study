#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图106-运行-列表逻辑-No.0
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图106-运行-列表逻辑-No.0
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V106_Run_TrainLogic_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private String[] _str_TrainLogicList;//列车逻辑列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行视图-列车逻辑";
        }

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            this._str_TrainLogicList = new String[] { 
            "State紧急制动状态",
            "State列车司机室已激活",
            "State列车模式已选择",
            "State列车方向已选择",
            "State牵引允许",
            "备用",
            "备用",
            "备用",
            "备用",
            "备用",
            "备用",
            "备用"};

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
            dcGs.DrawString("列车逻辑提示", new Font("宋体", 11), Brushs.WhiteBrush, new RectangleF(312, 155, 102, 24), FontInfo.SF_CC);
            for (int i = 0; i < 12; i++)
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
                    if (VC_C2_GetValue.TrainLogicValues[j + i * 6])
                    {
                        dcGs.FillRectangle(Brushs.RedBrush, new Rectangle(65 + i * 329 + 1, 236 + 32 * j + 2, 261, 30));
                    }
                    dcGs.DrawString(_str_TrainLogicList[j + i * 6], new Font("宋体", 11), Brushs.WhiteBrush, new RectangleF(65 + i * 329, 236 + 32 * j + 3, 262, 32), FontInfo.SF_CC);
                }
            }
        }
        #endregion
    }
}
