using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.IO;

namespace HXD1.DeepDomestic
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrainConfig : baseClass
    {

        private bool[] _currentCollector1 = new bool[3]; //受电弓1状态
        private bool[] _currentCollector2 = new bool[3]; //受电弓2状态

        private bool[] _isDriverRooms1 = new bool[2]; //司机室占用情况
        private bool[] _isDriverRooms2 = new bool[2]; //司机室占用情况

        private bool[] _isMainSegment1 = new bool[4]; //主断状态
        private bool[] _isMainSegment2 = new bool[4]; //主断状态

        private bool[] IsElectricMachine = new bool[4]; //电机状态 
        private bool[] IsStopBrake = new bool[4]; //停放制动
        private bool[] IsTrainBrake = new bool[5]; //机车制动
        private bool[] _trainTop1 = new bool[2]; //车顶隔离开关1状态
        private bool[] _trainTop2 = new bool[2]; //车顶隔离开关2状态

        private Rectangle[] _bowRect = new Rectangle[2] {new Rectangle(208, 60, 40, 20), new Rectangle(560, 60, 40, 20)};

        private Rectangle[] _switchRect = new Rectangle[]
        {new Rectangle(218, 60, 40, 20), new Rectangle(550, 60, 40, 20)};


        private Image[] _trainStatusImages = new Image[6]; //表示车上受电弓和车顶隔离开关的组合状态的图片

        private Bitmap[] dianji = new Bitmap[3];

        private Image[] _trainBrakeImages = new Image[3];

        private Image[] _motorImages = new Image[2];

        private Image[] _stopBrakeImages = new Image[4];

        private Image[] _mainSegmentImages = new Image[4];

        private string[] _BCUStringList = {"BCU\n本机切除", "BCU\n本机投入", "BCU\n重联"};

        private Rectangle _trainRect = new Rectangle(120, 50, 500, 120);

        private List<TrainConfigRect1> _trainConfigList1 = new List<TrainConfigRect1>();

        private List<TrainConfigRect2> _trainConfigList2 = new List<TrainConfigRect2>();

        private Color[] colorArray = {Color.Yellow, Color.Red};

        public override string GetInfo()
        {
            return "机车配置";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (var fs = new FileStream(Path.Combine(RecPath, UIObj.ParaList[i]), FileMode.Open))
                {
                    if (i < 6)
                    {
                        _trainStatusImages[i] = Image.FromStream(fs);
                    }
                    else if (i < 8)
                    {
                        _motorImages[i - 6] = Image.FromStream(fs);
                    }
                    else if (i < 11)
                    {
                        _trainBrakeImages[i - 8] = Image.FromStream(fs);
                    }
                    else if (i < 15)
                    {
                        _stopBrakeImages[i - 11] = Image.FromStream(fs);
                    }
                    else if (i < 19)
                    {
                        _mainSegmentImages[i - 15] = Image.FromStream(fs);
                    }
                }
            }
            dianji[0] = new Bitmap(Path.Combine(RecPath, @"机车配置\电机投入.png"));
            dianji[1] = new Bitmap(Path.Combine(RecPath, @"机车配置\电机切除.png"));
            dianji[2] = new Bitmap(Path.Combine(RecPath, @"机车配置\电机隔离.png"));


            return true;

        }

        public override void paint(Graphics g)
        {
            RefreshDatas();

            DrawOn(g);
        }

        public void InitData()
        {
            //机车位置初始化

            _trainConfigList1.Add(new TrainConfigRect1(new Point(_trainRect.X + 80, _trainRect.Bottom + 20)));

            _trainConfigList1.Add(new TrainConfigRect1(new Point(_trainRect.X + 175, _trainRect.Bottom + 20)));

            _trainConfigList1.Add(new TrainConfigRect1(new Point(_trainRect.X + 290, _trainRect.Bottom + 20)));

            _trainConfigList1.Add(new TrainConfigRect1(new Point(_trainRect.X + 375, _trainRect.Bottom + 20)));

            _trainConfigList2.Add(new TrainConfigRect2(new Point(_trainRect.X + 125, _trainRect.Bottom + 130)));

            _trainConfigList2.Add(new TrainConfigRect2(new Point(_trainRect.X + 335, _trainRect.Bottom + 130)));
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshDatas()
        {
            RefreshPantograhp();

            RefreshDisconnectingSwitch();

            RefreshMainDisconnectionSwtich();

            RefreshMotor1();
            //电机状态
            RefreshMotorA1();

            RefreshMotorA2();

            RefreshMotorB1();

            RefreshMotorB2();

            RefreshPakingBrake();


            RefreshNormalBrake();

            RefreshBCU();

            //高压禁止
            _trainConfigList2[0].RectTextList[3].Text = BoolList[1209] ? "高压禁止" : "";

            _trainConfigList2[1].RectTextList[3].Text = BoolList[1210] ? "高压禁止" : "";
        }

        private void RefreshBCU()
        {
            //BCU 
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[1211 + i])
                {
                    _trainConfigList2[0].RectTextList[2].Text = _BCUStringList[i];
                }

                if (BoolList[1214])
                {
                    _trainConfigList2[1].RectTextList[2].Text = _BCUStringList[i];
                }
            }
        }

        private void RefreshNormalBrake()
        {
            //机车制动
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[849 + i])
                {
                    foreach (var v in _trainConfigList1)
                    {
                        v.RectTextList[3].ImagePicture = _trainBrakeImages[i];
                    }
                }
            }
        }

        private void RefreshPakingBrake()
        {
            //停放制动
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[852 + i])
                {
                    foreach (var v in _trainConfigList2)
                    {
                        v.RectTextList[0].ImagePicture = _stopBrakeImages[i];
                    }
                }
            }
        }

        private void RefreshMotorB2()
        {
            //B车电机2
            if (BoolList[1228])
            {
                _trainConfigList1[3].RectTextList[2].ImagePicture = dianji[0];
            }
            else if (BoolList[1230])
            {
                _trainConfigList1[3].RectTextList[2].ImagePicture = dianji[1];
            }
            else if (BoolList[1232])
            {
                _trainConfigList1[3].RectTextList[2].ImagePicture = dianji[2];
            }
            else
            {
                _trainConfigList1[3].RectTextList[2].ImagePicture = null;
            }
        }

        private void RefreshMotorB1()
        {
            //B车电机1
            if (BoolList[1227])
            {
                _trainConfigList1[2].RectTextList[2].ImagePicture = dianji[0];
            }
            else if (BoolList[1229])
            {
                _trainConfigList1[2].RectTextList[2].ImagePicture = dianji[1];
            }
            else if (BoolList[1231])
            {
                _trainConfigList1[2].RectTextList[2].ImagePicture = dianji[2];
            }
            else
            {
                _trainConfigList1[2].RectTextList[2].ImagePicture = null;
            }
        }

        private void RefreshMotorA2()
        {
            //A车电机2
            if (BoolList[1222])
            {
                _trainConfigList1[1].RectTextList[2].ImagePicture = dianji[0];
            }
            else if (BoolList[1224])
            {
                _trainConfigList1[1].RectTextList[2].ImagePicture = dianji[1];
            }
            else if (BoolList[1226])
            {
                _trainConfigList1[1].RectTextList[2].ImagePicture = dianji[2];
            }
            else
            {
                _trainConfigList1[1].RectTextList[2].ImagePicture = null;
            }
        }

        private void RefreshMotorA1()
        {
            //A车电机1
            if (BoolList[1221])
            {
                _trainConfigList1[0].RectTextList[2].ImagePicture = dianji[0];
            }
            else if (BoolList[1223])
            {
                _trainConfigList1[0].RectTextList[2].ImagePicture = dianji[1];
            }
            else if (BoolList[1225])
            {
                _trainConfigList1[0].RectTextList[2].ImagePicture = dianji[2];
            }
            else
            {
                _trainConfigList1[0].RectTextList[2].ImagePicture = null;
            }
        }

        private void RefreshMotor1()
        {
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[974 + 4*i])
                {
                    _trainConfigList1[0].RectTextList[0].BackgroundColor = colorArray[i];
                }

                if (BoolList[975 + 4*i])
                {
                    _trainConfigList1[0].RectTextList[1].BackgroundColor = colorArray[i];
                }

                if (BoolList[976 + 4*i])
                {
                    _trainConfigList1[1].RectTextList[0].BackgroundColor = colorArray[i];
                }

                if (BoolList[977 + 4*i])
                {
                    _trainConfigList1[1].RectTextList[1].BackgroundColor = colorArray[i];
                }

                if (BoolList[1002 + 4*i])
                {
                    _trainConfigList1[2].RectTextList[0].BackgroundColor = colorArray[i];
                }

                if (BoolList[1003 + 4*i])
                {
                    _trainConfigList1[2].RectTextList[1].BackgroundColor = colorArray[i];
                }

                if (BoolList[1004 + 4*i])
                {
                    _trainConfigList1[3].RectTextList[0].BackgroundColor = colorArray[i];
                }

                if (BoolList[1005 + 4*i])
                {
                    _trainConfigList1[3].RectTextList[1].BackgroundColor = colorArray[i];
                }
            }
        }

        private void RefreshMainDisconnectionSwtich()
        {
            //左主断状态
            for (int i = 0; i < 4; i++)
            {
                _isMainSegment1[i] = BoolList[826 + i];
            }

            //右主断状态
            for (int i = 0; i < 4; i++)
            {
                _isMainSegment2[i] = BoolList[830 + i];
            }
        }

        private void RefreshDisconnectingSwitch()
        {
            //车顶隔离开关1
            for (int i = 0; i < 2; i++)
            {
                _trainTop1[i] = BoolList[UIObj.InBoolList[i + 6]];
            }

            //车顶隔离开关2
            for (int i = 0; i < 2; i++)
            {
                _trainTop2[i] = BoolList[UIObj.InBoolList[i + 8]];
            }
        }

        private void RefreshPantograhp()
        {
            //受电弓1
            for (int i = 0; i < 3; i++)
            {
                _currentCollector1[i] = BoolList[UIObj.InBoolList[i]];
            }

            //受电弓2
            for (int i = 0; i < 3; i++)
            {
                _currentCollector2[i] = BoolList[UIObj.InBoolList[i + 3]];
            }
        }

        public void DrawOn(Graphics g)
        {
            //车身
            g.DrawImage(_trainStatusImages[0], _trainRect);

            //受电弓
            DrawPantograph(g);

            //车顶开关
            for (int i = 0; i < 2; i++)
            {
                if (_trainTop1[i])
                {
                    g.DrawImage(_trainStatusImages[4 + i], new Rectangle(278, 60, 40, 20));
                }

                if (_trainTop2[i])
                {
                    g.DrawImage(_trainStatusImages[4 + i], new Rectangle(425, 60, 40, 20));
                }
            }

            DrawDriverState(g);


            for (int i = 0; i < 4; i++)
            {
                if (_isMainSegment1[i])
                {
                    g.DrawImage(_mainSegmentImages[i], new Rectangle(230, 100, 60, 25));
                }

                if (_isMainSegment2[i])
                {
                    g.DrawImage(_mainSegmentImages[i], new Rectangle(450, 100, 60, 25));
                }
            }

            _trainConfigList1.ForEach(e => e.OnDraw(g));

            _trainConfigList2.ForEach(e => e.OnDraw(g));

        }

        private void DrawDriverState(Graphics g)
        {
            //司机室占用
            if (BoolList[1187])
            {
                g.FillRectangle(Common.YellowBrush, new Rectangle(178, 95, 14, 36));
                g.FillRectangle(Common.YellowBrush, new Rectangle(164, 100, 8, 12));
            }

            if (BoolList[1188])
            {
                g.FillRectangle(Common.YellowBrush, new Rectangle(553, 95, 14, 36));
                g.FillRectangle(Common.YellowBrush, new Rectangle(572, 100, 8, 12));
            }
        }

        private void DrawPantograph(Graphics g)
        {
            for (int i = 0; i < 3; i++)
            {
                if (_currentCollector1[i])
                {
                    g.DrawImage(_trainStatusImages[1 + i], new Rectangle(208, 60, 40, 20));
                }


                if (_currentCollector2[i])
                {
                    g.DrawImage(_trainStatusImages[1 + i], new Rectangle(500, 60, 40, 20));
                }
            }
        }
    }

    public class TrainConfigRect1
    {
        private Point _startPoint;

        public List<RectText> RectTextList = new List<RectText>();

        public TrainConfigRect1(Point startPoint)
        {
            _startPoint = startPoint;

            RectTextList.Add(new RectText(new Rectangle(_startPoint.X, _startPoint.Y, 30, 20), "", 1, 1, Color.Black,
                Color.Black, Color.White, 1, true, null, true));
            RectTextList.Add(new RectText(new Rectangle(_startPoint.X + 30, _startPoint.Y, 30, 20), "", 1, 1,
                Color.Black, Color.Black, Color.White, 1, true, null, true));
            RectTextList.Add(new RectText(new Rectangle(_startPoint.X, _startPoint.Y + 20, 60, 45), "", 1, 1,
                Color.Black, Color.Black, Color.White, 1, true, null, true));
            RectTextList.Add(new RectText(new Rectangle(_startPoint.X, _startPoint.Y + 65, 60, 45), "", 1, 1,
                Color.Black, Color.Black, Color.White, 1, true, null, true));

        }

        public void OnDraw(Graphics g)
        {
            foreach (var v in RectTextList)
            {
                v.OnDraw(g);
            }
        }
    }

    public class TrainConfigRect2
    {
        private Point _startPoint;

        public List<RectText> RectTextList = new List<RectText>();

        public TrainConfigRect2(Point startPoint)
        {
            _startPoint = startPoint;

            RectTextList.Add(new RectText(new Rectangle(_startPoint.X, _startPoint.Y + 2, 60, 45), "", 1, 1, Color.Black,
                Color.Black, Color.White, 1, true, null, true));
            RectTextList.Add(new RectText(new Rectangle(_startPoint.X, _startPoint.Y + 45 + 3, 60, 45), "", 1, 1,
                Color.Black, Color.Black, Color.White, 1, false, null, true));
            RectTextList.Add(new RectText(new Rectangle(_startPoint.X, _startPoint.Y + 90 + 3, 60, 45), "BCU\n重联", 10, 1,
                Color.White, Color.Black, Color.White, 1, true, null, true));
            RectTextList.Add(new RectText(new Rectangle(_startPoint.X - 5, _startPoint.Y + 138 + 3, 70, 20), "高压禁止", 10,
                1, Color.White, Color.Black, Color.White, 1, false, null, true));
        }

        public void OnDraw(Graphics g)
        {
            foreach (var v in RectTextList)
            {
                v.OnDraw(g);
            }
        }
    }
}

