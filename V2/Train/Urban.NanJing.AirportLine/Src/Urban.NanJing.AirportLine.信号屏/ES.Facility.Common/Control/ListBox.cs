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
        public String Property { get; set; }
        /// <summary>
        /// 读取或设置Header文本对齐方式属性
        /// </summary>
        public StringFormat SfHeader { get; set; }
        /// <summary>
        /// 读取或设置Header对应列数据文本对齐属性
        /// </summary>
        public StringFormat SfData { get; set; }
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
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    /// <typeparam name="T">Item类型</typeparam>
    public class ListBox<T> : IControl, IDisposable
    {
        private ListBoxHeader[] m_Headers;
        private Int32 m_CurrentPage = 0;
        private List<T> m_CurrentDataList = new List<T>();

        #region IControl接口属性
        /// <summary>
        /// 读取或设置按钮文本属性
        /// </summary>
        public String Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        private String m_Text = String.Empty;

        /// <summary>
        /// 读取按钮标识ID属性
        /// </summary>
        public Int32 ID
        {
            get { return m_ID; }
        }
        private Int32 m_ID = -1;

        /// <summary>
        /// 读取按钮Style属性（使用时需转换为ButtonStyle）
        /// </summary>
        public IStyle Style
        {
            get { return m_Style; }
        }
        private IStyle m_Style;

        /// <summary>
        /// 读取或设置按钮所在矩形区域属性
        /// </summary>
        public RectangleF Rect
        {
            get { return m_Rect; }
            set
            {
                if (m_Rect == value)
                    return;

                m_Rect = value;
            }
        }
        private RectangleF m_Rect;

        /// <summary>
        /// 读取或设置是否获得焦点属性（功能后续添加）
        /// </summary>
        public Boolean IsFocus
        {
            get { return m_IsFocus; }
            set
            {
                if (m_IsFocus == value)
                    return;

                m_IsFocus = value;
            }
        }
        private Boolean m_IsFocus = false;
        #endregion

        /// <summary>
        /// 读取或设置数据列表属性
        /// </summary>
        public List<T> DataList
        {
            get { return m_DataList; }
            set
            {
                if (m_DataList == value)
                    return;

                m_DataList = value;
                m_FirstShowDataIndexInData = 0;
                m_IndexInData = 0;
                SelectedIndex = 0;
                m_CurrentPage = 0;
            }
        }
        private List<T> m_DataList = new List<T>();

        /// <summary>
        /// 读取或设置选中行索引值属性（不能用该属性在数据列表中索引项）
        /// </summary>
        public Int32 SelectedIndex
        {
            get { return m_SelectedIndex; }
            set { m_SelectedIndex = value; }
        }
        private Int32 m_SelectedIndex = -1;

        /// <summary>
        /// 读取当前选择项属性
        /// </summary>
        public T SelectedItem
        {
            get { return m_CurrentDataList[m_SelectedIndex]; }
        }

        /// <summary>
        /// 读取当前显示页面是否包含选中行属性
        /// </summary>
        public Boolean IsCurrentPageItemSelected
        {
            get { return m_IsCurrentPageItemSelected; }
        }
        private Boolean m_IsCurrentPageItemSelected = false;

        /// <summary>
        /// 读取或设置显示数据的行数属性
        /// </summary>
        public Int32 RowCount
        {
            get { return m_RowCount; }
            set { m_RowCount = value; }
        }
        private Int32 m_RowCount = 1;

        /// <summary>
        /// 读取或设置背景画刷属性（初始为透明）
        /// </summary>
        public Brush BackgroundBrush
        {
            get { return m_BackgroundBrush; }
            set
            {
                if (m_BackgroundBrush == value)
                    return;

                m_BackgroundBrush = value;
            }
        }
        private Brush m_BackgroundBrush = new SolidBrush(Color.Transparent);

        /// <summary>
        /// 读取或设置网格线画刷属性（初始为透明）
        /// </summary>
        public Brush GridBrush
        {
            get { return m_GridBrush; }
            set
            {
                if (m_GridBrush == value)
                    return;

                m_GridBrush = value;
            }
        }
        private Brush m_GridBrush = new SolidBrush(Color.Transparent);

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
            m_Rect = rect;
            m_Headers = header;
            m_DataList = dataList;
        }

        /// <summary>
        /// 绘制ListBox
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            paint_Background_Grid(g);
            paint_Header(g);
            paint_Data(g);


            IsFinalItem = (m_IndexInData == m_DataList.Count - 1);
            IsFirsItem = (m_IndexInData == 0);
        }

        /// <summary>
        /// 绘制页码信息
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        public void Paint_PageInfo(Graphics g, RectangleF rect)
        {
            g.FillRectangle(Brushes.White, rect);

            g.DrawRectangle(new Pen(Brushes.Black, 2), rect.X, rect.Y, rect.Width, rect.Height);

            g.DrawString(((m_FirstShowDataIndexInData+m_CurrentDataList.Count-1)/RowCount+1).ToString() + "/" + (m_DataList.Count / m_RowCount+1).ToString(),
                new Font("宋体", 11, FontStyle.Bold),
                Brushes.Black,
                rect,
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

        }

        /// <summary>
        /// 绘制表格
        /// </summary>
        /// <param name="g"></param>
        private void paint_Background_Grid(Graphics g)
        {
            g.FillRectangle(m_BackgroundBrush, m_Rect);

            if (HideHeader)//隐藏表头
            {
                //横线
                for (int i = 0; i < RowCount + 1; i++)
                {
                    g.DrawLine(
                        new Pen(m_GridBrush, 2),
                        new PointF(m_Rect.X, m_Rect.Y + (m_Rect.Height / RowCount) * i),
                        new PointF(m_Rect.X + m_Rect.Width, m_Rect.Y + (m_Rect.Height / RowCount) * i)
                        );
                }
                //丛线
                float xpos = m_Rect.X;
                g.DrawLine(
                         new Pen(m_GridBrush, 2),
                         new PointF(xpos, m_Rect.Y),
                         new PointF(xpos, m_Rect.Y + m_Rect.Height)
                         );
                xpos -= 1;
                for (int i = 0; i < m_Headers.Length; i++)
                {
                    xpos += m_Headers[i].Width + 2;
                    g.DrawLine(
                        new Pen(m_GridBrush, 2),
                        new PointF(xpos, m_Rect.Y),
                        new PointF(xpos, m_Rect.Y + m_Rect.Height)
                        );
                }
            }
            else
            {
                //横线
                g.DrawLine(
                    new Pen(m_GridBrush, 2),
                    new PointF(m_Rect.X, m_Rect.Y),
                    new PointF(m_Rect.X + m_Rect.Width, m_Rect.Y)
                    );
                float ypos = m_Rect.Y + m_Headers[0].Height;
                for (int i = 0; i < RowCount + 1; i++)
                {
                    g.DrawLine(
                        new Pen(m_GridBrush, 2),
                        new PointF(m_Rect.X, ypos),
                        new PointF(m_Rect.X + m_Rect.Width, ypos)
                        );
                    ypos += ((m_Rect.Height - m_Headers[0].Height) / RowCount);
                }
                //丛线
                float xpos = m_Rect.X;
                g.DrawLine(new Pen(m_GridBrush, 2), new PointF(xpos, m_Rect.Y), new PointF(xpos, m_Rect.Y + m_Rect.Height));
                xpos -= 1;
                for (int i = 0; i < m_Headers.Length; i++)
                {
                    xpos += m_Headers[i].Width + 2;
                    g.DrawLine(new Pen(m_GridBrush, 2), new PointF(xpos, m_Rect.Y), new PointF(xpos, m_Rect.Y + m_Rect.Height));
                }
            }
        }

        /// <summary>
        /// 绘制列表头
        /// </summary>
        /// <param name="g"></param>
        private void paint_Header(Graphics g)
        {
            //ListBox标题栏绘制（文字与）
            if (!HideHeader)
            {
                float xpos = m_Rect.X;
                for (int i = 0; i < m_Headers.Length; i++)
                {
                    g.FillRectangle(
                        m_Headers[i].BackgroundBrush,
                        new RectangleF(xpos, m_Rect.Y, m_Headers[i].Width, m_Headers[i].Height)
                        );//绘制列表头背景颜色
                    g.DrawString(
                        m_Headers[i].Text,
                        m_Headers[i].HeaderFont,
                        m_Headers[i].TextBrush,
                        new RectangleF(xpos, m_Rect.Y + 2, m_Headers[i].Width, m_Headers[i].Height),
                        m_Headers[i].SfHeader
                        );//绘制列表头数据

                    xpos += m_Headers[i].Width + 2;
                }
            }

            Boolean isSelected = false;
            float ypos = HideHeader ? m_Rect.Y : (m_Rect.Y + m_Headers[0].Height);
            float singleY = (HideHeader ? m_Rect.Height : (m_Rect.Height - m_Headers[0].Height)) / RowCount;
            for (int i = 1; i <= m_RowCount; i++)
            {
                //绘制选中行高亮显示
                if (i + m_CurrentPage * m_RowCount - 1 == SelectedIndex)
                {
                    float width = m_Headers[0].IsCount ? 0 : m_Headers[0].Width;
                    for (int j = 1; j < m_Headers.Length; j++)
                    {
                        width += (2 + m_Headers[j].Width);
                    }
                    g.FillRectangle(new SolidBrush(Color.Blue), new RectangleF(
                        m_Headers[0].IsCount ? m_Rect.X + m_Headers[0].Width + 2 : m_Rect.X,
                        ypos,
                        width,
                        singleY)
                        );
                    isSelected = true;
                }

                m_IsCurrentPageItemSelected = isSelected;

                //绘制行号
                if (m_Headers[0].IsCount)
                {
                    if (m_Headers[0].IsCountUp)
                    {
                        g.DrawString(
                            (i + m_CurrentPage * m_RowCount).ToString(),
                            m_Headers[0].HeaderFont,
                            m_Headers[0].TextBrush,
                            new RectangleF(
                                m_Rect.X,
                                ypos + 2,
                                m_Headers[0].Width,
                                singleY
                                ),
                                m_Headers[0].SfHeader
                                );
                    }
                    else
                    {
                        g.DrawString(
                            i.ToString(),
                            m_Headers[0].HeaderFont,
                            m_Headers[0].TextBrush,
                            new RectangleF(
                                m_Rect.X,
                                ypos + 2,
                                m_Headers[0].Width,
                                singleY
                                ),
                                m_Headers[0].SfHeader
                                );
                    }
                }
                ypos += (HideHeader ? m_Rect.Height : (m_Rect.Height - m_Headers[0].Height)) / m_RowCount;
            }
        }

        /// <summary>
        /// 绘制表格数据
        /// </summary>
        /// <param name="g"></param>
        private void paint_Data(Graphics g)
        {
            Int32 pages = m_DataList.Count / m_RowCount;//总页数
            if (pages > m_CurrentPage)//总页数大于当前页数，当前显示列表取到当前页的列表（从当前页第一个数据开始，取行数的个数）
                m_CurrentDataList = m_DataList.GetRange(m_FirstShowDataIndexInData, m_RowCount);
            else //总页数==当前页数，当前显示列表取最后一页数据列表（从当前页第一个数据开始，到最后一个数据）
                m_CurrentDataList = m_DataList.GetRange(m_FirstShowDataIndexInData, m_DataList.Count - m_CurrentPage * m_RowCount);

            float ypos = HideHeader ? m_Rect.Y : (m_Rect.Y + m_Headers[0].Height);//第一行Y坐标为列表Y坐标加列表头高度
            float singleY = (HideHeader ? m_Rect.Height : (m_Rect.Height - m_Headers[0].Height)) / RowCount;

            for (int i = 0; i <= m_CurrentDataList.Count - 1; i++)//显示当前列表数据
            {
                float xpos = m_Headers[0].IsCount ? m_Rect.X + m_Headers[0].Width + 5 : m_Rect.X;//为了数据显示居中，X坐标需调整加5个像素

                for (int j = m_Headers[0].IsCount ? 1 : 0; j < m_Headers.Length; j++)//显示每列数据
                {
                    if (m_Headers[j].IsHandleTime)
                    {
                        //根据属性名称以及所在对象反射获取数据
                        string data = m_CurrentDataList[i].GetType().GetProperty(m_Headers[j].Property).GetValue(m_CurrentDataList[i], null).ToString();
                        String[] strs = data.Split('\n');
                        g.FillRectangle(
                            new SolidBrush(Color.Black),
                            new RectangleF(xpos - 5, ypos + 2,
                                m_Headers[j].Width - 30,
                                singleY / 2 - 3)
                                );
                        //显示数据
                        g.DrawString(
                            strs[0],
                            m_Headers[j].DataFont,
                            m_Headers[j].TextBrush,
                            new RectangleF(
                                xpos - 5,
                                ypos + 2,
                                m_Headers[j].Width - 30,
                                singleY / 2 - 3),
                            new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                            );
                        if (strs.Length == 2)
                        {
                            g.FillRectangle(
                                new SolidBrush(Color.Black),
                                new RectangleF(
                                    xpos - 5,
                                    ypos + singleY / 2 + 1,
                                    m_Headers[j].Width - 30,
                                    singleY / 2 - 3
                                    )
                                );
                            //显示数据
                            g.DrawString(
                                strs[1],
                                m_Headers[j].DataFont,
                                m_Headers[j].TextBrush,
                                new RectangleF(
                                    xpos - 5,
                                    ypos + singleY / 2 + 1,
                                    m_Headers[j].Width - 30,
                                    singleY / 2 - 3
                                    ),
                                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                                );
                        }
                        xpos += m_Headers[j].Width + 2;//为了数据垂直方向显示居中，Y坐标需调整加2个像素
                    }
                    else
                    {
                        //根据属性名称以及所在对象反射获取数据
                        string data = m_CurrentDataList[i].GetType().GetProperty(m_Headers[j].Property).GetValue(m_CurrentDataList[i], null).ToString();
                        //显示数据
                        g.DrawString(
                            data,
                            m_Headers[j].DataFont,
                            m_Headers[j].TextBrush,
                            new RectangleF(
                                xpos,
                                ypos + 2,
                                m_Headers[j].Width,
                                singleY),
                            m_Headers[j].SfData
                            );
                        xpos += m_Headers[j].Width + 2;//为了数据垂直方向显示居中，Y坐标需调整加2个像素
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
            if (m_IndexInData == m_DataList.Count - 1)
                return;

            if (m_SelectedIndex == m_CurrentDataList.Count - 1)
            {
                m_CurrentDataList.RemoveAt(0);
                m_CurrentDataList.Add(m_DataList[m_IndexInData]);
                m_FirstShowDataIndexInData++;
            }
            else if (m_SelectedIndex < m_CurrentDataList.Count)
                m_SelectedIndex++;

            m_IndexInData++;
        }

        private Int32 m_IndexInData = 0;//用于上下移动时在顶端与低端时用于实现数据的上下移动
        private Int32 m_FirstShowDataIndexInData = 0;//用于翻页时确定需要显示的数据在总数据列表中的索引

        /// <summary>
        /// 向上选择项
        /// </summary>
        public void LastItem()
        {
            if (m_IndexInData == 0)
                return;

            if (m_SelectedIndex == 0)
            {
                m_CurrentDataList.RemoveAt(m_CurrentDataList.Count - 1);
                m_CurrentDataList.Add(m_DataList[m_IndexInData]);
                m_FirstShowDataIndexInData--;
            }
            else if (m_SelectedIndex > 0)
                m_SelectedIndex--;

            m_IndexInData--;
        }

        /// <summary>
        /// 向下翻页
        /// </summary>
        public void NextPage()
        {
            Int32 pages = m_DataList.Count / m_RowCount;
            if (m_CurrentPage == pages)
                return;

            m_CurrentPage++;
            m_FirstShowDataIndexInData += m_RowCount;
        }

        /// <summary>
        /// 向上翻页
        /// </summary>
        public void LastPage()
        {
            Int32 pages = m_DataList.Count / m_RowCount;
            if (m_CurrentPage == 0)
                return;

            m_CurrentPage--;
            if (m_CurrentPage == 0)
            {
                m_FirstShowDataIndexInData = 0;
            }
            else m_FirstShowDataIndexInData -= m_RowCount;
        }

        public void Dispose()
        {
        }
    }
}
