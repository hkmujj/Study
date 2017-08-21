using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using HD_HXD2_TMS.V2控制;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using HD_HXD2_TMS.Common;
using MMI.Facility.Interface.Data;
using HD_HXD2_TMS.VC公共组件;

namespace HD_HXD2_TMS.V0公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_ViewChange : baseClass
    {
        private ViewType _currentView = ViewType.Black;
        private List<Button> _btns = new List<Button>();
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btnsBack3 = new List<Button>();
        public static Boolean IsAddBackBtnStatic = false;

        public Boolean IsAddBackBtn
        {
            set
            {
                if (_isAddBackBtn == value)
                    return;

                _isAddBackBtn = value;
                if (value)
                {
                    ((ButtonStyle)_btns[0].Style).Background = _images[4];
                    ((ButtonStyle)_btns[0].Style).DownImage = _images[5];
                    _btns.AddRange(_btnsBack3);
                }
                else
                {
                    if (_btns.Count > 5)
                    {
                        _btns.RemoveRange(5, 3); 
                        ((ButtonStyle)_btns[0].Style).Background = _images[2];
                        ((ButtonStyle)_btns[0].Style).DownImage = _images[3];
                    }
                }
            }
        }
        private Boolean _isAddBackBtn = false;

        public override string GetInfo()
        {
            return "公共试图-视图切换";
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

            String[] btnNames = { "", "机车信息", "控制", "空气制动", "过程数据", "数据输入", "维护测试", "故障浏览" };
            Int32[] viewIDs =
            {
                (Int32) ViewType.Main, (Int32) ViewType.Info, (Int32) ViewType.ControlInsulateA,
                (Int32) ViewType.Breaking, (Int32) ViewType.DataDriveA, (Int32) ViewType.InputWheel,
                (Int32) ViewType.TestMilulation,(Int32) ViewType.FalutHistroyA
            };
            for (int i = 0; i < 8; i++)
            {
                Button btn = new Button(
                    btnNames[i],
                    new RectangleF(0+99*i, 1, 98, 42),
                    viewIDs[i],
                    new ButtonStyle() 
                    { 
                        Background = _images[i==0?2:0],
                        DownImage = _images[i == 0 ? 3 : 1], 
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
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;

                if (i <= 4)
                    _btns.Add(btn);
                else _btnsBack3.Add(btn);
            }
            _btns[0].IsReplication = false;

            return true;
        }

        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _currentView = (ViewType)nParaB;
                Button btn = _btns.Find(a => a.ID == nParaB);
                if (btn != null)
                {
                    _btns[2].ID = HD_V3_FunctionBtns.Btns[0].ID;
                    _btns[4].ID = HD_V5_FunctionBtns.Btns[0].ID;
                    _btns[7].ID = HD_V8_FunctionBtns.Btns[0].ID;
                    btn.IsReplication = false;
                    _btns.FindAll(a => a.ID != nParaB).ForEach(b => b.IsReplication = true);
                }
                //else
                //{
                //    _btns[0].IsReplication = false;
                //    _btns.FindAll(a => a.ID != _btns[0].ID).ForEach(b => b.IsReplication = true);
                //}
            }
        }

        public override void paint(Graphics dcGs)
        {
            IsAddBackBtn = IsAddBackBtnStatic;
            _btns.ForEach(a => a.Paint(dcGs));

            if (!_isAddBackBtn)
            {
                for (int i = 5; i < 8; i++)
                {
                    dcGs.DrawRectangle(Pens.White, new Rectangle(0 + 99 * i, 1, 97, 42));
                }
            }

            base.paint(dcGs);
        }

        void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            _btns.FindAll(a => a.ID != e.Message).ForEach(b =>
            {
                b.IsReplication = true;
            });
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            Button b = _btns.Find(a => a.ID == e.Message);
            if (b != null)
            {
                b.IsReplication = false;
                HD_VC_FalutManager.IsShowCurrentAView = false;
                HD_VC_FalutManager.IsShowCurrentBView = false;
                HD_VC_FalutManager.IsShowCurrentView = false;
                HD_VC_Password.IsShowPassword = false;

                if ((ViewType) b.ID == ViewType.InputWheel)
                {
                    HD_V6_FunctionBtns.IsShow = true;
                    HD_VC_Password.IsShowPassword = true;
                    HD_VC_Password.CurrentViewID = (Int32)_currentView;
                    HD_VC_Password.GotoViewID = (Int32) ViewType.InputWheel;
                }
                else
                {
                    HD_V6_FunctionBtns.IsShow = false;
                    append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
                }
            }
        }
    }
}
