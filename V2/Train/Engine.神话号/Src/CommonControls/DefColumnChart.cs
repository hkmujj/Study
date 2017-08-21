using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace CommonControls
{
    public partial class DefColumnChart : UserControl
    {
        #region 纵轴
        [Description("纵轴区域"), Category("纵轴属性")]
        [Browsable(true)]
        public Rectangle AxisRect { get; set; }

        [Description("单元格的实际值"), Category("纵轴属性")]
        [Browsable(true)]
        public float SingleData { get; set; }

        [Description("单元格的高度"), Category("纵轴属性")]
        [Browsable(true)]
        public float SingleLength { get; set; }

        [Description("基数的格数"), Category("纵轴属性")]
        [Browsable(true)]
        public float BaseNumberCount { get; set; }

        [Description("大单元格的的格数"), Category("纵轴属性")]
        [Browsable(true)]
        public float BigSingleCount { get; set; }

        [Description("单位"), Category("纵轴属性")]
        [Browsable(true)]
        public string Unit { get; set; }
        #endregion

        #region 柱形
        [Description("柱形数量"), Category("柱形属性")]
        [Browsable(true)]
        public int ColumniationCount
        {
            get { return _columniationCount; }
            set
            {
                _columniationCount = value;

                setColumniation(value);
            }
        }
        private int _columniationCount = 0;

        [Description("柱形尺寸"), Category("柱形属性")]
        [Browsable(true)]
        public Size ColumniationSize
        {
            get { return _columnSize; }
            set
            {
                if (_columnSize == value) return;
                _columnSize = value;
                Invalidate();
            }
        }

        private Size _columnSize = Size.Empty;

        [Description("柱形颜色"), Category("柱形属性")]
        [Browsable(true)]
        public Color ColumniationColor
        {
            get { return _columniationColor; }
            set
            {
                if (_columniationColor == value) return;
                _columniationColor = value;
                _columniationBrush.Color=value;
            }
        }
        private Color _columniationColor = Color.White;

        private SolidBrush _columniationBrush = (SolidBrush)Brushes.White;

        [Description("指针"), Category("柱形属性")]
        [Browsable(true)]
        public Image Pointer { get; set; }

        [Description("第一个柱距离纵轴的距离"), Category("柱形属性")]
        [Browsable(true)]
        public float FirstDistanceAxis { get; set; }

        [Description("柱形之间的距离"), Category("柱形属性")]
        [Browsable(true)]
        public float DistanceColumniation { get; set; }

        #endregion

        [Description("数据"), Category("自定义属性")]
        [Browsable(true)]
        public Int32[] Datas
        {
            get { return _datas; }
            set
            {
                if (_datas == value) return;
                _datas = value;
            }

        }
        private Int32[] _datas = new Int32[4];

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("柱形图配置"), Category("自定义属性")]
        [Browsable(true)]
        public List<ColumnConfigInfo> ColumnConfigInfos
        {
            get { return _columnConfigInfos; }
            set
            {
                _columnConfigInfos = value;
                Invalidate();
            }
        }
        private List<ColumnConfigInfo> _columnConfigInfos = new List<ColumnConfigInfo>();

        private List<Columniation> _columniations = new List<Columniation>();
        private bool _isFirstSet = true;

        private bool _isNeedInvalidate = false;

        public DefColumnChart()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            _columniationBrush = new SolidBrush(_columniationColor);
        }

        public void SetValue(Int32 logicID, float value)
        {
            if (_columnConfigInfos == null || _columnConfigInfos.Count == 0) return;

            _columnConfigInfos.ForEach(a =>
            {
                _isNeedInvalidate = a.SetValue(logicID, value);
                if (_isNeedInvalidate) Invalidate();
            });
        }

        public void SetValue(Int32 logicID, bool value)
        {
            if (_columnConfigInfos == null || _columnConfigInfos.Count == 0) return;

            _columnConfigInfos.ForEach(a =>
            {
                _isNeedInvalidate = a.SetValue(logicID, value);
                if (_isNeedInvalidate) Invalidate();
            });
        }

        protected override void NotifyInvalidate(Rectangle invalidatedArea)
        {
            if (_isNeedInvalidate)
            {
                base.NotifyInvalidate(invalidatedArea);
                _isNeedInvalidate = false;
            }
        }

        /// <summary>
        /// 实现柱形图
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_isFirstSet)
            {
                setColumniation(_columniationCount);
                _isFirstSet = false;
            }
            paint_VerticalAxis(e.Graphics);

            paint_Columniation_(e.Graphics);
        }

        /// <summary>
        /// 绘制刻度
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_VerticalAxis(Graphics dcGs)
        {
            int heightCount = (int)(AxisRect.Height / SingleLength + 1);
            for (int i = 0; i < heightCount; i++)
            {
                //刻度
                int interval1 = i % BigSingleCount == 0 ? 0 : 5;
                dcGs.DrawLine(
                    Pens.White,
                    new PointF(AxisRect.X + AxisRect.Width - 12 + interval1, AxisRect.Y + AxisRect.Height - SingleLength * i),
                    new PointF(AxisRect.X + AxisRect.Width, AxisRect.Y + AxisRect.Height - SingleLength * i)
                    );

                //基数
                if (i % BaseNumberCount == 0)
                {
                    float temp = i == 0 ? 0.5F : 0;
                    dcGs.DrawString(
                        (i * SingleData).ToString(),
                        StaticProperty.Font12,
                        Brushes.White,
                        new RectangleF(
                            AxisRect.X - 2,
                            AxisRect.Y + AxisRect.Height - SingleLength * (i + 1 + temp),
                            AxisRect.Width - 12 + 4,
                            SingleLength * 2
                            ),
                        StaticProperty.SfRightCenter
                        );
                }
            }

            //绘制竖线
            dcGs.DrawLine(
                Pens.White,
                new PointF(AxisRect.X + AxisRect.Width, AxisRect.Y),
                new PointF(AxisRect.X + AxisRect.Width, AxisRect.Y + AxisRect.Height)
                );

            //绘制单位
            int temp1 = (int)(heightCount / BigSingleCount);
            if (Unit == null) return;
            dcGs.DrawString(
                Unit,
                StaticProperty.Font10,
                Brushes.White,
                new RectangleF(
                    AxisRect.X - 2,
                    AxisRect.Y + AxisRect.Height - SingleLength * (temp1 * BigSingleCount),
                    AxisRect.Width - 12 + 4 + 8,
                    SingleLength * BigSingleCount
                    ),
                StaticProperty.SfCenter
                );
        }

        private void paint_Columniation_(Graphics g)
        {
            if (_columnConfigInfos == null || _columnConfigInfos.Count == 0)
                return;

            _columnConfigInfos.ForEach(a => a.Paint(g, SingleLength / SingleData));
        }

        /// <summary>
        /// 设置柱形
        /// </summary>
        /// <param name="count">数目</param>
        private void setColumniation(Int32 count)
        {
            _columniations = new List<Columniation>();
            RectangleF rect = new RectangleF(
                AxisRect.X + AxisRect.Width + FirstDistanceAxis,
                AxisRect.Y + AxisRect.Height - ColumniationSize.Height,
                ColumniationSize.Width,
                ColumniationSize.Height
                );
            if (count == 0) return;
            for (int i = 0; i < count; i++)
            {
                Columniation c = new Columniation(
                    i,
                    new RectangleF(rect.X + (ColumniationSize.Width + DistanceColumniation) * i, rect.Y, rect.Width, rect.Height),
                    _columniationBrush
                    );
                _columniations.Add(c);
            }
        }

        /// <summary>
        /// 绘制柱形图
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Columniation(Graphics dcGs)
        {
            foreach (var item in this._columniations)
            {
                //绘制柱形
                dcGs.DrawRectangle(Pens.White, (int)item.Rect.X, (int)item.Rect.Y, (int)item.Rect.Width, (int)item.Rect.Height);

                float chartHeight = (item.Data / SingleData) * SingleLength - 1 > item.Rect.Height
                    ? item.Rect.Height - 1 : (item.Data / SingleData) * SingleLength - 1;
                float y = (item.Data / SingleData) * SingleLength - 1 > item.Rect.Height
                    ? item.Rect.Y + 1 : item.Rect.Y + item.Rect.Height - (item.Data / SingleData) * SingleLength;
                dcGs.FillRectangle(
                    item.Color,
                    new RectangleF(
                        item.Rect.X + 2,
                        y,
                        item.Rect.Width - 2,
                        chartHeight
                        )
                    );

                dcGs.DrawString(
                        (item.Data).ToString("0.0"),
                        StaticProperty.Font11,
                        Brushes.White,
                        new RectangleF(
                            item.Rect.X,
                            item.Rect.Y - 25 + 2,
                            item.Rect.Width,
                            25
                            ),
                        StaticProperty.SfCenter
                        );

                //绘制指针
                if (item.Pointer == null) continue;
                dcGs.DrawImage(
                    item.Pointer,
                    new RectangleF(
                        item.Rect.X + 2,
                        item.Rect.Y + item.Rect.Height - (item.Data / SingleData) * SingleLength - item.Pointer.Height / 2,
                        item.Pointer.Width,
                        item.Pointer.Height
                        )
                    );
            }
        }
    }

    /// <summary>
    /// 柱形
    /// </summary>
    public class Columniation
    {
        public int ID { get; private set; }

        public RectangleF Rect { get; private set; }

        public Brush Color { get; private set; }

        public float Data { get; set; }

        public Image Pointer { get; private set; }

        public Columniation(int id, RectangleF rect, Brush color, Image pointer = null)
        {
            ID = id;
            Rect = rect;
            Color = color;
            Pointer = pointer;
        }
    }

}
