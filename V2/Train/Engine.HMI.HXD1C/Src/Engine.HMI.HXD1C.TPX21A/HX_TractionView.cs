using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_TractionView : baseClass, IButtonEventListener
    {
        private Rectangle m_Rect; //主界面基本信息显示区域
        private readonly HxRectText[] m_TxtTruckBolster = new HxRectText[2]; //转向架
        private readonly Rectangle[] m_InfoRect = new Rectangle[6]; //显示转向架牵引力/制动力的柱状图标
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>(); //界面中需要的界面元素
        private Rectangle m_IconRect; //图标信息显示区
        private readonly Rectangle[] m_ChildiconsRect = new Rectangle[8]; //图标区各子信息区的初始化
        private readonly HxRectText[] m_TxtStatus = new HxRectText[2]; //速度 牵引制动 状态显示图标

        private readonly bool[] m_IsNumButtonDown = new bool[10]; //底部数字按钮是否按下
        private readonly bool[] m_IsTrainDirection = new bool[3]; //机车方向
        private readonly bool[] m_IsAlert = new bool[2]; //警惕装置
        private readonly bool[] m_IsTrainBrake = new bool[5]; //机车制动
        private readonly bool[] m_IsStopBrake = new bool[4]; //停车制动
        private readonly bool[] m_IsCurrentCollector1 = new bool[3]; //受电弓1状态
        private readonly bool[] m_IsCurrentCollector2 = new bool[3]; //受电弓2状态
        private readonly TriangleMark[] m_TMark = new TriangleMark[6]; //三角图标
        private readonly bool[] m_IsElectricMachine = new bool[4]; //电机状态 
        private readonly int[] m_TargetTractionN = new int[6]; //目标牵引力
        private readonly int[] m_TargetBrakeN = new int[6]; //目标制动力
        private readonly int[] m_RealTractionN = new int[6]; //实际牵引力
        private readonly int[] m_RealBrakeN = new int[6]; //实际制动力

        private readonly int[] m_Value = new int[3]; //速度 牵引、制动力

        public override string GetInfo()
        {
            return "牵引数据视图";
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 4)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int) (btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 4)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            //加载图片
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);
            }
            nErrorObjectIndex = -1;

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void InitData()
        {
            m_Rect = new Rectangle(HxCommon.Recposition.X + 3, HxCommon.Recposition.Y + 30, 600, 455);
            m_IconRect = new Rectangle(m_Rect.Right + 4, m_Rect.Y, 195, 455);

            //状态信息区边框位置初始化
            for (int i = 0; i < 8; i++)
            {
                if (i < 2)
                {
                    m_ChildiconsRect[i] = new Rectangle(m_IconRect.X + 2, m_IconRect.Y + 2 + 91*i, 188, 89);
                }
                else
                {
                    if (i%2 == 0)
                    {
                        m_ChildiconsRect[i] = new Rectangle(m_IconRect.X + 2, m_IconRect.Y + 2 + 91*2 + (i - 2)/2*91, 93, 89);
                    }
                    else
                    {
                        m_ChildiconsRect[i] = new Rectangle(m_IconRect.X + 98, m_IconRect.Y + 2 + 91*2 + (i - 3)/2*91, 93, 89);
                    }
                }
            }

            //柱状信息显示区域的边框位置初始化
            for (int i = 0; i < 6; i++)
            {
                if (i < 3)
                {
                    m_InfoRect[i] = new Rectangle(m_Rect.X + 95 + i*75, m_Rect.Y + 35, 40, 380);
                }
                else
                {
                    m_InfoRect[i] = new Rectangle(m_Rect.X + 380 + (i - 3)*75, m_Rect.Y + 35, 40, 380);
                }

            }

            //转向架的文本框的初始化
            for (int i = 0; i < 2; i++)
            {
                m_TxtTruckBolster[i] = new HxRectText();
                m_TxtTruckBolster[i].SetBkColor(0, 0, 0);
                m_TxtTruckBolster[i].SetTextRect(m_Rect.X + 60 + 280*i, m_Rect.Y + 420, 220, 30);
                m_TxtTruckBolster[i].SetDrawFrm(true);
                m_TxtTruckBolster[i].SetLinePen(84, 84, 84, 1);
                m_TxtTruckBolster[i].SetText("转向架" + (i + 1).ToString());
                m_TxtTruckBolster[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_TxtTruckBolster[i].SetTextColor(255, 255, 255);
            }

            for (int i = 0; i < 2; i++)
            {
                m_TxtStatus[i] = new HxRectText();
                m_TxtStatus[i].SetBkColor(0, 0, 0);
                m_TxtStatus[i].SetDrawFrm(true);
                m_TxtStatus[i].SetTextColor(255, 255, 255);
                m_TxtStatus[i].SetTextStyle(14, FormatStyle.Center, true, "宋体");
                m_TxtStatus[i].SetTextRect(m_ChildiconsRect[i].X + 4, m_ChildiconsRect[i].Y + 45, 100, 35);
            }

            //三角图标的初始化
            for (int i = 0; i < 6; i++)
            {
                m_TMark[i] = new TriangleMark(m_InfoRect[i].X + 22, m_InfoRect[i].Bottom);
            }

        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("牵引数据");

            HxCommon.ButtonText[0].SetText("主要数据");
            HxCommon.ButtonText[1].SetText("");
            HxCommon.ButtonText[2].SetText("");
            HxCommon.ButtonText[3].SetText("");
            HxCommon.ButtonText[4].SetText("列车重量");
            HxCommon.ButtonText[5].SetText("机车配置");
            HxCommon.ButtonText[6].SetText("牵引数据");
            HxCommon.ButtonText[7].SetText("");
            HxCommon.ButtonText[8].SetText(" ");
            HxCommon.ButtonText[9].SetText("主界面");
            for (int i = 0; i < 10; i++)
            {
                if (i == 6)
                {
                    HxCommon.ButtonText[i].SetBkColor(255, 255, 0);
                    HxCommon.ButtonText[i].SetTextColor(0, 0, 0);
                }
                else
                {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                }
            }

            #region 各状态信息Bool 量的读入

            //机车方向
            for (int i = 0; i < 3; i++)
            {
                m_IsTrainDirection[i] = BoolList[UIObj.InBoolList[i]];
            }

            //警惕装置
            for (int i = 0; i < 2; i++)
            {
                m_IsAlert[i] = BoolList[UIObj.InBoolList[i + 3]];
            }

            //机车制动
            for (int i = 0; i < 4; i++)
            {
                m_IsTrainBrake[i] = BoolList[UIObj.InBoolList[i + 5]];
            }
            m_IsTrainBrake[4] = BoolList[UIObj.InBoolList[UIObj.InBoolList.Count - 1]];

            //停放制动 
            for (int i = 0; i < 4; i++)
            {
                m_IsStopBrake[i] = BoolList[UIObj.InBoolList[i + 9]];
            }

            //底部数字按钮
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i + 13]];
            }

            //受电弓1
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector1[i] = BoolList[UIObj.InBoolList[i + 23]];
            }

            //受电弓2
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector2[i] = BoolList[UIObj.InBoolList[i + 26]];
            }

            //电机状态
            for (int i = 0; i < 4; i++)
            {
                m_IsElectricMachine[i] = BoolList[UIObj.InBoolList[i + 29]];
            }

            #endregion

            #region;;;;;牵引力 制动力 读入 更新

            for (int i = 0; i < 12; i++)
            {
                if (i%2 == 0) //读入目标牵引力
                {
                    m_TargetTractionN[i/2] = Convert.ToInt32(FloatList[UIObj.InFloatList[i]]);

                    if (m_TargetTractionN[i/2] > 100) //读入的值为 大于100 或小于0的异常值时 使其等于 0 100
                    {
                        m_TargetTractionN[i/2] = 100;
                    }

                    if (m_TargetTractionN[i/2] < 0)
                    {
                        m_TargetTractionN[i/2] = 0;
                    }
                }
                else //读入实际制动力
                {
                    m_TargetBrakeN[i/2] = Convert.ToInt32(FloatList[UIObj.InFloatList[i]]);

                    if (m_TargetBrakeN[i/2] > 100)
                    {
                        m_TargetBrakeN[i/2] = 100;
                    }

                    if (m_TargetBrakeN[i/2] < 0)
                    {
                        m_TargetBrakeN[i/2] = 0;
                    }
                }

                if (i%2 == 0) //读入实际牵引力
                {
                    m_RealTractionN[i/2] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 12]]);

                    if (m_RealTractionN[i/2] > 100) //读入的值为 大于100 或小于0的异常值时 使其等于 0 100
                    {
                        m_RealTractionN[i/2] = 100;
                    }

                    if (m_RealTractionN[i/2] < 0)
                    {
                        m_RealTractionN[i/2] = 0;
                    }
                }
                else //读入实际制动力
                {
                    m_RealBrakeN[i/2] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 12]]);

                    if (m_RealBrakeN[i/2] > 100) //读入的值为 大于100 或小于0的异常值时 使其等于 0 100
                    {
                        m_RealBrakeN[i/2] = 100;
                    }

                    if (m_RealBrakeN[i/2] < 0)
                    {
                        m_RealBrakeN[i/2] = 0;
                    }
                }
            }

            #endregion

            //速速 牵引力 制动力读入
            for (int i = 0; i < 3; i++)
            {
                m_Value[i] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 24]]);
            }
            m_TxtStatus[0].SetText(m_Value[0].ToString());

            if (m_Value[1] > 0)
            {
                m_TxtStatus[1].SetTextColor(0, 0, 255);
                m_TxtStatus[1].SetText(m_Value[1].ToString());
            }
            else
            {
                m_TxtStatus[1].SetTextColor(255, 0, 0);
                m_TxtStatus[1].SetText(m_Value[2].ToString());
            }


            //根据更新的牵引力制动力改变三角图标位置
            for (int i = 0; i < 6; i++)
            {
                if (m_TargetTractionN[i] == 0 && m_TargetBrakeN[i] == 0)
                {
                    m_TMark[i].SetColor(TriangleMark.ColorType.Gray);
                    m_TMark[i].SetPosition(m_InfoRect[i].X + 22, m_InfoRect[i].Bottom);
                }
                else
                {
                    if (m_TargetTractionN[i] != 0) //牵引力不为0 背景为蓝色
                    {
                        m_TMark[i].SetColor(TriangleMark.ColorType.Blue);
                        m_TMark[i].SetPosition(m_InfoRect[i].X + 22,
                            m_InfoRect[i].Bottom - m_TargetTractionN[i]*m_InfoRect[i].Height/108);
                    }
                    else
                    {
                        m_TMark[i].SetColor(TriangleMark.ColorType.Red);
                        m_TMark[i].SetPosition(m_InfoRect[i].X + 22,
                            m_InfoRect[i].Bottom - m_TargetBrakeN[i]*m_InfoRect[i].Height/108);
                    }
                }
            }
        }

        public void DrawOn(Graphics g)
        {
            g.DrawRectangle(HxCommon.LinePen1, m_Rect);
            g.DrawRectangle(HxCommon.LinePen1, m_IconRect);

            //绘制子图标区的边框
            for (int i = 0; i < 8; i++)
            {
                g.DrawRectangle(HxCommon.LinePen1, m_ChildiconsRect[i]);
            }

            for (int i = 0; i < 2; i++)
            {
                m_TxtTruckBolster[i].OnDraw(g);
            }

            #region 绘 制 刻 度 线

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (j%5 == 0)
                    {
                        g.DrawLine(HxCommon.WhitePen2, m_InfoRect[i].X - 15.5f,
                            m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f,
                            m_InfoRect[i].X - 2.5f, m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f);

                        if (i == 0 || i == 3)
                        {
                            if (j == 25)
                            {
                                g.DrawString((j*4).ToString(), HxCommon.Font14, HxCommon.WhiteBrush,
                                    m_InfoRect[i].X - 50,
                                    m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f - 10);
                            }
                            else if (j == 0)
                            {
                                g.DrawString("0", HxCommon.Font14, HxCommon.WhiteBrush, m_InfoRect[i].X - 30,
                                    m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f - 15);
                            }
                            else
                            {
                                g.DrawString((j*4).ToString(), HxCommon.Font14, HxCommon.WhiteBrush,
                                    m_InfoRect[i].X - 40,
                                    m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f - 10);
                            }

                            g.DrawString("[%]", HxCommon.Font14, HxCommon.WhiteBrush, m_InfoRect[i].X - 45,
                                m_InfoRect[i].Y + 55);
                        }
                    }
                    else
                    {
                        g.DrawLine(HxCommon.WhitePen2, m_InfoRect[i].X - 10.5f,
                            m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f,
                            m_InfoRect[i].X - 2.5f, m_InfoRect[i].Bottom - 0.1f - j*m_InfoRect[i].Height/27.0f);
                    }
                }

                g.DrawLine(HxCommon.LightWhitePen1, m_InfoRect[i].X - 2.5f, m_InfoRect[i].Bottom, m_InfoRect[i].X - 2.5f,
                    m_InfoRect[i].Y);
            }

            g.DrawString("牵引", HxCommon.Font14B, HxCommon.BlueBrush, m_InfoRect[2].X + 30, m_InfoRect[2].Y - 25);
            g.DrawString("/", HxCommon.Font14B, HxCommon.GrayBrush, m_InfoRect[2].X + 68, m_InfoRect[2].Y - 25);
            g.DrawString("制动", HxCommon.Font14B, HxCommon.RedBrush, m_InfoRect[2].X + 83, m_InfoRect[2].Y - 25);

            #endregion

            #region 状态区各状态的绘制

            //速度 制动牵引状态的绘制
            for (int i = 0; i < 2; i++)
            {
                m_TxtStatus[i].OnDraw(g);
            }

            g.DrawString("设定速度", HxCommon.Font14B, HxCommon.WhiteBrush, m_ChildiconsRect[0].X + 25,
                m_ChildiconsRect[0].Y + 15);
            g.DrawString("牵引", HxCommon.Font14B, HxCommon.BlueBrush, m_ChildiconsRect[1].X + 20,
                m_ChildiconsRect[1].Y + 15);
            g.DrawString("/", HxCommon.Font14B, HxCommon.GrayBrush, m_ChildiconsRect[1].X + 58,
                m_ChildiconsRect[1].Y + 15);
            g.DrawString("制动", HxCommon.Font14B, HxCommon.RedBrush, m_ChildiconsRect[1].X + 73,
                m_ChildiconsRect[1].Y + 15);
            g.DrawString("km/h", HxCommon.Font14B, HxCommon.WhiteBrush, m_ChildiconsRect[0].X + 108,
                m_ChildiconsRect[0].Y + 45);
            g.DrawString("kN", HxCommon.Font14B, HxCommon.WhiteBrush, m_ChildiconsRect[1].X + 108,
                m_ChildiconsRect[1].Y + 45);

            //绘制机车方向信息
            for (int i = 0; i < 3; i++)
            {
                if (m_IsTrainDirection[i])
                {
                    g.DrawImage(m_ImageList[i], m_ChildiconsRect[2].X + 5, m_ChildiconsRect[2].Y + 5,
                        m_ChildiconsRect[2].Width - 10, m_ChildiconsRect[2].Height - 10);
                    break;
                }
                else
                {
                }
            }

            //绘制警惕装置状态信息
            for (int i = 0; i < 2; i++)
            {
                if (m_IsAlert[i])
                {
                    g.DrawImage(m_ImageList[i + 3], m_ChildiconsRect[4].X + 5, m_ChildiconsRect[4].Y + 5,
                        m_ChildiconsRect[4].Width - 10, m_ChildiconsRect[4].Height - 10);
                    break;
                }
                else
                {
                }
            }

            //绘制机车制动状态
            for (int i = 0; i < 5; i++)
            {
                if (m_IsTrainBrake[i])
                {
                    if (i < 4)
                    {
                        g.DrawImage(m_ImageList[i + 5], m_ChildiconsRect[5].X + 5, m_ChildiconsRect[5].Y + 5,
                            m_ChildiconsRect[5].Width - 10, m_ChildiconsRect[5].Height - 10);
                        break;
                    }
                    else
                    {
                        g.DrawImage(m_ImageList[m_ImageList.Count - 1], m_ChildiconsRect[5].X + 5, m_ChildiconsRect[5].Y + 5,
                            m_ChildiconsRect[5].Width - 10, m_ChildiconsRect[5].Height - 10);
                    }

                }
                else
                {
                }
            }

            //绘制紧急制动状态
            for (int i = 0; i < 4; i++)
            {
                if (m_IsStopBrake[i])
                {
                    g.DrawImage(m_ImageList[i + 9], m_ChildiconsRect[6].X + 5, m_ChildiconsRect[6].Y + 5,
                        m_ChildiconsRect[6].Width - 10, m_ChildiconsRect[6].Height - 10);
                    break;
                }
                else
                {
                }
            }

            //受电弓绘制
            for (int i = 0; i < 3; i++)
            {
                if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0]) //受电弓1 2降弓
                {
                    g.DrawImage(m_ImageList[13], m_ChildiconsRect[3].X + 5, m_ChildiconsRect[3].Y + 5,
                        m_ChildiconsRect[3].Width - 10, m_ChildiconsRect[3].Height - 40);
                    g.DrawString("1     2", HxCommon.Font12B, HxCommon.DeadBrush, m_ChildiconsRect[3].X + 10,
                        m_ChildiconsRect[3].Y + 60);
                }
                else if (m_IsCurrentCollector1[1] || m_IsCurrentCollector2[1]) //有弓升起
                {
                    g.DrawImage(m_ImageList[14], m_ChildiconsRect[3].X + 5, m_ChildiconsRect[3].Y + 5,
                        m_ChildiconsRect[3].Width - 10, m_ChildiconsRect[3].Height - 40);

                    if (m_IsCurrentCollector1[1])
                    {
                        g.DrawString("1", HxCommon.Font12B, HxCommon.WhiteBrush, m_ChildiconsRect[3].X + 10,
                            m_ChildiconsRect[3].Y + 60);
                    }
                    else
                    {
                        g.DrawString("1", HxCommon.Font12B, HxCommon.DeadBrush, m_ChildiconsRect[3].X + 10,
                            m_ChildiconsRect[3].Y + 60);
                    }

                    if (m_IsCurrentCollector2[1])
                    {
                        g.DrawString("2", HxCommon.Font12B, HxCommon.WhiteBrush, m_ChildiconsRect[3].X + 70,
                            m_ChildiconsRect[3].Y + 60);
                    }
                    else
                    {
                        g.DrawString("2", HxCommon.Font12B, HxCommon.DeadBrush, m_ChildiconsRect[3].X + 70,
                            m_ChildiconsRect[3].Y + 60);
                    }
                }
            }

            //绘制电机状态
            for (int i = 0; i < 4; i++)
            {
                if (m_IsElectricMachine[i])
                {
                    g.DrawImage(m_ImageList[i + 16], m_ChildiconsRect[7].X + 5, m_ChildiconsRect[7].Y + 5,
                        m_ChildiconsRect[7].Width - 10, m_ChildiconsRect[7].Height - 10);
                    break;
                }
            }

            #endregion

            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //显示实际牵引/制动力
            for (int i = 0; i < 6; i++)
            {
                if (m_RealTractionN[i] != 0)
                {
                    g.FillRectangle(HxCommon.BlueBrush, m_InfoRect[i].X + 1,
                        m_InfoRect[i].Bottom - m_RealTractionN[i]*m_InfoRect[i].Height/108,
                        m_InfoRect[i].Width - 2, m_RealTractionN[i]*m_InfoRect[i].Height/108);
                }
                else
                {
                    g.FillRectangle(HxCommon.RedBrush, m_InfoRect[i].X + 1,
                        m_InfoRect[i].Bottom - m_RealBrakeN[i]*m_InfoRect[i].Height/108,
                        m_InfoRect[i].Width - 2, m_RealBrakeN[i]*m_InfoRect[i].Height/108);
                }
            }

            //绘制三角图标
            for (int i = 0; i < 6; i++)
            {
                m_TMark[i].OnDraw(g);
            }

            //绘制制动/牵引力柱状图标
            for (int i = 0; i < 6; i++)
            {
                g.DrawRectangle(HxCommon.LightWhitePen1, m_InfoRect[i]);
            }
        }

        /// <summary>
        /// 响应硬件按钮按下事件
        /// </summary>
        public void ButtonDownEvent()
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_IsNumButtonDown[i])
                {
                    switch (i)
                    {
                        case 0: //按下"1"键跳转到主要数据视图
                            append_postCmd(CmdType.ChangePage, 2, 0, 0);
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4: //按下5切换到列车重量视图
                            append_postCmd(CmdType.ChangePage, 3, 0, 0);
                            break;
                        case 5: //按下6切换到列车配置界面
                            append_postCmd(CmdType.ChangePage, 16, 0, 0);
                            break;
                        case 6: //按下7切换到牵引数据界面
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9: //返回主界面
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }

    /*-----------------------------------------------------------------------------------
     * 
     *
     *在柱状图形的浮动的图标 
     * 可以通过设置高度来表现其表示的值
     * 
     *-----------------------------------------------------------------------------------
     */
}