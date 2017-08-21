using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using CommonUtil.Controls;
using CommonUtil.Util;
using MMI.Facility.Interface.Data;
using Motor.ATP._300T.Appraise;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.����;
using Motor.ATP._300T.����.���ܼ���˵�.FunState;

namespace Motor.ATP._300T.����.���ܼ���˵�
{
    public class OpenFunBtnCTCS300T : IOpennFunctionButton
    {
        /// <summary>
        /// ��ť״̬�仯
        /// </summary>
        /// <param name="currentFunMenu"></param>
        public void ChangeBtnState(FunMenuButtonId currentFunMenu)
        {
            if (!StateProvider.BtnStrDict.ContainsKey((int) currentFunMenu))
            {
                return;
            }

            for (var i = 0; i < 8; i++)
            {
                Buttons[i].Content = StateProvider.BtnStrDict[(int) currentFunMenu][i];
            }
        }

        /// <summary>
        /// ����Ŀ�ӻ���ģʽת��
        /// </summary>
        /// <param name="mode"></param>
        protected void ChangeMode(int mode)
        {
            for (var i = 0; i < 3; i++)
            {
                ModeSelect[i] = false;
            }
            ModeSelect[mode] = !ModeSelect[mode];

            m_StateProvider.FixModelText();
        }



        /// <summary>
        /// ˢ��ѡ�е�ģʽ
        /// </summary>
        private void RefreshSelectedModel()
        {
            if (m_ATPMainScreen.CurrentTrainMode == TrainMode.����)
            {
                ModeSelect[0] = true;
            }
            else
            {
                ModeSelect[0] = false;
            }

            StateProvider.BtnStrDict[2][0] = ModeSelect[0] ? "�˳�\n����" : "����";
            StateProvider.MenuValueArrDict[2][0].ContentStr = ModeSelect[0] ? "�˳�����ģʽ" : "����ģʽ";
        }

        /// <summary>
        /// ����˾���Ż��߳��κ�
        /// </summary>
        /// <param name="id">˾���Ż��߳��κ�</param>
        /// <param name="sendLetterInFloatList">˾���Ż��߳��κ�����ĸ����λ</param>
        /// <param name="sendIdInFloatList">˾���Ż��߳��κ������ַ���λ</param>
        protected void SetDriverIDorTrainId(string id, int sendLetterInFloatList, int sendIdInFloatList)
        {
            int result;

            if (int.TryParse(id, out result))
            {
                OnAppendPostCmd(CmdType.SetFloatValue, sendIdInFloatList, 0, result);
            }
            else
            {
                var allChars = id.ToCharArray(0, id.Length);
                //int test = (int)allChars[0];
                if (allChars.Length > 0 && OnAppendPostCmd != null)
                {
                    OnAppendPostCmd(CmdType.SetFloatValue, sendLetterInFloatList, 0, allChars[0]);
                }
                if (allChars.Length > 1)
                {
                    var tmp = string.Empty;
                    for (var i = 1; i < allChars.Length; i++)
                    {
                        tmp += allChars[i];
                    }
                    if (int.TryParse(tmp, out result))
                    {
                        OnAppendPostCmd(CmdType.SetFloatValue, sendIdInFloatList, 0, result);
                    }
                }
            }
        }

        public void MsgOver(int msgKey)
        {
            if (MsgCatchList.Contains(msgKey))
            {
                MsgCatchList.Remove(msgKey);
            }
            MsgHandlerATPList.MsgOver(msgKey, m_ATPMainScreen.CurrentTime);

        }

        /// <summary>
        /// ������Ϣ������������ת
        /// </summary>
        /// <param name="confirm">�Ƿ�ȷ��</param>
        private void SpecialMsgEndThenSendValueAndJumpView(bool confirm)
        {
            int jumpId;
            if (confirm) //��ǰ������ȷ����
            {
                //����ȷ������
                if (MsgHandlerATPList.SpecialMsgList[0].SendValueToCpu[0][0] != 0)
                {
                    foreach (var t in MsgHandlerATPList.SpecialMsgList[0].SendValueToCpu[0])
                    {

                        OnAppendPostCmd(CmdType.SetBoolValue,
                            Math.Abs(t),
                            t > 0 ? 1 : 0, 0);
                    }
                    MsgHandlerATPList.SpecialMsgList[0].BeSure = true;
                }

                //��ת�˵�
                jumpId = MsgHandlerATPList.SpecialMsgList[0].MsgOverThenJumpViewArr.Length > 1
                    ? MsgHandlerATPList.SpecialMsgList[0].MsgOverThenJumpViewArr[1]
                    : MsgHandlerATPList.SpecialMsgList[0].MsgOverThenJumpViewArr[0];

                if (StateProvider.BtnStrDict.ContainsKey(jumpId))
                {
                    if (MsgHandlerATPList.SpecialMsgList.Count == 1)
                    {
                        MenuID = (FunMenuButtonId) jumpId;
                    }
                    else if (MsgHandlerATPList.SpecialMsgList.Count > 1)
                    {
                        MenuID = (FunMenuButtonId) MsgHandlerATPList.SpecialMsgList[1].MsgStartThenJumpView;
                    }

                    MsgHandlerATPList.SpecialMsgList[0].Jumped = true;
                }
                else
                {
                    MenuID = FunMenuButtonId.δѡ��;
                }
            }
            else
            {
                //����ȡ������
                if (MsgHandlerATPList.SpecialMsgList[0].SendValueToCpu.Count == 2 &&
                    MsgHandlerATPList.SpecialMsgList[0].SendValueToCpu[1][0] != 0)
                {
                    foreach (var t in MsgHandlerATPList.SpecialMsgList[0].SendValueToCpu[1])
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue,
                            Math.Abs(t),
                            t > 0 ? 1 : 0, 0);
                    }
                    MsgHandlerATPList.SpecialMsgList[0].BeSure = true;
                }

                //��ת�˵�
                jumpId = MsgHandlerATPList.SpecialMsgList[0].MsgOverThenJumpViewArr.Length > 2
                    ? MsgHandlerATPList.SpecialMsgList[0].MsgOverThenJumpViewArr[2]
                    : MsgHandlerATPList.SpecialMsgList[0].MsgOverThenJumpViewArr[0];

