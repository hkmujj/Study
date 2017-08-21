using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ES.Facility.Common.Control.Common;

namespace ES.Facility.Common.Control
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
        private Int32 currentPage = 0;
        private List<T> _currentDataList = new List<T>();

        #region IControl接口属性
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public String Text
        {
            get { return this._text; }
            set { this._text = value; }
        }
        private String _text = String.Empty;

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public Int32 ID
        {
            get { return this._id; }
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
        public Boolean IsFocus
        {
            get { return this._isFocus; }
            set
            {
                if (this._isFocus == value)
                    return;

                this._isFocus = value;
            }
        }
        private Boolean _isFocus = false;
        #endregion

        /// <summary>
        /// 读取或设置数据列表属性
        /// </summary>
        public List<T> DataList
        {
            get { return this._dataList; }
            set { _dataList = value; }
        }
        private List<T> _dataList = new List<T>();

        /// <summary>
        /// 读取或设置选中行索引值属性
        /// </summary>
        public Int32 SelectedIndex
        {
            get { return this._selectedIndex; }
            set { this._selectedIndex = value; }
        }
        private Int32 _selectedIndex = -1;

        /// <summary>
        /// 读取当前显示页面是否包含选中行属性
        /// </summary>
        public Boolean IsCurrentPageItemSelected
        {
            get { return this._isCurrentPageItemSelected; }
        }
        private Boolean _isCurrentPageItemSelected = false;

        /// <summary>
        /// 读取或设置显示数据的行数属性
        /// </summary>
        public Int32 RowCount
        {
            get { return this._rowCount; }
            set { this._rowCount = value; }
        }
        private Int32 _rowCount = 1;

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

        public void MouseDown(Point nPoint)
        {
            if (!_rect.Contains(nPoint)) return;

        }

        public void MouseUp(Point nPoint)
        {
            if (!_rect.Contains(nPoint)) return;

            for (int i = 0; i < _currentDataList.Count; i++)
            {
                RectangleF r = new RectangleF(
                    this._rect.X,
                    this._rect.Y + this._headers[0].Height + (this._rect.Height - this._headers[0].Height) / 10*i,
                    this._rect.Width,
                    (this._rect.Height - this._headers[0].Height) / 10
                    );
                if (r.Contains(nPoint))
                {
                    SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 绘制ListBox
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            this.paint_Header(dcGs);
            this.paint_Data(dcGs);
        }

        /// <summary>
        /// 绘制列表头
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Header(Graphics dcGs)
        {
            //ListBox标题栏绘制（文字与）
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

            Boolean isSelected = false;
            float ypos = this._rect.Y + this._headers[0].Height;
            for (int i = 1; i <= this._rowCount; i++)
            {
                //绘制选中行高亮显示
                if (i + this.currentPage * this._rowCount - 1 == SelectedIndex)
                {
                    dcGs.FillRectangle(new SolidBrush(Color.Blue), new RectangleF(
                        this._rect.X + this._headers[0].Width + 2,
                        ypos,
                        this._headers[1].Width + 2 + this._headers[2].Width + 2 + this._headers[3].Width,
                        (this._rect.Height - this._headers[0].Height) / 10)
                        );
                    isSelected = true;
                }

                this._isCurrentPageItemSelected = isSelected;

                //绘制行号
                dcGs.DrawString((i + this.currentPage * this._rowCount).ToString(), this._headers[0].HeaderFont, this._headers[0].TextBrush, new RectangleF(this._rect.X, ypos + 2, this._headers[0].Width, (this._rect.Height - this._headers[0].Height) / this._rowCount), this._headers[0].SF_Header);
                ypos += (this._rect.Height - this._headers[0].Height) / 10;
            }
        }

        /// <summary>
        /// 绘制表格数据
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Data(Graphics dcGs)
        {
            Int32 pages = this._dataList.Count / this._rowCount;//总页数
            if (pages > this.currentPage)//总页数大于当前页数，当前显示列表取到当前页的列表（从当前页第一个数据开始，取行数的个数）
                this._currentDataList = this._dataList.GetRange(this.currentPage * this._rowCount, this._rowCount);
            else //总页数==当前页数，当前显示列表取最后一页数据列表（从当前页第一个数据开始，到最后一个数据）
                this._currentDataList = this._dataList.GetRange(this.currentPage * this._rowCount, this._dataList.Count - this.currentPage * this._rowCount);

            float ypos = this._rect.Y + this._headers[0].Height;//第一行Y坐标为列表Y坐标加列表头高度
            for (int i = 0; i <= this._currentDataList.Count - 1; i++)//显示当前列表数据
            {
                float xpos = this._rect.X + this._headers[0].Width + 5;//为了数据显示居中，X坐标需调整加5个像素

                for (int j = 1; j < this._headers.Length; j++)//显示每列数据
                {
                    //根据属性名称以及所在对象反射获取数据
                    string data = this._currentDataList[i].GetType().GetProperty(this._headers[j].TProperty).GetValue(this._currentDataList[i], null).ToString();
                    //显示数据
                    dcGs.DrawString(
                        data, 
                        this._headers[j].DataFont,
                        this._headers[j].TextBrush, 
                        new RectangleF(xpos, ypos + 2, this._headers[j].Width, (this._rect.Height - this._headers[0].Height) / this._rowCount),
                        this._headers[j].SF_Data
                        );
                    xpos += this._headers[j].Width + 2;//为了数据垂直方向显示居中，Y坐标需调整加2个像素
                }
                ypos += (this._rect.Height - this._headers[0].Height) / 10;//递加每行高度
            }
        }

        /// <summary>
        /// 向下翻页
        /// </summary>
        public void NextPage()
        {
            Int32 pages = this._dataList.Count / this._rowCount;
            if (this.currentPage == pages)
                return;

            this.currentPage++;
        }

        /// <summary>
        /// 向上翻页
        /// </summary>
        public void LastPage()
        {
            if (this.currentPage == 0)
                return;

            this.currentPage--;
        }

        public void Dispose()
        { 
        }
    }
}
