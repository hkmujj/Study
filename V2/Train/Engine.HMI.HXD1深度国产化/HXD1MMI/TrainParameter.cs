using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 机车参数
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrainParameter : baseClass
    {
        private Weight _weight = new Weight();

        private Info _type = new Info(new Rectangle(10, 185, 308, 145), "车辆类型", new List<string>() { "1-客车", "2-货车", "3-油罐车" }, "2");

        private Info _axesWeight = new Info(new Rectangle(322, 185, 308, 145), "机车轴重", new List<string>() { "1-21t", "2-23t", "3-25t" }, "1");

        private Speed _speed = new Speed();

        private Color _selectColor = Color.FromArgb(0, 255, 255);
        private List<RectText> _writeRectList = new List<RectText>();
        private List<RectText> _currentRectList = new List<RectText>();
        private int _currentRectIndex = 0;

        public override string GetInfo()
        {
            return "列车参数";
        }
        private void SetCurrenIndex(int i)
        {
            _writeRectList[_currentRectIndex].BackgroundColor = Color.Black;
            _currentRectIndex += i;
            if (i > 0)
            {
                _currentRectIndex = _currentRectIndex >= _writeRectList.Count - 1 ? _writeRectList.Count - 1 : _currentRectIndex;
            }
            else
            {
                _currentRectIndex = _currentRectIndex <= 0 ? 0 : _currentRectIndex;
            }
            _writeRectList[_currentRectIndex].BackgroundColor = _selectColor;
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            for (int i = 0; i < _weight._nowWeight.Count; i++)
                _writeRectList.Add(_weight._nowWeight[i]);
            _writeRectList.Add(_type._writeRectText);
            _writeRectList.Add(_axesWeight._writeRectText);
            _writeRectList.Add(_speed._writeRectText);


            for (int i = 0; i < _weight._currentWeight.Count; i++)
                _currentRectList.Add(_weight._currentWeight[i]);
            _currentRectList.Add(_type._selectRectText);
            _currentRectList.Add(_axesWeight._selectRectText);
            _currentRectList.Add(_speed._selectRectText);

            _writeRectList[_currentRectIndex].BackgroundColor = _selectColor;


            return true;
        }


        public static int trainwight = 4500;
        public static int trainType = 2;
        public static int axisWeight = 2;
        public static int speed = 2;

        private void RefreshDatas()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Cancel].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Cancel].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 16, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Left].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Left].State == MouseState.MouseDown)
            {
                SetCurrenIndex(-1);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Right].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Right].State == MouseState.MouseDown)
            {
                SetCurrenIndex(1);
            }

            ResponseEnsurBtn();

            for (int i = 0; i < 10; i++)
            {
                if (BoolList[800 + i])
                {
                    _writeRectList[_currentRectIndex].Text = i == 9 ? "0" : (i + 1).ToString();
                    SetCurrenIndex(1);
                }
            }
        }

        private void ResponseEnsurBtn()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Enter].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Enter].State == MouseState.MouseDown)
            {
                RefreshTrainType();

                RefreshAxisWeight();

                RefreshSpeed();

                RefreshTrainWeight();

                _currentRectList[5].Text = trainType.ToString();
                _currentRectList[6].Text = axisWeight.ToString();
                _currentRectList[7].Text = speed.ToString();

                ApeendValue();
            }
        }

        private void RefreshTrainWeight()
        {
            int tempValue = 0;
            for (int i = 0; i < 5; i++)
            {
                int value = int.Parse(_currentRectList[i].Text);

                if (!string.IsNullOrEmpty(_writeRectList[i].Text))
                {
                    value = int.Parse(_writeRectList[i].Text);
                }
                tempValue += (int) Math.Pow(10, 4 - i)*value;

                if (i == 4)
                {
                    if (tempValue > 0)
                    {
                        trainwight = tempValue;
                    }
                }
                _currentRectList[i].Text = value.ToString();
            }
        }

        private void RefreshSpeed()
        {
            speed = int.Parse(_currentRectList[7].Text);

            if (!string.IsNullOrEmpty(_writeRectList[7].Text))
            {
                int value = int.Parse(_writeRectList[7].Text);

                if (value >= 1 && value <= 3)
                {
                    speed = value;
                }
            }
        }

        private void RefreshAxisWeight()
        {
            axisWeight = int.Parse(_currentRectList[6].Text);


            if (!string.IsNullOrEmpty(_writeRectList[6].Text))
            {
                int value = int.Parse(_writeRectList[6].Text);

                if (value >= 1 && value <= 3)
                {
                    axisWeight = value;
                }
            }
        }

        private void RefreshTrainType()
        {
            trainType = int.Parse(_currentRectList[5].Text);

            if (!string.IsNullOrEmpty(_writeRectList[5].Text))
            {
                int value = int.Parse(_writeRectList[5].Text);

                if (value >= 1 && value <= 3)
                {
                    trainType = value;
                }
            }
        }

        private void ApeendValue()
        {
            var wight = string.Empty;

            for (int i = 0; i < 5; i++)
            {
                wight += _currentRectList[i].Text;
            }

            append_postCmd(CmdType.SetFloatValue, 250, 0, string.IsNullOrEmpty(wight) ? 0 : float.Parse(wight));
            append_postCmd(CmdType.SetFloatValue, 251, 0, string.IsNullOrEmpty(_currentRectList[5].Text) ? 0 : float.Parse(_currentRectList[5].Text));
            append_postCmd(CmdType.SetFloatValue, 253, 0, string.IsNullOrEmpty(_currentRectList[6].Text) ? 0 : float.Parse(_currentRectList[6].Text));
            append_postCmd(CmdType.SetFloatValue, 254, 0, string.IsNullOrEmpty(_currentRectList[7].Text) ? 0 : float.Parse(_currentRectList[7].Text));

        }
        public override void paint(Graphics dcGs)
        {
            RefreshDatas();

            _weight.OnDraw(dcGs);
            _type.OnDraw(dcGs);
            _axesWeight.OnDraw(dcGs);
            _speed.OnDraw(dcGs);
        }


    }

    public class Weight
    {
        private Rectangle _mainRect = new Rectangle(10, 35, 620, 145);
        private Pen _pen = new Pen(Color.White, 2);

        private Rectangle _rect = new Rectangle(220, 50, 360, 60);

        private Rectangle _stringRect1 = new Rectangle(30, 70, 175, 65);

        public List<RectText> _currentWeight = new List<RectText>();

        public List<RectText> _nowWeight = new List<RectText>();

        private RectText _currentUnit;
        private RectText _nowUnit;

        public Weight()
        {
            _currentWeight.Add(new RectText(new Rectangle(350, 60, 25, 35), "0", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _currentWeight.Add(new RectText(new Rectangle(390, 60, 25, 35), "4", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _currentWeight.Add(new RectText(new Rectangle(430, 60, 25, 35), "5", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _currentWeight.Add(new RectText(new Rectangle(470, 60, 25, 35), "0", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _currentWeight.Add(new RectText(new Rectangle(510, 60, 25, 35), "0", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _currentUnit = (new RectText(new Rectangle(550, 60, 25, 35), "t", 12, 1, Color.White, Color.Black, Color.White, 1, false, null, true));


            _nowWeight.Add(new RectText(new Rectangle(350, 125, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _nowWeight.Add(new RectText(new Rectangle(390, 125, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _nowWeight.Add(new RectText(new Rectangle(430, 125, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _nowWeight.Add(new RectText(new Rectangle(470, 125, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _nowWeight.Add(new RectText(new Rectangle(510, 125, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            _nowUnit = (new RectText(new Rectangle(550, 125, 25, 35), "t", 12, 1, Color.White, Color.Black, Color.White, 1, false, null, true));
        }

        public void OnDraw(Graphics g)
        {
            g.DrawRectangle(_pen, _mainRect);
            g.DrawRectangle(_pen, _rect);
            g.DrawString("列车重量\n(<=20000t)", Common._12Font, Common.WhiteBrush, _stringRect1);
            g.DrawString("当前值", Common._12Font, Common.WhiteBrush, new Point(240, 60));



            for (int i = 0; i < 5; i++)
            {
                _currentWeight[i].OnDraw(g);
                _nowWeight[i].OnDraw(g);
            }

            _nowUnit.OnDraw(g);
            _currentUnit.OnDraw(g);
        }
    }

    public class Info
    {
        private Rectangle _mainRect;
        private string _title;
        private List<string> _typesList = new List<string>();
        private string _defaultSelect;
        private Pen _pen = new Pen(Color.White, 2);

        private Rectangle _currentRect;

        public RectText _selectRectText;
        public RectText _writeRectText;

        public Info(Rectangle rect, string title, List<string> typesList, string defaultSelect)
        {
            _mainRect = rect;

            _currentRect = new Rectangle(_mainRect.X + 135, _mainRect.Y + 30, 160, 60);

            _selectRectText = new RectText(new Rectangle(_mainRect.X + 250, _mainRect.Y + 40, 25, 35), "2", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true);

            _writeRectText = new RectText(new Rectangle(_mainRect.X + 250, _mainRect.Y + 100, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true);

            _title = title;
            _typesList = typesList;
            _defaultSelect = defaultSelect;
        }

        public void OnDraw(Graphics g)
        {
            g.DrawRectangle(_pen, _mainRect);
            g.DrawRectangle(_pen, _currentRect);

            g.DrawString(_title, Common._20Font, Common.WhiteBrush, new Point(_mainRect.X + 4, _mainRect.Y + 4));
            g.DrawString("当前值", Common._16Font, Common.WhiteBrush, new Point(_currentRect.X + 15, _currentRect.Y + 10));

            for (int i = 0; i < _typesList.Count; i++)
            {
                g.DrawString(_typesList[i], Common._14Font, Common.WhiteBrush, new Point(_mainRect.X + 20, _mainRect.Y + 50 + 25 * i));
            }

            _selectRectText.OnDraw(g);
            _writeRectText.OnDraw(g);
        }
    }

    public class Speed
    {
        private Pen _pen = new Pen(Color.White, 2);
        private Rectangle _mainRect;
        private Rectangle _currentRect;

        public RectText _selectRectText;
        public RectText _writeRectText;

        public Speed()
        {
            _mainRect = new Rectangle(10, 335, 620, 145);
            _currentRect = new Rectangle(_mainRect.X + 200, _mainRect.Y + 20, 160, 60);

            _selectRectText = new RectText(new Rectangle(_mainRect.X + 310, _mainRect.Y + 30, 25, 35), "2", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true);
            _writeRectText = new RectText(new Rectangle(_mainRect.X + 310, _mainRect.Y + 100, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true);

        }

        public void OnDraw(Graphics g)
        {
            g.DrawRectangle(_pen, _mainRect);
            g.DrawRectangle(_pen, _currentRect);

            g.DrawString("联挂速度", Common._20Font, Common.WhiteBrush, new Point(_mainRect.X + 50, _mainRect.Y + 8));

            for (int i = 0; i < 3; i++)
            {
                g.DrawString((i + 1).ToString() + "-" + (i + 1).ToString() + "km/h", Common._14Font, Common.WhiteBrush, new PointF(70, _mainRect.Y + 40 + 25 * i));
            }

            g.DrawString("当前值", Common._16Font, Common.WhiteBrush, new Point(_currentRect.X + 15, _currentRect.Y + 10));


            _selectRectText.OnDraw(g);
            _writeRectText.OnDraw(g);

            g.DrawString("km/h", Common._16Font, Common.WhiteBrush, new Point(_mainRect.X + 400, _mainRect.Y + 30));
            g.DrawString("km/h", Common._16Font, Common.WhiteBrush, new Point(_mainRect.X + 400, _mainRect.Y + 105));
        }

    }
}
