#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图108-运行-旁路信息-No.0
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图108-运行-旁路信息-No.0
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V108_Run_DishRoad_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private String[] _str_DishRoad;//旁路信息文本列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行视图-旁路信息";
        }

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Images.Add(Image.FromStream(fs));
                }
            });

            this._str_DishRoad = new String[]  { 
            "Tc1警惕按钮旁路",
            "Tc1常用制动缓解旁路",
            "Tc1停放制动缓解旁路",
            "Tc1总风环路旁路",
            "Tc1安全环路旁路",
            "Tc1门关好旁路",
            "Tc1所有制动缓解旁路",
            "Tc1零速旁路",
            "Tc2警惕按钮旁路",
            "Tc2常用制动缓解旁路",
            "Tc2停放制动缓解旁路",
            "Tc2总风环路旁路",
            "Tc2安全环路旁路",
            "Tc2门关好旁路",
            "Tc2所有制动缓解旁路",
            "Tc2零速旁路"
            };

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
        /// 绘制线框与标题
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            String[] str = new String[] { "旁路投入", "旁路切除" };
            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawImage(_resource_Images[i], new Rectangle(30, 120 + 35 * i, 81, 31));
                dcGs.DrawString(str[i], new Font("宋体", 11), Brushs.WhiteBrush, new RectangleF(112, 120 + 35 * i, 81, 31), FontInfo.SF_LC);
            }
        }

        /// <summary>
        /// 绘制状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Value(Graphics dcGs)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BoolList[UIObj.InBoolList[j + i * 8]])
                    {
                        dcGs.DrawImage(_resource_Images[0], new Rectangle(220 + i * 250, 220-30 + j * 46, 81, 31));
                    }
                    else dcGs.DrawImage(_resource_Images[1], new Rectangle(220 + i * 250, 220-30 + j * 46, 81, 31));
                    dcGs.DrawString(this._str_DishRoad[j + i * 8], new Font("宋体", 11), Brushs.WhiteBrush, new RectangleF(51 + i * 250, 220-30 + j * 46, 169, 31), FontInfo.SF_RC);
                }
            }
        }
        #endregion
    }
}
