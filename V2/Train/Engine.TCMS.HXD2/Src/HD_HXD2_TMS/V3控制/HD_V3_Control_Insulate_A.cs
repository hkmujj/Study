using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Timers;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using HD_HXD2_TMS.Common;
using HD_HXD2_TMS.Extension;
using MMI.Facility.Interface.Data;

namespace HD_HXD2_TMS.V3控制
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V3_Control_Insulate_A : baseClass
    {
        private List<Image> _images = new List<Image>();
        private List<Controler> _controlers = new List<Controler>();
        private List<Button> _btns = new List<Button>();
        private Button _btnClearFalut = null;
        private Boolean _isShowFrame = false;
        private Timer _timer = null;
        private Int32 _countDown = 10;
        private List<Button> _btns_OK = new List<Button>();
        private Timer _timeReset = null;
        private Int32 _count = 0;

        public override string GetInfo()
        {
            return "控制试图-A车";
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
            for (int i = 0; i < 2; i++)
            {
                Controler c = new Controler(
                    "牵引变流器"+(i+1),
                    new Rectangle(135+314*i, 20 + 118, 91, 35),
                    new ControlInfo()
                    {
                        Image = _images[2],
                        InDataLogic = UIObj.InBoolList[i],
                        IsUsed = true,
                        OutDataLogic = UIObj.OutBoolList[i*2+0],
                        ControlType = ControlType.Normal
                    },
                    new ControlInfo()
                    {
                        Image = _images[3],
                        InDataLogic = UIObj.InBoolList[i] + 1,
                        IsUsed = false,
                        OutDataLogic = UIObj.OutBoolList[i * 2 + 1],
                        ControlType = ControlType.Falut
                    },
                    new ControlInfo()
                    {
                        Image = _images[4],
                        InDataLogic = UIObj.InBoolList[i] + 2,
                        IsUsed = false,
                        OutDataLogic = UIObj.OutBoolList[i*2+1],
                        ControlType = ControlType.Handle
                    },
                    _images[1],
                    this
                    );
                _controlers.Add(c);
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Controler c = new Controler(
                        "四象限整流器" + (i*2+j + 1),
                        new Rectangle(68 + 114 * j+315*i, 100 + 118, 100, 35),
                        new ControlInfo()
                        {
                            Image = _images[5],
                            InDataLogic = UIObj.InBoolList[i * 2 + j + 2],
                            IsUsed = true,
                            OutDataLogic = UIObj.OutBoolList[4 + i * 4 + j*2 + 0],
                            ControlType = ControlType.Normal
                        },
                        new ControlInfo()
                        {
                            Image = _images[6],
                            InDataLogic = UIObj.InBoolList[i * 2 + j + 2] + 1,
                            IsUsed = false,
                            OutDataLogic = UIObj.OutBoolList[4 + i * 4 + j * 2 + 1],
                            ControlType = ControlType.Falut
                        },
                        new ControlInfo()
                        {
                            Image = _images[7],
                            InDataLogic = UIObj.InBoolList[i * 2 + j + 2] + 2,
                            IsUsed = false,
                            OutDataLogic = UIObj.OutBoolList[4 + i * 4 + j*2 + 1],
                            ControlType = ControlType.Handle
                        },
                        _images[1],
                        this
                        );
                    _controlers.Add(c);
                }
            }
            String[] strs = {"电机逆变器1", "电机逆变器2", "辅助逆变器1", "辅助逆变器2", "电机逆变器3", "电机逆变器4"};
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Controler c = new Controler(
                        strs[i * 3 + j],
                        new Rectangle(23 + 105 * j + 315 * i, 207 + 118, 91, 35),
                        new ControlInfo()
                        {
                            Image = _images[2],
                            InDataLogic = UIObj.InBoolList[i * 3 + j + 6],
                            IsUsed = true,
                            OutDataLogic = UIObj.OutBoolList[12 + i * 6 + j * 2 + 0],
                            ControlType = ControlType.Normal
                        },
                        new ControlInfo()
                        {
                            Image = _images[3],
                            InDataLogic = UIObj.InBoolList[i * 3 + j + 6] + 1,
                            IsUsed = false,
                            OutDataLogic = UIObj.OutBoolList[12 + i * 6 + j * 2 + 1],
                            ControlType = ControlType.Falut
                        },
                        new ControlInfo()
                        {
                            Image = _images[4],
                            InDataLogic = UIObj.InBoolList[i * 3 + j + 6] + 2,
                            IsUsed = false,
                            OutDataLogic = UIObj.OutBoolList[12 + i * 6 + j * 2 + 1],
                            ControlType = ControlType.Handle
                        },
                        _images[1],
                        this
                        );
                    _controlers.Add(c);
                }
            }

            String[] strs1 = { "警惕控制", "动力制动", "空压机", "轮缘润滑", "自动撒沙切除", "空电联合切除" };
            for (int i = 0; i < 6; i++)
            {
                Controler c = new Controler(
                    strs1[i],
                    new Rectangle(670, 20 + 118+50*i, 100, 35),
                    new ControlInfo()
                    {
                        Image = _images[5],
                        InDataLogic = UIObj.InBoolList[i+12],
                        IsUsed = true,
                        OutDataLogic = UIObj.OutBoolList[i * 2 + 24],
                        ControlType = ControlType.Normal
                    },
                    new ControlInfo()
                    {
                        Image = _images[6],
                        InDataLogic = UIObj.InBoolList[i + 12] + 1,
                        IsUsed = false,
                        OutDataLogic = UIObj.OutBoolList[i * 2 + 24 + 1],
                        ControlType = ControlType.Falut
                    },
                    new ControlInfo()
                    {
                        Image = _images[7],
                        InDataLogic = UIObj.InBoolList[i + 12] + 2,
                        IsUsed = false,
                        OutDataLogic = UIObj.OutBoolList[i * 2 + 24+1],
                        ControlType = ControlType.Handle
                    },
                    _images[1],
                    this
                    );
                _controlers.Add(c);
            }

            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(5+75*i, 117 + 35 + 47 * 6 + 30 + 20, 70, 30),
                    301+i,
                    new ButtonStyle()
                    {
                        Background = _images[8+i*2],
                        DownImage = _images[9+i*2],
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("宋体", 13),
                            TextBrush = (SolidBrush)Brushes.White,
                            StringFormat = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            }
                        }
                    },
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            _btns[0].IsReplication = false;

            _btnClearFalut = new Button(
                "清除故障",
                new RectangleF(670, 342 + 118, 100, 35),
                0,
                new ButtonStyle()
                {
                    Background = _images[12],
                    DownImage = _images[13],
                    FontStyle = new FontStyle_ES()
                    {
                        Font = new Font("宋体", 11),
                        TextBrush = (SolidBrush) Brushes.White,
                        StringFormat = new StringFormat()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        }
                    }
                }
                );
            _btnClearFalut.ClickEvent += _btnClearFalut_ClickEvent;
            _btnClearFalut.MouseDownEvent += _btnClearFalut_MouseDownEvent;

            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            _timeReset = new Timer(1000);
            _timeReset.Elapsed += _timeReset_Elapsed;

            for (int i = 0; i < 2; i++)
            {
                Button btn1 = new Button(
                    "",
                    new Rectangle(300 + 127*i, 230 + 118, 78, 32),
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
                            TextBrush = (SolidBrush) Brushes.Black
                        }
                    }
                    );
                btn1.ClickEvent += btn1_ClickEvent;
                _btns_OK.Add(btn1);
            }

            return true;
        }

        void _timeReset_Elapsed(object sender, ElapsedEventArgs e)
        {
            _count++;
        }

        void btn1_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    _isShowFrame = false;
                    _timeReset.Start();
                    _timer.Stop();
                    _countDown = 10;
                    this.SetOutBoolValue( UIObj.OutBoolList[36], 1, 0);
                    break;
                case 1:
                    _isShowFrame = false;
                    _timer.Stop();
                    _countDown = 10;
                    break;
            }
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _countDown--;
        }

        void _btnClearFalut_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
        }

        void _btnClearFalut_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _isShowFrame = true;
            _timer.Start();
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _btns[1].IsReplication = true;
            append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
        }

        public override bool mouseDown(Point point)
        {
            if (VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            Boolean isShowFrame = _isShowFrame;
            Controler c = null;
            if (!isShowFrame)
            {
                c = _controlers.Find(a => a.IsShowFrame);
                if (c != null)
                {
                    isShowFrame = true;
                }
            }
            if (!isShowFrame)
            {
                _controlers.ForEach(a => a.MouseDwon(point));
                _btnClearFalut.MouseDown(point);
            }
            else
            {
                if (c != null) c.MouseDwon(point);
            }

            if (_isShowFrame)
            _btns_OK.ForEach(a => a.MouseDown(point));

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            Boolean isShowFrame = _isShowFrame;
            Controler c = null;
            if (!isShowFrame)
            {
                c = _controlers.Find(a => a.IsShowFrame);
                if (c != null)
                {
                    isShowFrame = true;
                }
            }
            if (!isShowFrame)
            {
                _controlers.ForEach(a => a.MouseUp(point));
                _btnClearFalut.MouseUp(point);
            }
            else
            {
                if (c != null) c.MouseUp(point);
            }
            if (_isShowFrame)
            _btns_OK.ForEach(a => a.MouseUp(point));

            return base.mouseUp(point);
        }

        public override void paint(Graphics dcGs)
        {
            if (VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return;

            //线框
            dcGs.DrawImage(_images[0], new Rectangle(0,118,800,424));
            _btnClearFalut.Paint(dcGs);

            //隔离阀状态
            String[] strs = {"紧急制动阀", "停放制动隔离塞门", "制动缸1隔离塞门", "制动缸2隔离塞门"};
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[18 + i] + j])
                    {
                        dcGs.DrawImage(_images[5 + j], new Rectangle(198 + 112*i, 118 + 296, 100, 35));

                        dcGs.DrawString(
                            strs[i],
                            new Font("宋体", 9, FontStyle.Bold),
                            Brushes.Black,
                            new RectangleF(198 + 112*i-5, 118 + 296, 100+10, 35),
                            new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            }
                            );
                    }
                }
            }

            dcGs.DrawString(
                 "隔离阀状态",
                 new Font("宋体", 9),
                 Brushes.White,
                 new RectangleF(195, 118 + 270, 70, 20),
                 new StringFormat()
                 {
                     LineAlignment = StringAlignment.Center,
                     Alignment = StringAlignment.Near
                 }
                 );
            _controlers.ForEach(a => a.Paint(dcGs));
            _controlers.ForEach(a => a.PaintFrame(dcGs));

            if (_isShowFrame)
            {
                dcGs.DrawImage(_images[1], new Rectangle(227, 96 + 118, 350, 195));
                _btns_OK.ForEach(a => a.Paint(dcGs));
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
                    _timer.Stop();
                    _countDown = 10;
                    _isShowFrame = false;
                }
            }

            if (_count >= 4)
            {
                _timeReset.Stop();
                _count = 0;
                this.SetOutBoolValue( UIObj.OutBoolList[36], 0, 0);
            }


            base.paint(dcGs);
        }
    }
}
