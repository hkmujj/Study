using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using HD_HXD2_TMS.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.VC公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_CurrentFalutB : baseClass
    {
        private List<Button> _btnsPage = new List<Button>();
        private List<Image> _images = new List<Image>();    //图片资源
        private String[] _strs = null;
        private List<Rectangle> _rect = null;
        private Int32 _currentPage = 0;
        private List<FalutInfo> _currentFalut = null;
        private List<Button> _btns = new List<Button>();
        private List<Button> _btnsClone = new List<Button>();

        public override string GetInfo()
        {
            return "当前故障界面-B车";
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

            _strs = new String[]
            {
                "故障\n代码", "发生\n时间", "事件\n内容", "故障\n级别", "是否\n确认"
            };

            _rect = new List<Rectangle>()
            {
                new Rectangle(55,144,91,43),
                new Rectangle(146,144,134,43),
                new Rectangle(280,144,438,43),
                new Rectangle(584,144,69,43),
                new Rectangle(653,144,88,43)
            };

            String[] strs1 = new String[3] { "刷新", "前一页", "下一页" };
            for (int i = 0; i < 3; i++)
            {
                Button btnPage = new Button(
                    strs1[i],
                    new RectangleF(583 + 70 * i, 511, 65, 32),
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
                btnPage.ClickEvent += btnPage_ClickEvent;
                _btnsPage.Add(btnPage);
            }
            
            //左下小车按钮
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(5 + 75 * i, 117 + 405, 70, 30),
                    301 + i,
                    new ButtonStyle()
                    {
                        Background = _images[2 + i * 2],
                        DownImage = _images[3 + i * 2],
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
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            _btns[1].IsReplication = false;

            return true;
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (HD_VC_FalutManager.IsShowCurrentBView)
            {
                _btns[0].IsReplication = true;
                _btns[1].IsReplication = false;
                HD_VC_FalutManager.IsShowCurrentAView = true;
                HD_VC_FalutManager.IsShowCurrentBView = false;
            }
        }

        void btnPage_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://刷新
                    break;
                case 1://上一页
                    if (_currentPage > 0) _currentPage--;
                    break;
                case 2://下一页
                    if (_currentPage < HD_VC_FalutManager.FalutsBCurrent.Count / 16) _currentPage++;
                    break;
            }
        }

        public override bool mouseDown(Point point)
        {
            if (HD_VC_FalutManager.IsShowCurrentBView)
            {
                _btnsPage.ForEach(a => a.MouseDown(point));
                _btns[0].MouseDown(point);
            }

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (HD_VC_FalutManager.IsShowCurrentBView)
            {
                _btnsPage.ForEach(a => a.MouseUp(point));
                _btns[0].MouseUp(point);
            }

            return base.mouseUp(point);
        }

        public override void paint(Graphics dcGs)
        {
            if (!HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return;

            paint_Frame(dcGs);
            _btnsPage.ForEach(a => a.Paint(dcGs));

            HD_HXD2_TMS.VC公共组件.HD_VC_Title.TrainName = "B";
            //_btns.Clear();
            if (BoolList[UIObj.InBoolList[0]]) //在A端
            {
                //if (!_btns.Contains(_btnsClone[0]))
                //{
                _btns[0].ID = 0;
                _btns[1].ID = 1;
                   // _btns.Add(_btnsClone[0]);
                    //_btnsClone[0].IsReplication = true;
                //}
                dcGs.DrawString(
                    "HXD2" + FloatList[UIObj.InFloatList[0]] + "A",
                    new Font("宋体", 9),
                    Brushes.White,
                    new RectangleF(5, 117 + 405 - 20, 70, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
                //if (!BoolList[UIObj.InBoolList[1]])
                //{
                //    if (!_btns.Contains(_btnsClone[1]))
                //    {
                //        _btns.Add(_btnsClone[1]);
                //        _btnsClone[1].IsReplication = false;
                //    }
                    dcGs.DrawString(
                        "HXD2" + FloatList[UIObj.InFloatList[1]] + "B",
                        new Font("宋体", 9),
                        Brushes.White,
                        new RectangleF(5 + 70 + 5, 117 + 405 - 20, 70, 20),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                //}
                //else
                //{
                //    if (_btns.Contains(_btnsClone[1]))
                //    {
                //        _btnsClone[1].IsReplication = true;
                //        _btns.Remove(_btnsClone[1]);
                //    }
                //}
            }
            else if (BoolList[UIObj.InBoolList[0] + 1]) //在B端
            {
                //if (!_btns.Contains(_btnsClone[0]))
                //{
                _btns[0].ID = 1;
                _btns[1].ID = 0;
                //    _btns.Add(_btnsClone[0]);
                //    _btnsClone[0].IsReplication = false;
                //}
                dcGs.DrawString(
                    "HXD2" + FloatList[UIObj.InFloatList[1]] + "B",
                    new Font("宋体", 9),
                    Brushes.White,
                    new RectangleF(5, 117 + 405 - 20, 70, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
                //if (!BoolList[UIObj.InBoolList[1] + 1])
                //{
                //    if (!_btns.Contains(_btnsClone[1]))
                //    {
                //        _btns.Add(_btnsClone[1]);
                //        _btnsClone[1].IsReplication = true;
                //        _btnsClone[0].IsReplication = false;
                //    }
                    dcGs.DrawString(
                        "HXD2" + FloatList[UIObj.InFloatList[0]] + "A",
                        new Font("宋体", 9),
                        Brushes.White,
                        new RectangleF(5 + 70 + 5, 117 + 405 - 20, 70, 20),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                //}
                //else
                //{
                //    if (_btns.Contains(_btnsClone[1]))
                //    {
                //        _btnsClone[1].IsReplication = true;
                //        _btns.Remove(_btnsClone[1]);
                //    }
                //}
            }

            if (_btns != null && _btns.Count != 0)
                _btns.ForEach(a => a.Paint(dcGs));

            paint_Falut(dcGs);

            base.paint(dcGs);
        }

        private void paint_Frame(Graphics dcGs)
        {
            dcGs.FillRectangle(Brushes.Black, 1, 119, 798, 451);
            //外框
            dcGs.DrawRectangle(Pens.White, new Rectangle(55, 144, 686, 347));

            for (int i = 0; i < 16; i++)
            {
                dcGs.DrawLine(Pens.White, new Point(55, 144 + 43 + 19 * i), new Point(741, 144 + 43 + 19 * i));
            }

            dcGs.DrawRectangle(Pens.White, new Rectangle(146, 144, 134, 347));
            dcGs.DrawRectangle(Pens.White, new Rectangle(584, 144, 69, 347));

            for (int i = 0; i < 5; i++)
            {
                dcGs.DrawString(
                    _strs[i],
                    new Font("宋体", 9),
                    Brushes.White,
                    new Rectangle(_rect[i].X, _rect[i].Y + 2, _rect[i].Width, _rect[i].Height),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            String[] strs = { "共         页", "第         页" };
            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawString(
                    strs[i],
                    new Font("宋体", 7),
                    Brushes.White,
                    new Rectangle(431 + 72 * i, 512, 66, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Center }
                    );
                dcGs.DrawRectangle(Pens.White, new Rectangle(443 + 71 * i, 514, 41, 17));
            }
        }

        private void paint_Falut(Graphics dcGs)
        {
            if (HD_VC_FalutManager.FalutsBCurrent == null || HD_VC_FalutManager.FalutsBCurrent.Count == 0)
                return;

            //获取到当前页故障
            List<FalutInfo> histroyFaluts = HD_VC_FalutManager.FalutsBCurrent;
            _currentFalut = new List<FalutInfo>();
            if (_currentPage > histroyFaluts.Count / 16) _currentPage--;
            if (histroyFaluts.Count - _currentPage * 16 >= 16)
            {
                _currentFalut.AddRange(histroyFaluts.GetRange(_currentPage * 16, 16));
            }
            else
            {
                _currentFalut.AddRange(histroyFaluts.GetRange(_currentPage * 16, histroyFaluts.Count - _currentPage * 16));
            }

            for (int i = 0; i < 2; i++)
            {
                Int32 value = i == 0 ? (histroyFaluts.Count / 16 + 1) : (_currentPage + 1);
                dcGs.DrawString(
                    value.ToString(),
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    new Rectangle(431 + 72 * i, 512, 66, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            for (int i = 0; i < _currentFalut.Count; i++)
            {
                //代码
                dcGs.DrawString(
                   _currentFalut[i].Code.Substring(0, 8),
                   new Font("宋体", 9),
                   Brushes.Yellow,
                   new RectangleF(_rect[0].X, _rect[0].Y + 43 + 19 * i, _rect[0].Width, 19),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
                //时间
                dcGs.DrawString(
                   _currentFalut[i].StartTime,
                   new Font("宋体", 9),
                   Brushes.Yellow,
                   new RectangleF(_rect[1].X, _rect[1].Y + 43 + 19 * i, _rect[1].Width, 19),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
                //故障内容
                dcGs.DrawString(
                   _currentFalut[i].Name,
                   new Font("宋体", 9),
                   Brushes.Yellow,
                   new RectangleF(_rect[2].X, _rect[2].Y + 43 + 19 * i + 2, _rect[2].Width, 19),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
                //级位
                dcGs.DrawString(
                   _currentFalut[i].Level.ToString("0.0"),
                   new Font("宋体", 9),
                   Brushes.Yellow,
                   new RectangleF(_rect[3].X, _rect[3].Y + 43 + 19 * i, _rect[3].Width, 19),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
                //故障级别
                dcGs.DrawString(
                    _currentFalut[i].IsOK?"已确认":"未确认",
                   new Font("宋体", 9),
                   Brushes.Yellow,
                   new RectangleF(_rect[4].X, _rect[4].Y + 43 + 19 * i, _rect[4].Width, 19),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
            }
        }
    }
}
