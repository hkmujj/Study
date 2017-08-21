using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Drawing;

namespace HD_HXD2_TMS.VC公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_PhaseFalut:baseClass
    {
        private Button _btn = null;
        private bool _oldValue = false;

        public Boolean IsTrigger
        {
            set
            {
                if (_isTrigger == value) return;
                _isTrigger = value;
            }
        }
        private Boolean _isTrigger = false;

        public override string GetInfo()
        {
            return "故障浏览界面-历史故障A";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _btn = new Button(
                "",
                new RectangleF(0, 0, 800, 600),
                0,
                new ButtonStyle()
                {
                    Background = null,
                    DownImage = null,
                    FontStyle = new FontStyle_ES()
                    {
                        Font = new Font("宋体", 10),
                        TextBrush = (SolidBrush) Brushes.Transparent
                    }
                }
                );
            _btn.ClickEvent += _btn_ClickEvent;
            return true;
        }

        void _btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _isTrigger = false;
        }

        public override bool mouseDown(Point point)
        {
            if (_isTrigger)
            _btn.MouseDown(point);

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (_isTrigger)
            _btn.MouseUp(point);

            return base.mouseUp(point);
        }

        public override void paint(Graphics dcGs)
        {
            Boolean value = BoolList[UIObj.InBoolList[0]];
            if (value != _oldValue)
            {
                IsTrigger = value;
                _oldValue = value;
            }

            if (_isTrigger)
            {
                dcGs.FillRectangle(Brushes.Yellow, new Rectangle(200, 170, 400, 260));
                dcGs.DrawString(
                    "过分相装置故障，\n过分相时请手动断主断\n（点击确认）",
                    new Font("宋体", 25, FontStyle.Bold),
                    Brushes.Black,
                    new Rectangle(200, 170, 400, 260),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
                _btn.Paint(dcGs);
            }

            base.paint(dcGs);
        }
    }
}
