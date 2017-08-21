using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using ES.Facility.PublicModule.Memo;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Wuxi.TMS.ά��;

namespace Urban.Wuxi.TMS.����
{
    /// <summary>
    /// վ������
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class StationSet : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        public override string GetInfo()
        {
            return "վ������";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadStationInfo();

            LoadRoadInfo();

            InitData();
            return true;
        }

        private void LoadRoadInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "��·��Ϣ.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split('\t');
                if (split.Length == 2)
                {
                    string tmpStations = split[1];
                    string tmpStr = string.Empty;
                    string outTmp = string.Empty;
                    if (StringHelper.findStringByKey(tmpStations, "[", "]", ref tmpStr))
                    {
                        outTmp = tmpStr.Trim();
                    }

                    if (outTmp != string.Empty)
                    {
                        string lineID = split[0];
                        string[] stations = outTmp.Split('-');
                        if (lineID.Trim() == "1����")
                        {
                            foreach (string st in stations)
                            {
                                //1��������վ��
                                m_LineID1.Add(st);
                            }
                        }
                        else if (lineID.Trim() == "2����")
                        {
                            foreach (string st in stations)
                            {
                                //2��������վ��
                                m_LineID2.Add(st);
                            }
                        }
                    }
                }
            }
        }

        private void LoadStationInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "��վ��Ϣ.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split('\t');
                int stationID;
                if (split.Length == 2 && int.TryParse(split[0], out stationID))
                {
                    m_StationNameID.Add(split[1], stationID);
                }
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB != Convert.ToInt32(nParaC))
                {
                    NeedResetStartAndEndStation(1);
                    ResetInfo(0);
                    ResetInfo(1);
                    ResetInfo(2);
                }
            }
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::

        public override void paint(Graphics dcGs)
        {
            //GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            if (m_IsInLinesSelectView)
            {
                #region :::::::: ��·ѡ��

                for (; index < 13; ++index)
                {
                    if (m_KeyBoardBtns[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                m_KeyIsDown[index] = true;

                #endregion
            }
            else
            {
                #region :::::::: �ұ�����ť

                index = 0;
                for (; index < 8; ++index)
                {
                    if (m_Regions[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                switch (index)
                {
                    case 0: //ȫ�Զ��㲥
                        //_buttonIsDown[0] = true;
                        //_buttonIsDown[1] = false;
                        break;
                    case 1: //���Զ��㲥
                        //_buttonIsDown[0] = false;
                        //_buttonIsDown[1] = true;
                        break;
                    case 2: //�ֶ�ģʽ
                        m_ButtonIsDown[2] = true;
                        break;
                    case 3: //��
                        m_ButtonIsDown[3] = true;
                        break;
                    case 4: //1����
                        m_ButtonIsDown[4] = true;
                        break;
                    case 5: //2����
                        m_ButtonIsDown[5] = true;
                        break;
                    case 6: //·������
                        m_ButtonIsDown[6] = true;
                        break;
                    case 7: //����
                        m_ButtonIsDown[7] = true;
                        break;
                }

                #endregion

                #region :::::::: ���λ�õ�ʼ��վ���յ�վ�Լ��趨����

                if (m_MenuID == StationSettingMenuID.Line1 ||
                    m_MenuID == StationSettingMenuID.Line2)
                {
                    index = 8;
                    for (; index < 10; ++index)
                    {
                        if (m_Regions[index].IsVisible(nPoint))
                        {
                            break;
                        }
                    }
                    switch (index)
                    {
                        case 8:
                            m_ButtonIsDown[8] = true;
                            m_ButtonIsDown[9] = false;
                            m_CursorID = 0;
                            break;
                        case 9: //ֻ��ʼ��վ��Ϊ�ղ��ܵ��յ�վ
                            if (m_SetStartStationName != string.Empty)
                            {
                                m_ButtonIsDown[8] = false;
                                m_ButtonIsDown[9] = true;
                                m_CursorID = 1;
                            }
                            break;
                    }
                    if (m_Regions[60].IsVisible(nPoint))
                    {
                        if (m_SetisBtnCanRun) m_ButtonIsDown[60] = true;
                    }
                }

                #endregion

                #region  :::::::: ��Ӧ��·ѡ��ʼ��վ���յ�վ

                index = 12;
                for (; index < BtnNumbers(m_MenuID) + 12; ++index)
                {
                    if (m_Regions[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                if (index >= 12 && index < BtnNumbers(m_MenuID) + 12)
                {
                    if (m_CursorID == 0)
                    {
                        StationBtnState(index, true);
                        StationName(index - 12, true);
                        m_CursorID = 1; //��ѡ��������վ����Զ���ת���յ�վ��
                    }
                    else if (m_CursorID == 1)
                    {
                        if (m_SetStartStationName != string.Empty)
                        {
                            StationBtnState(index, false);
                            StationName(index - 12, false);
                            m_CursorID = 2;
                            m_SetisBtnCanRun = true;
                        }
                    }
                    else if (m_CursorID == 2)
                    {
                    }
                    else
                    {
                        //��û��ѡ����·������£�����վ�������ᵯ��
                        m_ButtonIsDown[index] = true;
                    }
                }

                #endregion
            }

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            if (m_IsInLinesSelectView)
            {
                #region :::::::: ��·ѡ��

                for (; index < 13; ++index)
                {
                    if (m_KeyBoardBtns[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                m_KeyIsDown[index] = false;
                if (index >= 0 && index < 13)
                {
                    LinesCount(index);
                }

                #endregion
            }
            else
            {
                #region :::::::: �ұ�����ť

                for (; index < 8; ++index)
                {
                    if (m_Regions[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                switch (index)
                {
                    case 2: //�ֶ�ģʽ
                        m_ButtonIsDown[2] = false;
                        break;
                    case 3: //��
                        m_ButtonIsDown[3] = false;
                        break;
                    case 4: //1����
                        m_ButtonIsDown[4] = false;
                        m_MenuID = StationSettingMenuID.Line1;
                        m_CurrentLineID = 1;
                        if (NeedResetStartAndEndStation(1))
                        {
                            ResetInfo(0);
                            ResetInfo(1);
                            ResetInfo(2);
                        }
                        break;
                    case 5: //2����
                        m_ButtonIsDown[5] = false;
                        m_MenuID = StationSettingMenuID.Line2;
                        m_CurrentLineID = 2;
                        if (NeedResetStartAndEndStation(2))
                        {
                            ResetInfo(0);
                            ResetInfo(1);
                            ResetInfo(2);
                        }
                        break;
                    case 6: //Routeѡ��
                        m_ButtonIsDown[6] = false;
                        m_IsInLinesSelectView = true;
                        ResetInfo(0);
                        ResetInfo(1);
                        ResetInfo(2);
                        break;
                    case 7: //����
                        m_ButtonIsDown[7] = false;
                        OnPost(CmdType.ChangePage, 11, 0, 0);
                        break;
                }

                #endregion

                #region :::::::: �趨����

                if (m_MenuID == StationSettingMenuID.Line1 ||
                    m_MenuID == StationSettingMenuID.Line2)
                {
                    if (m_Regions[60].IsVisible(nPoint))
                    {
                        if (m_SetisBtnCanRun)
                        {
                            m_ButtonIsDown[60] = false;
                            OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, m_SetStartStationId);
                            OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[1], 0, m_SetEndStationID);
                            m_SetisBtnCanRun = false;
                        }
                    }
                }

                #endregion

                #region  :::::::: ��Ӧ��·ѡ��ʼ��վ���յ�վ

                index = 12;
                for (; index < BtnNumbers(m_MenuID) + 12; ++index)
                {
                    if (m_Regions[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                if (index >= 12 && index < BtnNumbers(m_MenuID) + 12)
                {
                    if (m_CursorID == 0)
                    {
                    }
                    else if (m_CursorID == 1)
                    {
                    }
                    else if (m_CursorID == 2)
                    {
                    }
                    else
                    {
                        m_ButtonIsDown[index] = false;
                    }
                }

                #endregion
            }

            return base.mouseUp(nPoint);
        }

        #endregion

        #region :::::::::::::::::::::::::::: draw funes ::::::::::::::::::::::::::

        /// <summary>
        /// ��·ѡ��
        /// </summary>
        /// <param name="e">��ͼ����</param>
        private void DrawTrainLinesSelect(Graphics e)
        {
            m_KeyBoard.DrawKeyboard(e, ref m_KeyIsDown, m_DrawFormat); //????
            e.DrawString("ԭ·��", FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush,
                m_Rects[68], m_RightFormat);
            e.DrawString("��·��", FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush,
                m_Rects[69], m_RightFormat);
            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.m_WhiteBrush, m_Rects[70 + i]);
            }
            e.DrawString(m_CurrentLineID.ToString(), FormatStyle.m_Font18B,
                FormatStyle.m_BlackBrush, m_Rects[70], m_LeftFormat);
            e.DrawString(m_SetLineID, FormatStyle.m_Font18B,
                FormatStyle.m_BlackBrush, m_Rects[71], m_LeftFormat);
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            e.DrawRectangle(FormatStyle.m_WhitePen, m_Rects[0].X, m_Rects[0].Y, m_Rects[0].Width, m_Rects[0].Height);
            e.DrawLine(FormatStyle.m_MediumGreyPen, m_PDrawPoint[0], m_PDrawPoint[1]);
        }

        /// <summary>
        /// ʼ��վ���յ�վ��ѡ����·��������·
        /// </summary>
        private void DrawStationNamesAndLineID(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FormatStyle.m_Str15[i + 8], FormatStyle.m_Font12B,
                    FormatStyle.m_WhiteBrush, m_Rects[59 + i], m_RightFormat);
                e.DrawImage(m_Images[3], m_Rects[63 + i]);
            }

            e.DrawString(m_SetStartStationName, FormatStyle.m_Font12,
                FormatStyle.m_WhiteBrush, m_Rects[63], m_LeftFormat);
            e.DrawString(m_SetEndStationName, FormatStyle.m_Font12,
                FormatStyle.m_WhiteBrush, m_Rects[64], m_LeftFormat);

            switch (m_CursorID)
            {
                case 0:
                    e.DrawImage(m_Images[2], m_Rects[63]);
                    e.DrawString(m_SetStartStationName + Cursor(), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[63], m_LeftFormat);
                    break;
                case 1:
                    e.DrawImage(m_Images[2], m_Rects[64]);
                    e.DrawString(m_SetEndStationName + Cursor(), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[64], m_LeftFormat);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// �����ֵ
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            DrawStationsBtn(e);

            //�趨��ť
            if (m_MenuID == StationSettingMenuID.Line1 ||
                m_MenuID == StationSettingMenuID.Line2)
            {
                if (m_SetisBtnCanRun)
                {
                    if (m_ButtonIsDown[60])
                        e.DrawImage(m_Images[2], m_Rects[49]);
                    else
                        e.DrawImage(m_Images[0], m_Rects[49]);
                }
                else
                    e.DrawImage(m_Images[4], m_Rects[49]);
                e.DrawString("�� ��", FormatStyle.m_Font12B,
                    m_SetisBtnCanRun ? FormatStyle.m_BlackBrush : FormatStyle.m_MediumGreySolidBrush
                    , m_Rects[49], m_DrawFormat);
            }

            //�Ҳ�˵���
            for (int i = 0; i < 2; i++)
            {
                //��ʱ���ɻ�ɫ��
                if (!m_ButtonIsDown[i])
                    e.DrawImage(m_Images[4], m_Rects[51 + i]);
                else
                    e.DrawImage(m_Images[2], m_Rects[51 + i]);
                e.DrawString(FormatStyle.m_Str15[i], FormatStyle.m_Font14,
                    FormatStyle.m_MediumGreySolidBrush, m_Rects[51 + i], m_DrawFormat);
            }
            for (int i = 2; i < 8; i++)
            {
                if (!m_ButtonIsDown[i])
                    e.DrawImage(m_Images[0], m_Rects[51 + i]);
                else
                    e.DrawImage(m_Images[5], m_Rects[51 + i]);
                e.DrawString(FormatStyle.m_Str15[i], FormatStyle.m_Font14,
                    FormatStyle.m_BlackBrush, m_Rects[51 + i], m_DrawFormat);
            }
        }

        /// <summary>
        /// ����ѡ��Ĳ˵���Ż���
        /// ��Ӧ��վ����Ϣ
        /// </summary>
        /// <param name="e"></param>


        private void DrawStationsBtn(Graphics e)
        {
            switch (m_MenuID)
            {
                case StationSettingMenuID.AutoBroadcast: //ȫ�Զ��㲥
                    break;
                case StationSettingMenuID.SemiAutoBroadcast: //���Զ��㲥
                    break;
                case StationSettingMenuID.Line1: //1����
                    for (int i = 0; i < m_LineID1.Count; i++)
                    {
                        if (!m_ButtonIsDown[i + 12])
                            e.DrawImage(m_Images[0], m_Rects[i + 1]);
                        else
                            e.DrawImage(m_Images[2], m_Rects[i + 1]);
                        e.DrawString(m_LineID1[i], FormatStyle.m_Font12B,
                            FormatStyle.m_BlackBrush, m_Rects[i + 1], m_DrawFormat);

                    }
                    break;
                case StationSettingMenuID.Line2: //2����
                    for (int i = 0; i < m_LineID2.Count; i++)
                    {
                        if (!m_ButtonIsDown[i + 12])
                            e.DrawImage(m_Images[0], m_Rects[i + 1]);
                        else
                            e.DrawImage(m_Images[2], m_Rects[i + 1]);
                        e.DrawString(m_LineID2[i], FormatStyle.m_Font12B,
                            FormatStyle.m_BlackBrush, m_Rects[i + 1], m_DrawFormat);
                    }
                    break;
                case StationSettingMenuID.RouteSelect: //Routeѡ��
                    break;
            }
        }

        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            if (m_IsInLinesSelectView)
            {
                DrawTrainLinesSelect(e);
            }
            else
            {
                DrawFrame(e);
                DrawStationNamesAndLineID(e);
                DrawValue(e);
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        private bool LinesSelectRight(string linesIndex)
        {
            if (m_SetLineID == "1" || m_SetLineID == "2")
            {
                m_SetLineRight = true;
            }
            else
                m_SetLineRight = false;

            return m_SetLineRight;
        }

        /// <summary>
        /// ��·����
        /// </summary>
        /// <param name="index"></param>
        private void LinesCount(int index)
        {
            if (index >= 0 && index < 9)
            {
                m_SetLineID = (index + 1).ToString();
            }
            else if (index == 9)
            {
                m_SetLineID = "0";
            }
            else if (index == 10)
            {
                m_SetLineID = string.Empty;
            }
            else if (index == 11)
            {
                if (LinesSelectRight(m_SetLineID) && m_SetLineID != string.Empty)
                {
                    int ii = Convert.ToInt32(m_SetLineID);
                    OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, Convert.ToInt32(m_SetLineID));
                    m_IsInLinesSelectView = false;
                    m_MenuID = StationSettingMenuID.Line1;
                }
                else
                    m_IsShowErrorMsg = true;
            }
            else if (index == 12)
            {
                m_IsInLinesSelectView = false;
                m_MenuID = StationSettingMenuID.Line1;
            }
        }

        /// <summary>
        /// �жϵ�ǰ���µ�վ�������վ�����յ�վ
        /// ��ȡ��Ӧվ�㲢��ȷ��ʾ
        /// </summary>
        /// <param name="btnId">���������</param>
        /// <param name="isPressStartStation">��ǰ�Ƿ�ѡ�����վ</param>
        private void StationBtnState(int btnId, bool isPressStartStation)
        {
            int initNumb = 12;
            for (int i = initNumb; i < BtnNumbers(m_MenuID) + initNumb; i++)
            {
                m_ButtonIsDown[i] = false;
            }

            if (isPressStartStation)
            {
                m_StartStationBtnLock = btnId;
                m_ButtonIsDown[m_StartStationBtnLock] = true;

                if (m_EndStationBtnLock != -1)
                {
                    m_ButtonIsDown[m_EndStationBtnLock] = true;
                }
            }
            else
            {
                m_EndStationBtnLock = btnId;
                m_ButtonIsDown[m_EndStationBtnLock] = true;
                if (m_StartStationBtnLock != -1)
                {
                    m_ButtonIsDown[m_StartStationBtnLock] = true;
                }
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private int m_Cursorcount = 0;

        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        private string Cursor()
        {
            m_Cursorcount++;
            if (m_Cursorcount < 4)
            {
                m_Cursorcount++;
                return "|";
            }
            else if (m_Cursorcount >= 4 && m_Cursorcount < 8)
            {
                m_Cursorcount++;
                return "";
            }
            else
            {
                m_Cursorcount = 0;
                return "";
            }
        }

        /// <summary>
        /// ��λ��Ϣ
        /// </summary>
        /// <param name="typeId">��λ����:0Ϊʼ���յ�վ��λ;1Ϊʼ���յ�վ���;2Ϊ���а�����λ</param>
        private void ResetInfo(int typeId)
        {
            switch (typeId)
            {
                case 0: //ʼ��վ�յ�վ��λ
                    for (int i = 0; i < 2; i++)
                    {
                        m_ButtonIsDown[8 + i] = false;
                    }
                    m_CursorID = -1;
                    break;
                case 1: //���ʼ��վ���յ�վ��Ϣ
                    m_SetStartStationId = -1;
                    m_SetStartStationName = string.Empty;

                    m_SetEndStationID = -1;
                    m_SetEndStationName = string.Empty;

                    break;
                case 2: //���а�����λ
                    for (int i = 0; i < 49; i++)
                    {
                        m_ButtonIsDown[i + 12] = false;
                    }
                    m_StartStationBtnLock = -1;
                    m_EndStationBtnLock = -1;
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }

        /// <summary>
        /// �ж��Ƿ���Ҫ����ʼ��վ���յ�վ
        /// ����������͹��
        /// ������һ�ΰ�����·�뵱ǰ��·�ж�
        /// </summary>
        /// <param name="currentID">��ǰ���ڵ�������·����</param>
        /// <returns></returns>
        private bool NeedResetStartAndEndStation(int currentLineID)
        {
            if (currentLineID == m_LastbtnOfLineID)
            {
                return false;
            }
            else
            {
                m_LastbtnOfLineID = currentLineID;
                return true;
            }
        }

        /// <summary>
        /// ����menusID�жϵ�ǰ��·
        /// ��������Ҫ�����ĵ���վ��ť����
        /// </summary>
        /// <param name="menusID">��·����</param>
        /// <returns></returns>
        private int BtnNumbers(StationSettingMenuID menusID)
        {
            switch (menusID)
            {
                case StationSettingMenuID.Line1:
                    return m_LineID1.Count;
                case StationSettingMenuID.Line2:
                    return m_LineID2.Count;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// ��ȡʼ��վ���յ�վ�����Լ����
        /// </summary>
        /// <param name="btnIndex">�����ı��</param>
        /// <param name="isStartStation">��ǰΪѡ��ʼ��վ�����յ�վ</param>
        private void StationName(int btnIndex, bool isStartStation)
        {
            switch (m_MenuID)
            {
                case StationSettingMenuID.Line1:
                    if (isStartStation)
                    {
                        m_SetStartStationName = m_LineID1[btnIndex]; //��·1�е�վ����ֵ
                        m_SetStartStationId = m_StationNameID[m_SetStartStationName]; //����վ���ҳ�վ������վ����ı�Ÿ�ֵ
                    }
                    else
                    {
                        m_SetEndStationName = m_LineID1[btnIndex];
                        if (m_SetEndStationName == m_SetStartStationName)
                        {
                            //�յ�վ�����վ������ͬ����ͬ���յ�վ�����ѡ
                            m_SetEndStationName = string.Empty;
                            m_SetEndStationID = -1;
                        }
                        else
                        {
                            m_SetEndStationName = m_LineID1[btnIndex];
                            m_SetEndStationID = m_StationNameID[m_SetEndStationName];
                        }
                    }
                    break;
                case StationSettingMenuID.Line2:
                    if (isStartStation)
                    {
                        m_SetStartStationName = m_LineID2[btnIndex];
                        m_SetStartStationId = m_StationNameID[m_SetStartStationName];
                    }
                    else
                    {
                        m_SetEndStationName = m_LineID2[btnIndex];
                        if (m_SetEndStationName == m_SetStartStationName)
                        {
                            m_SetEndStationName = string.Empty;
                            m_SetEndStationID = -1;
                        }
                        else
                        {
                            m_SetEndStationName = m_LineID2[btnIndex];
                            m_SetEndStationID = m_StationNameID[m_SetEndStationName];
                        }
                    }
                    break;
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        /// <summary>
        /// ��ʼ�����ꡢ���顢����
        /// </summary>
        private void InitData()
        {
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            m_DrawFormat.Alignment = StringAlignment.Center;

            m_RightFormat.LineAlignment = StringAlignment.Center;
            m_RightFormat.Alignment = StringAlignment.Far;

            m_LeftFormat.LineAlignment = StringAlignment.Center;
            m_LeftFormat.Alignment = StringAlignment.Near;

            m_PDrawPoint = new PointF[200];

            m_Rects = new RectangleF[200];

            m_Images = new Image[30];

            m_ButtonIsDown = new bool[70];

            m_KeyIsDown = new bool[14];

            m_KeyBoardBtns = new List<Region>();

            m_Regions = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: _rects :::::::::::::::::::::::::::::::::::

            m_Rects[0] = new Rectangle(10, 160, 690, 380);
            //վ��
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    m_Rects[1 + i*7 + j] = new Rectangle(25 + j*95, 165 + i*53, 90, 50);
                }
            }
            //վ������
            for (int i = 0; i < 8; i++)
            {
                m_Rects[51 + i] = new Rectangle(710, 100 + i*56, 89, 54);
            }
            //ʼ��վ/�յ�վ
            for (int i = 0; i < 2; i++)
            {
                m_Rects[59 + i] = new Rectangle(30 + i*425, 105, 75, 45);
                m_Rects[61 + i] = new Rectangle(430, 106 + i*23, 90, 20);
                m_Rects[63 + i] = new Rectangle(105 + i*425, 105, 130, 45);
                m_Rects[65 + i] = new Rectangle(530, 106 + i*23, 130, 20);
            }

            //��·ѡ��
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects[68 + i*2 + j] = new Rectangle(100 + i*100, 160 + j*38, 100, 35);
                }
            }

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX)*FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY)*FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }

            #endregion

            #region :::::::::::::::::::::::::::::::: _regions ::::::::::::::::::::::::::::::::::::::

            for (int i = 0; i < 8; i++)
            {
                m_Regions.Add(new Region(m_Rects[51 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                m_Regions.Add(new Region(m_Rects[63 + i]));
            }
            for (int i = 0; i < 49; i++)
            {
                m_Regions.Add(new Region(m_Rects[1 + i]));
            }

            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::

            m_PDrawPoint[0] = new Point(705, 95);
            m_PDrawPoint[1] = new Point(705, 550);
            m_PDrawPoint[2] = new Point(380, 160);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX)*FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY)*FormatStyle.Scale;
            }

            #endregion

            m_KeyBoard = new NumbKeyboard(m_PDrawPoint[2]);

            for (int i = 0; i < m_KeyBoard.Rects.Length; i++)
            {
                m_KeyBoardBtns.Add(new Region(m_KeyBoard.Rects[i]));
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        /// <summary>
        /// ����·
        /// </summary>
        private string m_SetLineID = string.Empty; //_routeLineID

        /// <summary>
        /// �Ƿ�����������·
        /// </summary>
        private bool m_SetLineRight = false;

        /// <summary>
        /// �Ƿ�Ҫ��ʾ������Ϣ
        /// </summary>
        private bool m_IsShowErrorMsg = false;

        /// <summary>
        /// �Ƿ������·ѡ�����
        /// </summary>
        private bool m_IsInLinesSelectView = false;

        /// <summary>
        /// �������λ��
        /// -1��ʾû��ѡ��վ���
        /// 0��ʾ�����վ��
        /// 1��ʾ���յ�վ��
        /// 2��ʾʼ��վ���յ�վ���Ѿ�ѡ����ϣ����԰��趨��ť
        /// </summary>
        private int m_CursorID = -1;

        /// <summary>
        /// -1��ʾδѡ�����վ��������վ���
        /// ��վ�����İ�����Ÿ�����ǰ����
        /// </summary>
        private int m_StartStationBtnLock = -1;

        /// <summary>
        /// -1��ʾδѡ���յ�վ��������վ���
        /// ��վ�����İ�����Ÿ�����ǰ����
        /// </summary>
        private int m_EndStationBtnLock = -1;

        /// <summary>
        /// �ж�ѡվ�����Ƿ���������԰��趨��ť
        /// </summary>
        private bool m_SetisBtnCanRun = false;

        /// <summary>
        /// ��һ�ΰ�����ţ���ʼ��վ�յ�վ�й�
        /// </summary>
        private int m_LastbtnOfLineID = 1;

        /// <summary>
        /// ��ǰ��·ID
        /// Ĭ��Ϊ1
        /// </summary>
        private int m_CurrentLineID = 1;

        /// <summary>
        /// ѡ��������·
        /// Ĭ��Ϊ1
        /// </summary>
        private int m_RouteLineID = 1;

        /// <summary>
        /// ʼ��վID
        /// </summary>
        private int m_SetStartStationId = -1;

        /// <summary>
        /// ʼ��վ����
        /// </summary>
        private string m_SetStartStationName = string.Empty;

        /// <summary>
        /// �յ�վID
        /// </summary>
        private int m_SetEndStationID = -1;

        /// <summary>
        /// �յ�վ����
        /// </summary>
        private string m_SetEndStationName = string.Empty;

        //�жϵ�ǰ��ȡ���ı���Ϣ��0Ϊ�գ�1Ϊ��վ��Ϣ��2Ϊ��·��Ϣ
        private int m_ReadInfoId = 0;

        //�����ұ߰�ťѡ���Ӧ�Ĳ˵�
        private StationSettingMenuID m_MenuID = StationSettingMenuID.Line1;

        /// <summary>
        /// ����վ������
        /// </summary>
        private List<string> m_Stations = new List<string>();

        //1����վ��
        private readonly List<string> m_LineID1 = new List<string>();
        //2����վ��
        private readonly List<string> m_LineID2 = new List<string>();

        //
        private NumbKeyboard m_KeyBoard;

        private List<Region> m_KeyBoardBtns;

        private bool[] m_KeyIsDown;

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::

        /// <summary>
        /// ���꼯
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// ���ο�
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// ͼƬ��
        /// </summary>
        private Image[] m_Images;

        /// <summary>
        /// ���Ƿ���
        /// </summary>
        private bool[] m_ButtonIsDown;

        /// <summary>
        /// ��ť�Ƿ���԰�
        /// </summary>
        private bool[] m_BtnCanDown;

        /// <summary>
        /// �����б�
        /// </summary>
        private List<Region> m_Regions;

        private readonly Dictionary<string, int> m_StationNameID = new Dictionary<string, int>();

        #endregion#

        #endregion
    }
}