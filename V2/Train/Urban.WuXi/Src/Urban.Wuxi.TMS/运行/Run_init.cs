using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using FormatStyle = Urban.Wuxi.TMS.FormatStyle;

namespace Urban.Wuxi.TMS.运行
{
    public partial class Run
    {

        //private SolidBrush GetDoorBrush()


        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            #region //字体对齐

            m_DrawFormat.LineAlignment = (StringAlignment)1;
            m_DrawFormat.Alignment = (StringAlignment)1;

            m_RightFormat.LineAlignment = (StringAlignment)2;
            m_RightFormat.Alignment = (StringAlignment)1;

            m_LeftFormat.LineAlignment = (StringAlignment)0;
            m_LeftFormat.Alignment = (StringAlignment)1;
            InitRect();
            m_PDrawPoint = new PointF[20];
            m_Rects = new RectangleF[250];
            m_Rectangles = new Rectangle[100];


            #endregion

            #region //数组，列表的赋值
            //定义乘客报警位置
            for (int i = 0; i < 12; i+=2)
            {
                m_Rectangles[i] = new Rectangle(60 + i * 60, 350, 22, 22);

            }
            for (int i = 1; i < 12; i+=2)
            {
                m_Rectangles[i] = new Rectangle(65 + i * 60, 350, 22, 22);

            }
            //定义空气制动位置
            for (int i = 0; i < 12; i += 2)
            {
                m_Rectangles[i + 12] = new Rectangle(55 + i * 60, 200, 32, 20);
            }
            for (int i = 1; i < 12; i += 2)
            {
                m_Rectangles[i + 12] = new Rectangle(50 + i * 60, 200, 32, 20);
            }
            //定义停放制动位置
            for (int i = 0; i < 5; i++)
            {
                m_Rectangles[i + 24] = new Rectangle(230 + i * 120, 255, 32, 20);
            }
            m_Rectangles[28] = new Rectangle(m_Rectangles[28].X - 30, m_Rectangles[28].Y, m_Rectangles[28].Width, m_Rectangles[28].Height);
            m_Rectangles[29] = new Rectangle(80, 255, 32, 20);

            //定义BHB位置
            for (int i = 0; i < 2; i++)
            {
                m_Rectangles[i + 30] = new Rectangle(335 + i * 120, 319, 22, 22);

                //定义BLB位置
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rectangles[i + 32] = new Rectangle(365 + i * 120, 319, 22, 22);

            }
            //电制动可用性

            for (int i = 0; i < 4; i++)
            {
                m_Rectangles[i + 34] = new Rectangle(179 + i * 120, 255, 22, 22);

            }
            //牵引系统
            for (int i = 0; i < 4; i++)
            {
                m_Rectangles[i + 38] = new Rectangle(193 + i * 120, 288, 44, 17);

            }
            //定义主断状态位置
            for (int i = 0; i < 4; i++)
            {
                m_Rectangles[i + 42] = new Rectangle(179 + i * 120, 317, 24, 24);

            }
            //定义空压机位置
            for (int i = 0; i < 2; i++)
            {
                m_Rectangles[i + 46] = new Rectangle(245 + i * 360, 319, 22, 22);

            }
            var size = new Size(20, 20);
            //SIV位置
            m_Rectangles[49] = new Rectangle(m_Rectangles[28].X + 3, m_Rectangles[28].Y + 30, size.Width, size.Height);
            m_Rectangles[48] = new Rectangle(m_Rectangles[29].X + 3, m_Rectangles[29].Y + 30, size.Width, size.Height);
            //BLB位置
            m_Rectangles[50] = new Rectangle(m_Rectangles[30].X, m_Rectangles[30].Y + 30, size.Width, size.Height);

            #endregion

            #region //对象，数组的初始化
            m_LineRectArr = new RectangleF[100];
            m_BtnRectArr = new RectangleF[100];

            m_Images = new Image[100];

            m_ButtonIsDown = new bool[15];
            m_BtnCanDown = new bool[15] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
            m_Rect = new List<Region>();
            m_BoolIds = new List<int>();

            m_FoolatIds = new List<int>();
            m_DoorStatesList = new List<DoorState>();

            #endregion

