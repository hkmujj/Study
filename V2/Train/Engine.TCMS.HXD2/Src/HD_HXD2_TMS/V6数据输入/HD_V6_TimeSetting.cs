using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Timers;

namespace HD_HXD2_TMS.V6数据输入
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V6_TimeSetting:baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private Int32 _year = 2016;
        private Int32 _month = 5;
        private Int32 _day = 23;
        private Int32 _hour = 13;
        private Int32 _min = 56;
        private Int32 _second = 30;

        private Int32 _yearClone = 2016;
        private Int32 _monthClone = 5;
        private Int32 _dayClone = 23;
        private Int32 _hourClone = 13;
        private Int32 _minClone = 56;
        private Int32 _secondClone = 30;
        private Timer _timer = null;
        private List<Button> _btns = new List<Button>();

        public override string GetInfo()
        {
            return "数据输入界面-时间设置";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    _images.Add(Image.FromStream(fs));
                }
            });

            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
            _year = DateTime.Now.Year;
            _month = DateTime.Now.Month;
            _day = DateTime.Now.Day;
            _hour = DateTime.Now.Hour;
            _min = DateTime.Now.Minute;
            _second = DateTime.Now.Second;

            for (int i = 0; i < 12; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(44+166*(i%2)+299*(i/6),152+120+89*(i/2%3),62,27),
                    i,
                    new ButtonStyle()
                    {
                        Background = null,
                        DownImage = null,
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("宋体", 10),
                            StringFormat =
                                new StringFormat()
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                },
                            TextBrush = (SolidBrush) Brushes.White
                        }
                    }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            Button btn1 = new Button(
                    "",
                    new RectangleF(653, 337+120, 84, 26),
                    12,
                    new ButtonStyle()
                    {
                        Background = null,
                        DownImage = null,
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("宋体", 10),
                            StringFormat =
                                new StringFormat()
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                },
                            TextBrush = (SolidBrush)Brushes.White
                        }
                    }
                    );
            btn1.ClickEvent += btn_ClickEvent;
            _btns.Add(btn1);

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB == 602)
                {
                    _yearClone = _year;
                    _monthClone = _month;
                    _dayClone = _day;
                    _hourClone = _hour;
                    _minClone = _min;
                    _secondClone = _second;
                }
            }
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    _yearClone--;
                    break;
                case 1:
                    _yearClone++;
                    break;
                case 2:
                    if (_monthClone == 1)
                        _monthClone = 12;
                    else _monthClone--;
                    break;
                case 3:
                    if (_monthClone == 12)
                        _monthClone = 1;
                    else _monthClone++;
                    break;
                case 4:
                    Int32 inva = 31;
                    switch (_monthClone)
                    {
                        case 2:
                            if (_yearClone % 4 == 0) inva = 29;
                            else inva = 28;
                            break;
                        case 4:
                            inva = 30;
                            break;
                        case 6:
                            inva = 30;
                            break;
                        case 9:
                            inva = 30;
                            break;
                        case 11:
                            inva = 30;
                            break;
                    }
                    if (_dayClone == 1) _dayClone = inva;
                    else _dayClone--;
                    break;
                case 5:
                    Int32 invaClone = 31;
                    switch (_monthClone)
                    {
                        case 2:
                            if (_yearClone % 4 == 0) invaClone = 29;
                            else invaClone = 28;
                            break;
                        case 4:
                            invaClone = 30;
                            break;
                        case 6:
                            invaClone = 30;
                            break;
                        case 9:
                            invaClone = 30;
                            break;
                        case 11:
                            invaClone = 30;
                            break;
                    }
                    if (_dayClone == invaClone) _dayClone = 1;
                    else _dayClone++;
                    break;
                case 6:
                    if (_hourClone == 0) _hourClone = 23;
                    else _hourClone--;
                    break;
                case 7:
                    _hourClone = (_hourClone + 1) % 24;
                    break;
                case 8:
                    if (_minClone == 0) _minClone = 59;
                    else _hourClone--;
                    break;
                case 9:
                    _minClone = (_minClone + 1) % 60;
                    break;
                case 10:
                    if (_secondClone == 0) _secondClone = 59;
                    else _secondClone--;
                    break;
                case 11:
                    _secondClone = (_secondClone + 1) % 60;
                    break;
                case 12:
                    _year = _yearClone;
                    _month = _monthClone;
                    _day = _dayClone;
                    _hour = _hourClone;
                    _min = _minClone;
                    _second = _secondClone;
                    break;
            }
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _second = (_second + 1)%60;
            if (_second == 0)
            {
                _min = (_min + 1)%60;
                if (_min == 0)
                {
                    _hour = (_hour + 1)%24;
                    if (_hour == 0)
                    {
                        Int32 inva = 31;
                        switch (_month)
                        {
                            case 2:
                                if (_year%4 == 0) inva = 29;
                                else inva = 28;
                                break;
                            case 4:
                                inva = 30;
                                break;
                            case 6:
                                inva = 30;
                                break;
                            case 9:
                                inva = 30;
                                break;
                            case 11:
                                inva = 30;
                                break;
                        }
                        _day = (_day + 1)%inva;
                        if (_day == 0)
                        {
                            _month = (_month-1 + 1)%12+1;
                            if (_month == 1)
                            {
                                _year++;
                            }
                        }
                    }
                }
            }
        }

        public override bool mouseDown(Point point)
        {
            _btns.ForEach(a => a.MouseDown(point));

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            _btns.ForEach(a => a.MouseUp(point));
            
            return base.mouseUp(point);
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            dcGs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            dcGs.DrawImage(_images[0], new Rectangle(0,120,800,417));

            _btns.ForEach(a => a.Paint(dcGs));

            String mpuTime = _hour + ":" + _min + ":" + _second + "\n" + _year + "/" + _month + "/" + _day;
            String sysTime = DateTime.Now.ToLongTimeString() + "\n" + DateTime.Now.ToShortDateString();
            dcGs.DrawString(
                mpuTime,
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(111, 41+120, 146, 47),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                sysTime,
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(499, 41 + 120, 146, 47),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                _yearClone.ToString(),
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(108, 152 + 120, 98, 24),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                _monthClone.ToString(),
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(108, 242 + 120, 98, 24),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                _dayClone.ToString(),
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(108, 333 + 120, 98, 24),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                _hourClone.ToString(),
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(409, 152 + 120, 98, 24),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                _minClone.ToString(),
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(409, 242 + 120, 98, 24),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                _secondClone.ToString(),
                new Font("宋体", 11),
                Brushes.Yellow,
                new RectangleF(409, 333 + 120, 98, 24),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            base.paint(dcGs);
        }
    }
}
