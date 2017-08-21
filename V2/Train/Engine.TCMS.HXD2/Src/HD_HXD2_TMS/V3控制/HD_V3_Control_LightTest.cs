using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using HD_HXD2_TMS.Extension;

namespace HD_HXD2_TMS.V3控制
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V3_Control_LightTest : baseClass
    {
        private List<Image> _images = new List<Image>();
        private Button _btn = null;

        public override string GetInfo()
        {
            return "控制试图-指示灯测试";
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
            _btn = new Button(
                "指示灯测试",
                new Rectangle(313, 139 + 119, 156, 56),
                0,
                new ButtonStyle()
                {
                    Background = _images[0],
                    DownImage = _images[1],
                    FontStyle = new FontStyle_ES()
                    {
                        Font = new Font("宋体", 11),
                        StringFormat =
                            new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            },
                        TextBrush = (SolidBrush)Brushes.White
                    }
                }
                );
            _btn.ClickEvent += _btn_ClickEvent;
            _btn.MouseDownEvent += _btn_MouseDownEvent;

            return true;
        }

        void _btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            this.SetOutBoolValue( UIObj.OutBoolList[0], 1, 0);
        }

        public override bool mouseDown(Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btn.MouseDown(point);

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btn.MouseUp(point);

            return base.mouseUp(point);
        }

        void _btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            this.SetOutBoolValue( UIObj.OutBoolList[0], 0, 0);
        }

        public override void paint(Graphics dcGs)
        {
            _btn.Paint(dcGs);

            base.paint(dcGs);
        }
    }
}
