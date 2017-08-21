#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-24
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图4-通讯状态-No.0-主界面（包括REF与EDCU状态绘制）
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.通讯状态
{
    /// <summary>
    /// 功能描述：视图4-通讯状态-No.0-主界面（包括REF与EDCU状态绘制）
    /// 创建人：唐林
    /// 创建时间：2014-7-24
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class V4_Message_C0_MainView : baseClass
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            paint_Frame(g);
            paint_REP(g);
            paint_EDCU(g);

            base.paint(g);
        }

        /// <summary>
        /// 线框绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            //上线框
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));
            g.DrawImage(_resource_Images[0], new Point(0, 108));//背景

            //横线
            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(170, 169, 152)), 3), new PointF(95, 155), new PointF(680, 155));

            //丛线
            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(170, 169, 152)), 3), new PointF(148, 155), new PointF(148, 470));
            g.DrawLines(new Pen(new SolidBrush(Color.FromArgb(170, 169, 152)), 3), new PointF[] { new PointF(148 + 152.33f, 155), new PointF(148 + 152.33f, 185), new PointF(352, 185), new PointF(352, 470) });
            g.DrawLines(new Pen(new SolidBrush(Color.FromArgb(170, 169, 152)), 3), new PointF[] { new PointF(148 + 152.33f * 2, 155), new PointF(148 + 152.33f * 2, 185), new PointF(401, 185), new PointF(401, 470) });
            g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(170, 169, 152)), 3), new PointF(148 + 152.33f * 3, 155), new PointF(148 + 152.33f * 3, 470));
        }

        /// <summary>
        /// REP状态绘制              
        /// </summary>
        /// <param name="g"></param>
        private void paint_REP(Graphics g)
        {
            for (int trainID = 0; trainID < 4; trainID++)
            {
                int index = 2;
                for (int state = 0; state < 3; state++)
                {
                    if (BoolList[UIObj.InBoolList[trainID] + state])
                    {
                        index = state;
                        break;
                    }
                }

                g.DrawImage(_resource_Images[index + 1], new RectangleF(119 + 152.33f * trainID, 171, 62, 31));
                g.DrawString("REP", new Font("宋体", 11f, FontStyle.Bold), Brushs.BlackBrush, new RectangleF(119 + 152.33f * trainID, 171, 62, 31), FontInfo.SF_CC);
            }
        }

        /// <summary>
        /// EDCU状态绘制              
        /// </summary>
        /// <param name="g"></param>
        private void paint_EDCU(Graphics g)
        {
            //EDCU1
            for (int trainID = 0; trainID < 4; trainID++)
            {
                int index = 2;
                for (int state = 0; state < 3; state++)
                {
                    if (BoolList[UIObj.InBoolList[trainID + 4] + state])
                    {
                        index = state;
                        break;
                    }
                }

                g.DrawImage(_resource_Images[index + 1], new RectangleF(119 + 194f * (trainID % 2) + 271 * (trainID / 2), 458, 62, 31));
                g.DrawString("EDCU1", new Font("宋体", 11f, FontStyle.Bold), Brushs.BlackBrush, new RectangleF(119 + 194f * (trainID % 2) + 271 * (trainID / 2), 458, 62, 31), FontInfo.SF_CC);
            }

            //EDCU2
            for (int trainID = 0; trainID < 4; trainID++)
            {
                int index = 2;
                for (int state = 0; state < 3; state++)
                {
                    if (BoolList[UIObj.InBoolList[trainID + 8] + state])
                    {
                        index = state;
                        break;
                    }
                }

                g.DrawImage(_resource_Images[index + 1], new RectangleF(119 + 194f * (trainID % 2) + 271 * (trainID / 2), 458+31, 62, 31));
                g.DrawString("EDCU2", new Font("宋体", 11f, FontStyle.Bold), Brushs.BlackBrush, new RectangleF(119 + 194f * (trainID % 2) + 271 * (trainID / 2), 458+31, 62, 31), FontInfo.SF_CC);
            }
        }
        #endregion
    }
}
