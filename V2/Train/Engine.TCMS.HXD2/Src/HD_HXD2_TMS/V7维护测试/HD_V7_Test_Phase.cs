
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using HD_HXD2_TMS.Extension;

namespace HD_HXD2_TMS.V7维护测试
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V7_Test_Phase : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();
        private Rectangle[] _rects1 = null;
        private static String _data = "";
        private Button _btnSend = null;

        public override string GetInfo()
        {
            return "维护测试界面-过分相测试";
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
            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button(
                    ((i + 1) % 10).ToString(),
                    new Rectangle(280 + (i % 4) * 58+8, 332 + (i / 4) * 58+8, 50, 50),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[0],
                        DownImage = _images[1],
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
                    }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            Button btnClear = new Button(
                    "清除",
                    new Rectangle(280 + (10 % 4) * 58+8, 332 + (10 / 4) * 58 + 8, 108, 50),
                    10,
                    new ButtonStyle()
                    {
                        Background = _images[2],
                        DownImage = _images[3],
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
                    }
                    );
            btnClear.ClickEvent += btn_ClickEvent;
            _btns.Add(btnClear);

            string[] strs = { "自动过分相测试激活", "自动过分相测试取消" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new Rectangle(190 + (i) * 250, 180, 170, 50),
                    i + 11,
                    new ButtonStyle()
                    {
                        Background = _images[4+i*2],
                        DownImage = _images[4+i*2],
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("宋体", 13),
                            TextBrush = (SolidBrush)Brushes.Black,
                            StringFormat = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            }
                        }
                    }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            _btnSend = new Button(
                    "数据发送",
                    new Rectangle(430, 260, 150, 40),
                    13,
                    new ButtonStyle()
                    {
                        Background = _images[8],
                        DownImage = _images[9],
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
            _btnSend.ClickEvent += _btnSend_ClickEvent;

            return true;
        }

        void _btnSend_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (!_btnSend.IsReplication)
            {
                ((ButtonStyle)_btnSend.Style).FontStyle.TextBrush = (SolidBrush)Brushes.Black;
                this.SetOutFloatValue( UIObj.OutFloatList[0], 0, _data == "" ? 0 : Convert.ToSingle(_data));
            }
            else
            {
                ((ButtonStyle)_btnSend.Style).FontStyle.TextBrush = (SolidBrush)Brushes.White;
                this.SetOutFloatValue( UIObj.OutFloatList[0], 0, 0);
            }
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 10://清除
                    if(_data!="")
                    _data = _data.Substring(0, _data.Length - 1);
                    break;
                case 11:
                    if (_index == e.Message)
                    {
                        this.SetOutBoolValue( UIObj.OutBoolList[0], 0, 0);
                    }
                    break;
                case 12:
                    if (_index == e.Message)
                    {
                        this.SetOutBoolValue( UIObj.OutBoolList[0], 1, 0);
                    }
                    break;
                default:
                    _data += ((e.Message + 1)%10);
                    break;
            }
        }

        public override bool mouseDown(Point point)
        {
            _btns.ForEach(a => a.MouseDown(point));
            _btnSend.MouseDown(point);
            
            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            _btns.ForEach(a => a.MouseUp(point));
            _btnSend.MouseUp(point);
            
            return base.mouseUp(point);
        }

        private Int32 _index = 12;
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(Pens.White, new Rectangle(280, 332, 240, 182));

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[i] + j])
                    {
                        if (j == 1) _index = 11+i;
                        ((ButtonStyle) _btns[11+i].Style).Background = _images[4+2*j];
                        ((ButtonStyle)_btns[11 + i].Style).DownImage = _images[4 + 2 * j];
                    }
                }
            }
            _btns.ForEach(a => a.Paint(dcGs));
            _btnSend.Paint(dcGs);

            dcGs.FillRectangle(Brushes.White, new Rectangle(300, 260, 120, 40));
            dcGs.DrawString(
                _data,
                new Font("宋体", 13),
                Brushes.Black,
                new Rectangle(300, 260, 120, 40),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                );

            base.paint(dcGs);
        }

        public static void Reset()
        {
            _data = "";
        }
    }
}
