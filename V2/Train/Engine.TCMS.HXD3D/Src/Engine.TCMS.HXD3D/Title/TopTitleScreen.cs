using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using Engine.TCMS.HXD3D.Constant;
using Engine.TCMS.HXD3D.Fault;
using Engine.TCMS.HXD3D.Fault.Ensure;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.MainInstance;
using Engine.TCMS.HXD3D.Title.TopTitle;
using Engine.TCMS.HXD3D.主菜单;
using Engine.TCMS.HXD3D.底层共用;
using Engine.TCMS.HXD3D.过程数据.NetControl;
using Engine.TCMS.HXD3D.隔离;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.Title
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TopTitleScreen : HXD3BaseClass
    {
        // Fields
        private Draw m_TitleDraw;

        private readonly Timer m_Altimer = new Timer(1000.0);
        private readonly Image[] m_BaseImage = new Image[2];
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly List<Region> m_BtnAreaTitle = new List<Region>();
        private readonly List<Region> m_BtnAreaTrunk = new List<Region>();
        private ButtonState m_BtnModel1;
        private ButtonState m_BtnModel2;
        private ButtonState m_BtnModel3;
        private ButtonState m_BtnModel4;
        private bool[] m_BValue;
        public static TheCountdown Countdown = new TheCountdown(60);
        private Region m_FaultArea;
        private bool m_FaultBtnIsDown;
        private FlashTimer m_FlashTimer;
        private float[] m_FValue;
        private List<Image> m_ImgsList;
        private bool m_IsBeCurrentFault;
        private bool m_IsBeUnFlagFault;
        private bool m_IsneedFalsh;
        private bool m_IsShowFaultBtn;
        private bool m_IsShowKeyboard;
        private Image[] m_KeyboardImgs;
        public int MainView = 1;
        private bool m_NeedFlash;
        private bool m_PasswordIsDrow;
        private bool m_PasswordIsRight;
        private Keyboard m_PasswordKeyboard;
        private List<RectangleF> m_RectsList;
        private readonly List<RectangleF> m_RectsTitleBtnList = new List<RectangleF>();
        private readonly List<RectangleF> m_RectsTitleImgList = new List<RectangleF>();
        private readonly List<RectangleF> m_RectsTrunkBtnList = new List<RectangleF>();
        private readonly List<RectangleF> m_RectsTrunkImgList = new List<RectangleF>();
        private readonly string[] m_StrHead1Ch;
        private readonly string[] m_StrHead2Ch;
        private readonly string[] m_StrTrunk2Ch;
        private readonly string[] m_StrTrunk3Ch;
        private readonly string[] m_StrTrunk4Ch;
        private readonly string[] m_StrTrunk5Ch;
        private readonly string[] m_StrTrunk6Ch;
        private readonly string[] m_StrTrunk7Ch;
        private readonly string[] m_StrTrunk8Ch;
        private readonly SolidBrush[] m_TheBtnsBrush = new SolidBrush[5];
        private string m_TheName;
        private List<ButtonState> m_TitleButtonList;
        private readonly List<ButtonState> m_TitleButtonList1;
        private readonly List<ButtonState> m_TitleButtonList2;
        private readonly bool[] m_TitleBtnIsDown;
        private readonly string m_TrainID;
        public static string TrainID = "0001";
        public static string TrainID1 = "HXD3D0001";
        public List<ButtonState> TrunkButtonList;
        public List<List<ButtonState>> TrunkButtonLists;
        private readonly bool[] m_TrunkBtnIsDown;
        private SolidBrush m_TxtColor;

        public TopTitleNameProvider TitleNameProvider { get; private set; }

        private Lazy<ITopTitleNameFixder> m_ProcessNetControlNameFixder;

        // Properties
        public bool MainModel { get; set; }

        // Methods
        public TopTitleScreen()
        {
            var flagArray = new bool[8];
            flagArray[0] = true;
            m_TitleBtnIsDown = flagArray;
            m_TrunkBtnIsDown = new bool[8];
            m_IsShowKeyboard = false;
            m_PasswordIsRight = false;
            m_PasswordIsDrow = false;
            m_FaultBtnIsDown = false;
            m_TitleButtonList = new List<ButtonState>();
            m_TitleButtonList1 = new List<ButtonState>();
            m_TitleButtonList2 = new List<ButtonState>();
            TrunkButtonList = new List<ButtonState>();
            TrunkButtonLists = new List<List<ButtonState>>();
            m_StrHead1Ch = new[] {"主页", "列车信息", "控制", "空气制动系统", "过程数据", "数据输入", "维护测试", "事件履历"};
            m_StrHead2Ch = new[] {"主页", "列车信息", "控制", "空气制动系统", "过程数据", "", "", ""};
            m_StrTrunk2Ch = new[] {"机车纵览", "", "", "", "", "", "", ""};
            m_StrTrunk3Ch = new[] {"隔离", "受电弓预选择", "距离计数器", "", "", "", "", ""};
            m_StrTrunk4Ch = new[] {"制动信息", "", "隔离阀\n状态", "", "", "", "", ""};
            m_StrTrunk5Ch = new[] {"列车供电", "驱动", "牵引/制动力", "开关状态", "辅助", "蓄电池", "网络控制", "计数"};
            m_StrTrunk6Ch = new[] {"轮径", "轮缘润滑", "日期时间设定", "其他设置", "", "", "", ""};
            m_StrTrunk7Ch = new[] {"主司控器试验", "启动试验", "零级位试验", "辅助电源试验", "显示灯试验", "无人警惕试验", "轮缘润滑试验", "顶轮试验"};
            m_StrTrunk8Ch = new[] {"无过滤", "激活事件", "", "", "", "", "", ""};
            m_TheName = string.Empty;
            m_TrainID = "HXD3D0001";
            TitleNameProvider = new TopTitleNameProvider();
            TitleNameProvider.RegisteNameFixder(new TopTitleManticeNameFixder(this));
        }

        private void ButtonDownState(int btnID)
        {
            switch (MainView)
            {
                case 1:
                    m_TrunkBtnIsDown[btnID] = true;
                    break;

                case 2:
                    if (btnID != 0)
                    {
                        m_TrunkBtnIsDown[btnID] = true;
                        break;
                    }

                    if (!TrunkButtonList[0].IsLocked)
                    {
                        m_TrunkBtnIsDown[0] = true;
                    }
                    break;

                case 3:
                    ButtonDownStateFun(3, btnID);
                    break;

                case 4:
                    ButtonDownStateFun(3, btnID);
                    break;

                case 5:
                    ButtonDownStateFun(8, btnID);
                    break;

                case 6:
                    ButtonDownStateFun(4, btnID);
                    break;

                case 7:
                    ButtonDownStateFun(8, btnID);
                    break;

                case 8:
                    ButtonDownStateFun(2, btnID);
                    break;
            }
        }

        private void ButtonDownStateFun(int listMaxNumb, int btnDownID)
        {
            if (btnDownID < listMaxNumb)
            {
                if (!TrunkButtonList[btnDownID].IsLocked)
                {
                    for (var i = 0; i < listMaxNumb; i++)
                    {
                        TrunkButtonList[i].BtnRecover();
                        m_TrunkBtnIsDown[i] = false;
                    }

                    m_TrunkBtnIsDown[btnDownID] = true;
                }
            }
            else
            {
                m_TrunkBtnIsDown[btnDownID] = true;
            }
        }

        public void ButtonUpState(int btnID)
        {
            switch (MainView)
            {
                case 1:
                    m_TrunkBtnIsDown[btnID] = false;
                    if (btnID == 7)
                    {
                        if (!MainModel)
                        {
                            MainModel = true;
                            m_TitleButtonList = m_TitleButtonList1;
                            m_TitleButtonList[0].BtnStateChange(true);
                            TrunkButtonList[7] = m_BtnModel2;
                            break;
                        }

                        MainModel = false;
                        m_TitleButtonList = m_TitleButtonList2;
                        m_TitleButtonList[0].BtnStateChange(true);
                        TrunkButtonList[7] = m_BtnModel4;
                    }

                    break;

                case 2:
                    if (btnID != 0)
                    {
                        m_TrunkBtnIsDown[btnID] = false;
                        break;
                    }

                    if (!TrunkButtonList[0].IsLocked)
                    {
                        TrunkButtonList[0].BtnStateChange(true);
                        m_TrunkBtnIsDown[0] = false;
                        append_postCmd(CmdType.ChangePage, ChangePageNumb(MainView, btnID + 1), 0, 0f);
                    }
                    break;

                case 3:
                    ButtonUpStateFun(3, btnID);
                    break;

                case 4:
                    if (btnID == 1)
                    {
                        TrunkButtonList[btnID].BtnRecover();
                        m_TrunkBtnIsDown[btnID] = false;
                        break;
                    }

                    ButtonUpStateFun(3, btnID);
                    break;

                case 5:
                    ButtonUpStateFun(8, btnID);
                    break;

                case 6:
                    ButtonUpStateFun(4, btnID);
                    break;

                case 7:
                    ButtonUpStateFun(8, btnID);
                    break;

                case 8:
                    ButtonUpStateFun(2, btnID);
                    break;
            }
        }

        private void ButtonUpStateFun(int listMaxNumb, int btnDownID)
        {
            if (btnDownID < listMaxNumb)
            {
                if (!TrunkButtonList[btnDownID].IsLocked)
                {
                    m_TrunkBtnIsDown[btnDownID] = false;
                    TrunkButtonList[btnDownID].BtnStateChange(true);
                    append_postCmd(CmdType.ChangePage, ChangePageNumb(MainView, btnDownID + 1), 0, 0f);
                }
            }
            else
            {
                m_TrunkBtnIsDown[btnDownID] = false;
            }
        }

        private int ChangePageNumb(int mainViewNumb, int subViewNumb)
        {
            if (subViewNumb == 0)
            {
                return mainViewNumb != 5 ? 1 : 90;
            }

            return Convert.ToInt32(mainViewNumb + subViewNumb.ToString());
        }

        public void Draw(Graphics g)
        {
            if (ButtomTitleScreen.TitleBtnIsDown[10])
            {
                MainView = 8;
                TrunkButtonList = TrunkButtonLists[7];
                append_postCmd(CmdType.ChangePage, 1, 0, 0f);
                m_TitleButtonList[0].BtnStateChange(false);
                m_TitleButtonList[7].BtnStateChange(true);
                ButtomTitleScreen.TitleBtnIsDown[10] = false;
            }
            if (!m_IsShowKeyboard)
            {
                PaintButtonsState(g);
            }
            else
            {
                m_PasswordIsRight = m_PasswordKeyboard.GetBoolFromKeyboard(g, ref m_PasswordIsDrow);
                if (!(m_PasswordIsDrow || !m_PasswordIsRight))
                {
                    ButtonUpState(7);
                    MainView = 6;
                    TrunkButtonList = TrunkButtonLists[5];
                    append_postCmd(CmdType.ChangePage, 1, 0, 0f);
                    m_IsShowKeyboard = false;
                }
                else if (!m_PasswordIsDrow && !m_PasswordIsRight && Keyboard.BtnIsDown[12])
                {
                    append_postCmd(CmdType.ChangePage, 2, 0, 0f);
                    Keyboard.BtnIsDown[12] = false;
                    m_IsShowKeyboard = false;
                }
            }
            PaintValue(g);
            for (var i = 0; i < 2; i++)
            {
                var ef = m_RectsList[20 + i];
                g.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }

            if (m_TitleDraw != null)
            {
                m_TitleDraw(g);
            }
        }

        private List<ButtonState> GetBtnList(int numb)
        {
            int num;
            var list = new List<ButtonState>();
            if (numb == 0)
            {
                list.Add(new ButtonState(m_RectsTrunkBtnList[0], m_BaseImage, m_RectsTrunkImgList[0], GetimgList(7, 1),
                    false));
                for (num = 0; num < 2; num++)
                {
                    list.Add(new ButtonState(m_RectsTrunkBtnList[1 + num], m_BaseImage, m_RectsTrunkImgList[1 + num],
                        GetimgList(9 + num, 1), false));
                }
                for (num = 0; num < 3; num++)
                {
                    list.Add(new ButtonState(m_RectsTrunkBtnList[num + 3], m_BaseImage, m_RectsTrunkImgList[num + 3],
                        GetimgList(1, 1), false));
                }

                list.Add(new ButtonState(m_RectsTrunkBtnList[6], m_BaseImage, m_RectsTrunkImgList[6], GetimgList(13, 1),
                    false));
                list.Add(m_BtnModel4);
                return list;
            }

            if ((numb > 0) && (numb < 8))
            {
                for (num = 0; num < 8; num++)
                {
                    list.Add(new ButtonState(m_RectsTrunkBtnList[num], m_BaseImage, m_RectsTrunkImgList[num],
                        GetTrunkNames(numb)[num], true));
                }
            }

            return list;
        }

        private List<Image> GetimgList(int id, int index)
        {
            var list = new List<Image>();
            for (var i = 0; i < index; i++)
            {
                list.Add(m_ImgsList[id + i]);
            }

            return list;
        }

        public override string GetInfo()
        {
            return "标题视图";
        }

        private string[] GetTrunkNames(int numb)
        {
            var strArray = new string[8];
            switch (numb)
            {
                case 1:
                    return m_StrTrunk2Ch;

                case 2:
                    return m_StrTrunk3Ch;

                case 3:
                    return m_StrTrunk4Ch;

                case 4:
                    return m_StrTrunk5Ch;

                case 5:
                    return m_StrTrunk6Ch;

                case 6:
                    return m_StrTrunk7Ch;

                case 7:
                    return m_StrTrunk8Ch;
            }

            return strArray;
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < m_BValue.Length; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < m_FValue.Length; num++)
            {
                m_FValue[num] = FloatList[UIObj.InFloatList[num]];
            }

            if (!m_BValue[0x13])
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0f);
            }
            if (FaultReceive.MsgInf.CurHighLevelMsgList.Count > 0)
            {
                m_IsBeCurrentFault = true;
                if (FaultReceive.MsgInf.UnFlagCurrentMsgList.Count > 0)
                {
                    m_IsShowFaultBtn = true;
                    m_IsBeUnFlagFault = true;
                    m_NeedFlash = true;
                }
                else
                {
                    m_IsBeUnFlagFault = false;
                    m_NeedFlash = false;
                }
            }
            else
            {
                m_IsBeCurrentFault = false;
                m_IsShowFaultBtn = false;
            }
        }

        protected override  void Initalize()
        {
            InitData();
            ButtonUpState(7);
            TimerStart();

            m_ProcessNetControlNameFixder =
                new Lazy<ITopTitleNameFixder>(
                    () =>
                        new NetControlNameFixder(
                            ObjectService.GetObject<ProcessNetControl>(ProjectName).FirstOrDefault()));
        }

        private void InitData()
        {
            int num;
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];
            m_ImgsList = new List<Image>();
            for (num = 0; num < UIObj.ParaList.Count; num++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[num]));
            }

            m_BaseImage[0] = m_ImgsList[1];
            m_BaseImage[1] = m_ImgsList[2];
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.TopTitleScreen, ref m_RectsList))
            {
                for (num = 0; num < 16; num++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[num]));
                }

                m_BtnArea.Add(new Region(m_RectsList[20]));
                m_BtnArea.Add(new Region(m_RectsList[0x15]));
                for (num = 0; num < 8; num++)
                {
                    m_BtnAreaTitle.Add(m_BtnArea[num]);
                    m_BtnAreaTrunk.Add(m_BtnArea[8 + num]);
                    m_RectsTitleBtnList.Add(m_RectsList[num]);
                    m_RectsTrunkBtnList.Add(m_RectsList[8 + num]);
                    m_RectsTitleImgList.Add(m_RectsList[0x1a + num]);
                    m_RectsTrunkImgList.Add(m_RectsList[0x22 + num]);
                }

                m_FaultArea = new Region(m_RectsList[20]);
                m_FlashTimer = new FlashTimer(1);
                InitTheButtons();
                m_TheBtnsBrush[0] = SolidBrushsItems.YellowBrush1;
                m_TheBtnsBrush[1] = SolidBrushsItems.WhiteBrush;
                m_TheBtnsBrush[2] = new SolidBrush(Color.FromArgb(90, 90, 0));
                m_TheBtnsBrush[3] = new SolidBrush(Color.FromArgb(0x7d, 0x7d, 0));
                m_TheBtnsBrush[4] = new SolidBrush(Color.FromArgb(0xff, 0xff, 0x71));
                m_KeyboardImgs = new[] {m_ImgsList[14], m_ImgsList[2], m_ImgsList[1], m_ImgsList[15]};
                m_PasswordKeyboard = new Keyboard(m_KeyboardImgs, m_RectsList[0x2a], KeyboardStyle.密码键盘之发送取消,
                    new[] {"123", "", "发送", "取消", ""});
            }
        }

        public void InitTheButtons()
        {
            int num;
            m_BtnModel1 = new ButtonState(m_RectsTitleBtnList[0], m_BaseImage, m_RectsTitleImgList[0], GetimgList(3, 2),
                true);
            m_BtnModel2 = new ButtonState(m_RectsTrunkBtnList[7], m_BaseImage, m_RectsTrunkImgList[7], GetimgList(5, 1),
                false);
            m_BtnModel3 = new ButtonState(m_RectsTitleBtnList[0], m_BaseImage, m_RectsTitleImgList[0], GetimgList(5, 2),
                true);
            m_BtnModel4 = new ButtonState(m_RectsTrunkBtnList[7], m_BaseImage, m_RectsTrunkImgList[7], GetimgList(3, 1),
                false);
            m_TitleButtonList1.Add(m_BtnModel1);
            m_TitleButtonList1[0].BtnStateChange(true);
            for (num = 0; num < 7; num++)
            {
                m_TitleButtonList1.Add(new ButtonState(m_RectsTitleBtnList[1 + num], m_BaseImage,
                    m_RectsTitleImgList[1 + num], m_StrHead1Ch[num + 1], true));
            }

            m_TitleButtonList2.Add(m_BtnModel3);
            m_TitleButtonList2[0].BtnStateChange(true);
            for (num = 0; num < 4; num++)
            {
                m_TitleButtonList2.Add(new ButtonState(m_RectsTitleBtnList[1 + num], m_BaseImage,
                    m_RectsTitleImgList[1 + num], m_StrHead2Ch[num + 1], true));
            }
            for (num = 0; num < 3; num++)
            {
                m_TitleButtonList2.Add(new ButtonState(m_RectsTitleBtnList[5 + num], m_BaseImage,
                    m_RectsTitleImgList[5 + num], m_StrHead2Ch[num + 5], false));
            }

            m_TitleButtonList = m_TitleButtonList2;
            for (num = 0; num < 8; num++)
            {
                TrunkButtonLists.Add(GetBtnList(num));
            }

            TrunkButtonList = TrunkButtonLists[0];
        }


        public override bool mouseDown(Point point)
        {
            if (m_IsShowKeyboard)
            {
                m_PasswordKeyboard.KeyboardResponseMouseDown(point);
            }
            else
            {
                int num;
                for (num = 0; num < m_BtnAreaTitle.Count; num++)
                {
                    if (m_BtnAreaTitle[num].IsVisible(point))
                    {
                        for (var i = 0; i < 8; i++)
                        {
                            m_TitleButtonList[i].BtnRecover();
                            m_TitleBtnIsDown[i] = false;
                        }

                        m_TitleBtnIsDown[num] = true;
                        break;
                    }
                }
                for (num = 0; num < m_BtnAreaTrunk.Count; num++)
                {
                    if (m_BtnAreaTrunk[num].IsVisible(point))
                    {
                        ButtonDownState(num);
                        break;
                    }
                }

                if (m_IsShowFaultBtn && m_FaultArea.IsVisible(point))
                {
                    m_FaultBtnIsDown = true;
                }
            }

            return true;
        }

        public override bool mouseUp(Point point)
        {
            if (m_IsShowKeyboard)
            {
                m_PasswordKeyboard.KeyboardResponseMouseUp(point);
            }
            else
            {
                int num;
                for (num = 0; num < m_BtnAreaTitle.Count; num++)
                {
                    if (m_BtnAreaTitle[num].IsVisible(point) && !m_TitleButtonList[num].IsLocked)
                    {
                        m_TitleButtonList[num].BtnStateChange(true);
                        m_TitleBtnIsDown[num] = false;
                        if (num != 7)
                        {
                            ButtomTitleScreen.TitleBtnIsDown[10] = false;
                        }
                        if (MainModel)
                        {
                            MainView = num + 1;
                            TrunkButtonList = TrunkButtonLists[num];
                            foreach (var state in TrunkButtonList)
                            {
                                state.BtnRecover();
                            }

                            if (num == 0)
                            {
                                append_postCmd(CmdType.ChangePage, 2, 0, 0f);
                            }
                            else
                            {
                                append_postCmd(CmdType.ChangePage, num == 5 ? 60 : 1, 0, 0f);
                            }
                            if (num == 5)
                            {
                                m_PasswordIsDrow = true;
                            }
                        }
                        else if (num < 5)
                        {
                            MainView = num + 1;
                            TrunkButtonList = TrunkButtonLists[num];
                            foreach (var state in TrunkButtonList)
                            {
                                state.BtnRecover();
                            }

                            append_postCmd(CmdType.ChangePage, num != 0 ? 1 : 2, 0, 0f);
                        }

                        break;
                    }
                }
                for (num = 0; num < m_BtnAreaTrunk.Count; num++)
                {
                    if (m_BtnAreaTrunk[num].IsVisible(point))
                    {
                        ButtonUpState(num);
                        break;
                    }
                }

                if (m_IsShowFaultBtn && m_FaultArea.IsVisible(point))
                {
                    m_FaultBtnIsDown = false;
                    if (!(!m_IsBeCurrentFault || m_IsBeUnFlagFault))
                    {
                        m_IsShowFaultBtn = false;
                    }
                    else
                    {
                        append_postCmd(CmdType.ChangePage, 101, 0, 0f);
                        FaultEnsure.NavigateTo(EnsureType.EnsureAll);
                    }
                }
            }

            return true;
        }

        private void OnDrawInOtherView(Graphics g)
        {
            var ef = m_RectsList[0x16];
            g.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            if (FaultReceive.MsgInf.CurLowLevelMsgList.Count > 0)
            {
                g.DrawString(
                    FaultReceive.MsgInf.CurLowLevelMsgList[0].MsgContent +
                    (FaultReceive.MsgInf.CurLowLevelMsgList[0].MsgShowType == MsgShowType.倒计时
                        ? " - " + Countdown.Counter()
                        : ""), FontsItems.DefaultFont, SolidBrushsItems.YellowBrush1, m_RectsList[0x16],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
        }

        private void OnDrawInViewFirst(Graphics g)
        {
            int num;
            var ef = m_RectsList[0x2b];
            g.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            if (FaultReceive.MsgInf.CurLowLevelMsgList.Count > 0)
            {
                g.DrawString(
                    FaultReceive.MsgInf.CurLowLevelMsgList[0].MsgContent +
                    (FaultReceive.MsgInf.CurLowLevelMsgList[0].MsgShowType == MsgShowType.倒计时
                        ? " - " + Countdown.Counter()
                        : ""), FontsItems.DefaultFont, SolidBrushsItems.YellowBrush1, m_RectsList[0x2b],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
            for (num = 0; num < 10; num++)
            {
                ef = m_RectsList[0x2c + num];
                g.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
            for (num = 0; num < 3; num++)
            {
                if (m_BValue[num])
                {
                    g.DrawImage(m_ImgsList[0x15 + num], m_RectsList[0x36]);
                }
            }
            for (num = 0; num < 8; num++)
            {
                if (m_BValue[3 + num] && num != 3 && num != 7)
                {
                    g.DrawImage(m_ImgsList[0x18 + num], m_RectsList[0x37]);
                }
            }
            for (num = 0; num < 2; num++)
            {
                if (m_BValue[11 + num])
                {
                    g.DrawImage(m_ImgsList[0x20 + num], m_RectsList[0x38]);
                }
            }

            m_TxtColor = SolidBrushsItems.WhiteBrush;
            if (m_BValue[20])
            {
                m_TxtColor = SolidBrushsItems.BlackBrush;
                ef = m_RectsList[0x2f];
                g.FillRectangle(AlertToTest.AltertBrush[0], ef.X + 1f, ef.Y + 1f, ef.Width - 1f, ef.Height - 1f);
            }
            if (m_BValue[0x15])
            {
                m_TxtColor = m_IsneedFalsh ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush;
                ef = m_RectsList[0x2f];
                g.FillRectangle(m_IsneedFalsh ? AlertToTest.AltertBrush[1] : CommonRes.BlackBrush, ef.X + 1f, ef.Y + 1f,
                    ef.Width - 1f, ef.Height - 1f);
                m_IsneedFalsh = false;
            }
            if (m_BValue[0x16])
            {
                m_TxtColor = m_IsneedFalsh ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush;
                ef = m_RectsList[0x2f];
                g.FillRectangle(m_IsneedFalsh ? AlertToTest.AltertBrush[2] : CommonRes.BlackBrush, ef.X + 1f, ef.Y + 1f,
                    ef.Width - 1f, ef.Height - 1f);
                m_IsneedFalsh = false;
            }
            if (m_BValue[0x17])
            {
                m_TxtColor = SolidBrushsItems.BlackBrush;
                ef = m_RectsList[0x2f];
                g.FillRectangle(AlertToTest.AltertBrush[2], ef.X + 1f, ef.Y + 1f, ef.Width - 1f, ef.Height - 1f);
            }
            g.DrawString("无人\n警惕", FontsItems.DefaultFont, m_TxtColor, m_RectsList[0x2f],
                FontsItems.TheAlignment(FontRelated.居中));
            if (m_BValue[15])
            {
                g.DrawImage(m_ImgsList[0x23], m_RectsList[0x3a]);
            }
            if (m_BValue[0x10])
            {
                g.DrawImage(m_ImgsList[0x24], m_RectsList[0x3b]);
            }
            if (m_BValue[0x11])
            {
                g.DrawImage(m_ImgsList[0x25], m_RectsList[0x3d]);
            }
            if (m_BValue[14])
            {
                g.DrawImage(m_ImgsList[0x22], m_RectsList[0x3e]);
            }
            if (m_BValue[0x12])
            {
                g.DrawImage(m_ImgsList[0x26], m_RectsList[0x3f]);
            }
            if (m_BValue[0x18])
            {
                g.DrawImage(m_ImgsList[0x27], m_RectsList[60]);
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            m_IsneedFalsh = true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        private void PaintButtonsState(Graphics g)
        {
            for (var i = 0; i < 8; i++)
            {
                m_TitleButtonList[i].DrawTheBtn(g, m_TitleBtnIsDown[i], m_TheBtnsBrush);
                TrunkButtonLists[MainView - 1][i].DrawTheBtn(g, m_TrunkBtnIsDown[i], m_TheBtnsBrush);
            }

            if (m_IsShowFaultBtn)
            {
                if (m_NeedFlash && m_FlashTimer.IsNeedFlash())
                {
                    g.DrawImage(m_FaultBtnIsDown ? m_ImgsList[0x11] : m_ImgsList[0x10], m_RectsList[20]);
                    g.DrawString(FaultReceive.MsgInf.UnFlagCurrentMsgList[0].MsgContent, FontsItems.DefaultFont,
                        SolidBrushsItems.BlackBrush, m_RectsList[20], FontsItems.TheAlignment(FontRelated.居中));
                }
                else if (!m_NeedFlash)
                {
                    g.DrawImage(m_FaultBtnIsDown ? m_ImgsList[0x11] : m_ImgsList[0x10], m_RectsList[20]);
                    g.DrawString("诊断数据 事件 (全部确认)", FontsItems.DefaultFont, SolidBrushsItems.BlackBrush, m_RectsList[20],
                        FontsItems.TheAlignment(FontRelated.居中));
                }
            }
            if (FaultReceive.MsgInf.CurHighLevelMsgList.Count != 0)
            {
                g.DrawImage(m_ImgsList[0x12 + (int) FaultReceive.MsgInf.CurHighLevelMsgList[0].TheMsgLevel],
                    m_RectsList[0x15]);
            }
        }

        private void PaintValue(Graphics g)
        {
            for (var i = 0; i < 4; i++)
            {
                var ef = m_RectsList[0x10 + i];
                g.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }

            g.DrawString(m_TheName, FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[0x10],
                FontsItems.TheAlignment(FontRelated.靠左));
            g.DrawString(DateTime.Now.ToString("yyyy.MM.dd"), FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush,
                m_RectsList[0x11], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(DateTime.Now.ToString("HH:mm:ss"), FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush,
                m_RectsList[0x12], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(m_TrainID, FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[0x13],
                FontsItems.TheAlignment(FontRelated.居中));
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                if (nParaB == (int)ViewId.ProcessDataNetControl)
                {
                    TitleNameProvider.RegisteNameFixder(m_ProcessNetControlNameFixder.Value);
                }
            }

            if (nParaA == SetRunValueDefine.ParaADefine.InCurrent)
            {
                m_TheName = TitleNameProvider.GetTitleName(nParaA, nParaB, nParaC);

                if (nParaB == 60)
                {
                    m_IsShowKeyboard = true;
                }

                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, nParaB);

                if (nParaB != 0x1f)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        ConverterCutOff.ButtonOldState[i] = false;
                        ConverterCutOff.ChangeState[i] = true;
                    }
                }
            }
            else if ((nParaA == 2) && ((int) nParaC == 2))
            {
                if (nParaB == 0x33)
                {
                    mouseDown(new Point(0x1c0, 0x15));
                    mouseUp(new Point(0x1c0, 0x15));
                    mouseDown(new Point(0x2d, 0x41));
                    mouseUp(new Point(0x2d, 0x41));
                }
            }

            if (nParaB == 1)
            {
                if (m_TitleDraw == null)
                {
                    m_TitleDraw = (Draw) Delegate.Combine(m_TitleDraw, new Draw(OnDrawInViewFirst));
                }
                else if (!m_TitleDraw.GetInvocationList().Contains(new Draw(OnDrawInViewFirst)))
                {
                    m_TitleDraw = (Draw) Delegate.Remove(m_TitleDraw, new Draw(OnDrawInOtherView));
                    m_TitleDraw = (Draw) Delegate.Combine(m_TitleDraw, new Draw(OnDrawInViewFirst));
                }
            }
            else if (m_TitleDraw == null)
            {
                m_TitleDraw = (Draw) Delegate.Combine(m_TitleDraw, new Draw(OnDrawInOtherView));
            }
            else if (!m_TitleDraw.GetInvocationList().Contains(new Draw(OnDrawInOtherView)))
            {
                m_TitleDraw = (Draw) Delegate.Remove(m_TitleDraw, new Draw(OnDrawInViewFirst));
                m_TitleDraw = (Draw) Delegate.Combine(m_TitleDraw, new Draw(OnDrawInOtherView));
            }
        }

        private void TimerStart()
        {
            m_Altimer.Elapsed += OnTimedEvent;
            m_Altimer.Interval = 1000.0;
            m_Altimer.Enabled = true;
            m_Altimer.Start();
        }
    }
}