using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.运行
{
    /// <summary>
    /// 运行
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public partial class Run : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: 初始化 重载 :::::::::::::::::::::::::

        public override string GetInfo()
        {
            return "运行";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: 事件 重载 :::::::::::::::::::::::::

        public override void paint(Graphics dcGs)
        {
            //GetValue();
            RefereshValue();
            DrawOn(dcGs);
            m_PowerComfirm.OnDraw(dcGs);
            m_ComfirmButton.OnDraw(dcGs);
        }

        private void RefereshValue()
        {

            if (BoolList[1408] && !m_IsPower)//电池电压低故障
            {

                m_ComfirmButton.Visible = true;
                m_PowerComfirm.Visible = true;
                m_IsPower = true;


            }
            else if (!BoolList[1408] && m_IsPower)
            {
                m_ComfirmButton.Visible = false;
                m_PowerComfirm.Visible = false;
                m_IsPower = !m_IsPower;
            }
            else if (BoolList[1408] && m_IsPower)
            {
                if (DataTimeDiff(DateTime.Now, m_ComfirmTime, 60))
                {
                    m_ComfirmButton.Visible = true;
                    m_PowerComfirm.Visible = true;
                }
                else
                {
                    m_ComfirmButton.Visible = false;
                    m_PowerComfirm.Visible = false;
                }
            }
        }

        private bool DataTimeDiff(DateTime t1, DateTime t2, int iPara)
        {
            var ts = t1.Subtract(t2);

            return ts.Ticks / TimeSpan.TicksPerSecond > iPara;
        }
        /// <summary>
        /// 鼠标按下是否在区域内
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            var index = 0;
            if (m_ComfirmButton.OnMouseDown(nPoint))
            {
                return true;
            }

            for (; index < m_Rect.Count; ++index)
            {
                if (m_Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0: //紧急制动
                    if (m_BtnCanDown[0])
                        m_ButtonIsDown[0] = true;
                    break;
                case 1: //牵引封锁
                    if (m_BtnCanDown[1])
                        m_ButtonIsDown[1] = true;
                    break;
                case 2: //紧急牵引
                    if (m_BtnCanDown[2])
                        m_ButtonIsDown[2] = true;
                    break;
                case 3: //限速
                    if (m_BtnCanDown[3])
                    {
                        m_ButtonIsDown[3] = true;

                    }
                    break;
                    ;
                case 4: //其他
                    if (m_BtnCanDown[4])
                    {
                        m_ButtonIsDown[4] = true;

                    }
                    break;
                case 5: //<S
                    if (m_BtnCanDown[5])
                        m_ButtonIsDown[5] = true;
                    break;
                case 6: //S>
                    if (m_BtnCanDown[6])
                        m_ButtonIsDown[6] = true;
                    break;
                case 7: //>喇叭
                    if (m_BtnCanDown[7])
                        m_ButtonIsDown[7] = true;
                    break;
                case 8: //喇叭>
                    if (m_BtnCanDown[8])
                        m_ButtonIsDown[8] = true;
                    break;
                case 9: //手动模式
                    if (m_BtnCanDown[9])
                        m_ButtonIsDown[9] = !m_ButtonIsDown[9];
                    break;
                case 10: //紧急广播
                    if (m_BtnCanDown[10])
                        m_ButtonIsDown[10] = true;
                    break;

                case 11: //站点设置
                    if (m_BtnCanDown[11])
                        m_ButtonIsDown[11] = true;
                    break;
                case 12: //烟火报警
                    if (m_BtnCanDown[12])
                        m_ButtonIsDown[12] = true;
                    break;
                case 13: //旁路信息
                    if (m_BtnCanDown[13])
                        m_ButtonIsDown[13] = true;
                    break;
                default:
                    break;
            }
            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 鼠标弹起是否在区域内
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            var index = 0;
            if (m_ComfirmButton.OnMouseUp(nPoint))
            {
                return true;
            }
            for (; index < m_Rect.Count; ++index)
            {
                if (m_Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0: //紧急制动
                    if (m_BtnCanDown[0])
                    {
                        m_ButtonIsDown[0] = false;
                        HelpInformation.m_MenuID = 0;
                        OnPost(CmdType.ChangePage, 32, 0, 0);

                    }
                    break;
                case 1: //牵引封锁
                    if (m_BtnCanDown[1])
                    {
                        m_ButtonIsDown[1] = false;
                        HelpInformation.m_MenuID = 1;
                        OnPost(CmdType.ChangePage, 32, 0, 0);

                    }
                    break;
                case 2: //紧急牵引
                    if (m_BtnCanDown[2])
                    {
                        m_ButtonIsDown[2] = false;
                        HelpInformation.m_MenuID = 3;
                        OnPost(CmdType.ChangePage, 32, 0, 0);

                    }
                    break;
                case 3: //限速
                    if (m_BtnCanDown[3])
                    {
                        m_ButtonIsDown[3] = false;
                        HelpInformation.m_MenuID = 2;
                        OnPost(CmdType.ChangePage, 32, 0, 0);
                    }
                    break;
                case 4: //其他
                    if (m_BtnCanDown[4])
                    {
                        m_ButtonIsDown[4] = false;
                        HelpInformation.m_MenuID = 4;
                        OnPost(CmdType.ChangePage, 32, 0, 0);
                    }
                    break;
                case 5: //S>
                    if (m_BtnCanDown[5])
                    {
                        m_ButtonIsDown[5] = false;
                    }
                    break;
                case 6: //S<
                    if (m_BtnCanDown[6])
                    {
                        m_ButtonIsDown[6] = false;
                    }
                    break;
                case 7: //>喇叭
                    if (m_BtnCanDown[7])
                    {
                        m_ButtonIsDown[7] = false;
                    }
                    break;
                case 8: //喇叭>
                    if (m_BtnCanDown[8])
                    {
                        m_ButtonIsDown[8] = false;
                    }
                    break;
                case 9: //手动模式
                    if (m_BtnCanDown[9])
                    {
                        m_ButtonIsDown[9] = false;
                    }
                    break;
                case 10: //紧急广播
                    if (m_BtnCanDown[10])
                    {
                        m_ButtonIsDown[10] = false;
                        OnPost(CmdType.ChangePage, 29, 0, 0);
                    }
                    break;
                case 11: //站点设置
                    if (m_BtnCanDown[11])
                    {
                        m_ButtonIsDown[11] = false;

                        OnPost(CmdType.ChangePage, 30, 0, 0);
                    }
                    break;
                case 12: //烟火报警
                    if (m_BtnCanDown[12])
                    {
                        m_ButtonIsDown[12] = false;
                        OnPost(CmdType.ChangePage, 43, 0, 0);
                    }
                    break;
                case 13: //旁路信息
                    if (m_BtnCanDown[13])
                    {
                        m_ButtonIsDown[13] = false;

                        OnPost(CmdType.ChangePage, 31, 0, 0);
                    }

                    break;
                default:
                    break;
            }
            return base.mouseUp(nPoint);
        }

        #endregion

        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="e"></param>
        //框

        private void DrawFrame(Graphics e)
        {
            for (var i = 0; i < 3; i++)
            {
                e.DrawRectangle(FormatStyle.m_WhitePen, Rectangle.Round(m_LineRectArr[1 + i]));
            }
            //六个门的底图灰色框
            for (var i = 0; i < 6; i++)
            {
                e.FillRectangle(FormatStyle.m_GreyBrush, 45 + i * 120, 155, 110, 255);

            }
            //六个门中间相隔灰色小框
            for (var i = 0; i < 5; i++)
            {
                e.FillRectangle(FormatStyle.m_GreyBrush, 157 + i * 120, 160, 8, 245);
            }
            //六个门右边的白色直线
            for (var i = 0; i < 6; i++)
            {
                e.FillRectangle(FormatStyle.m_WhiteBrush, 45 + i * 120, 155, 1, 255);
            }
            //提示信息的5个矩形框
            for (var i = 0; i < 5; i++)
            {
                e.DrawRectangle(FormatStyle.m_MediumGreyPen, m_LineRectArr[i + 4].X, m_LineRectArr[i + 4].Y,
                    m_LineRectArr[i + 4].Width, m_LineRectArr[i + 4].Height);
            }
            // 画白色48个矩形框
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    for (var k = 0; k < 4; k++)
                    {
                        e.DrawRectangle(FormatStyle.m_WhitePen, 48 + i * 120 + k * 25, 160 + j * 220, 25, 25);
                    }

                }
            }

            //填充边部
            for (var i = 0; i < 6; i++)
            {
                e.FillRectangle(FormatStyle.m_WhiteBrush, 45 + i * 120, 155, 2, 255);

            }
            //填充顶部
            for (var i = 0; i < 6; i++)
            {
                e.FillRectangle(FormatStyle.m_WhiteBrush, 45 + i * 120, 155, 110, 2);

            }
        }

        //<summary>
        //填充数值
        //</summary>
        //<param name="e"></param>
        //空压机 /牵引系统 //切除—>故障—>通讯故障—>正常

        private void DrawValue(Graphics e)
        {
            #region ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

            for (var i = 0; i < 2; i++)
            {
                if (BoolList[m_BoolIds[11] + i * 2] && !BoolList[m_BoolIds[13] + i * 2])
                    e.DrawImage(m_Images[69], m_Rectangles[46 + i]);
                else if (!BoolList[m_BoolIds[11] + i * 2] && BoolList[m_BoolIds[13] + i * 2])
                    e.DrawImage(m_Images[70], m_Rectangles[46 + i]);
                else
                    e.DrawImage(m_Images[71], m_Rectangles[46 + i]);
            }


            for (var i = 0; i < 4; i++)
            {
                if (BoolList[m_BoolIds[15] + i * 4])
                    e.DrawImage(m_Images[62], m_Rectangles[i + 38]);
                else if (BoolList[m_BoolIds[19] + i * 4])
                    e.DrawImage(m_Images[63], m_Rectangles[i + 38]);
                else if (BoolList[m_BoolIds[23] + i * 4])
                    e.DrawImage(m_Images[64], m_Rectangles[i + 38]);
                else if (BoolList[m_BoolIds[27] + i * 4])
                    e.DrawImage(m_Images[65], m_Rectangles[i + 38]);
            }
        }

        /// <summary>
        /// //乘客报警//故障—>报警—>对讲—>正常       
        /// </summary>
        /// <param name="e">绘图对象</param>
        private void DrawP(Graphics e)
        {
            for (var i = 0; i < 12; i++)
            {
                if (BoolList[m_BoolIds[55] + i * 4])
                    e.DrawImage(m_Images[39], m_Rectangles[i]);
                else if (BoolList[m_BoolIds[43] + i * 4])
                    e.DrawImage(m_Images[40], m_Rectangles[i]);
                else if (BoolList[m_BoolIds[31] + i * 4])
                    e.DrawImage(m_Images[41], m_Rectangles[i]);
                else if (BoolList[m_BoolIds[67] + i * 4])
                    e.DrawImage(m_Images[38], m_Rectangles[i]);
            }
        }

        private void DrawSiv(Graphics g)
        {
            for (var i = 0; i < 2; i++)
            {
                if (BoolList[m_BoolIds[222 + i * 3]])
                {
                    g.DrawImage(m_Images[73], m_Rectangles[48 + i]);
                }
                else if (BoolList[m_BoolIds[223 + i * 3]])
                {
                    g.DrawImage(m_Images[74], m_Rectangles[48 + i]);
                }
                else if (BoolList[m_BoolIds[224 + i * 3]])
                {
                    g.DrawImage(m_Images[75], m_Rectangles[48 + i]);
                }
            }
        }
        /// <summary>
        /// //BHB//BLB
        /// </summary>
        /// <param name="e">绘图对象</param>
        private void DrawB(Graphics e)
        {
            for (var i = 0; i < 2; i++)
            {
                if (BoolList[m_BoolIds[79] + i * 2])
                    e.DrawImage(m_Images[55], m_Rectangles[30 + i]);
                else if (BoolList[m_BoolIds[81] + i * 2])
                    e.DrawImage(m_Images[56], m_Rectangles[30 + i]);
                else
                {
                    e.DrawImage(m_Images[57], m_Rectangles[30 + i]);
                }



                if (BoolList[m_BoolIds[83] + i * 2])
                    e.DrawImage(m_Images[58], m_Rectangles[32 + i]);
                else if (BoolList[m_BoolIds[85] + i * 2])
                    e.DrawImage(m_Images[59], m_Rectangles[32 + i]);
                else
                {
                    e.DrawImage(m_Images[80], m_Rectangles[32 + i]);
                }

            }

            if (BoolList[1410])
            {
                e.DrawImage(m_Images[58], m_Rectangles[50]);
            }
            else if (BoolList[1411])
            {
                e.DrawImage(m_Images[59], m_Rectangles[50]);
            }
            else
            {
                e.DrawImage(m_Images[80], m_Rectangles[50]);
            }

            for (var i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    if (BoolList[m_BoolIds[87] + i * 2])
                        e.DrawImage(m_Images[46], m_Rectangles[29]);
                    else if (BoolList[m_BoolIds[93] + i * 2])
                        e.DrawImage(m_Images[47], m_Rectangles[29]);
                    else if (BoolList[m_BoolIds[228 + i]])
                        e.DrawImage(m_Images[76], m_Rectangles[29]);
                }

                if (i != 0 && i != 5)
                {

                    if (BoolList[m_BoolIds[87] + i * 2])
                        e.DrawImage(m_Images[46], m_Rectangles[23 + i]);
                    else if (BoolList[m_BoolIds[93] + i * 2])
                        e.DrawImage(m_Images[47], m_Rectangles[23 + i]);
                    else if (BoolList[m_BoolIds[228 + i]])
                        e.DrawImage(m_Images[76], m_Rectangles[23 + i]);
                }
                if (i == 5)
                {
                    if (BoolList[m_BoolIds[87] + i * 2])
                        e.DrawImage(m_Images[46], m_Rectangles[28]);
                    else if (BoolList[m_BoolIds[93] + i * 2])
                        e.DrawImage(m_Images[47], m_Rectangles[28]);
                    else if (BoolList[m_BoolIds[228 + i]])
                        e.DrawImage(m_Images[76], m_Rectangles[28]);
                }

            }
        }

        /// <summary>
        ///   //空气制动 //主断状态 //客室温度  ////电制动可用性
        /// </summary>
        /// <param name="e">绘图对象</param>
        private void DrawR(Graphics e)
        {


            for (var i = 0; i < 12; i++)
            {
                if (BoolList[m_BoolIds[99 + i * 3]])
                    e.DrawImage(m_Images[45], m_Rectangles[i + 12]);
                else if (BoolList[m_BoolIds[100] + i * 3])
                    e.DrawImage(m_Images[42], m_Rectangles[i + 12]);
                else if (BoolList[m_BoolIds[101] + i * 3])
                    e.DrawImage(m_Images[43], m_Rectangles[i + 12]);
                else if (BoolList[m_BoolIds[234 + i]])
                    e.DrawImage(m_Images[77], m_Rectangles[i + 12]);
                else
                    e.DrawImage(m_Images[44], m_Rectangles[i + 12]);
            }


            for (var i = 0; i < 4; i++)
            {
                if (BoolList[m_BoolIds[135] + i * 2])
                    e.DrawImage(m_Images[66], m_Rectangles[i + 42]);
                else if (BoolList[m_BoolIds[139] + i * 2])
                    e.DrawImage(m_Images[67], m_Rectangles[i + 42]);
                else
                    e.DrawImage(m_Images[68], m_Rectangles[i + 42]);
            }


            //for (int i = 0; i < 6; i++)
            //{
            e.DrawString(FloatList[m_FoolatIds[0]].ToString("0.0℃"), FormatStyle.m_Font14,
                FormatStyle.m_WhiteBrush, m_LineRectArr[3], m_DrawFormat);
            //}


            for (var i = 0; i < 4; i++)
            {

                e.DrawImage(BoolList[m_BoolIds[143 + i]] ? m_Images[60] : m_Images[61], m_Rectangles[34 + i]);

            }

        }

        /// <summary>
        ///  //画门的名字的位置
        /// </summary>
        /// <param name="e">绘图对象</param>
        public void DrawM(Graphics e)
        {
            for (var i = 0; i < 6; i++)
            {
                e.DrawString(FormatStyle.m_Str30[i], FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, 90 + i * 120, 130);
            }
        }

        /// <summary>
        /// 画主界面的字
        /// </summary>
        /// <param name="e">绘图对象</param>
        private void DrawE(Graphics e)
        {
            e.DrawString("模式:", FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, 50, 445, m_DrawFormat);
            e.DrawString("工况和级位:", FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, 280, 445, m_DrawFormat);
            e.DrawString("空调室内温度:", FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, 540, 445, m_DrawFormat);
            e.DrawString("提示信息:", FormatStyle.m_Font12, FormatStyle.m_WhiteBrush, 100, 490, m_DrawFormat);
            //e.DrawString("0℃", FormatStyle.Font12B, FormatStyle.WhiteBrush, _lineRectArr[3], drawFormat);
            //e.DrawImage(_images[36], 2, 155, 37, 255);
            //e.DrawImage(_images[37], 756, 155, 37, 255);
            e.FillRectangle(FormatStyle.m_GreenBrush, 40, 170, 6, 220);//左门车门占有的颜色
            e.FillRectangle(FormatStyle.m_GreenBrush, 752, 170, 6, 220);//右门车门占用的颜色

            for (var i = 0; i < 5; i++)
            {

                e.DrawString(FormatStyle.m_Str22[i], FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[4 + i],
                    m_DrawFormat);
            }

            ////工况1模式显示
            var tmp = "";
            for (var i = 0; i < 5; i++)
            {
                if (BoolList[m_BoolIds[147 + i]])
                {
                    tmp = FormatStyle.m_Str18[i];
                    break;
                }
                if ((BoolList[m_BoolIds[152 + i]]))
                {
                    tmp = FormatStyle.m_Str18[i + 5];
                    break;

                }
                tmp = "未知";
            }
            e.DrawString(tmp, FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[1],
                m_DrawFormat);
            //工况2
            if (BoolList[m_BoolIds[221]])
                e.DrawString("牵引 - " + Convert.ToInt32(FloatList[m_FoolatIds[6]]).ToString() + "%",
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[2], m_DrawFormat);
            else if (BoolList[m_BoolIds[167]])
                e.DrawString("快速制动 - " + Convert.ToInt32(FloatList[m_FoolatIds[6]]).ToString() + "%",
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[2], m_DrawFormat);
            else if (BoolList[m_BoolIds[168]])
                e.DrawString("惰行 - " + Convert.ToInt32(FloatList[m_FoolatIds[6]]).ToString() + "%",
                    FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[2], m_DrawFormat);
            else if (BoolList[m_BoolIds[169]])
                e.DrawString(FormatStyle.m_Str19[0] + " - " + Convert.ToInt32(FloatList[m_FoolatIds[6]]).ToString() + "%"
                    , FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[2], m_DrawFormat);
            else if (BoolList[m_BoolIds[170]])
                e.DrawString(FormatStyle.m_Str19[1] + " - " + Convert.ToInt32(FloatList[m_FoolatIds[6]]).ToString() + "%"
                    , FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[2], m_DrawFormat);

            else if (BoolList[m_BoolIds[171]])
                e.DrawString(FormatStyle.m_Str19[2] + " - " + Convert.ToInt32(FloatList[m_FoolatIds[6]]).ToString() + "%"
                    , FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[2], m_DrawFormat);

            else if (BoolList[m_BoolIds[172]])
                e.DrawString(FormatStyle.m_Str19[3] + " - " + Convert.ToInt32(FloatList[m_FoolatIds[6]]).ToString() + "%"
                    , FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[2], m_DrawFormat);

            else
            {
                e.DrawString("未知-0%", FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush, m_LineRectArr[2], m_DrawFormat);
            }

            #endregion

            //紧急广播、站点设置
            for (var i = 0; i < 1; i++)
            {
                if (m_ButtonIsDown[i + 10])
                {
                    e.DrawImage(m_Images[21], m_BtnRectArr[i + 10]);
                }
                else
                {
                    e.DrawImage(m_Images[22], m_BtnRectArr[i + 10]);
                }
                e.DrawString(FormatStyle.m_站点按键[i], FormatStyle.m_Font12B,
                    FormatStyle.m_BlackBrush, m_BtnRectArr[i], m_DrawFormat);
            }

            //旁路信息
            e.DrawImage(m_Images[21], m_BtnRectArr[13]);
            e.DrawImage(m_Images[22], m_Rects[0]);
            for (var i = 0; i < 11; i++)
            {
                if (!BoolList[m_BoolIds[i]])
                    e.DrawImage(m_Images[21], m_BtnRectArr[13]);
                else
                {
                    e.DrawImage(m_Images[22], m_BtnRectArr[13]);
                    break;
                }
            }
            //TODO 烟火报警按钮变色，缺少将图片给颜色
            if (m_FirListInbool.Any(a => BoolList[a]))
            {
                for (var i = 0; i < m_FirListInbool.Count; i++)
                {

                    if (!BoolList[m_FirListInbool[i]] || i % 3 == 0)
                    {
                        if (i % 3 == 0)
                        {
                            if (!BoolList[m_FirListInbool[i]] && !BoolList[m_FirListInbool[i + 1]] && !BoolList[m_FirListInbool[i + 2]])
                            {
                                e.DrawImage(m_Images[78], m_BtnRectArr[12]);
                                break;
                            }
                        }
                        e.DrawImage(m_Images[21], m_BtnRectArr[12]);
                    }
                    else
                    {
                        e.DrawImage(m_Images[78], m_BtnRectArr[12]);
                        break;
                    }
                }
            }
            else
            {
                e.DrawImage(m_Images[78], m_BtnRectArr[12]);
            }

            e.DrawString("旁路信息", FormatStyle.m_Font12B, FormatStyle.m_BlackBrush,
                m_BtnRectArr[13], m_DrawFormat);
            e.DrawString("烟火报警", FormatStyle.m_Font12B,
                           FormatStyle.m_BlackBrush, m_BtnRectArr[12], m_DrawFormat);
            //声音
            for (var i = 0; i < 4; i++)
            {
                if (m_BtnCanDown[i + 5])
                {
                    if (m_ButtonIsDown[i + 5])
                    {
                        e.DrawImage(m_Images[22], m_BtnRectArr[i + 5]);
                    }
                    else
                    {
                        e.DrawImage(m_Images[72], m_BtnRectArr[i + 5]);
                    }
                }
                e.DrawImage(m_Images[23 + i], 10 + i * 88, 515, 40, 20);

            }

            //手动模式，烟火报警，紧急广播，站点设置
            for (var i = 9; i < 12; i++)
            {
                if (m_BtnCanDown[i])
                {
                    e.DrawImage(m_ButtonIsDown[i] ? m_Images[22] : m_Images[21], m_BtnRectArr[i]);

                    switch (i)
                    {
                        case 9:
                            e.DrawString("手动模式", FormatStyle.m_Font12B,
                                FormatStyle.m_BlackBrush, m_BtnRectArr[9], m_DrawFormat);
                            break;
                        case 12:
                            e.DrawString("烟火报警", FormatStyle.m_Font12B,
                                FormatStyle.m_BlackBrush, m_BtnRectArr[12], m_DrawFormat);
                            break;
                        case 10:
                            e.DrawString("紧急广播", FormatStyle.m_Font12B,
                                FormatStyle.m_BlackBrush, m_BtnRectArr[10], m_DrawFormat);
                            break;
                        case 11:
                            e.DrawString("站点设置", FormatStyle.m_Font12B,
                                FormatStyle.m_BlackBrush, m_BtnRectArr[11], m_DrawFormat);
                            break;
                    }
                }
                else
                {
                    e.DrawImage(m_Images[22], m_BtnRectArr[4]);

                    switch (i)
                    {
                        case 9:
                            e.DrawString("手动模式", FormatStyle.m_Font12B,
                                FormatStyle.m_HalfPGreySolidBrush, m_BtnRectArr[9], m_DrawFormat);
                            break;
                        case 12:
                            e.DrawString("烟火报警", FormatStyle.m_Font12B,
                                FormatStyle.m_HalfPGreySolidBrush, m_BtnRectArr[12], m_DrawFormat);
                            break;
                        default:
                            e.DrawImage(m_Images[29], m_BtnRectArr[0]);
                            break;
                    }
                }
            }

            foreach (var doorState in m_DoorStatesList)
            {
                doorState.DrawDoorState(e);
            }
        }
        /// <summary>
        /// 当在紧急制动，牵引封锁，限速条件，紧急牵引，其他
        /// 二级界面的内容被选中后背景颜色变红色。
        /// </summary>
        /// <param name="e"></param>
        private void DrawBground(Graphics e)
        {
            //紧急制动
            if (HelpInformation.m_HelpInfoDic[HelpInfoMenu.MenuOne].Any(a => BoolList[a]) && Flicker())
            {
                e.FillRectangle(FormatStyle.m_RedBrush, m_LineRectArr[4]);
            }
            if (HelpInformation.m_HelpInfoDic[HelpInfoMenu.MenuTwo].Any(a => BoolList[a]) && Flicker())
            {
                e.FillRectangle(FormatStyle.m_RedBrush, m_LineRectArr[5]);
            }
            if (HelpInformation.m_HelpInfoDic[HelpInfoMenu.MenuThree].Any(a => BoolList[a]) && Flicker())
            {
                e.FillRectangle(FormatStyle.m_RedBrush, m_LineRectArr[7]);
            }
            if (HelpInformation.m_HelpInfoDic[HelpInfoMenu.MenuFour].Any(a => BoolList[a]) && Flicker())
            {
                e.FillRectangle(FormatStyle.m_RedBrush, m_LineRectArr[6]);
            }
            if (HelpInformation.m_HelpInfoDic[HelpInfoMenu.MenuFive].Any(a => BoolList[a]) && Flicker())
            {
                e.FillRectangle(FormatStyle.m_RedBrush, m_LineRectArr[8]);
            }
        }

        bool Flicker()
        {
            return DateTime.Now.Second % 2 == 0;
        }
        /// <summary>
        /// 调用各个绘图的方法
        /// </summary>
        /// <param name="e">绘图对象</param>
        private void DrawOn(Graphics e)
        {

            DrawBground(e);
            GetValue();
            DrawFrame(e);
            DrawValue(e);
            DrawB(e);
            DrawE(e);
            DrawP(e);
            DrawR(e);
            DrawM(e);
            DrawSiv(e);

        }

        /// <summary>
        /// 传递门的7种状态
        /// </summary>
        private void GetValue()
        {
            foreach (var i in m_DoorStatesList)
            {
                var tmp = new bool[7];
                for (var j = 0; j < tmp.Length; j++)
                {
                    tmp[j] = BoolList[i.LogicIdArr[j]];
                }
                i.DoorStateUpdata(tmp);
            }
        }

        /// <summary>
        /// 门的位置初始化
        /// </summary>
        private void InitRect()
        {
            m_RectsList = new List<RectangleF>();
            int index;
            //车厢号    /0-5
            for (index = 0; index < 6; index++)
            {
                m_RectsList.Add(new RectangleF(38 + index * 100, 90, 100, 25));
            }

            for (var j = 0; j < 2; j++)
            {
                for (index = 0; index < 6; index++)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        m_RectsList.Add(new RectangleF(49 + index * 120 + i * 25, 161 + j * 220, 24, 23));
                    }
                }
            }
        }
    }
}


