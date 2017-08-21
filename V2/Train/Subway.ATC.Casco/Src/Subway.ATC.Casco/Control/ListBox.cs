using System;
using System.Collections.Generic;
using System.Drawing;
using Subway.ATC.Casco.Control.Common;

namespace Subway.ATC.Casco.Control
{
    /// <summary>
    /// 功能描述：ListBox头
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public struct ListBoxHeader
    {
        /// <summary>
        /// 读取或设置Header文本属性
        /// </summary>
        public string Text { get; set; }
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
        public string TProperty { get; set; }
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
        public bool IsCount { get; set; }
        /// <summary>
        /// 读取或设置编号行编号是否跟随翻页改变属性
        /// </summary>
        public bool IsCountUp { get; set; }
        /// <summary>
        /// 读取或设置是否处理故障开始时间与结束时间属性（主要针对开始时间与结束时间上下分别显示并带背景框）
        /// </summary>
        public bool IsHandleTime { get; set; }
    }

    /// <summary>
    /// 功能描述：自定义ListBox
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    /// <typeparam name="T">Item类型</typeparam>
    public class ListBox<T> : IControl, IDisposable
    {
        private ListBoxHeader[] _headers;
        private int currentPage = 0;
        private List<T> _currentDataList = new List<T>();

        #region IControl接口属性
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        private string _text = string.Empty;

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public int ID
        {
            get { return this._id; }
        }
        private int _id = -1;

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
            get { return this._rect; }
            set
            {
                if (this._rect == value)
                    return;

                this._rect = value;
            }
        }
        private RectangleF _rect;

        /// <summary>
        /// 读取或设置是否获得焦点属性（功能后续添加）
        /// </summary>
        public bool IsFocus
        {
            get { return this._isFocus; }
            set
            {
                if (this._isFocus == value)
                    return;

                this._isFocus = value;
            }
        }
        private bool _isFocus = false;
        #endregion

        /// <summary>
        /// 读取或设置数据列表属性
        /// </summary>
        public List<T> DataList
        {
            get { return this._dataList; }
            set
            {
                if (_dataList == value)
                    return;

                _dataList = value;
                this.firstShowDataIndexInData = 0;
                this.indexInData = 0;
                this.SelectedIndex = (value == null||value.Count==0)?-1:0;
                this.currentPage = 0;
            }
        }
        private List<T> _dataList = new List<T>();

        /// <summary>
        /// 读取或设置选中行索引值属性（不能用该属性在数据列表中索引项）
        /// </summary>
        public int SelectedIndex
        {
            get { return this._selectedIndex; }
            set { this._selectedIndex = value; }
        }
        private int _selectedIndex = -1;

        /// <summary>
        /// 读取当前选择项属性
        /// </summary>
        public T SelectedItem
        {
            get { return this._currentDataList[this._selectedIndex]; }
        }

        /// <summary>
        /// 读取当前显示页面是否包含选中行属性
        /// </summary>
        public bool IsCurrentPageItemSelected
        {
            get { return this._isCurrentPageItemSelected; }
        }
        private bool _isCurrentPageItemSelected = false;

        /// <summary>
        /// 读取或设置显示数据的行数属性
        /// </summary>
        public int RowCount
        {
            get { return this._rowCount; }
            set { this._rowCount = value; }
        }
        private int _rowCount = 1;

        /// <summary>
        /// 读取或设置背景画刷属性（初始为透明）
        /// </summary>
        public Brush BackgroundBrush
        {
            get { return this._backgroundBrush; }
            set
            {
                if (this._backgroundBrush == value)
                    return;

                this._backgroundBrush = value;
            }
        }
        private Brush _backgroundBrush = new SolidBrush(Color.Transparent);

        /// <summary>
        /// 读取或设置网格线画刷属性（初始为透明）
        /// </summary>
        public Brush GridBrush
        {
            get { return this._gridBrush; }
            set
            {
                if (this._gridBrush == value)
                    return;

                this._gridBrush = value;
            }
        }
        private Brush _gridBrush = new SolidBrush(Color.Transparent);

