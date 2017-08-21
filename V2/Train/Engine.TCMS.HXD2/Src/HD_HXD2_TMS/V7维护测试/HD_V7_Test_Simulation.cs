using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V7维护测试
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V7_Test_Simulation : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();
        private Rectangle[] _rects = null;

        public override string GetInfo()
        {
            return "维护测试界面-仿真测试";
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
                new Rectangle(48,168,132,45),
                new Rectangle(48,240,132,45),
                new Rectangle(329,240,132,45),
                new Rectangle(621,240,132,45),
                new Rectangle(387,168,167,45),
                new Rectangle(439,431,132,45),
                new Rectangle(584,431,132,45)
            };
            string[] strs = { "仿真关", "速度输入", "网压输入", "数据发送", "顺序试验激活" ,"高压隔离开关","钥匙开关"};
            for (int i = 0; i < 7; i++)
            {
                Int32 index = 0;
                if (i == 4) index = 2;
                if (i == 5) index = 4;
                if (i == 6) index = 6;
                Button btn = new Button(
                    strs[i],
                    _rects[i],
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[0+index],
                        DownImage = _images[1+index],
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

            dcGs.DrawRectangle(Pens.White, new Rectangle(188, 168, 167, 45));
            dcGs.DrawString(
                "仿真模式：关",
                new Font("宋体", 13),
                Brushes.Yellow,
                new Rectangle(188, 168, 167, 45),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.FillRectangle(Brushes.White, new Rectangle(188, 240, 132, 45));
            dcGs.DrawString(
                "0",
                new Font("宋体", 13),
                Brushes.Black,
                new Rectangle(188, 240, 132, 45),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
            dcGs.FillRectangle(Brushes.White, new Rectangle(469, 240, 132, 45));
            dcGs.DrawString(
                "0",
                new Font("宋体", 13),
                Brushes.Black,
                new Rectangle(469, 240, 132, 45),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            base.paint(dcGs);
        }
    }
}
