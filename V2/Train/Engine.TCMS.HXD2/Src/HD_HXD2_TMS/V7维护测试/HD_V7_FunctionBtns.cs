using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using HD_HXD2_TMS.VC公共组件;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using HD_HXD2_TMS.Common;
using MMI.Facility.Interface.Data;

namespace HD_HXD2_TMS.V2控制
{
     [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V7_FunctionBtns : baseClass
    {
        private List<Button> _btns = new List<Button>();
        private List<Image> _images = new List<Image>();    //图片资源
         private ViewType _currentViewType = ViewType.TestMilulation;

        public override string GetInfo()
        {
            return "维护测试界面-功能按钮";
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

            String[] strs = {"仿真测试", "轮缘","辅助测试","库内380V","过分相\n测试"};
            Int32[] viewIDs =
            {
                (Int32) ViewType.TestMilulation, (Int32) ViewType.TestWheel,(Int32)ViewType.TestAuxiliary,
                (Int32)ViewType.Test380V,(Int32)ViewType.TestPhase
            };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new RectangleF(8 + 99 * i, 44, 98, 42),
                    viewIDs[i],
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
                    },
                    false
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            _btns[0].IsReplication = false;

            return true;
        }

        public override bool mouseDown(Point nPoint)
        {
            if (!HD_V6_FunctionBtns.IsShow)
            _btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            if (!HD_V6_FunctionBtns.IsShow)
            _btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _currentViewType = (ViewType) nParaB;
                Button btn = _btns.Find(a => a.ID == nParaB);
                if (btn != null)
                {
                    btn.IsReplication = false;
                }

                _btns.FindAll(a => a.ID != nParaB).ForEach(b => b.IsReplication = true);
            }
        }

        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));

            for (int i = 5; i < 8; i++)
            {
                dcGs.DrawRectangle(Pens.White, new Rectangle(8 + 99 * i, 44, 98, 42));
            }

            base.paint(dcGs);
        }

        void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            _btns.ForEach(a => a.IsReplication = true);
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            HD_VC_FalutManager.IsShowCurrentView = false;
            HD_VC_FalutManager.IsShowCurrentAView = false;
            HD_VC_FalutManager.IsShowCurrentBView = false;
            HD_VC_Password.IsShowPassword = false;

            if (((ViewType) e.Message) == ViewType.TestPhase)
            {
                HD_VC_Password.IsShowPassword = true;
                HD_VC_Password.CurrentViewID = (Int32) _currentViewType;
                HD_VC_Password.GotoViewID = (Int32) ViewType.TestPhase;
            }
            else
            {
                append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
            }
        }
    }
}
