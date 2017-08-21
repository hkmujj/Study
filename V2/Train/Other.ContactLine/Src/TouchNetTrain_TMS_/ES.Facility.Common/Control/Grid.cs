using System;
using System.Collections.Generic;
using ES.Facility.Common.Control.Common;
using System.Drawing;

namespace ES.Facility.Common.Control
{
    /// <summary>
    /// 功能描述：表-行-项
    /// </summary>
    public class RowItem
    {
        /// <summary>
        /// 读取或设置表-行-项-宽度属性
        /// </summary>
        public float Wideh { get; set; }

        /// <summary>
        /// 读取或设置表-行-项-值属性
        /// </summary>
        public Object Value { get; set; }

        /// <summary>
        /// 读取或设置表-行-项-矩形框属性（用于绘制项状态，不用在外部设置，会在构造的时候自动获得）
        /// </summary>
        public RectangleF Rect { get; set; }
    }

    /// <summary>
    /// 功能描述：表-行
    /// 创建人：唐林
    /// 创建时间：2014-09-16
    /// </summary>
    public class Row
    {
        /// <summary>
        /// 读取或设置表-行-行头属性
        /// </summary>
        public String Header { get; set; }

        /// <summary>
        /// 读取或设置行内容列表属性
        /// </summary>
        public List<RowItem> Values { get; set; }
    }

    /// <summary>
    /// 功能描述：表-列
    /// 创建人：唐林
    /// 创建时间：2014-09-16
    /// </summary>
    public class Column
    {
        /// <summary>
        /// 读取或设置表-列-宽度属性
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// 读取或设置列字体属性
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 读取或设置
        /// </summary>
        public Brush Brush { get; set; }

        /// <summary>
        /// 读取或设置文本对齐方式属性
        /// </summary>
        public StringFormat SF { get; set; }

        /// <summary>
        /// 读取或设置线条笔属性
        /// </summary>
        public Pen LinePen { get; set; }
    }

    public struct PageButton
    {
        public Rectangle Rect { get; set; }

        public Image UpImage { get; set; }

        public Image DownImage { get; set; }

        public Image DisableImage { get; set; }
    }

    /// <summary>
    /// 功能描述：表
    /// 创建人：唐林
    /// 创建时间：2014-09-16
    /// </summary>
    public class Grid : IDisposable
    {
        #region 事件/代理
        /// <summary>
        /// 添加或移除按钮点击事件响应函数属性
        /// </summary>
        public event EventHandle_ClickEvent ClickEvent
        {
            add { _clickEvent += value; }
            remove { _clickEvent -= value; }
        }
        private EventHandle_ClickEvent _clickEvent;

        public event EventHandle_ClickEvent MouseDownEvent
        {
            add { _mouseDownEvent += value; }
            remove { _mouseDownEvent -= value; }
        }
        private EventHandle_ClickEvent _mouseDownEvent;
        #endregion

        #region 属性/变量
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public String Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private String _text = String.Empty;

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public Int32 ID
        {
            get { return _id; }
        }
        private Int32 _id = -1;

        /// <summary>
        /// 读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style
        {
            get { return _style; }
        }
        private IStyle _style;

        /// <summary>
        /// 读取或设置按钮所在矩形区域属性
        /// </summary>
        public RectangleF Rect
        {
            get { return _rect; }
            set
            {
                if (_rect == value)
                    return;

                _rect = value;
            }
        }
        private RectangleF _rect;

        /// <summary>
        /// 读取或设置是否获得焦点属性（功能后续添加）
        /// </summary>
        public Boolean IsFocus
        {
            get { return _isFocus; }
            set
            {
                if (_isFocus == value)
                    return;

                _isFocus = value;
            }
        }
        private Boolean _isFocus = false;
        #endregion

        private Row[] _rows;
        private Column[] _columns;

