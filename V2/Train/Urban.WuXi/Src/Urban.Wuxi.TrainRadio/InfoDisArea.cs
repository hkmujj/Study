using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TrainRadio
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class InfoDisArea : TrainBaseClass
    {
        // Fields
        private static readonly SortedDictionary<int, string> StationList = new SortedDictionary<int, string>();
        private readonly bool[] m_BlackOrStart = new bool[2];
        private static readonly SolidBrush BlueBrush = new SolidBrush(Color.Blue);
        private readonly bool[] m_BValue = new bool[50];
        private int m_CurrentMenu = 0;
        private static readonly StringFormat DrawFormat = new StringFormat();
        private readonly float[] m_FValue = new float[20];
        private static readonly Image[] Img = new Image[30];
        private readonly bool[] m_IsDown = new bool[28];
        private bool m_IsDrawMicClose = false;
        private bool m_IsDrawTouch = false;
        private bool m_IsMicOpen = false;
        private int m_IsOkCount = 0;
        private bool m_IsUrgency = false;
        private static readonly StringFormat LeftFormat = new StringFormat();
        private string[] m_Menu51;
        private string m_Message = "";
        private string m_ModelValue = "单呼模式";
        private readonly bool[] m_OldbValue = new bool[50];
        private readonly Point[] m_PDrawPoint = new Point[50];
        private readonly Rectangle[] m_PDrawRect = new Rectangle[150];
        private static readonly StringFormat RightFormat = new StringFormat();
        private string row1_ = "";
        public string m_Row1 = "";
        private string row2_ = "";
        public string m_Row2 = "";
        private string row3_ = "";
        private string m_Row3 = "";
        private readonly bool[] m_SetBValue = new bool[10];
        private readonly int[] m_SetBValueNumb = new int[10];
        private readonly int[] m_Signplace = new int[28];
        private int m_StartCount = 0;
        private readonly string[] m_Str2 = {"车次号：", "终点站：", "位  置："};
        private readonly string[] m_Str3 = {"司机号：", "车组号："};
        private readonly Font m_TestFont18 = new Font("宋体", 18f, FontStyle.Bold);
        private int m_UrgencyCount = 0;
        private readonly string[] m_Urgencystr1 = {"紧急麦克风激活", "正在使用紧急模式", "紧急模式", "紧急模式"};
        private readonly string[] m_Urgencystr2 = {"正在使用紧急模式", "紧急模式", "紧急模式"};
        private int m_ViewMenu = 0;
        private static readonly SolidBrush WhiteBrush = new SolidBrush(Color.White);
        private static readonly Pen WhitePen2 = new Pen(Color.White, 2f);
        private static string m_Zhongdian = "";



        public void ButtonDown()
        {
            switch (OnButtonClick(1))
            {
                case 0:
                    m_IsDown[0] = true;
                    break;

                case 1:
                    m_IsDown[1] = true;
                    break;

                case 2:
                    m_IsDown[2] = true;
                    break;

                case 3:
                    m_IsDown[3] = true;
                    break;

                case 4:
                    m_IsDown[4] = true;
                    break;

                case 5:
                    m_IsDown[5] = true;
                    break;

                case 6:
                    m_IsDown[6] = true;
                    break;

                case 7:
                    m_IsDown[7] = true;
                    break;

                case 8:
                    m_IsDown[8] = true;
                    break;

                case 9:
                    m_IsDown[9] = true;
                    break;

                case 10:
                    m_IsDown[10] = true;
                    break;

                case 11:
                    m_IsDown[11] = true;
                    break;

                case 12:
                    m_IsDown[12] = true;
                    break;

                case 13:
                    m_IsDown[13] = true;
                    if (m_CurrentMenu == 10)
                    {
                        m_IsDrawTouch = true;
                        if (IsOk(4))
                        {
                            OnPost(CmdType.ChangePage, 51, 0, 0f);
                            m_IsUrgency = false;
                            m_IsDrawTouch = false;
                        }
                    }
                    break;

                case 14:
                    m_IsDown[14] = true;
                    break;

                case 15:
                    m_IsDown[15] = true;
                    break;

                case 16:
                    m_IsDown[16] = true;
                    break;

                case 17:
                    m_IsDown[17] = true;
                    break;

                case 18:
                    m_IsDown[18] = true;
                    break;

                case 19:
                    m_IsDown[19] = true;
                    break;

                case 20:
                    m_IsDown[20] = true;
                    break;

                case 21:
                    m_IsDown[21] = true;
                    break;

                case 22:
                    m_IsDown[22] = true;
                    break;

                case 23:
                    m_IsDown[23] = true;
                    break;

                case 24:
                    m_IsDown[24] = true;
                    break;

                case 25:
                    m_IsDown[25] = true;
                    break;

                case 26:
                    m_IsDown[26] = true;
                    break;

                case 27:
                    m_IsDown[27] = true;
                    break;
            }
            switch (OnButtonClick(2))
            {
                case 0:
                    m_IsDown[0] = false;
                    if (!m_BlackOrStart[0] && !m_BlackOrStart[1])
                    {
                        m_BlackOrStart[0] = m_BlackOrStart[1] = true;
                        return;
                    }
                    if (!m_BlackOrStart[0] || !m_BlackOrStart[1])
                    {
                        if (m_BlackOrStart[0] && !m_BlackOrStart[1])
                        {
                            m_BlackOrStart[1] = true;
                        }
                        return;
                    }
                    m_BlackOrStart[1] = false;
                    return;

                case 1:
                    m_IsDown[1] = false;
                    OnPost(CmdType.ChangePage, 501, 0, 0f);
                    OnPost(CmdType.SetBoolValue, m_SetBValueNumb[0], 1, 0f);
                    return;

                case 2:
                    m_IsDown[2] = false;
                    if (m_CurrentMenu == 1)
                    {
                        OnPost(CmdType.ChangePage, 502, 0, 0f);
                        OnPost(CmdType.SetBoolValue, m_SetBValueNumb[1], 1, 0f);
                    }
                    return;

                case 3:
                    m_IsDown[3] = false;
                    if (m_CurrentMenu == 1)
                    {
                        OnPost(CmdType.ChangePage, 503, 0, 0f);
                        OnPost(CmdType.SetBoolValue, m_SetBValueNumb[2], 1, 0f);
                    }
                    return;

                case 4:
                    m_IsDown[4] = false;
                    if (m_CurrentMenu == 1)
                    {
                        OnPost(CmdType.ChangePage, 504, 0, 0f);
                    }
                    return;

                case 5:
                    m_IsDown[5] = false;
                    if (m_CurrentMenu == 1)
                    {
                        OnPost(CmdType.ChangePage, 505, 0, 0f);
                    }
                    return;

                case 6:
                    m_IsDown[6] = false;
                    if (m_CurrentMenu == 1)
                    {
                        OnPost(CmdType.ChangePage, 506, 0, 0f);
                    }
                    return;

                case 7:
                    m_IsDown[7] = false;
                    if (m_CurrentMenu == 1)
                    {
                        OnPost(CmdType.ChangePage, 507, 0, 0f);
                    }
                    return;

                case 8:
                    m_IsDown[8] = false;
                    if (m_CurrentMenu == 1)
                    {
                        OnPost(CmdType.ChangePage, 508, 0, 0f);
                        return;
                    }
                    if (m_CurrentMenu != 9)
                    {
                        return;
                    }
                    if (!(m_ModelValue == "单呼模式"))
                    {
                        m_ModelValue = "单呼模式";
                        break;
                    }
                    m_ModelValue = "PABX模式";
                    break;

                case 9:
                    m_IsDown[9] = false;
                    if (!m_IsUrgency)
                    {
                        m_IsUrgency = true;
                        m_IsMicOpen = true;
                        OnPost(CmdType.ChangePage, 509, 0, 0f);
                    }
                    return;

                case 10:
                    m_IsDown[10] = false;
                    OnPost(CmdType.ChangePage, 51, 0, 0f);
                    return;

                case 11:
                    m_IsDown[11] = false;
                    m_Signplace[m_CurrentMenu]--;
                    m_IsOkCount = 0;
                    return;

                case 12:
                    m_IsDown[12] = false;
                    m_Signplace[m_CurrentMenu]++;
                    m_IsOkCount = 0;
                    return;

                case 13:
                    m_IsDown[13] = false;
                    if (m_CurrentMenu == 10)
                    {
                        m_IsDrawTouch = false;
                        m_IsDrawMicClose = true;
                    }
                    else
                    {
                        CancleButtonDown(m_CurrentMenu);
                    }
                    m_IsOkCount = 0;
                    return;

                case 14:
                    m_IsDown[14] = false;
                    return;

                case 15:
                    m_IsDown[15] = false;
                    return;

                case 16:
                    m_IsDown[16] = false;
                    return;

                case 17:
                    m_IsDown[17] = false;
                    return;

                case 18:
                    m_IsDown[18] = false;
                    return;

                case 19:
                    m_IsDown[19] = false;
                    return;

                case 20:
                    m_IsDown[20] = false;
                    return;

                case 21:
                    m_IsDown[21] = false;
                    return;

                case 22:
                    m_IsDown[22] = false;
                    return;

                case 23:
                    m_IsDown[23] = false;
                    return;

                case 24:
                    m_IsDown[24] = false;
                    return;

                case 25:
                    m_IsDown[25] = false;
                    return;

                case 26:
                    m_IsDown[26] = false;
                    return;

                case 27:
                    m_IsDown[27] = false;
                    return;

                default:
                    return;
            }
            m_IsOkCount = 0;
        }

        private void CancleButtonDown(int menuNumber)
        {
            switch (menuNumber)
            {
                case 2:
                    OnPost(CmdType.ChangePage, 51, 0, 0f);
                    break;

                case 3:
                    OnPost(CmdType.ChangePage, 51, 0, 0f);
                    break;

                case 4:
                    OnPost(CmdType.ChangePage, 51, 0, 0f);
                    break;

                case 5:
                    OnPost(CmdType.ChangePage, 51, 0, 0f);
                    break;

                case 6:
                    OnPost(CmdType.ChangePage, 51, 0, 0f);
                    break;
            }
        }



        private void ChangeCurrentMenu(bool isTimeOver, int settingTime)
        {
            if (isTimeOver && IsOk(settingTime))
            {
                OnPost(CmdType.ChangePage, 51, 0, 0f);
            }
        }

        private void DrawBlack(Graphics e)
        {
            if (m_BlackOrStart[0] && m_BlackOrStart[1])
            {
                OnPost(CmdType.ChangePage, 50, 0, 0f);
            }
        }

        private void DrawCall(Graphics e)
        {
            DrawDefault(e);
            ChangeCurrentMenu(true, 5);
        }

        private void DrawCallStation(Graphics e)
        {
            e.DrawString("1 开始当前站呼叫", m_TestFont18, WhiteBrush, m_PDrawRect[31], LeftFormat);
            e.DrawString("2 结束当前站呼叫", m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
            DrawNumber(e, m_Signplace[4], 2);
            ChangeCurrentMenu(true, 5);
        }

        private void DrawDefault(Graphics e)
        {
            e.DrawString(m_Row1, m_TestFont18, WhiteBrush, m_PDrawRect[31], LeftFormat);
            m_Row2 = ReturnRow2(row2_);
            e.DrawString(m_Row2, m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
            m_Row3 = ReturnRow3(row3_);
            e.DrawString(m_Row3, m_TestFont18, WhiteBrush, m_PDrawRect[33], LeftFormat);
        }

        private void DrawDriverID(Graphics e)
        {
            e.FillRectangle(BlueBrush, m_PDrawRect[20]);
            e.DrawLine(WhitePen2, m_PDrawPoint[20], m_PDrawPoint[21]);
            e.DrawString(m_Str3[0], m_TestFont18, WhiteBrush, m_PDrawRect[21], LeftFormat);
            e.DrawString(m_Str3[1], m_TestFont18, WhiteBrush, m_PDrawRect[22], LeftFormat);
            string s = (Convert.ToInt32(m_FValue[4]) == 0) ? "" : Convert.ToInt32(m_FValue[4]).ToString("0000");
            e.DrawString(s, m_TestFont18, WhiteBrush, m_PDrawRect[23], LeftFormat);
            string str2 = (Convert.ToInt32(m_FValue[5]) == 0) ? "" : Convert.ToInt32(m_FValue[5]).ToString("0000");
            e.DrawString(str2, m_TestFont18, WhiteBrush, m_PDrawRect[24], LeftFormat);
        }

        private void DrawFetion(Graphics e)
        {
            e.DrawString("1 发送信号故障", m_TestFont18, WhiteBrush, m_PDrawRect[31], LeftFormat);
            e.DrawString("2 发送机车故障", m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
            DrawNumber(e, m_Signplace[7], 2);
            ChangeCurrentMenu(true, 5);
        }

        private void DrawFixedInfoDisArea(Graphics e)
        {
            e.FillRectangle(BlueBrush, m_PDrawRect[10]);
            e.DrawLine(WhitePen2, m_PDrawPoint[10], m_PDrawPoint[11]);
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(m_Str2[i], m_TestFont18, WhiteBrush, m_PDrawRect[i + 11], LeftFormat);
            }
            string s = (Convert.ToInt32(m_FValue[1]) == 0) ? "" : Convert.ToInt32(m_FValue[1]).ToString("0000");
            e.DrawString(s, m_TestFont18, WhiteBrush, m_PDrawRect[14], LeftFormat);
            e.DrawString(m_Zhongdian, m_TestFont18, WhiteBrush, m_PDrawRect[15], LeftFormat);
            e.DrawString(Convert.ToInt32(m_FValue[3]).ToString(), m_TestFont18, WhiteBrush, m_PDrawRect[16], LeftFormat);
        }

        private void DrawInfoDisArea(Graphics e)
        {
            e.FillRectangle(BlueBrush, m_PDrawRect[30]);
            e.DrawLine(WhitePen2, m_PDrawPoint[30], m_PDrawPoint[31]);
            e.DrawString(m_Message, m_TestFont18, WhiteBrush, m_PDrawRect[55], LeftFormat);
            switch (m_CurrentMenu)
            {
                case 1:
                    DrawDefault(e);
                    break;

                case 2:
                    DrawCall(e);
                    break;

                case 3:
                    DrawResponses(e);
                    break;

                case 4:
                    DrawCallStation(e);
                    break;

                case 5:
                    DrawLocation(e);
                    break;

                case 6:
                    DrawMainmenu(e);
                    break;

                case 7:
                    DrawFetion(e);
                    break;

                case 8:
                    DrawSms(e);
                    break;

                case 9:
                    DrawModel(e);
                    break;

                case 10:
                    DrawUrgency(e);
                    break;
            }
        }

        private void DrawLocation(Graphics e)
        {
            e.DrawString("1 转行车调度", m_TestFont18, WhiteBrush, m_PDrawRect[31], LeftFormat);
            e.DrawString("2 转车辆段调度", m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
            e.DrawString("3 转停车场调度", m_TestFont18, WhiteBrush, m_PDrawRect[33], LeftFormat);
            DrawNumber(e, m_Signplace[5], 3);
            ChangeCurrentMenu(true, 5);
        }

        private void DrawMainmenu(Graphics e)
        {
            for (int i = 0; i < 12; i++)
            {
                e.DrawString(m_Menu51[i], m_TestFont18, WhiteBrush, m_PDrawRect[31 + i], LeftFormat);
            }
            DrawNumber(e, m_Signplace[6], 12);
            ChangeCurrentMenu(true, 5);
        }

        private void DrawModel(Graphics e)
        {
            row2_ = m_ModelValue;
            e.DrawString(m_ModelValue, m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
            e.DrawString("1012010", m_TestFont18, WhiteBrush, m_PDrawRect[33], LeftFormat);
            ChangeCurrentMenu(true, 5);
        }

        private void DrawNumber(Graphics e, int currentNumber, int maxNumber)
        {
            if (maxNumber < 13)
            {
                int num;
                if (currentNumber > 0)
                {
                    num = currentNumber%maxNumber;
                    if (num == 0)
                    {
                        e.DrawString("V", m_TestFont18, WhiteBrush, m_PDrawRect[42 + maxNumber], RightFormat);
                    }
                    else
                    {
                        e.DrawString("V", m_TestFont18, WhiteBrush, m_PDrawRect[42 + num], RightFormat);
                    }
                }
                else
                {
                    num = (currentNumber%maxNumber) + maxNumber;
                    e.DrawString("V", m_TestFont18, WhiteBrush, m_PDrawRect[42 + num], RightFormat);
                }
            }
        }

        private void DrawOn(Graphics e)
        {
            DrawSignalstrength(e);
            DrawFixedInfoDisArea(e);
            DrawDriverID(e);
            DrawInfoDisArea(e);
        }

        private void DrawRadioSetStart(Graphics e)
        {
            e.FillRectangle(BlueBrush, m_PDrawRect[9]);
            e.DrawString("自检成功\n正在加载系统\n请稍候……", m_TestFont18, WhiteBrush, m_PDrawRect[9], DrawFormat);
            m_StartCount++;
            if (m_StartCount > 25)
            {
                OnPost(CmdType.ChangePage, 51, 0, 0f);
                m_StartCount = 0;
            }
        }

        private void DrawResponses(Graphics e)
        {
            DrawDefault(e);
            ChangeCurrentMenu(true, 5);
        }

        private void DrawSignalstrength(Graphics e)
        {
            e.FillRectangle(BlueBrush, m_PDrawRect[0]);
            switch (Convert.ToInt32(m_FValue[0]))
            {
                case 0:
                    e.DrawImage(Img[0], m_PDrawPoint[0]);
                    break;

                case 1:
                    e.DrawImage(Img[1], m_PDrawPoint[0]);
                    break;

                case 2:
                    e.DrawImage(Img[2], m_PDrawPoint[0]);
                    break;

                case 3:
                    e.DrawImage(Img[3], m_PDrawPoint[0]);
                    break;

                default:
                    e.DrawImage(Img[3], m_PDrawPoint[0]);
                    break;
            }
            if (m_IsUrgency)
            {
                e.DrawImage(Img[4], m_PDrawRect[1]);
            }
        }

        private void DrawSms(Graphics e)
        {
            e.DrawString("1 新建信息", m_TestFont18, WhiteBrush, m_PDrawRect[31], LeftFormat);
            e.DrawString("2 收件箱", m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
            e.DrawString("3 预制信箱", m_TestFont18, WhiteBrush, m_PDrawRect[33], LeftFormat);
            DrawNumber(e, m_Signplace[8], 3);
            ChangeCurrentMenu(true, 5);
        }

        private void DrawUrgency(Graphics e)
        {
            e.DrawString(m_Row1, m_TestFont18, WhiteBrush, m_PDrawRect[31], LeftFormat);
            if (!m_IsDrawTouch)
            {
                if (!(!m_IsMicOpen || m_IsDrawMicClose))
                {
                    e.DrawString(ReturnUrgency(m_Urgencystr1), m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
                    e.DrawString(m_Row3, m_TestFont18, WhiteBrush, m_PDrawRect[33], LeftFormat);
                }
                else if (m_IsMicOpen && m_IsDrawMicClose)
                {
                    e.DrawString("紧急麦克风", m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
                    e.DrawString("结束", m_TestFont18, WhiteBrush, m_PDrawRect[33], LeftFormat);
                    if (IsOk(2))
                    {
                        m_IsMicOpen = m_IsDrawMicClose = false;
                    }
                }
                else if (!(m_IsMicOpen || m_IsDrawMicClose))
                {
                    e.DrawString(ReturnUrgency(m_Urgencystr2), m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
                    e.DrawString(m_Row3, m_TestFont18, WhiteBrush, m_PDrawRect[33], LeftFormat);
                }
            }
            else
            {
                e.DrawString("长按 此键", m_TestFont18, WhiteBrush, m_PDrawRect[32], LeftFormat);
                e.DrawString("退出", m_TestFont18, WhiteBrush, m_PDrawRect[33], LeftFormat);
            }
        }



        public override string GetInfo()
        {
            return "信息显示区";
        }


        private void GetValue()
        {
            int num;
            for (num = 0; num < UIObj.InBoolList.Count; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
                m_OldbValue[num] = BoolOldList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < UIObj.InFloatList.Count; num++)
            {
                m_FValue[num] = FloatList[UIObj.InFloatList[num]];
            }
            for (num = 0; num < UIObj.OutBoolList.Count; num++)
            {
                m_SetBValue[num] = BoolList[UIObj.OutBoolList[num]];
                m_SetBValueNumb[num] = UIObj.OutBoolList[num];
            }
            ButtonDown();
            JudgeIsOver();
            GetZhongDian(Convert.ToInt32(m_FValue[2]));
        }

        private void GetZhongDian(int stationID)
        {
            StationList.TryGetValue(stationID, out m_Zhongdian);
            if (m_Zhongdian == null)
            {
                m_Zhongdian = "";
            }
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;
            if (UIObj.ParaList.Count >= 0)
            {
                for (int i = 0; i < UIObj.ParaList.Count; i++)
                {
                    Img[i] = Image.FromFile(RecPath + @"\" + UIObj.ParaList[i]);
                }
            }
            else
            {
                nErrorObjectIndex = 0;
                return false;
            }
            InitData();
            LoadStationFile();

            return true;
        }

        private void LoadStationFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\车站信息.txt");
            var allLine = File.ReadAllLines(file, Encoding.Default);
            var index = 1;
            foreach (var line in allLine)
            {
                StationList.Add(index, line);
                index++;
            }
        }

        private void InitData()
        {
            int num;
            DrawFormat.Alignment = StringAlignment.Center;
            DrawFormat.LineAlignment = StringAlignment.Center;
            RightFormat.Alignment = StringAlignment.Far;
            RightFormat.LineAlignment = StringAlignment.Center;
            LeftFormat.Alignment = StringAlignment.Near;
            LeftFormat.LineAlignment = StringAlignment.Center;
            m_PDrawPoint[0] = new Point(10, 10);
            m_PDrawPoint[1] = new Point(497, 10);
            m_PDrawPoint[2] = new Point(0, 0);
            m_PDrawPoint[3] = new Point(0, 0);
            m_PDrawPoint[4] = new Point(0, 0);
            m_PDrawPoint[5] = new Point(0, 0);
            m_PDrawPoint[6] = new Point(0, 0);
            m_PDrawPoint[7] = new Point(0, 0);
            m_PDrawPoint[8] = new Point(0, 0);
            m_PDrawPoint[9] = new Point(0, 0);
            m_PDrawPoint[10] = new Point(5, 69);
            m_PDrawPoint[11] = new Point(635, 69);
            m_PDrawPoint[12] = new Point(0, 0);
            m_PDrawPoint[13] = new Point(0, 0);
            m_PDrawPoint[14] = new Point(0, 0);
            m_PDrawPoint[15] = new Point(0, 0);
            m_PDrawPoint[16] = new Point(0, 0);
            m_PDrawPoint[17] = new Point(0, 0);
            m_PDrawPoint[18] = new Point(0, 0);
            m_PDrawPoint[19] = new Point(0, 0);
            m_PDrawPoint[20] = new Point(5, 435);
            m_PDrawPoint[21] = new Point(635, 435);
            m_PDrawPoint[22] = new Point(0, 0);
            m_PDrawPoint[23] = new Point(0, 0);
            m_PDrawPoint[24] = new Point(0, 0);
            m_PDrawPoint[25] = new Point(0, 0);
            m_PDrawPoint[26] = new Point(0, 0);
            m_PDrawPoint[27] = new Point(0, 0);
            m_PDrawPoint[28] = new Point(0, 0);
            m_PDrawPoint[29] = new Point(0, 0);
            m_PDrawPoint[30] = new Point(5, 155);
            m_PDrawPoint[31] = new Point(635, 155);
            m_PDrawPoint[32] = new Point(0, 0);
            m_PDrawPoint[33] = new Point(0, 0);
            m_PDrawPoint[34] = new Point(0, 0);
            m_PDrawPoint[35] = new Point(0, 0);
            m_PDrawPoint[36] = new Point(0, 0);
            m_PDrawPoint[37] = new Point(0, 0);
            m_PDrawPoint[38] = new Point(0, 0);
            m_PDrawPoint[39] = new Point(0, 0);
            m_PDrawRect[0] = new Rectangle(0, 0, 640, 70);
            m_PDrawRect[1] = new Rectangle(426, 10, 70, 56);
            m_PDrawRect[2] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[3] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[4] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[5] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[6] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[7] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[8] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[9] = new Rectangle(0, 0, 640, 480);
            m_PDrawRect[10] = new Rectangle(0, 70, 640, 85);
            m_PDrawRect[11] = new Rectangle(10, 70, 150, 40);
            m_PDrawRect[12] = new Rectangle(310, 70, 150, 40);
            m_PDrawRect[13] = new Rectangle(10, 115, 150, 40);
            m_PDrawRect[14] = new Rectangle(150, 70, 150, 40);
            m_PDrawRect[15] = new Rectangle(450, 70, 150, 40);
            m_PDrawRect[16] = new Rectangle(150, 115, 150, 40);
            m_PDrawRect[17] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[18] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[19] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[20] = new Rectangle(0, 435, 640, 45);
            m_PDrawRect[21] = new Rectangle(10, 435, 150, 45);
            m_PDrawRect[22] = new Rectangle(310, 435, 150, 45);
            m_PDrawRect[23] = new Rectangle(150, 435, 150, 45);
            m_PDrawRect[24] = new Rectangle(450, 435, 150, 45);
            m_PDrawRect[25] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[26] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[27] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[28] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[29] = new Rectangle(0, 0, 0, 0);
            m_PDrawRect[30] = new Rectangle(0, 155, 640, 280);
            for (num = 0; num < 2; num++)
            {
                for (int i = 0; i < 6; i++)
                {
                    m_PDrawRect[(i + (num*6)) + 31] = new Rectangle(30 + (320*num), 155 + (i*40), 290, 40);
                    m_PDrawRect[(i + (num*6)) + 43] = new Rectangle(320*num, 155 + (i*40), 30, 40);
                }
            }
            m_PDrawRect[55] = new Rectangle(10, 395, 640, 40);
            m_PDrawRect[56] = new Rectangle(0, 0, 0, 0);
            m_Menu51 = new string[]
            {
                "1 短消息", "2 地址簿", "3 保密", "4 设置", "5 本机号码", "6 网络", "7 扫描", "8 通话记录", "9 录音回放", "10 调度归属", "11 操作日志",
                "12 列车信息"
            };
            for (num = 0; num < m_Signplace.Length; num++)
            {
                m_Signplace[num] = 1;
            }
        }

        private bool IsOk(int count)
        {
            m_IsOkCount++;
            if (m_IsOkCount > (count*5))
            {
                m_IsOkCount = 0;
                return true;
            }
            return false;
        }

        private void JudgeIsOver()
        {
            if ((m_BlackOrStart[0] && !m_BlackOrStart[1]) && IsOk(3))
            {
                m_BlackOrStart[0] = false;
            }
            if (!(m_BlackOrStart[0] || m_BlackOrStart[1]))
            {
                OnPost(CmdType.ChangePage, 510, 0, 0f);
            }
        }

        public int OnButtonClick(int type)
        {
            int num;
            if (type == 1)
            {
                for (num = 0; num < 28; num++)
                {
                    if (m_BValue[num])
                    {
                        return num;
                    }
                }
            }
            if (type == 2)
            {
                for (num = 0; num < 28; num++)
                {
                    if (!(m_BValue[num] || !m_OldbValue[num]))
                    {
                        return num;
                    }
                }
            }
            else if (type == 3)
            {
                for (int i = 0; i < 28; i++)
                {
                    if (!(!m_BValue[i] || m_OldbValue[i]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            if (m_ViewMenu == 23)
            {
                DrawBlack(dcGs);
            }
            else if (m_ViewMenu == 0)
            {
                DrawRadioSetStart(dcGs);
            }
            else
            {
                DrawOn(dcGs);
            }
            base.paint(dcGs);
        }

        private string ReturnRow2(string r)
        {
            if (r == "")
            {
                return "WXML1";
            }
            if (IsOk(3))
            {
                row2_ = "";
                return "WXML1";
            }
            return r;
        }

        private string ReturnRow3(string r)
        {
            if (r == "")
            {
                return "TCC019";
            }
            if (IsOk(3))
            {
                row3_ = "";
                return "TCC019";
            }
            return r;
        }

        private string ReturnUrgency(string[] str)
        {
            if (str.Length > 0)
            {
                if (IsOk(3))
                {
                    return str[(++m_UrgencyCount >= (str.Length - 1)) ? (m_UrgencyCount = 0) : m_UrgencyCount];
                }
                return str[m_UrgencyCount];
            }
            return "";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 1)
            {
                switch (nParaB)
                {
                    case 50:
                        m_ViewMenu = 0;
                        break;

                    case 51:
                        m_ViewMenu = 1;
                        m_CurrentMenu = 1;
                        m_Message = "";
                        break;

                    case 501:
                        m_ViewMenu = 2;
                        m_CurrentMenu = 2;
                        break;

                    case 502:
                        m_ViewMenu = 3;
                        m_CurrentMenu = 3;
                        m_Message = "正在发送呼叫确认信息…";
                        break;

                    case 503:
                        m_ViewMenu = 4;
                        m_CurrentMenu = 4;
                        m_Message = "当前站呼叫菜单";
                        break;

                    case 504:
                        m_ViewMenu = 5;
                        m_CurrentMenu = 5;
                        m_Message = "转调度归属菜单";
                        break;

                    case 505:
                        m_ViewMenu = 6;
                        m_CurrentMenu = 6;
                        m_Message = "主菜单";
                        break;

                    case 506:
                        m_ViewMenu = 7;
                        m_CurrentMenu = 7;
                        m_Message = "发送故障报告菜单";
                        break;

                    case 507:
                        m_ViewMenu = 8;
                        m_CurrentMenu = 8;
                        m_Message = "中文短信菜单";
                        break;

                    case 508:
                        m_ViewMenu = 9;
                        m_CurrentMenu = 9;
                        break;

                    case 509:
                        m_ViewMenu = 10;
                        m_CurrentMenu = 10;
                        break;

                    case 510:
                        m_ViewMenu = 23;
                        break;
                }
            }
        }

        // Nested Types
        private enum ButtonName
        {
            电源,
            呼叫,
            回应,
            呼站,
            位置,
            菜单,
            飞信,
            短信,
            模式,
            紧急,
            确认,
            上页,
            下页,
            取消,
            音量减,
            音量加,
            数字1,
            数字2,
            数字3,
            数字4,
            数字5,
            数字6,
            数字7,
            数字8,
            数字9,
            符号1,
            数字0,
            符号2
        }

        public enum Menu
        {
            开机,
            主显示区,
            呼叫,
            回应,
            呼站,
            位置,
            主菜单,
            飞信,
            短信,
            模式,
            紧急,
            短消息,
            地址簿,
            保密,
            设置,
            本机号码,
            网络,
            扫描,
            通话记录,
            录音回放,
            调度归属,
            操作日志,
            列车信息,
            黑屏
        }

        public enum ViewPage
        {
            短信视图 = 507,
            飞信视图 = 506,
            黑屏视图 = 510,
            呼叫视图 = 501,
            呼站视图 = 503,
            回应视图 = 502,
            紧急视图 = 509,
            开机视图 = 50,
            模式视图 = 508,
            位置视图 = 504,
            主菜单视图 = 505,
            主显示区视图 = 51
        }
    }
}




