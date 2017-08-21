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
    public class HD_V7_Test_Wheel : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();
        private Rectangle[] _rects = null;

        public override string GetInfo()
        {
            return "维护测试界面-轮缘测试";
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
                new Rectangle(335,395,129,41),
                new Rectangle(335,448,129,41)
            };
            string[] strs = { "运行测试", "取消"};
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    strs[i],
                    _rects[i],
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[1],
                        DownImage = _images[2],
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
            dcGs.DrawImage(_images[0], new Rectangle(0, 120, 800, 400));
            _btns.ForEach(a => a.Paint(dcGs));

            dcGs.DrawString(
                "返回0秒",
                new Font("宋体", 13),
                Brushes.Yellow,
                new Rectangle(211, 167, 363, 33),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                );

            base.paint(dcGs);
        }
    }
}
