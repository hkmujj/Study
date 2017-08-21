#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-9
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图5-通讯状态-No.0-主界面
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
    /// 功能描述：视图5-通讯状态-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-9
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V5_Message_C0_MainView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "通讯状态视图-主界面";
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

            return true;
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            this.paint_Frame(dcGs);
            this.paint_REP(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 线框绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(5, 96, 796, 450));

            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(124, 177), new PointF(680, 177));

            //8根丛线
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(95, 210), new PointF(95, 500));
            for (int i = 0; i < 6; i++)
            {
                if (i == 0 || i == 5)
                    dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(165 + 95 * i, 177), new PointF(165 + 95 * i, 195));
                else
                    dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(165 + 95 * i, 177), new PointF(165 + 95 * i, 500));
            }
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(704, 210), new PointF(704, 500));

            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(95, 210), new PointF(704, 210));
        }

        /// <summary>
        /// REP状态绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_REP(Graphics dcGs)
        {
            for (int trainID = 0; trainID < 6; trainID++)
            {
                Int32 index = 2;
                for (int state = 0; state < 3; state++)
                {
                    if (BoolList[UIObj.InBoolList[trainID] + state])
                    {
                        index = state;
                        break;
                    }
                }

                dcGs.DrawImage(this._resource_Images[index], new RectangleF(135 + 95 * trainID, 195, 58, 29));
                dcGs.DrawString("REP", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(135 + 95 * trainID, 195, 58, 29), FontInfo.SF_CC);
            }
        }
        #endregion
    }
}
