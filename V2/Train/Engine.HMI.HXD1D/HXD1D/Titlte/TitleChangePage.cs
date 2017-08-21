using System.Globalization;
using HXD1D.控制设置;
using HXD1D.故障界面;
using HXD1D.运行条件;
using MMI.Facility.Interface.Data;

namespace HXD1D.Titlte
{
    public partial class Title
    {
        /// <summary>
        /// 页面的跳转
        /// </summary>
        /// <param name="btnID"></param>
        private void ChangePage(int btnID)
        {
            switch (btnID)
            {
                case 0:
                    if (CurentView == 1 || CurentView == 5)
                    {
                        Record.GoNextView(this, 8);
                        TitleName = "主要数据";
                        ButtonReset();
                        buttonIsDown[0] = true;
                    }

                    if (CurentView == 2 || CurentView == 9 || CurentView == 10 || CurentView == 11 || CurentView == 12 || CurentView == 13 || CurentView == 14)
                    {
                        ButtonReset();
                        buttonIsDown[0] = true;
                        Record.GoNextView(this, 8);
                        TitleName = "牵引数据";
                    }

                    //if (CurentView == 16)
                    //{
                    //    //ButtonReset();
                    //    buttonIsDown[0] = false;

                    //}
                    if (CurentView == 3)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 19);
                        //append_postCmd(CmdType.ChangePage, 19, 0, 0);
                        TitleName = "载重设置";

                    }
                    BtnList[10].Cursors();
                    if (CurentView == 4)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 23);
                        //append_postCmd(CmdType.ChangePage, 23, 0, 0);
                        TitleName = "分相设置";
                    }
                    if (CurentView == 6 || CurentView == 30 || CurentView == 31 || CurentView == 32 || CurentView == 45)
                    {
                        ButtonReset();
                        buttonIsDown[0] = true;
                        Record.GoNextView(this, 29);
                        //append_postCmd(CmdType.ChangePage, 29, 0, 0);
                        TitleName = "辅机测试";
                    }
                    if (CurentView == 33)
                    {
                        ButtonReset();
                        buttonIsDown[0] = true;
                        Record.GoNextView(this, 34);
                        //append_postCmd(CmdType.ChangePage, 34, 0, 0);
                        TitleName = "机车编号";
                    }

                    if (CurentView == 7 || CurentView == 48 || CurentView == 49 || CurentView == 50 || CurentView == 51 || CurentView == 52)
                    {
                        ButtonReset();
                        V48_PressModeSet.PointOut = "";
                        buttonIsDown[0] = true;
                        Record.GoNextView(this, 47);
                        TitleName = "版本信息界面";
                    }

                    if (CurentView == 53)
                    {
                        V53_LongDistanceCutOff.CutOffList[V53_LongDistanceCutOff.CurrentRow] = 1;
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[17 + V53_LongDistanceCutOff.CurrentRow], 1, 0);
                    }
                    break;
                case 1:
                    if (CurentView == 1 || CurentView == 5)
                    {
                        ButtonReset();
                        buttonIsDown[9] = false;
                        Record.GoNextView(this, 3);
                        //append_postCmd(CmdType.ChangePage, 3, 0, 0);
                        TitleName = "控制设置";
                    }
                    if (CurentView == 2 || CurentView == 8 || CurentView == 10 || CurentView == 11 || CurentView == 12 || CurentView == 13 || CurentView == 14)
                    {
                        ButtonReset();
                        buttonIsDown[1] = true;
                        Record.GoNextView(this, 9);
                        //append_postCmd(CmdType.ChangePage, 9, 0, 0);
                        TitleName = "机车配置";
                    }
                    //if (CurentView == 16)
                    //{
                    //    //ButtonReset();
                    //    buttonIsDown[1] = false;
                    //}
                    if (CurentView == 3)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 20);
                        //append_postCmd(CmdType.ChangePage, 20, 0, 0);
                        TitleName = "联挂设置";
                    }

                    BtnList[11].Cursors();

                    if (CurentView == 6 || CurentView == 29 || CurentView == 31 || CurentView == 32 || CurentView == 45)
                    {
                        ButtonReset();
                        buttonIsDown[1] = true;
                        Record.GoNextView(this, 30);
                        //append_postCmd(CmdType.ChangePage, 30, 0, 0);
                        TitleName = "库内动车";
                    }
                    if (CurentView == 33)
                    {
                        ButtonReset();
                        buttonIsDown[1] = true;
                        Record.GoNextView(this, 35);
                        //append_postCmd(CmdType.ChangePage, 35, 0, 0);
                        TitleName = "操作段";
                    }

                    if (CurentView == 53)
                    {
                        V53_LongDistanceCutOff.CutOffList[V53_LongDistanceCutOff.CurrentRow] = 0;
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[17 + V53_LongDistanceCutOff.CurrentRow], 0, 0);
                    }
                    break;
                case 2:
                    if (CurentView == 1 || CurentView == 5)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 4);
                        //append_postCmd(CmdType.ChangePage, 4, 0, 0);
                        TitleName = "机车设置";
                    }
                    if (CurentView == 2 || CurentView == 8 || CurentView == 9 || CurentView == 11 || CurentView == 12 || CurentView == 13 || CurentView == 14)
                    {
                        ButtonReset();
                        buttonIsDown[2] = true;
                        Record.GoNextView(this, 10);
                        //append_postCmd(CmdType.ChangePage, 10, 0, 0);
                        TitleName = "控制系统";
                    }
                    if (CurentView == 3)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 21);
                        TitleName = "运行模式";

                    }
                    //if (CurentView == 16)
                    //{
                    //    ButtonReset();
                    //    buttonIsDown[2] = true;
                    //    ButtonName = "V>0";
                    //}
                    if (CurentView == 20)
                    {
                        //ButtonReset();
                        //Record.GoNextView(this, 21);
                        ////append_postCmd(CmdType.ChangePage, 21, 0, 0);
                        //TitleName = "运行模式";

                    }
                    BtnList[12].Cursors();
                    if (CurentView == 4)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 24);
                        //append_postCmd(CmdType.ChangePage, 24, 0, 0);
                        TitleName = "高隔设置";
                    }
                    if (CurentView == 6 || CurentView == 29 || CurentView == 30 || CurentView == 32 || CurentView == 45)
                    {
                        ButtonReset();
                        buttonIsDown[2] = true;
                        Record.GoNextView(this, 31);
                        //append_postCmd(CmdType.ChangePage, 31, 0, 0);
                        TitleName = "淋雨模式";
                    }
                    if (CurentView == 33)
                    {
                        ButtonReset();
                        buttonIsDown[2] = true;
                        Record.GoNextView(this, 36);
                        TitleName = "轮径设置";
                    }
                    break;
                case 3:
                    if (CurentView == 1 || CurentView == 6)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 26);
                        TitleName = "运行条件";
                    }
                    if (CurentView == 2 || CurentView == 8 || CurentView == 9 || CurentView == 10 || CurentView == 12 || CurentView == 13 || CurentView == 14)
                    {
                        ButtonReset();
                        buttonIsDown[3] = true;
                        Record.GoNextView(this, 11);
                        TitleName = "辅助系统";
                    }
                    if (CurentView == 3)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 22);
                        TitleName = "调度设置";
                    }
                    //if (CurentView == 16)
                    //{
                    //    ButtonReset();
                    //    buttonIsDown[3] = true;
                    //    ButtonName = "V<0";
                    //}
                    if (CurentView == 6 || CurentView == 29 || CurentView == 30 || CurentView == 31 || CurentView == 45)
                    {
                        ButtonReset();
                        buttonIsDown[3] = true;
                        Record.GoNextView(this, 32);
                        TitleName = "库内分相测试";
                    }

                    BtnList[13].Cursors();
                    if (CurentView == 33)
                    {
                        ButtonReset();
                        buttonIsDown[3] = true;
                        Record.GoNextView(this, 37);
                        TitleName = "轮缘润滑";
                    }


                    if (CurentView == 7 || CurentView == 47 || CurentView == 49 || CurentView == 50 || CurentView == 51 || CurentView == 52)
                    {
                        ButtonReset();
                        buttonIsDown[3] = true;
                        Record.GoNextView(this, 48);
                        TitleName = "空压机选择界面";
                    }
                    break;
                case 4:
                    if (CurentView == 1 || CurentView == 5)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 6);
                        TitleName = "维护测试";
                    }
                    if (CurentView == 2 || CurentView == 8 || CurentView == 9 || CurentView == 10 || CurentView == 11 || CurentView == 13 || CurentView == 14)
                    {
                        ButtonReset();
                        buttonIsDown[4] = true;
                        Record.GoNextView(this, 12);
                        TitleName = "列车供电";
                    }
                    //if (CurentView == 16)
                    //{
                    //    ButtonReset();
                    //    buttonIsDown[4] = true;
                    //    ButtonName = "信息";
                    //}
                    if (CurentView == 6 || CurentView == 29 || CurentView == 30 || CurentView == 31 || CurentView == 32)
                    {
                        ButtonReset();
                        buttonIsDown[4] = true;
                        Record.GoNextView(this, 45);
                        TitleName = "顶轮设置";
                    }
                    BtnList[14].Cursors();

                    if (CurentView == 24)
                    {
                        ButtonReset();
                        buttonIsDown[4] = true;
                    }
                    if (CurentView == 33)
                    {
                        ButtonReset();
                        buttonIsDown[4] = true;
                        Record.GoNextView(this, 38);
                        TitleName = "时间设置";
                    }

                    if (CurentView == 7 || CurentView == 47 || CurentView == 48 || CurentView == 50 || CurentView == 51 || CurentView == 52)
                    {
                        ButtonReset();
                        V48_PressModeSet.PointOut = "";
                        buttonIsDown[4] = true;
                        Record.GoNextView(this, 49);
                        TitleName = "强电DC110V选择界面";
                    }

                    if (CurentView == 3)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 53);
                        TitleName = "运程切除界面";
                    }
                    break;

                case 5:
                    if (CurentView == 1 || CurentView == 3 || CurentView == 5 || CurentView == 6)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 46);
                        TitleName = "密码输入界面";
                    }
                    if (CurentView == 2 || CurentView == 8 || CurentView == 9 || CurentView == 10 || CurentView == 11 || CurentView == 12 || CurentView == 14)
                    {
                        ButtonReset();
                        buttonIsDown[5] = true;
                        Record.GoNextView(this, 13);
                        TitleName = "状态信息";
                    }

                    BtnList[15].Cursors();
                    if (CurentView == 33)
                    {
                        ButtonReset();
                        Record.GoNextView(this, 39);
                        TitleName = "密码设置";
                    }

                    if (CurentView == 7 || CurentView == 47 || CurentView == 48 || CurentView == 49 || CurentView == 51 || CurentView == 52)
                    {
                        ButtonReset();
                        V48_PressModeSet.PointOut = "";
                        buttonIsDown[5] = true;
                        Record.GoNextView(this, 50);
                        TitleName = "断电模式界面";
                    }
                    break;
                case 6:
                    if (CurentView == 15)
                    {
                        ButtonReset();
                        buttonIsDown[6] = true;
                        TitleName = "当前故障";
                    }

                    BtnList[16].Cursors();

                    if (CurentView == 24)
                    {
                        ButtonReset();
                        buttonIsDown[6] = true;
                    }
                    if (CurentView == 5 || CurentView == 26 || CurentView == 27 || CurentView == 28)
                    {
                        ButtonReset();
                        buttonIsDown[6] = true;
                        Record.GoNextView(this, 26);
                        TitleName = "升弓条件";
                    }
                    break;
                case 7:
                    BtnList[17].Cursors();

                    if (CurentView == 5 || CurentView == 26 || CurentView == 27 || CurentView == 28)
                    {
                        ButtonReset();
                        buttonIsDown[7] = true;
                        Record.GoNextView(this, 27);
                        TitleName = "主断条件";
                    }
                    //if (CurentView == 7)
                    //{
                    //    Record.GoNextView(this, 33);
                    //    TitleName = "检修设置";
                    //}

                    if (CurentView == 16)//下一条
                    {
                        ButtonReset();
                    }

                    if (CurentView == 7 || CurentView == 47 || CurentView == 48 || CurentView == 49 || CurentView == 50 || CurentView == 52)
                    {
                        //ButtonReset();
                        buttonIsDown[7] = false;
                        //Record.GoNextView(this, 51);
                        //TitleName = "检修设置界面";
                    }
                    break;

                case 8:

                    if (CurentView == 2 || CurentView == 8 || CurentView == 9 || CurentView == 10 || CurentView == 11 || CurentView == 12 || CurentView == 13)
                    {
                        ButtonReset();
                        buttonIsDown[0] = true;
                        Record.GoNextView(this, 17);
                        TitleName = "冷却系统";
                    }
                    if (CurentView == 15)
                    {
                        ButtonReset();
                        buttonIsDown[8] = true;
                        TitleName = "历史故障";
                    }

                    if (CurentView == 16)//下一条
                    {
                        ButtonReset();
                    }

                    BtnList[18].Cursors();

                    if (CurentView == 4)
                    {
                        ButtonReset();
                        buttonIsDown[8] = true;
                        TitleName = "手动校时";
                    }
                    if (CurentView == 5 || CurentView == 26 || CurentView == 27 || CurentView == 28)
                    {
                        ButtonReset();
                        buttonIsDown[8] = true;
                        Record.GoNextView(this, 28);
                        TitleName = "牵引条件";
                    }

                    if (CurentView == 1)
                    {
                        if (_isConnect)
                        {
                            buttonIsDown[8] = true;
                        }
                    }

                    if (CurentView == 7 || CurentView == 47 || CurentView == 48 || CurentView == 49 || CurentView == 50 || CurentView == 51)
                    {
                        //ButtonReset();
                        buttonIsDown[8] = false;
                        //Record.GoNextView(this, 52);
                        //TitleName = "I/O监视界面";
                    }
                    break;
                case 9:
                    if (CurentView != 19 && CurentView != 20 && CurentView != 21 && CurentView != 23 && CurentView != 24 && CurentView != 39 && CurentView != 22)
                    {
                        ButtonReset();
                        buttonIsDown[9] = true;
                        Record.GoNextView(this, 1);
                        TitleName = "主界面";
                    }
                    if (CurentView == 33)
                    {
                        ButtonReset();
                        buttonIsDown[9] = true;
                        Record.GoNextView(this, 33);
                        TitleName = "检修模式";
                    }
                    if (CurentView == 24)
                    {
                        Record.Back(this, 1);
                    }
                    BtnList[19].Cursors();
                    V48_PressModeSet.PointOut = "";
                    break;
                case 10:
                    if (CurentView == 13)
                    {
                        StateInfo.num = 0;
                        append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[4], 0, 0);
                        break;
                    }
                    if (CurentView == 19 || CurentView == 20 || CurentView == 21 || CurentView == 22 || CurentView == 39)
                    {
                        Record.Back(this, 1);
                        //if (ContentDictionary.ContainsKey(ControlSeting._rowid))
                        //{
                        //    ContentDictionary.Remove(ControlSeting._rowid);
                        //}

                        if (PassWordDictionary.ContainsKey(ControlSeting._rowid))
                        {
                            PassWordDictionary.Remove(ControlSeting._rowid);
                        }
                        ControlSeting.PointOut = "";
                        break;
                    }
                    if (CurentView == 23)
                    {
                        EngineSetting.PointOut = "";
                        Record.Back(this, 1);
                    }
                    if (CurentView == 22)
                    {
                        ContentLists.Clear();
                    }

                    if (CurentView == 53 || CurentView == 46)
                    {
                        Record.Back(this, 1);
                    }

                    if(CurentView==55)
                        Record.GoNextView(this, 1);
                    break;
                case 11:
                    if (CurentView == 1)
                    {
                        ButtonReset();
                        buttonIsDown[9] = true;
                    }
                    if (Title.CurentView == 26)
                    {
                        RunCondtion.BtnisDown[0] = false;
                    }
                    if (Title.CurentView == 27)
                    {
                        RunCondtion.BtnisDown[1] = false;
                    }
                    if (Title.CurentView == 28)
                    {
                        RunCondtion.BtnisDown[2] = false;
                    }

                    if (CurentView == 53)
                    {
                        if (V53_LongDistanceCutOff.CurrentRow != 0) V53_LongDistanceCutOff.CurrentRow--;
                    }
                    break;
                case 12:
                    if (CurentView == 1)
                    {
                        ButtonReset();
                        buttonIsDown[9] = true;
                    }
                    //ButtonReset();
                    if (Title.CurentView == 26)
                    {
                        RunCondtion.BtnisDown[0] = true;
                    }
                    if (Title.CurentView == 27)
                    {
                        RunCondtion.BtnisDown[1] = true;
                    }
                    if (Title.CurentView == 28)
                    {
                        RunCondtion.BtnisDown[2] = true;
                    }

                    if (CurentView == 53)
                    {
                        if (V53_LongDistanceCutOff.CurrentRow != 15) V53_LongDistanceCutOff.CurrentRow++;
                    }
                    break;
                case 13:
                    if (CurentView == 1)
                    {
                        ButtonReset();
                        buttonIsDown[9] = true;
                    }
                    if (CurentView == 19 || CurentView == 39)
                    {
                        if (ControlSeting._rowid > 0)
                            ControlSeting._rowid--;
                    }
                    if (CurentView == 21)
                    {
                        ControlSeting.RunModel_Normal = 1;
                        ControlSeting.RunModel_Urgency = 0;
                    }
                    if (CurentView == 23)
                    {
                        EngineSetting.PartMutually = EngineSetting.PartMutuallyType.Open;
                    }

                    if (CurentView == 48)
                    {
                        V48_PressModeSet.IsCutOff = true;
                    }
                    if (CurentView == 49)
                    {
                        V49_Power.IsMode1 = true;
                    }
                    if (CurentView == 50)
                    {
                        V50_Blackout.IsMode1 = true;
                    }
                    break;
                case 14:
                    if (CurentView == 1)
                    {
                        ButtonReset();
                        buttonIsDown[9] = true;
                    }
                    if (CurentView == 19 || CurentView == 20 || CurentView == 21 ||
                         CurentView == 22)
                    {
                        if (ControlSeting._rowid < 7)
                            ControlSeting._rowid++;
                    }
                    if (CurentView == 39)
                    {
                        if (ControlSeting._rowid < 6)
                            ControlSeting._rowid++;

                    }
                    //ButtonReset();
                    //buttonIsDown[13] = false;
                    //buttonIsDown[14] = true;
                    if (CurentView == 21)
                    {
                        ControlSeting.RunModel_Normal = 0;
                        ControlSeting.RunModel_Urgency = 1;
                    }
                    if (CurentView == 23)
                    {
                        EngineSetting.PartMutually = EngineSetting.PartMutuallyType.Close;
                    }

                    if (CurentView == 46)
                    {
                        Password.PointOut = "";
                        Record.GoNextView(this, 0);
                        ButtonReset();
                        buttonIsDown[9] = true;
                    }

                    if (CurentView == 48)
                    {
                        V48_PressModeSet.IsCutOff = false;
                    }

                    if (CurentView == 49)
                    {
                        V49_Power.IsMode1 = false;
                    }
                    if (CurentView == 50)
                    {
                        V50_Blackout.IsMode1 = false;
                    }
                    break;
                case 15:
                    if (CurentView == 1)
                    {
                        ButtonReset();
                        buttonIsDown[9] = true;
                    }
                    if (CurentView == 19)
                    {
                        setWeightData();
                        ControlSeting.PointOut = "设置成功，请返回！";
                    }
                    if (CurentView == 20)
                    {
                        ControlSeting.DisplayValue = Current;
                        ControlSeting.PointOut = "设置成功，请返回！";
                    }
                    if (CurentView == 23)
                    {
                        EngineSetting.PointOut = "设置成功，请返回！";
                        SendEngineSetingData(EngineSetting.PartMutually);
                    }

                    if (CurentView == 21)
                    {
                        sendRunMode();
                        ControlSeting.PointOut = "设置成功，请返回！";
                    }
                    if (CurentView == 31)
                    {
                        sendLinYuMode();
                    }
                    if (CurentView == 32)
                    {
                        sendTest();
                    }
                    if (CurentView == 45)
                    {
                        sendDingLunMode();
                    }

                    if (CurentView == 46)
                    {
                        if (PassWordDictionary.Count < 6) Password.PointOut = "密码错误！";
                        else
                        {
                            bool isTrue = true;
                            foreach (var i in PassWordDictionary.Values)
                            {
                                if (i != "1")
                                {
                                    isTrue = false;
                                    break;
                                }
                            }
                            if (isTrue)
                            {
                                PassWordDictionary.Clear();
                                Password.PointOut = "";
                                ButtonReset();
                                TitleName = "检修模式界面";
                                Record.GoNextView(this, 7);
                            }
                            else
                            {
                                PassWordDictionary.Clear();
                                Password.PointOut = "密码错误！";
                            }
                        }
                    }

                    if (CurentView == 48)
                    {
                        V48_PressModeSet.PointOut = "设置成功，请返回！";
                        sendPressMode();
                    }

                    if (CurentView == 55)
                    {
                        ButtonReset();
                        //buttonIsDown[9] = true;
                        Record.GoNextView(this, 1);
                        TitleName = "主界面";
                    }
                    break;
                case 16:
                    ButtonReset();
                    TitleName = "冷却系统";
                    buttonIsDown[0] = true;
                    Record.GoNextView(this, 17);
                    break;
                case 23:
                    //if (CurentView == 1)
                    //{
                    //    ButtonReset();
                    //    buttonIsDown[9] = true;
                    //}

                    ButtonReset();
                    buttonIsDown[6] = true;
                    TitleName = "当前故障";
                    Record.GoNextView(this, 15);
                    //append_postCmd(CmdType.ChangePage, 15, 0, 0);
                    break;
                case 24:
                    //if (CurentView == 1)
                    //{
                    //    ButtonReset();
                    //    buttonIsDown[9] = true;
                    //}
                    //ButtonReset();
                    //buttonIsDown[24] = true;
                    if (CurentView == 15 && FaultInfo.MenuId == 0)
                    {
                        ButtonReset();
                        ButtonName = "司机模式";
                        //buttonIsDown[2] = true;
                        Record.GoNextView(this, 16);
                        TitleName = "处理提示界面";
                    }
                    //append_postCmd(CmdType.ChangePage, 16, 0, 0);
                    break;
            }
        }
    }
}
