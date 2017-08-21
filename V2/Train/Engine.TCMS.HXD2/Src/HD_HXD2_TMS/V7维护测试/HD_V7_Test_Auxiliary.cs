using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V7维护测试
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V7_Test_Auxiliary : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();
        private Rectangle[] _rects = null;

        public override string GetInfo()
        {
            return "维护测试界面-辅助测试";
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

            _rects = new Rectangle[]
            {
                new Rectangle(223,195,140,44),
                new Rectangle(223,248,140,44),
                new Rectangle(223,248+53,140,44),
                new Rectangle(223+173,195,140,44),
                new Rectangle(223+173,248,140,44),
                new Rectangle(223+173,248+53,140,44),
                new Rectangle(223+173,248+53*2,140,44)
            };
            string[] strs = { "主压机测试", "冷却塔风机1测试","冷却塔风机2测试","牵引风机1测试","牵引风机2测试","机械间风机1测试","机械间风机2测试"};
            for (int i = 0; i < 7; i++)
            {
                Button btn = new Button(
                    strs[i],
                    _rects[i],
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
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            return true;
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //throw new NotImplementedException();
        }

        public override bool mouseDown(Point point)
        {
            _btns.ForEach(a => a.MouseDown(point));
            
            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            _btns.ForEach(a => a.MouseUp(point));
            
            return base.mouseUp(point);
        }

        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }
    }
}
