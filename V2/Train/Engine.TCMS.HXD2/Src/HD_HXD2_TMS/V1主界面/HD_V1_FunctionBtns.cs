using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using HD_HXD2_TMS.V2控制;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HD_HXD2_TMS.V1主界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V1_FunctionBtns : baseClass
    {
        private List<Button> _btns = new List<Button>();
        private List<Image> _images = new List<Image>();    //图片资源

        public override string GetInfo()
        {
            return "主界面-功能按钮";
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

            for (int i = 0; i < 8; i++)
            {
                Button btn = new Button(
                    "",
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

        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == 7)
            {
                HD_HXD2_TMS.V0公共组件.HD_VC_ViewChange.IsAddBackBtnStatic = 
                    !HD_HXD2_TMS.V0公共组件.HD_VC_ViewChange.IsAddBackBtnStatic;

                if (HD_HXD2_TMS.V0公共组件.HD_VC_ViewChange.IsAddBackBtnStatic)
                {
                    ((ButtonStyle)_btns[7].Style).Background = _images[8];
                    ((ButtonStyle)_btns[7].Style).DownImage = _images[8];
                }
                else
                {
                    ((ButtonStyle)_btns[7].Style).Background = _images[7];
                    ((ButtonStyle)_btns[7].Style).DownImage = _images[7];
                }
            }
        }
    }
}
