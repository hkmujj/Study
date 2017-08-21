using System;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD1.DeepDomestic
{

    /// <summary>
    /// 主要负责后台接受命令
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ReceiveRequest : baseClass
    {
        public static CommonUtil.Model.IReadOnlyDictionary<ButtonType, ButtonInfo> ButtonInfoDictionary { private set; get; }


        public override string GetInfo()
        {
            return "接受硬IO数据";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            var indexs = Enumerable.Range(799, 27).ToList();
            // 故障
            //indexs.Add(823);

            ReceiveRequest.ButtonInfoDictionary = Enum.GetValues(typeof (ButtonType))
                .Cast<ButtonType>()
                .ToDictionary(kvp => kvp, kvp => new ButtonInfo(kvp, indexs[(int) kvp]))
                .AsReadOnlyDictionary();

            return true;
        }

        public void ClearDownStates()
        {
            foreach (var i in ReceiveRequest.ButtonInfoDictionary.Values)
            {
                i.State = MouseState.MouseUp;
            }
        }

        public override void paint(Graphics dcGs)
        {

        }

        private void SendValue()
        {
            if (Alert.AlertModelList[0].SendValue == 0)
            {
                append_postCmd(CmdType.SetBoolValue, 2402, 1, 0);
                append_postCmd(CmdType.SetBoolValue, 2403, 0, 0);
            }
            else
            {
                append_postCmd(CmdType.SetBoolValue, 2403, 1, 0);
                append_postCmd(CmdType.SetBoolValue, 2402, 0, 0);
            }
            if (Alert.AlertModelList[1].SendValue == 0)
            {
                append_postCmd(CmdType.SetBoolValue, 2405, 0, 0);
                append_postCmd(CmdType.SetBoolValue, 2404, 1, 0);
            }
            else
            {
                append_postCmd(CmdType.SetBoolValue, 2404, 0, 0);
                append_postCmd(CmdType.SetBoolValue, 2405, 1, 0);
            }
            if (Alert.AlertModelList[2].SendValue == 0)
            {
                append_postCmd(CmdType.SetBoolValue, 2407, 1, 0);
                append_postCmd(CmdType.SetBoolValue, 2406, 0, 0);
            }
            else
            {
                append_postCmd(CmdType.SetBoolValue, 2406, 1, 0);
                append_postCmd(CmdType.SetBoolValue, 2407, 0, 0);
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            SendValue();

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.CurrentFault].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.CurrentFault].State == MouseState.MouseDown) //故障信息
            {
                append_postCmd(CmdType.ChangePage, 13, 0, 0);
            }

            switch (nParaB)
            {
                case 1: //主界面
                    RequestMainPage();
                    break;
                case 2: //主要数据界面
                case 4: //温度
                case 6: //控制系统
                case 7: //辅助系统
                case 12: //库内动车
                case 11: //辅机测试
                    RequestAssMachineTest();
                    break;

                case 5: //牵引数据界面
                    RequestTowData();
                    break;

                case 8: //牵引封锁A
                    RequestTowLockA();
                    break;
                case 31: //牵引封锁B
                    RequestTowLockB();
                    break;
                case 9: //主断A
                    RequestMainSwitchA();
                    break;
                case 32: //主断B
                    RequestMainSwitchB();
                    break;
                case 10: //受电工B
                    RequestPantographB();
                    break;
                case 33:
                    RequestPatographA();
                    break;
                case 16:
                    RequestTrainConfig();
                    break;
                case 34: //当前故障A
                    RequestCurrentFaultA();
                    break;

                case 14: //所有故障
                    RequestAllFault();
                    break;
                case 15:
                    RequestHistoryFault();
                    break;
                case 13: //当前故障B
                    RequestCurrentFaultB();
                    break;
                case 35: //所有故障B
                    ReqeustAllFaultB();
                    break;
                case 24:
                    RequestView24();
                    break;
                case 23:
                    RequestView23();

                    break;
            }

            UpdateFaults();
        }

        private void UpdateFaults()
        {
            UpdateFaultA();

            CurrentFaultA.SortedFaultList.Sort();

            UpdateFaultB();

            CurrentFaultB.SortedFaultList.Sort();
        }

        private void UpdateFaultB()
        {
            foreach (Fault t in HisteryFault.EventListB)
            {
                if (BoolList[t.LogicalBit])
                {
                    var isExist = CurrentFaultB.SortedFaultList.Any(v => v.LogicalBit == t.LogicalBit);
                    if (!isExist)
                    {
                        var fault = new Fault(t) {HappenedTime = DateTime.Now};
                        FaultFacode.Instance.AddFaultB(fault);
                    }
                }
                else
                {
                    foreach (var v in CurrentFaultB.SortedFaultList.Where(v => v.LogicalBit == t.LogicalBit))
                    {
                        v.EndedTime = DateTime.Now;

                        CurrentFaultB.SortedFaultList.Remove(v);

                        break;
                    }
                }
            }
        }

        private void UpdateFaultA()
        {
            foreach (Fault t in HisteryFault.EventListA)
            {
                if (BoolList[t.LogicalBit])
                {
                    bool isExist = CurrentFaultA.SortedFaultList.Any(v => v.LogicalBit == t.LogicalBit);
                    if (!isExist)
                    {
                        var fault = new Fault(t) {HappenedTime = DateTime.Now};
                        FaultFacode.Instance.AddFaultA(fault);
                    }
                }
                else
                {
                    foreach (var v in CurrentFaultA.SortedFaultList.Where(v => v.LogicalBit == t.LogicalBit))
                    {
                        v.EndedTime = DateTime.Now;

                        CurrentFaultA.SortedFaultList.Remove(v);

                        break;
                    }
                }
            }
        }

        private void RequestPatographA()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].State == MouseState.MouseDown) //Shou Dian Gong  B
            {
                append_postCmd(CmdType.ChangePage, 10, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].State == MouseState.MouseDown) //牵引状态
            {
                append_postCmd(CmdType.ChangePage, 31, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].State == MouseState.MouseDown) // ZhuDuan A
            {
                append_postCmd(CmdType.ChangePage, 32, 0, 0);
            }


            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown) //主界面
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestTrainConfig()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].State == MouseState.MouseDown) //机车配置
            {
                append_postCmd(CmdType.ChangePage, 16, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].State == MouseState.MouseDown) //列车参数
            {
                append_postCmd(CmdType.ChangePage, 21, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn3].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn3].State == MouseState.MouseDown) //轮缘融化
            {
                append_postCmd(CmdType.ChangePage, 22, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn4].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn4].State == MouseState.MouseDown) //分相/警惕
            {
                append_postCmd(CmdType.ChangePage, 23, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].State == MouseState.MouseDown) //车顶开关
            {
                append_postCmd(CmdType.ChangePage, 24, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestHistoryFault()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 13, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn8].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn8].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 14, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 15, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestView23()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Enter].LogicIndex])
            {
                append_postCmd(CmdType.SetBoolValue, Alert.AlertModelList[2].SelectIndex == 0 ? 2407 : 2406, 1,
                    0);
                append_postCmd(CmdType.SetBoolValue, Alert.AlertModelList[2].SelectIndex == 0 ? 2406 : 2407, 0,
                    0);
            }
        }

        private void RequestView24()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void ReqeustAllFaultB()
        {
            {
                if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].LogicIndex] ||
                    ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].State == MouseState.MouseDown)
                {
                    append_postCmd(CmdType.ChangePage, 14, 0, 0);
                }

                if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].LogicIndex] ||
                    ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].State == MouseState.MouseDown)
                {
                    append_postCmd(CmdType.ChangePage, 34, 0, 0);
                }

                if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].LogicIndex] ||
                    ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].State == MouseState.MouseDown)
                {
                    append_postCmd(CmdType.ChangePage, 15, 0, 0);
                }

                if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                    ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown)
                {
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                }
            }
        }

        private void RequestCurrentFaultB()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 34, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn8].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn8].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 35, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 15, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }

            foreach (Fault t in HisteryFault.EventListB)
            {
                if (BoolList[t.LogicalBit])
                {
                    var isExist = CurrentFaultB.SortedFaultList.Any(v => v.LogicalBit == t.LogicalBit);
                    if (isExist)
                    {
                        continue;
                    }
                    Fault fault = new Fault(t) {HappenedTime = DateTime.Now};
                    FaultFacode.Instance.AddFaultB(fault);
                }
                else
                {
                    foreach (var v in CurrentFaultB.SortedFaultList)
                    {
                        if (v.LogicalBit == t.LogicalBit)
                        {
                            v.EndedTime = DateTime.Now;

                            CurrentFaultB.SortedFaultList.Remove(v);

                            break;
                        }
                    }
                }
            }
            CurrentFaultB.SortedFaultList.Sort();
        }

        private void RequestAllFault()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 35, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 13, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 15, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestCurrentFaultA()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 13, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn8].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn8].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 14, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 15, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestPantographB()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].State == MouseState.MouseDown) //Shou Dian Gong  B
            {
                append_postCmd(CmdType.ChangePage, 33, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].State == MouseState.MouseDown) //牵引状态
            {
                append_postCmd(CmdType.ChangePage, 8, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].State == MouseState.MouseDown) // ZhuDuan A
            {
                append_postCmd(CmdType.ChangePage, 9, 0, 0);
            }


            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown) //主界面
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestMainSwitchB()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].State == MouseState.MouseDown) //zhu duan A
            {
                append_postCmd(CmdType.ChangePage, 9, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].State == MouseState.MouseDown) //牵引状态
            {
                append_postCmd(CmdType.ChangePage, 31, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].State == MouseState.MouseDown) //受电弓
            {
                append_postCmd(CmdType.ChangePage, 33, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown) //主界面
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestMainSwitchA()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].State == MouseState.MouseDown) //zhuduanB
            {
                append_postCmd(CmdType.ChangePage, 32, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].State == MouseState.MouseDown) //牵引状态
            {
                append_postCmd(CmdType.ChangePage, 8, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].State == MouseState.MouseDown) //受电弓
            {
                append_postCmd(CmdType.ChangePage, 10, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown) //主界面
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestTowLockB()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 8, 0, 0);
            }


            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].State == MouseState.MouseDown) //主断状态
            {
                append_postCmd(CmdType.ChangePage, 32, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].State == MouseState.MouseDown) //受电弓
            {
                append_postCmd(CmdType.ChangePage, 33, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown) //主界面
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestTowLockA()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 31, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].State == MouseState.MouseDown) //主断状态
            {
                append_postCmd(CmdType.ChangePage, 9, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].State == MouseState.MouseDown) //受电弓
            {
                append_postCmd(CmdType.ChangePage, 10, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown) //主界面
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestTowData()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestAssMachineTest()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 2, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn2].State == MouseState.MouseDown) //切到温度
            {
                append_postCmd(CmdType.ChangePage, 4, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn3].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn3].State == MouseState.MouseDown) //控制系统
            {
                append_postCmd(CmdType.ChangePage, 6, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn4].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn4].State == MouseState.MouseDown) //辅助系统
            {
                append_postCmd(CmdType.ChangePage, 7, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn7].State == MouseState.MouseDown) //封锁条件
            {
                append_postCmd(CmdType.ChangePage, 8, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn8].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn8].State == MouseState.MouseDown) //库内动车  
            {
                append_postCmd(CmdType.ChangePage, 12, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn9].State == MouseState.MouseDown) //切到辅机测试
            {
                append_postCmd(CmdType.ChangePage, 11, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn10].State == MouseState.MouseDown) //切到主界面
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        private void RequestMainPage()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn1].State == MouseState.MouseDown) //主要数据
            {
                append_postCmd(CmdType.ChangePage, 2, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn6].State == MouseState.MouseDown) //牵引数据
            {
                append_postCmd(CmdType.ChangePage, 5, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].LogicIndex] ||
                ReceiveRequest.ButtonInfoDictionary[ButtonType.Btn5].State == MouseState.MouseDown) //参数配置
            {
                append_postCmd(CmdType.ChangePage, 16, 0, 0);
            }
        }
    }
}
