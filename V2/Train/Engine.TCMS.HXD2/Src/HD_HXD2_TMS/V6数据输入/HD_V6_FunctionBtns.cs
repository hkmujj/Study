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
    public class HD_V6_FunctionBtns : baseClass
    {
        private List<Button> _btns = new List<Button>();
        private List<Image> _images = new List<Image>();    //图片资源
        private Int32 _currentViewID = 0;
        public static Boolean IsShow { get; set; }

        public override string GetInfo()
        {
            return "数据输入界面-功能按钮";
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

            String[] strs = {"轮径相关", "日期/时间","其他设置","屏幕校准","润滑设定"};
            Int32[] viewIDs =
            {
                (Int32) ViewType.InputWheel, (Int32) ViewType.InputTime,(Int32)ViewType.InputOther,
                (Int32)ViewType.InputScreen,(Int32)ViewType.InputOil
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
            if (IsShow)
            _btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            if (IsShow)
            _btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _currentViewID = nParaB;
                Button btn = _btns.Find(a => a.ID == nParaB);
                if (btn != null)
                {
                    btn.IsReplication = false;
                    _btns.FindAll(a => a.ID != nParaB).ForEach(b => b.IsReplication = true);
                }
                else
                {
                    _btns[0].IsReplication = false;
                    _btns.FindAll(a => a.ID != _btns[0].ID).ForEach(b => b.IsReplication = true);
                }
            }
        }

        public override void paint(Graphics dcGs)
        {
            if (!IsShow) return;
            _btns.ForEach(a => a.Paint(dcGs));

            for (int i = 5; i < 8; i++)
            {
                dcGs.DrawRectangle(Pens.White, new Rectangle(8 + 99 * i, 44, 98, 42));
                dcGs.FillRectangle(Brushes.Black, new Rectangle(8 + 99*i + 1, 44 + 1, 98 - 2, 42 - 2));
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

            if (e.Message == (Int32) ViewType.InputWheel)
            {
                HD_VC_Password.IsShowPassword = true;
                HD_VC_Password.CurrentViewID = _currentViewID;
                HD_VC_Password.GotoViewID = (Int32) ViewType.InputWheel;
            }
            else
            {
                HD_VC_Password.IsShowPassword = false;
                append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
            }
        }

         public static void Reset()
         {
             IsShow = false;
         }
    }
}
