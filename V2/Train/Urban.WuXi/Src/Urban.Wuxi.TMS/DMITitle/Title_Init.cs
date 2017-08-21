using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MsgReceiveMod;

namespace Urban.Wuxi.TMS.DMITitle
{
    [GksDataType(DataType.isMMIObjectClass)]
    public partial class Title : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "TMS上下标题";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            LoadStationInfo();

            LoadFaultInfo();

            InitEditModel();
            return true;
        }

        private void LoadFaultInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "故障信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                String[] split = cStr.Split(new char[] { '\t' });
                var tmp = new String[5];
                var i = 0;
                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        if (i < 5)
                            tmp[i] = s;
                        i++;
                    }
                    if (i == 5)
                    {
                        AllMsgList.Add(Convert.ToInt32(tmp[0]), tmp);

                    }
                }
            }
        }

        private void LoadStationInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "车站信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                String[] split = cStr.Split(new char[] { '\t' });
                var tmp = new String[2];
                var i = 0;
                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        if (i < 2)
                            tmp[i] = s;
                        i++;
                    }
                    if (i == 2)
                    {
                        m_StationList.Add(int.Parse(tmp[0]), tmp[1]);

                    }
                }
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            m_DrawFormat.LineAlignment = (StringAlignment)1;
            m_DrawFormat.Alignment = (StringAlignment)1;

            m_RightFormat.LineAlignment = (StringAlignment)2;
            m_RightFormat.Alignment = (StringAlignment)1;

            m_LeftFormat.LineAlignment = (StringAlignment)0;
            m_LeftFormat.Alignment = (StringAlignment)1;

            m_BisValue = new bool[20];
            m_SetBisValue = new bool[10];
            m_SetValueNumb = new int[10];

            m_FValue = new float[10];
            m_SetFValue = new float[5];
            m_SetFValueNumb = new int[5];

            m_PDrawPoint = new PointF[20];

            m_Rects = new RectangleF[80];

            m_Images = new Image[25];
            m_BoolIds = new List<int>();

            m_ButtonIsDown = new bool[10] { false, true, false, false, false, false, false, false, false, false };

            if (UIObj.InBoolList.Count > 1)
            {
                int len = UIObj.InBoolList.Count;
            }

            m_Regions = new List<Region>();

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

            //_StationList = new SortedDictionary<int, string>();

            #region :::::::::::::::::::::::::::: _rects ::::::::::::::::::::::::::::
            //外框
            m_Rects[0] = new Rectangle(0, 0, 800, 600);
            //底层按键
            m_Rects[1] = new Rectangle(0, 550, 800, 50);
            //编组
            m_Rects[2] = new Rectangle(0, 0, 60, 30);
            //V
            m_Rects[3] = new Rectangle(540, 63, 30, 30);
            //km/h
            m_Rects[4] = new Rectangle(670, 63, 60, 30);
            //编组
            m_Rects[5] = new Rectangle(50, 0, 80, 30);
            //日期
            m_Rects[6] = new Rectangle(5, 30, 130, 30);
            //时间
            m_Rects[7] = new Rectangle(5, 63, 130, 30);
            //当前站
            m_Rects[8] = new Rectangle(140, 30, 130, 30);
            //当前站站名
            m_Rects[9] = new Rectangle(140, 63, 130, 30);
            //终点站
            m_Rects[10] = new Rectangle(275, 30, 130, 30);
            //终点站站名
            m_Rects[11] = new Rectangle(275, 63, 130, 30);
            //网压
            m_Rects[12] = new Rectangle(410, 30, 130, 63);
            //速度
            m_Rects[13] = new Rectangle(560, 30, 120, 63);
            //标题
            m_Rects[14] = new Rectangle(0, 0, 800, 30);
            //广播模式........
            //rect1[0]=new RectangleF(660,10,100,20);
            //
            m_Rects[20] = new Rectangle(20, 561, 115, 33);
            for (int i = 0; i < 7; i++)
            {
                m_Rects[21 + i] = new Rectangle(115 + i * 97, 555, 90, 40);
            }

            //
            for (int i = 0; i < 6; i++)
            {
                m_Rects[30 + i] = new Rectangle(125 + i * 95, 100, 95, 60);
            }

            //img
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_Rects[36 + i * 3 + j] = new Rectangle(5 + j * 135, 30 + i * 33, 130, 30);
                }
            }
            m_Rects[42] = new Rectangle(410, 30, 130, 63);
            m_Rects[43] = new Rectangle(570, 30, 105, 63);
            m_Rects[44] = new Rectangle(730, 30, 63, 63);
            m_Rects[45] = new Rectangle(741, 41, m_Images[0].Size.Width, m_Images[0].Size.Height);
            m_Rects[46] = new Rectangle(14, 552, m_Images[5].Size.Width, m_Images[5].Size.Height - 5);
            m_Rects[47] = new Rectangle(125, 100, m_Images[9].Size.Width, m_Images[9].Size.Height);
            m_Rects[48] = new Rectangle(80, 100, m_Images[11].Size.Width, m_Images[11].Size.Height);
            m_Rects[49] = new Rectangle(705, 100, m_Images[11].Size.Width, m_Images[11].Size.Height);
            m_Rects[50] = new Rectangle(90, 112, m_Images[13].Size.Width, m_Images[13].Size.Height);
            m_Rects[51] = new Rectangle(715, 112, m_Images[13].Size.Width, m_Images[13].Size.Height);

            //运行界面的车
            m_Rects[52] = new RectangleF(90, 110, 640, 80);
            for (int i = 0; i < 2; i++)
            {
                m_Rects[53 + i] = new RectangleF(2 + 754 * i, 155, 37, 255);
                m_Rects[55 + i] = new RectangleF(5 + 754 * i, 235, 30, 100);
            }
            for (int i = 0; i < 6; i++)
            {
                m_Rects[57 + i] = new RectangleF(125 + i * 95, 140, 95, 20);
                m_Rects[64 + i] = new RectangleF(175 + i * 95, 100, 95, 60);
                m_Rects[71 + i] = new RectangleF(115 + i * 95, 100, 95, 60);
            }

            //空调界面的车
            m_Rects[63] = new Rectangle(175, 100, m_Images[9].Size.Width, m_Images[9].Size.Height);

            //通讯界面的车
            m_Rects[70] = new RectangleF(115, 100, m_Images[9].Size.Width, m_Images[9].Size.Height);
            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::
            m_PDrawPoint[0] = new Point(0, 94);
            m_PDrawPoint[1] = new Point(800, 94);
            m_PDrawPoint[2] = new Point(0, 550);
            m_PDrawPoint[3] = new Point(800, 550);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::: _regions 按键列表:::::::::::::::::::::::::::::
            for (int i = 0; i < 8; i++)
            {
                m_Regions.Add(new Region(m_Rects[20 + i]));
            }
            m_Regions.Add(new Region(m_Rects[45]));
            #endregion
            m_Rects[77] = new RectangleF(m_Rects[44].X + 2, m_Rects[44].Y + 2, m_Rects[44].Width - 4, m_Rects[44].Height - 4);
            m_MsgInfList = new MsgHandlerFault0<FaultMsgOrdinary>();
        }
        /// <summary>
        /// jiaz
        /// </summary>
        private void InitEditModel()
        {
            append_postCmd(CmdType.SetInBoolValue, 72, 1, 0);
            //for (int i = 0; i < 4500; i++)
            //{
            //    append_postCmd(CmdType.SetInBoolValue, 222 + i, 1, 0);
            //}
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();
        private readonly string NextStation_ = null;
        /// <summary>
        /// 工况区颜色
        /// </summary>
        public static int[] m_RectColor = { 0, 0, 0, 0, 0 };

        #region ::::::::::::::::::: 车站信息 :::::::::::::::::::::::::
        /// <summary>
        /// 车站列表
        /// </summary>
        public static SortedDictionary<int, String> m_StationList = new SortedDictionary<int, String>();

        /// <summary>
        /// 终点站
        /// </summary>
        private String m_EndStation = "";

        /// <summary>
        /// 下一站
        /// </summary>
        private String m_NextStation = "";
        #endregion

        #region ::::::::::::::::::: 故障信息 :::::::::::::::::::::::::
        /// <summary>
        /// 读文本信息的编号
        /// </summary>
        private int m_ReadTxtID;

        /// <summary>
        /// 读文本
        /// 消息列表
        /// </summary>
        private static readonly SortedDictionary<int, string[]> AllMsgList = new SortedDictionary<int, string[]>();

        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        private static MsgHandlerFault0<FaultMsgOrdinary> m_MsgInfList;

        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        public static MsgHandlerFault0<FaultMsgOrdinary> MsgInfList { get { return m_MsgInfList; } }

        private readonly List<int> m_FaultLogicIDList = new List<int>();

        /// <summary>
        /// 收到新消息
        /// </summary>
        private bool m_ReceiveNewMsg;

        #endregion

        #region ::::::::::::::::::: 标题信息 :::::::::::::::::::::::::
        /// <summary>
        /// 根据当前视图判断是否有帮助页面
        /// </summary>
        private bool m_IsExistHelp = true;

        /// <summary>
        /// 标题名称
        /// </summary>
        private String m_TitleName = string.Empty;

        /// <summary>
        /// 是否要画列车
        /// </summary>
        private bool m_IsShowCar = false;

        /// <summary>
        /// 是否要画方向
        /// </summary>
        private bool m_IsShowDirection = false;

        /// <summary>
        /// 当前视图
        /// </summary>
        private int m_CurrentView = -1;
        #endregion

        #region::::::::::::::::::: 传值部分 :::::::::::::::::::::::::
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        private bool[] m_BisValue;

        /// <summary>
        /// 发送的数字量
        /// </summary>
        private bool[] m_SetBisValue;

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        /// 
        /// 
        private int[] m_SetValueNumb;

        /// <summary>
        /// 接收模拟量
        /// </summary>
        private float[] m_FValue;

        /// <summary>
        /// 发送的模拟量
        /// </summary>
        private float[] m_SetFValue;

        /// <summary>
        /// 发送的模拟量在floatlist中的序号
        /// </summary>
        private int[] m_SetFValueNumb;

        /// <summary>
        /// 坐标集
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] m_Images;

        /// <summary>
        /// 键是否按下
        /// </summary>
        private bool[] m_ButtonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> m_Regions;
        /// <summary>
        /// bool逻辑号
        /// </summary>
        private List<int> m_BoolIds;
        #endregion#
        #endregion
    }
}
