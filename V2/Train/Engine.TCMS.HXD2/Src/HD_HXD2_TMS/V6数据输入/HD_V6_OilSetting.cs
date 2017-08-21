using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V3控制
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V6_OilSetting : baseClass
    {
        private List<Image> _images = new List<Image>();
        private Button _btn = null;

        public override string GetInfo()
        {
            return "数据输入试图-润滑设定";
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
            //_btn = new Button(
            //    "屏幕调整",
            //    new Rectangle(313, 139 + 119, 156, 56),
            //    0,
            //    new ButtonStyle()
            //    {
            //        Background = _images[0],
            //        DownImage = _images[1],
            //        FontStyle = new FontStyle_ES()
            //        {
            //            Font = new Font("宋体", 11),
            //            StringFormat =
            //                new StringFormat()
            //                {
            //                    Alignment = StringAlignment.Center,
            //                    LineAlignment = StringAlignment.Center
            //                },
            //            TextBrush = (SolidBrush)Brushes.White
            //        }
            //    }
            //    );
            //_btn.ClickEvent += _btn_ClickEvent;
            //_btn.MouseDownEvent += _btn_MouseDownEvent;

            return true;
        }

        void _btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            //this.SetOutBoolValue( UIObj.OutBoolList[0], 1, 0);
        }

        public override bool mouseDown(Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            //_btn.MouseDown(point);

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            //_btn.MouseUp(point);

            return base.mouseUp(point);
        }

        void _btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //this.SetOutBoolValue( UIObj.OutBoolList[0], 0, 0);
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            dcGs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //_btn.Paint(dcGs);
            dcGs.DrawImage(_images[0], new Rectangle(0,119,799,435));

            base.paint(dcGs);
        }
    }
}
