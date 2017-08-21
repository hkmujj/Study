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
using HD_HXD2_TMS.Extension;

namespace HD_HXD2_TMS.V3控制
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V3_Control_Bow_A : baseClass
    {
        private List<Image> _images = new List<Image>();
        private List<Button> _btns = new List<Button>();
        private Timer _timer = null;
        private Int32 _count = 0;
        private Int32 _btnIndex = 0;
        private String[] _strs = { "受电弓隔离", "受电弓隔离取消" };
        private bool _isStartCount = false;

        public override string GetInfo()
        {
            return "控制试图-A车受电弓";
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
                Button btn1 = new Button(
                    _strs[i],
                    new Rectangle(34, 173 + 119+58*i, 109, 38),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[6],
                        DownImage = _images[6],
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
                btn1.ClickEvent += btn1_ClickEvent;
                _btns.Add(btn1);
            }

            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;

            return true;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _count++;
        }

        void btn1_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    _count = 0;
                    _isStartCount = true;
                    _btns[_btnIndex].Text = _strs[_btnIndex];
                    ((ButtonStyle) _btns[_btnIndex].Style).Background = _images[6];
                    ((ButtonStyle) _btns[_btnIndex].Style).DownImage = _images[6];
                    _timer.Start();
                    _btnIndex = 0;
                    this.SetOutBoolValue( UIObj.OutBoolList[1], 0, 0);
                    this.SetOutBoolValue( UIObj.OutBoolList[0], 1, 0);
                    break;
                case 1:
                    _count = 0;
                    _isStartCount = true;
                    _timer.Start();
                    _btns[_btnIndex].Text = _strs[_btnIndex];
                    ((ButtonStyle) _btns[_btnIndex].Style).Background = _images[6];
                    ((ButtonStyle) _btns[_btnIndex].Style).DownImage = _images[6];
                    _btnIndex = 1;
                    this.SetOutBoolValue( UIObj.OutBoolList[0], 0, 0);
                    this.SetOutBoolValue( UIObj.OutBoolList[1], 1, 0);
                    break;
            }
        }

        public override bool mouseDown(Point point)
        {
            if (VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns.ForEach(a => a.MouseDown(point));

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns.ForEach(a => a.MouseUp(point));

            return base.mouseUp(point);
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(_images[0], new Rectangle(0,119,800,436));

            _btns.ForEach(a => a.Paint(dcGs));
            if (_isStartCount)
            {
                if (_count%2 == 0)
                {
                    _btns[_btnIndex].Text = "";
                    ((ButtonStyle) _btns[_btnIndex].Style).Background = _images[7];
                    ((ButtonStyle) _btns[_btnIndex].Style).DownImage = _images[7];
                }
                else
                {
                    _btns[_btnIndex].Text = _strs[_btnIndex];
                    ((ButtonStyle) _btns[_btnIndex].Style).Background = _images[6];
                    ((ButtonStyle) _btns[_btnIndex].Style).DownImage = _images[6];
                }
            }
            if (_count >= 3)
            {
                _isStartCount = false;
                _timer.Stop();
                _count = 0;
                this.SetOutBoolValue( UIObj.OutBoolList[_btnIndex], 0, 0);
            }


            dcGs.DrawString(
                 "HXD2"+FloatList[UIObj.InFloatList[0]]+"A",
                 new Font("宋体", 18, FontStyle.Bold),
                 Brushes.White,
                 new RectangleF(306, 93 + 119, 175, 40),
                 new StringFormat()
                 {
                     LineAlignment = StringAlignment.Center,
                     Alignment = StringAlignment.Center
                 }
                 );

            //受电弓
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                    dcGs.DrawImage(_images[1 + i], new Rectangle(322, 28 + 119, 42, 42));
            }
            //司机室占用
            if (BoolList[UIObj.InBoolList[1]])
                dcGs.DrawImage(_images[5], new Rectangle(272, 87 + 119, 29, 18));

            base.paint(dcGs);
        }
    }
}