            #region//从对象配置表里取东西
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }
            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    m_BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    m_FoolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }


            #endregion


            #region :::::::::::::::::::::::::::::: _rects :::::::::::::::::::::::::::::::::::

            ///////////////////////////////////////
            //  框线
            //////////////////////////////////////

            #region

            //模式框
            m_LineRectArr[70] = new RectangleF(10, 510, 110, 35);
            m_LineRectArr[0] = new RectangleF(40, 125, 115, 390);
            m_LineRectArr[1] = new RectangleF(73, 420, 150, 35);
            m_LineRectArr[2] = new RectangleF(328, 420, 150, 35);
            m_LineRectArr[3] = new RectangleF(600, 420, 120, 35);
            for (int i = 0; i < 5; i++)
            {
                m_LineRectArr[4 + i] = new RectangleF(150 + i * 115, 465, 115, 35);
            }
            #endregion

            ///////////////////////////////////////
            //  图
            //////////////////////////////////////

            ///////////////////////////////////////
            //  按钮
            //////////////////////////////////////

            #region
            //紧急制动、牵引封锁、紧急牵引、限速、其他
            for (int i = 0; i < 5; i++)
            {
                m_BtnRectArr[i] = new RectangleF(150 + i * 115, 465, 115, 35);
            }
            //声音，手动模式，紧急广播，站点设置，旁路，烟火报警
            for (int i = 0; i < 9; i++)
            {
                m_BtnRectArr[5 + i] = new RectangleF(0 + i * 88, 505, 80, 40);
            }
            #endregion

            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 6; i++)
            {
                //制动缸压力
                m_PDrawPoint[i * 2] = new PointF(173 + i * 95, 402);
                m_PDrawPoint[1 + i * 2] = new PointF(173 + i * 95, 431);
            }

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }

            #endregion

            #region :::::::::::::::::::::::::::::::: _rect ::::::::::::::::::::::::::::::::::::::
            //定义提示信息，音量，烟火报警，手动模式，站点设置，旁路的位置
            for (int i = 0; i < 14; i++)
            {
                m_Rect.Add(new Region(m_BtnRectArr[i]));
            }

            #endregion


            for (int i = 0; i < 48; i++)
            {
                m_DoorStatesList.Add(new DoorState(288 + i * 8, m_RectsList[6 + i], m_DoorNameArr[i]));
            }

            m_ComfirmButton = new GDIButton();

            m_ComfirmButton.ButtonDownEvent = (sender, args) =>
            {

            };
            m_ComfirmButton.ButtonUpEvent = (sender, args) =>
            {
                m_ComfirmTime = DateTime.Now;
            };
            m_ComfirmButton.Text = "确定";
            m_ComfirmButton.ContentTextControl.BkColor = Color.FromArgb(144, 163, 175);
            m_ComfirmButton.ContentTextControl.BKImage = m_Images[79];
            m_ComfirmButton.ContentTextControl.TextFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            m_ComfirmButton.ContentTextControl.NeedDarwOutline = true;
            m_ComfirmButton.TextColor = Color.Black;
            m_ComfirmButton.OutLineRectangle = new Rectangle(495, 340, 90, 40);
            m_ComfirmButton.Visible = false;
            m_PowerComfirm = new GDIRectText()
            {
                Text = "\nTC2车蓄电池电压低于100V",
                TextColor = Color.Black,
                NeedDarwOutline = true,
                OutLinePen = new Pen(Color.FromArgb(144, 163, 175), 2f),
                BackColorVisible = true,
                BkColor = Color.Yellow,
                OutLineRectangle = new Rectangle(235, 255, 370, 150),
                DrawFont = FormatStyle.m_Font22B,
                TextFormat = new StringFormat()
                {
                    LineAlignment = StringAlignment.Near,
                    Alignment = StringAlignment.Center
                },
                Visible = false
            };
            m_FirListInbool = new List<int>();
            for (var i = 0; i < 18; i++)
            {
                m_FirListInbool.Add(270 + i);
            }
        }

        private List<int> m_FirListInbool;
        //门状态列表
        private List<DoorState> m_DoorStatesList;
        private bool m_IsPower;
        private DateTime m_ComfirmTime;
        private GDIButton m_ComfirmButton;
        private GDIRectText m_PowerComfirm;
        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::

        /// <summary>
        /// 坐标集
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// 线条框
        /// </summary>
        private RectangleF[] m_LineRectArr;

        /// <summary>
        /// 所有矩形框集合
        /// </summary>
        private List<RectangleF> m_RectsList;

        /// <summary>
        /// 按钮相关框
        /// </summary>
        private RectangleF[] m_BtnRectArr;

        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] m_Images;

        /// <summary>
        /// 键是否按下
        /// </summary>
        private bool[] m_ButtonIsDown;

        /// <summary>
        /// 键是否能按下
        /// </summary>
        private bool[] m_BtnCanDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> m_Rect;
        /// <summary>
        /// 修改按键列表
        /// </summary>
        private Rectangle[] m_Rectangles;
        /// <summary>
        /// bool逻辑号
        /// </summary>
        private List<int> m_BoolIds;
        /// <summary>
        /// float逻辑号
        /// </summary>
        private List<int> m_FoolatIds;
        /// <summary>
        /// 各车门的名字
        /// </summary>
        private readonly string[] m_DoorNameArr =
        {
            "1B", "2B", "3B", "4B",
            "1B", "2B", "3B", "4B",
            "1B", "2B", "3B", "4B",
            "4A", "3A", "2A", "1A",
            "4A", "3A", "2A", "1A",
            "4A", "3A", "2A", "1A",
            "1A", "2A", "3A", "4A",
            "1A", "2A", "3A", "4A",
            "1A", "2A", "3A", "4A",
            "4B", "3B", "2B", "1B",
            "4B", "3B", "2B", "1B",
            "4B", "3B", "2B", "1B",

        };



        #endregion#

        #endregion
    }
}
