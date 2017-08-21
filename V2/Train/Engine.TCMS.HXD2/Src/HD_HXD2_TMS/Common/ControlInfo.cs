using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using HD_HXD2_TMS.Extension;

namespace HD_HXD2_TMS.Common
{
    public enum ControlType
    {
        Normal = 0,
        Falut = 1,
        Handle = 2
    }

    public class ControlInfo
    {
        public ControlType ControlType { get; set; }
        public Image Image { get; set; }
        public Int32 InDataLogic { get; set; }
        public Int32 OutDataLogic { get; set; }

        public Boolean IsUsed { get; set; }
    }

    public class Controler
    {
        private List<ControlInfo> _controlInfos = new List<ControlInfo>();
        private Button _btn = null;
        private Timer _timer = null;
        private Boolean _isControlBySelf = false;
        private Int32 _count = 0;
        private ControlInfo _currentControlInfo = null;
        private Boolean _isShowFrame = false;
        private Image _form = null;
        private List<Button> _btns = new List<Button>();
        private String _name = "";
        private baseClass _bc = null;
        private Timer _timerCountDown = null;

        public Boolean IsShowFrame
        {
            get { return _isShowFrame; }
        }

        public Controler(String name, Rectangle rect,ControlInfo normal,ControlInfo falut,ControlInfo handle,Image form,baseClass bc)
        {
            _controlInfos.Add(normal);
            _controlInfos.Add(falut);
            _controlInfos.Add(handle);
            _currentControlInfo = normal;
            _form = form;
            _name = name;
            _bc = bc;

            _btn = new Button(
                name,
                rect,
                0,
                new ButtonStyle()
                {
                    Background = normal.Image,
                    DownImage = normal.Image,
                    FontStyle = new FontStyle_ES()
                    {
                        Font = new Font("宋体", 10, FontStyle.Bold),
                        StringFormat =
                            new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            },
                        TextBrush = (SolidBrush) Brushes.Black
                    }
                }
                );
            _btn.MouseDownEvent += _btn_MouseDownEvent;
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;

            _timerCountDown = new Timer(1000);
            _timerCountDown.Elapsed += _timerCountDown_Elapsed;

            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                "",
                new Rectangle(300+127*i,230+118,78,32),
                i,
                new ButtonStyle()
                {
                    Background = null,
                    DownImage = null,
                    FontStyle = new FontStyle_ES()
                    {
                        Font = new Font("宋体", 11),
                        StringFormat =
                            new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            },
                        TextBrush = (SolidBrush)Brushes.Black
                    }
                }
                );
                btn.MouseDownEvent += btn_MouseDownEvent;
                _btns.Add(btn);
            }
        }

        private Int32 _countDown = 10;
        void _timerCountDown_Elapsed(object sender, ElapsedEventArgs e)
        {
            _countDown--;
        }

        void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://确定
                    _isShowFrame = false;
                    _timer.Start();
                    _isControlBySelf = true;
                    _bc.SetOutBoolValue( _currentControlInfo.OutDataLogic, 1, 0);
                    break;
                case 1://取消
                    _isShowFrame = false;
                    _isControlBySelf = false;
                    break;
            }
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _count++;
        }

        void _btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            //if (_currentControlInfo.ControlType == ControlType.Normal||
            //    _currentControlInfo.ControlType== ControlType.Handle)
            //{
                _isShowFrame = true;
            _countDown = 10;
                _timerCountDown.Start();
           // }
        }

        public void MouseDwon(Point point)
        {
            if (!_isShowFrame)
                _btn.MouseDown(point);
            else
                _btns.ForEach(a => a.MouseDown(point));
        }

        public void MouseUp(Point point)
        {
            if (!_isShowFrame)
                _btn.MouseUp(point);
            else
                _btns.ForEach(a => a.MouseUp(point));
        }

        public void Paint(Graphics dcGs)
        {
            if (!_isControlBySelf)
            {
                _controlInfos.ForEach(a => a.IsUsed = _bc.BoolList[a.InDataLogic]);
                _currentControlInfo = _controlInfos.Find(a => a.IsUsed);
                if (_currentControlInfo == null) return;
                if (_currentControlInfo.ControlType == ControlType.Handle)
                    ((ButtonStyle) _btn.Style).FontStyle.TextBrush = (SolidBrush) Brushes.White;
                else ((ButtonStyle) _btn.Style).FontStyle.TextBrush = (SolidBrush) Brushes.Black;
                ((ButtonStyle) _btn.Style).Background = _currentControlInfo.Image;
                ((ButtonStyle) _btn.Style).DownImage = _currentControlInfo.Image;
            }
            else
            {
                switch (_count)
                {
                    case 0:
                        if (_currentControlInfo.ControlType == ControlType.Normal)
                        {
                            ((ButtonStyle) _btn.Style).Background = _controlInfos[1].Image;
                            ((ButtonStyle) _btn.Style).DownImage = _controlInfos[1].Image;
                        }
                        _btn.Text = "";
                        break;
                    case 1:
                        if (_currentControlInfo.ControlType == ControlType.Normal)
                        {
                            ((ButtonStyle) _btn.Style).Background = _controlInfos[0].Image;
                            ((ButtonStyle) _btn.Style).DownImage = _controlInfos[0].Image;
                        }
                        _btn.Text = _name;
                        break;
                    case 2:
                        if (_currentControlInfo.ControlType == ControlType.Normal)
                        {
                            ((ButtonStyle) _btn.Style).Background = _controlInfos[1].Image;
                            ((ButtonStyle) _btn.Style).DownImage = _controlInfos[1].Image;
                        }
                        _btn.Text = "";
                        break;
                    default:
                        if (_currentControlInfo.ControlType == ControlType.Normal)
                        {
                            ((ButtonStyle) _btn.Style).Background = _controlInfos[0].Image;
                            ((ButtonStyle) _btn.Style).DownImage = _controlInfos[0].Image;
                        }
                        _btn.Text = _name;
                        _bc.SetOutBoolValue( _currentControlInfo.OutDataLogic, 0, 0);
                        _timer.Stop();
                        _count = 0;
                        _isControlBySelf = false;
                        break;
                }
            }

            _btn.Paint(dcGs);

        }

        public void PaintFrame(Graphics dcGs)
        {
            if (_isShowFrame)
            {
                dcGs.DrawImage(_form, new Rectangle(227, 96 + 118, 350, 195));
                _btns.ForEach(a => a.Paint(dcGs));
                dcGs.DrawString(
                    _countDown.ToString(),
                    new Font("宋体", 13, FontStyle.Bold),
                    Brushes.Red,
                    new RectangleF(460, 118 + 190, 30, 20),
                    new StringFormat()
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    }
                    );
                if (_countDown <= 0)
                {
                    _timerCountDown.Stop();
                    _countDown = 10;
                    _isControlBySelf = false;
                    _isShowFrame = false;
                }
            }
        }
    }
}
