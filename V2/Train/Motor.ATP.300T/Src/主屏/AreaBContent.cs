using System;
using System.Drawing;
using System.Globalization;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.共用;

namespace Motor.ATP._300T.主屏
{
    public class AreaBContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaBContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }

        /// <summary>
        /// B区，速度信息
        /// </summary>
        public void DrawAreaB(Graphics g,AreaBView bView = AreaBView.All)
        {
            if (bView.HasFlag(AreaBView.SpeedValue))
            {
                DrawAreaBSpeedValue(g);
            }

            if (bView.HasFlag(AreaBView.Cir))
            {
                DrawAreaBSpeedCir(g);
            }

            if (bView.HasFlag(AreaBView.ControlModel))
            {
                DrawAreaBControlModel(g);
            }
        }

        public void DrawAreaBControlModel(Graphics g)
        {
            #region ::::::::::: B7区 控制模式

            /*
             * 在B7区以文字方式显示列车模式信息。
             * 列车模式信息与显示文字如表所示。
             * 字体为幼圆大小为16磅（推荐），颜色为白色。
             *--------------------------------------
             * 序号 |    模式           |  显示文本
             * -----|-------------------|-----------
             *   1  |    完全监控模式   |  完全
             * -----|-------------------|-----------
             *   2  |    部分监控模式   |  部分
             * -----|-------------------|-----------
             *   3  |    目视行车模式   |  目视
             * -----|-------------------|-----------
             *   4  |    引导模式       |  引导
             * -----|-------------------|-----------
             *   5  |    调车模式       |  调车
             * -----|-------------------|-----------
             *   6  |    待机模式       |  待机
             * -----|-------------------|-----------
             *   7  |    隔离模式       |  隔离
             * -----|-------------------|-----------
             *   8  |    机车信号模式   |  机信
             * -----|-------------------|-----------
             *   9  |    休眠模式       |  休眠
             * -----|-------------------|-----------
             *  10  |    冒进模式       |  冒进
             * -----|-------------------|-----------
             *  11  |    冒进后模式     |  冒后
             * -------------------------------------
             */
            if (m_ATPMainScreen.CurrentTrainMode != TrainMode.无)
            {
                g.DrawString(m_ATPMainScreen.CurrentTrainMode.ToString(), FontsItems.Font13YouB,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[27], FontsItems.TheAlignment(FontRelated.居中));
            }

            #endregion
        }


        public void DrawAreaBSpeedValue(Graphics g)
        {
            #region ::::::::::: B1区 数字方式显示的列车速度

            /*
             * 假定0度从中心垂直向上，-140度的角度显示0km/h，+140度的角度显示450km/h，在-5度的角度显示150km/h。
             * 将绘制速度刻度的方式分成两段：第一段包括的角度从-140度到-5度，表示从0km/h到150km/h的速度；
             * 第二段包括的角度从-5度到+140度，表示从150km/h到450km/h的速度
             * 字体为Arial大小为16磅（推荐），颜色为白色。
             * 长刻度线的长度为25个像素；短刻度线的长度为15个像素。
             * 
             * 在B1中用数字显示列车速度，数字始终居中显示。
             * 圆形的宽度至少可以显示3位数字，指针的长度不应触到CSG的钩，顶端宽度不应比CSG的钩宽。
             * 字体为Arial大小为22磅（推荐），颜色为黑色
             * 当列车速度超过制动速度时，指针为红色，数字为白色
             * ------------------------------------------------------
             *    控制模式   |         运行状态          |   颜色   
             * --------------|---------------------------|-----------
             *    |          |      Vtrain ≤ Vperm      |   灰色
             *    |          |---------------------------|-----------
             *    |   CSM    |    Vperm < Vtrain ≤Vint  |   橙色
             *    |          |---------------------------|-----------
             *    |          |       Vtrain > Vint       |   红色
             *    |----------|---------------------------|-----------
             *    |          |      Vtrain ≤ Vperm      |   黄色
             *    |          |---------------------------|-----------
             * FS |   TSM    |    Vperm < Vtrain ≤Vint  |   橙色
             *    |          |---------------------------|-----------
             *    |          |        Vtrain > Vint      |   红色
             *    |----------|---------------------------|-----------
             *    |  RSM     |      Vtrain ≤ Vrelease   |   黄色
             *    |          |---------------------------|-----------
             *    |          |       Vtrain > Vrelease   |   红色
             * --------------|---------------------------|-----------
             *               |       Vtrain ≤ Vperm     |   灰色
             *               |---------------------------|-----------
             *    其它模式   |    Vperm < Vtrain ≤Vint  |   橙色
             *               |---------------------------|-----------
             *               |       Vtrain > Vint       |   红色
             * ------------------------------------------------------
             * Vtrain:当前速度      Vperm:允许速度;    Vint:干预速度;    Vrelease:开口速度;     Vtarget:目标速度
             * 
             */

            //表盘
            g.DrawImage(DialPointerImages.表盘_CTCS300T, m_ATPMainScreen.m_RectsList[17]);

            //指针
            m_ATPMainScreen.m_C3ThePointer[Math.Abs(m_ATPMainScreen.m_Vtrain) <= 150f ? 0 : 1].PaintPointerColor(g, m_ATPMainScreen.m_ThePointerImg, Math.Abs(m_ATPMainScreen.m_Vtrain));

            //当列车速度超过制动速度颜色显示红色
            g.DrawString(Convert.ToInt32(m_ATPMainScreen.m_Vtrain).ToString(CultureInfo.InvariantCulture), FontsItems.Font18DefB,
                m_ATPMainScreen.m_Vtrain > m_ATPMainScreen.m_Vint ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush,
                m_ATPMainScreen.m_RectsList[20], FontsItems.TheAlignment(FontRelated.居中));

            #endregion
        }

        /// <summary>
        /// 环形速度光
        /// </summary>
        /// <param name="g"></param>
        public void DrawAreaBSpeedCir(Graphics g)
        {
            #region ::::::::::: B2区 环形速度光带

            /*
             * 沿着速度表盘的外边界，从-145度到+145度显示环形光带，环形光带的角度范围比速度刻度的稍大，即-145度到-140度和140度到145度用于显示光带的最大外边界。
             * 环形光带宽9个像素点，允许速度处的宽度为20个像素点（宽出的部分叫做“钩”）。钩子的尺寸应为6×20。
             * 
             * 在列车未超速情况下，显示器以不同颜色的光带在速度表的表圈上显示目标速度和允许速度。
             * 
             * 在CSM 区，列车未超速情况下，目标速度光带以暗灰色显示；允许速度光带以灰色显示。
             * 
             * 在TSM 区，列车未超速情况下，允许速度光带以黄色显示。
             * 
             * 列车速度超过允许速度、但未超过SBI的情况下，开始以光带方式显示SBI速度，从允许速度到SBI之间光带的宽度为正常光带宽度的两倍，显示颜色为橙色。
             * 
             * 列车速度超过SBI或 EBI时，以光带方式显示紧急制动速度，从允许速度到紧急制动速度之间光带的宽度为正常光带宽度的两倍，颜色为红色；
             * 或者，列车速度超过干预速度（Vint）时，以光带方式显示干预速度，颜色为红色。
             * 
             * 在不同状态下环形光带的显示条件和颜色如表所示
             * --------------------------------------------------------------------------
             *    控制模式   |         运行状态          |             CSG颜色  
             *               |                           |--------------------------------
             *               |                           |   Vperm   |  Vtarget  |   Vint 
             * --------------|---------------------------|-----------|-----------|--------
             *    |          |      Vtrain ≤ Vperm      |    灰色   |   暗灰色  |  不显示
             *    |          |---------------------------|-----------|-----------|--------
             *    |   CSM    |    Vperm < Vtrain ≤Vint  |    黄色   |   暗灰色  |   橙色
             *    |          |---------------------------|-----------|-----------|--------
             *    |          |       Vtrain > Vint       |    黄色   |   暗灰色  |   红色
             *    |----------|---------------------------|-----------|-----------|--------
             *    |          |      Vtrain ≤ Vperm      |    黄色   |   暗灰色  |  不显示
             *    |          |---------------------------|-----------|-----------|--------
             * FS |   TSM    |    Vperm < Vtrain ≤Vint  |    黄色   |   暗灰色  |   橙色
             *    |          |---------------------------|-----------|-----------|--------
             *    |          |        Vtrain > Vint      |    黄色   |   暗灰色  |   红色
             *    |----------|---------------------------|-----------|-----------|--------
             *    |  RSM     |      Vtrain ≤ Vrelease   |    黄色   |   不显示  |  不显示
             *    |          |---------------------------|-----------|-----------|--------
             *    |          |       Vtrain > Vrelease   |    红色   |   不显示  |  不显示
             * --------------|---------------------------|--------------------------------
             *     其它模式  |         所有状态          |            不适用
             * ----------------------------------------------------------------------------
             */

            //超过表盘边界的部分
            //if (ReturnModeAndRunStatus(_theControlMode) < 40)
            //    _C3_theOutCircle[0].PaintCircle(e, 5, 0, PenItems.DarkGrayPen10);


            var theNumbOfControlMode = m_ATPMainScreen.ReturnModeAndRunStatus(m_ATPMainScreen.m_TheControlMode);

            #region :::::::::::::::: 干预速度光带 Vint

            if (theNumbOfControlMode != 11 && theNumbOfControlMode != 22 && theNumbOfControlMode < 30)
            {
                m_ATPMainScreen.m_C3TheVintCircle[0].PaintCircle(g, m_ATPMainScreen.m_Vint > 150 ? 150 : m_ATPMainScreen.m_Vint, m_ATPMainScreen.m_Vperm > 150 ? 150 : m_ATPMainScreen.m_Vperm,
                    (theNumbOfControlMode == 13 || theNumbOfControlMode == 23)
                        ? PenItems.OrangePen15
                        : PenItems.RedPen15);
                if (m_ATPMainScreen.m_Vint > 150)
                {
                    m_ATPMainScreen.m_C3TheVintCircle[1].PaintCircle(g, m_ATPMainScreen.m_Vint, m_ATPMainScreen.m_Vperm,
                        (theNumbOfControlMode == 13 || theNumbOfControlMode == 23)
                            ? PenItems.OrangePen15
                            : PenItems.RedPen15);
                }
            }

            #endregion

            #region ::::::::::::::::: 允许速度光带 Vperm

            if (theNumbOfControlMode < 40)
            {
                if (m_ATPMainScreen.CurrentTrainMode != TrainMode.目视 && m_ATPMainScreen.CurrentTrainMode != TrainMode.部分 &&
                    m_ATPMainScreen.CurrentTrainMode != TrainMode.调车 && m_ATPMainScreen.CurrentTrainMode != TrainMode.机信)
                {
                    //超过表盘边界的部分
                    m_ATPMainScreen.m_C3TheOutCircle[0].PaintCircle(g, 5, 0, PenItems.DarkGrayPen10);

                    m_ATPMainScreen.m_C3TheVpermCircle[0].PaintCircle(g, m_ATPMainScreen.m_Vperm > 150 ? 150 : m_ATPMainScreen.m_Vperm,
                        theNumbOfControlMode == 11
                            ? PenItems.GrayPen9
                            : (theNumbOfControlMode == 34 ? PenItems.RedPen9 : PenItems.YellowPen9));
                    if (m_ATPMainScreen.m_Vperm > 150)
                    {
                        m_ATPMainScreen.m_C3TheVpermCircle[1].PaintCircle(g, m_ATPMainScreen.m_Vperm,
                            theNumbOfControlMode == 11
                                ? PenItems.GrayPen9
                                : (theNumbOfControlMode == 34 ? PenItems.RedPen9 : PenItems.YellowPen9));
                    }
                }

                if (m_ATPMainScreen.m_Vperm >= 0)
                {
                    m_ATPMainScreen.m_C3TheHookCircle[m_ATPMainScreen.m_Vperm > 150 ? 1 : 0].PaintCircleHook(g, m_ATPMainScreen.m_Vperm, 5,
                        theNumbOfControlMode == 11
                            ? PenItems.GrayPen18
                            : (theNumbOfControlMode == 34 ? PenItems.RedPen18 : PenItems.YellowPen9));
                }
            }

            #endregion

            #region ::::::::::::::::::: 目标速度光带 Vtarget

            if (theNumbOfControlMode < 30)
            {
                m_ATPMainScreen.m_C3TheVtargetCircle[0].PaintCircle(g, m_ATPMainScreen.m_Vtarget > 150 ? 150 : m_ATPMainScreen.m_Vtarget, PenItems.DarkGrayPen10);
                if (m_ATPMainScreen.m_Vtarget > 150)
                {
                    m_ATPMainScreen.m_C3TheVtargetCircle[1].PaintCircle(g, m_ATPMainScreen.m_Vtarget, PenItems.DarkGrayPen10);
                }
            }

            #endregion

            #endregion

            #region ::::::::::: B3区/B4区/B5区 命令图标

            /*
             * 命令图标按从左到右的顺序显示在B3/4/5区域内。
             * 当一个区域被占用后，检查下一区域是否也被占用；
             * 如果所有区域都已被占用，生成等待列表。
             * 当要求司机执行一个动作时，用黄色图标显示，并带有黄色闪烁边框，边框的宽度为1个像素，闪烁频率为1Hz，执行了该动作后，显示为灰色。
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb进分相区)])
            {
                g.DrawImage(PlanImages.M6, m_ATPMainScreen.m_RectsList[25]);
            }

            #endregion

            #region ::::::::::: B6区 开口速度(预留)

            /*
             * 当列车设有开口速度并符合条件时，在B6区和B2区显示开口速度，B6区以数字方式显示，字体为Arial大小为16磅（推荐），颜色为灰色
             * 完全监控模式有效时，如果列车设有开口速度，以数字方式和环形速度光带显示开口速度，在其它控制模式下，只以数字方式显示开口速度，
             * 具体显示开口速度的条件和颜色见表所示。
             * --------------------------------------------------------------------
             *    监视区     |         运行状态          |   表盘显示   |  数字显示   
             * --------------|---------------------------|--------------|----------
             *    |   CSM    |         所有状态          |    不显示    |   不显示
             *    |----------|---------------------------|--------------|----------
             *    |          |      Vtrain ≤ Vperm      |     灰色     |   灰色
             *    |          |---------------------------|--------------|----------
             *    |   TSM    |    Vperm < Vtrain ≤Vint  |     灰色     |   灰色
             *    |          |---------------------------|--------------|----------
             * FS |          |       Vtrain > Vint       |     灰色     |   灰色
             *    |----------|---------------------------|--------------|----------
             *    |          |     Vtrain ≤ Vrelease    |     灰色     |   灰色
             *    |   RSM    |---------------------------|--------------|----------
             *    |          |      Vtrain > Vrelease    |     灰色     |   灰色
             * --------------|---------------------------|--------------|----------
             *               |      Vtrain ≤ Vperm      |    不显示    |   不显示
             *   其他模式    |---------------------------|--------------|----------
             *               |      Vtrain >  Vperm      |    不显示    |   灰色
             * --------------------------------------------------------------------
             */

            #endregion
        }
    }
}