                if (StateProvider.BtnStrDict.ContainsKey(jumpId))
                {
                    if (MsgHandlerATPList.SpecialMsgList.Count == 1)
                    {
                        MenuID = (FunMenuButtonId) jumpId;
                    }
                    else if (MsgHandlerATPList.SpecialMsgList.Count > 1)
                    {
                        MenuID = (FunMenuButtonId) MsgHandlerATPList.SpecialMsgList[1].MsgStartThenJumpView;
                    }

                    MsgHandlerATPList.SpecialMsgList[0].Jumped = true;
                }
                else
                {
                    MenuID = FunMenuButtonId.δѡ��;
                }
            }
        }

        #region ::::::::::::::::::::::::::::: ����������ĸ����Ӧ���� ::::::::::::::::::::::::::::

        /// <summary>
        /// ��ȡ��ť����ʾ����ĸ
        /// </summary>
        /// <param name="input">��Ҫ�޸ĵ�ֵ, ��ʾ˾���Ż��߳��κ�</param>
        /// <param name="nKeyWords">��ť��ʾ��ֵ������ABC</param>
        /// <param name="nNowKeyCode">��ť��ʾ��ֵ������2</param>
        /// <returns></returns>
        protected string GetKeyChar(string input, string nKeyWords, int nNowKeyCode)
        {
            if (m_Keyboard.LastKey > -1)
            {
                if (nNowKeyCode == m_Keyboard.LastKey)
                {
                    m_Keyboard.TurnTheSameBtn();
                    input = input.Remove(input.Length - 1, 1);
                    input += m_Keyboard.GetKeyValue(false);
                }
                else
                {
                    m_Keyboard.RegisterKeyWords(nKeyWords, nNowKeyCode);

                    input += m_Keyboard.GetKeyValue(false);
                }
            }
            else
            {
                m_Keyboard.RegisterKeyWords(nKeyWords, nNowKeyCode);

                input += m_Keyboard.GetKeyValue(false);
            }

            return input;
        }

        /// <summary>
        /// ��ȡ��ǰ���°�ť��ʵ�ʻ�ȡ����ֵ������ֵ����ĳ����ĸ
        /// </summary>
        /// <param name="value">��Ҫ�޸ĵ�ֵ, ��ʾ˾���Ż��߳��κ�</param>
        /// <param name="btnOfLetter">��ǰ����ʾ����ĸ,����"ABC"</param>
        /// <param name="btnOfNumb">��ǰ��ť���������</param>
        /// <returns></returns>
        protected string GetTrainIDorDriverId(string value, string btnOfLetter, int btnOfNumb)
        {
            if (value.Length >= 8)
                return value;
            if (!m_SwitchOver || string.IsNullOrEmpty(btnOfLetter))
            {
                value += btnOfNumb.ToString();
            }
            else
            {
                value = GetKeyChar(value, btnOfLetter, btnOfNumb);
            }

            return value;
        }

        /// <summary>
        /// ������ӦmenuID��Ӧ������ĸ��ť
        /// �����г�����
        /// </summary>
        /// <param name="btnOfLetter">��ť�������ĸ,����"ABC"</param>
        /// <param name="btnOfNumb">��ť���������</param>
        protected void GetValueOfTrainInfo(string btnOfLetter, int btnOfNumb)
        {
            switch (MenuID)
            {
                case FunMenuButtonId.������ʼ_˾����:
                case FunMenuButtonId.F1����_F1˾����:
                    DriverId = GetTrainIDorDriverId(DriverId, btnOfLetter, btnOfNumb);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    break;
                case FunMenuButtonId.������ʼ_���κ�:
                case FunMenuButtonId.F1����_F2���κ�:
                    TrainId = GetTrainIDorDriverId(TrainId, btnOfLetter, btnOfNumb);
                    StateProvider.MenuValueArrDict[(int) MenuID][MenuID == FunMenuButtonId.F1����_F2���κ� ? 1 : 3]
                        .ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F1����_F3�г�����:
                    if (btnOfNumb != 1 && btnOfNumb != 2)
                        return;
                    TrainData = btnOfNumb.ToString();
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainData;
                    break;
                case FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC:
                    RBCID += btnOfNumb.ToString();
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    break;
                case FunMenuButtonId.F4�ȼ�_F1CTCS3_�绰����:
                    TelNmub += btnOfNumb.ToString();
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
                case FunMenuButtonId.δѡ��:
                    ResponseLinkBtnEvent(btnOfNumb);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btnOfNumb">��ť���������</param>
        private void ResponseLinkBtnEvent(int btnOfNumb)
        {
            switch (btnOfNumb)
            {
                case 1:
                    SelectModelControlModel(TrainMode.����);
                    break;
                case 2:
                    SelectModelControlModel(TrainMode.Ŀ��);
                    break;
                case 3:
                    SelectModelControlModel(TrainMode.����);
                    break;
                case 4:
                    RequestStart();
                    break;
                case 5:
                    RequestRelaseBrake();
                    break;
            }
        }

        private void SelectModelControlModel(TrainMode trainMode)
        {
            switch (trainMode)
            {
                case TrainMode.Ŀ��:
                    MenuID = FunMenuButtonId.F2ģʽ_F2����Ŀ��;
                    MenuID = m_ATPMainScreen.CurrentTrainMode == TrainMode.Ŀ��
                        ? FunMenuButtonId.F2ģʽ_F2�˳�Ŀ��
                        : FunMenuButtonId.F2ģʽ_F2����Ŀ��;
                    break;
                case TrainMode.����:
                    MenuID = m_ATPMainScreen.CurrentTrainMode == TrainMode.����
                        ? FunMenuButtonId.F2ģʽ_F1�˳�����
                        : FunMenuButtonId.F2ģʽ_F1�������;
                    break;
                case TrainMode.����:
                    MenuID = m_ATPMainScreen.CurrentTrainMode == TrainMode.����
                        ? FunMenuButtonId.F2ģʽ_F3�˳�����
                        : FunMenuButtonId.F2ģʽ_F3�������;
                    break;
            }
        }

        #endregion

        /// <summary>
        /// ��ť״̬��Ӧ
        /// </summary>
        /// <param name="btnId"></param>
        /// <param name="state"></param>
        public void ButtonStateChange(BtnInfo btnId, MouseState state)
        {
            if (state == MouseState.MouseDown)
            {
                if (btnId.IsPressResponsed)
                {
                    return;
                }
                btnId.ResponsePress();
            }
            else
            {
                if (btnId.IsUpResponsed)
                {
                    return;
                }
                btnId.ResponseUp();
            }

            ResponseBtnEvent(btnId.BtnId, state);
        }



        private void ResponseBtnEvent(int btnId, MouseState state)
        {
            //AppraiseControl.ResetNotifies();

            //F1,~ F8
            if (btnId >= 0 && btnId < 7 && !Buttons[btnId].IsEnable)
            {
                return;
            }
            Buttons[0].IsOutlineVisible = false;
            Buttons[1].IsOutlineVisible = false;
            switch (btnId)
            {
                case 0: //F1
                    ResponseF1();
                    break;
                case 1: //F2
                    ResponseF2();
                    break;
                case 2: //F3
                    ResponseF3();
                    break;
                case 3: //F4
                    ResponseF4();
                    break;
                case 4: //F5
                    ResponseF5();
                    break;
                case 5: //F6
                    ResponseF6();
                    break;
                case 6: //F7
                    ResponseF7();
                    break;
                case 7: //F8
                    ResponseF8();
                    break;
                default:
                    ResponseNumbers(btnId, state);
                    break;
            }
        }

        private void ResponseF1()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.δѡ��:
                    MenuID = FunMenuButtonId.F1����;
                    break;
                case FunMenuButtonId.F1����:
                    MenuID = FunMenuButtonId.F1����_F1˾����;
                    //AppraiseControl.NotifyInputDriverID();
                    if (DriverId == "0")
                    {
                        DriverId = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    break;
                case FunMenuButtonId.F2ģʽ:
                    MenuID = m_ATPMainScreen.CurrentTrainMode == TrainMode.����
                        ? FunMenuButtonId.F2ģʽ_F1�˳�����
                        : FunMenuButtonId.F2ģʽ_F1�������;

                    break;
                case FunMenuButtonId.F3��Ƶ:
                    MenuID = FunMenuButtonId.F3��Ƶ_F1����;
                    break;
                case FunMenuButtonId.F4�ȼ�:
                    //MenuID = FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC;
                    MenuID = FunMenuButtonId.δѡ��;
                    //if (RBCID == "0")
                    //{
                    //    RBCID = string.Empty;
                    //}
                    //MenuValueArrDict[(int)FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC][1].ContentStr = RBCID;
                    Buttons[0].IsOutlineVisible = false;
                    Buttons[1].IsOutlineVisible = false;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub�����ֶ�����C3ģʽ), 1,
                        0);
                    //SendSelectCtcsState(3);

                    break;
                case FunMenuButtonId.F5����:
                    //����Ϣ�б����һ����Ϣ
                    OnAppendPostCmd(CmdType.SetBoolValue,
                        m_AppStartData[0]
                            ? m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub�˳�Ŀ��ģʽ)
                            : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oubִ���ƶ�����1), 1,
                        0);
                    break;
                case FunMenuButtonId.F5����_F2����:
                    if (SoundValue <= 90)
                    {
                        SoundValue += 10;
                    }

                    break;
                case FunMenuButtonId.F5����_F3����:
                    if (LightValue < TopAdorner.MaxLightValue)
                    {
                        LightValue += 10;
                    }
                    break;
                case FunMenuButtonId.������ʼ_���κ�:
                    MenuID = FunMenuButtonId.������ʼ_˾����;
                    if (DriverId == "0")
                    {
                        DriverId = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    StateProvider.MenuValueArrDict[(int) FunMenuButtonId.������ʼ_���κ�][3].ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F4�ȼ�_F1CTCS3_�绰����:
                    if (RBCID == "0")
                    {
                        RBCID = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    MenuID = FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC;
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
            }
        }

        private void ResponseF2()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.δѡ��:
                    RefreshSelectedModel();
                    MenuID = FunMenuButtonId.F2ģʽ;
                    break;
                case FunMenuButtonId.F1����:
                    MenuID = FunMenuButtonId.F1����_F2���κ�;
                    if (TrainId == "0")
                    {
                        TrainId = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F2ģʽ:
                    //MenuID = FunMenuButtonId.F2ģʽ_F2����Ŀ��;
                    SelectModelControlModel(TrainMode.Ŀ��);
                    break;
                case FunMenuButtonId.F3��Ƶ:
                    MenuID = FunMenuButtonId.F3��Ƶ_F2����;
                    break;
                case FunMenuButtonId.F4�ȼ�:
                    //����Ϣ�б����һ����Ϣ

                    OnAppendPostCmd(CmdType.SetBoolValue,
                        m_IsLoadC2
                            ? ((AppStartUp[1] && m_BrakeTest)
                                ? m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub��������ɺ�ȷ��CTCS2)
                                : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oubȷ��CTCS2))
                            : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oubȷ��CTCS21), 1, 0);

                    Buttons[0].IsOutlineVisible = false;
                    Buttons[1].IsOutlineVisible = false;
                    break;
                case FunMenuButtonId.F5����:
                    MenuID = FunMenuButtonId.F5����_F2����;
                    break;
                case FunMenuButtonId.F5����_F2����:
                    if (SoundValue >= 10)
                    {
                        SoundValue -= 10;
                    }
                    break;
                case FunMenuButtonId.F5����_F3����:
                    if (LightValue >= TopAdorner.MinLightValue)
                    {
                        LightValue -= 10;
                    }
                    break;
                case FunMenuButtonId.������ʼ_˾����:
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    MenuID = FunMenuButtonId.������ʼ_���κ�;
                    if (TrainId == "0")
                    {
                        TrainId = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) FunMenuButtonId.������ʼ_���κ�][1].ContentStr = DriverId;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC:
                    if (TelNmub == "0")
                    {
                        TelNmub = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    MenuID = FunMenuButtonId.F4�ȼ�_F1CTCS3_�绰����;
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
            }
        }

        private void ResponseF3()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.δѡ��:
                    MenuID = FunMenuButtonId.F3��Ƶ;
                    break;
                case FunMenuButtonId.F1����:
                    MenuID = FunMenuButtonId.F1����_F3�г�����;
                    break;
                case FunMenuButtonId.F2ģʽ:
                    SelectModelControlModel(TrainMode.����);
                    break;
                case FunMenuButtonId.F5����:
                    MenuID = FunMenuButtonId.F5����_F3����;
                    break;
            }
        }

        private void ResponseF4()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.δѡ��:
                    MenuID = FunMenuButtonId.F4�ȼ�;
                    if (m_TheSignalSystem == SignalSystem.CTCS3)
                    {
                        Buttons[0].IsOutlineVisible = true;
                    }
                    else if (m_TheSignalSystem == SignalSystem.CTCS2)
                    {
                        Buttons[1].IsOutlineVisible = true;
                    }
                    break;
                case FunMenuButtonId.F1����:
                    //��Ϣ�б����Ϸ�
                    if (MsgListCouldUpOrDown[0] && ShowFirstInMsgList > 0)
                    {
                        ShowFirstInMsgList--;
                    }
                    break;

            }
        }

        private void ResponseF5()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.δѡ��:
                    MenuID = FunMenuButtonId.F5����;
                    break;
                case FunMenuButtonId.F1����:
                    //��Ϣ�б����·�
                    if (MsgListCouldUpOrDown[1])
                    {
                        ShowFirstInMsgList++;
                    }
                    break;

            }
        }

        private void ResponseF6()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.δѡ��:
                    RequestStart();
                    break;

                    #region :::::::: ˾����

                case FunMenuButtonId.F1����_F1˾����:
                    if (!string.IsNullOrEmpty(DriverId))
                    {
                        MenuID = FunMenuButtonId.F1����_F1˾����_F6ȷ��;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    }
                    break;
                case FunMenuButtonId.F1����_F1˾����_F6ȷ��:
                    //��Ⲣ����˾����,���κŲ�Ϊ��Ҳ����
                    SetDriverIDorTrainId(DriverId, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf˾������ĸ),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf˾��������));
                    MenuID = FunMenuButtonId.F1����;
                    break;
                case FunMenuButtonId.������ʼ_˾����:
                    DriverId = string.Empty;
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    if (!string.IsNullOrEmpty(DriverId))
                    {
                        MenuID = FunMenuButtonId.������ʼ_���κ�;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                        TrainId = string.Empty;
                        StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    }
                    break;

                    #endregion

                    #region :::::::: ���κ�

                case FunMenuButtonId.F1����_F2���κ�:
                    if (!string.IsNullOrEmpty(TrainId))
                    {
                        MenuID = FunMenuButtonId.F1����_F2���κ�_F6ȷ��;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainId;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.F1����_F1˾����;
                        DriverId = string.Empty;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    }
                    break;
                case FunMenuButtonId.F1����_F2���κ�_F6ȷ��:
                    //��Ⲣ���ͳ��κţ�˾���Ų�Ϊ��Ҳ����
                    SetDriverIDorTrainId(TrainId, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf���κ���ĸ),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf���κ�����));
                    MenuID = FunMenuButtonId.F1����;
                    break;
                case FunMenuButtonId.������ʼ_���κ�:
                    if (!string.IsNullOrEmpty(DriverId) && !string.IsNullOrEmpty(TrainId))
                    {
                        StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                        MenuID = FunMenuButtonId.������ʼ_˾�����κ�ȷ��;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                        StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.������ʼ_˾����;
                        DriverId = string.Empty;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    }
                    break;
                case FunMenuButtonId.������ʼ_˾�����κ�ȷ��:
                    MenuID = FunMenuButtonId.δѡ��;
                    SetDriverIDorTrainId(DriverId, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf˾������ĸ),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf˾��������));
                    SetDriverIDorTrainId(TrainId, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf���κ���ĸ),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf���κ�����));
                    if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbCTCS3)])
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub4858), 1, 0);
                    }
                    else
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue,
                            m_AppStartData[0]
                                ? m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oubִ���ƶ�����4)
                                : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oubִ���ƶ�����3), 1,
                            0);
                    }

                    //ִ���ƶ�����
                    break;

                    #endregion

                    #region :::::::: �г�����

                case FunMenuButtonId.F1����_F3�г�����:
                    try
                    {
                        if (Convert.ToInt32(TrainData) == 1 || Convert.ToInt32(TrainData) == 2)
                        {
                            MenuID = FunMenuButtonId.F1����_F3�г�����_F6ȷ��;
                            StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainData;
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                    break;
                case FunMenuButtonId.F1����_F3�г�����_F6ȷ��:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2])
                    {

                        MenuID = FunMenuButtonId.F3��Ƶ;
                        m_IsLoadC2 = true;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.F1����;
                    }
                    //�����г�����
                    SetDriverIDorTrainId(TrainData, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf�г�����),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf�г�����));
                    break;

                    #endregion

                    #region :::::::: ģʽ

                case FunMenuButtonId.F2ģʽ_F1�������:
                    //���͵���ģʽ��־
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub����ģʽ), 1, 0);
                    MenuID = FunMenuButtonId.δѡ��;
                    ChangeMode(0);
                    ModeSelect[0] = true;
                    //BtnStrDict[2][0] = "�˳�\n����";
                    //m_MenuValueArrDict[2][0].ContentStr = "�˳�����ģʽ";
                    break;
                case FunMenuButtonId.F2ģʽ_F1�˳�����:
                    //���͵���ģʽ��־
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub�˳�����ģʽ), 1, 0);
                    MenuID = FunMenuButtonId.δѡ��;
                    ModeSelect[0] = false;
                    //BtnStrDict[2][0] = "����";
                    //m_MenuValueArrDict[2][0].ContentStr = "����ģʽ";
                    break;
                case FunMenuButtonId.F2ģʽ_F2����Ŀ��:
                    //����Ŀ��ģʽ��־
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.OubĿ��ģʽ), 1, 0);
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F2ģʽ_F2�˳�Ŀ��:
                    //����Ŀ��ģʽ��־
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub�˳�Ŀ��ģʽ), 1, 0);
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F2ģʽ_F3�������:
                    //���ͻ���ģʽ��־
                    //ChangeMode(2);
                    // ModeSelect[2] = true;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub����ģʽ), 1, 0);
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F2ģʽ_F3�˳�����:
                    //���ͻ���ģʽ��־
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub�˳�����ģʽ), 1, 0);
                    MenuID = FunMenuButtonId.δѡ��;
                    break;

                    #endregion

                    #region :::::::: ��Ƶ

                case FunMenuButtonId.F3��Ƶ_F1����:
                    MenuID = FunMenuButtonId.F3��Ƶ_F1����_ȷ��;
                    Buttons[0].IsOutlineVisible = true;
                    //�˴���Ӧλ��Ҫ�ӿ�
                    break;
                case FunMenuButtonId.F3��Ƶ_F2����:
                    MenuID = FunMenuButtonId.F3��Ƶ_F2����_ȷ��;
                    Buttons[1].IsOutlineVisible = true;
                    //�˴���Ӧλ��Ҫ�ӿ�
                    break;

                    #endregion

                    #region :::::::: �ȼ�

                case FunMenuButtonId.F4�ȼ�_F1CTCS3:
                    //����Ӧ��Ϣ��־Ϊ����
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F4�ȼ�_F2CTCS2:
                    //����Ӧ��Ϣ��־Ϊ����
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC:
                    if (TelNmub == "0")
                    {
                        TelNmub = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    MenuID = FunMenuButtonId.F4�ȼ�_F1CTCS3_�绰����;
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
                case FunMenuButtonId.F4�ȼ�_F1CTCS3_�绰����:
                    int tmpCTCS;
                    if (int.TryParse(RBCID, out tmpCTCS) && int.TryParse(TelNmub, out tmpCTCS))
                    {
                        MenuID = FunMenuButtonId.F4�ȼ�_F2CTCS3_RBCȷ��;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                        StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    }
                    break;
                case FunMenuButtonId.F4�ȼ�_F2CTCS3_RBCȷ��:

                    SendNumber(
                        new[]
                        {

                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.OufRBCID2),
                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.OufRBCID3),
                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.OufRBCID1),
                        }, RBCID);
                    SendNumber(
                        new[]
                        {

                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf�绰����2),
                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf�绰����3),
                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf�绰����1),
                        }, TelNmub);


                    //OnAppendPostCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.OufRBCID1), 0, FormatConversion(RBCID));
                    //OnAppendPostCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf�绰����1), 0, FormatConversion(TelNmub));
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub�绰���뷢��ȷ��), 1, 0);
                    // OnAppendPostCmd(CmdType.SetBoolValue, m_BoolSendList[11], 1, 0);

                    if (AppStartUp[0] && !AppStartUp[1])
                    {
                        MenuID = FunMenuButtonId.F1����_F3�г�����;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.δѡ��;
                    }
                    break;

                    #endregion

                    //case FunMenuButtonID.������ʼ_���еȼ�:
                //    SpecialMsgEndThenSendValueAndJumpView();
                //    break;

                case FunMenuButtonId.F5�ƶ�����:
                    //�����ƶ����Ա�־λ
                    //����Ӧ��Ϣ��־Ϊ����
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F6����:
                    SendStartFlag();
                    break;
                case FunMenuButtonId.F7����:
                    MenuID = FunMenuButtonId.δѡ��;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub������), 1, 0);
                    break;
                case FunMenuButtonId.ȷ��ȡ��:
                    if (MsgHandlerATPList.SpecialMsgList.Count != 0)
                    {
                        SpecialMsgEndThenSendValueAndJumpView(true);
                    }
                    break;
            }
        }

        private void ResponseF7()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.δѡ��:
                    RequestRelaseBrake();
                    break;
                case FunMenuButtonId.F1����_F1˾����:
                    DriverId = Backspace(DriverId);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    break;
                case FunMenuButtonId.F1����_F2���κ�:
                    TrainId = Backspace(TrainId);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F1����_F3�г�����:
                    TrainData = Backspace(TrainData);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainData;
                    break;
                case FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC:
                    RBCID = Backspace(RBCID);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    break;
                case FunMenuButtonId.F4�ȼ�_F1CTCS3_�绰����:
                    TelNmub = Backspace(TelNmub);
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
                case FunMenuButtonId.������ʼ_˾����:
                    DriverId = Backspace(DriverId);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    break;
                case FunMenuButtonId.������ʼ_���κ�:
                    TrainId = Backspace(TrainId);
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    break;
            }
        }

        private void ResponseF8()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.δѡ��:
                    SendVigilantFlag();
                    break;

                    #region ::::::::::F1

                case FunMenuButtonId.������ʼ_˾����:
                    if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbCTCS3)])
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub4858), 1, 0);
                    }
                    else
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue,
                            m_AppStartData[0]
                                ? m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oubִ���ƶ�����4)
                                : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oubִ���ƶ�����3), 1,
                            0);
                    }
                    break;
                case FunMenuButtonId.F1����:
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F1����_F1˾����:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2])
                    {
                        MenuID = FunMenuButtonId.F1����;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.δѡ��;
                    }
                    break;
                case FunMenuButtonId.F1����_F1˾����_F6ȷ��:
                    MenuID = FunMenuButtonId.F1����_F1˾����;
                    break;
                case FunMenuButtonId.F1����_F2���κ�:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2])
                    {
                        MenuID = FunMenuButtonId.F1����;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.δѡ��;
                    }
                    break;
                case FunMenuButtonId.F1����_F2���κ�_F6ȷ��:
                    MenuID = FunMenuButtonId.F1����_F2���κ�;
                    break;
                case FunMenuButtonId.F1����_F3�г�����:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2])
                    {
                        MenuID = FunMenuButtonId.F1����;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.δѡ��;
                    }
                    break;
                case FunMenuButtonId.F1����_F3�г�����_F6ȷ��:
                    MenuID = FunMenuButtonId.F1����_F3�г�����;
                    break;
                //case FunMenuButtonId.������ʼ_˾����:
                //    MenuID = FunMenuButtonId.δѡ��;
                //    _appStartUp[2] = true;//���������ж�
                //    break;
                //case FunMenuButtonId.������ʼ_���κ�:
                //    MenuID = FunMenuButtonId.δѡ��;
                //    _appStartUp[2] = true;//���������ж�
                //    break;
                case FunMenuButtonId.������ʼ_˾�����κ�ȷ��:
                    MenuID = FunMenuButtonId.������ʼ_���κ�;
                    break;

                    #endregion

                    #region ::::::::::F2

                case FunMenuButtonId.F2ģʽ:
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F2ģʽ_F1�������:
                case FunMenuButtonId.F2ģʽ_F1�˳�����:
                case FunMenuButtonId.F2ģʽ_F2����Ŀ��:
                case FunMenuButtonId.F2ģʽ_F2�˳�Ŀ��:
                case FunMenuButtonId.F2ģʽ_F3�������:
                case FunMenuButtonId.F2ģʽ_F3�˳�����:
                    MenuID = FunMenuButtonId.F2ģʽ;
                    break;

                    #endregion

                    #region ::::::::: F3

                case FunMenuButtonId.F3��Ƶ:
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F3��Ƶ_F1����:
                    MenuID = FunMenuButtonId.F3��Ƶ;
                    break;
                case FunMenuButtonId.F3��Ƶ_F1����_ȷ��:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2] && m_BrakeTest)
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub4856), 1, 0);
                        AppStartUp[0] = false;
                        AppStartUp[1] = true;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.δѡ��;
                    }
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub��Ƶ����), 1, 0);
                    //������1
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub��Ƶ����), 0, 0);
                    //������0
                    Buttons[0].IsOutlineVisible = false;
                    //�������б�־
                    break;
                case FunMenuButtonId.F3��Ƶ_F2����:
                    MenuID = FunMenuButtonId.F3��Ƶ;
                    break;
                case FunMenuButtonId.F3��Ƶ_F2����_ȷ��:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2] && m_BrakeTest)
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub4856), 1, 0);
                        AppStartUp[0] = false;
                        AppStartUp[1] = true;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.δѡ��;
                    }
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub��Ƶ����), 0, 0);
                    //������0
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub��Ƶ����), 1, 0);
                    //������1
                    Buttons[1].IsOutlineVisible = false;
                    //�������б�־
                    break;

                    #endregion

                    #region ::::::::: F4

                case FunMenuButtonId.F4�ȼ�:
                    if (OldMenuId == FunMenuButtonId.ȷ��ȡ�� && !AppStartUp[1])
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oubȷ��CTCS21),
                            1, 0); //ȷ��CTCS2
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.δѡ��;

                    }
                    Buttons[0].IsOutlineVisible = false;
                    Buttons[1].IsOutlineVisible = false;
                    break;

                case FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC:
                    MenuID = FunMenuButtonId.F4�ȼ�;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.OubRBC����ȡ��), 1, 0);
                    break;

                case FunMenuButtonId.F4�ȼ�_F1CTCS3_�绰����:
                    StateProvider.MenuValueArrDict[(int) FunMenuButtonId.F4�ȼ�_F1CTCS3_RBC][3].ContentStr = TelNmub;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.OubRBC����ȡ��), 1, 0);
                    MenuID = FunMenuButtonId.F4�ȼ�;
                    break;

                case FunMenuButtonId.F4�ȼ�_F2CTCS3_RBCȷ��:
                    MenuID = FunMenuButtonId.F4�ȼ�_F1CTCS3_�绰����;
                    break;

                case FunMenuButtonId.F4�ȼ�_F1CTCS3:
                    MenuID = FunMenuButtonId.F4�ȼ�;
                    break;

                case FunMenuButtonId.F4�ȼ�_F2CTCS2:
                    MenuID = FunMenuButtonId.F4�ȼ�;
                    break;

                    #endregion

                    #region ::::::::: F5

                case FunMenuButtonId.F5����:
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.F5����_F2����:
                    MenuID = FunMenuButtonId.F5����;
                    break;
                case FunMenuButtonId.F5����_F3����:
                    MenuID = FunMenuButtonId.F5����;
                    break;

                    #endregion

                    #region ::::::::: F6

                case FunMenuButtonId.F6����:
                    MenuID = FunMenuButtonId.δѡ��;
                    break;

                    #endregion

                case FunMenuButtonId.F7����:
                    MenuID = FunMenuButtonId.δѡ��;
                    break;
                case FunMenuButtonId.ȷ��:
                    if (MsgHandlerATPList.SpecialMsgList.Count != 0)
                    {
                        //����ȷ��
                        if (MsgHandlerATPList.SpecialMsgList[0].MsgID == "9")
                        {
                            AppStartUp[1] = true;
                        }
                        SpecialMsgEndThenSendValueAndJumpView(true);
                    }
                    break;

                case FunMenuButtonId.ȷ��ȡ��:

                    if (MsgHandlerATPList.SpecialMsgList.Count != 0)
                    {
                        SpecialMsgEndThenSendValueAndJumpView(false);
                    }
                    break;
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private void ResponseNumbers(int btnId, MouseState state)
        {
            switch (btnId)
            {
                case 8: //1 
                    GetValueOfTrainInfo("", 1);
                    return;
                case 9: //2 ABC
                    GetValueOfTrainInfo("ABC", 2);
                    return;
                case 10: //3 DEF
                    GetValueOfTrainInfo("DEF", 3);
                    return;
                case 11: //4 GHI
                    GetValueOfTrainInfo("GHI", 4);
                    return;
                case 12: //5 JKL
                    GetValueOfTrainInfo("JKL", 5);
                    return;
                case 13: //6 MNO
                    GetValueOfTrainInfo("MNO", 6);
                    return;
                case 14: //7 PQRS
                    GetValueOfTrainInfo("PQRS", 7);
                    return;
                case 15: //8 TUV
                    GetValueOfTrainInfo("TUV", 8);
                    return;
                case 16: //9 WXYZ
                    GetValueOfTrainInfo("WXYZ", 9);
                    return;
                case 17: //0
                    GetValueOfTrainInfo("", 0);
                    return;
                case 18: //������ĸ�л�
                    if (MenuID == FunMenuButtonId.F1����_F2���κ� ||
                        MenuID == FunMenuButtonId.������ʼ_���κ�)
                    {
                        m_SwitchOver = !m_SwitchOver;
                    }
                    else if (MenuID == FunMenuButtonId.δѡ��)
                    {
                        SendVigilantFlag();
                    }
                    return;
            }
        }

        /// <summary>
        /// �˸�
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        private string Backspace(string st)
        {
            if (!string.IsNullOrEmpty(st))
            {
                st = st.Remove(st.Length - 1);
            }

            return st;
        }

        private void SendNumber(int[] iPara, string sPara)
        {
            if (string.IsNullOrEmpty(sPara))
            {
                return;
            }
            var chararr = new char[12];
            sPara.CopyTo(0, chararr, 0, sPara.Length);
            var sf = new StringFloatConverter();
            var rt = sf.ConvertCharArray(chararr);
            var tmp = sf.ConvertFloatArray(rt);
            OnAppendPostCmd(CmdType.SetFloatValue, iPara[0], 0, rt[0]);
            OnAppendPostCmd(CmdType.SetFloatValue, iPara[1], 0, rt[1]);
            OnAppendPostCmd(CmdType.SetFloatValue, iPara[2], 0, rt[2]);

            AppLog.Info("��������Ϊ��" + sPara);
            AppLog.Info(string.Format("��������λ{0}��{1}", iPara[0], rt[0]));
            AppLog.Info(string.Format("��������λ{0}��{1}", iPara[1], rt[1]));
            AppLog.Info(string.Format("��������λ{0}��{1}", iPara[2], rt[2]));
            AppLog.Info("�������ݽ��룺" +
                        Encoding.ASCII.GetString(tmp.Select(s => BitConverter.GetBytes(s)[0]).ToArray()));
        }


        public void ClearButtonOutline()
        {
            foreach (var btn in Buttons)
            {
                btn.IsOutlineVisible = false;
            }
        }

        private void SendStartFlag()
        {
            //����������־λ
            if (OnAppendPostCmd != null)
            {
                OnAppendPostCmd(CmdType.SetBoolValue,
                    m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub����ȷ��û����Ϣ��ʾ��ȷ��), 1, 0);
            }
            //����Ӧ��Ϣ��־Ϊ����
            MenuID = FunMenuButtonId.δѡ��;
        }

        private void SendVigilantFlag()
        {
            //����
            if (OnAppendPostCmd != null)
            {
                OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub���谴ť), 1, 0);
            }
            return;
        }

        private void RequestRelaseBrake()
        {
            //���ͻ����־
            if (m_AppRunData[0])
            {
                MenuID = FunMenuButtonId.F7����;
            }
        }

        private void RequestStart()
        {
            if (m_CanStartUp)
            {
                MenuID = FunMenuButtonId.F6����;
            }
        }

        private void RefreshButtonEnableableState()
        {
            foreach (var buttonBar in Buttons)
            {
                // need �Ż�
                var target = string.Format("{0}�����Ƿ�ʧЧ", buttonBar.Content);
                if (m_ATPMainScreen.IndexDescriptionConfig.InBoolDescriptionDictionary.ContainsKey(target))
                {
                    buttonBar.IsEnable = !m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(target)];
                }
                else
                {
                    buttonBar.IsEnable = true;
                }
            }
        }

        private void RefreshValue()
        {
            m_Progresses[0].CurrentValue = SoundValue;
            m_Progresses[1].CurrentValue = LightValue;
        }


        // ReSharper disable once InconsistentNaming
        protected ATPMainScreen m_ATPMainScreen;
        // ReSharper disable once InconsistentNaming
        protected bool m_IsLoadC2;
        // ReSharper disable once InconsistentNaming
        protected List<PictrueAndProgress> m_Progresses;
        private readonly Timer m_ClearSoundTimer;


        public string CurrentInfo
        {
            get { return m_CurrentInfo; }
            private set
            {
                if (m_CurrentInfo == value)
                {
                    return;
                }

                m_CurrentInfo = value;
                if (string.IsNullOrEmpty(m_CurrentInfo))
                {
                    ClearSound();
                }
                else
                {
                    SendSound();
                }
            }
        }

        public OpenFunBtnCTCS300T(ATPMainScreen mainScreen, Image[] imgArr)
        {
            m_IsLoadC2 = false;
            m_ATPMainScreen = mainScreen;
            Buttons = new ButtonBar[8];
            TrainId = "0";
            ATPCurrentType = ATP_Type.C300T;
            LightValue = TopAdorner.MaxLightValue;
            SoundValue = 50;
            TrainData = "2";
            TelNmub = "0";
            RBCID = "0";
            DriverId = "0";
            RectList = Coordinate.RectangleFLists(View_ID_Name.OpenFunBtn);
            m_ImgsArr = imgArr;
            MsgHandlerATPList = new MsgHandlerATP<Message>();
            MsgCatchList = new List<int>();
            m_StateProvider = new StateProvider(this);

            Init();
            if (RectList.Count < 8)
            {
                AppLog.Info("RectList.Count < 8 == false, Can not parser rect, unkown...");
                return;
            }

            for (var i = 0; i < 8; i++)
            {
                Buttons[i] = new ButtonBar(RectList[i], StateProvider.BtnStrDict[0][i], false);
            }
            m_ClearSoundTimer = new Timer(ClearSound);
            CurrentInfo = string.Empty;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void Init()
        {
            m_CanStartUp = false;

            m_SwitchOver = false;

            m_MenuId = FunMenuButtonId.δѡ��;

            m_TheSignalSystem = SignalSystem.CTCS3;

            ModeSelect = new[] {false, false, false};

            AppStartUp = new[] {false, false, false};

            MsgHandlerATPList.ClearAllList();

            MsgCatchList.Clear();

            ShowFirstInMsgList = 0;

            MsgListCouldUpOrDown = null;
            MsgListCouldUpOrDown = new[] {false, false};

            m_BrakeTest = false;

            TrainId = "0";
            DriverId = "0";
            NetId = "0000";
            RBCID = "0";
            TelNmub = "0";
            TrainData = "2";
            SoundValue = 50;
            LightValue = TopAdorner.MaxLightValue;

            var tem = new RectangleF(445, 180, 240, 30);
            m_Progresses = new List<PictrueAndProgress>()
            {
                new PictrueAndProgress(m_ATPMainScreen, new[] {m_ImgsArr[5]}, new[] {tem}, 100)
                {
                    BackGroundColor = Color.FromArgb(51, 50, 53),
                    Interval = 15,
                },
                new PictrueAndProgress(m_ATPMainScreen, new[] {m_ImgsArr[4]}, new[] {tem}, 100)
                {
                    BackGroundColor = Color.FromArgb(51, 50, 53),
                    Interval = 15
                }
            };


            StateProvider.InitalizeStates();
        }

        #region :::::::::::::::::: value init :::::::::::::::::::

        #region :::::::::::::::::ȫ����

        public AppraiseControl AppraiseControl { get; set; }

        //���о��ο��б�
        public List<RectangleF> RectList { private set; get; }

        /// <summary>
        /// ��һ�εĲ˵����
        /// </summary>
        public FunMenuButtonId OldMenuId { get; private set; }


        public ButtonBar[] Buttons { get; private set; }

        /// <summary>
        /// ����������ĸ��ť
        /// </summary>
        private readonly KeyBoard m_Keyboard = new KeyBoard(5);

        //������ĸ�л�����
        private bool m_SwitchOver;

        //menu���ݴʵ䣬����MenuID�仯

        /// <summary>
        /// ����ģʽ��Ŀ��ģʽ������ģʽת��
        /// </summary>
        public bool[] ModeSelect { get; set; }

        private readonly Image[] m_ImgsArr;

        #endregion

        #region :::::::::::::: ��������������

        /// <summary>
        /// ��������
        /// �ֱ��ʾ ��ʼ���������ж�
        /// </summary>
        public bool[] AppStartUp { get; set; }

        /// <summary>
        /// ����������������Ҫ������
        /// ��һ����ʾ�ϴγɹ������ƶ����Ե�ʱ���Ƿ���30Сʱ����
        /// </summary>
        private readonly bool[] m_AppStartData = new bool[10];

        /// <summary>
        /// �������й�������Ҫ������
        /// ��һ����ʾ������
        /// �ڶ�����ʾ����
        /// </summary>
        private readonly bool[] m_AppRunData = new bool[10];

        /// <summary>
        /// �ƶ�����
        /// </summary>
        private bool m_BrakeTest;

        /// <summary>
        /// ��������
        /// </summary>
        private bool m_CanStartUp;

        #endregion

        #region ::::::::::::::::::::: ��Ϣ�б�

        public MsgHandlerATP<Message> MsgHandlerATPList { set; get; }

        /// <summary>
        /// ��ʾ�ĵ�һ����Ϣ����Ϣ�б��λ��
        /// </summary>
        public int ShowFirstInMsgList { get; private set; }

        /// <summary>
        /// ��Ϣ�б���������ͼ����ʾ
        /// </summary>
        public bool[] MsgListCouldUpOrDown { get; private set; }

        /// <summary>
        /// ��ǰ��ȡ����Ϣ�����б�
        /// </summary>
        public List<int> MsgCatchList { get; set; }

        #endregion

        #region ::::::::::::::::::::: ��������

        /// <summary>
        /// ���κ�
        /// </summary>
        public string TrainId { get; set; }

        /// <summary>
        /// ˾����
        /// </summary>
        public string DriverId { get; set; }

        /// <summary>
        /// RBC����
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string RBCID { get; set; }

        /// <summary>
        /// �绰����
        /// </summary>
        public string TelNmub { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string TrainData { get; set; }

        /// <summary>
        ///  ����ID
        /// </summary>
        public string NetId { set; get; }

        //����

        /// <summary>
        /// ����
        /// </summary>
        public int SoundValue
        {
            get { return m_SoundValue; }
            set
            {
                m_SoundValue = value;
                if (AppraiseControl != null)
                {
                    AppraiseControl.Voloumn = value;
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int LightValue { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// ������ҳ��
        /// </summary>
        public List<FunMenuButtonId> m_AppraiseList;

        private bool Appraise
        {
            set
            {
                if (value == m_Appraise)
                {
                    return;
                }
                m_Appraise = value;
                OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.OubATP����˾�����������),
                    m_Appraise ? 1 : 0, 0);
            }
        }

        public event AppendPostCmd OnAppendPostCmd;

        public bool Painting { get; set; }

        protected FunMenuButtonId m_MenuId = FunMenuButtonId.δѡ��;

        /// <summary>
        /// ��ǰ���ڲ˵����
        /// </summary>
        public FunMenuButtonId MenuID
        {
            get { return m_MenuId; }
            set
            {
                if (m_MenuId == value)
                {
                    return;
                }
                OldMenuId = MenuID;
                m_MenuId = value;
                ChangeBtnState(m_MenuId);
            }
        }

        protected SignalSystem m_TheSignalSystem = SignalSystem.CTCS3;
        private int m_SoundValue;
        private bool m_Appraise;
        private string m_CurrentInfo;
        private readonly StateProvider m_StateProvider;

        public ATP_Type ATPCurrentType { get; set; }

        public StateProvider StateProvider
        {
            get { return m_StateProvider; }
        }

        #region :::::::::::::: ��Ϣ�б����� :::::::::::::::::

        /// <summary>
        /// ������Ϣ�б�
        /// </summary>
        /// <param name="g"></param>
        public void DrawMsgList(Graphics g)
        {
            if (MsgHandlerATPList.SpecialMsgList.Any())
            {
                //��˸����Ϣ
                g.DrawString(
                    MsgHandlerATPList.SpecialMsgList[0].MsgReceiveTime.ToLongTimeString() + " " +
                    MsgHandlerATPList.SpecialMsgList[0].MsgContent,
                    FontsItems.Font12YouB, SolidBrushsItems.WhiteBrush,
                    RectList[50], FontsItems.TheAlignment(FontRelated.����));
                if (MsgHandlerATPList.SpecialMsgList[0].MsgContent.Equals("ִ���ƶ�����") ||
                    MsgHandlerATPList.SpecialMsgList[0].MsgContent.Equals("ȷ��CTCS-2"))
                {
                    CurrentInfo = MsgHandlerATPList.SpecialMsgList[0].MsgContent;
                }
                else
                {
                    CurrentInfo = string.Empty;
                }


                if (m_ATPMainScreen.CurrentTime.Second%2 == 0)
                {
                    g.DrawRectangle(PenItems.YellowPen2, Rectangle.Round(RectList[50]));
                }
            }
            else
            {
                CurrentInfo = string.Empty;
            }
            if (MsgHandlerATPList.AllMsgsList.Any())
            {
                //���Ʋ�����˸����Ϣ
                DrawSortOfLevelMsgList(g, MsgHandlerATPList.SpecialMsgList.Count != 0, ShowFirstInMsgList);
            }
        }

        private void DrawSortOfLevelMsgList(Graphics g, bool beLevelMsg, int idInMsgList)
        {
            for (var i = idInMsgList;
                i < (TheCursorCanDown(beLevelMsg, idInMsgList)
                    ? ((beLevelMsg ? 3 : 4) + idInMsgList)
                    : MsgHandlerATPList.AllMsgsList.Count);
                i++)
            {
                g.DrawString(
                    MsgHandlerATPList.AllMsgsList[i].MsgReceiveTime.ToLongTimeString() + " " +
                    MsgHandlerATPList.AllMsgsList[i].MsgContent,
                    FontsItems.Font12YouB,
                    beLevelMsg
                        ? SolidBrushsItems.DarkGrayBrush
                        : i == idInMsgList ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.DarkGrayBrush,
                    RectList[(beLevelMsg ? 51 : 50) + i - idInMsgList],
                    FontsItems.TheAlignment(FontRelated.����));
            }
        }

        private bool TheCursorCanDown(bool beLevelMsg, int idInMsgList)
        {
            return MsgHandlerATPList.AllMsgsList.Count - idInMsgList > (beLevelMsg ? 3 : 4);
        }

        #endregion

        public void Update(ATPBaseClass source)
        {
            //��������
            if (source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb��������������)] && !m_CanStartUp)
            {
                OnAppendPostCmd(CmdType.SetBoolValue, source.GetOutBoolIndex(OutBoolKeys.Oub4856), 1, 0);
                m_CanStartUp = true;
            }


            m_BrakeTest = source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb�ƶ����Գɹ�)]; //�ƶ����Գɹ���־

            if (!source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb��ʻ��δ����)] && !AppStartUp[0] && !AppStartUp[1])
                //��ʻ�Ҽ���
            {
                MenuID = FunMenuButtonId.������ʼ_˾����;
                if (DriverId == "0")
                {
                    DriverId = string.Empty;
                }
                StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                AppStartUp[0] = true;
            }

            m_AppStartData[0] = source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb�ϴγɹ������ƶ����Ե�ʱ���Ƿ���30Сʱ����)];
            //�ϴγɹ������ƶ����Ե�ʱ���Ƿ���30Сʱ����

            m_AppRunData[0] = source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb������1)]; //������
            m_AppRunData[1] = source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb���谴��)]; //���谴ť

            //
            foreach (var t in MsgHandlerATPList.AllMsgsList.Where(
                t => t.TheSpecialHanding == SpecialHanding.һ���Ӻ��Զ��������������� &&
                     !t.FaultBeOver && (m_ATPMainScreen.CurrentTime.Minute == 0 ||
                                        m_ATPMainScreen.CurrentTime.Minute - t.MsgReceiveTime.Minute == 1)))
            {
                if (t.SendValueToCpu[0][0] != 0)
                {
                    foreach (var t1 in t.SendValueToCpu[0])
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, Math.Abs(t1),
                            t1 > 0 ? 1 : 0, 0);
                    }
                }
                MenuID = (FunMenuButtonId) t.MsgOverThenJumpViewArr[0];
                MsgHandlerATPList.MsgOver(t.MsgLogicID, m_ATPMainScreen.CurrentTime);
                break;
            }
        }

        public void ClearData()
        {
            Init();
        }

        public void Paint(Graphics g)
        {
            RefreshValue();

            //ˢ�°���״̬
            RefreshButtonEnableableState();

            DrawStateView(g);

            //�л������ַ�
            ResponseSwitchInputModel();

        }

        private void DrawStateView(Graphics g)
        {
            //��ť״̬
            DrawButtons(g);

            //������ 
            DrawPopView(g);

            //��Ϣ
            DrawMsgList(g);

            //���·�ҳ
            DrawTurnPageView(g);
        }

        /// <summary>
        /// �����һ��������ʾ��
        /// </summary>
        private void ClearSound(object obj = null)
        {
            if (m_ATPMainScreen.OutBoolList[61])
            {
                OnAppendPostCmd(CmdType.SetBoolValue, 61, 0, 0);
            }
        }

        /// <summary>
        /// ������һ��������ʾ��
        /// </summary>
        private void SendSound()
        {
            OnAppendPostCmd(CmdType.SetBoolValue, 61, 1, 0);
            m_ClearSoundTimer.Change(1000, int.MaxValue);
        }

        private void DrawButtons(Graphics g)
        {
            for (var index = 0; index < 8; index++)
            {
                Buttons[index].OnPaint(g);
            }
        }

        /// <summary>
        /// �л������ַ�
        /// </summary>
        private void ResponseSwitchInputModel()
        {
            if (m_SwitchOver && m_Keyboard.TimeOut)
            {
                m_Keyboard.GetKeyValue(true);
            }
        }

        private void DrawTurnPageView(Graphics g)
        {
            if (MsgHandlerATPList.AllMsgsList.Count > 0)
            {
                MsgListCouldUpOrDown[0] = ShowFirstInMsgList > 0;
                MsgListCouldUpOrDown[1] = (MsgHandlerATPList.AllMsgsList.Count - ShowFirstInMsgList >
                                           (MsgHandlerATPList.SpecialMsgList.Count != 0 ? 3 : 4)); //�·�
            }
            for (var index = 0; index < 2; index++)
            {
                g.DrawImage(MsgListCouldUpOrDown[index]
                    ? (index == 0 ? m_ImgsArr[0] : m_ImgsArr[2])
                    : (index == 0 ? m_ImgsArr[1] : m_ImgsArr[3]),
                    RectList[index == 0 ? 54 : 55]);
            }
        }

        /// <summary>
        /// �浯����
        /// </summary>
        /// <param name="g"></param>
        private void DrawPopView(Graphics g)
        {
            Appraise = m_AppraiseList.Contains(MenuID);
            //�˵�
            if (StateProvider.BtnStrDict[(int) MenuID].Length > 8)
            {
                g.FillRectangle(SolidBrushsItems.BtnDisabledBrush, RectList[8]);
                g.DrawString(StateProvider.BtnStrDict[(int) MenuID][8], FontsItems.Font16YouB,
                    SolidBrushsItems.WhiteBrush, RectList[8], FontsItems.TheAlignment(FontRelated.����));
                g.FillRectangle(SolidBrushsItems.BtnAbledBrush, RectList[9]);

                if (StateProvider.BtnStrDict[(int) MenuID].Length == 10)
                {
                    g.DrawString(StateProvider.BtnStrDict[(int) MenuID][9], FontsItems.Font17YouB,
                        SolidBrushsItems.WhiteBrush, RectList[9], FontsItems.TheAlignment(FontRelated.����));
                }
            }

            //���
            if (StateProvider.MenuValueArrDict.ContainsKey((int) MenuID))
            {
                foreach (var mv in StateProvider.MenuValueArrDict[(int) MenuID])
                {
                    mv.OnDraw(g);
                }
            }

            switch (MenuID) //����������
            {
                case FunMenuButtonId.F5����_F2����:
                    m_Progresses[0].OnDraw(g);
                    break;
                case FunMenuButtonId.F5����_F3����:
                    m_Progresses[1].OnDraw(g);
                    break;
            }
        }
    }
}