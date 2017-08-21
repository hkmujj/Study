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
    public class HD_V7_Test_380V : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();
        private Rectangle[] _rects = null;

        //private String[] _strs1 = null;
        //private String[] _strs2 = null;

        public override string GetInfo()
        {
            return "维护测试界面-380V测试";
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

            //_strs1 = new String[] { "QS-GHV", "压缩机1状态", "库", "K-Q(PP)XSA1", "压缩机2状态", "QF(M)" };
            //_strs2 = new String[] { "正常位", "停止", "正常位", "相序错误", "停止", "停止" };

            //_rects = new Rectangle[]
            //{
            //    new Rectangle(335,395,129,41),
            //    new Rectangle(335,448,129,41)
            //};
            //string[] strs = { "运行测试", "取消"};
            //for (int i = 0; i < 2; i++)
            //{
            //    Button btn = new Button(
            //        strs[i],
            //        _rects[i],
            //        i,
            //        new ButtonStyle()
            //        {
            //            Background = _images[1],
            //            DownImage = _images[2],
            //            FontStyle = new FontStyle_ES()
            //            {
            //                Font = new Font("宋体", 13),
            //                TextBrush = (SolidBrush)Brushes.White,
            //                StringFormat = new StringFormat()
            //                {
            //                    Alignment = StringAlignment.Center,
            //                    LineAlignment = StringAlignment.Center
            //                }
            //            }
            //        }
            //        );
            //    btn.ClickEvent += btn_ClickEvent;
            //    _btns.Add(btn);
            //}

            return true;
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //throw new NotImplementedException();
        }

        //public override bool mouseDown(Point point)
        //{
        //    _btns.ForEach(a => a.MouseDown(point));
            
        //    return base.mouseDown(point);
        //}

        //public override bool mouseUp(Point point)
        //{
        //    _btns.ForEach(a => a.MouseUp(point));
            
        //    return base.mouseUp(point);
        //}

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(_images[0], new Rectangle(0, 120, 800, 400));

            //for (int i = 0; i < 6; i++)
            //{
            //    dcGs.DrawRectangle(Pens.White, new Rectangle(119 + (i / 3) * 284, 423 + (i % 3) * 28, 136, 28));
            //    dcGs.DrawString(
            //        _strs1[i],
            //        new Font("宋体", 13),
            //        Brushes.White,
            //        new Rectangle(119 + (i / 3) * 284, 423 + (i % 3) * 28, 136, 28),
            //        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
            //        );

            //    dcGs.DrawRectangle(Pens.White, new Rectangle(255 + (i / 3) * 284, 423 + (i % 3) * 28, 126, 28));
            //    dcGs.DrawString(
            //        _strs2[i],
            //        new Font("宋体", 13),
            //        Brushes.Yellow,
            //        new Rectangle(255 + (i / 3) * 284, 423 + (i % 3) * 28, 126, 28),
            //        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
            //        );
            //}

            base.paint(dcGs);
        }
    }
}
