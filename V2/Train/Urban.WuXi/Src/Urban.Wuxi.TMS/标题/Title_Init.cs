using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;
using MMI_Message;

namespace KumM_TMS.DMITitle
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public partial class Title : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "TMS上下标题";
        }

        public override int getInBoolParaCount()
        {
            return 6;
        }

        public override int getInFloatParaCount()
        {
            return 5;
        }

        public override int getUIParaCount()
        {
            return 0;
        }

        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //1
        public override string GetTypeName()
        {
            return "Title";
        }

        //4
        public override bool initObject(string nPara)
        {
            return base.initObject(nPara);
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        //7
        public override bool canSetStringList(int nPara)
        {
            if (nPara == 1)
            {
                readTxtID = 1;
                return true;
            }
            else if (nPara == 2)
            {
                readTxtID = 2;
                return true;
            }
            else
            {
                readTxtID = 0;
                return false;
            }
        }

        //8
        public override void addStringByLine(int nIndex, string cStr)
        {
            String[] split = cStr.Split(new char[] { '\t', ' ' });
            int i;
            String[] tmp;
            if (readTxtID == 1)
            {
                tmp = new String[2];
                i = 0;
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
                        _StationList.Add(int.Parse(tmp[0]), tmp[1]);
                        return;
                    }
                }
            }
            else if (readTxtID == 2)
            {
                tmp = new String[5];
                i = 0;
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
                        _AllMsgList.Add(Convert.ToInt32(tmp[0]), tmp);
                        return;
                    }
                }
            }
        }

        public override bool getLists(MMI.Facility.Interface.Data.ParaType listType, out List<int> valueList)
        {
            return base.getLists(listType, out valueList);
        }

        public override bool getLists(MMI.Facility.Interface.Data.ParaType listType, out List<string> valueList)
        {
            return base.getLists(listType, out valueList);
        }

        //3
        public override string GetParaInfo()
        {
            return base.GetParaInfo();
        }

        public override string GetParaInfo(MMI.Facility.Interface.Data.ParaType paraType, int paraIndex)
        {
            return base.GetParaInfo(paraType, paraIndex);
        }

        public override bool initValue(string nParaString, ref int nErrorObjectIndex)
        {
            return base.initValue(nParaString, ref nErrorObjectIndex);
        }

        //5
        public override void rangeObject()
        {
            base.rangeObject();
        }

        public override void setListValue(MMI.Facility.Interface.Data.ParaType listType, int nindex, object objValue)
        {
            base.setListValue(listType, nindex, objValue);
        }

        public override void setValue(int nIndex, bool nValue)
        {
            base.setValue(nIndex, nValue);
        }

        public override void setValue(int nIndex, float nValue)
        {
            base.setValue(nIndex, nValue);
        }

        //9
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Far;
            RightFormat.Alignment = StringAlignment.Center;

            LeftFormat.LineAlignment = StringAlignment.Near;
            LeftFormat.Alignment = StringAlignment.Center;

            bValue = new bool[20];
            oldbValue = new bool[20];
            setBValue = new bool[10];
            setBValueNumb = new int[10];

            fValue = new float[10];
            oldfValue = new float[10];
            setFValue = new float[5];
            setFValueNumb = new int[5];

            pDrawPoint = new PointF[20];

            rects = new RectangleF[60];

            Img = new Image[20];

            buttonIsDown = new bool[10] { false, true, false, false, false, false, false, false, false, false };

            _AllMsgList = new SortedDictionary<int, string[]>();

            if (this.UIObj.InBoolList.Count > 1)
            {
                int len = this.UIObj.InBoolList.Count;
                msgBeginAdress = this.UIObj.InBoolList[len - 2];
                msgLen = this.UIObj.InBoolList[len - 1] - this.UIObj.InBoolList[len - 2];
            }

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(_recPath + "\\" + UIObj.ParaList[i]);
            }

            _StationList = new SortedDictionary<int, string>();

            #region :::::::::::::::::::::::::::: rects ::::::::::::::::::::::::::::
            //外框
            rects[0] = new Rectangle(0, 0, 800, 600);
            //底层按键
            rects[1] = new Rectangle(0, 550, 800, 50);
            //编组
            rects[2] = new Rectangle(0, 0, 60, 30);
            //V
            rects[3] = new Rectangle(540, 63, 30, 30);
            //km/h
            rects[4] = new Rectangle(670, 63, 60, 30);
            //编组
            rects[5] = new Rectangle(50, 0, 80, 30);
            //日期
            rects[6] = new Rectangle(5, 30, 130, 30);
            //时间
            rects[7] = new Rectangle(5, 63, 130, 30);
            //当前站
            rects[8] = new Rectangle(140, 30, 130, 30);
            //当前站站名
            rects[9] = new Rectangle(140, 63, 130, 30);
            //终点站
            rects[10] = new Rectangle(275, 30, 130, 30);
            //终点站站名
            rects[11] = new Rectangle(275, 63, 130, 30);
            //网压
            rects[12] = new Rectangle(410, 30, 130, 63);
            //速度
            rects[13] = new Rectangle(560, 30, 120, 63);
            //标题
            rects[14] = new Rectangle(0, 0, 800, 30);

            //
            rects[20] = new Rectangle(20, 561, 115, 33);
            for (int i = 0; i < 7; i++)
            {
                rects[21 + i] = new Rectangle(115 + i * 97, 555, 90, 40);
            }

            //
            for (int i = 0; i < 6; i++)
            {
                rects[30 + i] = new Rectangle(125 + i * 95, 100, 95, 60);
            }

            //img
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[36 + i * 3 + j] = new Rectangle(5 + j * 135, 30 + i * 33, 130, 30);
                }
            }
            rects[42] = new Rectangle(410, 30, 130, 63);
            rects[43] = new Rectangle(570, 30, 105, 63);
            rects[44] = new Rectangle(730, 30, 63, 63);
            rects[45] = new Rectangle(741, 41, Img[0].Size.Width, Img[0].Size.Height);
            rects[46] = new Rectangle(14, 557, Img[5].Size.Width, Img[5].Size.Height);
            rects[47] = new Rectangle(125, 100, Img[9].Size.Width, Img[9].Size.Height);
            rects[48] = new Rectangle(80, 100, Img[11].Size.Width, Img[11].Size.Height);
            rects[49] = new Rectangle(705, 100, Img[11].Size.Width, Img[11].Size.Height);
            rects[50] = new Rectangle(90, 112, Img[13].Size.Width, Img[13].Size.Height);
            rects[51] = new Rectangle(715, 112, Img[13].Size.Width, Img[13].Size.Height);

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::
            pDrawPoint[0] = new Point(0, 94);
            pDrawPoint[1] = new Point(800, 94);
            pDrawPoint[2] = new Point(0, 550);
            pDrawPoint[3] = new Point(800, 550);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::: Rect :::::::::::::::::::::::::::::
            for (int i = 0; i < 8; i++)
            {
                Rect.Add(new Region(rects[20 + i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        #region ::::::::::::::::::: 车站信息 :::::::::::::::::::::::::
        /// <summary>
        /// 车站列表
        /// </summary>
        public static SortedDictionary<int, String> _StationList;

        /// <summary>
        /// 终点站
        /// </summary>
        internal String endStation = "";

        /// <summary>
        /// 下一站
        /// </summary>
        internal String nextStation = "";
        #endregion

        #region ::::::::::::::::::: 故障信息 :::::::::::::::::::::::::
        /// <summary>
        /// 读文本信息的编号
        /// </summary>
        private int readTxtID;

        /// <summary>
        /// 读文本
        /// 消息列表
        /// </summary>
        private SortedDictionary<int, string[]> _AllMsgList;

        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        private static MessageDis _msgInf = new MessageDis();
        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        public static MessageDis MsgInf { get { return _msgInf; } }

        /// <summary>
        /// 收到新消息
        /// </summary>
        private bool _receiveNewMsg;

        /// <summary>
        /// 故障开始地址
        /// </summary>
        private int msgBeginAdress;

        /// <summary>
        /// 故障长度个数
        /// </summary>
        private int msgLen;
        #endregion

        #region ::::::::::::::::::: 标题信息 :::::::::::::::::::::::::
        /// <summary>
        /// 根据当前视图判断是否有帮助页面
        /// </summary>
        bool isExistHelp = true;

        /// <summary>
        /// 标题名称
        /// </summary>
        internal String titleName = "";

        /// <summary>
        /// 是否要画列车
        /// </summary>
        internal bool isShowCar = false;

        /// <summary>
        /// 是否要画方向
        /// </summary>
        internal bool isShowDirection = false;

        /// <summary>
        /// 当前视图
        /// </summary>
        internal int currentView = -1;
        #endregion

        #region::::::::::::::::::: 传值部分 :::::::::::::::::::::::::
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        internal bool[] bValue;

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        internal bool[] oldbValue;

        /// <summary>
        /// 发送的数字量
        /// </summary>
        internal bool[] setBValue;

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        internal int[] setBValueNumb;

        /// <summary>
        /// 接收模拟量
        /// </summary>
        internal float[] fValue;

        /// <summary>
        /// 前一个周期接收的模拟量
        /// </summary>
        internal float[] oldfValue;

        /// <summary>
        /// 发送的模拟量
        /// </summary>
        internal float[] setFValue;

        /// <summary>
        /// 发送的模拟量在floatlist中的序号
        /// </summary>
        internal int[] setFValueNumb;

        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }
}
