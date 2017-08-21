using System;
using System.Collections.Generic;
using System.Drawing;
using ES.Facility.Common.Control.Common;

namespace ES.Facility.Common.Control
{
    /// <summary>
    /// 功能描述：ListBox头
    /// 创建人：lih
    /// 创建时间：2014-07-15
    /// </summary>
    public struct ListBoxHeader
    {
        /// <summary>
        /// 读取或设置Header文本属性
        /// </summary>
        public String Text { get; set; }
        /// <summary>
        /// 读取或设置Header文本字体属性
        /// </summary>
        public Font HeaderFont { get; set; }
        /// <summary>
        /// 读取或设置Header对应列数据文本字体属性
        /// </summary>
        public Font DataFont { get; set; }
        /// <summary>
        /// 读取或设置Header文本画刷属性
        /// </summary>
        public Brush TextBrush { get; set; }

        /// <summary>
        /// 选中时文本字体颜色
        /// </summary>
        public Brush SelectedTextBrush { get; set; }
        /// <summary>
        /// 读取或设置Header背景画刷属性
        /// </summary>
        public Brush BackgroundBrush { get; set; }
        /// <summary>
        /// 读取或设置Header宽度属性
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// 读取或设置Header高度属性
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// 读取或设置Header对应列显示列表对象的属性名称属性
        /// </summary>
        public String TProperty { get; set; }
        /// <summary>
        /// 读取或设置Header文本对齐方式属性
        /// </summary>
        public StringFormat SF_Header { get; set; }
        /// <summary>
        /// 读取或设置Header对应列数据文本对齐属性
        /// </summary>
        public StringFormat SF_Data { get; set; }
        /// <summary>
        /// 读取或设置该列是否为编号行属性
        /// </summary>
        public Boolean IsCount { get; set; }
        /// <summary>
        /// 读取或设置编号行编号是否跟随翻页改变属性
        /// </summary>
        public Boolean IsCountUp { get; set; }
        /// <summary>
        /// 读取或设置是否处理故障开始时间与结束时间属性（主要针对开始时间与结束时间上下分别显示并带背景框）
        /// </summary>
        public Boolean IsHandleTime { get; set; }
    }

    /// <summary>
    /// 功能描述：自定义ListBox
    /// 创建人：lih
    /// 创建时间：2014-07-15
    /// </summary>
    /// <typeparam name="T">Item类型</typeparam>
    public class ListBox<T> : IControl, IDisposable
    {
        private ListBoxHeader[] _headers;
        private Int32 currentPage = 0;
        private List<T> _currentDataList = new List<T>();

        #region IControl接口属性
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

        /// <summary>
        /// 读取或设置数据列表属性
        /// </summary>
        public List<T> DataList
        {
            get { return _dataList; }
            set
            {
                if (_dataList == value)
                    return;

                _dataList = value;
                firstShowDataIndexInData = 0;
                indexInData = 0;
                SelectedIndex = (value == null||value.Count==0)?-1:0;
                currentPage = 0;
            }
        }
        private List<T> _dataList = new List<T>();

        /// <summary>
        /// 读取或设置选中行索引值属性（不能用该属性在数据列表中索引项）
        /// </summary>
        public Int32 SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }
        private Int32 _selectedIndex = -1;

        /// <summary>
        /// 读取当前选择项属性
        /// </summary>
        public T SelectedItem
        {
            get { return _currentDataList[_selectedIndex]; }
        }

        /// <summary>
        /// 读取当前显示页面是否包含选中行属性
        /// </summary>
        public Boolean IsCurrentPageItemSelected
        {
            get { return _isCurrentPageItemSelected; }
        }
        private Boolean _isCurrentPageItemSelected = false;

        /// <summary>
        /// 读取或设置显示数据的行数属性
        /// </summary>
        public Int32 RowCount
        {
            get { return _rowCount; }
            set { _rowCount = value; }
        }
        private Int32 _rowCount = 1;