        private List<List<Object>> _values = new List<List<object>>();
        private List<List<RowItem>> _rowList_Show = new List<List<RowItem>>();//用于存储当前显示的行信息
        private List<Button> _buttons = new List<Button>();
        private Int32 _currentPage = 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Grid(RectangleF rect, Row[] rows, Column[] columns, List<List<Object>> values, PageButton pageDownButton, PageButton pageUpButton)
        {
            _rect = rect;
            _rows = rows;
            _columns = columns;
            _values = values;


            Button upButton = new Button(
                "",
                pageUpButton.Rect,
                1,
                new ButtonStyle()
                {
                    Background = pageUpButton.UpImage,
                    DownImage = pageUpButton.DownImage,
                    DisableImage = pageUpButton.DisableImage,
                    FontStyle = new FontStyle_ES()
                }
                );
            upButton.ClickEvent += btn_ClickEvent;
            _buttons.Add(upButton);

            Button downButton = new Button(
                "",
                pageDownButton.Rect,
                0,
                new ButtonStyle()
                {
                    Background = pageDownButton.UpImage,
                    DownImage = pageDownButton.DownImage,
                    DisableImage = pageDownButton.DisableImage,
                    FontStyle = new FontStyle_ES()
                }
                );
            downButton.ClickEvent += btn_ClickEvent;
            _buttons.Add(downButton);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    Int32 pageCount = (_values.Count - 1) / _rows.Length + 1;
                    _currentPage = (pageCount == _currentPage ? _currentPage : _currentPage + 1);
                    break;
                case 1:
                    _currentPage = (_currentPage == 1 ? 1 : _currentPage - 1);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 鼠标按下功能函数（该函数需在界面检测鼠标按下函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseUp(Point p)
        {
            _buttons.ForEach(a => a.MouseUp(p));

            if (_rect.Contains(p))
            {
                for (int i = 0; i < _rows.Length; i++)
                {
                    RectangleF rect = new RectangleF(
                        _rect.Left,
                        _rect.Top + (_rect.Height / _rows.Length) * i,
                        _rect.Width,
                        _rect.Height / _rows.Length
                        );
                    if (_rowList_Show.Count - 1 < i)
                        return;

                    if (rect.Contains(p))
                    {
                        if (_clickEvent != null)
                            _clickEvent(this, new ClickEventArgs<int>(_rows.Length * (_currentPage - 1) + i));
                    }
                }
            }
        }

        /// <summary>
        /// 鼠标按下功能函数（该函数需在界面检测鼠标按下函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseDown(Point p)
        {
            _buttons.ForEach(a => a.MouseDown(p));


        }

        private void getCurrentValue()
        {
            if (_values == null || _values.Count == 0)
            {
                _rowList_Show.Clear();
                return;
            }

            Int32 currentMaxIndex = _rows.Length * _currentPage;
            if (currentMaxIndex >= _values.Count)
            {
                _rowList_Show.Clear();
                for (int i = 0; i < _values.Count - _rows.Length * (_currentPage - 1); i++)
                {
                    List<RowItem> rowItems = new List<RowItem>();
                    for (int j = 0; j < _columns.Length; j++)
                    {
                        rowItems.Add(new RowItem() { Value = _values[_rows.Length * (_currentPage - 1) + i][j] });
                    }
                    _rowList_Show.Add(rowItems);

                }
            }
            else
            {
                _rowList_Show.Clear();
                for (int i = 0; i < _rows.Length; i++)
                {
                    List<RowItem> rowItems = new List<RowItem>();
                    for (int j = 0; j < _columns.Length; j++)
                    {
                        rowItems.Add(new RowItem() { Value = _values[_rows.Length * (_currentPage - 1) + i][j] });
                    }
                    _rowList_Show.Add(rowItems);

                }
            }
        }

        /// <summary>
        /// 按钮绘制（需在界面绘制函数中实时调用）
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs, Rectangle rect)
        {
            getCurrentValue();

            paint_Page(dcGs, rect);
            paint_PageControl(dcGs);
            paint_GridFrame(dcGs);
            paint_GridValue(dcGs);
        }

