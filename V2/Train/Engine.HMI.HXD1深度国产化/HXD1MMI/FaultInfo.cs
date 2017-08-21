using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 错误的详细信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class FaultInfo : baseClass
    {
        private List<RectText> _downRectTextList = new List<RectText>();
        private Dictionary<int, List<string>> _infoAlert = new Dictionary<int, List<string>>();
        private Dictionary<int, List<string>> _brakeInfoAlert = new Dictionary<int, List<string>>();

        private List<string> _infoList = new List<string>();
        private int _infoIndex = 0;
        private DateTime _infoStartTime;

        private List<string> _brakeList = new List<string>();
        private int _brakeIndex = 0;
        private DateTime _brakeStartTime;

        public static int index = 0;
        private DateTime _startTime;
        public override string GetInfo()
        {
            return "故障信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _downRectTextList.Add(new RectText(new Rectangle(3, Common.InitialPosY + 495, 225, 50), "", 14, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true));

            _downRectTextList.Add(new RectText(new Rectangle(228, Common.InitialPosY + 495, 408, 50), "", 14, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true));

            _downRectTextList.Add(new RectText(new Rectangle(636, Common.InitialPosY + 495, 156, 50), "", 14, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true, null, true));

            var file = Path.Combine(AppPaths.ConfigDirectory, "辅机测试.txt");

            try
            {
                var allLines = File.ReadAllLines(file, Encoding.Default);

                ParseLines(allLines);
            }
            catch (Exception e)
            {
                LogMgr.Fatal(string.Format("Can not read file : {0} \r\n {1}", file, e));
            }

            return true;
        }

        private void ParseLines(string[] allLines)
        {
            foreach (var cStr in allLines)
            {
                if (cStr.Trim() != "")
                {
                    string[] str = cStr.Split(';');
                    if (str.Length == 4)
                    {
                        if (int.Parse(str[0]) == 1)
                            _infoAlert.Add(int.Parse(str[1]), new List<string>() { str[2], str[3] });


                        if (int.Parse(str[0]) == 2)
                            _brakeInfoAlert.Add(int.Parse(str[1]), new List<string>() { str[2], str[3] });

                    }
                }
            }
        }
        int fealutFlickerIndex = 0;
        public void GetValue()
        {
            if (HisteryFault.CurrentFault.Count <= 0)
            {
                _downRectTextList[2].BackgroundColor = Color.Black;
                _downRectTextList[2].Text = " ";
                return;
            }
            var his = HisteryFault.CurrentFault.FindAll(f => f.EndedTime == new DateTime(1, 1, 1) && BoolList[f.LogicalBit]);
            if (his == null || his.Count == 0)
            {
                fealutFlickerIndex = 0;
                _downRectTextList[2].BackgroundColor = Color.Black;
                _downRectTextList[2].Text = " ";
                return;
            }
            var ts = DateTime.Now - _startTime;
            if (ts.Seconds <= 2)
            {
                return;
            }

            _startTime = DateTime.Now;
            if (_downRectTextList[2].BackgroundColor == Color.Red)
            {
                _downRectTextList[2].BackgroundColor = Color.Black;
                _downRectTextList[2].Text = " ";
            }
            else
            {
                fealutFlickerIndex++;
                if (his.Count <= fealutFlickerIndex)
                {
                    fealutFlickerIndex = 0;
                }
                _downRectTextList[2].BackgroundColor = Color.Red;
                _downRectTextList[2].Text = his[fealutFlickerIndex].XXXXXX;

            }
        }

        public override void paint(Graphics dcGs)
        {
            try
            {
                GetValue();

                DrawBrakeInfoAlert();

                DrawInfoAlert();

                DrawInfo();

                DrawBrake();

                if (_downRectTextList[0]._isSetValue)
                {
                    _downRectTextList[0].BackgroundColor = Color.Orange;
                    _downRectTextList[0].TextColor = Color.Black;
                }
                else
                {
                    _downRectTextList[0].BackgroundColor = Color.Black;
                    _downRectTextList[0].TextColor = Color.White;
                }


                if (_downRectTextList[1]._isSetValue)
                {
                    _downRectTextList[1].BackgroundColor = Color.White;
                    _downRectTextList[1].TextColor = Color.Black;
                }
                else
                {
                    _downRectTextList[1].BackgroundColor = Color.Black;
                    _downRectTextList[1].TextColor = Color.White;
                }

                foreach (var v in _downRectTextList)
                {
                    v.OnDraw(dcGs);
                }
            }
            catch (Exception e)
            {
                LogMgr.Error(e.ToString());
            }
        }

        private void DrawBrake()
        {
            if (_brakeList.Count > 0)
            {
                _downRectTextList[1].Text = _brakeList[_brakeIndex];
                TimeSpan ts = DateTime.Now - _brakeStartTime;
                if (ts.Seconds > 3)
                {
                    _brakeStartTime = DateTime.Now;
                    _brakeIndex++;
                    if (_brakeIndex == _brakeList.Count)
                    {
                        _brakeIndex = 0;
                    }
                }
            }
        }

        private void DrawInfo()
        {
            if (_infoList.Count > 0)
            {
                _downRectTextList[0].Text = _infoList[_infoIndex];
                TimeSpan ts = DateTime.Now - _infoStartTime;
                if (ts.Seconds > 3)
                {
                    _infoStartTime = DateTime.Now;
                    _infoIndex++;
                    if (_infoIndex == _infoList.Count)
                    {
                        _infoIndex = 0;
                    }
                }
            }
        }

        private void DrawInfoAlert()
        {
            foreach (var v in _infoAlert)
            {
                if (BoolList[v.Key])
                {
                    string text = v.Value[0];
                    if (v.Value[1] != "")
                    {
                        text += "\n" + v.Value[1];
                    }
                    if (!_infoList.Contains(text))
                    {
                        _infoList.Add(text);
                    }
                }
                else
                {
                    string text = v.Value[0];
                    if (v.Value[1] != "")
                    {
                        text += "\n" + v.Value[1];
                    }
                    if (_infoList.Contains(text))
                    {
                        _infoList.Remove(text);
                    }
                }
            }
        }

        private void DrawBrakeInfoAlert()
        {
            foreach (var v in _brakeInfoAlert)
            {
                if (BoolList[v.Key])
                {
                    string text = v.Value[0];
                    if (v.Value[1] != "")
                    {
                        text += "\n" + v.Value[1];
                    }
                    if (!_brakeList.Contains(text))
                    {
                        _brakeList.Add(text);
                    }
                }
                else
                {
                    string text = v.Value[0];
                    if (v.Value[1] != "")
                    {
                        text += "\n" + v.Value[1];
                    }
                    if (_brakeList.Contains(text))
                    {
                        _brakeList.Remove(text);
                    }
                }
            }
        }
    }
}