        /// <summary>
        /// 读取或设置背景画刷属性（初始为透明）
        /// </summary>
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set
            {
                if (_backgroundBrush == value)
                    return;

                _backgroundBrush = value;
            }
        }
        private Brush _backgroundBrush = new SolidBrush(Color.Transparent);

        /// <summary>
        /// 读取或设置网格线画刷属性（初始为透明）
        /// </summary>
        public Brush GridBrush
        {
            get { return _gridBrush; }
            set
            {
                if (_gridBrush == value)
                    return;

                _gridBrush = value;
            }
        }
        private Brush _gridBrush = new SolidBrush(Color.Transparent);

        /// <summary>
        /// 设置或获取选中项的背景色
        /// </summary>
        public Brush SelectedItemBrush
        {
            get { return _selectedItemBrush; }
            set
            {
                if (value != _selectedItemBrush)
                {
                    _selectedItemBrush = value;
                }
            }
        }
        private Brush _selectedItemBrush = new SolidBrush(Color.Blue);

        /// <summary>
        /// 读取或设置是否隐藏表头属性
        /// </summary>
        public Boolean HideHeader { get; set; }

        /// <summary>
        /// 读取或设置是否选择最后Item属性
        /// </summary>
        public Boolean IsFinalItem { get; set; }

        /// <summary>
        /// 读取或设置是否选择第一个Item属性
        /// </summary>
        public Boolean IsFirsItem { get; set; }

        /// <summary>
        /// 构造函数：
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="dataList"></param>
        /// <param name="header"></param>
        public ListBox(RectangleF rect, List<T> dataList, params ListBoxHeader[] header)
        {
            _rect = rect;
            _headers = header;
            _dataList = dataList;
        }

        //public void SetSelectedName(String name)
        //{
        //    for (int i = 0; i < this._dataList.Count; i++)
        //    {
        //        if(this._dataList[i].GetType().GetProperty(this._headers[0].TProperty).GetValue(this._dataList[i], null).ToString())
        //    }
        //}

        #region 鼠标检测函数
        /// <summary>
        /// 鼠标按下功能函数（该函数需在界面检测鼠标按下函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseDown(Point p)
        {
            if (_rect.Contains(p))//光标坐标在按钮矩形区域内，实现按钮按下功能
            {
                float singleHeight = _rect.Height/RowCount;

                for (int i = 0; i < RowCount; i++)
                {
                    if (p.Y > _rect.Y + singleHeight*i && p.Y < _rect.Y + singleHeight*(i + 1))
                    {
                        if (i >= _currentDataList.Count)
                            return;

                        SelectedIndex = i + currentPage*_rowCount;
                        indexInData = i + currentPage * _rowCount;
                    }
                }
            }
        }

        /// <summary>
        /// 鼠标弹起功能函数（该函数需在界面检测鼠标弹起函数中调用）
        /// </summary>
        /// <param name="p">光标所在坐标</param>
        public void MouseUp(Point p)
        {
            if (_rect.Contains(p))//光标坐标在按钮矩形区域内，实现按钮弹起功能
            {
            }
        }
        #endregion

        /// <summary>
        /// 绘制ListBox
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            paint_Background_Grid(dcGs);
            paint_Header(dcGs);
            paint_Data(dcGs);


            IsFinalItem = (indexInData == _dataList.Count - 1);
            IsFirsItem = (indexInData == 0);
        }

        /// <summary>
        /// 绘制页码信息
        /// </summary>
        /// <param name="dcGs"></param>
        /// <param name="rect"></param>
        public void Paint_PageInfo(Graphics dcGs, RectangleF rect)
        {
            dcGs.FillRectangle(Brushes.White, rect);

            dcGs.DrawRectangle(new Pen(Brushes.Black, 2), rect.X, rect.Y, rect.Width, rect.Height);

            dcGs.DrawString(
                ((firstShowDataIndexInData+_currentDataList.Count-1)/RowCount+1).ToString() + "/" + ((_dataList.Count-1) / _rowCount+1).ToString(),
                new Font("宋体", 11, FontStyle.Bold),
                Brushes.Black,
                rect,
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

        }

        /// <summary>
        /// 绘制表格
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Background_Grid(Graphics dcGs)
        {
            dcGs.FillRectangle(_backgroundBrush, _rect);

            if (HideHeader)//隐藏表头
            {
                //横线
                for (int i = 0; i < RowCount + 1; i++)
                {
                    dcGs.DrawLine(
                        new Pen(_gridBrush, 2),
                        new PointF(_rect.X, _rect.Y + (_rect.Height / RowCount) * i),
                        new PointF(_rect.X + _rect.Width, _rect.Y + (_rect.Height / RowCount) * i)
                        );
                }
                //丛线
                float xpos = _rect.X;
                dcGs.DrawLine(
                         new Pen(_gridBrush, 2),
                         new PointF(xpos, _rect.Y),
                         new PointF(xpos, _rect.Y + _rect.Height)
                         );
                xpos -= 1;
                for (int i = 0; i < _headers.Length; i++)
                {
                    xpos += _headers[i].Width + 2;
                    dcGs.DrawLine(
                        new Pen(_gridBrush, 2),
                        new PointF(xpos, _rect.Y),
                        new PointF(xpos, _rect.Y + _rect.Height)
                        );
                }
            }
            else
            {
                //横线
                dcGs.DrawLine(
                    new Pen(_gridBrush, 2),
                    new PointF(_rect.X, _rect.Y),
                    new PointF(_rect.X + _rect.Width, _rect.Y)
                    );
                float ypos = _rect.Y + _headers[0].Height;
                for (int i = 0; i < RowCount + 1; i++)
                {
                    dcGs.DrawLine(
                        new Pen(_gridBrush, 2),
                        new PointF(_rect.X, ypos),
                        new PointF(_rect.X + _rect.Width, ypos)
                        );
                    ypos += ((_rect.Height - _headers[0].Height) / RowCount);
                }
                //丛线
                float xpos = _rect.X;
                dcGs.DrawLine(new Pen(_gridBrush, 2), new PointF(xpos, _rect.Y), new PointF(xpos, _rect.Y + _rect.Height));
                xpos -= 1;
                for (int i = 0; i < _headers.Length; i++)
                {
                    xpos += _headers[i].Width + 2;
                    dcGs.DrawLine(new Pen(_gridBrush, 2), new PointF(xpos, _rect.Y), new PointF(xpos, _rect.Y + _rect.Height));
                }
            }
        }

        /// <summary>
        /// 绘制列表头
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Header(Graphics dcGs)
        {
            //ListBox标题栏绘制（文字与）
            if (!HideHeader)
            {
                float xpos = _rect.X;
                for (int i = 0; i < _headers.Length; i++)
                {
                    dcGs.FillRectangle(
                        _headers[i].BackgroundBrush,
                        new RectangleF(xpos, _rect.Y, _headers[i].Width, _headers[i].Height)
                        );//绘制列表头背景颜色
                    dcGs.DrawString(
                        _headers[i].Text,
                        _headers[i].HeaderFont,
                        _headers[i].TextBrush,
                        new RectangleF(xpos, _rect.Y + 2, _headers[i].Width, _headers[i].Height),
                        _headers[i].SF_Header
                        );//绘制列表头数据

                    xpos += _headers[i].Width + 2;
                }
            }

            Boolean isSelected = false;
            float ypos = HideHeader ? _rect.Y : (_rect.Y + _headers[0].Height);
            float singleY = (HideHeader ? _rect.Height : (_rect.Height - _headers[0].Height)) / RowCount;
            for (int i = 1; i <= _rowCount; i++)
            {
                //绘制选中行高亮显示
                if (i + currentPage * _rowCount - 1 == SelectedIndex)
                {
                    float width = _headers[0].IsCount ? 0 : _headers[0].Width;
                    for (int j = 1; j < _headers.Length; j++)
                    {
                        width += (2 + _headers[j].Width);
                    }
                    dcGs.FillRectangle(_selectedItemBrush, new RectangleF(
                        _headers[0].IsCount ? _rect.X + _headers[0].Width + 2 : _rect.X,
                        ypos,
                        width,
                        singleY)
                        );
                    isSelected = true;
                }

                _isCurrentPageItemSelected = isSelected;

                //绘制行号
                if (_headers[0].IsCount)
                {
                    if (_headers[0].IsCountUp)
                    {
                        dcGs.DrawString(
                            (i + currentPage * _rowCount).ToString(),
                            _headers[0].HeaderFont,
                            _headers[0].TextBrush,
                            new RectangleF(
                                _rect.X,
                                ypos + 2,
                                _headers[0].Width,
                                singleY
                                ),
                                _headers[0].SF_Header
                                );
                    }
                    else
                    {
                        dcGs.DrawString(
                            i.ToString(),
                            _headers[0].HeaderFont,
                            _headers[0].TextBrush,
                            new RectangleF(
                                _rect.X,
                                ypos + 2,
                                _headers[0].Width,
                                singleY
                                ),
                                _headers[0].SF_Header
                                );
                    }
                }
                ypos += (HideHeader ? _rect.Height : (_rect.Height - _headers[0].Height)) / _rowCount;
            }
        }

        /// <summary>
        /// 绘制表格数据
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Data(Graphics dcGs)
        {
            Int32 pages = _dataList.Count / _rowCount;//总页数
            if (pages > currentPage)//总页数大于当前页数，当前显示列表取到当前页的列表（从当前页第一个数据开始，取行数的个数）
                _currentDataList = _dataList.GetRange(firstShowDataIndexInData, _rowCount);
            else //总页数==当前页数，当前显示列表取最后一页数据列表（从当前页第一个数据开始，到最后一个数据）
                _currentDataList = _dataList.GetRange(firstShowDataIndexInData, _dataList.Count - currentPage * _rowCount);

            float ypos = HideHeader ? _rect.Y : (_rect.Y + _headers[0].Height);//第一行Y坐标为列表Y坐标加列表头高度
            float singleY = (HideHeader ? _rect.Height : (_rect.Height - _headers[0].Height)) / RowCount;

            for (int i = 0; i <= _currentDataList.Count - 1; i++)//显示当前列表数据
            {
                float xpos = _headers[0].IsCount ? _rect.X + _headers[0].Width + 5 : _rect.X;//为了数据显示居中，X坐标需调整加5个像素

                for (int j = _headers[0].IsCount ? 1 : 0; j < _headers.Length; j++)//显示每列数据
                {
                    if (_headers[j].IsHandleTime)
                    {
                        //根据属性名称以及所在对象反射获取数据
                        string data;
                        if (_headers[j].TProperty != null
                           && String.Compare(_headers[j].TProperty, String.Empty) != 0)
                        {
                            data = _currentDataList[i].GetType().GetProperty(_headers[j].TProperty).GetValue(_currentDataList[i], null).ToString();
                        }
                        else
                        {
                            data = _currentDataList[i].ToString();
                        }
                        String[] strs = data.Split('\n');
                        dcGs.FillRectangle(
                            new SolidBrush(Color.Black),
                            new RectangleF(xpos - 5, ypos + 2,
                                _headers[j].Width - 30,
                                singleY / 2 - 3)
                                );
                        //显示数据
                        dcGs.DrawString(
                            strs[0],
                            _headers[j].DataFont,
                            _headers[j].TextBrush,
                            new RectangleF(
                                xpos - 5,
                                ypos + 2,
                                _headers[j].Width - 30,
                                singleY / 2 - 3),
                            new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                            );
                        if (strs.Length == 2)
                        {
                            dcGs.FillRectangle(
                                new SolidBrush(Color.Black),
                                new RectangleF(
                                    xpos - 5,
                                    ypos + singleY / 2 + 1,
                                    _headers[j].Width - 30,
                                    singleY / 2 - 3
                                    )
                                );
                            //显示数据
                            dcGs.DrawString(
                                strs[1],
                                _headers[j].DataFont,
                                _headers[j].TextBrush,
                                new RectangleF(
                                    xpos - 5,
                                    ypos + singleY / 2 + 1,
                                    _headers[j].Width - 30,
                                    singleY / 2 - 3
                                    ),
                                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                                );
                        }
                        xpos += _headers[j].Width + 2;//为了数据垂直方向显示居中，Y坐标需调整加2个像素
                    }
                    else
                    {
                        //根据属性名称以及所在对象反射获取数据
                        string data;
                        if (_headers[j].TProperty!=null 
                           &&  String.Compare(_headers[j].TProperty, String.Empty) != 0)
                        {
                            data = _currentDataList[i].GetType().GetProperty(_headers[j].TProperty).GetValue(_currentDataList[i], null).ToString();
                        }
                        else
                        {
                            data = _currentDataList[i].ToString();
                        }


                        if ((SelectedIndex!=-1)&& (i + currentPage * _rowCount == SelectedIndex))
                        {
                            if (_headers[j].SelectedTextBrush != null)
                            {
                                //显示数据
                                dcGs.DrawString(
                                    data,
                                    _headers[j].DataFont,
                                    _headers[j].SelectedTextBrush,
                                    new RectangleF(
                                        xpos,
                                        ypos + 2,
                                        _headers[j].Width,
                                        singleY),
                                    _headers[j].SF_Data
                                    );
                            }
                               
                        }
                        else
                        {
                            //显示数据
                            dcGs.DrawString(
                                data,
                                _headers[j].DataFont,
                                _headers[j].TextBrush,
                                new RectangleF(
                                    xpos,
                                    ypos + 2,
                                    _headers[j].Width,
                                    singleY),
                                _headers[j].SF_Data
                                );
                        }
                      
                        xpos += _headers[j].Width + 2;//为了数据垂直方向显示居中，Y坐标需调整加2个像素
                    }
                }
                ypos += singleY;//递加每行高度
            }
        }

        /// <summary>
        /// 向下选择项
        /// </summary>
        public void NextItem()
        {
            if (indexInData == _dataList.Count - 1)
                return;

            if (_selectedIndex == _currentDataList.Count - 1)
            {
                _currentDataList.RemoveAt(0);
                _currentDataList.Add(_dataList[indexInData]);
                firstShowDataIndexInData++;
            }
            else if (_selectedIndex < _currentDataList.Count)
                _selectedIndex++;

            indexInData++;
        }

        private Int32 indexInData = 0;//用于上下移动时在顶端与低端时用于实现数据的上下移动
        private Int32 firstShowDataIndexInData = 0;//用于翻页时确定需要显示的数据在总数据列表中的索引


        /// <summary>
        /// 
        /// </summary>
        public void IniteIndexInData()
        {
            indexInData = 0;
            _selectedIndex = 0;
        }

        /// <summary>
        /// 向上选择项
        /// </summary>
        public void LastItem()
        {
            if (indexInData == 0)
                return;

            if (_selectedIndex == 0)
            {
                _currentDataList.RemoveAt(_currentDataList.Count - 1);
                _currentDataList.Add(_dataList[indexInData]);
                firstShowDataIndexInData--;
            }
            else if (_selectedIndex > 0)
                _selectedIndex--;

            indexInData--;
        }

        /// <summary>
        /// 向下翻页
        /// </summary>
        public void NextPage()
        {
            Int32 pages = _dataList.Count / _rowCount;
            if (currentPage == pages)
                return;

            currentPage++;
            firstShowDataIndexInData += _rowCount;
        }

        /// <summary>
        /// 向上翻页
        /// </summary>
        public void LastPage()
        {
            Int32 pages = _dataList.Count / _rowCount;
            if (currentPage == 0)
                return;

            currentPage--;
            if (currentPage == 0)
            {
                firstShowDataIndexInData = 0;
            }
            else firstShowDataIndexInData -= _rowCount;
        }

        public void Dispose()
        {
        }
    }
}
