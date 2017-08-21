using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using HD_HXD2_TMS.Common;
using MMI.Facility.Interface.Data;

namespace HD_HXD2_TMS.VC公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_TrainChang : baseClass
    {
        private List<Image> _images = new List<Image>();
        private List<Button> _btns = new List<Button>();
        private List<Button> _btnsClone = new List<Button>();
        private Int32 _currentViewID = 0;
        private float _yOffset = 0;
        private List<float> _yOffsets = new List<float>();

        public override string GetInfo()
        {
            return "公共试图-小车";
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

            _yOffsets = new List<float>()
            {
                347,
                405
            };
            _yOffset = _yOffsets[0];

            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(5 + 75 * i, 117 + _yOffset, 70, 30),
                    i,
                    new ButtonStyle()
                    {
                        Background = _images[0 + i * 2],
                        DownImage = _images[1 + i * 2],
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
                btn.MouseDownEvent += btn_MouseDownEvent;
                _btnsClone.Add(btn);
            }

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _currentViewID = nParaB;
                _yOffset = _yOffsets[0];
                if((ViewType)nParaB== ViewType.DataDriveA||(ViewType)nParaB== ViewType.DataDriveB
                    || (ViewType)nParaB == ViewType.DataAuxiliaryA || (ViewType)nParaB == ViewType.DataAuxiliaryB
                    || (ViewType)nParaB == ViewType.FalutHistroyA || (ViewType)nParaB == ViewType.FalutHistroyB
                    || (ViewType)nParaB == ViewType.FalutConveterB || (ViewType)nParaB == ViewType.FalutConveterA)
                    _yOffset = _yOffsets[1];

                _btnsClone.ForEach(a => a.Rect = new RectangleF(a.Rect.X, 117 + _yOffset, a.Rect.Width, a.Rect.Height));
            }
        }

        public override bool mouseDown(Point point)
        {
            if (HD_VC_FalutManager.IsShowCurrentView ||
                HD_VC_FalutManager.IsShowCurrentAView ||
                HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns.ForEach(a => a.MouseDown(point));

            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            if (HD_VC_FalutManager.IsShowCurrentView ||
                HD_VC_FalutManager.IsShowCurrentAView ||
                HD_VC_FalutManager.IsShowCurrentBView)
                return base.mouseDown(point);

            _btns.ForEach(a => a.MouseUp(point));

            return base.mouseUp(point);
        }

        void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if ((e.Message+1) % 2 == _currentViewID % 2)
            {
                return;
            }

            Int32 viewID = _currentViewID + 1;
            if (_currentViewID % 2 == 0) viewID = _currentViewID - 1;
            
            append_postCmd(CmdType.ChangePage, viewID, 0, 0);
        }

        public override void paint(Graphics dcGs)
        {
            _btns.Clear();
            if (BoolList[UIObj.InBoolList[0]]) //在A端
            {
                if (!_btns.Contains(_btnsClone[0]))
                {
                    _btnsClone[0].ID = 0;
                    _btnsClone[1].ID = 1;
                    _btns.Add(_btnsClone[0]);
                    if ((_currentViewID + 1) % 2 == 0)
                    {
                        _btnsClone[0].IsReplication = false;
                        _btnsClone[1].IsReplication = true;
                        HD_HXD2_TMS.VC公共组件.HD_VC_Title.TrainName = "A";
                    }
                }
                dcGs.DrawString(
                    "HXD2" + FloatList[UIObj.InFloatList[0]] + "A",
                    new Font("宋体", 9),
                    Brushes.White,
                    new RectangleF(5, 117 + _yOffset - 20, 70, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
                if (!BoolList[UIObj.InBoolList[1]])
                {
                    if (!_btns.Contains(_btnsClone[1]))
                    {
                        _btns.Add(_btnsClone[1]);
                        if ((_currentViewID + 1) % 2 == 1)
                        {
                            _btns[0].IsReplication = true;
                            _btns[1].IsReplication = false;
                            HD_HXD2_TMS.VC公共组件.HD_VC_Title.TrainName = "B";
                        }
                    }
                    dcGs.DrawString(
                        "HXD2" + FloatList[UIObj.InFloatList[1]] + "B",
                        new Font("宋体", 9),
                        Brushes.White,
                        new RectangleF(5 + 70 + 5, 117 + _yOffset - 20, 70, 20),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                }
                else
                {
                    if (_btns.Contains(_btnsClone[1]))
                    {
                        _btnsClone[1].IsReplication = true;
                        _btns.Remove(_btnsClone[1]);
                    }
                }
            }
            else if (BoolList[UIObj.InBoolList[0] + 1]) //在B端
            {
                if (!_btns.Contains(_btnsClone[0]))
                {
                    _btnsClone[0].ID = 1;
                    _btnsClone[1].ID = 0;
                    _btns.Add(_btnsClone[0]);
                    if ((_currentViewID + 1) % 2 == 1)
                    {
                        _btnsClone[0].IsReplication = false;
                        _btnsClone[1].IsReplication = true;
                        HD_HXD2_TMS.VC公共组件.HD_VC_Title.TrainName = "B";
                    }
                }
                dcGs.DrawString(
                    "HXD2" + FloatList[UIObj.InFloatList[1]] + "B",
                    new Font("宋体", 9),
                    Brushes.White,
                    new RectangleF(5, 117 + _yOffset - 20, 70, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
                if (!BoolList[UIObj.InBoolList[1] + 1])
                {
                    if (!_btns.Contains(_btnsClone[1]))
                    {
                        _btns.Add(_btnsClone[1]);
                        if ((_currentViewID + 1) % 2 == 0)
                        {
                            _btns[0].IsReplication = true;
                            _btns[1].IsReplication = false;
                            HD_HXD2_TMS.VC公共组件.HD_VC_Title.TrainName = "A";
                        }
                    }
                    dcGs.DrawString(
                        "HXD2" + FloatList[UIObj.InFloatList[0]] + "A",
                        new Font("宋体", 9),
                        Brushes.White,
                        new RectangleF(5 + 70 + 5, 117 + _yOffset - 20, 70, 20),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                }
                else
                {
                    if (_btns.Contains(_btnsClone[1]))
                    {
                        _btnsClone[1].IsReplication = true;
                        _btns.Remove(_btnsClone[1]);
                    }
                }
            }

            if(_btns!=null&&_btns.Count!=0)
                _btns.ForEach(a => a.Paint(dcGs));
            
            base.paint(dcGs);
        }
    }
}
