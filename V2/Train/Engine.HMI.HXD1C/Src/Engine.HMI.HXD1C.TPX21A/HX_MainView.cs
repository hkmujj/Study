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
    internal class HX_MainView : baseClass, IButtonEventListener
    {
        private Rectangle m_Rect; //主界面基本信息显示区域
        private readonly HxRectText[] m_TxtInfo = new HxRectText[6];
        private readonly Rectangle[] m_InfoRect = new Rectangle[4];
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>(); //界面中需要的界面元素
        private readonly string[] m_StrInfo = new string[4] {"转向架1", "转向架2", "原边电压", "原边电流",};
        private readonly bool[] m_IsDisconnector = new bool[4]; //主断路器状态
        private readonly string[] m_StrUnit = new string[4] {"[kV]", "[A]", "[%]", "[%]"};

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
        private readonly bool[] m_IsElectricMachine = new bool[4]; //电机状态 

        private int m_ElectricPressure; //原边电压
        private int m_ElectricFlow; //原边电流

        private readonly int[] m_TargetTractionNs = new int[2]; //转向架1 ,2给定牵引值
        private readonly int[] m_RealTractionNs = new int[2]; //转向架 1 2 牵引力实际值
        private readonly int[] m_TargetBrakeNs = new int[2]; //转向架1 ,2给定 制动力值
        private readonly int[] m_RealBrakeNs = new int[2]; //转向架1 ,2 制动力实际值
        private float m_TargetSpeed; //给定速度
        private float m_TractionN; //牵引力
        private float m_BrakeN; //制动力
        private readonly TriangleMark[] m_TMark = new TriangleMark[2]; //三角图标
        private readonly bool[] m_IsElectricPressures = new bool[2]; //原边电压 状态
        private bool m_IsTraction; //机车工况牵引
        private bool m_IsBrake; //机车工况制动
        private readonly bool[] m_DingShuStatus = new bool[3];
        private InterlinkingShowType m_InterlinkingShowType;

        private InterlinkingShowType InterlinkingShowType
        {
            set
            {
                m_InterlinkingShowType = value;
                switch (value)
                {
                    case InterlinkingShowType.None:
                        HxCommon.ButtonText[7].SetText("");
                        HxCommon.ButtonText[7].BKBrush.Color = Color.Black;
                        HxCommon.ButtonText[7].m_TextBrush.Color = Color.White;
                        break;
                    case InterlinkingShowType.White:
                        HxCommon.ButtonText[7].SetText("联挂");
                        HxCommon.ButtonText[7].BKBrush.Color = Color.Black;
                        HxCommon.ButtonText[7].m_TextBrush.Color = Color.White;
                        break;
                    case InterlinkingShowType.Yellow:
                        HxCommon.ButtonText[7].SetText("联挂");
                        HxCommon.ButtonText[7].BKBrush.Color = Color.Yellow;
                        HxCommon.ButtonText[7].m_TextBrush.Color = Color.Black;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value", value, null);
                }
            }
            get { return m_InterlinkingShowType; }
        }


        public override string GetInfo()
        {
            return "主界面视图元素";
        }

        public override bool init(ref int nErrorObjectIndex)
        {


            InitData();

            GlobalParam.Instance.AddButtonEventListener(this);

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
            for (int i = 0; i < 4; i++)
            {
                m_InfoRect[i] = new Rectangle(m_Rect.X + 95 + i*135, m_Rect.Y + 5, 35, 384);
            }

            //显示原边电流 原边电压等的文本框的初始化
            for (int i = 0; i < 6; i++)
            {
                m_TxtInfo[i] = new HxRectText();
                m_TxtInfo[i].SetBkColor(0, 0, 0);
                m_TxtInfo[i].SetLinePen(84, 84, 84, 1);
                m_TxtInfo[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_TxtInfo[i].SetTextColor(255, 255, 255);

                if (i == 0)
                {
                    m_TxtInfo[i].SetTextRect(m_InfoRect[1].X - 20, m_InfoRect[1].Bottom + 4, 55, 25);
                    m_TxtInfo[i].SetDrawFrm(true);
                }
                else if (i == 1 || i == 2)
                {
                    m_TxtInfo[i].SetTextRect(m_InfoRect[i + 1].X - 20, m_InfoRect[i + 1].Bottom + 4, 65, 25);
                }
                else if (i == 3 || i == 4)
                {
                    m_TxtInfo[i].SetDrawFrm(true);
                    m_TxtInfo[i].SetTextRect(m_InfoRect[i - 3].X - 20, m_InfoRect[i - 3].Bottom + 35, 75, 25);
                }
                else
                {
                    m_TxtInfo[i].SetDrawFrm(true);
                    m_TxtInfo[i].SetTextRect(m_InfoRect[i - 3].X - 20, m_InfoRect[i - 3].Bottom + 35, 190, 25);
                }

                if (i > 0 && i < 5)
                {
                    m_TxtInfo[i].SetText(m_StrInfo[i - 1]);
                }
            }

            //显示速度 牵引制动信息的文本框的初始化
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
            for (int i = 0; i < 2; i++)
            {
                m_TMark[i] = new TriangleMark(m_InfoRect[i + 2].X + 22, m_InfoRect[i + 2].Bottom);
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void GetValue()
        {
            //设置标题
            HxCommon.ButtonText[0].SetText("主要数据");
            HxCommon.ButtonText[1].SetText("维护");
            HxCommon.ButtonText[2].SetText("隔离解锁");
            HxCommon.ButtonText[3].SetText("轮缘润滑");
            HxCommon.ButtonText[4].SetText("列车参数");
            HxCommon.ButtonText[5].SetText("机车配置");
            HxCommon.ButtonText[6].SetText("牵引数据");
            HxCommon.ButtonText[7].SetText(" ");
            HxCommon.ButtonText[8].SetText(" ");
            HxCommon.ButtonText[9].SetText("主界面");


            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    HxCommon.ButtonText[i].SetBkColor(255, 255, 0);
                    HxCommon.ButtonText[i].SetTextColor(0, 0, 0);
                }
                else if (i < 9)
                {
                    if (i != 7)
                    {
                        HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                        HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                    }
                }
            }



            for (int i = 0; i < 4; i++)
            {
                m_IsDisconnector[i] = BoolList[UIObj.InBoolList[i]];
            }

            //绘制主断状态
            for (int i = 0; i < 4; i++)
            {
                if (m_IsDisconnector[i])
                {
                    m_TxtInfo[0].SetImage(m_ImageList[i]);
                    break;
                }

                // txtInfo[0].SetImage(default(Image));
            }

            #region 各状态信息Bool 量的读入

            //机车方向
            for (int i = 0; i < 3; i++)
            {
                m_IsTrainDirection[i] = BoolList[UIObj.InBoolList[i + 4]];
            }

            //警惕装置
            for (int i = 0; i < 2; i++)
            {
                m_IsAlert[i] = BoolList[UIObj.InBoolList[i + 7]];
            }

            //机车制动
            for (int i = 0; i < 4; i++)
            {
                m_IsTrainBrake[i] = BoolList[UIObj.InBoolList[i + 9]];
            }
            m_IsTrainBrake[4] = BoolList[UIObj.InBoolList[44]];
            //停放制动
            for (int i = 0; i < 4; i++)
            {
                m_IsStopBrake[i] = BoolList[UIObj.InBoolList[i + 13]];
            }

            //底部数字按钮
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i + 17]];
            }

            //受电弓1
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector1[i] = BoolList[UIObj.InBoolList[i + 27]];
            }

            //受电弓2
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector2[i] = BoolList[UIObj.InBoolList[i + 30]];
            }

            //电机状态
            for (int i = 0; i < 4; i++)
            {
                m_IsElectricMachine[i] = BoolList[UIObj.InBoolList[i + 33]];
            }

            for (int i = 0; i < 2; i++)
            {
                m_IsElectricPressures[i] = BoolList[UIObj.InBoolList[i + 37]];
            }

            m_IsTraction = BoolList[UIObj.InBoolList[39]];
            m_IsBrake = BoolList[UIObj.InBoolList[40]];
            for (int i = 0; i < 3; i++)
            {
                m_DingShuStatus[i] = BoolList[UIObj.InBoolList[42 + i]];
            }

            //联挂状态
            if (!m_IsTrainDirection[2]) //机车有方向
            {
                if (BoolList[UIObj.InBoolList[45]])
                {
                    InterlinkingShowType = InterlinkingShowType.Yellow;
                }
                else
                {
                    InterlinkingShowType = InterlinkingShowType.White;
                }
            }
            else
            {
                InterlinkingShowType = InterlinkingShowType.None;
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
            }

            //定速控制
            if (m_DingShuStatus[0])
            {
                HxCommon.ButtonText[8].SetText(" ");
                HxCommon.ButtonText[8].SetBkColor(0, 0, 0);
            }
            else if (m_DingShuStatus[1])
            {
                HxCommon.ButtonText[8].SetText("定速控制");
                HxCommon.ButtonText[8].SetBkColor(255, 255, 0);
                HxCommon.ButtonText[8].SetTextColor(0, 0, 0);
            }
            else
            {
                HxCommon.ButtonText[8].SetText("定速控制");
                HxCommon.ButtonText[8].SetBkColor(0, 0, 0);
                HxCommon.ButtonText[8].SetTextColor(255, 255, 255);
            }

            #endregion

            #region 各 float 量的读入

            //给定速度
            m_TargetSpeed = (int) FloatList[UIObj.InFloatList[10]];

            //牵引力
            m_TractionN = (int) FloatList[UIObj.InFloatList[11]];

            //制动力
            m_BrakeN = (int) FloatList[UIObj.InFloatList[12]];

            //原边电压
            m_ElectricPressure = Convert.ToInt32(FloatList[UIObj.InFloatList[0]]);
            //范围在0 到 32Kv 之间
            if (m_ElectricPressure < 0)
            {
                m_ElectricPressure = 0;
            }

            if (m_ElectricPressure > 32)
            {
                m_ElectricPressure = 32;
            }

            //原边电流
            m_ElectricFlow = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]);
            //范围在0 到640之间
            if (m_ElectricFlow < 0)
            {
                m_ElectricFlow = 0;
            }

            if (m_ElectricFlow > 640)
            {
                m_ElectricFlow = 640;
            }

            //牵引力给定值 取值在0 到100
            for (int i = 0; i < 2; i++)
            {
                m_TargetTractionNs[i] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 2]]);
                if (m_TargetTractionNs[i] < 0)
                {
                    m_TargetTractionNs[i] = 0;
                }

                if (m_TargetTractionNs[i] > 100)
                {
                    m_TargetTractionNs[i] = 100;
                }
            }

            //牵引力实际值  取值在0 到100
            for (int i = 0; i < 2; i++)
            {
                m_RealTractionNs[i] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 4]]);
                if (m_RealTractionNs[i] < 0)
                {
                    m_RealTractionNs[i] = 0;
                }

                if (m_RealTractionNs[i] > 100)
                {
                    m_RealTractionNs[i] = 100;
                }
            }

            //制动力给定值
            for (int i = 0; i < 2; i++)
            {
                m_TargetBrakeNs[i] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 6]]);

                if (m_TargetBrakeNs[i] < 0)
                {
                    m_TargetBrakeNs[i] = 0;
                }

                if (m_TargetBrakeNs[i] > 100)
                {
                    m_TargetBrakeNs[i] = 100;
                }

            }

            //制动力实际值
            for (int i = 0; i < 2; i++)
            {
                m_RealBrakeNs[i] = Convert.ToInt32(FloatList[UIObj.InFloatList[i + 8]]);

                if (m_RealBrakeNs[i] < 0)
                {
                    m_RealBrakeNs[i] = 0;
                }

                if (m_RealBrakeNs[i] > 100)
                {
                    m_RealBrakeNs[i] = 100;
                }
            }

            #endregion

            m_TxtStatus[0].SetText(m_TargetSpeed.ToString());

            if (m_IsTraction) //当前工况为牵引以蓝色字体显示牵引力
            {
                m_TxtStatus[1].SetTextColor(0, 0, 255);
                m_TxtStatus[1].SetText(m_TractionN.ToString());
            }
            else if (m_IsBrake) //当前工况为电制以红色显示制动力
            {

                m_TxtStatus[1].SetTextColor(255, 0, 0);
                m_TxtStatus[1].SetText(m_BrakeN.ToString());
            }
            else
            {
                m_TxtStatus[1].SetTextColor(255, 255, 255);
                m_TxtStatus[1].SetText("0");
            }

            //根据更新的牵引力制动力改变三角图标位置
            for (int i = 0; i < 2; i++)
            {
                if (m_IsTraction) //当前工况为牵引 背景为蓝色
                {
                    m_TMark[i].SetColor(TriangleMark.ColorType.Blue);
                    m_TMark[i].SetPosition(m_InfoRect[i + 2].X + 17,
                        m_InfoRect[i + 2].Bottom - m_TargetTractionNs[i]*m_InfoRect[i].Height/108);

                }
                else if (m_IsBrake)
                {
                    m_TMark[i].SetColor(TriangleMark.ColorType.Red);
                    m_TMark[i].SetPosition(m_InfoRect[i + 2].X + 17,
                        m_InfoRect[i + 2].Bottom - m_TargetBrakeNs[i]*m_InfoRect[i].Height/108);

                }
                else
                {
                    m_TMark[i].SetColor(TriangleMark.ColorType.Gray);
                    m_TMark[i].SetPosition(m_InfoRect[i + 2].X + 17, m_InfoRect[i + 2].Bottom);
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

            for (int i = 0; i < 4; i++)
            {
                g.DrawRectangle(HxCommon.LightWhitePen1, m_InfoRect[i]);
            }

            for (int i = 0; i < 6; i++)
            {
                m_TxtInfo[i].OnDraw(g);
            }

            #region 绘 制 刻 度 线

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 33; j++)
                {
                    if (j%5 == 0)
                    {
                        g.DrawLine(HxCommon.LightWhitePen2, m_InfoRect[i].X - 17,
                            m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/32),
                            m_InfoRect[i].X - 3, m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/32));
                        if (i == 0)
                        {
                            if (j < 10)
                            {
                                g.DrawString(j.ToString(), HxCommon.Font14B, HxCommon.WhiteBrush, m_InfoRect[i].X - 33,
                                    m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/32) - 13);
                            }
                            else
                            {
                                g.DrawString(j.ToString(), HxCommon.Font14B, HxCommon.WhiteBrush, m_InfoRect[i].X - 41,
                                    m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/32) - 13);
                            }
                        }
                        else
                        {
                            if (j == 0)
                            {
                                g.DrawString(j.ToString(), HxCommon.Font14B, HxCommon.WhiteBrush, m_InfoRect[i].X - 33,
                                    m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/32) - 13);
                            }
                            else if (j%10 == 0)
                            {
                                g.DrawString((j*20).ToString(), HxCommon.Font14B, HxCommon.WhiteBrush,
                                    m_InfoRect[i].X - 51, m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/32) - 13);
                            }
                        }

                    }
                    else
                    {
                        g.DrawLine(HxCommon.LightWhitePen2, m_InfoRect[i].X - 12,
                            m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/32),
                            m_InfoRect[i].X - 3, m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/32));
                    }
                }

                g.DrawLine(HxCommon.LightWhitePen1, m_InfoRect[i].X - 3, m_InfoRect[i].Bottom, m_InfoRect[i].X - 3,
                    m_InfoRect[i].Y);

                if (i == 0)
                {
                    g.DrawString(m_StrUnit[i], HxCommon.Font14, HxCommon.WhiteBrush, m_InfoRect[i].X - 60,
                        m_InfoRect[i].Y + 35);
                }
                else
                {
                    g.DrawString(m_StrUnit[i], HxCommon.Font14, HxCommon.WhiteBrush, m_InfoRect[i].X - 45,
                        m_InfoRect[i].Y + 35);
                }
            }

            for (int i = 2; i < 4; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (j%5 == 0)
                    {
                        g.DrawLine(HxCommon.LightWhitePen2, m_InfoRect[i].X - 17,
                            m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/27f),
                            m_InfoRect[i].X - 3, m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/27f));
                        if (j == 0)
                        {
                            g.DrawString(j.ToString(), HxCommon.Font14B, HxCommon.WhiteBrush, m_InfoRect[i].X - 33,
                                m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/27f) - 13);
                        }
                        else if (j > 0 && j < 25)
                        {
                            g.DrawString((j*4).ToString(), HxCommon.Font14B, HxCommon.WhiteBrush, m_InfoRect[i].X - 42,
                                m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/27f) - 11);
                        }
                        else
                        {
                            g.DrawString((j*4).ToString(), HxCommon.Font14B, HxCommon.WhiteBrush, m_InfoRect[i].X - 55,
                                m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/27f) - 11);
                        }
                    }
                    else
                    {
                        g.DrawLine(HxCommon.LightWhitePen2, m_InfoRect[i].X - 12,
                            m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/27f),
                            m_InfoRect[i].X - 3, m_InfoRect[i].Bottom - j*(m_InfoRect[i].Height/27f));
                    }
                }

                g.DrawLine(HxCommon.LightWhitePen1, m_InfoRect[i].X - 3, m_InfoRect[i].Bottom, m_InfoRect[i].X - 3,
                    m_InfoRect[i].Y);
                g.DrawString(m_StrUnit[i], HxCommon.Font14, HxCommon.WhiteBrush, m_InfoRect[i].X - 45,
                    m_InfoRect[i].Y + 40);

            }

            g.DrawString("牵引", HxCommon.Font14B, HxCommon.BlueBrush, m_TxtInfo[5].m_RectPosition.X + 50,
                m_TxtInfo[5].m_RectPosition.Y + 2);
            g.DrawString("/", HxCommon.Font14B, HxCommon.GrayBrush, m_TxtInfo[5].m_RectPosition.X + 88,
                m_TxtInfo[5].m_RectPosition.Y + 2);
            g.DrawString("制动", HxCommon.Font14B, HxCommon.RedBrush, m_TxtInfo[5].m_RectPosition.X + 98,
                m_TxtInfo[5].m_RectPosition.Y + 2);

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
                    g.DrawImage(m_ImageList[i + 4], m_ChildiconsRect[2].X + 5, m_ChildiconsRect[2].Y + 5,
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
                    g.DrawImage(m_ImageList[i + 7], m_ChildiconsRect[4].X + 5, m_ChildiconsRect[4].Y + 5,
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
                        g.DrawImage(m_ImageList[i + 9], m_ChildiconsRect[5].X + 5, m_ChildiconsRect[5].Y + 5,
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
                    g.DrawImage(m_ImageList[i + 13], m_ChildiconsRect[6].X + 5, m_ChildiconsRect[6].Y + 5,
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
                    g.DrawImage(m_ImageList[17], m_ChildiconsRect[3].X + 5, m_ChildiconsRect[3].Y + 5,
                        m_ChildiconsRect[3].Width - 10, m_ChildiconsRect[3].Height - 40);
                    g.DrawString("1     2", HxCommon.Font12B, HxCommon.DeadBrush, m_ChildiconsRect[3].X + 10,
                        m_ChildiconsRect[3].Y + 60);
                }
                else if (m_IsCurrentCollector1[1] || m_IsCurrentCollector2[1]) //有弓升起
                {
                    g.DrawImage(m_ImageList[18], m_ChildiconsRect[3].X + 5, m_ChildiconsRect[3].Y + 5,
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
                    g.DrawImage(m_ImageList[i + 20], m_ChildiconsRect[7].X + 5, m_ChildiconsRect[7].Y + 5,
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

            //显示 牵引力/制动力实际值
            for (int i = 0; i < 2; i++)
            {
                if (m_IsTraction)
                {
                    g.FillRectangle(HxCommon.BlueBrush, m_InfoRect[i + 2].X + 1,
                        m_InfoRect[i + 2].Bottom - m_RealTractionNs[i]*m_InfoRect[i + 2].Height/108,
                        m_InfoRect[i + 2].Width - 2, m_RealTractionNs[i]*m_InfoRect[i + 2].Height/108);
                }
                else if (m_IsBrake)
                {
                    g.FillRectangle(HxCommon.RedBrush, m_InfoRect[i + 2].X + 1,
                        m_InfoRect[i + 2].Bottom - m_RealBrakeNs[i]*m_InfoRect[i + 2].Height/108,
                        m_InfoRect[i + 2].Width - 2, m_RealBrakeNs[i]*m_InfoRect[i + 2].Height/108);
                }
                else
                {
                }
            }

            //显示原边电流
            g.FillRectangle(HxCommon.YellowBrush, m_InfoRect[1].X + 1,
                m_InfoRect[1].Bottom - m_ElectricFlow*m_InfoRect[1].Height/640,
                m_InfoRect[1].Width - 2, m_ElectricFlow*m_InfoRect[1].Height/640);

            //显示原边电压
            if (m_IsElectricPressures[0]) //不能正常工作 显示红色
            {
                g.FillRectangle(HxCommon.RedBrush, m_InfoRect[0].X + 1,
                    m_InfoRect[0].Bottom - m_ElectricPressure*m_InfoRect[0].Height/32,
                    m_InfoRect[0].Width - 2, m_ElectricPressure*m_InfoRect[0].Height/32);
            }

            if (m_IsElectricPressures[1]) //正常工作 显示绿色
            {
                g.FillRectangle(HxCommon.GreenBrush, m_InfoRect[0].X + 1,
                    m_InfoRect[0].Bottom - m_ElectricPressure*m_InfoRect[0].Height/32,
                    m_InfoRect[0].Width - 2, m_ElectricPressure*m_InfoRect[0].Height/32);
            }

            g.DrawLine(HxCommon.RedPen, m_InfoRect[0].X + 1, m_InfoRect[0].Bottom - 17.5f*m_InfoRect[0].Height/32.0f,
                m_InfoRect[0].Right, m_InfoRect[0].Bottom - 17.5f*m_InfoRect[0].Height/32.0f);
            g.DrawLine(HxCommon.RedPen, m_InfoRect[0].X + 1, m_InfoRect[0].Bottom - 31.0f*m_InfoRect[0].Height/32.0f,
                m_InfoRect[0].Right, m_InfoRect[0].Bottom - 31.0f*m_InfoRect[0].Height/32.0f);
            g.DrawLine(HxCommon.GreenPen, m_InfoRect[0].X + 1, m_InfoRect[0].Bottom - 22.5f*m_InfoRect[0].Height/32.0f,
                m_InfoRect[0].Right, m_InfoRect[0].Bottom - 22.5f*m_InfoRect[0].Height/32.0f);
            g.DrawLine(HxCommon.GreenPen, m_InfoRect[0].X + 1, m_InfoRect[0].Bottom - 30.0f*m_InfoRect[0].Height/32.0f,
                m_InfoRect[0].Right, m_InfoRect[0].Bottom - 30.0f*m_InfoRect[0].Height/32.0f);
            g.DrawLine(HxCommon.BluePen, m_InfoRect[0].X + 1, m_InfoRect[0].Bottom - 19.0f*m_InfoRect[0].Height/32.0f,
                m_InfoRect[0].Right, m_InfoRect[0].Bottom - 19.0f*m_InfoRect[0].Height/32.0f);

            //绘制三角图标
            for (int i = 0; i < 2; i++)
            {
                m_TMark[i].OnDraw(g);
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
                            append_postCmd(CmdType.ChangePage, 17, 0, 0);
                            break;
                        case 2:

                            break;
                        case 3:
                            append_postCmd(CmdType.ChangePage, 18, 0, 0);
                            break;
                        case 4: //按下5切换到列车参数视图
                            append_postCmd(CmdType.ChangePage, 3, 0, 0);
                            break;
                        case 5: //按下6切换到列车配置界面
                            append_postCmd(CmdType.ChangePage, 16, 0, 0);
                            break;
                        case 6: //按下7切换到牵引数据界面
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            break;
                        case 7:
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0],
                                InterlinkingShowType != InterlinkingShowType.None ? 1 : 0, 0);
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                        default:
                            break;


                    }
                }
            }
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 1)
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
            if (GlobalParam.Instance.CurrentViewId == 1)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

    }
}
