using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using HD_HXD2_TMS.Common;
using System.Timers;

namespace HD_HXD2_TMS.VC公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_MessageInfo:baseClass
    {
        private Button _btn = null;
        private List<Image> _images = new List<Image>();    //图片资源
        private Timer _timer=null;
        private Int32 _count = 0;
        private Int32 _countDown = 60;
        private Timer _timerCountDown = null;

        public Boolean IsStartCountDown
        {
            set
            {
                if (_isStartCountDown == value) return;
                _isStartCountDown = value;
                if (value)
                {
                    _countDown = _countDownClone;
                    _timerCountDown.Start();
                }
                else
                {
                    _timerCountDown.Stop();
                }
            }
        }
        private Boolean _isStartCountDown = false;

        public Int32 CountDown
        {
            set
            {
                if (_countDownClone == value) return;
                _countDownClone = value;

                _countDown = value;
            }
        }
        private Int32 _countDownClone = 0;

        public Boolean IsFlicker
        {
            set
            {
                if (_isFlicker == value) return;
                _isFlicker = value;
                if (value)
                {
                    _count = 0;
                    _timer.Start();
                }
                else
                {
                    _count = 0;
                    _timer.Stop();
                }
            }
        }
        private Boolean _isFlicker = false;

        public override string GetInfo()
        {
            return "公共试图-通讯信息";
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

            _btn = new Button(
                "",
                new RectangleF(1, 571, 383, 28),
                0,
                new ButtonStyle()
                {
                    Background = _images[0],
                    DownImage = _images[2],
                    FontStyle = new FontStyle_ES()
                    {
                        Font = new Font("宋体", 10),
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = new StringFormat()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        }
                    }
                }
                );
            _btn.MouseDownEvent += _btn_MouseDownEvent;
            _btn.ClickEvent += _btn_ClickEvent;

            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;

            _timerCountDown = new Timer(1000);
            _timerCountDown.Elapsed += _timerCountDown_Elapsed;

            return true;
        }

        void _timerCountDown_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_countDown > 0) _countDown--;
        }

        void _btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)_btn.Style).FontStyle.TextBrush = (SolidBrush)Brushes.Black;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _count++;
        }

        public override bool mouseDown(Point point)
        {
            _btn.MouseDown(point);
            
            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            _btn.MouseUp(point);

            return base.mouseUp(point);
        }

        void _btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            HD_VC_FalutManager.IsShowCurrentAView = false;
            HD_VC_FalutManager.IsShowCurrentBView = false;
            HD_VC_FalutManager.IsShowCurrentView = true;
        }

        public override void paint(Graphics dcGs)
        {
            //故障
            dcGs.DrawRectangle(Pens.White, new Rectangle(0, 570, 385, 30));

            //中间
            dcGs.DrawRectangle(Pens.White, new Rectangle(385, 570, 30, 30));

            if (!_btn.IsDown)
            {
                FalutInfo falutInfo = HD_VC_FalutManager.CurrentFalut;
                if (falutInfo == null)
                {
                    IsFlicker = false;
                    _btn.Text = "机车无故障";
                    ((ButtonStyle)_btn.Style).Background = _images[0];
                    ((ButtonStyle)_btn.Style).FontStyle.TextBrush = (SolidBrush)Brushes.White;
                }
                else
                {
                    IsFlicker = !falutInfo.IsOK;
                    if (falutInfo.IsOK)
                    {
                        _btn.Text = falutInfo.Name;
                    }
                    else//闪烁
                    {
                        if (_count % 2 == 0)
                            _btn.Text = falutInfo.Name;
                        else _btn.Text = "";
                    }
                    ((ButtonStyle)_btn.Style).Background = _images[1];
                    ((ButtonStyle)_btn.Style).FontStyle.TextBrush = (SolidBrush)Brushes.Black;

                    if (!falutInfo.IsOK)
                        dcGs.DrawImage(_images[3 + (Int32)falutInfo.Grade], new Rectangle(386, 571, 28, 28));
                }
            }
            _btn.Paint(dcGs);

            if (VC公共组件.HD_VC_ConverterManager.CurrentFalut != null)
                dcGs.DrawString(
                    HD_VC_ConverterManager.CurrentFalut.Name,
                    new Font("宋体", 9),
                    Brushes.Yellow,
                    new Rectangle(415, 570 + 2, 192-20, 30),
                    new StringFormat() {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center}
                    );

            //通讯
            dcGs.DrawRectangle(Pens.White, new Rectangle(415, 570, 385, 30));
            dcGs.DrawString(
                "通信",
                new Font("宋体", 9),
                Brushes.Yellow,
                new Rectangle(415, 570+2, 192, 30),
                new StringFormat() {  Alignment= StringAlignment.Far, LineAlignment= StringAlignment.Center}
                );

            CountDown = Convert.ToInt32(FloatList[UIObj.InFloatList[0]]);
            IsStartCountDown = BoolList[UIObj.InBoolList[0]];
            Brush[] brushesBack = {Brushes.LimeGreen, Brushes.Yellow, Brushes.Red};
            Brush[] brushesText = { Brushes.Black, Brushes.Black, Brushes.Yellow };
            String[] texts = {"秒后无人警惕报警", "秒后无人警惕报警", "秒后惩罚制动施加"};
            Brush backBrush = Brushes.Black;
            Brush textBrush = Brushes.White;
            String text = "无人警惕";
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[1]+i])
                {
                    backBrush = brushesBack[i];
                    textBrush = brushesText[i];
                    text = _countDown + texts[i];
                }
            }

            dcGs.FillRectangle(backBrush, new Rectangle(608, 570 + 2, 192, 30));
            dcGs.DrawString(
                text,
                new Font("宋体", 11),
                textBrush,
                new Rectangle(608, 570 + 2, 192, 30),
                new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                );

            if (BoolList[UIObj.InBoolList[2]])
            {
                dcGs.FillRectangle(Brushes.Red, new Rectangle(200, 170, 400, 260));
                dcGs.DrawString(
                    "惩罚制动倒计时\n"+_countDown,
                    new Font("宋体", 25, FontStyle.Bold),
                    Brushes.Black,
                    new Rectangle(200, 170, 400, 260),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
            }

            base.paint(dcGs);
        }
    }
}
