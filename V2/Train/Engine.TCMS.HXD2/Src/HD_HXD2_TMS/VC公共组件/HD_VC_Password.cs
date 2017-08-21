using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using HD_HXD2_TMS.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.VC公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_Password:baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();
        private String _pasword = "";

        public static Boolean IsShowPassword { get; set; }
        public static Int32 CurrentViewID { get; set; }
        public static Int32 GotoViewID { get; set; }

        public override string GetInfo()
        {
            return "公共界面-密码输入";
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
                    new Rectangle(396 + (i % 4) * 58, 74 + (i / 4) * 58 + 120, 50, 50),
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

            string[] strs = { "确认", "取消" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new Rectangle(151 + (i) * 99, 193 + 120, 92, 38),
                    i + 10,
                    new ButtonStyle()
                    {
                        Background = _images[3],
                        DownImage = _images[4],
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

            Button btnClear = new Button(
                    "清除",
                    new Rectangle(396 + (10 % 4) * 58, 74 + (10 / 4) * 58 + 120, 108, 50),
                    12,
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
                    }
                    );
            btnClear.ClickEvent += btn_ClickEvent;
            _btns.Add(btnClear);

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _pasword = "";
            }
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 10:
                    if (_pasword == "123")
                    {
                        if (GotoViewID != (Int32) ViewType.InputWheel)
                        {
                            IsShowPassword = false;
                            _pasword = "";
                            append_postCmd(CmdType.ChangePage, GotoViewID, 0, 0);
                        }
                    }
                    break;
                case 11:
                    //HD_V6_FunctionBtns.IsShow = false;
                    IsShowPassword = false;
                    _pasword = "";
                    append_postCmd(CmdType.ChangePage, CurrentViewID, 0, 0);
                    break;
                case 12:
                    if(_pasword!="")
                    _pasword = _pasword.Substring(0, _pasword.Length - 1);
                    break;
                default:
                    _pasword += ((e.Message+1)%10);
                    break;
            }
        }

        public override bool mouseDown(Point point)
        {
            if(IsShowPassword)
            _btns.ForEach(a => a.MouseDown(point));
            
            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (IsShowPassword)
            _btns.ForEach(a => a.MouseUp(point));
            
            return base.mouseUp(point);
        }

        public override void paint(Graphics dcGs)
        {
            if (IsShowPassword)
            {
                dcGs.DrawImage(_images[0], new Rectangle(0, 120, 800, 400));
                _btns.ForEach(a => a.Paint(dcGs));
                dcGs.DrawString(
                    _pasword.ToString(),
                    new Font("宋体", 13),
                    Brushes.Black,
                    new Rectangle(153, 111 + 120, 172, 27),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                    );
            }

            base.paint(dcGs);
        }
    }
}
