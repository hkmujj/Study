#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.3-状态
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using LightRail.Ethiopia.TMS.Common;

namespace LightRail.Ethiopia.TMS.Main
{
    /// <summary>
    /// 速度指针
    /// </summary>
    public class SpeedPointer
    {
        /// <summary>
        /// 需要画的最大角度
        /// </summary>
        private float dialAnglev;

        /// <summary>
        /// 指针初始角度
        /// </summary>
        private float initalAnglev;

        /// <summary>
        /// 指针最大值
        /// </summary>
        private float maxSpeed;

        /// <summary>
        /// 绘图顶点
        /// </summary>
        private PointF topPoint;

        /// <summary>
        /// 绘图中心点
        /// </summary>
        private PointF centralPoint;

        private Matrix matrix;

        /// <summary>
        /// 转动角度
        /// </summary>
        private float anglev = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxAnglev"></param>
        /// <param name="initAnglev"></param>
        /// <param name="maxValue"></param>
        /// <param name="apexPoint"></param>
        /// <param name="centrePoint"></param>
        public SpeedPointer(float maxAnglev, float initAnglev, float maxValue, PointF apexPoint, PointF centrePoint)
        {
            dialAnglev = maxAnglev;
            initalAnglev = initAnglev;
            maxSpeed = maxValue;
            topPoint = apexPoint;
            centralPoint = centrePoint;
        }

        /// <summary>
        /// 绘指针
        /// </summary>
        /// <param name="g"></param>
        /// <param name="speed"></param>
        public void PaintPointer(Graphics g, Image tmpPic, ref float speed)
        {
            if (speed <= maxSpeed)
            {
                anglev = speed * dialAnglev / maxSpeed + initalAnglev;
            }
            matrix = g.Transform;
            matrix.RotateAt(anglev, centralPoint);
            g.Transform = matrix;
            g.DrawImage(tmpPic, topPoint);
            matrix.Reset();
            g.Transform = matrix;
        }
    }

    /// <summary>
    /// 功能描述：视图1-运行-No.2-状态
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_Main_C0_Speed : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private SpeedPointer _speedPointer;//速度指针
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "Main-速度";
        }

        /// <summary>
        /// 初始化函数：导入图片、初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //导入图片资源
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Images.Add(Image.FromStream(fs));
                }
            });

            _speedPointer = new SpeedPointer(240f, -30f, 80f, new PointF(377, 82), new PointF(488, 193));

            return true;
        }

        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            paint_SpeedPointer(dcGs);
            paint_LimitSpeed(dcGs);
            paint_Distance(dcGs);

            base.paint(dcGs);
        }
        #endregion

        /// <summary>
        /// 绘制速度指针
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_SpeedPointer(Graphics dcGs)
        {
            float realSpeed = FloatList[UIObj.InFloatList[0]];

            dcGs.DrawString(
                realSpeed.ToString("0"),
                new Font("Verdana", 15, FontStyle.Bold),
                new SolidBrush(Color.Yellow),
                new RectangleF(461, 217, 55, 32),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );//显示速度
            _speedPointer.PaintPointer(dcGs, _resource_Images[0], ref realSpeed);//指针
        }

        /// <summary>
        /// 绘制限速
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_LimitSpeed(Graphics dcGs)
        {
            String[] strs = new[] { "3", "10", "20", "30", "40", "50", "60", "STOP" };
            Font font = new Font("Verdana", 30, FontStyle.Bold);
            for (int i = 0; i < 8; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.DrawImage(_resource_Images[1], new Point(645, 168));
                    if (i == 7) font = new Font("Verdana", 15, FontStyle.Bold);
                    dcGs.DrawString(
                        strs[i],
                        font,
                        new SolidBrush(Color.Black),
                        new RectangleF(656, 179, 78, 78),
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        );
                }
            }
        }

        /// <summary>
        /// 绘制总里程
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Distance(Graphics dcGs)
        {
            float distance = FloatList[UIObj.InFloatList[1]];

            dcGs.DrawString(
                distance.ToString("0km"),
                new Font("Verdana", 10, FontStyle.Bold),
                new SolidBrush(Color.Yellow),
                new RectangleF(461, 217+32+16, 55, 16),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );//显示速度
        }
    }
}
