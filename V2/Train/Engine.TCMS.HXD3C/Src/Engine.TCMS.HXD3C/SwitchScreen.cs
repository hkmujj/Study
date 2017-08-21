using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    class SwitchScreen : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "开关状态画面";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            nErrorObjectIndex = -1;

            m_Img = this.GetImages();

            InitData();
            return true;
        }

        #endregion#

        #region ::::::::::::::::::::::::event override:::::::::::::::::::::::::::::#

        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < 2; index++)
            {
                if (m_Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    if (m_IsShowButton[0])
                    {
                        m_IsDown[0] = false;
                        if (m_Menu > 0)
                        {
                            m_Menu--;
                        }
                    }
                    break;
                case 1:
                    if (m_IsShowButton[1])
                    {
                        m_IsDown[1] = false;
                        if (m_Menu < 2)
                        {
                            m_Menu++;
                        }
                    }
                    break;
                default:
                    return false;
            }
            return true;

        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < 2; index++)
            {
                if (m_Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    if (m_IsShowButton[0])
                    {
                        m_IsDown[0] = true;
                    }
                    break;
                case 1:
                    if (m_IsShowButton[1])
                    {
                        m_IsDown[1] = true;
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }
        #endregion#

        #region :::::::::::::::::::::::: ex funes ::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 更新数据
        /// </summary>
        private void GetValue()
        {
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
                m_OldbValue[i] = BoolOldList[UIObj.InBoolList[i]];
            }
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
            for (int i = 0; i < UIObj.OutBoolList.Count; i++)
            {
                m_SetBValue[i] = BoolList[UIObj.OutBoolList[i]];
                m_SetBValueNumb[i] = UIObj.OutBoolList[i];
            }

            if (m_Menu > 0)
                m_IsShowButton[0] = true;
            else
                m_IsShowButton[0] = false;

            if (m_Menu < 2)
                m_IsShowButton[1] = true;
            else
                m_IsShowButton[1] = false;
        }

        /// <summary>
        /// 长条框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRect(Graphics e)
        {
            switch (m_Menu)
            {
                case 0:
                    for (int i = 0; i < 16; i++)
                    {
                        if (m_BValue[0 + i * 2])
                        {
                            e.FillRectangle(Common.GreenBrush, m_Rects[4 + m_Menu0Array[i]]);
                            e.DrawString(m_Menu0String[i], Common.Txt12FontB, Common.BlackBrush,
                                m_Rects[4 + m_Menu0Array[i]], Common.DrawFormat);
                        }
                        else if (m_BValue[1 + i * 2])
                        {
                            e.FillRectangle(Common.RedBrush, m_Rects[4 + m_Menu0Array[i]]);
                            e.DrawString(m_Menu0String[i], Common.Txt12FontB, Common.WhiteBrush,
                                m_Rects[4 + m_Menu0Array[i]], Common.DrawFormat);
                        }
                        else
                            e.DrawString(m_Menu0String[i], Common.Txt12FontB, Common.WhiteBrush,
                                m_Rects[4 + m_Menu0Array[i]], Common.DrawFormat);
                        e.DrawRectangle(Common.WhitePen1, m_Rects[4 + m_Menu0Array[i]]);
                    }
                    break;
                case 1:
                    for (int i = 0; i < 16; i++)
                    {
                        if (m_BValue[32 + i * 2])
                        {
                            e.FillRectangle(Common.GreenBrush, m_Rects[4 + m_Menu1Array[i]]);
                            e.DrawString(m_Menu1String[i], Common.Txt12FontB, Common.BlackBrush,
                                m_Rects[4 + m_Menu1Array[i]], Common.DrawFormat);
                        }
                        else if (m_BValue[33 + i * 2])
                        {
                            e.FillRectangle(Common.RedBrush, m_Rects[4 + m_Menu1Array[i]]);
                            e.DrawString(m_Menu1String[i], Common.Txt12FontB, Common.WhiteBrush,
                                m_Rects[4 + m_Menu1Array[i]], Common.DrawFormat);
                        }
                        else
                            e.DrawString(m_Menu1String[i], Common.Txt12FontB, Common.WhiteBrush,
                                m_Rects[4 + m_Menu1Array[i]], Common.DrawFormat);
                        e.DrawRectangle(Common.WhitePen1, m_Rects[4 + m_Menu1Array[i]]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < 12; i++)
                    {
                        if (m_BValue[64 + i * 2])
                        {
                            e.FillRectangle(Common.GreenBrush, m_Rects[4 + m_Menu2Array[i]]);
                            e.DrawString(m_Menu2String[i], Common.Txt12FontB, Common.BlackBrush,
                                m_Rects[4 + m_Menu2Array[i]], Common.DrawFormat);
                        }
                        else if (m_BValue[65 + i * 2])
                        {
                            e.FillRectangle(Common.RedBrush, m_Rects[4 + m_Menu2Array[i]]);
                            e.DrawString(m_Menu2String[i], Common.Txt12FontB, Common.WhiteBrush,
                                m_Rects[4 + m_Menu2Array[i]], Common.DrawFormat);
                        }
                        else
                            e.DrawString(m_Menu2String[i], Common.Txt12FontB, Common.WhiteBrush,
                                m_Rects[4 + m_Menu2Array[i]], Common.DrawFormat);
                        e.DrawRectangle(Common.WhitePen1, m_Rects[4 + m_Menu2Array[i]]);
                    }
                    break;
            }
        }

        private void DrawForm(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_IsShowButton[i])
                {
                    m_HXD3Buttons[i].DrawPolygonButoonFillAndNoState(e);
                    e.DrawString(m_Str[i], Common.Txt14FontB, Common.WhiteBrush, m_Rects[2 + i], Common.DrawFormat);
                }
            }
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawRect(e);
            DrawForm(e);
        }

        #endregion#

        #region:::::::::::::::所有坐标点的初始化、表盘和进度条的初始化:::::::::::::::#
        /// <summary>
        /// 初始化坐标点
        /// </summary>
        private void InitData()
        {
            Common.DrawFormat.Alignment = (StringAlignment)1;
            Common.DrawFormat.LineAlignment = (StringAlignment)1;

            Common.RightFormat.Alignment = (StringAlignment)2;
            Common.RightFormat.LineAlignment = (StringAlignment)1;

            Common.LeftFormat.Alignment = (StringAlignment)0;
            Common.LeftFormat.LineAlignment = (StringAlignment)1;


            #region ::::::::::::::::::::::::坐标点:::::::::::::::::::::::::::::#
            m_PDrawPoint[0] = new Point(20, 220);
            m_PDrawPoint[1] = new Point(130, 215);
            m_PDrawPoint[2] = new Point(0, 380);
            m_PDrawPoint[3] = new Point(142, 385);
            m_PDrawPoint[4] = new Point(142, 415);

            //point1
            m_PDrawPoint[5] = new Point(440, 440);
            m_PDrawPoint[6] = new Point(400, 455);
            m_PDrawPoint[7] = new Point(400, 485);
            m_PDrawPoint[8] = new Point(480, 485);
            m_PDrawPoint[9] = new Point(480, 455);

            //point11
            m_PDrawPoint[10] = new Point(440, 445);
            m_PDrawPoint[11] = new Point(405, 460);
            m_PDrawPoint[12] = new Point(405, 480);
            m_PDrawPoint[13] = new Point(475, 480);
            m_PDrawPoint[14] = new Point(475, 460);

            //point2
            m_PDrawPoint[15] = new Point(400, 485);
            m_PDrawPoint[16] = new Point(400, 515);
            m_PDrawPoint[17] = new Point(440, 530);
            m_PDrawPoint[18] = new Point(480, 515);
            m_PDrawPoint[19] = new Point(480, 485);

            //point21
            m_PDrawPoint[20] = new Point(405, 490);
            m_PDrawPoint[21] = new Point(405, 510);
            m_PDrawPoint[22] = new Point(440, 525);
            m_PDrawPoint[23] = new Point(475, 510);
            m_PDrawPoint[24] = new Point(475, 490);
            #endregion#

            m_Rects = new Rectangle[50];
            m_Rects[0] = new Rectangle(400, 440, 80, 45);
            m_Rects[1] = new Rectangle(400, 485, 80, 45);

            m_Rects[2] = new Rectangle(400, 450, 80, 40);
            m_Rects[3] = new Rectangle(400, 485, 80, 40);

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    //4-19
                    m_Rects[4 + j * 4 + i] = new Rectangle(5 + i * 95, 210 + j * 40, 90, 35);
                }
                for (int k = 0; k < 4; k++)
                {
                    //20-35
                    m_Rects[20 + j * 4 + k] = new Rectangle(5 + k * 95, 370 + j * 40, 90, 35);
                }
            }

            m_Rect = new List<Region>();
            m_Rect.Add(new Region(m_Rects[0]));
            m_Rect.Add(new Region(m_Rects[1]));

            m_Points1 = new Point[5] { m_PDrawPoint[5], m_PDrawPoint[6], m_PDrawPoint[7], m_PDrawPoint[8], m_PDrawPoint[9] };
            m_Points11 = new Point[5] { m_PDrawPoint[10], m_PDrawPoint[11], m_PDrawPoint[12], m_PDrawPoint[13], m_PDrawPoint[14] };
            m_Points2 = new Point[5] { m_PDrawPoint[15], m_PDrawPoint[16], m_PDrawPoint[17], m_PDrawPoint[18], m_PDrawPoint[19] };
            m_Points21 = new Point[5] { m_PDrawPoint[20], m_PDrawPoint[21], m_PDrawPoint[22], m_PDrawPoint[23], m_PDrawPoint[24] };

            m_HXD3Buttons = new HXD3Button[2];
            m_HXD3Buttons[0] = new HXD3Button(m_Points1, m_Points11);
            m_HXD3Buttons[1] = new HXD3Button(m_Points2, m_Points21);

            m_Menu0Array = new int[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            m_Menu0String = new string[16] { "CI1AK", "CI2AK", "CI3AK", "CI4AK", 
                "CI5AK", "CI6AK", "CI1K", "CI2K",
                "CI3K", "CI4K","CI5K", "CI6K",
                "QS1","QS2", "QS3", "QS4" };

            m_Menu1Array = new int[16] { 0, 1, 2, 3, 4, 5, 8, 9, 10, 11, 16, 17, 18, 20, 21, 24 };
            m_Menu1String = new string[16] { "QA11", "QA12", "QA23", "QA24",
                "QA13", "QA14",
                "QA19", "QA20", "QA21", "QA22",
                "KM11", "KM12", "KM20",
                "KM13", "KM14",
                "QS11" };

            m_Menu2Array = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 16 };
            m_Menu2String = new string[12] { "SA41", "SA42", "SA43", "SA44",
                "SA45", "SA46", "SA47", "SA48",
                "SA75",
                "KP52", "KP53",
                "KC1" };
        }

        #endregion#

        #region :::::::::::::::::::::::: value init :::::::::::::::::::::::::::::::#

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        public bool[] m_BValue = new bool[100];

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        public bool[] m_OldbValue = new bool[100];

        /// <summary>
        /// 发送的数字量
        /// </summary>
        public bool[] m_SetBValue = new bool[10];

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        public int[] m_SetBValueNumb = new int[10];

        /// <summary>
        /// 接收模拟量
        /// </summary>
        public float[] m_FValue = new float[30];

        /// <summary>
        /// 坐标集
        /// </summary>
        public Point[] m_PDrawPoint = new Point[80];

        /// <summary>
        /// 图片集
        /// </summary>
        public static Image[] m_Img = new Image[30];
        #endregion#

        string[] m_UpDownStr = new string[2] { "上一页", "下一页" };

        private readonly bool[] m_IsShowButton = new bool[2] { false, true };
        private readonly bool[] m_IsDown = new bool[2];

        /// <summary>
        /// 按键区域
        /// </summary>
        public Rectangle[] m_Rects;


        public Point[] m_Points1;
        public Point[] m_Points11;

        public Point[] m_Points2;
        public Point[] m_Points21;

        public string[] m_Str = new string[2] { "上一页", "下一页" };

        /// <summary>
        /// 两个按键
        /// </summary>
        public HXD3Button[] m_HXD3Buttons;

        public int[] m_Menu0Array;
        public string[] m_Menu0String;

        public int[] m_Menu1Array;
        public string[] m_Menu1String;

        public int[] m_Menu2Array;
        public string[] m_Menu2String;

        /// <summary>
        /// 按键列表
        /// </summary>
        public List<Region> m_Rect;

        /// <summary>
        /// 菜单页
        /// </summary>
        public int m_Menu = 0;
        #endregion#
    }
}