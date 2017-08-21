using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;

namespace YunDa.JC.MMI.Common
{

    [Serializable]
    public class ColumnConfigInfo
    {
        [Browsable(true)]
        public Boolean IsShowValue { get; set; }

        [Browsable(true)]
        public String Name { get; set; }

        [Browsable(true)]
        public Boolean IsControlByValue { get; set; }

        [Browsable(true)]
        public bool IsShowCondition { get; set; }

        [Browsable(true)]
        public String Format { get; set; }

        [Browsable(true)]
        public Rectangle Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }
        private Rectangle _rect = new Rectangle(0, 0, 0, 0);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public List<ColumnChartLogicInfo> ColumnChartLogicInfos
        {
            get { return _columnChartLogicInfos; }
            set
            {
                _columnChartLogicInfos = value;
            }
        }
        private List<ColumnChartLogicInfo> _columnChartLogicInfos = new List<ColumnChartLogicInfo>();

        private List<ColumnChartLogicInfo> _currentLogicInfos = new List<ColumnChartLogicInfo>();

        public bool SetValue(Int32 logicID, float value)
        {
            bool isFind = false;
            _currentLogicInfos.ForEach(a =>
            {
                if (a.FloatLogicID == logicID)
                {
                    a.FloatValue = value;
                    isFind = true;
                }
            });

            return isFind;
        }

        public bool SetValue(Int32 logicID, Boolean value)
        {
            bool isFind = false;
            _currentLogicInfos.ForEach(a =>
            {
                if (a.BoolLogicID == logicID)
                {
                    a.BoolValue = value;
                    isFind = true;
                }
            });

            return isFind;
        }

        private bool _isFirstSet = true;

        public void SetLogicID(params Int32[] logicID)
        {
            //if (_columnChartLogicInfos.Count != logicID.Length) return;
            for (int i = 0; i < logicID.Length; i++)
            {
                _columnChartLogicInfos[i].FloatLogicID = logicID[i];
                ColumnChartLogicInfo ccli = _currentLogicInfos.Find(b => b.ID == _columnChartLogicInfos[i].ID);
                if (ccli == null)
                {
                    ccli = new ColumnChartLogicInfo();
                    _currentLogicInfos.Add(ccli);
                }
                ccli.ID = _columnChartLogicInfos[i].ID;
                ccli.FloatLogicID = _columnChartLogicInfos[i].FloatLogicID;
                ccli.BoolLogicID = _columnChartLogicInfos[i].BoolLogicID;
                ccli.MaxValue = _columnChartLogicInfos[i].MaxValue;
                ccli.MinValue = _columnChartLogicInfos[i].MinValue;
                ccli.FillColor = _columnChartLogicInfos[i].FillColor;
                ccli.Pointer = _columnChartLogicInfos[i].Pointer;
                if (IsControlByValue) ccli.BoolValue = _columnChartLogicInfos[i].BoolValue;
            }
        }

        public void SetBoolLogicID(params Int32[] logicID)
        {
            for (int i = 0; i < logicID.Length; i++)
            {
                _columnChartLogicInfos[i].BoolLogicID = logicID[i];
                ColumnChartLogicInfo ccli = _currentLogicInfos.Find(b => b.ID == _columnChartLogicInfos[i].ID);
                if (ccli == null)
                {
                    ccli = new ColumnChartLogicInfo();
                    _currentLogicInfos.Add(ccli);
                }
                ccli.ID = _columnChartLogicInfos[i].ID;
                ccli.FloatLogicID = _columnChartLogicInfos[i].FloatLogicID;
                ccli.BoolLogicID = _columnChartLogicInfos[i].BoolLogicID;
                ccli.MaxValue = _columnChartLogicInfos[i].MaxValue;
                ccli.MinValue = _columnChartLogicInfos[i].MinValue;
                ccli.FillColor = _columnChartLogicInfos[i].FillColor;
                ccli.Pointer = _columnChartLogicInfos[i].Pointer;
                if (IsControlByValue) ccli.BoolValue = _columnChartLogicInfos[i].BoolValue;
            }
        }

