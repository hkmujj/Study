#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图2-车辆状态-第1个组件-主界面（主要是线框与车辆状态绘制）
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图2-车辆状态-第1个组件-主界面（主要是线框与车辆状态绘制）
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V2_TrainState_C0_MainView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private Font _font;//字体
        private Button _btn_Check;//自检按钮
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "车辆状态视图-主界面";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            this._font = new Font("宋体", 10.5f);

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            this._btn_Check = new Button(
                "开始自检",
                new RectangleF(706, 183 - 6 + 31.5f * 10, 84, 39.5F),
                0,
                new ButtonStyle()
                {
                    FontStyle = FontStyles.FS_Song_11_CC_B,
                    Background = this._resource_Image[0],
                    DownImage = this._resource_Image[1]
                }
                );
            this._btn_Check.ClickEvent += _btn_Check_ClickEvent;

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            this._btn_Check.MouseDown(nPoint);
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btn_Check.MouseUp(nPoint);
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 开始自检按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_Check_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //开始自检
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            this._btn_Check.Paint(dcGs);

            this.paint_Frame(dcGs);
            this.paint_LinkingState(dcGs);
            this.paint_MiddleVoltageState(dcGs);
            this.paint_MachineElectricity(dcGs);
            this.paint_SIVState(dcGs);
            this.paint_300VOut(dcGs);
            this.paint_110VOut(dcGs);
            this.paint_ChargeElectricity(dcGs);
            this.paint_WindPressure(dcGs);
            this.paint_NoSpringPressure(dcGs);
            this.paint_BrakingPressure(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 线框与不变文本绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(5, 96, 996, 454));
            //横线
            for (int i = 0; i < 11; i++)
            {
                dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 183 - 6 + 31.5f * i), new PointF(684, 183 - 6 + 31.5f * i));
            }
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 183 - 6 + 31.5f * 11 + 8), new PointF(684, 183 - 6 + 31.5f * 11 + 8));

            //丛线
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 183 - 6), new PointF(15.5f - 4, 183 - 6 + 31.5f * 11 + 8));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f, 183 - 6), new PointF(15.5f - 4 + 114.899f, 183 - 6 + 31.5f * 11 + 8));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f, 183 - 6), new PointF(15.5f - 4 + 114.899f + 92.934f, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 2, 183 - 6), new PointF(15.5f - 4 + 114.899f + 92.934f * 2, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 3, 183 - 6), new PointF(15.5f - 4 + 114.899f + 92.934f * 3, 183 - 6 + 31.5f * 11 + 8));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 4, 183 - 6), new PointF(15.5f - 4 + 114.899f + 92.934f * 4, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6), new PointF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 6, 183 - 6), new PointF(15.5f - 4 + 114.899f + 92.934f * 6, 183 - 6 + 31.5f * 11 + 8));

            //短丛线
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f / 2, 183 - 6 + 31.5f * 8), new PointF(15.5f - 4 + 114.899f + 92.934f / 2, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 3 / 2, 183 - 6 + 31.5f * 8), new PointF(15.5f - 4 + 114.899f + 92.934f * 3 / 2, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 5 / 2, 183 - 6 + 31.5f * 8), new PointF(15.5f - 4 + 114.899f + 92.934f * 5 / 2, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 7 / 2, 183 - 6 + 31.5f * 8), new PointF(15.5f - 4 + 114.899f + 92.934f * 7 / 2, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 9 / 2, 183 - 6 + 31.5f * 8), new PointF(15.5f - 4 + 114.899f + 92.934f * 9 / 2, 183 - 6 + 31.5f * 10));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * 11 / 2, 183 - 6 + 31.5f * 8), new PointF(15.5f - 4 + 114.899f + 92.934f * 11 / 2, 183 - 6 + 31.5f * 10));

            String[] terms = new String[10] { "主断状态", "中间电压(V)", "电机电流(A)", "SIV", "300V输出(V)", "110V输出(V)", "充电电流(A)", "总风压力", "空簧压力", "制动缸压力" };
            for (int i = 0; i < 10; i++)
            {
                dcGs.DrawString(terms[i], this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4, 183 - 6 + 31.5f * i + 2, 114.899f, 31.5f), FontInfo.SF_CC);
            }

            dcGs.DrawString("制动自检", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4, 183 - 6 + 31.5f * 10 + 2, 114.899f, 31.5f + 8), FontInfo.SF_CC);
        }

        /// <summary>
        /// 主断状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_LinkingState(Graphics dcGs)
        {
            SolidBrush[] sb = new SolidBrush[2] { new SolidBrush(Color.FromArgb(168, 168, 152)), new SolidBrush(Color.FromArgb(0, 255, 0)) };
            #region Tc1
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.FillEllipse(sb[i], new RectangleF(15.5f - 4 + 114.899f + 92.934f / 2 - 10f, 183 - 6 + 31.5f / 2 - 10f, 20, 20));
                    break;
                }
            }
            #endregion

            #region Tc2
            for (int i = 2; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.FillEllipse(sb[i % 2], new RectangleF(15.5f - 4 + 114.899f + 92.934f * 5 + 92.934f / 2 - 10f, 183 - 6 + 31.5f / 2 - 10f, 20, 20));
                    break;
                }
            }
            #endregion

            SolidBrush[] sb_ = new SolidBrush[3] { Brushs.GreenBrush, Brushs.YellowBrush, Brushs.RedBrush };
            #region Mp1
            Int32 mp1 = 2;
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                    dcGs.FillRectangle(sb_[i], new RectangleF(15.5f - 4 + 114.899f + 92.934f, 183 - 6 + 1, 92.934f - 1, 31.5f - 2));
            }
            #endregion

            #region M1
            Int32 m1 = 2;
            for (int i = 3; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                    dcGs.FillRectangle(sb_[i%3], new RectangleF(15.5f - 4 + 114.899f + 92.934f * 2, 183 - 6 + 1, 92.934f - 1, 31.5f - 2));
            }
            #endregion

            #region M2
            Int32 m2 = 2;
            for (int i = 6; i < 9; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                    dcGs.FillRectangle(sb_[i%3], new RectangleF(15.5f - 4 + 114.899f + 92.934f * 3, 183 - 6 + 1, 92.934f - 1, 31.5f - 2));
            }
            #endregion

            #region Mp2
            Int32 mp2 = 2;
            for (int i = 9; i < 12; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                    dcGs.FillRectangle(sb_[i%3], new RectangleF(15.5f - 4 + 114.899f + 92.934f * 4, 183 - 6 + 1, 92.934f - 1, 31.5f - 2));
            }
            #endregion
        }

        /// <summary>
        /// 中间电压状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_MiddleVoltageState(Graphics dcGs)
        {
            dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 0, 183 - 6 + 31.5f + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6 + 31.5f + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString(FloatList[UIObj.InFloatList[0] + i].ToString("0"), this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * (i + 1), 183 - 6 + 31.5f + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            }
        }

        /// <summary>
        /// 电机电流状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_MachineElectricity(Graphics dcGs)
        {
            dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 0, 183 - 6 + 31.5f * 2 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6 + 31.5f * 2 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString(FloatList[UIObj.InFloatList[1] + i].ToString("0"), this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * (i + 1), 183 - 6 + 31.5f * 2 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            }
        }

        /// <summary>
        /// SIV状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_SIVState(Graphics dcGs)
        {
            SolidBrush[] sb_SIV = new SolidBrush[3] { Brushs.GreenBrush, Brushs.RedBrush, Brushs.OrangeBrush };

            dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 1, 183 - 6 + 31.5f * 3 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 3, 183 - 6 + 31.5f * 3 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 4, 183 - 6 + 31.5f * 3 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            #region Tc1
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                    dcGs.FillRectangle(sb_SIV[i], new RectangleF(15.5f - 4 + 114.899f + 92.934f * 0, 183 - 6 + 31.5f * 3, 92.934f - 1, 31.5f));
            }
            dcGs.DrawString("SIV", new Font("宋体", 11f, FontStyle.Bold), new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 0, 183 - 6 + 31.5f * 3 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            #endregion

            #region Tc2
            for (int i = 3; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                    dcGs.FillRectangle(sb_SIV[i % 3], new RectangleF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6 + 31.5f * 3, 92.934f, 31.5f));
            }
            dcGs.DrawString("SIV", new Font("宋体", 11f, FontStyle.Bold), new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6 + 31.5f * 3 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            #endregion

            SolidBrush[] sb_KMK = new SolidBrush[3] { Brushs.GreenBrush, Brushs.GrayBrush, Brushs.OrangeBrush };
            #region M1
            for (int i = 6; i < 9; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                    dcGs.FillRectangle(sb_KMK[i % 3], new RectangleF(15.5f - 4 + 114.899f + 92.934f * 2, 183 - 6 + 31.5f * 3, 92.934f - 1, 31.5f));
            }
            dcGs.DrawString("KMK", new Font("宋体", 11f, FontStyle.Bold), new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 2, 183 - 6 + 31.5f * 3 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            #endregion
        }

        /// <summary>
        /// 300v电压状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_300VOut(Graphics dcGs)
        {
            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * (i + 1), 183 - 6 + 31.5f * 4 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            }
            dcGs.DrawString(FloatList[UIObj.InFloatList[2] + 0].ToString("0"), this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 0, 183 - 6 + 31.5f * 4 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString(FloatList[UIObj.InFloatList[2] + 1].ToString("0"), this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6 + 31.5f * 4 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
        }

        /// <summary>
        /// 110v电压状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_110VOut(Graphics dcGs)
        {
            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * (i + 1), 183 - 6 + 31.5f * 5 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            }
            dcGs.DrawString(FloatList[UIObj.InFloatList[3] + 0].ToString("0"), this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 0, 183 - 6 + 31.5f * 5 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString(FloatList[UIObj.InFloatList[3] + 1].ToString("0"), this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6 + 31.5f * 5 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
        }

        /// <summary>
        /// 充电电流状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ChargeElectricity(Graphics dcGs)
        {
            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString("-", this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * (i + 1), 183 - 6 + 31.5f * 6 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            }
            dcGs.DrawString(FloatList[UIObj.InFloatList[4] + 0].ToString("0"), this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 0, 183 - 6 + 31.5f * 6 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
            dcGs.DrawString(FloatList[UIObj.InFloatList[4] + 1].ToString("0"), this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4 + 114.899f + 92.934f * 5, 183 - 6 + 31.5f * 6 + 2, 92.934f, 31.5f), FontInfo.SF_CC);
        }

        /// <summary>
        /// 总风压力状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_WindPressure(Graphics dcGs)
        {
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[5] + i].ToString("0"),
                    this._font,
                    new SolidBrush(Color.White),
                    new RectangleF(15.5f - 4 + 114.899f + 92.934f * i, 183 - 6 + 31.5f * 7 + 2, 92.934f, 31.5f),
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 空簧压力状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_NoSpringPressure(Graphics dcGs)
        {
            for (int i = 0; i < 12; i++)
            {
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[6] + i].ToString("0"),
                    this._font,
                    new SolidBrush(Color.White),
                    new RectangleF(15.5f - 4 + 114.899f + (92.934f / 2) * i, 183 - 6 + 31.5f * 8 + 2, 92.934f / 2, 31.5f),
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 制动缸压力状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_BrakingPressure(Graphics dcGs)
        {
            for (int i = 0; i < 12; i++)
            {
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[7] + i].ToString("0"),
                    this._font,
                    new SolidBrush(Color.White),
                    new RectangleF(15.5f - 4 + 114.899f + (92.934f / 2) * i, 183 - 6 + 31.5f * 9 + 2, 92.934f / 2, 31.5f),
                    FontInfo.SF_CC
                    );
            }
        }
        #endregion
    }
}
