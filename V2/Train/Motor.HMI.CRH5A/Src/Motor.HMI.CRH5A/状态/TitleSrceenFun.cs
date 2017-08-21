using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5A.Resource;
using Motor.HMI.CRH5A.制动试验;

namespace Motor.HMI.CRH5A.Staus
{
    public partial class TitleScreen
    {
        private void GetValue()
        {
            BrakeOneTest.Instance.RefreshBrakeTestStatus();
            BrakeTwoTest.Instance.RefreshBrakeTestStatus();
            BrakeThreeTest.Instance.RefreshBrakeTestStatus();
            TrainInside = GetInBoolValue(InBoolKeys.InB换端标志480); //车辆换端
            OldChangeLength = ChangeLength;
            ChangeLength = GetInBoolValue(InBoolKeys.InB重联标志481); //8车换16车

            if (ViewId == 101 || ViewId == 102) return;

            if (ButtonsScreen.BtnState != null && !ButtonsScreen.BtnState.IsPress)
            {
                ChangePage(ButtonsScreen.BtnState.BtnId);
            }

        }
        private bool ResponseBottomBtnEventByVisitor(int bottomBtnIndex)
        {
            return m_BottomButtonVisitor != null && m_BottomButtonVisitor.OnButtonDown(bottomBtnIndex);
        }

        private void OnBottomBtn0Down()
        {
            switch (ViewId)
            {
                case 3:
                case 2:
                case 1:
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    break;
                case 6:
                case 5:
                case 4:
                    append_postCmd(CmdType.ChangePage, 4, 0, 0);
                    break;
                case 9:
                case 8:
                case 7:
                    append_postCmd(CmdType.ChangePage, 7, 0, 0);
                    break;
                case 11:
                case 10:
                    append_postCmd(CmdType.ChangePage, 10, 0, 0);
                    break;
                case 22:
                case 21:
                    append_postCmd(CmdType.ChangePage, 21, 0, 0);
                    break;
                case 32:
                case 31:
                case 30:
                    append_postCmd(CmdType.ChangePage, 30, 0, 0);
                    break;
                case 36:
                case 35:
                case 34:
                    append_postCmd(CmdType.ChangePage, 34, 0, 0);
                    break;
                case 38:
                case 37:
                    append_postCmd(CmdType.ChangePage, 37, 0, 0);
                    break;
                case 39:
                    append_postCmd(CmdType.ChangePage, 39, 0, 0);
                    break;
                case 40:
                    append_postCmd(CmdType.ChangePage, 40, 0, 0);
                    break;
                case 43:
                case 42:
                case 41:
                    append_postCmd(CmdType.ChangePage, 41, 0, 0);
                    break;
            }
            ButtonsScreen.BtnState.Press();
        }
        /// <summary>
        /// 清除发送的按钮数据
        /// </summary>
        private void ClearSendData(object obj = null)
        {
            foreach (var s in LogicName.Where(GetOutBoolValue))
            {
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(s), 0, 0);

            }
        }


        private void SendTestFlag(int index)
        {
            append_postCmd(CmdType.SetBoolValue, index, 1, 0);
            m_ClearSendDatatTimer.Change(500, int.MaxValue);
        }
        public ReadOnlyCollection<RectangleF> GetButtonBtnRegions()
        {
            return new ReadOnlyCollection<RectangleF>(m_RectsList.Skip(6).Take(10).ToArray());
        }

        private void ChangePage(int btnId)
        {
            if (btnId >= 15 && btnId <= 25)
            {
                if (ResponseBottomBtnEventByVisitor(btnId - 15))
                {
                    return;
                }
            }

            switch (btnId)
            {
                case 10: //退出至菜单栏
                    append_postCmd(CmdType.ChangePage, 33, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 14: //全停键
                    if (ChangedMainViewsWhenPressReturn.Contains(ViewId))
                    {
                        append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 15: //1
                    OnBottomBtn0Down();
                    break;
                case 16: //2
                    switch (ViewId)
                    {
                        case 3:
                        case 2:
                        case 1:
                            append_postCmd(CmdType.ChangePage, 2, 0, 0);
                            break;
                        case 6:
                        case 5:
                        case 4:
                            append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 9:
                        case 8:
                        case 7:
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 11:
                        case 10:
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 22:
                        case 21:
                            append_postCmd(CmdType.ChangePage, 44, 0, 0);
                            break;
                        case 32:
                        case 31:
                        case 30:
                            append_postCmd(CmdType.ChangePage, 31, 0, 0);
                            break;
                        case 36:
                        case 35:
                        case 34:
                            append_postCmd(CmdType.ChangePage, 35, 0, 0);
                            break;
                        case 38:
                        case 37:
                            append_postCmd(CmdType.ChangePage, 38, 0, 0);
                            break;
                        case 43:
                        case 42:
                        case 41:
                            append_postCmd(CmdType.ChangePage, 42, 0, 0);
                            break;
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17: //3
                    switch (ViewId)
                    {
                        case 3:
                        case 2:
                        case 1:
                            append_postCmd(CmdType.ChangePage, 3, 0, 0);
                            break;
                        case 6:
                        case 5:
                        case 4:
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 9:
                        case 8:
                        case 7:
                            append_postCmd(CmdType.ChangePage, 9, 0, 0);
                            break;
                        case 32:
                        case 31:
                        case 30:
                            append_postCmd(CmdType.ChangePage, 32, 0, 0);
                            break;
                        case 36:
                        case 35:
                        case 34:
                            append_postCmd(CmdType.ChangePage, 36, 0, 0);
                            break;
                        case 43:
                        case 42:
                        case 41:
                            append_postCmd(CmdType.ChangePage, 43, 0, 0);
                            break;
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18: //其他
                    switch (ViewId)
                    {
                        case 3:
                        case 2:
                        case 1:
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            break;
                        case 6:
                        case 5:
                        case 4:
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 9:
                        case 8:
                        case 7:
                            append_postCmd(CmdType.ChangePage, 10, 0, 0);
                            break;
                        case 36:
                            SendTestFlag(GetOutBoolIndex(OutBoolKeys.OutB启动备用制动实验));
                            LogMgr.Debug("启动备用制动实验");
                            break;
                        case 35:
                            SendTestFlag(GetOutBoolIndex(OutBoolKeys.OutB启动导向制动实验));
                            LogMgr.Debug("启动导向制动实验");
                            break;
                        case 34:
                            SendTestFlag(GetOutBoolIndex(OutBoolKeys.OutB启动自动制动实验));
                            LogMgr.Debug("启动自动制动实验");
                            break;
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 19:
                    switch (ViewId)
                    {
                        case 36:
                            SendTestFlag(GetOutBoolIndex(OutBoolKeys.OutB停止备用制动实验));
                            LogMgr.Debug("停止备用制动实验");
                            break;
                        case 35:
                            SendTestFlag(GetOutBoolIndex(OutBoolKeys.OutB停止导向制动实验));
                            LogMgr.Debug("停止导向制动实验");
                            break;
                        case 34:
                            SendTestFlag(GetOutBoolIndex(OutBoolKeys.OutB停止自动制动实验));
                            LogMgr.Debug("停止自动制动实验");
                            break;
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                //case 24:
                //    break;
            }
        }
    }
}