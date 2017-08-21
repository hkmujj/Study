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

namespace HD_HXD2_TMS.VC公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_CurrentFalut : baseClass
    {
        private List<Image> _images = new List<Image>();
        private List<Button> _btns = new List<Button>();
        private List<Rectangle> _rectTitle = null;
        private String[] _strs1 = null;
        private List<Rectangle> _rect = null;

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

            _rectTitle = new List<Rectangle>()
            {
                new Rectangle(70-30, 144, 90, 20),
                new Rectangle(70-30, 196, 90, 20),
                new Rectangle(80-30, 291, 90, 20),
                new Rectangle(80-30, 377, 90, 20),
                new Rectangle(80-30, 513, 90, 20),
                new Rectangle(262-30, 144, 140, 20),
                new Rectangle(380-30, 513, 90, 20)
            };
            _rect = new List<Rectangle>
            {
                new Rectangle(70+90-30, 145, 90, 20),
                new Rectangle(70+30-30, 196+20, 500, 20),
                new Rectangle(80+10-30, 291+20, 200, 20),
                new Rectangle(80+20-30, 377+20, 700, 20),
                new Rectangle(80+80-30, 513, 130, 20),
                new Rectangle(262+120-30, 144, 300, 20),
                new Rectangle(380+90-30, 513, 90, 20)
            };

            _strs1 = new String[]
            {
                "机车编号：","故障事件：","故障点：","解决方法：","错误代码：","故障发生时间：","故障级别："
            };

            String[] strs = { "确认", "未解决故障" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new RectangleF(594, 138+312*i, 102, 33),
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
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            return true;
        }

        void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            if (HD_VC_FalutManager.IsShowCurrentView)
                ((ButtonStyle)_btns[e.Message].Style).FontStyle.TextBrush = (SolidBrush)Brushes.Black;
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (!HD_VC_FalutManager.IsShowCurrentView)
                return;

            ((ButtonStyle)_btns[e.Message].Style).FontStyle.TextBrush = (SolidBrush)Brushes.White;
            switch (e.Message)
            {
                case 0:
                    if (HD_VC_FalutManager.CurrentFalut != null)
                    {
                        HD_VC_FalutManager.CurrentFalut.IsOK = true;
                        FalutInfo fi = HD_VC_FalutManager.CurrentFalut;
                        HD_VC_FalutManager.CurrentFalut = null;
                        if (HD_VC_FalutManager.FalutsACurrent.Count != 0)
                        {
                            foreach (var i in HD_VC_FalutManager.FalutsACurrent)
                            {
                                if (!i.IsOK)
                                {
                                    if (HD_VC_FalutManager.CurrentFalut == null)
                                        HD_VC_FalutManager.CurrentFalut = i;
                                    else if (((Int32)HD_VC_FalutManager.CurrentFalut.Grade)<((Int32)i.Grade))
                                    {
                                        HD_VC_FalutManager.CurrentFalut = i;
                                    }
                                }
                            }
                        }
                        if (HD_VC_FalutManager.FalutsBCurrent.Count != 0)
                        {
                            foreach (var i in HD_VC_FalutManager.FalutsBCurrent)
                            {
                                if (!i.IsOK)
                                {
                                    if (HD_VC_FalutManager.CurrentFalut == null)
                                        HD_VC_FalutManager.CurrentFalut = i;
                                    else if (((Int32)HD_VC_FalutManager.CurrentFalut.Grade) < ((Int32)i.Grade))
                                    {
                                        HD_VC_FalutManager.CurrentFalut = i;
                                    }
                                }
                            }
                        }
                        if (HD_VC_FalutManager.CurrentFalut == null)
                            HD_VC_FalutManager.CurrentFalut = fi;
                    }
                    break;
                case 1:
                    HD_VC_FalutManager.IsShowCurrentView = false;
                    HD_VC_FalutManager.IsShowCurrentAView = true;
                    //append_postCmd(CmdType.ChangePage, (Int32)ViewType.CurrentFalutA, 0, 0);
                    break;
            }
        }

        public override bool mouseDown(Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns.ForEach(a => a.MouseDown(point));

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns.ForEach(a => a.MouseUp(point));

            return base.mouseUp(point);
        }

        public override void paint(Graphics dcGs)
        {
            if (!HD_VC_FalutManager.IsShowCurrentView)
                return;

            dcGs.FillRectangle(Brushes.Black, 1, 119, 798, 451);

            String[] strs = new String[7];
            if (HD_VC_FalutManager.CurrentFalut != null)
            {
                strs[0] = HD_VC_FalutManager.CurrentFalut.TrainID.ToString();
                strs[1] = HD_VC_FalutManager.CurrentFalut.Name;
                strs[2] = HD_VC_FalutManager.CurrentFalut.Point;
                strs[3] = HD_VC_FalutManager.CurrentFalut.PointOut;
                strs[4] = HD_VC_FalutManager.CurrentFalut.Code;
                strs[5] = HD_VC_FalutManager.CurrentFalut.StartTime;
                strs[6] = HD_VC_FalutManager.CurrentFalut.Grade.ToString();
            }

            for (int i = 0; i < 7; i++)
            {
                dcGs.DrawString(
                    _strs1[i],
                    new Font("宋体", 11),
                    Brushes.White,
                    _rectTitle[i],
                    new StringFormat()
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    }
                    );

                dcGs.DrawString(
                    strs[i],
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    _rect[i],
                    new StringFormat()
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    }
                    );
            }

            _btns.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }
    }
}
