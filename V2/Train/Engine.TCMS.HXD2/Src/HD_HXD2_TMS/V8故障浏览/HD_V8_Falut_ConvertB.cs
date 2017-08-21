using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using HD_HXD2_TMS.Common;
using HD_HXD2_TMS.VC公共组件;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V8故障浏览
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V8_Falut_ConvertB : baseClass
    {
        private List<Button> _btns = new List<Button>();
        private List<Button> _btnsPage = new List<Button>();
        private List<Image> _images = new List<Image>();    //图片资源
        private String[] _strs = null;
        private List<Rectangle> _rect = null;
        private Int32 _currentPage = 0;
        private List<FalutInfo> _currentFalut = null;
        private List<Button> _btnsTrain = new List<Button>();

        public override string GetInfo()
        {
            return "故障浏览界面-主断状态B";
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
                "序\n号", "故障\n代码", "故障\n开始时间", "故障\n内容"
            };

            _rect = new List<Rectangle>()
            {
                new Rectangle(11,144,31,43),
                new Rectangle(41,144,67,43),
                new Rectangle(107,144,120,43),
                new Rectangle(226,144,560,43)
            };

            String[] strs1 = { "故障查询", "主断查询" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    strs1[i],
                    new RectangleF(226+97*i, 511, 90, 34),
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
                    },
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            ((ButtonStyle)_btns[1].Style).FontStyle.TextBrush = (SolidBrush)Brushes.Black;
            _btns[1].IsReplication = false;

            strs1= new String[3]{"刷新","上一页","下一页"};
            for (int i = 0; i < 3; i++)
            {
                Button btnPage = new Button(
                    strs1[i],
                    new RectangleF(583 + 70 * i, 511, 65, 32),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[2],
                        DownImage = _images[3],
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
                Button btnTrain = new Button(
                    "",
                    new RectangleF(5 + 75 * i, 117 + 405, 70, 30),
                    301 + i,
                    new ButtonStyle()
                    {
                        Background = _images[4 + i * 2],
                        DownImage = _images[5 + i * 2],
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
                btnTrain.ClickEvent += btnTrain_ClickEvent;
                _btnsTrain.Add(btnTrain);
            }
            _btnsTrain[1].IsReplication = false;

            return true;
        }

        void btnTrain_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _btnsTrain[1].IsReplication = false;
            _btnsTrain[0].IsReplication = true;
            append_postCmd(CmdType.ChangePage, (Int32)ViewType.FalutConveterA, 0, 0);
        }

        void btnPage_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch(e.Message)
            {
                case 0://刷新
                    break;
                case 1://上一页
                    if (_currentPage > 0) _currentPage--;
                    break;
                case 2://下一页
                    if (_currentPage < HD_VC_ConverterManager.FalutsBHistroy.Count / 16) _currentPage++;
                    break;
            }
        }

        public override bool mouseDown(System.Drawing.Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns[0].MouseDown(point);
            _btnsPage.ForEach(a => a.MouseDown(point));
            _btnsTrain[0].MouseDown(point);

            return base.mouseDown(point);
        }

        public override bool mouseUp(System.Drawing.Point point)
        {
            if (HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentAView ||
                HD_HXD2_TMS.VC公共组件.HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns[0].MouseUp(point);
            _btnsPage.ForEach(a => a.MouseUp(point));
            _btnsTrain[0].MouseUp(point);

            return base.mouseUp(point);
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _btns[0].IsReplication = true;
            append_postCmd(CmdType.ChangePage, (Int32)ViewType.FalutHistroyB, 0, 0);//跳转到主断查询界面
        }

        public override void paint(Graphics dcGs)
        {
            paint_Frame(dcGs);
            paint_Falut(dcGs);

            _btns.ForEach(a => a.Paint(dcGs));
            _btnsPage.ForEach(a => a.Paint(dcGs));
            _btnsTrain.ForEach(a => a.Paint(dcGs));
            HD_HXD2_TMS.VC公共组件.HD_VC_Title.TrainName = "B";
            //_btns.Clear();
            if (BoolList[UIObj.InBoolList[0]]) //在A端
            {
                //if (!_btns.Contains(_btnsClone[0]))
                //{
                _btnsTrain[0].ID = 0;
                _btnsTrain[1].ID = 1;
                //_btns.Add(_btnsClone[0]);
                //_btnsClone[0].IsReplication = false;
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
                //        _btnsClone[1].IsReplication = true;
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
                _btnsTrain[0].ID = 1;
                _btnsTrain[1].ID = 0;
                //_btns.Add(_btnsClone[0]);
                //_btnsClone[0].IsReplication = true;
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
                //        _btns[0].IsReplication = true;
                //        _btns[1].IsReplication = false;
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
                //        _btnsClone[0].IsReplication = true;
                //        _btns.Remove(_btnsClone[1]);
                //    }
                //}
            }

            base.paint(dcGs);
        }

        private void paint_Frame(Graphics dcGs)
        {
            //外框
            dcGs.DrawRectangle(Pens.White, new Rectangle(11,144,775,347));

            for (int i = 0; i < 16; i++)
            {
                dcGs.DrawLine(Pens.White, new Point(11,144+43+19*i), new Point(786,144+43+19*i));
                dcGs.DrawString(
                    (i+1).ToString(),
                    new Font("宋体", 9),
                    Brushes.Yellow,
                    new Rectangle(_rect[0].X, _rect[0].Y+19*i+43, _rect[0].Width, 19),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            dcGs.DrawRectangle(Pens.White, new Rectangle(41, 144, 67, 347));
            dcGs.DrawRectangle(Pens.White, new Rectangle(226, 144, 560, 347));

            for (int i = 0; i < 4; i++)
            {
                dcGs.DrawString(
                    _strs[i],
                    new Font("宋体", 9),
                    Brushes.White,
                    new Rectangle(_rect[i].X, _rect[i].Y+2, _rect[i].Width, _rect[i].Height),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            String[] strs={ "共         页","第         页"};
            for (int i = 0; i < 2; i++)
            { 
                dcGs.DrawString(
                    strs[i],
                    new Font("宋体", 7),
                    Brushes.White,
                    new Rectangle(431+72*i, 512, 66, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Center }
                    );
                dcGs.DrawRectangle(Pens.White, new Rectangle(443+71*i, 514, 41, 17));
            }
        }

        private void paint_Falut(Graphics dcGs)
        {
            if (HD_VC_ConverterManager.FalutsBHistroy == null || HD_VC_ConverterManager.FalutsBHistroy.Count == 0)
                return;

            //获取到当前页故障
            List<FalutInfo> histroyFaluts = HD_VC_ConverterManager.FalutsBHistroy;
            _currentFalut = new List<FalutInfo>();
            if (_currentPage > HD_VC_ConverterManager.FalutsAHistroy.Count / 16) _currentPage--;
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
                Int32 value = i == 0 ? (HD_VC_ConverterManager.FalutsBHistroy.Count / 16 + 1) : (_currentPage + 1);
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
                   _currentFalut[i].Code,
                   new Font("宋体", 9),
                   Brushes.Yellow,
                   new RectangleF(_rect[1].X, _rect[1].Y + 43 + 19 * i, _rect[1].Width, 19),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
                //时间
                dcGs.DrawString(
                   _currentFalut[i].StartTime,
                   new Font("宋体", 9),
                   Brushes.Yellow,
                   new RectangleF(_rect[2].X, _rect[2].Y + 43 + 19 * i, _rect[2].Width, 19),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
                //故障内容
                dcGs.DrawString(
                   _currentFalut[i].Name,
                   new Font("宋体", 9),
                   Brushes.Yellow,
                   new RectangleF(_rect[3].X, _rect[3].Y + 43 + 19 * i+2, _rect[3].Width, 19),
                   new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                   );
            }
        }
    }
}