        private void paint_Page(Graphics dcGs, Rectangle rect)
        {
            dcGs.DrawString(
                _currentPage.ToString() + @"/" + ((_values.Count - 1) / _rows.Length + 1).ToString(),
                new Font("Verdana", 9),
                Brushes.Yellow,
                rect,
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        private void paint_PageControl(Graphics dcGs)
        {
            if (_currentPage == 1) _buttons[0].Enable = false;
            else
            {
                _buttons[0].Enable = true;
            }

            if (((_values.Count - 1) / _rows.Length + 1) == _currentPage) _buttons[1].Enable = false;
            else
            {
                _buttons[1].Enable = true;
            }

            _buttons.ForEach(a => a.Paint(dcGs));

        }

        private void paint_GridFrame(Graphics dcGs)
        {
            dcGs.DrawRectangle(_columns[0].LinePen, (int)_rect.Left, (int)_rect.Top, (int)_rect.Width, (int)_rect.Height);
            for (int i = 0; i < _rows.Length + 1; i++)
            {
                Brush brush = Brushes.Yellow;
                if (i % 2 == 0) brush = new SolidBrush(Color.FromArgb(78, 199, 209));
                dcGs.DrawLine(
                    new Pen(brush, 1),
                    new PointF(_rect.Left, _rect.Top + (_rect.Height / _rows.Length) * i),
                    new PointF(_rect.Left + _rect.Width, _rect.Top + (_rect.Height / _rows.Length) * i)
                    );
            }

            float temp = _columns[0].Width;
            for (int i = 1; i < _columns.Length; i++)
            {
                dcGs.DrawLine(
                    _columns[i].LinePen,
                    new PointF(_rect.Left + temp, _rect.Top),
                    new PointF(_rect.Left + temp, _rect.Top + _rect.Height)
                    );
                if (i != _columns.Length)
                    temp += _columns[i].Width;
            }
        }

        private void paint_GridValue(Graphics dcGs)
        {
            if (_rowList_Show == null || _rowList_Show.Count == 0)
                return;

            for (int i = 0; i < _rowList_Show.Count; i++)
            {
                float temp_width = 0;
                for (int j = 0; j < _columns.Length; j++)
                {
                    if (_rowList_Show[i][j].Value == null)
                    {
                        temp_width += _columns[j].Width;
                        continue;
                    }

                    if (_rowList_Show[i][j].Value is String)
                        dcGs.DrawString(
                            _rowList_Show[i][j].Value.ToString(),
                            _columns[j].Font,
                            _columns[j].Brush,
                            new RectangleF(_rect.Left + temp_width, _rect.Top + (_rect.Height / _rows.Length) * i, _columns[j].Width, _rect.Height / _rows.Length),
                            _columns[j].SF
                            );
                    else if (_rowList_Show[i][j].Value is Image)
                    {
                        Image image = (Image)_rowList_Show[i][j].Value;
                        dcGs.DrawImage(
                            image,
                            new RectangleF(
                                _rect.Left + temp_width + (_columns[j].Width - image.Width) / 2,
                                _rect.Top + (_rect.Height / _rows.Length) * i + (_rect.Height / _rows.Length - image.Height) / 2,
                                image.Width,
                                image.Height
                                )
                            );
                    }
                    else if (_rowList_Show[i][j].Value is List<Object>)
                    {
                        ((List<Object>)_rowList_Show[i][j].Value).ForEach(a =>
                        {
                            if (a is String)
                                dcGs.DrawString(
                                    a.ToString(),
                                    _columns[j].Font,
                                    _columns[j].Brush,
                                    new RectangleF(
                                        _rect.Left + temp_width,
                                        _rect.Top + (_rect.Height / _rows.Length) * i,
                                        _columns[j].Width,
                                        _rect.Height / _rows.Length
                                        ),
                                    _columns[j].SF
                                    );
                            else if (a is Image)
                            {
                                Image image = (Image)a;
                                dcGs.DrawImage(
                                    image,
                                    new RectangleF(
                                        _rect.Left + temp_width + (_columns[j].Width - image.Width) / 2,
                                        _rect.Top + (_rect.Height / _rows.Length) * i + (_rect.Height / _rows.Length - image.Height) / 2,
                                        image.Width,
                                        image.Height
                                        )
                                    );
                            }
                            else if (a is ValueType)
                            {
                                String str = a.ToString();
                                if (a is Boolean)
                                {
                                    str = (Boolean)a ? "Y" : "N";
                                }

                                dcGs.DrawString(
                                    str,
                                    _columns[j].Font,
                                    _columns[j].Brush,
                                    new RectangleF(
                                        _rect.Left + temp_width,
                                        _rect.Top + (_rect.Height / _rows.Length) * i,
                                        _columns[j].Width,
                                        _rect.Height / _rows.Length
                                        ),
                                    _columns[j].SF
                                    );
                            }
                        });
                    }
                    else if (_rowList_Show[i][j].Value is ValueType)
                    {
                        String str = _rowList_Show[i][j].Value.ToString();
                        if (_rowList_Show[i][j].Value is Boolean)
                        {
                            str = (Boolean)_rowList_Show[i][j].Value ? "Y" : "N";
                        }

                        dcGs.DrawString(
                            str,
                            _columns[j].Font,
                            _columns[j].Brush,
                            new RectangleF(_rect.Left + temp_width, _rect.Top + (_rect.Height / _rows.Length) * i, _columns[j].Width, _rect.Height / _rows.Length),
                            _columns[j].SF
                            );
                    }
                    temp_width += _columns[j].Width;
                }
            }
        }

        #region IDisposable接口函数
        /// <summary>
        /// IDisposable接口函数：根据需要添加功能
        /// </summary>
        public void Dispose()
        {
            //按钮释放资源
        }
        #endregion
    }
}
