using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Common.Train
{
    public class TrainvView : CommonInnerControlBase
    {
        private List<Car> m_Cars;

        /// <summary>
        /// 形状
        /// </summary>
        private GraphicsPath m_ViewPath;


        private Point[] m_Point1;
        private Point[] m_Point2;

        // ReSharper disable once InconsistentNaming
        protected readonly SolidBrush m_Blue1Brush = new SolidBrush(Color.FromArgb(48, 128, 168)); //较深蓝色画刷 用于绘制车身

        public string TrainName
        {
            set { m_TrainName.Text = value; }
            get { return m_TrainName.Text; }
        }

        private GDIRectText m_TrainName;

        public EventHandler SelectedChanged { get; set; }

        public EventHandler FaultChanged { set; get; }

        public bool IsAnyFault { private set; get; }

        public static readonly Size DefaultSize;

        static TrainvView()
        {
            DefaultSize = new Size(Car.DefaultSize.Width * GlobalParam.CarCount + 30 * 2, 60);
        }

        public TrainvView()
        {
            Init();

            RefreshAction = o =>
                            {
                                var anyFault = false;
                                for (int i = 0; i < TrainConfig.TrainNames.Count; i++)
                                {
                                    anyFault |= CommonTrainInBoolRes.Instance.IsFault(i);
                                }
                                if (anyFault != IsAnyFault)
                                {
                                    IsAnyFault = anyFault;
                                    HandleUtil.OnHandle(FaultChanged, this, null);
                                }
                                // 没有任何故障
                                if (!anyFault)
                                {
                                    return;
                                }
                            };

            OutLineChanged += OnOutLineChanged;
        }

        public ReadOnlyCollection<bool> GetSelectedState() { return m_Cars.Select(s => s.IsSelected).ToList().AsReadOnly(); }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            m_OutLineRectangle.Size = DefaultSize;

            Translate(LocationOffset);

        }

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="idx"></param>
        public void SelectOfIndex(int idx)
        {
            Debug.Assert(idx < TrainConfig.RoomCount);
            var old = GetSelectedState();
            m_Cars[idx].IsSelected = true;
            HandleUtil.OnHandle(SelectedChanged, this,
                new TrainSelectedChangedArgs(TrainSelectedChangedArgs.ChangedFrom.Manaul, old, GetSelectedState()));
        }

        public void SelectOfIndex(IEnumerable<int> idxs)
        {
            SetSelectOfIndex(idxs, true);
        }

        public void DeselectOfIndex(IEnumerable<int> idxs)
        {
            SetSelectOfIndex(idxs, false);
        }

        private void SetSelectOfIndex(IEnumerable<int> idxs, bool isSelect)
        {
            var old = GetSelectedState();

            foreach (var idx in idxs)
            {
                m_Cars[idx].IsSelected = isSelect;
            }
            HandleUtil.OnHandle(SelectedChanged, this, new TrainSelectedChangedArgs(TrainSelectedChangedArgs.ChangedFrom.Manaul, old, GetSelectedState()));
        }

        public void SelectAll()
        {
            SetSelectAll(true);
        }

        public void DeselectAll()
        {
            SetSelectAll(false);
        }

        private void SetSelectAll(bool b)
        {
            var old = GetSelectedState();
            
            m_Cars.ForEach(e => e.IsSelected = b);

            HandleUtil.OnHandle(SelectedChanged, this, new TrainSelectedChangedArgs(TrainSelectedChangedArgs.ChangedFrom.Manaul, old, GetSelectedState()));
        }

        public override void Translate(float offsetX, float offsetY)
        {
            m_Cars.ForEach(e =>
                           {
                               e.OutLineRectangle.Offset((int) offsetX, (int) offsetY);
                               e.Translate(offsetX, offsetY);
                           });

            var mat = new Matrix();
            mat.Translate(offsetX, offsetY);

            m_ViewPath.Transform(mat);
            m_TrainName.Translate(offsetX, offsetY);
        }


        public override void Init()
        {

            m_Point1 = new Point[9];
            m_Point2 = new Point[9];

            var location = new Point(0, 15);
            m_Cars = new List<Car>()
            {
                new HeadCar(0, HeadCarType.Head) {Location = new Point(location.X  , location.Y)},
            
            };
            location += new Size(m_Cars[0].Size.Width, 0);
            for (int i = 1; i < 7; i++)
            {
                m_Cars.Add(new Car(i) { Location = location });
                location += new Size(Car.DefaultSize.Width, 0);
            }
            m_Cars.Add(new HeadCar(7, HeadCarType.Tail) { Location = location });

            //个点坐标初始化
            m_Point1[0] = new Point(location.X + 4, location.Y + 15);
            m_Point1[8] = new Point(location.X + 331, location.Y + 15);
            for (int i = 1; i < 8; i++)
            {
                m_Point1[i] = new Point(location.X + 42 * i, location.Y + 15);
            }
            for (int i = 0; i < 7; i++)
            {
                m_Point2[i] = new Point(location.X + 42 * (i + 1), location.Y + 57);
            }
            location = Point.Empty;
            // 列车形状设置
            m_ViewPath = new GraphicsPath();
            m_ViewPath.AddArc(location.X, location.Y, 30, 60, 90, 180);
            m_ViewPath.AddLine(location.X + 15, location.Y, location.X + 320, location.Y);
            m_ViewPath.AddArc(location.X + 305, location.Y, 30, 60, 270, 180);
            m_ViewPath.AddLine(location.X + 15, location.Y + 60, location.X + 320, location.Y + 60);

            m_TrainName = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(20, 1, 300, 12),
                BackColorVisible = true,
                BkColor = m_Blue1Brush.Color,
                Text = "CRH1A",
                TextFormat = new StringFormat(){ Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center},
                NeedDarwOutline = false,
                
            };
            m_TrainName.DrawFont = new Font(m_TrainName.DrawFont.FontFamily, 10, FontStyle.Bold);
        }

        public override void OnDraw(Graphics g)
        {
            g.FillPath(m_Blue1Brush, m_ViewPath);

            m_Cars.ForEach(e => e.OnPaint(g));

            m_TrainName.OnDraw(g);
        }

        public override bool OnMouseDown(Point point)
        {
            foreach (var car in m_Cars)
            {
                if (car.OutLineRectangle.Contains(point))
                {
                    var old = m_Cars.Select(s => s.IsSelected).ToList().AsReadOnly();

                    m_Cars.ForEach(e => e.IsSelected = false);

                    car.IsSelected = true;

                    HandleUtil.OnHandle(SelectedChanged, this,
                        new TrainSelectedChangedArgs(TrainSelectedChangedArgs.ChangedFrom.UiClick, old,
                            m_Cars.Select(s => s.IsSelected).ToList().AsReadOnly()));

                    return true;
                }
            }
            return false;
        }

        public override void Reverse()
        {
            var bound = m_ViewPath.GetBounds();

            var mat = MatrixHelper.CreateTurnMatrix(bound.GetCenterPoint().X - Car.DefaultSize.Width/2, TurnOrientation.Horizontal);

            m_Cars.ForEach(e =>
                           {
                               e.Location = mat.TransformPoint(e.Location);
                               e.Reverse();
                           });


        }
    }
}