        /// <summary>
        /// 读取或设置是否隐藏表头属性
        /// </summary>
        public bool HideHeader { get; set; }

        /// <summary>
        /// 读取或设置是否选择最后Item属性
        /// </summary>
        public bool IsFinalItem { get; set; }

        /// <summary>
        /// 读取或设置是否选择第一个Item属性
        /// </summary>
        public bool IsFirsItem { get; set; }

        /// <summary>
        /// 构造函数：
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="dataList"></param>
        /// <param name="header"></param>
        public ListBox(RectangleF rect, List<T> dataList, params ListBoxHeader[] header)
        {
            this._rect = rect;
            this._headers = header;
            this._dataList = dataList;
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
            if (this._rect.Contains(p))//光标坐标在按钮矩形区域内，实现按钮按下功能
            {
                float singleHeight = this._rect.Height/this.RowCount;

                for (int i = 0; i < this.RowCount; i++)
                {
                    if (p.Y > this._rect.Y + singleHeight*i && p.Y < this._rect.Y + singleHeight*(i + 1))
                    {
                        if (i >= this._currentDataList.Count)
                            return;

                        SelectedIndex = i + this.currentPage*this._rowCount;
                        this.indexInData = i + this.currentPage * this._rowCount;
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
            if (this._rect.Contains(p))//光标坐标在按钮矩形区域内，实现按钮弹起功能
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
            this.paint_Background_Grid(dcGs);
            this.paint_Header(dcGs);
            this.paint_Data(dcGs);


            this.IsFinalItem = (this.indexInData == this._dataList.Count - 1);
            this.IsFirsItem = (this.indexInData == 0);
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
                ((this.firstShowDataIndexInData+this._currentDataList.Count-1)/this.RowCount+1).ToString() + "/" + ((this._dataList.Count-1) / this._rowCount+1).ToString(),
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
            dcGs.FillRectangle(this._backgroundBrush, this._rect);

            if (this.HideHeader)//隐藏表头
            {
                //横线
                for (int i = 0; i < this.RowCount + 1; i++)
                {
                    dcGs.DrawLine(
                        new Pen(_gridBrush, 2),
                        new PointF(this._rect.X, this._rect.Y + (this._rect.Height / this.RowCount) * i),
                        new PointF(this._rect.X + this._rect.Width, this._rect.Y + (this._rect.Height / this.RowCount) * i)
                        );
                }
                //丛线
                float xpos = this._rect.X;
                dcGs.DrawLine(
                         new Pen(_gridBrush, 2),
                         new PointF(xpos, this._rect.Y),
                         new PointF(xpos, this._rect.Y + this._rect.Height)
                         );
                xpos -= 1;
                for (int i = 0; i < this._headers.Length; i++)
                {
                    xpos += this._headers[i].Width + 2;
                    dcGs.DrawLine(
                        new Pen(_gridBrush, 2),
                        new PointF(xpos, this._rect.Y),
                        new PointF(xpos, this._rect.Y + this._rect.Height)
                        );
                }
            }
            else
            {
                //横线
                dcGs.DrawLine(
                    new Pen(_gridBrush, 2),
                    new PointF(this._rect.X, this._rect.Y),
                    new PointF(this._rect.X + this._rect.Width, this._rect.Y)
                    );
                float ypos = this._rect.Y + this._headers[0].Height;
                for (int i = 0; i < this.RowCount + 1; i++)
                {
                    dcGs.DrawLine(
                        new Pen(_gridBrush, 2),
                        new PointF(this._rect.X, ypos),
                        new PointF(this._rect.X + this._rect.Width, ypos)
                        );
                    ypos += ((this._rect.Height - this._headers[0].Height) / this.RowCount);
                }
                //丛线
                float xpos = this._rect.X;
                dcGs.DrawLine(new Pen(_gridBrush, 2), new PointF(xpos, this._rect.Y), new PointF(xpos, this._rect.Y + this._rect.Height));
                xpos -= 1;
                for (int i = 0; i < this._headers.Length; i++)
                {
                    xpos += this._headers[i].Width + 2;
                    dcGs.DrawLine(new Pen(_gridBrush, 2), new PointF(xpos, this._rect.Y), new PointF(xpos, this._rect.Y + this._rect.Height));
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
            if (!this.HideHeader)
            {
                float xpos = this._rect.X;
                for (int i = 0; i < this._headers.Length; i++)
                {
                    dcGs.FillRectangle(
                        this._headers[i].BackgroundBrush,
                        new RectangleF(xpos, this._rect.Y, this._headers[i].Width, this._headers[i].Height)
                        );//绘制列表头背景颜色
                    dcGs.DrawString(
                        this._headers[i].Text,
                        this._headers[i].HeaderFont,
                        this._headers[i].TextBrush,
                        new RectangleF(xpos, this._rect.Y + 2, this._headers[i].Width, this._headers[i].Height),
                        this._headers[i].SF_Header
                        );//绘制列表头数据

                    xpos += this._headers[i].Width + 2;
                }
            }

            bool isSelected = false;
            float ypos = this.HideHeader ? this._rect.Y : (this._rect.Y + this._headers[0].Height);
            float singleY = (this.HideHeader ? this._rect.Height : (this._rect.Height - this._headers[0].Height)) / this.RowCount;
            for (int i = 1; i <= this._rowCount; i++)
            {
                //绘制选中行高亮显示
                if (i + this.currentPage * this._rowCount - 1 == SelectedIndex)
                {
                    float width = this._headers[0].IsCount ? 0 : this._headers[0].Width;
                    for (int j = 1; j < this._headers.Length; j++)
                    {
                        width += (2 + this._headers[j].Width);
                    }
                    dcGs.FillRectangle(new SolidBrush(Color.Blue), new RectangleF(
                        this._headers[0].IsCount ? this._rect.X + this._headers[0].Width + 2 : this._rect.X,
                        ypos,
                        width,
                        singleY)
                        );
                    isSelected = true;
                }

                this._isCurrentPageItemSelected = isSelected;

                //绘制行号
                if (this._headers[0].IsCount)
                {
                    if (this._headers[0].IsCountUp)
                    {
                        dcGs.DrawString(
                            (i + this.currentPage * this._rowCount).ToString(),
                            this._headers[0].HeaderFont,
                            this._headers[0].TextBrush,
                            new RectangleF(
                                this._rect.X,
                                ypos + 2,
                                this._headers[0].Width,
                                singleY
                                ),
                                this._headers[0].SF_Header
                                );
                    }
                    else
                    {
                        dcGs.DrawString(
                            i.ToString(),
                            this._headers[0].HeaderFont,
                            this._headers[0].TextBrush,
                            new RectangleF(
                                this._rect.X,
                                ypos + 2,
                                this._headers[0].Width,
                                singleY
                                ),
                                this._headers[0].SF_Header
                                );
                    }
                }
                ypos += (this.HideHeader ? this._rect.Height : (this._rect.Height - this._headers[0].Height)) / this._rowCount;
            }
        }

        /// <summary>
        /// 绘制表格数据
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Data(Graphics dcGs)
        {
            int pages = this._dataList.Count / this._rowCount;//总页数
            if (pages > this.currentPage)//总页数大于当前页数，当前显示列表取到当前页的列表（从当前页第一个数据开始，取行数的个数）
                this._currentDataList = this._dataList.GetRange(this.firstShowDataIndexInData, this._rowCount);
            else //总页数==当前页数，当前显示列表取最后一页数据列表（从当前页第一个数据开始，到最后一个数据）
                this._currentDataList = this._dataList.GetRange(this.firstShowDataIndexInData, this._dataList.Count - this.currentPage * this._rowCount);

            float ypos = this.HideHeader ? this._rect.Y : (this._rect.Y + this._headers[0].Height);//第一行Y坐标为列表Y坐标加列表头高度
            float singleY = (this.HideHeader ? this._rect.Height : (this._rect.Height - this._headers[0].Height)) / this.RowCount;

            for (int i = 0; i <= this._currentDataList.Count - 1; i++)//显示当前列表数据
            {
                float xpos = this._headers[0].IsCount ? this._rect.X + this._headers[0].Width + 5 : this._rect.X;//为了数据显示居中，X坐标需调整加5个像素

                for (int j = this._headers[0].IsCount ? 1 : 0; j < this._headers.Length; j++)//显示每列数据
                {
                    if (this._headers[j].IsHandleTime)
                    {
                        //根据属性名称以及所在对象反射获取数据
                        string data = this._currentDataList[i].GetType().GetProperty(this._headers[j].TProperty).GetValue(this._currentDataList[i], null).ToString();
                        string[] strs = data.Split('\n');
                        dcGs.FillRectangle(
                            new SolidBrush(Color.Black),
                            new RectangleF(xpos - 5, ypos + 2,
                                this._headers[j].Width - 30,
                                singleY / 2 - 3)
                                );
                        //显示数据
                        dcGs.DrawString(
                            strs[0],
                            this._headers[j].DataFont,
                            this._headers[j].TextBrush,
                            new RectangleF(
                                xpos - 5,
                                ypos + 2,
                                this._headers[j].Width - 30,
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
                                    this._headers[j].Width - 30,
                                    singleY / 2 - 3
                                    )
                                );
                            //显示数据
                            dcGs.DrawString(
                                strs[1],
                                this._headers[j].DataFont,
                                this._headers[j].TextBrush,
                                new RectangleF(
                                    xpos - 5,
                                    ypos + singleY / 2 + 1,
                                    this._headers[j].Width - 30,
                                    singleY / 2 - 3
                                    ),
                                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                                );
                        }
                        xpos += this._headers[j].Width + 2;//为了数据垂直方向显示居中，Y坐标需调整加2个像素
                    }
                    else
                    {
                        //根据属性名称以及所在对象反射获取数据
                        string data = this._currentDataList[i].GetType().GetProperty(this._headers[j].TProperty).GetValue(this._currentDataList[i], null).ToString();
                        //显示数据
                        dcGs.DrawString(
                            data,
                            this._headers[j].DataFont,
                            this._headers[j].TextBrush,
                            new RectangleF(
                                xpos,
                                ypos + 2,
                                this._headers[j].Width,
                                singleY),
                            this._headers[j].SF_Data
                            );
                        xpos += this._headers[j].Width + 2;//为了数据垂直方向显示居中，Y坐标需调整加2个像素
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
            if (this.indexInData == this._dataList.Count - 1)
                return;

            if (this._selectedIndex == this._currentDataList.Count - 1)
            {
                this._currentDataList.RemoveAt(0);
                this._currentDataList.Add(this._dataList[this.indexInData]);
                this.firstShowDataIndexInData++;
            }
            else if (this._selectedIndex < this._currentDataList.Count)
                this._selectedIndex++;

            this.indexInData++;
        }

        private int indexInData = 0;//用于上下移动时在顶端与低端时用于实现数据的上下移动
        private int firstShowDataIndexInData = 0;//用于翻页时确定需要显示的数据在总数据列表中的索引

        /// <summary>
        /// 向上选择项
        /// </summary>
        public void LastItem()
        {
            if (this.indexInData == 0)
                return;

            if (this._selectedIndex == 0)
            {
                this._currentDataList.RemoveAt(this._currentDataList.Count - 1);
                this._currentDataList.Add(this._dataList[indexInData]);
                this.firstShowDataIndexInData--;
            }
            else if (this._selectedIndex > 0)
                this._selectedIndex--;

            this.indexInData--;
        }

        /// <summary>
        /// 向下翻页
        /// </summary>
        public void NextPage()
        {
            int pages = this._dataList.Count / this._rowCount;
            if (this.currentPage == pages)
                return;

            this.currentPage++;
            this.firstShowDataIndexInData += this._rowCount;
        }

        /// <summary>
        /// 向上翻页
        /// </summary>
        public void LastPage()
        {
            int pages = this._dataList.Count / this._rowCount;
            if (this.currentPage == 0)
                return;

            this.currentPage--;
            if (this.currentPage == 0)
            {
                this.firstShowDataIndexInData = 0;
            }
            else this.firstShowDataIndexInData -= this._rowCount;
        }

        public void Dispose()
        {
        }
    }
}
