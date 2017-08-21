using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HXD1D.控制设置;
using HXD1D.故障界面;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using HXD1D.运行条件;

namespace HXD1D.Titlte
{
    [GksDataType(DataType.isMMIObjectClass)]
    public partial class Title
    {
        public static List<int> CurrentTest = new List<int>();

        /// <summary>
        /// 给当前视图赋值
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {

            CurentView = nParaB;
            RunView();
            if (nParaA == 2 && !EngineSetting.IsSetFenX)
            {
                EngineSetting.PartMutually = EngineSetting.PartMutuallyType.Open;
                SendEngineSetingData(EngineSetting.PartMutually);
            }
            else if (nParaA == 2)
            {
                SendEngineSetingData(EngineSetting.PartMutually);
            }

        }
        /// <summary>
        /// 按键复位
        /// </summary>
        public static void ButtonReset()
        {
            for (int i = 0; i < 16; i++)
            {
                buttonIsDown[i] = false;
            }
        }

        private string _weight = string.Empty;
        private string _strdeScription = string.Empty;
        private string _axleWeight = string.Empty;

        /// <summary>
        /// 设置载重，类型，轴重（唐林20150803）
        /// </summary>
        private void setWeightData()
        {
            _weight = string.Empty;
            Int32 temp = -1;
            temp = ContentDictionary.Keys.ToList().Find(a => a < 5 && a > 0);
            if (temp != 0 || ContentDictionary.ContainsKey(0))
            {
                for (int i = 0; i < 5; i++)
                {
                    if (ControlSeting.DisplayDictionary.ContainsKey(i))
                    {
                        ControlSeting.DisplayDictionary.Remove(i);
                        ControlSeting.DisplayDictionary.Add(i,
                            ContentDictionary.ContainsKey(i) ? ContentDictionary[i] : 0);
                    }
                    else
                    {
                        ControlSeting.DisplayDictionary.Add(i,
                            ContentDictionary.ContainsKey(i) ? ContentDictionary[i] : 0);
                    }
                    _weight += ContentDictionary.ContainsKey(i) ? ContentDictionary[i].ToString("0") : "0";
                }
                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, Convert.ToSingle(_weight));
            }

            if (ContentDictionary.ContainsKey(5))
            {
                if (ControlSeting.DisplayDictionary.ContainsKey(5))
                {
                    ControlSeting.DisplayDictionary.Remove(5);
                    ControlSeting.DisplayDictionary.Add(5,
                        ContentDictionary.ContainsKey(5) ? ContentDictionary[5] : 0);
                }
                else
                {
                    ControlSeting.DisplayDictionary.Add(5,
                        ContentDictionary.ContainsKey(5) ? ContentDictionary[5] : 0);
                }
                _strdeScription = ContentDictionary.ContainsKey(5) ? ContentDictionary[5].ToString("0") : "0";
                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[1], 0, Convert.ToSingle(_strdeScription));
            }

