using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using HXD1D.控制设置;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Data;
using MsgReceiveMod;

namespace HXD1D.Titlte
{
    /// <summary>
    /// Title界面 用于初始化，和画图
    /// </summary>
    public partial class Title : baseClass
    {
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> Imgs;

        /// <summary>
        /// 标题界面列表
        /// </summary>
        private List<Rectangle> Rectangles;
        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> Rects;

        /// <summary>
        /// 标题名字
        /// </summary>
        public static String TitleName = String.Empty;
        /// <summary>
        /// 按钮的名字
        /// </summary>
        public static String ButtonName = "信息";
        /// <summary>
        /// 当前视图
        /// </summary>
        public static int CurentView = -1;

        /// <summary>
        /// 键是否按下
        /// </summary>
        public static bool[] buttonIsDown;
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
        /// 
        /// 
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
        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> BoolIds;
        //<summary>
        //float逻辑号
        // </summary>
        private List<int> FoolatIds;
        private int[] _ids;
        public static int Current = -1;

        /// <summary>
        /// 按键传值列表
        /// </summary>
        public static List<int> _ints;
        private static bool[] _btnState = new bool[26];
        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        private static MsgHandlerFault0<Faul> _msgInfList;

        private List<int> _faultLogicIDList = new List<int>();
        /// <summary>
        ///视图19的输入内容 
        /// </summary>
        public static Dictionary<int, int> ContentDictionary = new Dictionary<int, int>();
        /// <summary>
        /// 存密码的字典
        /// </summary>
        public static Dictionary<int, string> PassWordDictionary = new Dictionary<int, string>();
        /// <summary>
        /// 创建CursorMovecs的列表对象
        /// </summary>
        public static List<CursorMovecs> BtnList = new List<CursorMovecs>();
        /// <summary>
        /// 视图22的输入内容
        /// </summary>
        public static List<int> ContentLists = new List<int>();

        /// <summary>
        /// 所有发生过的消息
        /// </summary>
        public static MsgHandlerFault0<Faul> MsgInfList { get { return _msgInfList; } }

        // ReSharper disable once InconsistentNaming
        private static readonly List<int> _btnDownList = new List<int>();
        /// <summary>
        /// 按钮按下列表
        /// </summary>
        public static List<int> BtnDownList { get { return GetBtnDownOrUpList(_btnDownList); } }

        // ReSharper disable once InconsistentNaming
        private static readonly List<int> _btnUpList = new List<int>();
        /// <summary>
        /// 按钮弹起列表
        /// </summary>
        public static List<int> BtnUpList { get { return GetBtnDownOrUpList(_btnUpList); } }

        static List<int> tmp = new List<int>();

        #region ::::::::::::::::::: 故障信息 :::::::::::::::::::::::::
        /// <summary>
        /// 读文本信息的编号
        /// </summary>
        private int readTxtID;

        /// <summary>
        /// 读文本
        /// 消息列表
        /// </summary>
        private SortedDictionary<int, string[]> _allMsgList = new SortedDictionary<int, string[]>();

        #endregion

        #region ::::::::::::::::::: 标题信息 :::::::::::::::::::::::::
        /// <summary>
        /// 根据当前视图判断是否有帮助页面
        /// </summary>
        private bool _isExistHelp = true;

        /// <summary>
        /// 标题名称
        /// </summary>
        private String _titleName = string.Empty;

        /// <summary>
        /// 是否要画列车
        /// </summary>
        private bool _isShowCar = false;

        /// <summary>
        /// 是否要画方向
        /// </summary>
        private bool _isShowDirection = false;

        /// <summary>
        /// 当前视图
        /// </summary>
        private int _currentView = -1;

        /// <summary>
        /// 故障编号
        /// </summary>
        public static int Rowid;
        /// <summary>
        /// 故障等级
        /// </summary>
        public static string level;
        /// <summary>
        /// 故障内容
        /// </summary>
        public static string Content;
        /// <summary>
        /// 故障提示
        /// </summary>
        public static string Tishi;


        #endregion


