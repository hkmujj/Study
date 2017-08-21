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

namespace HD_HXD2_TMS.V2机车信息
{
     [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V2_FunctionBtns : baseClass
    {
        private List<Button> _btns = new List<Button>();
        private List<Image> _images = new List<Image>();    //图片资源

        public override string GetInfo()
        {
            return "机车信息界面-功能按钮";
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

            for (int i = 0; i < 1; i++)
            {
                Button btn = new Button(
                    "机车概况",
                    new RectangleF(8 + 99 * i, 44, 98, 42),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[i],
                        DownImage = _images[i],
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
                //_currentView = (ViewType)nParaB;
            }
        }

        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));

            for (int i = 1; i < 8; i++)
            {
                dcGs.DrawRectangle(Pens.White, new Rectangle(8 + 99 * i, 44, 98, 42));
            }

            base.paint(dcGs);
        }

        void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            //_btns.FindAll(a => a.ID != e.Message).ForEach(b =>
            //{
            //    b.IsReplication = true;
            //});
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView = false;
            HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView = false;
            HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView = false;

            append_postCmd(CmdType.ChangePage, (Int32)ViewType.Info, 0, 0);
        }
    }
}