            if (ContentDictionary.ContainsKey(6))
            {
                if (ControlSeting.DisplayDictionary.ContainsKey(6))
                {
                    ControlSeting.DisplayDictionary.Remove(6);
                    ControlSeting.DisplayDictionary.Add(6,
                        ContentDictionary.ContainsKey(6) ? ContentDictionary[6] : 0);
                }
                else
                {
                    ControlSeting.DisplayDictionary.Add(6,
                        ContentDictionary.ContainsKey(6) ? ContentDictionary[6] : 0);
                }
                _axleWeight = ContentDictionary.ContainsKey(6) ? ContentDictionary[6].ToString("0") : "0";
                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, Convert.ToSingle(_axleWeight));
            }

            //ContentDictionary.Clear();
            //ControlSeting._rowid = 0;
        }

        /// <summary>
        /// 发送运行界面模式（唐林20150803）
        /// </summary>
        private void sendRunMode()
        {
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[7], ControlSeting.RunModel_Normal, 0);
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[8], ControlSeting.RunModel_Urgency, 0);
        }

        /// <summary>
        /// 发送分相测试（唐林20150803）
        /// </summary>
        private void sendTest()
        {
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[9], MaintainTesting.KuNeiFenXiangTest_Over, 0);
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[10], MaintainTesting.KuNeiFenXiangTest_Start, 0);
        }

        /// <summary>
        /// 发送淋雨模式（唐林20150803）
        /// </summary>
        private void sendLinYuMode()
        {
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[13], MaintainTesting.LinYuMode_Cancel, 0);
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[14], MaintainTesting.LinYuMode_Active, 0);
        }

        /// <summary>
        /// 发送顶轮模式（唐林20150803）
        /// </summary>
        private void sendDingLunMode()
        {
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[11], MaintainTesting.DingLunMode_Cancel, 0);
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[12], MaintainTesting.DingLunMode_Active, 0);
        }

        /// <summary>
        /// 发送空压机模式
        /// </summary>
        private void sendPressMode()
        {
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[15], V48_PressModeSet.IsCutOff ? 1 : 0, 0);
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[16], V48_PressModeSet.IsCutOff ? 0 : 1, 0);
        }

        private void SendLoadHXDData()
        {
            //载重
            float weight = 0f;
            //种类
            float description = 0f;
            //轴重
            float axleWeight = 0f;
            string strweight = string.Empty;
            string strdescription = string.Empty;
            string straxleWeight = string.Empty;
            for (int i = 0; i < 7; i++)
            {
                if (ControlSeting.DisplayDictionary.ContainsKey(i))
                {
                    ControlSeting.DisplayDictionary.Remove(i);
                    ControlSeting.DisplayDictionary.Add(i, ContentDictionary.ContainsKey(i) ? ContentDictionary[i] : 0);
                }
                else
                {
                    ControlSeting.DisplayDictionary.Add(i, ContentDictionary.ContainsKey(i) ? ContentDictionary[i] : 0);
                }
                if (i <= 4)
                {
                    strweight += ContentDictionary.ContainsKey(i) ? ContentDictionary[i].ToString("0") : "0";
                    continue;
                }
                if (i == 5)
                {
                    strdescription = ContentDictionary.ContainsKey(i) ? ContentDictionary[i].ToString("0") : "0";
                    continue;
                }
                if (i == 6)
                {
                    straxleWeight = ContentDictionary.ContainsKey(i) ? ContentDictionary[i].ToString("0") : "0";
                }
            }
            //将字符串格式化为Float类型
            weight = float.Parse(strweight.Trim());
            description = float.Parse(strdescription.Trim());
            axleWeight = float.Parse(straxleWeight.Trim());
            //清除集合
            ContentDictionary.Clear();
            ControlSeting._rowid = 0;
            ////发送数据
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, weight);
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[1], 0, description);
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, axleWeight);
        }

        private void SendEngineSetingData(EngineSetting.PartMutuallyType PartMutually)
        {
            switch (PartMutually)
            {
                case EngineSetting.PartMutuallyType.Open:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                    break;
                case EngineSetting.PartMutuallyType.Close:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                    break;
            }
        }
        private void ReadFaultFile()
        {
            string file = Path.Combine(RecPath + @"\..\config\故障信息.txt");
            string[] allLine = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in allLine.Skip(1))
            {
                string[] str = cStr.Split(new[] { ';' });
                int i = 0;
                String[] temp = new String[9];
                if (str.Length == 9)
                {
                    foreach (string s in str.Where(s => s.Trim() != ""))
                    {
                        if (i < 9)
                            temp[i] = s;
                        i++;
                    }
                    if (i == 9)
                    {
                        _allMsgList.Add(int.Parse(temp[0]), temp);
                    }
                }
            }
        }


        private void RecAndDispMsg()
        {
            foreach (var index in _allMsgList.Keys)
            {
                if (BoolList[index] && !_faultLogicIDList.Contains(index))
                {
                    var masgtemp = new Faul
                                   {
                                       MsgLogicID = index,
                                       Level = _allMsgList[index][1],
                                       MsgContent = _allMsgList[index][4],
                                       MsgID = _allMsgList[index][5],
                                       Code = _allMsgList[index][3],
                                       FaultSolutionStr = _allMsgList[index][6],
                                       Reason = _allMsgList[index][7],
                                       Result = _allMsgList[index][8],
                                       MsgReceiveTime = DateTime.Now
                                   };
                    _msgInfList.AddNewMsg(masgtemp);
                    _faultLogicIDList.Add(index);

                }
                else if (_faultLogicIDList.Contains(index) && !BoolList[index])
                {
                    _msgInfList.MsgOver(index);
                    _faultLogicIDList.Remove(index);
                }

            }

        }

        private static void MsgSort()
        {
            foreach (Faul t in MsgInfList.CurrentMsgList)
            {
                DateTime time = MsgInfList.CurrentMsgList[0].MsgReceiveTime;
                level = MsgInfList.CurrentMsgList[0].Level;
                if (Char.Parse(t.Level) > Char.Parse(level))
                {
                    Content = t.MsgContent;
                    Tishi = t.FaultSolutionStr;
                }
                if (Char.Parse(t.Level) != Char.Parse(level) || time > t.MsgReceiveTime)
                {
                    continue;
                }
                Content = t.MsgContent;
                Tishi = t.FaultSolutionStr;
            }
        }
    }
}
