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
using Motor.ATP._300T.主屏;
using Motor.ATP._300T.共用.功能键与菜单.FunState;

namespace Motor.ATP._300T.共用.功能键与菜单
{
    public class OpenFunBtnCTCS300T : IOpennFunctionButton
    {
        /// <summary>
        /// 按钮状态变化
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
        /// 调车目视机信模式转换
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
        /// 刷新选中的模式
        /// </summary>
        private void RefreshSelectedModel()
        {
            if (m_ATPMainScreen.CurrentTrainMode == TrainMode.调车)
            {
                ModeSelect[0] = true;
            }
            else
            {
                ModeSelect[0] = false;
            }

            StateProvider.BtnStrDict[2][0] = ModeSelect[0] ? "退出\n调车" : "调车";
            StateProvider.MenuValueArrDict[2][0].ContentStr = ModeSelect[0] ? "退出调车模式" : "调车模式";
        }

        /// <summary>
        /// 设置司机号或者车次号
        /// </summary>
        /// <param name="id">司机号或者车次号</param>
        /// <param name="sendLetterInFloatList">司机号或者车次号中字母发送位</param>
        /// <param name="sendIdInFloatList">司机号或者车次号中数字发送位</param>
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
        /// 特殊消息发送数据与跳转
        /// </summary>
        /// <param name="confirm">是否确定</param>
        private void SpecialMsgEndThenSendValueAndJumpView(bool confirm)
        {
            int jumpId;
            if (confirm) //当前按的是确定键
            {
                //发送确定数据
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

                //跳转菜单
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
                    MenuID = FunMenuButtonId.未选择;
                }
            }
            else
            {
                //发送取消数据
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

                //跳转菜单
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
                    MenuID = FunMenuButtonId.未选择;
                }
            }
        }

        #region ::::::::::::::::::::::::::::: 下排数字字母键响应方法 ::::::::::::::::::::::::::::

        /// <summary>
        /// 获取按钮所表示的字母
        /// </summary>
        /// <param name="input">需要修改的值, 表示司机号或者车次号</param>
        /// <param name="nKeyWords">按钮表示的值，例如ABC</param>
        /// <param name="nNowKeyCode">按钮表示的值，例如2</param>
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
        /// 获取当前按下按钮后实际获取到的值，是数值或是某个字母
        /// </summary>
        /// <param name="value">需要修改的值, 表示司机号或者车次号</param>
        /// <param name="btnOfLetter">当前键表示的字母,例如"ABC"</param>
        /// <param name="btnOfNumb">当前按钮代表的数字</param>
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
        /// 根据相应menuID相应数字字母按钮
        /// 设置列车数据
        /// </summary>
        /// <param name="btnOfLetter">按钮代表的字母,例如"ABC"</param>
        /// <param name="btnOfNumb">按钮代表的数字</param>
        protected void GetValueOfTrainInfo(string btnOfLetter, int btnOfNumb)
        {
            switch (MenuID)
            {
                case FunMenuButtonId.启动开始_司机号:
                case FunMenuButtonId.F1数据_F1司机号:
                    DriverId = GetTrainIDorDriverId(DriverId, btnOfLetter, btnOfNumb);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    break;
                case FunMenuButtonId.启动开始_车次号:
                case FunMenuButtonId.F1数据_F2车次号:
                    TrainId = GetTrainIDorDriverId(TrainId, btnOfLetter, btnOfNumb);
                    StateProvider.MenuValueArrDict[(int) MenuID][MenuID == FunMenuButtonId.F1数据_F2车次号 ? 1 : 3]
                        .ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F1数据_F3列车数据:
                    if (btnOfNumb != 1 && btnOfNumb != 2)
                        return;
                    TrainData = btnOfNumb.ToString();
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainData;
                    break;
                case FunMenuButtonId.F4等级_F1CTCS3_RBC:
                    RBCID += btnOfNumb.ToString();
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    break;
                case FunMenuButtonId.F4等级_F1CTCS3_电话号码:
                    TelNmub += btnOfNumb.ToString();
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
                case FunMenuButtonId.未选择:
                    ResponseLinkBtnEvent(btnOfNumb);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btnOfNumb">按钮代表的数字</param>
        private void ResponseLinkBtnEvent(int btnOfNumb)
        {
            switch (btnOfNumb)
            {
                case 1:
                    SelectModelControlModel(TrainMode.调车);
                    break;
                case 2:
                    SelectModelControlModel(TrainMode.目视);
                    break;
                case 3:
                    SelectModelControlModel(TrainMode.机信);
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
                case TrainMode.目视:
                    MenuID = FunMenuButtonId.F2模式_F2进入目视;
                    MenuID = m_ATPMainScreen.CurrentTrainMode == TrainMode.目视
                        ? FunMenuButtonId.F2模式_F2退出目视
                        : FunMenuButtonId.F2模式_F2进入目视;
                    break;
                case TrainMode.调车:
                    MenuID = m_ATPMainScreen.CurrentTrainMode == TrainMode.调车
                        ? FunMenuButtonId.F2模式_F1退出调车
                        : FunMenuButtonId.F2模式_F1进入调车;
                    break;
                case TrainMode.机信:
                    MenuID = m_ATPMainScreen.CurrentTrainMode == TrainMode.机信
                        ? FunMenuButtonId.F2模式_F3退出机信
                        : FunMenuButtonId.F2模式_F3进入机信;
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 按钮状态响应
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
                case FunMenuButtonId.未选择:
                    MenuID = FunMenuButtonId.F1数据;
                    break;
                case FunMenuButtonId.F1数据:
                    MenuID = FunMenuButtonId.F1数据_F1司机号;
                    //AppraiseControl.NotifyInputDriverID();
                    if (DriverId == "0")
                    {
                        DriverId = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    break;
                case FunMenuButtonId.F2模式:
                    MenuID = m_ATPMainScreen.CurrentTrainMode == TrainMode.调车
                        ? FunMenuButtonId.F2模式_F1退出调车
                        : FunMenuButtonId.F2模式_F1进入调车;

                    break;
                case FunMenuButtonId.F3载频:
                    MenuID = FunMenuButtonId.F3载频_F1上行;
                    break;
                case FunMenuButtonId.F4等级:
                    //MenuID = FunMenuButtonId.F4等级_F1CTCS3_RBC;
                    MenuID = FunMenuButtonId.未选择;
                    //if (RBCID == "0")
                    //{
                    //    RBCID = string.Empty;
                    //}
                    //MenuValueArrDict[(int)FunMenuButtonId.F4等级_F1CTCS3_RBC][1].ContentStr = RBCID;
                    Buttons[0].IsOutlineVisible = false;
                    Buttons[1].IsOutlineVisible = false;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub请求手动进入C3模式), 1,
                        0);
                    //SendSelectCtcsState(3);

                    break;
                case FunMenuButtonId.F5其他:
                    //向消息列表插入一条消息
                    OnAppendPostCmd(CmdType.SetBoolValue,
                        m_AppStartData[0]
                            ? m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub退出目视模式)
                            : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub执行制动测试1), 1,
                        0);
                    break;
                case FunMenuButtonId.F5其他_F2音量:
                    if (SoundValue <= 90)
                    {
                        SoundValue += 10;
                    }

                    break;
                case FunMenuButtonId.F5其他_F3亮度:
                    if (LightValue < TopAdorner.MaxLightValue)
                    {
                        LightValue += 10;
                    }
                    break;
                case FunMenuButtonId.启动开始_车次号:
                    MenuID = FunMenuButtonId.启动开始_司机号;
                    if (DriverId == "0")
                    {
                        DriverId = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    StateProvider.MenuValueArrDict[(int) FunMenuButtonId.启动开始_车次号][3].ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F4等级_F1CTCS3_电话号码:
                    if (RBCID == "0")
                    {
                        RBCID = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    MenuID = FunMenuButtonId.F4等级_F1CTCS3_RBC;
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
            }
        }

        private void ResponseF2()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.未选择:
                    RefreshSelectedModel();
                    MenuID = FunMenuButtonId.F2模式;
                    break;
                case FunMenuButtonId.F1数据:
                    MenuID = FunMenuButtonId.F1数据_F2车次号;
                    if (TrainId == "0")
                    {
                        TrainId = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F2模式:
                    //MenuID = FunMenuButtonId.F2模式_F2进入目视;
                    SelectModelControlModel(TrainMode.目视);
                    break;
                case FunMenuButtonId.F3载频:
                    MenuID = FunMenuButtonId.F3载频_F2下行;
                    break;
                case FunMenuButtonId.F4等级:
                    //向消息列表插入一条消息

                    OnAppendPostCmd(CmdType.SetBoolValue,
                        m_IsLoadC2
                            ? ((AppStartUp[1] && m_BrakeTest)
                                ? m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub在启动完成后确认CTCS2)
                                : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub确认CTCS2))
                            : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub确认CTCS21), 1, 0);

                    Buttons[0].IsOutlineVisible = false;
                    Buttons[1].IsOutlineVisible = false;
                    break;
                case FunMenuButtonId.F5其他:
                    MenuID = FunMenuButtonId.F5其他_F2音量;
                    break;
                case FunMenuButtonId.F5其他_F2音量:
                    if (SoundValue >= 10)
                    {
                        SoundValue -= 10;
                    }
                    break;
                case FunMenuButtonId.F5其他_F3亮度:
                    if (LightValue >= TopAdorner.MinLightValue)
                    {
                        LightValue -= 10;
                    }
                    break;
                case FunMenuButtonId.启动开始_司机号:
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    MenuID = FunMenuButtonId.启动开始_车次号;
                    if (TrainId == "0")
                    {
                        TrainId = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) FunMenuButtonId.启动开始_车次号][1].ContentStr = DriverId;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F4等级_F1CTCS3_RBC:
                    if (TelNmub == "0")
                    {
                        TelNmub = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    MenuID = FunMenuButtonId.F4等级_F1CTCS3_电话号码;
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
            }
        }

        private void ResponseF3()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.未选择:
                    MenuID = FunMenuButtonId.F3载频;
                    break;
                case FunMenuButtonId.F1数据:
                    MenuID = FunMenuButtonId.F1数据_F3列车数据;
                    break;
                case FunMenuButtonId.F2模式:
                    SelectModelControlModel(TrainMode.机信);
                    break;
                case FunMenuButtonId.F5其他:
                    MenuID = FunMenuButtonId.F5其他_F3亮度;
                    break;
            }
        }

        private void ResponseF4()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.未选择:
                    MenuID = FunMenuButtonId.F4等级;
                    if (m_TheSignalSystem == SignalSystem.CTCS3)
                    {
                        Buttons[0].IsOutlineVisible = true;
                    }
                    else if (m_TheSignalSystem == SignalSystem.CTCS2)
                    {
                        Buttons[1].IsOutlineVisible = true;
                    }
                    break;
                case FunMenuButtonId.F1数据:
                    //消息列表向上翻
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
                case FunMenuButtonId.未选择:
                    MenuID = FunMenuButtonId.F5其他;
                    break;
                case FunMenuButtonId.F1数据:
                    //消息列表向下翻
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
                case FunMenuButtonId.未选择:
                    RequestStart();
                    break;

                    #region :::::::: 司机号

                case FunMenuButtonId.F1数据_F1司机号:
                    if (!string.IsNullOrEmpty(DriverId))
                    {
                        MenuID = FunMenuButtonId.F1数据_F1司机号_F6确定;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    }
                    break;
                case FunMenuButtonId.F1数据_F1司机号_F6确定:
                    //检测并发送司机号,车次号不为空也发送
                    SetDriverIDorTrainId(DriverId, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf司机号字母),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf司机号数字));
                    MenuID = FunMenuButtonId.F1数据;
                    break;
                case FunMenuButtonId.启动开始_司机号:
                    DriverId = string.Empty;
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    if (!string.IsNullOrEmpty(DriverId))
                    {
                        MenuID = FunMenuButtonId.启动开始_车次号;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                        TrainId = string.Empty;
                        StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    }
                    break;

                    #endregion

                    #region :::::::: 车次号

                case FunMenuButtonId.F1数据_F2车次号:
                    if (!string.IsNullOrEmpty(TrainId))
                    {
                        MenuID = FunMenuButtonId.F1数据_F2车次号_F6确定;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainId;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.F1数据_F1司机号;
                        DriverId = string.Empty;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    }
                    break;
                case FunMenuButtonId.F1数据_F2车次号_F6确定:
                    //检测并发送车次号，司机号不为空也发送
                    SetDriverIDorTrainId(TrainId, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf车次号字母),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf车次号数字));
                    MenuID = FunMenuButtonId.F1数据;
                    break;
                case FunMenuButtonId.启动开始_车次号:
                    if (!string.IsNullOrEmpty(DriverId) && !string.IsNullOrEmpty(TrainId))
                    {
                        StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                        MenuID = FunMenuButtonId.启动开始_司机车次号确定;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                        StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.启动开始_司机号;
                        DriverId = string.Empty;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    }
                    break;
                case FunMenuButtonId.启动开始_司机车次号确定:
                    MenuID = FunMenuButtonId.未选择;
                    SetDriverIDorTrainId(DriverId, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf司机号字母),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf司机号数字));
                    SetDriverIDorTrainId(TrainId, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf车次号字母),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf车次号数字));
                    if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbCTCS3)])
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub4858), 1, 0);
                    }
                    else
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue,
                            m_AppStartData[0]
                                ? m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub执行制动测试4)
                                : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub执行制动测试3), 1,
                            0);
                    }

                    //执行制动测试
                    break;

                    #endregion

                    #region :::::::: 列车数据

                case FunMenuButtonId.F1数据_F3列车数据:
                    try
                    {
                        if (Convert.ToInt32(TrainData) == 1 || Convert.ToInt32(TrainData) == 2)
                        {
                            MenuID = FunMenuButtonId.F1数据_F3列车数据_F6确定;
                            StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainData;
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                    break;
                case FunMenuButtonId.F1数据_F3列车数据_F6确定:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2])
                    {

                        MenuID = FunMenuButtonId.F3载频;
                        m_IsLoadC2 = true;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.F1数据;
                    }
                    //发送列车数据
                    SetDriverIDorTrainId(TrainData, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf列车数据),
                        m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf列车数据));
                    break;

                    #endregion

                    #region :::::::: 模式

                case FunMenuButtonId.F2模式_F1进入调车:
                    //发送调车模式标志
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub调车模式), 1, 0);
                    MenuID = FunMenuButtonId.未选择;
                    ChangeMode(0);
                    ModeSelect[0] = true;
                    //BtnStrDict[2][0] = "退出\n调车";
                    //m_MenuValueArrDict[2][0].ContentStr = "退出调车模式";
                    break;
                case FunMenuButtonId.F2模式_F1退出调车:
                    //发送调车模式标志
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub退出调车模式), 1, 0);
                    MenuID = FunMenuButtonId.未选择;
                    ModeSelect[0] = false;
                    //BtnStrDict[2][0] = "调车";
                    //m_MenuValueArrDict[2][0].ContentStr = "调车模式";
                    break;
                case FunMenuButtonId.F2模式_F2进入目视:
                    //发送目视模式标志
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub目视模式), 1, 0);
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F2模式_F2退出目视:
                    //发送目视模式标志
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub退出目视模式), 1, 0);
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F2模式_F3进入机信:
                    //发送机信模式标志
                    //ChangeMode(2);
                    // ModeSelect[2] = true;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub机信模式), 1, 0);
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F2模式_F3退出机信:
                    //发送机信模式标志
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub退出机信模式), 1, 0);
                    MenuID = FunMenuButtonId.未选择;
                    break;

                    #endregion

                    #region :::::::: 载频

                case FunMenuButtonId.F3载频_F1上行:
                    MenuID = FunMenuButtonId.F3载频_F1上行_确定;
                    Buttons[0].IsOutlineVisible = true;
                    //此处相应位置要加框
                    break;
                case FunMenuButtonId.F3载频_F2下行:
                    MenuID = FunMenuButtonId.F3载频_F2下行_确定;
                    Buttons[1].IsOutlineVisible = true;
                    //此处相应位置要加框
                    break;

                    #endregion

                    #region :::::::: 等级

                case FunMenuButtonId.F4等级_F1CTCS3:
                    //将相应消息标志为结束
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F4等级_F2CTCS2:
                    //将相应消息标志为结束
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F4等级_F1CTCS3_RBC:
                    if (TelNmub == "0")
                    {
                        TelNmub = string.Empty;
                    }
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    MenuID = FunMenuButtonId.F4等级_F1CTCS3_电话号码;
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
                case FunMenuButtonId.F4等级_F1CTCS3_电话号码:
                    int tmpCTCS;
                    if (int.TryParse(RBCID, out tmpCTCS) && int.TryParse(TelNmub, out tmpCTCS))
                    {
                        MenuID = FunMenuButtonId.F4等级_F2CTCS3_RBC确认;
                        StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                        StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    }
                    break;
                case FunMenuButtonId.F4等级_F2CTCS3_RBC确认:

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

                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf电话号码2),
                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf电话号码3),
                            m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf电话号码1),
                        }, TelNmub);


                    //OnAppendPostCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.OufRBCID1), 0, FormatConversion(RBCID));
                    //OnAppendPostCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf电话号码1), 0, FormatConversion(TelNmub));
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub电话号码发送确认), 1, 0);
                    // OnAppendPostCmd(CmdType.SetBoolValue, m_BoolSendList[11], 1, 0);

                    if (AppStartUp[0] && !AppStartUp[1])
                    {
                        MenuID = FunMenuButtonId.F1数据_F3列车数据;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.未选择;
                    }
                    break;

                    #endregion

                    //case FunMenuButtonID.启动开始_运行等级:
                //    SpecialMsgEndThenSendValueAndJumpView();
                //    break;

                case FunMenuButtonId.F5制动测试:
                    //发送制动测试标志位
                    //将相应消息标志为结束
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F6启动:
                    SendStartFlag();
                    break;
                case FunMenuButtonId.F7缓解:
                    MenuID = FunMenuButtonId.未选择;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub允许缓解), 1, 0);
                    break;
                case FunMenuButtonId.确定取消:
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
                case FunMenuButtonId.未选择:
                    RequestRelaseBrake();
                    break;
                case FunMenuButtonId.F1数据_F1司机号:
                    DriverId = Backspace(DriverId);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    break;
                case FunMenuButtonId.F1数据_F2车次号:
                    TrainId = Backspace(TrainId);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainId;
                    break;
                case FunMenuButtonId.F1数据_F3列车数据:
                    TrainData = Backspace(TrainData);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = TrainData;
                    break;
                case FunMenuButtonId.F4等级_F1CTCS3_RBC:
                    RBCID = Backspace(RBCID);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = RBCID;
                    break;
                case FunMenuButtonId.F4等级_F1CTCS3_电话号码:
                    TelNmub = Backspace(TelNmub);
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TelNmub;
                    break;
                case FunMenuButtonId.启动开始_司机号:
                    DriverId = Backspace(DriverId);
                    StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                    break;
                case FunMenuButtonId.启动开始_车次号:
                    TrainId = Backspace(TrainId);
                    StateProvider.MenuValueArrDict[(int) MenuID][3].ContentStr = TrainId;
                    break;
            }
        }

        private void ResponseF8()
        {
            switch (MenuID)
            {
                case FunMenuButtonId.未选择:
                    SendVigilantFlag();
                    break;

                    #region ::::::::::F1

                case FunMenuButtonId.启动开始_司机号:
                    if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbCTCS3)])
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub4858), 1, 0);
                    }
                    else
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue,
                            m_AppStartData[0]
                                ? m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub执行制动测试4)
                                : m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub执行制动测试3), 1,
                            0);
                    }
                    break;
                case FunMenuButtonId.F1数据:
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F1数据_F1司机号:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2])
                    {
                        MenuID = FunMenuButtonId.F1数据;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.未选择;
                    }
                    break;
                case FunMenuButtonId.F1数据_F1司机号_F6确定:
                    MenuID = FunMenuButtonId.F1数据_F1司机号;
                    break;
                case FunMenuButtonId.F1数据_F2车次号:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2])
                    {
                        MenuID = FunMenuButtonId.F1数据;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.未选择;
                    }
                    break;
                case FunMenuButtonId.F1数据_F2车次号_F6确定:
                    MenuID = FunMenuButtonId.F1数据_F2车次号;
                    break;
                case FunMenuButtonId.F1数据_F3列车数据:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2])
                    {
                        MenuID = FunMenuButtonId.F1数据;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.未选择;
                    }
                    break;
                case FunMenuButtonId.F1数据_F3列车数据_F6确定:
                    MenuID = FunMenuButtonId.F1数据_F3列车数据;
                    break;
                //case FunMenuButtonId.启动开始_司机号:
                //    MenuID = FunMenuButtonId.未选择;
                //    _appStartUp[2] = true;//启动过程中断
                //    break;
                //case FunMenuButtonId.启动开始_车次号:
                //    MenuID = FunMenuButtonId.未选择;
                //    _appStartUp[2] = true;//启动过程中断
                //    break;
                case FunMenuButtonId.启动开始_司机车次号确定:
                    MenuID = FunMenuButtonId.启动开始_车次号;
                    break;

                    #endregion

                    #region ::::::::::F2

                case FunMenuButtonId.F2模式:
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F2模式_F1进入调车:
                case FunMenuButtonId.F2模式_F1退出调车:
                case FunMenuButtonId.F2模式_F2进入目视:
                case FunMenuButtonId.F2模式_F2退出目视:
                case FunMenuButtonId.F2模式_F3进入机信:
                case FunMenuButtonId.F2模式_F3退出机信:
                    MenuID = FunMenuButtonId.F2模式;
                    break;

                    #endregion

                    #region ::::::::: F3

                case FunMenuButtonId.F3载频:
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F3载频_F1上行:
                    MenuID = FunMenuButtonId.F3载频;
                    break;
                case FunMenuButtonId.F3载频_F1上行_确定:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2] && m_BrakeTest)
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub4856), 1, 0);
                        AppStartUp[0] = false;
                        AppStartUp[1] = true;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.未选择;
                    }
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub载频上行), 1, 0);
                    //上行置1
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub载频下行), 0, 0);
                    //下行置0
                    Buttons[0].IsOutlineVisible = false;
                    //发送上行标志
                    break;
                case FunMenuButtonId.F3载频_F2下行:
                    MenuID = FunMenuButtonId.F3载频;
                    break;
                case FunMenuButtonId.F3载频_F2下行_确定:
                    if (AppStartUp[0] && !AppStartUp[1] && !AppStartUp[2] && m_BrakeTest)
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub4856), 1, 0);
                        AppStartUp[0] = false;
                        AppStartUp[1] = true;
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.未选择;
                    }
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub载频上行), 0, 0);
                    //上行置0
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub载频下行), 1, 0);
                    //下行置1
                    Buttons[1].IsOutlineVisible = false;
                    //发送下行标志
                    break;

                    #endregion

                    #region ::::::::: F4

                case FunMenuButtonId.F4等级:
                    if (OldMenuId == FunMenuButtonId.确定取消 && !AppStartUp[1])
                    {
                        OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub确认CTCS21),
                            1, 0); //确认CTCS2
                    }
                    else
                    {
                        MenuID = FunMenuButtonId.未选择;

                    }
                    Buttons[0].IsOutlineVisible = false;
                    Buttons[1].IsOutlineVisible = false;
                    break;

                case FunMenuButtonId.F4等级_F1CTCS3_RBC:
                    MenuID = FunMenuButtonId.F4等级;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.OubRBC输入取消), 1, 0);
                    break;

                case FunMenuButtonId.F4等级_F1CTCS3_电话号码:
                    StateProvider.MenuValueArrDict[(int) FunMenuButtonId.F4等级_F1CTCS3_RBC][3].ContentStr = TelNmub;
                    OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.OubRBC输入取消), 1, 0);
                    MenuID = FunMenuButtonId.F4等级;
                    break;

                case FunMenuButtonId.F4等级_F2CTCS3_RBC确认:
                    MenuID = FunMenuButtonId.F4等级_F1CTCS3_电话号码;
                    break;

                case FunMenuButtonId.F4等级_F1CTCS3:
                    MenuID = FunMenuButtonId.F4等级;
                    break;

                case FunMenuButtonId.F4等级_F2CTCS2:
                    MenuID = FunMenuButtonId.F4等级;
                    break;

                    #endregion

                    #region ::::::::: F5

                case FunMenuButtonId.F5其他:
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.F5其他_F2音量:
                    MenuID = FunMenuButtonId.F5其他;
                    break;
                case FunMenuButtonId.F5其他_F3亮度:
                    MenuID = FunMenuButtonId.F5其他;
                    break;

                    #endregion

                    #region ::::::::: F6

                case FunMenuButtonId.F6启动:
                    MenuID = FunMenuButtonId.未选择;
                    break;

                    #endregion

                case FunMenuButtonId.F7缓解:
                    MenuID = FunMenuButtonId.未选择;
                    break;
                case FunMenuButtonId.确定:
                    if (MsgHandlerATPList.SpecialMsgList.Count != 0)
                    {
                        //启动确认
                        if (MsgHandlerATPList.SpecialMsgList[0].MsgID == "9")
                        {
                            AppStartUp[1] = true;
                        }
                        SpecialMsgEndThenSendValueAndJumpView(true);
                    }
                    break;

                case FunMenuButtonId.确定取消:

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
                case 18: //数字字母切换
                    if (MenuID == FunMenuButtonId.F1数据_F2车次号 ||
                        MenuID == FunMenuButtonId.启动开始_车次号)
                    {
                        m_SwitchOver = !m_SwitchOver;
                    }
                    else if (MenuID == FunMenuButtonId.未选择)
                    {
                        SendVigilantFlag();
                    }
                    return;
            }
        }

        /// <summary>
        /// 退格
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

            AppLog.Info("输入数据为：" + sPara);
            AppLog.Info(string.Format("发送数据位{0}：{1}", iPara[0], rt[0]));
            AppLog.Info(string.Format("发送数据位{0}：{1}", iPara[1], rt[1]));
            AppLog.Info(string.Format("发送数据位{0}：{1}", iPara[2], rt[2]));
            AppLog.Info("发送数据解码：" +
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
            //发送启动标志位
            if (OnAppendPostCmd != null)
            {
                OnAppendPostCmd(CmdType.SetBoolValue,
                    m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub启动确认没有消息提示与确认), 1, 0);
            }
            //将相应消息标志为结束
            MenuID = FunMenuButtonId.未选择;
        }

        private void SendVigilantFlag()
        {
            //警惕
            if (OnAppendPostCmd != null)
            {
                OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.Oub警惕按钮), 1, 0);
            }
            return;
        }

        private void RequestRelaseBrake()
        {
            //发送缓解标志
            if (m_AppRunData[0])
            {
                MenuID = FunMenuButtonId.F7缓解;
            }
        }

        private void RequestStart()
        {
            if (m_CanStartUp)
            {
                MenuID = FunMenuButtonId.F6启动;
            }
        }

        private void RefreshButtonEnableableState()
        {
            foreach (var buttonBar in Buttons)
            {
                // need 优化
                var target = string.Format("{0}按键是否失效", buttonBar.Content);
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
        /// 初始化
        /// </summary>
        protected void Init()
        {
            m_CanStartUp = false;

            m_SwitchOver = false;

            m_MenuId = FunMenuButtonId.未选择;

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

        #region :::::::::::::::::全局量

        public AppraiseControl AppraiseControl { get; set; }

        //所有矩形框列表
        public List<RectangleF> RectList { private set; get; }

        /// <summary>
        /// 上一次的菜单编号
        /// </summary>
        public FunMenuButtonId OldMenuId { get; private set; }


        public ButtonBar[] Buttons { get; private set; }

        /// <summary>
        /// 下排数字字母按钮
        /// </summary>
        private readonly KeyBoard m_Keyboard = new KeyBoard(5);

        //数字字母切换开关
        private bool m_SwitchOver;

        //menu内容词典，根据MenuID变化

        /// <summary>
        /// 调车模式、目视模式、机信模式转换
        /// </summary>
        public bool[] ModeSelect { get; set; }

        private readonly Image[] m_ImgsArr;

        #endregion

        #region :::::::::::::: 启动过程数据量

        /// <summary>
        /// 启动过程
        /// 分别表示 开始、结束、中断
        /// </summary>
        public bool[] AppStartUp { get; set; }

        /// <summary>
        /// 程序启动过程中需要的数据
        /// 第一个表示上次成功进行制动测试的时间是否在30小时以内
        /// </summary>
        private readonly bool[] m_AppStartData = new bool[10];

        /// <summary>
        /// 程序运行过程中需要的数据
        /// 第一个表示允许缓解
        /// 第二个表示警惕
        /// </summary>
        private readonly bool[] m_AppRunData = new bool[10];

        /// <summary>
        /// 制动测试
        /// </summary>
        private bool m_BrakeTest;

        /// <summary>
        /// 可以启动
        /// </summary>
        private bool m_CanStartUp;

        #endregion

        #region ::::::::::::::::::::: 消息列表

        public MsgHandlerATP<Message> MsgHandlerATPList { set; get; }

        /// <summary>
        /// 显示的第一条消息在消息列表的位置
        /// </summary>
        public int ShowFirstInMsgList { get; private set; }

        /// <summary>
        /// 消息列表向上向下图标显示
        /// </summary>
        public bool[] MsgListCouldUpOrDown { get; private set; }

        /// <summary>
        /// 当前获取的消息个数列表
        /// </summary>
        public List<int> MsgCatchList { get; set; }

        #endregion

        #region ::::::::::::::::::::: 车辆数据

        /// <summary>
        /// 车次号
        /// </summary>
        public string TrainId { get; set; }

        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverId { get; set; }

        /// <summary>
        /// RBC号码
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string RBCID { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelNmub { get; set; }

        /// <summary>
        /// 车辆长度
        /// </summary>
        public string TrainData { get; set; }

        /// <summary>
        ///  网络ID
        /// </summary>
        public string NetId { set; get; }

        //音量

        /// <summary>
        /// 音量
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
        /// 亮度
        /// </summary>
        public int LightValue { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// 评价用页面
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
                OnAppendPostCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.OubATP进入司机号输入界面),
                    m_Appraise ? 1 : 0, 0);
            }
        }

        public event AppendPostCmd OnAppendPostCmd;

        public bool Painting { get; set; }

        protected FunMenuButtonId m_MenuId = FunMenuButtonId.未选择;

        /// <summary>
        /// 当前所在菜单编号
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

        #region :::::::::::::: 消息列表内容 :::::::::::::::::

        /// <summary>
        /// 绘制消息列表
        /// </summary>
        /// <param name="g"></param>
        public void DrawMsgList(Graphics g)
        {
            if (MsgHandlerATPList.SpecialMsgList.Any())
            {
                //闪烁的消息
                g.DrawString(
                    MsgHandlerATPList.SpecialMsgList[0].MsgReceiveTime.ToLongTimeString() + " " +
                    MsgHandlerATPList.SpecialMsgList[0].MsgContent,
                    FontsItems.Font12YouB, SolidBrushsItems.WhiteBrush,
                    RectList[50], FontsItems.TheAlignment(FontRelated.靠左));
                if (MsgHandlerATPList.SpecialMsgList[0].MsgContent.Equals("执行制动测试") ||
                    MsgHandlerATPList.SpecialMsgList[0].MsgContent.Equals("确认CTCS-2"))
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
                //绘制不会闪烁的消息
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
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
        }

        private bool TheCursorCanDown(bool beLevelMsg, int idInMsgList)
        {
            return MsgHandlerATPList.AllMsgsList.Count - idInMsgList > (beLevelMsg ? 3 : 4);
        }

        #endregion

        public void Update(ATPBaseClass source)
        {
            //可以启动
            if (source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb流程中启动条件)] && !m_CanStartUp)
            {
                OnAppendPostCmd(CmdType.SetBoolValue, source.GetOutBoolIndex(OutBoolKeys.Oub4856), 1, 0);
                m_CanStartUp = true;
            }


            m_BrakeTest = source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb制动测试成功)]; //制动测试成功标志

            if (!source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb驾驶室未激活)] && !AppStartUp[0] && !AppStartUp[1])
                //驾驶室激活
            {
                MenuID = FunMenuButtonId.启动开始_司机号;
                if (DriverId == "0")
                {
                    DriverId = string.Empty;
                }
                StateProvider.MenuValueArrDict[(int) MenuID][1].ContentStr = DriverId;
                AppStartUp[0] = true;
            }

            m_AppStartData[0] = source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb上次成功进行制动测试的时间是否在30小时以内)];
            //上次成功进行制动测试的时间是否在30小时以内

            m_AppRunData[0] = source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb允许缓解1)]; //允许缓解
            m_AppRunData[1] = source.BoolList[source.GetInBoolIndex(InBoolKeys.Inb警惕按键)]; //警惕按钮

            //
            foreach (var t in MsgHandlerATPList.AllMsgsList.Where(
                t => t.TheSpecialHanding == SpecialHanding.一分钟后自动结束并发送数据 &&
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

            //刷新按键状态
            RefreshButtonEnableableState();

            DrawStateView(g);

            //切换数字字符
            ResponseSwitchInputModel();

        }

        private void DrawStateView(Graphics g)
        {
            //按钮状态
            DrawButtons(g);

            //弹出框 
            DrawPopView(g);

            //消息
            DrawMsgList(g);

            //上下翻页
            DrawTurnPageView(g);
        }

        /// <summary>
        /// 清除下一步操作提示音
        /// </summary>
        private void ClearSound(object obj = null)
        {
            if (m_ATPMainScreen.OutBoolList[61])
            {
                OnAppendPostCmd(CmdType.SetBoolValue, 61, 0, 0);
            }
        }

        /// <summary>
        /// 发送下一步操作提示音
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
        /// 切换数字字符
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
                                           (MsgHandlerATPList.SpecialMsgList.Count != 0 ? 3 : 4)); //下翻
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
        /// 绘弹出框
        /// </summary>
        /// <param name="g"></param>
        private void DrawPopView(Graphics g)
        {
            Appraise = m_AppraiseList.Contains(MenuID);
            //菜单
            if (StateProvider.BtnStrDict[(int) MenuID].Length > 8)
            {
                g.FillRectangle(SolidBrushsItems.BtnDisabledBrush, RectList[8]);
                g.DrawString(StateProvider.BtnStrDict[(int) MenuID][8], FontsItems.Font16YouB,
                    SolidBrushsItems.WhiteBrush, RectList[8], FontsItems.TheAlignment(FontRelated.居中));
                g.FillRectangle(SolidBrushsItems.BtnAbledBrush, RectList[9]);

                if (StateProvider.BtnStrDict[(int) MenuID].Length == 10)
                {
                    g.DrawString(StateProvider.BtnStrDict[(int) MenuID][9], FontsItems.Font17YouB,
                        SolidBrushsItems.WhiteBrush, RectList[9], FontsItems.TheAlignment(FontRelated.居中));
                }
            }

            //填表
            if (StateProvider.MenuValueArrDict.ContainsKey((int) MenuID))
            {
                foreach (var mv in StateProvider.MenuValueArrDict[(int) MenuID])
                {
                    mv.OnDraw(g);
                }
            }

            switch (MenuID) //音量，亮度
            {
                case FunMenuButtonId.F5其他_F2音量:
                    m_Progresses[0].OnDraw(g);
                    break;
                case FunMenuButtonId.F5其他_F3亮度:
                    m_Progresses[1].OnDraw(g);
                    break;
            }
        }
    }
}