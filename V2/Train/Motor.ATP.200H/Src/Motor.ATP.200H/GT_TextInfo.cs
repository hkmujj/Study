using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 文本信息提示框
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class TextInfo : baseClass
    {
        #region  私有字段
        private static RectText[] G_Text_Time = new RectText[5];
        private static RectText[] G_Text_Info = new RectText[5];
        public static SortedList<int, string> Total_Infos = new SortedList<int, string>();//从文本文件中存储入的所有的文本信息种类
        public static List<ConfigInfo> ConfigInfos = new List<ConfigInfo>();

        /// <summary>
        /// 当前触发的文本信息
        /// </summary>
        private int _currentTriggerIndex1 = -1;
        private int _lastTriggerIndex1 = -1;

        private int _lastTriggerIndex2 = -1;

        private static SortedList<int, Text_Info> _happened_Infos = new SortedList<int, Text_Info>();

        #endregion

        #region 重载方法
        public override string GetInfo()
        {
            return "文本信息滚动显示";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            nErrorObjectIndex = -1;

            LoadTestFile();

            return true;
        }

        private void LoadTestFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\文本信息.txt");
            if (File.Exists(file))
            {
                var allLines = File.ReadAllLines(file, Encoding.Default);
                foreach (var str in allLines.Select(s => s.Split(';')))
                {
                    ParserLine(str);
                }
            }
        }

        private void ParserLine(string[] Str)
        {
            switch (Str.Length)
            {
                case 2:
                    try
                    {
                        Total_Infos.Add(Convert.ToInt32(Str[0]), Str[1]);
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                case 3:
                    {
                        var configInfo = new ConfigInfo { ReceiveIndex = Convert.ToInt32(Str[0]), ShowInfo = Str[1], SendIndex = Convert.ToInt32(Str[2]) };
                        ConfigInfos.Add(configInfo);
                    }
                    break;
            }
        }

        public override void paint(Graphics g)
        {
            getValue();
            OnDraw(g);
        }
        #endregion

        #region 私有方法
        private void InitData()
        {
            for (int i = 0; i < 5; i++)
            {
                G_Text_Time[i] = new RectText();
                G_Text_Time[i].SetBkColor(0, 0, 0);
                if (i == 0)
                {
                    G_Text_Time[i].SetTextColor(255, 255, 255);
                }
                else
                {
                    G_Text_Time[i].SetTextColor(90, 90, 101);
                }
                G_Text_Time[i].SetTextStyle(12, FormatStyle.DirectionLeftToRight, true, "宋体");
                G_Text_Time[i].SetTextRect(Common2D.RectE[i + 4].X + 2, Common2D.RectE[i + 4].Y + 8, 80, Common2D.RectE[i + 4].Height - 12);
            }

            for (int i = 0; i < 5; i++)
            {
                G_Text_Info[i] = new RectText();
                G_Text_Info[i].SetBkColor(0, 0, 0);
                if (i == 0)
                {
                    G_Text_Info[i].SetTextColor(255, 255, 255);
                }
                else
                {
                    G_Text_Info[i].SetTextColor(90, 90, 101);
                }
                G_Text_Info[i].SetTextStyle(14, FormatStyle.DirectionLeftToRight, true, "宋体");
                G_Text_Info[i].SetTextRect(Common2D.RectE[i + 4].X + 78, Common2D.RectE[i + 4].Y + 6, 200, Common2D.RectE[i + 4].Height - 10);
            }

        }

        public void getValue()
        {
            #region 刷新第一位文本信息
            int _currentTriggerIndex = Convert.ToInt32(FloatList[158]);
            if (Total_Infos.ContainsKey(_currentTriggerIndex))
            {
                if (_currentTriggerIndex != _lastTriggerIndex1)
                {
                    Text_Info text_Info = new Text_Info();
                    text_Info.TriggerTime = DateTime.Now.ToLongTimeString();
                    text_Info.TextInfo = Total_Infos[_currentTriggerIndex];
                    _happened_Infos.Add(_happened_Infos.Count, text_Info);
                    _lastTriggerIndex1 = _currentTriggerIndex;
                }
            }
            else
            {
                _lastTriggerIndex1 = -1;
            }
            #endregion

            #region 刷新第二位
            int currentTriggerIndex2 = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]);
            if (Total_Infos.ContainsKey(currentTriggerIndex2))//若当前故障集合中有该编号 
            {
                if (currentTriggerIndex2 != _lastTriggerIndex2)
                {
                    Text_Info text_Info = new Text_Info();
                    text_Info.TriggerTime = DateTime.Now.ToLongTimeString();
                    text_Info.TextInfo = Total_Infos[currentTriggerIndex2];
                    _happened_Infos.Add(_happened_Infos.Count, text_Info);
                    _lastTriggerIndex2 = currentTriggerIndex2;
                }
            }
            #endregion

            #region 根据是否处于确认模式更新 文本框
            if (sinal.isConfigFlag)//处于确认模式
            {
                G_Text_Time[0].SetText(string.Empty);
                G_Text_Info[0].SetText(string.Empty);

                for (int i = index; i < 4 + index; i++)
                {
                    if (i < _happened_Infos.Count)
                    {
                        G_Text_Time[i + 1 - index].SetText(_happened_Infos[_happened_Infos.Count - i - 1].TriggerTime);
                        G_Text_Info[i + 1 - index].SetText(_happened_Infos[_happened_Infos.Count - i - 1].TextInfo);
                    }
                    else
                    {
                        G_Text_Time[i + 1 - index].SetText(string.Empty);
                        G_Text_Info[i + 1 - index].SetText(string.Empty);
                    }
                }
            }
            else
            {
                for (int i = index; i < 5 + index; i++)
                {
                    if (i < _happened_Infos.Count)
                    {
                        G_Text_Time[i - index].SetText(_happened_Infos[_happened_Infos.Count - i - 1].TriggerTime);
                        G_Text_Info[i - index].SetText(_happened_Infos[_happened_Infos.Count - i - 1].TextInfo);
                    }
                    else
                    {
                        G_Text_Time[i - index].SetText(string.Empty);
                        G_Text_Info[i - index].SetText(string.Empty);
                    }
                }
            }
            #endregion
        }

        public void OnDraw(Graphics g)
        {
            for (int i = 0; i < 5; i++)
            {
                G_Text_Time[i].OnDraw(g);
                G_Text_Info[i].OnDraw(g);
            }
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 清空当前所有消息记录
        /// </summary>
        public static void ClearCurrentInforRecords()
        {
            _happened_Infos.Clear();
        }

        private static int index = 0;

        private static void SetDisplayText()
        {
            for (int i = index; i < 5 + index; i++)
            {
                G_Text_Info[i - index].SetText(_happened_Infos[index].TextInfo);
                G_Text_Time[i - index].SetText(_happened_Infos[index].TriggerTime);
            }
        }
        /// <summary>
        /// 下一个
        /// </summary>
        public static void Next()
        {
            if (_happened_Infos.Count() > 5 + index)
            {
                index++;
            }
            SetDisplayText();
        }

        public static void Last()
        {
            if (_happened_Infos.Count() > 5 && index != 0)
            {
                index--;
            }
            SetDisplayText();
        }
        #endregion
    }

    
    /// <summary>
    /// 确认信息
    /// </summary>
    public class ConfigInfo
    {
        public int ReceiveIndex { get; set; }
        public string ShowInfo { get; set; }
        public int SendIndex { get; set; }
    }
}