        public void Paint(Graphics g, float singleValue)
        {
            foreach (var a in _columnChartLogicInfos)
            {
                ColumnChartLogicInfo ccli = _currentLogicInfos.Find(b => b.ID == a.ID);
                if (ccli == null)
                {
                    ccli = new ColumnChartLogicInfo();
                    _currentLogicInfos.Add(ccli);
                }
                ccli.ID = a.ID;
                ccli.FloatLogicID = a.FloatLogicID;
                ccli.BoolLogicID = a.BoolLogicID;
                ccli.MaxValue = a.MaxValue;
                ccli.MinValue = a.MinValue;
                ccli.FillColor = a.FillColor;
                ccli.Pointer = a.Pointer;
                if (IsControlByValue) ccli.BoolValue = a.BoolValue;
            }

            //绘制矩形框
            g.DrawRectangle(Pens.White, _rect);


            //绘制柱形
            float value = 0, valuePointer = 0, maxValue = 0;
            Color fillColor = Color.Transparent, maxFillColor = Color.Transparent;
            Image pointer = null;
            bool isNotZero = false;
            bool isFind = false;
            Int32 logicID = 0;
            if (_currentLogicInfos != null || _currentLogicInfos.Count > 0)
            {
                foreach (var a in _currentLogicInfos)
                {
                    if (a.BoolValue && logicID == 0)
                    {
                        if (a.FloatValue != 0) isNotZero = true;
                        if (a.Pointer == null)//绘制矩形填充
                        {
                            if (IsControlByValue)
                            {
                                if (maxValue < a.FloatValue)
                                {
                                    maxValue = a.FloatValue;
                                    maxFillColor = a.FillColor;
                                }
                                if (a.MaxValue < a.FloatValue || a.MinValue >= a.FloatValue)
                                    continue;
                            }
                            fillColor = a.FillColor;
                            value = a.FloatValue;
                        }
                        else//绘制图标
                        {
                            if (pointer == null)
                            {
                                pointer = a.Pointer;
                                valuePointer = a.FloatValue;
                            }
                        }
                    }
                    else if (a.FloatValue != 0)
                    {
                        isNotZero = true;
                        if (a.Pointer == null)//绘制矩形填充
                        {
                            if (IsControlByValue)
                            {
                                if (maxValue < a.FloatValue)
                                {
                                    maxValue = a.FloatValue;
                                    maxFillColor = a.FillColor;
                                }
                                if (a.MaxValue < a.FloatValue || a.MinValue >= a.FloatValue)
                                    continue;
                            }
                            fillColor = a.FillColor;
                            value = a.FloatValue;
                        }
                        else//绘制图标
                        {
                            if (pointer == null)
                            {
                                pointer = a.Pointer;
                                valuePointer = a.FloatValue;
                            }
                        }
                        logicID = a.BoolLogicID;
                        isFind = true;
                    }
                }

                if (logicID != 0)
                {
                    var temp = _currentLogicInfos.Find(a => a.BoolLogicID == logicID && a.Pointer != null);
                    if (temp != null)
                    {
                        if (pointer == null)
                        {
                            pointer = temp.Pointer;
                            valuePointer = temp.FloatValue;
                        }
                    }
                }
            }

            float ypos = (_rect.Y + _rect.Height) - singleValue * value;
            float height = singleValue * value;
            float yposPointer = (_rect.Y + _rect.Height) - singleValue * valuePointer;
            if (isNotZero)
            {
                if (value == 0)
                {
                    if (singleValue * maxValue >= _rect.Height)
                    {
                        ypos = _rect.Y;
                        height = _rect.Height;
                        fillColor = maxFillColor;
                    }
                    else
                    {
                        ypos = (_rect.Y + _rect.Height) - singleValue * maxValue;
                        height = singleValue * maxValue;
                        fillColor = maxFillColor;
                    }
                    value = maxValue;
                }
                else
                {
                    if (singleValue * value >= _rect.Height)
                    {
                        ypos = _rect.Y;
                        height = _rect.Height;
                    }
                }

                if (singleValue * valuePointer >= _rect.Height)
                {
                    yposPointer = _rect.Y;
                }
            }
            else
            {

            }

            if (_fillBrush == null) _fillBrush = new SolidBrush(Color.White);
            _fillBrush.Color = fillColor;
            g.FillRectangle(
                _fillBrush,
                new RectangleF(
                    _rect.X + 1,
                    ypos,
                    _rect.Width - 1,
                    height
                    )
                );
            if (pointer != null)
                g.DrawImage(
                    pointer,
                    new RectangleF(
                        _rect.X,
                        yposPointer - pointer.Size.Height / 2,
                        _rect.Width,
                        pointer.Size.Height
                        )
                    );

            //绘制条件（区域线）
            if (_columnChartLogicInfos == null || _columnChartLogicInfos.Count == 0)
                return;
            if (IsControlByValue && IsShowCondition)
            {
                _columnChartLogicInfos.ForEach(a =>
                {
                    if (a.MaxValue > 0)
                    {
                        if (_fillPen == null) _fillPen = new Pen(Color.White);
                        _fillPen.Color = a.FillColor;
                        g.DrawLine(
                            _fillPen,
                            new PointF(_rect.X, (_rect.Y + _rect.Height) - singleValue * a.MaxValue),
                            new PointF(_rect.X + _rect.Width, (_rect.Y + _rect.Height) - singleValue * a.MaxValue)
                            );
                    }
                });
            }

            //绘制当前值
            if (IsShowValue)
            {
                String format = (Format == null || Format == "") ? "0" : Format;

                if (_font == null)
                {
                    _font = new Font("Verdana", 11);
                }

                if (_sf==null)
                    _sf = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                g.DrawString(
                    value.ToString(format),
                    _font,
                    Brushes.White,
                    new RectangleF(_rect.X, _rect.Y - 25, _rect.Width, 25),
                    _sf
                    );
            }

            if (Name != null)
            {
                if (_font == null)
                {
                    _font = new Font("Verdana", 11);
                }

                if (_sf == null)
                    _sf = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                g.DrawString(
                     Name,
                     _font,
                     Brushes.White,
                     new RectangleF(_rect.X, _rect.Y + _rect.Height + 5, _rect.Width, 25),
                     _sf
                     );
            }
        }

        [NonSerialized]
        private SolidBrush _fillBrush = new SolidBrush(Color.White);
        [NonSerialized]
        private Pen _fillPen = new Pen(new SolidBrush(Color.White));

        [NonSerialized]
        private StringFormat _sf = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        [NonSerialized]
        private Font _font = new Font("Verdana", 11);

        public ColumnConfigInfo()
        {
        }
    }

    [Serializable]
    public class ColumnChartLogicInfo
    {
        [Browsable(true)]
        public Int32 ID { get; set; }

        [Browsable(true)]
        public Int32 FloatLogicID { get; set; }

        [Browsable(true)]
        public float MaxValue { get; set; }

        [Browsable(true)]
        public float MinValue { get; set; }

        [Browsable(true)]
        public Color FillColor { get; set; }

        [Browsable(true)]
        public Image Pointer { get; set; }

        [Browsable(true)]
        public Int32 BoolLogicID { get; set; }

        [Browsable(false)]
        public float FloatValue { get; set; }

        [Browsable(false)]
        public Boolean BoolValue { get; set; }

        public ColumnChartLogicInfo()
        {
        }
    }
}
