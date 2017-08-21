using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_NetWorkView : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //底部数字按钮是否按下
        private Rectangle m_Cab1Rect; //cab1区域
        private Rectangle m_Cab2Rect; //cab2区域
        private Rectangle m_MioRect; //MIO区域
        private Rectangle m_CCURect; //CCU区域

        private readonly HxRectText[] m_Cab1Button = new HxRectText[4]; //cab1区设备图标
        private readonly HxRectText[] m_Cab2Button = new HxRectText[4]; //cab2区设备图标
        private readonly HxRectText[] m_MioButton = new HxRectText[7]; //MIO区设备图标
        private readonly HxRectText[] m_CCUButton = new HxRectText[4]; //CCU区设备图标
        private readonly HxRectText[] m_OtherButton = new HxRectText[5]; //其余部分

        private readonly string[] m_StrCab1S = new string[4] {"DXM11", "DIM12", "AXM13", "IDU1"};
        private readonly string[] m_StrCab2S = new string[4] {"DXM21", "DIM22", "AXM23", "IDU2"};
        private readonly string[] m_StrCcu = new string[5] {"ACU1", "TCU1", "BCU", "ACU2", "TCU2"};
        private readonly string[] m_StrCcuCenter = {"GWM/OCE", "ERM", "VCM2", "VCM1"};
        private readonly Pen m_LightPen = new Pen(Color.FromArgb(192, 191, 191), 1);
        private readonly Pen m_GrayPen = new Pen(Color.FromArgb(192, 191, 191), 2);
        private readonly Pen m_GreenPen = new Pen(Color.FromArgb(149, 231, 150), 2);
        private readonly Pen m_RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        private readonly Point[] m_PointA = new Point[6];
        private readonly Point[] m_PointB = new Point[6];

        public int m_StarBit; //Bool数据起始位置
        public int m_BitCount; //Bool数据量的个数

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 6)
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
            if (GlobalParam.Instance.CurrentViewId == 6)
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
            return "网络拓扑视图";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            if (UIObj.InBoolList.Count >= 2)
            {
                m_StarBit = UIObj.InBoolList[0];
                m_BitCount = UIObj.InBoolList[1];
            }

            nErrorObjectIndex = -1;
            return true;
        }

        public void InitData()
        {
            #region 各区域初始化

            m_Cab1Rect = new Rectangle(HxCommon.Recposition.X + 15, HxCommon.Recposition.Y + 40, 150, 300);
            m_Cab2Rect = new Rectangle(HxCommon.Recposition.X + 640, HxCommon.Recposition.Y + 40, 150, 300);
            m_MioRect = new Rectangle(m_Cab1Rect.X + 60, m_Cab1Rect.Bottom + 65, 580, 64);
            m_CCURect = new Rectangle(HxCommon.Recposition.X + 360, HxCommon.Recposition.Y + 100, 170, 240);

            #endregion

            #region

            /*-----------------------------------------------------
              * 
              *            设备区域初始化
              * 
              *-----------------------------------------------------
              */
            //cab1区
            for (int i = 0; i < 4; i++)
            {
                m_Cab1Button[i] = new HxRectText();
                m_Cab1Button[i].SetBkColor(198, 195, 198);
                m_Cab1Button[i].SetText(m_StrCab1S[i]);
                m_Cab1Button[i].SetTextColor(0, 0, 0);
                m_Cab1Button[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_Cab1Button[i].SetLinePen(143, 142, 141, 2);
                m_Cab1Button[i].SetDrawFrm(true);

                if (i < 3)
                {
                    m_Cab1Button[i].SetTextRect(m_Cab1Rect.X + 65, m_Cab1Rect.Y + 5 + 60*i, 75, 55);
                }
                else
                {
                    m_Cab1Button[i].SetTextRect(m_Cab1Rect.X + 65, m_Cab1Rect.Bottom - 60, 75, 55);
                }
            }

            //cab2区
            for (int i = 0; i < 4; i++)
            {
                m_Cab2Button[i] = new HxRectText();
                m_Cab2Button[i].SetBkColor(198, 195, 198);
                m_Cab2Button[i].SetText(m_StrCab2S[i]);
                m_Cab2Button[i].SetTextColor(0, 0, 0);
                m_Cab2Button[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_Cab2Button[i].SetLinePen(143, 142, 141, 2);
                m_Cab2Button[i].SetDrawFrm(true);

                if (i < 3)
                {
                    m_Cab2Button[i].SetTextRect(m_Cab2Rect.X + 5, m_Cab1Rect.Y + 5 + 60*i, 75, 55);
                }
                else
                {
                    m_Cab2Button[i].SetTextRect(m_Cab2Rect.X + 5, m_Cab1Rect.Bottom - 60, 75, 55);
                }
            }

            //MIO区
            for (int i = 0; i < 7; i++)
            {
                m_MioButton[i] = new HxRectText();
                m_MioButton[i].SetBkColor(198, 195, 198);
                m_MioButton[i].SetText("DXM3" + (i + 1).ToString());
                m_MioButton[i].SetTextColor(0, 0, 0);
                m_MioButton[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_MioButton[i].SetLinePen(143, 142, 141, 2);
                m_MioButton[i].SetDrawFrm(true);
                m_MioButton[i].SetTextRect(m_MioRect.X + 5 + 83*i, m_MioRect.Y + 4, 75, 55);
            }

            //CCU区
            for (int i = 0; i < 4; i++)
            {
                m_CCUButton[i] = new HxRectText();
                m_CCUButton[i].SetBkColor(198, 195, 198);
                m_CCUButton[i].SetText(m_StrCcuCenter[i]);
                m_CCUButton[i].SetTextColor(0, 0, 0);
                m_CCUButton[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_CCUButton[i].SetLinePen(143, 142, 141, 2);
                m_CCUButton[i].SetDrawFrm(true);
            }

            m_CCUButton[0].SetTextRect(m_CCURect.X + 4, m_CCURect.Y + 4, 75, 55);
            m_CCUButton[1].SetTextRect(m_CCURect.X + 4, m_CCURect.Bottom - 59, 75, 55);
            m_CCUButton[2].SetTextRect(m_CCURect.Right - 79, m_CCURect.Y + 4, 75, 55);
            m_CCUButton[3].SetTextRect(m_CCURect.Right - 79, m_CCURect.Bottom - 59, 75, 55);

            //其他区
            for (int i = 0; i < 5; i++)
            {
                m_OtherButton[i] = new HxRectText();
                m_OtherButton[i].SetBkColor(198, 195, 198);
                m_OtherButton[i].SetTextColor(0, 0, 0);
                m_OtherButton[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_OtherButton[i].SetLinePen(143, 142, 141, 2);
                m_OtherButton[i].SetDrawFrm(true);
                m_OtherButton[i].SetText(m_StrCcu[i]);
            }
            m_OtherButton[0].SetTextRect(m_Cab1Button[1].m_RectPosition.Right + 25, m_Cab1Button[1].m_RectPosition.Y, 75, 55);
            m_OtherButton[1].SetTextRect(m_Cab1Button[3].m_RectPosition.Right + 25, m_Cab1Button[3].m_RectPosition.Y, 75, 55);
            m_OtherButton[2].SetTextRect(m_OtherButton[0].m_RectPosition.Right + 15, m_OtherButton[0].m_RectPosition.Y, 75, 55);
            m_OtherButton[3].SetTextRect(m_CCURect.Right + 15, m_OtherButton[0].m_RectPosition.Y, 75, 55);
            m_OtherButton[4].SetTextRect(m_CCURect.Right + 15, m_OtherButton[1].m_RectPosition.Y, 75, 55);

            #endregion

            #region 线条坐标初始化 

            m_PointA[0] = new Point(m_Cab1Rect.X + 25, m_Cab1Button[0].m_RectPosition.Y + 15);
            m_PointA[1] = new Point(m_Cab2Rect.Right - 5, m_Cab2Button[0].m_RectPosition.Y + 20);
            m_PointA[2] = new Point(m_Cab1Rect.X + 25, m_Cab1Button[2].m_RectPosition.Bottom + 20);
            m_PointA[3] = new Point(m_Cab2Rect.Right - 5, m_PointA[2].Y);
            m_PointA[4] = new Point(m_MioButton[0].m_RectPosition.X + 25, m_MioButton[0].m_RectPosition.Y - 45);
            m_PointA[5] = new Point(m_MioButton[6].m_RectPosition.X + 25, m_MioButton[6].m_RectPosition.Y - 45);

            m_PointB[0] = new Point(m_PointA[0].X - 10, m_PointA[0].Y + 10);
            m_PointB[1] = new Point(m_PointA[1].X - 10, m_PointA[1].Y + 10);
            m_PointB[2] = new Point(m_PointA[2].X - 10, m_PointA[2].Y + 10);
            m_PointB[3] = new Point(m_PointA[3].X - 10, m_PointA[3].Y + 10);
            m_PointB[4] = new Point(m_PointA[4].X + 10, m_PointA[4].Y + 10);
            m_PointB[5] = new Point(m_PointA[5].X + 10, m_PointA[5].Y + 10);


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
            HxCommon.HTitle.SetText("网络拓扑");

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
                if (i == 2)
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

            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i + 2]];
            }
        }

        public void DrawOn(Graphics g)
        {
            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //各区域位置的绘制
            g.DrawRectangle(m_LightPen, m_Cab1Rect);
            g.DrawRectangle(m_LightPen, m_Cab2Rect);
            g.DrawRectangle(m_LightPen, m_CCURect);
            g.DrawRectangle(m_LightPen, m_MioRect);

            #region 各区域图形绘制

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_StarBit + 2*i]) //cab1设备处于正常状态
                {
                    m_Cab1Button[i].SetBkColor(146, 245, 133);
                    m_Cab1Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_GreenPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 15,
                            m_PointA[0].X, m_Cab1Button[i].m_RectPosition.Y + 15);
                        g.DrawLine(m_GreenPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 25,
                            m_PointB[0].X, m_Cab1Button[i].m_RectPosition.Y + 25);

                    }
                    else
                    {
                        g.DrawLine(m_GreenPen, m_Cab1Button[i].m_RectPosition.X + 25, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_Cab1Button[i].m_RectPosition.X + 35, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
                else if (BoolList[m_StarBit + 2*i + 1]) //cab1设备处于故障状态
                {
                    m_Cab1Button[i].SetBkColor(255, 0, 0);
                    m_Cab1Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_RedPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 15, m_PointA[0].X,
                            m_Cab1Button[i].m_RectPosition.Y + 15);
                        g.DrawLine(m_RedPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 25, m_PointB[0].X,
                            m_Cab1Button[i].m_RectPosition.Y + 25);

                    }
                    else
                    {
                        g.DrawLine(m_RedPen, m_Cab1Button[i].m_RectPosition.X + 25, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_Cab1Button[i].m_RectPosition.X + 35, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
                else
                {
                    m_Cab1Button[i].SetBkColor(198, 195, 198);
                    m_Cab1Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_GrayPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 15, m_PointA[0].X,
                            m_Cab1Button[i].m_RectPosition.Y + 15);
                        g.DrawLine(m_GrayPen, m_Cab1Button[i].m_RectPosition.X, m_Cab1Button[i].m_RectPosition.Y + 25, m_PointB[0].X,
                            m_Cab1Button[i].m_RectPosition.Y + 25);

                    }
                    else
                    {
                        g.DrawLine(m_GrayPen, m_Cab1Button[i].m_RectPosition.X + 25, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_Cab1Button[i].m_RectPosition.X + 35, m_Cab1Button[i].m_RectPosition.Y,
                            m_Cab1Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
            }

            //cab2
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_StarBit + 2*i + 8]) //cab1设备处于正常状态
                {
                    m_Cab2Button[i].SetBkColor(146, 245, 133);
                    m_Cab2Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_GreenPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 20,
                            m_PointA[1].X, m_Cab2Button[i].m_RectPosition.Y + 20);
                        g.DrawLine(m_GreenPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 30,
                            m_PointB[1].X, m_Cab2Button[i].m_RectPosition.Y + 30);

                    }
                    else
                    {
                        g.DrawLine(m_GreenPen, m_Cab2Button[i].m_RectPosition.X + 25, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_Cab2Button[i].m_RectPosition.X + 35, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
                else if (BoolList[m_StarBit + 2*i + 9]) //cab1设备处于故障状态
                {
                    m_Cab2Button[i].SetBkColor(255, 0, 0);
                    m_Cab2Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_RedPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 20,
                            m_PointA[1].X, m_Cab2Button[i].m_RectPosition.Y + 20);
                        g.DrawLine(m_RedPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 30,
                            m_PointB[1].X, m_Cab2Button[i].m_RectPosition.Y + 30);

                    }
                    else
                    {
                        g.DrawLine(m_RedPen, m_Cab2Button[i].m_RectPosition.X + 25, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_Cab2Button[i].m_RectPosition.X + 35, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
                else
                {
                    m_Cab2Button[i].SetBkColor(198, 195, 198);
                    m_Cab2Button[i].OnDraw(g);
                    if (i < 3)
                    {
                        g.DrawLine(m_GrayPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 20,
                            m_PointA[1].X, m_Cab2Button[i].m_RectPosition.Y + 20);
                        g.DrawLine(m_GrayPen, m_Cab2Button[i].m_RectPosition.Right, m_Cab1Button[i].m_RectPosition.Y + 30,
                            m_PointB[1].X, m_Cab2Button[i].m_RectPosition.Y + 30);
                    }
                    else
                    {
                        g.DrawLine(m_GrayPen, m_Cab2Button[i].m_RectPosition.X + 25, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 25, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_Cab2Button[i].m_RectPosition.X + 35, m_Cab2Button[i].m_RectPosition.Y,
                            m_Cab2Button[i].m_RectPosition.X + 35, m_PointB[2].Y);
                    }
                }
            }

            //ccu
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_StarBit + 2*i + 16]) //ccu设备处于正常状态
                {
                    m_CCUButton[i].SetBkColor(146, 245, 133);
                    m_CCUButton[i].OnDraw(g);
                    if (i == 0 || i == 2)
                    {
                        g.DrawLine(m_GreenPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else
                    {
                        g.DrawLine(m_GreenPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
                else if (BoolList[m_StarBit + 2*i + 17]) //ccu设备处于故障状态
                {
                    m_CCUButton[i].SetBkColor(255, 0, 0);
                    m_CCUButton[i].OnDraw(g);
                    if (i == 0 || i == 2)
                    {
                        g.DrawLine(m_RedPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else
                    {
                        g.DrawLine(m_RedPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
                else
                {
                    m_CCUButton[i].SetBkColor(198, 195, 198);
                    m_CCUButton[i].OnDraw(g);
                    if (i == 0 || i == 2)
                    {
                        g.DrawLine(m_GrayPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Bottom,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else
                    {
                        g.DrawLine(m_GrayPen, m_CCUButton[i].m_RectPosition.X + 35, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_CCUButton[i].m_RectPosition.X + 45, m_CCUButton[i].m_RectPosition.Y,
                            m_CCUButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
            }

            //MIO
            for (int i = 0; i < 7; i++)
            {
                if (BoolList[m_StarBit + 2*i + 24]) //mio设备处于正常状态
                {
                    m_MioButton[i].SetBkColor(146, 245, 133);
                    m_MioButton[i].OnDraw(g);
                    g.DrawLine(m_GreenPen, m_MioButton[i].m_RectPosition.X + 25, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 25, m_PointA[4].Y);
                    g.DrawLine(m_GreenPen, m_MioButton[i].m_RectPosition.X + 35, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 35, m_PointB[4].Y);
                }
                else if (BoolList[m_StarBit + 2*i + 25]) //mio设备处于故障状态
                {
                    m_MioButton[i].SetBkColor(255, 0, 0);
                    m_MioButton[i].OnDraw(g);
                    g.DrawLine(m_RedPen, m_MioButton[i].m_RectPosition.X + 25, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 25, m_PointA[4].Y);
                    g.DrawLine(m_RedPen, m_MioButton[i].m_RectPosition.X + 35, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 35, m_PointB[4].Y);
                }
                else
                {
                    m_MioButton[i].SetBkColor(198, 195, 198);
                    m_MioButton[i].OnDraw(g);
                    g.DrawLine(m_GrayPen, m_MioButton[i].m_RectPosition.X + 25, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 25, m_PointA[4].Y);
                    g.DrawLine(m_GrayPen, m_MioButton[i].m_RectPosition.X + 35, m_MioButton[i].m_RectPosition.Y,
                        m_MioButton[i].m_RectPosition.X + 35, m_PointB[4].Y);
                }
            }

            //other
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[m_StarBit + 2*i + 38]) //other设备处于正常状态
                {
                    m_OtherButton[i].SetBkColor(146, 245, 133);
                    m_OtherButton[i].OnDraw(g);
                    if (i == 1 || i == 4)
                    {
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[4].Y);
                    }
                    else if (i == 2)
                    {
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[4].Y);
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[4].Y);
                    }
                    else
                    {
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GreenPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
                else if (BoolList[m_StarBit + 2*i + 39]) //other设备处于故障状态
                {
                    m_OtherButton[i].SetBkColor(255, 0, 0);
                    m_OtherButton[i].OnDraw(g);
                    if (i == 1 || i == 4)
                    {
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else if (i == 2)
                    {
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[4].Y);
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[4].Y);
                    }
                    else
                    {
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_RedPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
                else
                {
                    m_OtherButton[i].SetBkColor(198, 195, 198);
                    m_OtherButton[i].OnDraw(g);
                    if (i == 1 || i == 4)
                    {
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Y,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                    else if (i == 2)
                    {
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[4].Y);
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[4].Y);
                    }
                    else
                    {
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 35, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 35, m_PointA[2].Y);
                        g.DrawLine(m_GrayPen, m_OtherButton[i].m_RectPosition.X + 45, m_OtherButton[i].m_RectPosition.Bottom,
                            m_OtherButton[i].m_RectPosition.X + 45, m_PointB[2].Y);
                    }
                }
            }

            #endregion

            #region 主线条绘制

            if (BoolList[m_StarBit + 48]) //主线A正常
            {
                g.DrawLine(m_GreenPen, m_PointA[0], m_PointA[2]);
                g.DrawLine(m_GreenPen, m_PointA[1], m_PointA[3]);
                g.DrawLine(m_GreenPen, m_PointA[2], m_PointA[3]);
                g.DrawLine(m_GreenPen, m_PointA[4], m_PointA[5]);

            }
            else if (BoolList[m_StarBit + 49]) //主线A故障
            {
                g.DrawLine(m_RedPen, m_PointA[0], m_PointA[2]);
                g.DrawLine(m_RedPen, m_PointA[1], m_PointA[3]);
                g.DrawLine(m_RedPen, m_PointA[2], m_PointA[3]);
                g.DrawLine(m_RedPen, m_PointA[4], m_PointA[5]);
            }
            else
            {
                g.DrawLine(m_GrayPen, m_PointA[0], m_PointA[2]);
                g.DrawLine(m_GrayPen, m_PointA[1], m_PointA[3]);
                g.DrawLine(m_GrayPen, m_PointA[2], m_PointA[3]);
                g.DrawLine(m_GrayPen, m_PointA[4], m_PointA[5]);
            }

            if (BoolList[m_StarBit + 50]) //主线B正常
            {
                g.DrawLine(m_GreenPen, m_PointB[0], m_PointB[2]);
                g.DrawLine(m_GreenPen, m_PointB[1], m_PointB[3]);
                g.DrawLine(m_GreenPen, m_PointB[2], m_PointB[3]);
                g.DrawLine(m_GreenPen, m_PointB[4], m_PointB[5]);

            }
            else if (BoolList[m_StarBit + 51]) //主线A故障
            {
                g.DrawLine(m_RedPen, m_PointB[0], m_PointB[2]);
                g.DrawLine(m_RedPen, m_PointB[1], m_PointB[3]);
                g.DrawLine(m_RedPen, m_PointB[2], m_PointB[3]);
                g.DrawLine(m_RedPen, m_PointB[4], m_PointB[5]);
            }
            else
            {
                g.DrawLine(m_GrayPen, m_PointB[0], m_PointB[2]);
                g.DrawLine(m_GrayPen, m_PointB[1], m_PointB[3]);
                g.DrawLine(m_GrayPen, m_PointB[2], m_PointB[3]);
                g.DrawLine(m_GrayPen, m_PointB[4], m_PointB[5]);
            }

            #endregion

            g.DrawString("line_A", HxCommon.Font12B, HxCommon.WhiteBrush, m_PointA[2].X + 15, m_PointA[2].Y - 18);
            g.DrawString("line_B", HxCommon.Font12B, HxCommon.WhiteBrush, m_PointB[2].X + 5, m_PointB[2].Y + 5);
            g.DrawString("line_A", HxCommon.Font12B, HxCommon.WhiteBrush, m_OtherButton[1].m_RectPosition.X + 40,
                m_Cab1Rect.Bottom + 5);
            g.DrawString("line_B", HxCommon.Font12B, HxCommon.WhiteBrush, m_MioButton[1].m_RectPosition.Right - 25,
                m_PointB[4].Y + 10);

            g.DrawString("cab1", HxCommon.Font12B, HxCommon.WhiteBrush, m_Cab1Rect.X + 60, m_Cab1Rect.Bottom + 5);
            g.DrawString("cab2", HxCommon.Font12B, HxCommon.WhiteBrush, m_Cab2Rect.X + 60, m_Cab2Rect.Bottom + 5);
            g.DrawString("MIO", HxCommon.Font12B, HxCommon.WhiteBrush, m_MioRect.Right + 5, m_MioRect.Y + 30);
            g.DrawString("CCU", HxCommon.Font12B, HxCommon.WhiteBrush, m_CCURect.X + 65, m_CCURect.Y - 20);



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
                        case 1: //跳转到温度视图
                            append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 2: //跳转到网络界面
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 3: //跳转到辅助系统
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 4: //跳转到牵引状态
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7: //跳转到辅机测试 
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 8: //跳转到库内动车 
                            append_postCmd(CmdType.ChangePage, 12, 0, 0);
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
}
