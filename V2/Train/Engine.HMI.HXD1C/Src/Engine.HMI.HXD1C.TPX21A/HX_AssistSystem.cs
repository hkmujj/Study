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
    class HX_AssistSystem:baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10];//底部数字按钮是否按下
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>();//界面中需要的界面元素
        private readonly HxRectText[] m_InfoText = new HxRectText[6];//显示辅助变流器的电压 频率等的文本框
        private readonly Rectangle[] m_SinalRect = new Rectangle[2];
        private readonly Pen m_LinePen2 = new Pen(Color.FromArgb(231,231,231),2);
        private readonly Pen m_LinePen3 = new Pen(Color.FromArgb(233, 232, 231), 3);
        private readonly Pen m_GreenPen = new Pen(Color.FromArgb(0, 255, 0),2);
        private SolidBrush m_EllipseBrush=new SolidBrush(Color.FromArgb(233, 232, 231));
        private readonly Rectangle[] m_ImgRect = new Rectangle[19];

        private readonly Point[] m_Point1S = new Point[12];
        private readonly Point[] m_Point2S = new Point[18];
        private readonly Point[] m_Point3S = new Point[20];

        private readonly bool[] m_IsContactor = new bool[6];//接触器状态
        private readonly bool[] m_IsAutoswitch = new bool[19];//自动状态
        private int m_StarBit;//bool 量起始位置

        private readonly float[] m_AssistVs = new float[2];//辅助变流器电压
        private readonly float[] m_AssistHzs = new float[2];//辅助变流器频率
        private readonly string[] m_Strs = new string[9] { "油泵1", "油泵2", "水泵1", "变流柜", "水泵2", "变流柜", "卫生间", "1端空调", "2端空调" };
        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 7)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int)(btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 7)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }
        public override string GetInfo()
        {
            return "辅助系统";
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

        public void InitData()
        {
            #region 显示辅助变流器的电压 频率等的文本框的初始化
            for (int i = 0; i < 6; i++)
            {
                m_InfoText[i] = new HxRectText();
                m_InfoText[i].SetBkColor(0, 0, 0);
                m_InfoText[i].SetDrawFrm(true);
                m_InfoText[i].SetTextColor(255, 255, 255);
                m_InfoText[i].SetTextStyle(14, FormatStyle.Center, true, "宋体");
                if (i == 0)
                {
                    m_InfoText[i].SetTextRect(HxCommon.Recposition.X + 7 + i * 123, HxCommon.Recposition.Y + 31, 120, 40);
                }
                else if (i == 1 || i == 2)
                {
                    m_InfoText[i].SetTextRect(HxCommon.Recposition.X + 27 + i * 103, HxCommon.Recposition.Y + 31, 100, 40);
                }
                else if (i == 3 || i == 4)
                {
                    m_InfoText[i].SetTextRect(HxCommon.Recposition.X + 150 + i * 103, HxCommon.Recposition.Y + 31, 100, 40);
                }
                else
                {
                    m_InfoText[i].SetTextRect(HxCommon.Recposition.X + 150 + i * 103, HxCommon.Recposition.Y + 31, 125, 40);
                }
            }

            m_InfoText[0].SetText("辅助变流器1");
            m_InfoText[5].SetText("辅助变流器2");
            #endregion

            #region 显示信号信息的矩形框的初始化
            m_SinalRect[0] = new Rectangle(m_InfoText[0].m_RectPosition.X + 50, m_InfoText[0].m_RectPosition.Bottom + 15, 45, 30);
            m_SinalRect[1] = new Rectangle(m_InfoText[5].m_RectPosition.X + 50, m_InfoText[5].m_RectPosition.Bottom + 15, 45, 30);
            #endregion

            #region 第一排点坐标初始化
            m_Point1S[0] = new Point(m_SinalRect[0].X + 23, m_SinalRect[0].Bottom + 20);
            m_Point1S[1] = new Point(m_Point1S[0].X + 110, m_Point1S[0].Y);
            m_Point1S[2] = new Point(m_Point1S[1].X + 22, m_Point1S[1].Y);
            m_Point1S[3] = new Point(m_Point1S[2].X + 87, m_Point1S[2].Y);
            m_Point1S[4] = new Point(m_Point1S[3].X + 12, m_Point1S[3].Y);
            m_Point1S[5] = new Point(m_Point1S[4].X + 150, m_Point1S[4].Y);
            m_Point1S[6] = new Point(m_Point1S[5].X + 22, m_Point1S[5].Y);
            m_Point1S[7] = new Point(m_Point1S[6].X + 180, m_Point1S[6].Y);
            m_Point1S[8] = new Point(m_Point1S[7].X + 12, m_Point1S[7].Y);
            m_Point1S[9] = new Point(m_Point1S[8].X + 8, m_Point1S[8].Y);
            m_Point1S[10] = new Point(m_Point1S[9].X + 22, m_Point1S[9].Y);
            m_Point1S[11] = new Point(m_Point1S[9].X + 65, m_Point1S[10].Y);
            #endregion

            #region 第二排点坐标初始化
            m_Point2S[0] = new Point(m_InfoText[0].m_RectPosition.X + 30, m_Point1S[0].Y+25);
            for (int i = 1; i < 7;i++ )
            {
                if (i % 2 == 0)
                {
                    m_Point2S[i] = new Point(m_Point2S[i - 1].X + 4, m_Point2S[0].Y);
                }
                else
                {
                    m_Point2S[i] = new Point(m_Point2S[i - 1].X + 59, m_Point2S[0].Y);
                }
            }

            m_Point2S[7] = new Point(m_Point2S[6].X +26, m_Point2S[0].Y);
            m_Point2S[8] = new Point(m_Point2S[7].X + 12, m_Point2S[0].Y);
            m_Point2S[9] = new Point(m_Point2S[8].X + 26, m_Point2S[0].Y);

            for (int i = 10; i < 18; i++)
            {
                if (i % 2 == 0)
                {
                    m_Point2S[i] = new Point(m_Point2S[i - 1].X + 12, m_Point2S[0].Y);
                }
                else
                {
                    m_Point2S[i] = new Point(m_Point2S[i - 1].X + 62, m_Point2S[0].Y);
                }
            }
            #endregion

            #region 第三排坐标点
            for (int i = 0; i < 20; i++)
            {
                if (i == 0)
                {
                    m_Point3S[i] = new Point(HxCommon.Recposition.X + 45, m_Point2S[0].Y + 150);
                }
                else if (i % 2 == 0)
                {
                    m_Point3S[i] = new Point(m_Point3S[i - 1].X + 16, m_Point3S[0].Y);
                }
                else
                {
                    m_Point3S[i] = new Point(m_Point3S[i - 1].X + 55, m_Point3S[0].Y);
                }

            }
            #endregion


            #region 自动开关位置初始化
            for (int i = 0; i < 19 ; i++ )
            {
                if (i < 8)
                {
                    if (i < 4)
                    {
                        m_ImgRect[i] = new Rectangle(m_Point2S[2 * i].X - 21, m_Point2S[2 * i + 2].Y + 18, 30, 45);
                    }
                    else
                    {
                        m_ImgRect[i] = new Rectangle(m_Point2S[2 * i + 2].X - 21, m_Point2S[2 * i].Y + 18, 30, 45);
                    }
                }
                else
                {
                    if (i < 18)
                    {
                        m_ImgRect[i] = new Rectangle(m_Point3S[2 * (i - 8)].X - 21, m_Point3S[i].Y + 18, 30, 45);
                    }
                    else
                    {
                        m_ImgRect[i] = new Rectangle(m_Point3S[19].X - 7, m_Point3S[i].Y + 18, 30, 45);
                    }
                }
                
            }
            #endregion

        }
        
        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("辅助系统");

            HxCommon.ButtonText[0].SetText("牵引数据");
            HxCommon.ButtonText[1].SetText("温度");
            HxCommon.ButtonText[2].SetText("网络");
            HxCommon.ButtonText[3].SetText("辅助系统");
            HxCommon.ButtonText[4].SetText("工作状态");
            HxCommon.ButtonText[5].SetText("无线重联");
            HxCommon.ButtonText[6].SetText(" ");
            HxCommon.ButtonText[7].SetText("辅机测试");
            HxCommon.ButtonText[8].SetText("库内动车");
            HxCommon.ButtonText[9].SetText("主界面");

            for (int i = 0; i < 10; i++)
            {
                if (i == 3)
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
            m_StarBit = UIObj.InBoolList[10];

            //接触器
            for (int i = 0; i < 6;i++ )
            {
                m_IsContactor[i] = BoolList[i + 4+m_StarBit];
            }

            //自动开关
            for (int i = 0; i < 19;i++ )
            {
                m_IsAutoswitch[i] = BoolList[i + 10+m_StarBit];
            }
            #endregion

            #region float 量 的 输 入
           
             //辅助变流器电压
            for (int i = 0; i < 2; i++)
            {
                m_AssistVs[i] = FloatList[UIObj.InFloatList[i]];
            }

            //辅助变流器频率
            for (int i = 0; i < 2; i++)
            {
                m_AssistHzs[i] = FloatList[UIObj.InFloatList[i+2]];
            }
            #endregion



            //底部导航按钮
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }
        }

        public void DrawOn(Graphics g)
        {
            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //绘制辅助变流器的电压 频率等的文本框
            m_InfoText[1].SetText(m_AssistVs[0].ToString() + "   V");
            m_InfoText[2].SetText(m_AssistHzs[0].ToString() + "   Hz");
            m_InfoText[3].SetText(m_AssistVs[1].ToString() + "   Hz");
            m_InfoText[4].SetText(m_AssistHzs[1].ToString() + "   V");

            for (int i = 0; i < 6;i++ )
            {
                m_InfoText[i].OnDraw(g);
            }



            //绘制固定信号
            for (int i = 0; i < 2;i++ )
            {
                g.DrawString("=/3", HxCommon.Font12, HxCommon.WhiteBrush, m_SinalRect[i].X + 2, m_SinalRect[i].Y + 4);
                g.DrawString("~", HxCommon.Font12, HxCommon.WhiteBrush, m_SinalRect[i].X + 23, m_SinalRect[i].Y + 10);
                g.DrawLine(m_LinePen2,m_SinalRect[i].X+15,m_SinalRect[i].Y,m_SinalRect[i].X+15, m_InfoText[i*5].m_RectPosition.Bottom);
                g.DrawLine(m_LinePen2,m_SinalRect[i].X+35,m_SinalRect[i].Y,m_SinalRect[i].X+35, m_InfoText[i*5].m_RectPosition.Bottom);
                g.DrawRectangle(m_LinePen2,m_SinalRect[i]);
            }

            #region 绘制线条
            //第一排
            for (int i = 0; i < 11;i+=2 )
            {
                g.DrawLine(m_LinePen3, m_Point1S[i], m_Point1S[i + 1]);
            }

            g.DrawImage(m_ImageList[0], m_Point1S[3].X+1,m_Point1S[3].Y-5,10,10);
            g.DrawImage(m_ImageList[0], m_Point1S[7].X + 1, m_Point1S[7].Y - 5, 10, 10);
            g.DrawLine(m_LinePen2, m_Point1S[0].X, m_Point1S[0].Y, m_Point1S[0].X, m_SinalRect[0].Bottom);
            g.DrawLine(m_LinePen2, m_Point1S[11].X, m_Point1S[11].Y, m_Point1S[11].X, m_SinalRect[1].Bottom);

            g.DrawString("31-K10",HxCommon.Font12B,HxCommon.WhiteBrush , m_Point1S[1].X - 10, m_Point1S[1].Y - 30);
            g.DrawString("31-K02", HxCommon.Font12B, HxCommon.WhiteBrush, m_Point1S[5].X - 10, m_Point1S[5].Y - 30);
            g.DrawString("31-K20", HxCommon.Font12B, HxCommon.WhiteBrush, m_Point1S[7].X - 10, m_Point1S[7].Y - 30);

            //接触器状态
            if (m_IsContactor[0])//接触器断开
            {
                g.DrawLine(m_LinePen2, m_Point1S[2].X, m_Point1S[2].Y, m_Point1S[1].X, m_Point1S[1].Y - 12);
            }
            else
            {
                g.DrawLine(m_GreenPen, m_Point1S[1].X, m_Point1S[1].Y, m_Point1S[1].X, m_Point1S[1].Y - 7);
                g.DrawLine(m_GreenPen, m_Point1S[2].X, m_Point1S[2].Y, m_Point1S[1].X-4, m_Point1S[1].Y - 5);
            }

            if (m_IsContactor[1])//接触器断开
            {
                g.DrawLine(m_LinePen2, m_Point1S[6].X, m_Point1S[6].Y, m_Point1S[5].X, m_Point1S[5].Y - 12);
            }
            else
            {
                g.DrawLine(m_GreenPen, m_Point1S[5].X, m_Point1S[5].Y, m_Point1S[5].X, m_Point1S[5].Y - 7);
                g.DrawLine(m_GreenPen, m_Point1S[6].X, m_Point1S[6].Y, m_Point1S[5].X - 4, m_Point1S[5].Y - 5);
            }

            if (m_IsContactor[2])//接触器断开
            {
                g.DrawLine(m_LinePen2, m_Point1S[10].X, m_Point1S[10].Y, m_Point1S[9].X, m_Point1S[9].Y - 12);
            }
            else
            {
                g.DrawLine(m_GreenPen, m_Point1S[9].X, m_Point1S[9].Y, m_Point1S[9].X, m_Point1S[9].Y - 7);
                g.DrawLine(m_GreenPen, m_Point1S[10].X, m_Point1S[10].Y, m_Point1S[9].X - 4, m_Point1S[9].Y - 5);
            }

            //第二排
            for (int i = 0; i < 18 ; i += 2 )
            {
                g.DrawLine(m_LinePen3, m_Point2S[i], m_Point2S[i + 1]);
            }

            g.DrawImage(m_ImageList[0], m_Point2S[0].X - 8, m_Point2S[0].Y - 5, 10, 10);
            for (int i = 1; i < 18;i += 2 )
            {
                g.DrawImage(m_ImageList[0], m_Point2S[i].X + 1, m_Point2S[0].Y - 5,10, 10);
            }
            g.DrawLine(m_LinePen3, m_Point2S[7].X + 5, m_Point2S[7].Y - 6, m_Point2S[7].X + 5, m_Point2S[7].Y - 20);

            //第三排
            for (int i = 1; i < 20; i += 2)
            {
                g.DrawImage(m_ImageList[0], m_Point3S[i].X + 1, m_Point3S[0].Y - 5, 10, 10);
            }

            for (int i = 0; i < 20; i += 2)
            {
                g.DrawLine(m_LinePen3, m_Point3S[i], m_Point3S[i + 1]);
            }

            g.DrawImage(m_ImageList[0], m_Point3S[0].X -8, m_Point3S[0].Y - 5, 10, 10);

            g.DrawLine(m_LinePen3, 645, 137, 645, 309);
            g.DrawImage(m_ImageList[0], 645 - 6, 132, 10, 10);
            g.DrawImage(m_ImageList[0], 645 - 6, 309, 10, 10);
            #endregion

            #region  绘制自动开关
                for (int i = 0; i < 8; i++)
                {
                    if (!m_IsAutoswitch[i])//自动开关闭合
                    {
                        g.DrawImage(m_ImageList[1], m_ImgRect[i]);
                    }
                    else
                    {
                        g.DrawImage(m_ImageList[2], m_ImgRect[i]);
                    }

                    g.DrawLine(m_LinePen3, m_ImgRect[i].X + 15, m_ImgRect[i].Y - 17, m_ImgRect[i].X + 15, m_ImgRect[i].Bottom + 25);
                    g.DrawString("34-Q1" + i.ToString(), HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i].X - 6, m_ImgRect[i].Bottom + 4);
                    if (i < 6)
                    {
                        g.DrawString("牵引", HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i].X - 3, m_ImgRect[i].Bottom + 29);
                        g.DrawString("风机" + (i+1).ToString(), HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i].X - 3, m_ImgRect[i].Bottom + 44);
                    }
                    else
                    {
                        g.DrawString("冷却塔", HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i].X - 10, m_ImgRect[i].Bottom + 29);
                        g.DrawString("风机" + (i - 5).ToString(), HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i].X - 3, m_ImgRect[i].Bottom + 44);
                    }
                }

                for (int i = 0; i < 11; i++)
                {
                    if (!m_IsAutoswitch[i + 8])
                    {
                        g.DrawImage(m_ImageList[1], m_ImgRect[i + 8]);
                    }
                    else
                    {
                        g.DrawImage(m_ImageList[2], m_ImgRect[i+8]);
                    }

                    g.DrawLine(m_LinePen3, m_ImgRect[i+8].X + 15, m_ImgRect[i+8].Y - 17, m_ImgRect[i+8].X + 15, m_ImgRect[i+8].Bottom + 25);
                    if (i < 7)
                    {
                        g.DrawString("34-Q2" + (i+3).ToString(), HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i+8].X-4 , m_ImgRect[i+8].Bottom + 4);
                    }
                    else if (i == 8)
                    {
                        g.DrawString("34-Q30", HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i+8].X-4 , m_ImgRect[i+8].Bottom + 4);
                    }
                    else if (i == 9)
                    {
                        g.DrawString("34-Q46", HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i+8].X-4, m_ImgRect[i+8].Bottom + 4);
                    }
                    else
                    {
                        g.DrawString("34-Q3"+(i-8).ToString(), HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i+8].X-4, m_ImgRect[i+8].Bottom + 4);
                    }

                    if (i > 1)
                    {
                        g.DrawString(m_Strs[i-2], HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[i+8].X-4, m_ImgRect[i+8].Bottom + 29);
                    }
                }

                g.DrawString("风机1", HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[13].X, m_ImgRect[13].Bottom + 46);
                g.DrawString("风机2", HxCommon.Font12B, HxCommon.WhiteBrush, m_ImgRect[15].X, m_ImgRect[15].Bottom + 46);

                    if (m_IsContactor[3])//第四个接触器断开
                    {
                        g.DrawLine(m_LinePen3, m_Point2S[17].X + 4, m_Point2S[17].Y + 6, m_Point2S[17].X + 4, m_Point2S[17].Y + 30);
                        g.DrawLine(m_LinePen3, m_Point2S[17].X + 4, m_Point2S[17].Y + 45, m_Point2S[17].X + 4, m_Point2S[17].Y + 70);
                        g.DrawLine(m_LinePen3, m_Point2S[17].X + 4, m_Point2S[17].Y + 45, m_Point2S[17].X - 4, m_Point2S[17].Y + 30);
                    }
                    else
                    {
                        g.DrawLine(m_LinePen3, m_Point2S[17].X + 4, m_Point2S[17].Y + 6, m_Point2S[17].X + 4, m_Point2S[17].Y + 28);
                        g.DrawLine(m_LinePen3, m_Point2S[17].X + 4, m_Point2S[17].Y + 45, m_Point2S[17].X + 4, m_Point2S[17].Y + 70);
                        g.DrawLine(m_GreenPen, m_Point2S[17].X + 4, m_Point2S[17].Y + 28, m_Point2S[17].X - 3, m_Point2S[17].Y + 28);
                        g.DrawLine(m_GreenPen, m_Point2S[17].X + 4, m_Point2S[17].Y + 45, m_Point2S[17].X, m_Point2S[17].Y + 22);
                    }

            g.DrawString("31-K47", HxCommon.Font12B, HxCommon.WhiteBrush, m_Point2S[17].X-20, m_Point2S[17].Y+42);

            g.DrawImage(m_ImageList[0], m_Point2S[17].X-30, m_Point2S[17].Y+67, 10, 10);
            g.DrawImage(m_ImageList[0], m_Point2S[17].X -1, m_Point2S[17].Y + 67, 10, 10);
            g.DrawImage(m_ImageList[0], m_Point2S[17].X +24, m_Point2S[17].Y + 67, 10, 10);

            g.DrawLine(m_LinePen3, m_Point2S[17].X - 25, m_Point2S[17].Y + 77, m_Point2S[17].X - 25, m_Point2S[17].Y + 95);
            g.DrawLine(m_LinePen3, m_Point2S[17].X + 29, m_Point2S[17].Y + 77, m_Point2S[17].X + 29, m_Point2S[17].Y + 95);
            g.DrawLine(m_LinePen3, m_Point2S[17].X - 19, m_Point2S[17].Y + 72, m_Point2S[17].X -1, m_Point2S[17].Y + 72);
            g.DrawLine(m_LinePen3, m_Point2S[17].X +9, m_Point2S[17].Y + 72, m_Point2S[17].X+24, m_Point2S[17].Y + 72);
            g.DrawString("34-X71 34-F72", HxCommon.Font10B, HxCommon.WhiteBrush, m_Point2S[17].X - 40, m_Point2S[17].Y + 100);

            if (m_IsContactor[4])//第五个接触器断开
            {
                g.DrawLine(m_LinePen3, m_ImgRect[8].X + 15, m_ImgRect[8].Bottom + 41, m_ImgRect[8].X + 15, m_ImgRect[8].Bottom + 67);
                g.DrawLine(m_LinePen3, m_ImgRect[8].X + 15, m_ImgRect[8].Bottom + 41, m_ImgRect[8].X + 6, m_ImgRect[8].Bottom + 25);
            }
            else
            {
                g.DrawLine(m_LinePen3, m_ImgRect[8].X + 15, m_ImgRect[8].Bottom + 41, m_ImgRect[8].X + 15, m_ImgRect[8].Bottom + 67);
                g.DrawLine(m_GreenPen, m_ImgRect[8].X + 15, m_ImgRect[8].Bottom + 41, m_ImgRect[8].X + 9, m_ImgRect[8].Bottom +22);
                g.DrawLine(m_GreenPen, m_ImgRect[8].X + 15, m_ImgRect[8].Bottom + 25, m_ImgRect[8].X + 6, m_ImgRect[8].Bottom +25);
            }
            g.DrawString("34-K23", HxCommon.Font10B, HxCommon.WhiteBrush, m_ImgRect[8].X - 5, m_ImgRect[8].Bottom+55);
            g.DrawString("压缩机1", HxCommon.Font10B, HxCommon.WhiteBrush, m_ImgRect[8].X - 5, m_ImgRect[8].Bottom +70);

            if (m_IsContactor[5])//第六个接触器断开
            {
                g.DrawLine(m_LinePen3, m_ImgRect[9].X + 15, m_ImgRect[9].Bottom + 41, m_ImgRect[9].X + 15, m_ImgRect[9].Bottom + 67);
                g.DrawLine(m_LinePen3, m_ImgRect[9].X + 15, m_ImgRect[9].Bottom + 41, m_ImgRect[9].X + 6, m_ImgRect[9].Bottom + 25);
            }
            else
            {
                g.DrawLine(m_LinePen3, m_ImgRect[9].X + 15, m_ImgRect[9].Bottom + 41, m_ImgRect[9].X + 15, m_ImgRect[9].Bottom + 67);
                g.DrawLine(m_GreenPen, m_ImgRect[9].X + 15, m_ImgRect[9].Bottom + 41, m_ImgRect[9].X + 9, m_ImgRect[9].Bottom + 22);
                g.DrawLine(m_GreenPen, m_ImgRect[9].X + 15, m_ImgRect[9].Bottom + 25, m_ImgRect[9].X + 6, m_ImgRect[9].Bottom + 25);
            }
            g.DrawString("34-K24", HxCommon.Font10B, HxCommon.WhiteBrush, m_ImgRect[9].X - 5, m_ImgRect[9].Bottom + 55);
            g.DrawString("压缩机2", HxCommon.Font10B, HxCommon.WhiteBrush, m_ImgRect[9].X - 5, m_ImgRect[9].Bottom + 70);


            #endregion

           

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
                        case 0:
                            break;
                        case 1://跳转到温度视图
                            append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 2://跳转到网络界面
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 3://跳转到辅助系统
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 4://跳转到牵引状态
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7://跳转到辅机测试 
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 8://跳转到库内动车 
                            append_postCmd(CmdType.ChangePage, 12, 0, 0);
                            break;
                        case 9://返回主界面
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
