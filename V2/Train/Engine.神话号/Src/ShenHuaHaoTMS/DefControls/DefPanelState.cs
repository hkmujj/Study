using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace ShenHuaHaoTMS.DefControls
{
    public partial class DefPanelState : TableLayoutPanel, ILogic
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("接收到的数据配置信息集合"), Category("数据配置属性")]
        [Browsable(true)]
        public List<DataConfigInfo> DataConfigInfoCollection
        {
            get { return _dataConfigInfoCollection; }
            set
            {
                _dataConfigInfoCollection = value;
                if (value != null && value.Count != 0)
                {
                    CurrentDataConfigInfo = value[0];
                }
            }
        }
        private List<DataConfigInfo> _dataConfigInfoCollection = new List<DataConfigInfo>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("发送布尔数据配置信息集合"), Category("数据配置属性")]
        [Browsable(true)]
        public List<Int32> OutBoolConfigInfoCollection
        {
            get { return _outBoolConfigInfoCollection; }
            set { _outBoolConfigInfoCollection = value; }
        }
        private List<Int32> _outBoolConfigInfoCollection = new List<int>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("发送浮点数据配置信息集合"), Category("数据配置属性")]
        [Browsable(true)]
        public List<Int32> OutFloatConfigInfoCollection
        {
            get { return _outFloatConfigInfoCollection; }
            set { _outFloatConfigInfoCollection = value; }
        }
        private List<Int32> _outFloatConfigInfoCollection = new List<int>();

        [Browsable(false)]
        public DataConfigInfo CurrentDataConfigInfo
        {
            set
            {
                _currentDataConfigInfo = value;

                Invalidate();
            }
        }
        private DataConfigInfo _currentDataConfigInfo = null;


        public void SetState(Int32 logicID, Boolean state)
        {
            var clone = _dataConfigInfoCollection.FindAll(a => a.BoolLogic == logicID);
            switch (clone.Count)
            {
                case 1:
                    if (state)
                    {
                        for (var i = 0; i < _dataConfigInfoCollection.Count; i++)
                        {
                            if (_dataConfigInfoCollection[i].BoolLogic != logicID)
                            {
                                continue;
                            }

                            _dataConfigInfoCollection[i].BoolValue = true;
                            if (_currentDataConfigInfo != null)
                            {
                                if (_currentDataConfigInfo.Grade > _dataConfigInfoCollection[i].Grade)
                                {
                                    continue;
                                }

                                _currentDataConfigInfo = new DataConfigInfo()
                                {
                                    BoolLogic = _dataConfigInfoCollection[i].BoolLogic,
                                    DefBackground = _dataConfigInfoCollection[i].DefBackground,
                                    BoolValue = _dataConfigInfoCollection[i].BoolValue,
                                    DefFontInfo = _dataConfigInfoCollection[i].DefFontInfo,
                                    DefText = _dataConfigInfoCollection[i].DefText,
                                    FloatLogic = _dataConfigInfoCollection[i].FloatLogic,
                                    Format = _dataConfigInfoCollection[i].Format,
                                    BackColor = _dataConfigInfoCollection[i].BackColor,
                                    Grade = _dataConfigInfoCollection[i].Grade
                                };
                                Invalidate();
                            }
                            else
                            {
                                _currentDataConfigInfo = new DataConfigInfo()
                                {
                                    BoolLogic = _dataConfigInfoCollection[i].BoolLogic,
                                    DefBackground = _dataConfigInfoCollection[i].DefBackground,
                                    BoolValue = _dataConfigInfoCollection[i].BoolValue,
                                    DefFontInfo = _dataConfigInfoCollection[i].DefFontInfo,
                                    DefText = _dataConfigInfoCollection[i].DefText,
                                    FloatLogic = _dataConfigInfoCollection[i].FloatLogic,
                                    Format = _dataConfigInfoCollection[i].Format,
                                    BackColor = _dataConfigInfoCollection[i].BackColor,
                                    Grade = _dataConfigInfoCollection[i].Grade
                                };
                                Invalidate();
                            }
                        }
                    }
                    else
                    {
                        if (_currentDataConfigInfo == null)
                        {
                            return;
                        }

                        if (_currentDataConfigInfo.BoolLogic == logicID)
                        {
                            _dataConfigInfoCollection.Find(a => a.BoolLogic == logicID).BoolValue = state;
                            var dc = _dataConfigInfoCollection.Find(a => a.BoolValue);
                            if (dc == null)
                            {
                                _currentDataConfigInfo = null;
                            }
                            else
                            {
                                _currentDataConfigInfo = new DataConfigInfo()
                                {
                                    BoolLogic = dc.BoolLogic,
                                    DefBackground = dc.DefBackground,
                                    BoolValue = dc.BoolValue,
                                    DefFontInfo = dc.DefFontInfo,
                                    DefText = dc.DefText,
                                    FloatLogic = dc.FloatLogic,
                                    Format = dc.Format,
                                    Grade = dc.Grade
                                };
                            }
                            Invalidate();
                        }
                        else
                        {
                            var dci = _dataConfigInfoCollection.Find(a => a.BoolLogic == logicID);
                            if (dci != null)
                            {
                                dci.BoolValue = false;
                            }
                        }
                    }
                    break;
                case 2:
                    if (state)
                    {
                        _currentDataConfigInfo = new DataConfigInfo()
                        {
                            BoolLogic = clone[0].BoolLogic,
                            DefBackground = clone[0].DefBackground,
                            BoolValue = clone[0].BoolValue,
                            DefFontInfo = clone[0].DefFontInfo,
                            DefText = clone[0].DefText,
                            FloatLogic = clone[0].FloatLogic,
                            Format = clone[0].Format,
                            Grade = clone[0].Grade
                        };
                        Invalidate();
                    }
                    else
                    {
                        _currentDataConfigInfo = new DataConfigInfo()
                        {
                            BoolLogic = clone[1].BoolLogic,
                            DefBackground = clone[1].DefBackground,
                            BoolValue = clone[1].BoolValue,
                            DefFontInfo = clone[1].DefFontInfo,
                            DefText = clone[1].DefText,
                            FloatLogic = clone[1].FloatLogic,
                            Format = clone[1].Format,
                            Grade = clone[0].Grade
                        };
                        Invalidate();
                    }
                    break;
            }
        }

        public void SetValue(Int32 logicID, float value)
        {
        }

        public DefPanelState()
        {
            InitializeComponent();
        }

        public StringFormat SF
        {
            get { return _sf; }
            set
            {
                if (_sf.Alignment == value.Alignment && _sf.LineAlignment == value.LineAlignment)
                {
                    value.Dispose();
                    return;
                }
                _sf = value;
            }
        }
        private StringFormat _sf = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public SolidBrush FontBrush
        {
            get { return _fontBrush; }
            set
            {
                if (_fontBrush.Color == value.Color)
                {
                    value.Dispose();
                    return;
                }

                _fontBrush = value;
            }
        }
        private SolidBrush _fontBrush = new SolidBrush(Color.White);

        public SolidBrush BackBrush
        {
            get { return _backBrush; }
            set
            {
                if (_backBrush.Color == value.Color)
                {
                    value.Dispose();
                    return;
                }

                _backBrush = value;
            }
        }
        private SolidBrush _backBrush = new SolidBrush(Color.White);

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (_currentDataConfigInfo == null)
            {
                return;
            }

            if (_currentDataConfigInfo.DefBackground != null)
            {
                //绘制按钮背景
                pe.Graphics.DrawImage(
                    _currentDataConfigInfo.DefBackground,
                    new RectangleF(
                        0,
                        0,
                        _currentDataConfigInfo.DefBackground.Size.Width,
                        _currentDataConfigInfo.DefBackground.Size.Height)
                    );
            }

            if (!string.IsNullOrEmpty(_currentDataConfigInfo.DefText))
            {
                BackBrush.Color = _currentDataConfigInfo.BackColor;
                FontBrush.Color = _currentDataConfigInfo.DefFontInfo.FontColor;
                //BackBrush = new SolidBrush(_currentDataConfigInfo.BackColor);
                //FontBrush = new SolidBrush(_currentDataConfigInfo.DefFontInfo.FontColor);
                SF.Alignment =
                    (StringAlignment)
                        Enum.Parse(typeof (StringAlignment), _currentDataConfigInfo.DefFontInfo.Vertical.ToString());
                SF.LineAlignment =
                    (StringAlignment)
                        Enum.Parse(typeof (StringAlignment), _currentDataConfigInfo.DefFontInfo.Horizontal.ToString());
                //SF = new StringFormat()
                //{
                //    Alignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), _currentDataConfigInfo.DefFontInfo.Vertical.ToString()),
                //    LineAlignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), _currentDataConfigInfo.DefFontInfo.Horizontal.ToString())
                //};
                pe.Graphics.FillRectangle(
                        BackBrush,
                        new RectangleF(
                            0,
                            0,
                            Size.Width,
                            Size.Height)
                        );

                pe.Graphics.DrawString(
                    _currentDataConfigInfo.DefText,
                    _currentDataConfigInfo.DefFontInfo.DefFont,
                    FontBrush,
                    new RectangleF(
                        0,
                        0 + 2,
                        Size.Width,
                        Size.Height),
                    SF
                    );

            }
        }
    }
}