        private static List<int> GetBtnDownOrUpList(List<int> btnList)
        {
            tmp.Clear();
            foreach (int temp in btnList)
            {
                tmp.Add(temp % 26);
            }
            return tmp;
        }



        public override string GetInfo()
        {
            return "标题";
        }
        public override bool init(ref int nErrorObjectIndex)
        {

            InitData();
            return true;
        }

        public static int OpenCloseCount = 0;//主断分合次数

        private bool _isOpened = false;
        private bool _isClosed = false;

        private bool IsOpen
        {
            set
            {
                if (_isOpen == value) return;
                _isOpen = value;

                if (value)//分
                {
                    _isOpened = true;
                    if (_isClosed)
                    {
                        OpenCloseCount++;
                        _isOpened = false;
                        _isClosed = false;
                    }
                }
            }
        }
        private bool _isOpen = false;

        private int _openFlag = 0;
        private int _closeFlag = 0;

        public bool IsClose1
        {
            set
            {
                if (_isClose1 == value) return;
                _isClose1 = value;

                if (value)//分
                {
                    _isClosed = true;
                    if (_isOpened)
                    {
                        OpenCloseCount++;
                        _isOpened = false;
                        _isClosed = false;
                    }
                }
            }
        }
        private bool _isClose1 = false;

        public bool IsClose2
        {
            set
            {
                if (_isClose2 == value) return;
                _isClose2 = value;

                if (value)//分
                {
                    _isClosed = true;
                    if (_isOpened)
                    {
                        OpenCloseCount++;
                        _isOpened = false;
                        _isClosed = false;
                    }
                }
            }
        }
        private bool _isClose2 = false;

        public override void paint(Graphics dcGs)
        {
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[3], 0, ControlSeting.DisplayValue);

            if (BoolList[1100])
            {
                append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 54, 0, 0);
            }

            base.paint(dcGs);
            Draw(dcGs);
            RecAndDispMsg();
            MsgSort();
            UpdateButtonState();
            paint_Connect(dcGs);

