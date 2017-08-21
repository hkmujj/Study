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

namespace HD_HXD2_TMS.V6数据输入
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V6_OtherSetting : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();
        private List<Button> _btnsOther = new List<Button>();
        public static Int32 CurrentModeIndex = 1;
        private static String _data = "";
        private static String _value = "0";

        public override string GetInfo()
        {
            return "数据输入界面-其他设置";
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
                    new Rectangle(495 + (i % 4) * 58, 149 + (i / 4) * 58 + 120, 50, 50),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[1],
                        DownImage = _images[2],
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
                    new Rectangle(495 + (10 % 4) * 58, 149 + (10 / 4) * 58 + 120, 108, 50),
                    10,
                    new ButtonStyle()
                    {
                        Background = _images[3],
                        DownImage = _images[4],
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

            Button btn1 = new Button(
                    "存储",
                    new Rectangle(358, 281 + 120, 87, 26),
                    11,
                    new ButtonStyle()
                    {
                        Background = _images[7],
                        DownImage = _images[8],
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
            btn1.ClickEvent += btn_ClickEvent;
            _btns.Add(btn1);

            String[] strs = {"冬季模式", "夏季模式"};
            for (int i = 0; i < 2; i++)
            {
                Button btnOther = new Button(
                    strs[i],
                    new Rectangle(121, 166 + 120+48*i, 93, 35),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[5],
                        DownImage = _images[6],
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
                btnOther.ClickEvent += btnOther_ClickEvent;
                _btnsOther.Add(btnOther);
            }
            _btnsOther[1].IsReplication = false;
            ((ButtonStyle)_btnsOther[CurrentModeIndex].Style).FontStyle.TextBrush = (SolidBrush)Brushes.Black;

            return true;
        }

        void btnOther_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (CurrentModeIndex == e.Message)
            {
                _btnsOther[CurrentModeIndex].IsReplication = false;
                ((ButtonStyle) _btnsOther[CurrentModeIndex].Style).FontStyle.TextBrush = (SolidBrush) Brushes.Black;
                return;
            }
            _btnsOther[CurrentModeIndex].IsReplication = true;
            ((ButtonStyle)_btnsOther[CurrentModeIndex].Style).FontStyle.TextBrush = (SolidBrush)Brushes.White;
            _btnsOther[e.Message].IsReplication = false;
            ((ButtonStyle)_btnsOther[e.Message].Style).FontStyle.TextBrush = (SolidBrush)Brushes.Black;
            CurrentModeIndex = e.Message;

            switch (e.Message)
            {
                case 0:
                    this.SetOutBoolValue( UIObj.OutBoolList[0], 0, 0);
                    break;
                case 1:
                    this.SetOutBoolValue( UIObj.OutBoolList[0], 1, 0);
                    break;
            }
        }

        public override bool mouseDown(Point point)
        {
            _btns.ForEach(a => a.MouseDown(point));
            _btnsOther.ForEach(a => a.MouseDown(point));
            
            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            _btns.ForEach(a => a.MouseUp(point));
            _btnsOther.ForEach(a => a.MouseUp(point));
            
            return base.mouseUp(point);
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 10:
                    if (_data != "") _data = _data.Substring(0, _data.Length - 1);
                    break;
                case 11:
                    _value = ((_data == "") ? "0" : _data);
                    this.SetOutFloatValue( UIObj.OutFloatList[0], 0, Convert.ToSingle(_data));
                    break;
                default:
                    _data += ((e.Message + 1)%10);
                    break;
            }
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(_images[0], new Rectangle(0,120,800,434));
            _btns.ForEach(a => a.Paint(dcGs));
            _btnsOther.ForEach(a => a.Paint(dcGs));

            dcGs.DrawString(
                "目前的模式："+_btnsOther[CurrentModeIndex].Text,
                new Font("宋体", 11),
                Brushes.White,
                new Rectangle(93, 275 + 120+2, 156, 33),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawString(
                _data,
                new Font("宋体", 13),
                Brushes.Black,
                new Rectangle(338, 155 + 120, 122, 24),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.DrawString(
                _value,
                new Font("宋体", 13),
                Brushes.Yellow,
                new Rectangle(478, 74 + 120, 90, 36),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            base.paint(dcGs);
        }

        public static void Reset()
        {
            _data = "";
            _value = "";
        }
    }
}
