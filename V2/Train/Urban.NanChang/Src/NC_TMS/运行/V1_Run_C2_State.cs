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
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图1-运行-No.2-状态
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_Run_C2_State : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private SolidBrush[] _airBrakingStates;//空气制动状态
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "运行试图-状态栏";
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Images.Add(Image.FromStream(fs));
                }
            });

            //空气制动状态（白、黄、绿、红）
            this._airBrakingStates = new SolidBrush[4] { 
                new SolidBrush(Color.FromArgb(255, 255, 255)), 
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new SolidBrush(Color.FromArgb(0, 255, 0)),
                new SolidBrush(Color.FromArgb(255, 0, 0)) 
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
            this.paint_AirCondition(dcGs);
            this.paint_Traction(dcGs);
            this.paint_PassengerPolice(dcGs);
            this.paint_Braking(dcGs);
            this.paint_AirBraking(dcGs);
            this.paint_Temperature(dcGs);
            this.paint_Busload(dcGs);
            this.paint_Door_Up(dcGs);
            this.paint_Door_Down(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 运行界面-状态栏线框和不变状态的绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new Point(0, 96), new Point(800, 96));

            //横线
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 183 - 6), new PointF(684, 183 - 6));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(122.33f - 4, 227.75f - 3), new PointF(684, 227.75f - 3));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 272.5f), new PointF(684, 272.5f));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 304), new PointF(684, 304));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 335.5f), new PointF(684, 335.5f));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 367), new PointF(684, 367));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 398.5f), new PointF(684, 398.5f));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 430), new PointF(684, 430));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 462), new PointF(684, 462));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 494), new PointF(684, 494));
            //丛线
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 183 - 6), new PointF(15.5f - 4, 494));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(122.33f - 4, 183 - 6), new PointF(122.33f - 4, 494));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(216.67f - 4, 183 - 6), new PointF(216.67f - 4, 494));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(310.99f - 4, 183 - 6), new PointF(310.99f - 4, 494));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(405.32f - 4, 183 - 6), new PointF(405.32f - 4, 494));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(499.65f - 4, 183 - 6), new PointF(499.65f - 4, 494));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(593.98f - 4, 183 - 6), new PointF(593.98f - 4, 494));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(684, 183 - 6), new PointF(684, 494));

            dcGs.DrawString("门状态", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15.5f - 4, 183, 106.83f, 89.5f), FontInfo.SF_CC);
            dcGs.DrawString("空气压缩机", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15.5f - 4, 275f, 106.83f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("牵引辅助系统", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15.5f - 4, 307, 106.83f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("高断状态", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15.5f - 4, 339f, 106.83f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("停放制动", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15.5f - 4, 370, 106.83f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("空气制动", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15.5f - 4, 402f, 106.83f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("客室温度", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15.5f - 4, 434, 106.83f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("载客率", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15.5f - 4, 466, 106.83f, 31.5f), FontInfo.SF_CC);

            dcGs.DrawString("-", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(216.67f - 4, 275f, 94.34f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("-", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(310.99f - 4, 275f, 94.34f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("-", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(405.32f - 4, 275f, 94.34f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("-", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(499.65f - 4, 275f, 94.34f, 31.5f), FontInfo.SF_CC);

        }

        /// <summary>
        /// 运行界面-空调压缩机状态绘制(数据下标0-1)
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_AirCondition(Graphics dcGs)
        {
            Brush[] brushs = new SolidBrush[] { new SolidBrush(Color.FromArgb(0, 255, 0)), new SolidBrush(Color.FromArgb(188, 188, 188)), new SolidBrush(Color.FromArgb(255, 165, 0)) };
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.FillRectangle(brushs[i], new RectangleF(122.33f - 4, 273f, 94.34f, 30.5f));
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    dcGs.FillRectangle(brushs[i], new RectangleF(594.98f - 4, 273f, 88.34f + 4, 30.5f));
                }
            }
        }

        /// <summary>
        /// 运行界面-牵引辅助系统状态绘制（Tc1与Tc2接口需添加）(数据下标2-7)
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Traction(Graphics dcGs)
        {
            Brush[] brushs = new SolidBrush[] {
                new SolidBrush(Color.FromArgb(0, 255, 0)), 
                new SolidBrush(Color.FromArgb(255, 0, 0)), 
                new SolidBrush(Color.FromArgb(255, 165, 0))
            };
            Brush[] brushs_ = new SolidBrush[] {
                new SolidBrush(Color.FromArgb(0, 255, 0)),
                new SolidBrush(Color.FromArgb(255, 255, 255)), 
                new SolidBrush(Color.FromArgb(255, 0, 0)),
                new SolidBrush(Color.FromArgb(255, 165, 0)), 
                new SolidBrush(Color.FromArgb(160, 160, 160)) 
            };
            #region Tc1
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    dcGs.FillRectangle(brushs[i], new RectangleF(122.33f - 4, 305f, 94.34f, 29.5f));
                    break;
                }
            }
            dcGs.DrawString("SIV", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(122.33f - 4, 307, 94.34f, 30.5f), FontInfo.SF_CC);
            #endregion

            #region Mp1
            Int32 index_Mp1 = 0;
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    dcGs.FillRectangle(brushs_[i], new RectangleF(217.67f - 4, 305f, 93.34f, 29.5f));
                    index_Mp1 = i;
                    break;
                }
            }
            SolidBrush sb_Mp1 = index_Mp1 == 1 ? Brushs.BlackBrush : Brushs.WhiteBrush;
            dcGs.DrawString("VVVF", new Font("宋体", 11), sb_Mp1, new RectangleF(216.67f - 4, 307, 94.34f, 30.5f), FontInfo.SF_CC);
            #endregion

            #region M1
            Int32 index_M1 = 0;
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[UIObj.InBoolList[4] + i])
                {
                    dcGs.FillRectangle(brushs_[i], new RectangleF(311.99f - 4, 305f, 92.34f, 29.5f));
                    index_M1 = i;
                    break;
                }
            }
            SolidBrush sb_M1 = index_M1 == 1 ? Brushs.BlackBrush : Brushs.WhiteBrush;
            dcGs.DrawString("VVVF", new Font("宋体", 11), sb_M1, new RectangleF(310.99f - 4, 307, 94.34f, 30.5f), FontInfo.SF_CC);
            #endregion

            #region M2
            Int32 index_M2 = 0;
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[UIObj.InBoolList[5] + i])
                {
                    dcGs.FillRectangle(brushs_[i], new RectangleF(405.32f - 4, 305f, 94.34f, 29.5f));
                    index_M2 = i;
                    break;
                }
            }
            SolidBrush sb_M2 = index_M2 == 1 ? Brushs.BlackBrush : Brushs.WhiteBrush;
            dcGs.DrawString("VVVF", new Font("宋体", 11), sb_M2, new RectangleF(405.32f - 4, 307, 94.34f, 30.5f), FontInfo.SF_CC);
            #endregion

            #region MP2
            Int32 index_Mp2 = 0;
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[UIObj.InBoolList[6] + i])
                {
                    dcGs.FillRectangle(brushs_[i], new RectangleF(500.65f - 4, 305f, 93.34f, 29.5f));
                    index_Mp2 = i;
                    break;
                }
            }
            SolidBrush sb_Mp2 = index_Mp2 == 1 ? Brushs.BlackBrush : Brushs.WhiteBrush;
            dcGs.DrawString("VVVF", new Font("宋体", 11), sb_Mp2, new RectangleF(499.65f - 4, 307, 94.34f, 30.5f), FontInfo.SF_CC);
            #endregion

            #region Tc2
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[7] + i])
                {
                    dcGs.FillRectangle(brushs[i], new RectangleF(594.98f - 4, 305f, 88.34f + 4, 29.5f));
                    break;
                }
            }
            dcGs.DrawString("SIV", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(594.98f - 4, 307, 94.34f, 30.5f), FontInfo.SF_CC);
            #endregion
        }

        private Brush[] states = new Brush[3] { Brushes.LimeGreen, Brushes.Yellow, Brushes.Red };
        /// <summary>
        /// 运行界面-乘客报警状态绘制（数据下标8，所占长度48位，图片下标0-3）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_PassengerPolice(Graphics dcGs)
        {
            Int32 index = 8;

            //#region Tc1
            //Int32 tc1_1 = -1;
            //for (int i = 0; i < 4; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        tc1_1 = i % 4;
            //        break;
            //    }
            //}
            //if (tc1_1 != -1) dcGs.DrawImage(this._resource_Images[tc1_1], new PointF(122.33f - 4 + 10, 338f));
            //Int32 tc1_2 = -1;
            //for (int i = 4; i < 8; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        tc1_2 = i % 4;
            //        break;
            //    }
            //}
            //if (tc1_2 != -1) dcGs.DrawImage(this._resource_Images[tc1_2], new PointF(122.33f - 4 + 10 + 30 + 16, 338f));
            //#endregion

            #region Mp1
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(states[i], new RectangleF(216.67f - 4+1, 338f-2,91.34F+2,31.5F-2));
                    break;
                }
            }
            //Int32 Mp1_1 = -1;
            //for (int i = 8; i < 12; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        Mp1_1 = i % 4;
            //        break;
            //    }
            //}
            //if (Mp1_1 != -1) dcGs.DrawImage(this._resource_Images[Mp1_1], new PointF(216.67f - 4 + 10, 338f));
            //Int32 Mp1_2 = -1;
            //for (int i = 12; i < 16; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        Mp1_2 = i % 4;
            //        break;
            //    }
            //}
            //if (Mp1_2 != -1) dcGs.DrawImage(this._resource_Images[Mp1_2], new PointF(216.67f - 4 + 10 + 30 + 16, 338f));
            #endregion

            #region M1
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[index]+3 + i])
                {
                    Brush b = states[i];
                    if (i == 0) b = states[1];
                    if (i == 1) b = states[0];
                    dcGs.FillRectangle(states[i], new RectangleF(310.99f - 3, 338f-2, 92.34F, 31.5F-2));
                    break;
                }
            }

            //Int32 M1_1 = -1;
            //for (int i = 16; i < 20; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        M1_1 = i % 4;
            //        break;
            //    }
            //}
            //if (M1_1 != -1) dcGs.DrawImage(this._resource_Images[M1_1], new PointF(310.99f - 4 + 10, 338f));
            //Int32 M1_2 = -1;
            //for (int i = 20; i < 24; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        M1_2 = i % 4;
            //        break;
            //    }
            //}
            //if (M1_2 != -1) dcGs.DrawImage(this._resource_Images[M1_2], new PointF(310.99f - 4 + 10 + 30 + 16, 338f));
            #endregion

            #region M2
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + 6 + i])
                {
                    dcGs.FillRectangle(states[i], new RectangleF(405.32f - 4, 338f-2, 94.34F, 31.5F-2));
                    break;
                }
            }
            //Int32 M2_1 = -1;
            //for (int i = 24; i < 28; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        M2_1 = i % 4;
            //        break;
            //    }
            //}
            //if (M2_1 != -1) dcGs.DrawImage(this._resource_Images[M2_1], new PointF(405.32f - 4 + 10, 338f));
            //Int32 M2_2 = -1;
            //for (int i = 28; i < 32; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        M2_2 = i % 4;
            //        break;
            //    }
            //}
            //if (M2_2 != -1) dcGs.DrawImage(this._resource_Images[M2_2], new PointF(405.32f - 4 + 10 + 30 + 16, 338f));
            #endregion

            #region Mp2
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + 9 + i])
                {
                    dcGs.FillRectangle(states[i], new RectangleF(499.65f - 3, 338f-2, 93.34F, 31.5F-2));
                    break;
                }
            }
            //Int32 Mp2_1 = -1;
            //for (int i = 32; i < 36; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        Mp2_1 = i % 4;
            //        break;
            //    }
            //}
            //if (Mp2_1 != -1) dcGs.DrawImage(this._resource_Images[Mp2_1], new PointF(499.65f - 4 + 10, 338f));
            //Int32 Mp2_2 = -1;
            //for (int i = 36; i < 40; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        Mp2_2 = i % 4;
            //        break;
            //    }
            //}
            //if (Mp2_2 != -1) dcGs.DrawImage(this._resource_Images[Mp2_2], new PointF(499.65f - 4 + 10 + 30 + 16, 338f));
            #endregion

            //#region Tc2
            //Int32 tc2_1 = -1;
            //for (int i = 40; i < 44; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        tc2_1 = i % 4;
            //        break;
            //    }
            //}
            //if (tc2_1 != -1) dcGs.DrawImage(this._resource_Images[tc2_1], new PointF(593.98f - 4 + 10, 338f));
            //Int32 tc2_2 = -1;
            //for (int i = 44; i < 48; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[index] + i])
            //    {
            //        tc2_2 = i % 4;
            //        break;
            //    }
            //}
            //if (tc2_2 != -1) dcGs.DrawImage(this._resource_Images[tc2_2], new PointF(593.98f - 4 + 10 + 30 + 13, 338f));
            //#endregion
        }

        /// <summary>
        /// 运行界面-停放制动状态绘制（数据下标9，所占长度12位，图片下标4-6
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Braking(Graphics dcGs)
        {
            Int32 index = 9;
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.DrawImage(this._resource_Images[4 + i % 3], new PointF(122.33f - 4 + 30, 370f));
                    break;
                }
            }

            for (int i = 3; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.DrawImage(this._resource_Images[4 + i % 3], new PointF(216.67f - 4 + 30, 370f));
                    break;
                }
            }

            for (int i = 6; i < 9; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.DrawImage(this._resource_Images[4 + i % 3], new PointF(310.99f - 4 + 30, 370f));
                    break;
                }
            }

            for (int i = 9; i < 12; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.DrawImage(this._resource_Images[4 + i % 3], new PointF(405.32f - 4 + 30, 370f));
                    break;
                }
            }

            for (int i = 12; i < 15; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.DrawImage(this._resource_Images[4 + i % 3], new PointF(499.65f - 4 + 30, 370f));
                    break;
                }
            }

            for (int i = 15; i < 18; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.DrawImage(this._resource_Images[4 + i % 3], new PointF(593.98f - 4 + 30, 370f));
                    break;
                }
            }
        }

        /// <summary>
        /// 运行界面-空气制动状态绘制（数据下标10，所占长度48位）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_AirBraking(Graphics dcGs)
        {
            #region Tc1
            Int32 index = 10;
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(127.33f - 4, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[tc1_1], new RectangleF(127.33f - 4, 405f, 40.4f, 20.8f));
            //Int32 tc1_2 = 3;
            for (int i = 4; i < 8; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(127.33f - 4 + 40.4f + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[tc1_2], new RectangleF(127.33f - 4 + 40.4f + 5, 405f, 40.4f, 20.8f));
            #endregion

            #region Mp1
            //Int32 mp1_1 = 3;
            for (int i = 8; i < 12; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(216.67f - 4 + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[mp1_1], new RectangleF(216.67f - 4 + 5, 405f, 40.4f, 20.8f));
            //Int32 mp1_2 = 3;
            for (int i = 12; i < 16; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(216.67f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[mp1_2], new RectangleF(216.67f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
            #endregion

            #region M1
            //Int32 m1_1 = 3;
            for (int i = 16; i < 20; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(310.99f - 4 + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[m1_1], new RectangleF(310.99f - 4 + 5, 405f, 40.4f, 20.8f));
            //Int32 m1_2 = 3;
            for (int i = 20; i < 24; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(310.99f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[m1_2], new RectangleF(310.99f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
            #endregion

            #region M2
            //Int32 m2_1 = 3;
            for (int i = 24; i < 28; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(405.32f - 4 + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[m2_1], new RectangleF(405.32f - 4 + 5, 405f, 40.4f, 20.8f));
            //Int32 m2_2 = 3;
            for (int i = 28; i < 32; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(405.32f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[m2_2], new RectangleF(405.32f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
            #endregion

            #region Mp2
            //Int32 mp2_1 = 3;
            for (int i = 32; i < 36; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(499.65f - 4 + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[mp2_1], new RectangleF(499.65f - 4 + 5, 405f, 40.4f, 20.8f));
            //Int32 mp2_2 = 3;
            for (int i = 36; i < 40; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(499.65f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[mp2_2], new RectangleF(499.65f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
            #endregion

            #region Tc2
            //Int32 tc2_1 = 3;
            for (int i = 40; i < 44; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(593.98f - 4 + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[tc2_1], new RectangleF(593.98f - 4 + 5, 405f, 40.4f, 20.8f));
            //Int32 tc2_2 = 3;
            for (int i = 44; i < 48; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    dcGs.FillRectangle(_airBrakingStates[i % 4], new RectangleF(593.98f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
                    break;
                }
            }
            //dcGs.FillRectangle(_airBrakingStates[tc2_2], new RectangleF(593.98f - 4 + 5 + 40.4f + 5, 405f, 40.4f, 20.8f));
            #endregion
        }

        /// <summary>
        /// 运行界面-客室温度状态绘制（F数据下标0，所占长度6位）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Temperature(Graphics dcGs)
        {
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawString(
                    String.Format("{0:F1}℃", FloatList[UIObj.InFloatList[0] + i]),
                    new Font("宋体", 14),
                    new SolidBrush(Color.White),
                    new RectangleF(122.33f - 4 + 94.33f * i, 434, 94.33f, 31.5f),
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 运行界面-载客率状态绘制（F数据下标1，所占长度6位）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Busload(Graphics dcGs)
        {
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawString(
                    (FloatList[UIObj.InFloatList[1] + i]).ToString("0.0")+"%",
                    new Font("宋体", 14),
                    new SolidBrush(Color.White),
                    new RectangleF(122.33f - 4 + 94.33f * i, 466, 94.33f, 31.5f),
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 运行界面-门-上部状态绘制（B数据下标11，长度192位）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Door_Up(Graphics dcGs)
        {
            Int32 index = 11;
            Int32 imageIndec = 7;
            #region Tc1_UP
            Int32 tc1_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i])
                {
                    tc1_1 = i % 6;
                    break;
                }
            }
            if (tc1_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc1_1], new PointF(122.33f - 5 + 5, 183 + 10));
            dcGs.DrawString("24", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(122.33f - 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 tc1_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6])
                {
                    tc1_2 = i % 6;
                    break;
                }
            }
            if (tc1_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc1_2], new PointF(122.33f - 5 + 5 + 16 + 6, 183 + 10));
            dcGs.DrawString("23", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(122.33f - 4 + 94.34f / 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 tc1_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 2])
                {
                    tc1_3 = i % 6;
                    break;
                }
            }
            if (tc1_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc1_3], new PointF(122.33f - 5 + 5 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("22", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(122.33f - 4 + 94.34f / 4 * 2, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 tc1_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 3])
                {
                    tc1_4 = i % 6;
                    break;
                }
            }
            if (tc1_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc1_4], new PointF(122.33f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("21", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(122.33f - 4 + 94.34f / 4 * 3, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region Mp1_UP
            Int32 mp1_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 4])
                {
                    mp1_1 = i % 6;
                    break;
                }
            }
            if (mp1_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp1_1], new PointF(216.67f - 5 + 5, 183 + 10));
            dcGs.DrawString("20", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(216.67f - 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 mp1_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 5])
                {
                    mp1_2 = i % 6;
                    break;
                }
            }
            if (mp1_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp1_2], new PointF(216.67f - 5 + 5 + 16 + 6, 183 + 10));
            dcGs.DrawString("19", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(216.67f - 4 + 94.34f / 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 mp1_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 6])
                {
                    mp1_3 = i % 6;
                    break;
                }
            }
            if (mp1_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp1_3], new PointF(216.67f - 5 + 5 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("18", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(216.67f - 4 + 94.34f / 4 * 2, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 mp1_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 7])
                {
                    mp1_4 = i % 6;
                    break;
                }
            }
            if (mp1_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp1_4], new PointF(216.67f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("17", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(216.67f - 4 + 94.34f / 4 * 3, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region M1_UP
            Int32 m1_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 8])
                {
                    m1_1 = i % 6;
                    break;
                }
            }
            if (m1_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m1_1], new PointF(310.99f - 5 + 5, 183 + 10));
            dcGs.DrawString("16", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(310.99f - 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 m1_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 9])
                {
                    m1_2 = i % 6;
                    break;
                }
            }
            if (m1_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m1_2], new PointF(310.99f - 5 + 5 + 16 + 6, 183 + 10));
            dcGs.DrawString("15", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(310.99f - 4 + 94.34f / 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 m1_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 10])
                {
                    m1_3 = i % 6;
                    break;
                }
            }
            if (m1_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m1_3], new PointF(310.99f - 5 + 5 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("14", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(310.99f - 4 + 94.34f / 4 * 2, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 m1_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 11])
                {
                    m1_4 = i % 6;
                    break;
                }
            }
            if (m1_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m1_4], new PointF(310.99f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("13", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(310.99f - 4 + 94.34f / 4 * 3, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region M2_UP
            Int32 m2_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 12])
                {
                    m2_1 = i % 6;
                    break;
                }
            }
            if (m2_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m2_1], new PointF(405.32f - 5 + 5, 183 + 10));
            dcGs.DrawString("12", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(405.32f - 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 m2_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 13])
                {
                    m2_2 = i % 6;
                    break;
                }
            }
            if (m2_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m2_2], new PointF(405.32f - 5 + 5 + 16 + 6, 183 + 10));
            dcGs.DrawString("11", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(405.32f - 4 + 94.34f / 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 m2_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 14])
                {
                    m2_3 = i % 6;
                    break;
                }
            }
            if (m2_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m2_3], new PointF(405.32f - 5 + 5 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("10", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(405.32f - 4 + 94.34f / 4 * 2, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 m2_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 15])
                {
                    m2_4 = i % 6;
                    break;
                }
            }
            if (m2_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m2_4], new PointF(405.32f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("9", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(405.32f - 4 + 94.34f / 4 * 3, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region Mp2_UP
            Int32 mp2_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 16])
                {
                    mp2_1 = i % 6;
                    break;
                }
            }
            if (mp2_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp2_1], new PointF(499.65f - 5 + 5, 183 + 10));
            dcGs.DrawString("8", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(499.65f - 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 mp2_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 17])
                {
                    mp2_2 = i % 6;
                    break;
                }
            }
            if (mp2_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp2_2], new PointF(499.65f - 5 + 5 + 16 + 6, 183 + 10));
            dcGs.DrawString("7", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(499.65f - 4 + 94.34f / 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 mp2_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 18])
                {
                    mp2_3 = i % 6;
                    break;
                }
            }
            if (mp2_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp2_3], new PointF(499.65f - 5 + 5 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("6", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(499.65f - 4 + 94.34f / 4 * 2, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 mp2_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 19])
                {
                    mp2_4 = i % 6;
                    break;
                }
            }
            if (mp2_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp2_4], new PointF(499.65f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("5", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(499.65f - 4 + 94.34f / 4 * 3, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region Tc2_UP
            Int32 tc2_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 20])
                {
                    tc2_1 = i % 6;
                    break;
                }
            }
            if (tc2_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc2_1], new PointF(593.98f - 5 + 5, 183 + 10));
            dcGs.DrawString("4", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(593.98f - 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 tc2_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 21])
                {
                    tc2_2 = i % 6;
                    break;
                }
            }
            if (tc2_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc2_2], new PointF(593.98f - 5 + 5 + 16 + 6, 183 + 10));
            dcGs.DrawString("3", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(593.98f - 4 + 94.34f / 4, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 tc2_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 22])
                {
                    tc2_3 = i % 6;
                    break;
                }
            }
            if (tc2_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc2_3], new PointF(593.98f - 5 + 5 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("2", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(593.98f - 4 + 94.34f / 4 * 2, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 tc2_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 23])
                {
                    tc2_4 = i % 6;
                    break;
                }
            }
            if (tc2_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc2_4], new PointF(593.98f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 183 + 10));
            dcGs.DrawString("1", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(593.98f - 4 + 94.34f / 4 * 3, 183 - 6, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion
        }

        /// <summary>
        /// 运行界面-门-下部状态绘制（B数据下标12，长度192位）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Door_Down(Graphics dcGs)
        {
            Int32 index = 12;
            Int32 imageIndec = 7;
            #region Tc1_UP
            Int32 tc1_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 23])
                {
                    tc1_1 = i % 6;
                    break;
                }
            }
            if (tc1_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc1_1], new PointF(122.33f - 5 + 5, 227.75f));
            dcGs.DrawString("1", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(122.33f - 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 tc1_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 22])
                {
                    tc1_2 = i % 6;
                    break;
                }
            }
            if (tc1_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc1_2], new PointF(122.33f - 5 + 5 + 16 + 6, 227.75f));
            dcGs.DrawString("2", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(122.33f - 4 + 94.34f / 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 tc1_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 21])
                {
                    tc1_3 = i % 6;
                    break;
                }
            }
            if (tc1_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc1_3], new PointF(122.33f - 5 + 5 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("3", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(122.33f - 4 + 94.34f / 4 * 2, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 tc1_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 20])
                {
                    tc1_4 = i % 6;
                    break;
                }
            }
            if (tc1_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc1_4], new PointF(122.33f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("4", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(122.33f - 4 + 94.34f / 4 * 3, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region Mp1_UP
            Int32 mp1_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 19])
                {
                    mp1_1 = i % 6;
                    break;
                }
            }
            if (mp1_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp1_1], new PointF(216.67f - 5 + 5, 227.75f));
            dcGs.DrawString("5", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(216.67f - 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 mp1_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 18])
                {
                    mp1_2 = i % 6;
                    break;
                }
            }
            if (mp1_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp1_2], new PointF(216.67f - 5 + 5 + 16 + 6, 227.75f));
            dcGs.DrawString("6", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(216.67f - 4 + 94.34f / 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 mp1_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 17])
                {
                    mp1_3 = i % 6;
                    break;
                }
            }
            if (mp1_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp1_3], new PointF(216.67f - 5 + 5 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("7", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(216.67f - 4 + 94.34f / 4 * 2, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 mp1_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 16])
                {
                    mp1_4 = i % 6;
                    break;
                }
            }
            if (mp1_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp1_4], new PointF(216.67f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("8", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(216.67f - 4 + 94.34f / 4 * 3, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region M1_UP
            Int32 m1_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 15])
                {
                    m1_1 = i % 6;
                    break;
                }
            }
            if (m1_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m1_1], new PointF(310.99f - 5 + 5, 227.75f));
            dcGs.DrawString("9", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(310.99f - 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 m1_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 14])
                {
                    m1_2 = i % 6;
                    break;
                }
            }
            if (m1_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m1_2], new PointF(310.99f - 5 + 5 + 16 + 6, 227.75f));
            dcGs.DrawString("10", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(310.99f - 4 + 94.34f / 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 m1_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 13])
                {
                    m1_3 = i % 6;
                    break;
                }
            }
            if (m1_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m1_3], new PointF(310.99f - 5 + 5 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("11", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(310.99f - 4 + 94.34f / 4 * 2, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 m1_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 12])
                {
                    m1_4 = i % 6;
                    break;
                }
            }
            if (m1_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m1_4], new PointF(310.99f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("12", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(310.99f - 4 + 94.34f / 4 * 3, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region M2_UP
            Int32 m2_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 11])
                {
                    m2_1 = i % 6;
                    break;
                }
            }
            if (m2_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m2_1], new PointF(405.32f - 5 + 5, 227.75f));
            dcGs.DrawString("13", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(405.32f - 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 m2_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 10])
                {
                    m2_2 = i % 6;
                    break;
                }
            }
            if (m2_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m2_2], new PointF(405.32f - 5 + 5 + 16 + 6, 227.75f));
            dcGs.DrawString("14", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(405.32f - 4 + 94.34f / 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 m2_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 9])
                {
                    m2_3 = i % 6;
                    break;
                }
            }
            if (m2_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m2_3], new PointF(405.32f - 5 + 5 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("15", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(405.32f - 4 + 94.34f / 4 * 2, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 m2_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 8])
                {
                    m2_4 = i % 6;
                    break;
                }
            }
            if (m2_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + m2_4], new PointF(405.32f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("16", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(405.32f - 4 + 94.34f / 4 * 3, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region Mp2_UP
            Int32 mp2_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 7])
                {
                    mp2_1 = i % 6;
                    break;
                }
            }
            if (mp2_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp2_1], new PointF(499.65f - 5 + 5, 227.75f));
            dcGs.DrawString("17", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(499.65f - 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 mp2_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 6])
                {
                    mp2_2 = i % 6;
                    break;
                }
            }
            if (mp2_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp2_2], new PointF(499.65f - 5 + 5 + 16 + 6, 227.75f));
            dcGs.DrawString("18", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(499.65f - 4 + 94.34f / 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 mp2_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 5])
                {
                    mp2_3 = i % 6;
                    break;
                }
            }
            if (mp2_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp2_3], new PointF(499.65f - 5 + 5 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("19", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(499.65f - 4 + 94.34f / 4 * 2, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 mp2_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 4])
                {
                    mp2_4 = i % 6;
                    break;
                }
            }
            if (mp2_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + mp2_4], new PointF(499.65f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("20", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(499.65f - 4 + 94.34f / 4 * 3, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion

            #region Tc2_UP
            Int32 tc2_1 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 3])
                {
                    tc2_1 = i % 6;
                    break;
                }
            }
            if (tc2_1 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc2_1], new PointF(593.98f - 5 + 5, 227.75f));
            dcGs.DrawString("21", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(593.98f - 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 tc2_2 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 2])
                {
                    tc2_2 = i % 6;
                    break;
                }
            }
            if (tc2_2 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc2_2], new PointF(593.98f - 5 + 5 + 16 + 6, 227.75f));
            dcGs.DrawString("22", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(593.98f - 4 + 94.34f / 4, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);

            Int32 tc2_3 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 1])
                {
                    tc2_3 = i % 6;
                    break;
                }
            }
            if (tc2_3 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc2_3], new PointF(593.98f - 5 + 5 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("23", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(593.98f - 4 + 94.34f / 4 * 2, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            Int32 tc2_4 = -1;
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[index] + i + 6 * 0])
                {
                    tc2_4 = i % 6;
                    break;
                }
            }
            if (tc2_4 != -1) dcGs.DrawImage(this._resource_Images[imageIndec + tc2_4], new PointF(593.98f - 5 + 5 + 16 + 6 + 16 + 6 + 16 + 6, 227.75f));
            dcGs.DrawString("24", new Font("宋体", 8), new SolidBrush(Color.White), new RectangleF(593.98f - 4 + 94.34f / 4 * 3, 227.75f + 28, 94.34f / 4, 16), FontInfo.SF_CC);
            #endregion
        }
        #endregion
    }
}