            IsOpen = BoolList[826];
            IsClose1 = BoolList[828];
            IsClose2 = BoolList[829];
        }

        public Boolean IsConnect
        {
            set
            {
                if (_isConnect == value) return;
                _isConnect = value;

                if (CurentView == 1)
                {
                    buttonIsDown[6] = false;
                    buttonIsDown[8] = false;
                }
            }
        }

        private Boolean _isConnect = false;
        /// <summary>
        /// 绘制联挂按钮（唐林20150803）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Connect(Graphics dcGs)
        {
            if (CurentView == 1)
            {
                bool isConnect = false;
                for (int i = 0; i < UIObj.InBoolList[6 + 1]; i++)
                {
                    if (BoolList[UIObj.InBoolList[6] + i])
                    {
                        isConnect = true;
                        break;
                    }
                }
                IsConnect = isConnect;
                if (isConnect)
                {
                    dcGs.DrawString("联挂", FormatStyle.Font12,
                        buttonIsDown[8] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[13], FormatStyle.MFormat);

                    dcGs.DrawString("调车\n激活", FormatStyle.Font12,
                        buttonIsDown[6] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[11], FormatStyle.MFormat);
                }
            }
        }

        /// <summary>
        /// 画矩形框
        /// </summary>
        /// <param name="e"></param>
        private void Drawon(Graphics e)
        {
            // title上行标题： 界面标题，车间，时间显示

            e.DrawRectangle(FormatStyle.WhitePen, Rectangles[0]);
            e.DrawRectangle(FormatStyle.WhitePen, Rectangles[1]);
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, Rectangles[2 + i]);

            } //title下行标题    
            e.DrawRectangle(FormatStyle.WhitePen, Rectangles[4]);
            for (int i = 0; i < 10; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, Rectangles[5 + i]);
            }

        }
        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="e">画图对象</param>

        private void DrawCharacter(Graphics e)
        {
            if (CurentView == 19 || CurentView == 24 || CurentView == 20 || CurentView == 21 || CurentView == 22 || CurentView == 23 || CurentView == 39 || CurentView == 46 || CurentView == 54 || CurentView == 55)
            {
            }
            else
            {
                e.DrawString(FormatStyle.str1[6], FormatStyle.Font12, buttonIsDown[9] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[14], FormatStyle.MFormat);
            }
            if (CurentView == 24)
            {
                e.DrawString(FormatStyle.str1[2], FormatStyle.Font12, buttonIsDown[9] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[14], FormatStyle.MFormat);
            }

            if (CurentView == 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    e.DrawString(FormatStyle.str1[i], FormatStyle.Font12, buttonIsDown[i] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[5 + i], FormatStyle.MFormat);

                }
                e.DrawString(FormatStyle.str1[6], FormatStyle.Font12, buttonIsDown[9] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[14], FormatStyle.MFormat);
            }

            else if (CurentView == 2 || CurentView == 8 || CurentView == 9 || CurentView == 10 || CurentView == 11 ||
                CurentView == 12 || CurentView == 13)
            {
                for (int i = 0; i < 6; i++)
                {
                    e.DrawString(FormatStyle.str4[i], FormatStyle.Font12, buttonIsDown[i] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[5 + i], FormatStyle.MFormat);
                }
                e.DrawString(FormatStyle.str4[6], FormatStyle.Font12, buttonIsDown[8] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[13], FormatStyle.MFormat);
                e.DrawString(FormatStyle.str4[7], FormatStyle.Font12, buttonIsDown[9] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                     Rectangles[14], FormatStyle.MFormat);

            }
            else if (CurentView == 7 || CurentView == 47 || CurentView == 48 || CurentView == 49 || CurentView == 50 || CurentView == 51 || CurentView == 52)
            {
                e.DrawString("版本\n信息", FormatStyle.Font12, buttonIsDown[0] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[5], FormatStyle.MFormat);
                e.DrawString("空压机\n模式", FormatStyle.Font12, buttonIsDown[3] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[8], FormatStyle.MFormat);
                e.DrawString("强供\n110V", FormatStyle.Font12, buttonIsDown[4] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[9], FormatStyle.MFormat);
                e.DrawString("断电\n模式", FormatStyle.Font12, buttonIsDown[5] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[10], FormatStyle.MFormat);
                e.DrawString("检修\n设置", FormatStyle.Font12, buttonIsDown[7] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[12], FormatStyle.MFormat);
                e.DrawString("I/O\n信息", FormatStyle.Font12, buttonIsDown[8] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[13], FormatStyle.MFormat);
            }
            else if (CurentView == 47)
            {
                e.DrawString("版本\n信息", FormatStyle.Font12, buttonIsDown[0] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[5], FormatStyle.MFormat);
            }
            else if (CurentView == 53)
            {
                e.DrawString("切除", FormatStyle.Font12, buttonIsDown[0] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[5], FormatStyle.MFormat);
                e.DrawString("投入", FormatStyle.Font12, buttonIsDown[1] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                       Rectangles[6], FormatStyle.MFormat);
            }
            else if (CurentView == 15)//故障界面底部框的文字
            {
                e.DrawString("当前\n故障", FormatStyle.Font12,
                    buttonIsDown[6] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[11], FormatStyle.MFormat);
                e.DrawString("历史\n故障", FormatStyle.Font12,
                    buttonIsDown[8] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[13], FormatStyle.MFormat);
            }
            else if (CurentView == 16)
            {
                for (int i = 0; i < 9; i++)
                {
                    e.DrawString(FormatStyle.str12[i], FormatStyle.Font12,
                        buttonIsDown[i] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[5 + i], FormatStyle.MFormat);
                }
            }
            //
            else if (CurentView == 17)
            {
                e.DrawString("冷却\n系统", FormatStyle.Font12,
                    buttonIsDown[0] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[5], FormatStyle.MFormat);
                e.DrawString(FormatStyle.str1[6], FormatStyle.Font12,
                    buttonIsDown[7] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[14], FormatStyle.MFormat);

            }
            else if (CurentView == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    e.DrawString(FormatStyle.str18[i], FormatStyle.Font12,
                        buttonIsDown[0 + i] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[5 + i], FormatStyle.MFormat);
                    e.DrawString(FormatStyle.str1[6], FormatStyle.Font12,
                        buttonIsDown[9] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[14], FormatStyle.MFormat);
                }
                e.DrawString("远程\n切除", FormatStyle.Font12,
                    buttonIsDown[0 + 4] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[5 + 4], FormatStyle.MFormat);
            }
            else if (CurentView == 4)
            {
                e.DrawString("分相\n设置", FormatStyle.Font12, FormatStyle.WhiteBrush, Rectangles[5],
                    FormatStyle.MFormat);
                e.DrawString("高隔\n设置", FormatStyle.Font12, FormatStyle.WhiteBrush, Rectangles[7],
                    FormatStyle.MFormat);
                e.DrawString("手动\n校时", FormatStyle.Font12, FormatStyle.WhiteBrush, Rectangles[13],
                    FormatStyle.MFormat);
                e.DrawString(FormatStyle.str1[6], FormatStyle.Font12,
                    buttonIsDown[9] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[14], FormatStyle.MFormat);

            }
            else if (CurentView == 5 || CurentView == 26 || CurentView == 27 || CurentView == 28)
            {
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FormatStyle.str24[i], FormatStyle.Font12,
                        buttonIsDown[i + 6] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[11 + i], FormatStyle.MFormat);

                }
            }
            else if (CurentView == 24)
            {
                if (BoolList[BoolIds[27]])
                {
                    e.DrawString("RDC1\nOFF", FormatStyle.Font12, FormatStyle.WhiteBrush, Rectangles[9],
                        FormatStyle.MFormat);
                }
                else if (BoolList[BoolIds[28]])
                {
                    e.DrawString("RDC1\nON", FormatStyle.Font12, FormatStyle.WhiteBrush, Rectangles[9],
                        FormatStyle.MFormat);
                }

                if (BoolList[BoolIds[29]])
                {
                    e.DrawString("RDC2\nOFF", FormatStyle.Font12, FormatStyle.WhiteBrush, Rectangles[11],
                        FormatStyle.MFormat);
                }
                else if (BoolList[BoolIds[30]])
                {
                    e.DrawString("RDC2\nON", FormatStyle.Font12, FormatStyle.WhiteBrush, Rectangles[11],
                        FormatStyle.MFormat);
                }

            }
            else if (CurentView == 6 || CurentView == 29 || CurentView == 30 || CurentView == 31 || CurentView == 32 ||
                     CurentView == 45)
            {
                for (int i = 0; i < 5; i++)
                {
                    e.DrawString(FormatStyle.str25[i], FormatStyle.Font12,
                        buttonIsDown[0 + i] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[5 + i], FormatStyle.MFormat);
                }
            }

            else if (CurentView == 19 || CurentView == 20 || CurentView == 21 || CurentView == 22 ||
                     CurentView == 23 || CurentView == 39 || CurentView == 46)
            {
                for (int i = 0; i < 9; i++)
                {
                    e.DrawString((i + 1).ToString(), FormatStyle.Font12,
                        buttonIsDown[i] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[5 + i], FormatStyle.MFormat);
                }
                e.DrawString(0.ToString(), FormatStyle.Font12,
                    buttonIsDown[9] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[14], FormatStyle.MFormat);
            }
            else if (CurentView == 33)
            {
                for (int i = 0; i < 6; i++)
                {
                    e.DrawString(FormatStyle.str29[i], FormatStyle.Font12,
                        buttonIsDown[i] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                        Rectangles[5 + i], FormatStyle.MFormat);
                }
                e.DrawString(FormatStyle.str29[6], FormatStyle.Font12,
                    buttonIsDown[9] ? FormatStyle.BlackBrush : FormatStyle.WhiteBrush,
                    Rectangles[14], FormatStyle.MFormat);
            }


            //时间
            e.DrawString(DateTime.Now.ToString("yyyy-MM-dd"), FormatStyle.Font12B,
                FormatStyle.WhiteBrush, Rectangles[2], FormatStyle.MFormat);
            e.DrawString(DateTime.Now.ToLongTimeString(), FormatStyle.Font12B,
                FormatStyle.WhiteBrush, Rectangles[3], FormatStyle.MFormat);
            //机车编号
            e.DrawString("机车编号:", FormatStyle.Font12, FormatStyle.WhiteBrush, 325, 10);
            //标题
            e.DrawString(TitleName, FormatStyle.Font12, FormatStyle.WhiteBrush, Rectangles[1].X + 60, Rectangles[1].Y + 8);
        }
        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawImg(Graphics e)
        {
            e.DrawImage(Imgs[0], Rectangles[4].X + 2, Rectangles[4].Y + 2, Rectangles[4].Width - 2, Rectangles[4].Height - 2);
            for (int i = 0; i < 10; i++)
            {
                if (buttonIsDown[i])
                {
                    e.DrawImage(Imgs[1], Rectangles[i + 5]);
                }

            }
        }
        /// <summary>
        /// 调用各画图函数
        /// </summary>
        /// <param name="e">画图对象</param>

        private void Draw(Graphics e)
        {
            DrawImg(e);
            DrawCharacter(e);
            Drawon(e);

        }

        private void RunView()
        {
            if (!BoolList[BoolIds[26]])
            {
                append_postCmd(CmdType.ChangePage, 42, 0, 0);
            }
        }

        /// <summary>
        /// 发送隔离开关状态信息
        /// </summary>
        /// <param name="index">按下的按钮</param>
        /// <param name="flg">发送状态 true为1 false 为0</param>
        private void SendDisconnectorStatus(int index, bool flg)
        {
            switch (index)
            {
                case 4:
                    if (BoolList[BoolIds[27]])
                    {
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], flg ? 1 : 0, 0);
                    }
                    else if (BoolList[BoolIds[28]])
                    {
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4], flg ? 1 : 0, 0);
                    }
                    break;
                case 6:
                    if (BoolList[BoolIds[29]])
                    {
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3], flg ? 1 : 0, 0);
                    }
                    else if (BoolList[BoolIds[30]])
                    {
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[5], flg ? 1 : 0, 0);
                    }
                    break;
            }
        }

        private int _btnDownID = -1;
        /// <summary>
        /// 实时更新按钮的状态
        /// 有2套按钮方案，所以有26个按钮
        /// </summary>
        private void UpdateButtonState()
        {
            for (int index = 0; index < 26; index++)
            {
                lock (_btnDownList)
                {
                    lock (_btnUpList)
                    {
                        if (BoolList[BoolIds[index]]) //判断按钮按下
                        {
                            //if (!_btnDownList.Contains(index))
                            //{
                            //_btnState[index%26] = true;
                            //if (_btnDownList.Count == 0) //按键列表为空
                            //{
                            //    _btnDownList.Add(index);
                            //}
                            //else if (_btnDownList[0] != index) //按键列表已经有值并且前后不同
                            //{
                            //    _btnUpList.Clear(); //上一个按钮弹起状态存到弹起按钮列表
                            //    _btnUpList.Add(_btnDownList[0]);
                            //    ChangePage(_btnUpList[0]%26);

                            //    _btnDownList.Clear(); //清空一遍再把后收到的加入
                            //    _btnDownList.Add(index);
                            //}
                            //ChangePage(index % 26);
                            _btnDownID = index;
                            if (CurentView == 24)
                            {
                                buttonIsDown[index] = true;
                                Boolean isBowUp = false;
                                for (int i = 0; i < UIObj.InBoolList[8 + 1]; i++)
                                {
                                    if (BoolList[UIObj.InBoolList[8] + i])
                                    {
                                        isBowUp = true;
                                        break;
                                    }
                                }

                                if (!isBowUp)
                                    SendDisconnectorStatus(index % 26, true);
                            }

                            if (CurentView == 1)
                            {
                                if (index == 8)
                                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[6], 1, 0);
                            }
                            if (CurentView == 1 || CurentView == 8)
                            {
                                if (BoolList[UIObj.InBoolList[10]])
                                {
                                    if (index == 11)
                                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[13], 1, 0);
                                    if (index == 12)
                                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[14], 1, 0);
                                }
                            }

                            //if (CurentView == 19 || CurentView == 20 || CurentView == 21 || CurentView == 22)
                            //{
                            buttonIsDown[index] = true;
                            //}
                            //}
                            //_btnUpList.Clear();
                        }
                        else //判断按钮弹起
                        {
                            //if (!_btnUpList.Contains(index))
                            //{
                            //_btnState[index%26] = false;
                            ////弹起后下一个周期就把弹起列表清空
                            //if (_btnUpList.Count != 0 && _btnUpList[0] == index)
                            //{
                            //    _btnUpList.Clear();
                            //    break;
                            //}
                            ////按钮按下和弹起转换瞬间
                            //if (_btnDownList.Count == 0 || _btnDownList[0] != index) continue;
                            //_btnUpList.Clear();
                            //_btnUpList.Add(_btnDownList[0]);
                            //ChangePage(_btnUpList[0]%26);
                            if (index != _btnDownID) continue;
                            _btnDownID = -1;
                            ChangePage(index % 26);
                            if (CurentView == 24)
                            {
                                buttonIsDown[index] = false;
                                Boolean isBowUp = false;
                                for (int i = 0; i < UIObj.InBoolList[8 + 1]; i++)
                                {
                                    if (BoolList[UIObj.InBoolList[8] + i])
                                    {
                                        isBowUp = true;
                                        break;
                                    }
                                }

                                if (!isBowUp)
                                    SendDisconnectorStatus(index % 26, false);
                            }

                            if (CurentView == 1)
                            {
                                if (index == 8)
                                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[6], 0, 0);
                            }
                            if (CurentView == 1 || CurentView == 8)
                            {
                                if (BoolList[UIObj.InBoolList[10]])
                                {
                                    if (index == 11)
                                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[13], 0, 0);
                                    if (index == 12)
                                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[14], 0, 0);
                                }
                            }
                            _btnDownList.Clear(); //清空按下列表
                            if (CurentView == 19 || CurentView == 20 || CurentView == 21 || CurentView == 22 || CurentView == 46 || CurentView == 53)
                            {
                                buttonIsDown[index] = false;
                            }
                            break;
                        }
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            ReadFaultFile();
            Rectangles = new List<Rectangle>();
            Imgs = new List<Image>();
            Rects = new List<Region>();
            BoolIds = new List<int>();
            FoolatIds = new List<int>();
            buttonIsDown = new bool[25] { false, false, false, false, false, false, false, false, false, true, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false };
            _ids = new[] { 800, 801, 802, 803, 804, 805, 806, 807, 808, 809 };
            _msgInfList = new MsgHandlerFault0<Faul>();
            _faultLogicIDList = new List<int>();
            _ints = new List<int>();
            Rectangles.Add(new Rectangle(0, 0, 44, 34));
            Rectangles.Add(new Rectangle(45, 0, 427, 34));//界面标题,车间
            for (int i = 0; i < 2; i++)
            {
                Rectangles.Add(new Rectangle(473 + i * 165, 0, 165, 34));//时间

            }

            Rectangles.Add(new Rectangle(5, 540, 104, 58));//logo

            for (int i = 0; i < 10; i++)
            {
                Rectangles.Add(new Rectangle(120 + i * 68, 540, 58, 58));//下标题各按钮位置
            }
            for (int index = 5; index < Rectangles.Count; index++)
            {
                Rects.Add(new Region(Rectangles[index]));
            }
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }

            //取出配置表Boolids编号

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            //取出配置表Floatids编号

            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    FoolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                BtnList.Add(new CursorMovecs(i, (i + 1) % 10, 0));
            }
            for (int i = 0; i < 10; i++)
            {
                BtnList.Add(new CursorMovecs(i, (i + 1) % 10, 1));
            }
        }
    }
}
