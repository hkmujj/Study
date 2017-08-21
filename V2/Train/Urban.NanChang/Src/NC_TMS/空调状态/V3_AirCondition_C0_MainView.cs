#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-3
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图3-空调-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图3-空调-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-3
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V3_AirCondition_C0_MainView : baseClass
    {
        private Font _font;//字体

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "空调状态视图-主界面";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            this._font = new Font("宋体", 10.5f);

            return true;
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            this.paint_Frame(dcGs);
            this.paint_TrainTemperature(dcGs);
            this.paint_TrainTargetTemperature(dcGs);
            this.paint_AirConditionState(dcGs);
            this.paint_AirConditionMode(dcGs);
            this.paint_CompressorState(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 状态框绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new Point(0, 96), new Point(800, 96));
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 188 - 6 + 31.5f * i), new PointF(684, 188 - 6 + 31.5f * i));
            }

            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4, 188 - 6), new PointF(15.5f - 4, 188 - 6 + 31.5f * 5));
            for (int i = 0; i < 7; i++)
            {
                dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(15.5f - 4 + 114.899f + 92.934f * i, 188 - 6), new PointF(15.5f - 4 + 114.899f + 92.934f * i, 188 - 6 + 31.5f * 5));
            }

            String[] terms = new String[5] { "室内温度", "空调目标温度", "空调状态", "空调模式", "压缩机状态" };
            for (int i = 0; i < 5; i++)
            {
                dcGs.DrawString(terms[i], this._font, new SolidBrush(Color.White), new RectangleF(15.5f - 4, 188 - 6 + 31.5f * i + 2, 114.899f, 31.5f), FontInfo.SF_CC);
            }
        }

        /// <summary>
        /// 室内温度
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TrainTemperature(Graphics dcGs)
        {
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawString(
                    V301_AirCondition_C2_TemperatureControl.Temperatures[i].ToString("0℃"),//FloatList[UIObj.InFloatList[0] + i].ToString("0.0℃")
                    this._font,
                    new SolidBrush(Color.White),
                    new RectangleF(15.5f - 4 + 114.899f + 92.934f * i, 182 + 2, 92.934f, 31.5f),
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 车厢目标温度
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TrainTargetTemperature(Graphics dcGs)
        {
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawString(
                    V301_AirCondition_C2_TemperatureControl.Temperatures[i].ToString("0℃"),
                    this._font,
                    new SolidBrush(Color.White),
                    new RectangleF(15.5f - 4 + 114.899f + 92.934f * i, 182 + 2 + 31.5f * 1, 92.934f, 31.5f),
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 空调状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_AirConditionState(Graphics dcGs)
        {
            String[] str = new String[] { "预冷", "全冷", "半冷", "自动冷", "通风", "停机", "未知" };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (BoolList[UIObj.InBoolList[i] + j])
                    {
                        dcGs.DrawString(
                            str[j],
                            this._font,
                            new SolidBrush(Color.White),
                            new RectangleF(15.5f - 4 + 114.899f + 92.934f * i, 182 + 2 + 31.5f * 2, 92.934f, 31.5f),
                            FontInfo.SF_CC
                            );
                    }
                }
            }
        }

        /// <summary>
        /// 空调模式
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_AirConditionMode(Graphics dcGs)
        {
            String[] str = new String[] { "本地控制", "集中控制", "紧急通风测试", "紧急通风", "未知" };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (BoolList[UIObj.InBoolList[i + 6] + j])
                    {
                        dcGs.DrawString(
                            str[j],
                            this._font,
                            new SolidBrush(Color.White),
                            new RectangleF(15.5f - 4 + 114.899f + 92.934f * i, 182 + 2 + 31.5f * 3, 92.934f, 31.5f),
                            FontInfo.SF_CC
                            );
                    }
                }
            }
        }

        /// <summary>
        /// 压缩机状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_CompressorState(Graphics dcGs)
        {
            SolidBrush[] sbs_state = new SolidBrush[] { Brushs.GreenBrush, Brushs.RedBrush, Brushs.GrayBrush, Brushs.OrangeBrush };
            #region Tc1
            for (int trainID = 0; trainID < 6; trainID++)
            {
                for (int compressorID = 0; compressorID < 4; compressorID++)
                {
                    for (int state = 0; state < 4; state++)
                    {
                        if (BoolList[UIObj.InBoolList[12] + state + 4 * compressorID + trainID * 16])
                        {
                            dcGs.FillRectangle(
                               sbs_state[state],
                               new RectangleF(15.5f - 4 + 114.899f + 92.934f * trainID + 2.5868f * (compressorID + 1) + 20 * compressorID, 182 + 2 + 31.5f * 4 + 5.75f, 20f, 20f)
                               );
                            break;
                        }
                    }
                    dcGs.DrawString(
                        trainID < 3 ? (compressorID + 1).ToString() : (4 - compressorID).ToString(),
                        this._font,
                        Brushs.BlackBrush,
                        new RectangleF(15.5f - 4 + 114.899f + 92.934f * trainID + 2.5868f * (compressorID + 1) + 20 * compressorID, 182 + 2 + 31.5f * 4 + 5.75f, 20f, 20f),
                        FontInfo.SF_CC
                        );
                }
            }
            #endregion
        }
        #endregion
    }
}
