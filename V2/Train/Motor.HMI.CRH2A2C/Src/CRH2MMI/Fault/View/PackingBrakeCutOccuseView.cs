using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Fault.View
{
    /// <summary>
    /// 停放制动隔离发生
    /// </summary>
    internal class PackingBrakeCutOccuseView : CommonInnerControlBase
    {
        public event Action ConfirmEvent;

        private TrainView m_UpTrainView;

        private List<GDIRectText> m_ConstInfos;

        private CRH2Button m_ConfirmButton;

        /// <summary>
        /// 此页面默认btn大小 
        /// </summary>
        public static readonly Size DefaultBtnSize = new Size(120, 38);

        public static readonly Size DefaultTextSizeRed = new Size(130, 20);

        public static readonly Size DefaultTextSizeYellow = new Size(500, 20);

        // ReSharper disable once InconsistentNaming
        protected Font m_DefaultTextFont = new Font(CRH2Resource.Font12.FontFamily, 13, FontStyle.Bold);
        private bool m_HasOccuse;

        public bool HasOccuse
        {
            set
            {
                m_HasOccuse = value;
                Visible = value;
                if (value)
                {
                    foreach (var source in PackingBrakeCutManager.Instance.PackingBrakeCutItems.Where(w => w.State == PackingBrakeCutState.Cut))
                    {
                        m_UpTrainView.Cars[source.CarIndex].CarStateProxy.UserSetState = CarState.Fault;
                    }
                }
                else
                {
                    foreach (var car in m_UpTrainView.Cars)
                    {
                        car.CarStateProxy.UserSetState = CarState.Normal;
                    }
                }
            }
            get
            {
                return m_HasOccuse;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {

            InitalizeUpTrainView();

            InitConstTextCollection();

            m_ConfirmButton = new CRH2Button()
            {
                OutLineRectangle = new Rectangle(new Point(800 - 130, 300 - 45), DefaultBtnSize),
                Text = "确认",
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                TextColor = Color.White,
                ButtonDownEvent = (sender, args) => OnConfirmEvent(),
            };
        }

        private void InitConstTextCollection()
        {

            m_ConstInfos = new List<GDIRectText>();
            var location = new Point(30, 200);
            const int hinterval = 4;
            const int vinterval = 4;
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(0, 110, 800, 27),
                Text = " 停放制动隔离信息",
                DrawFont = new Font(CRH2Resource.Font12.FontFamily, 12, FontStyle.Bold),
                TextColor = Color.White,
                BkColor = Color.Red,
                TextFormat =
                    new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center},
            });
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSizeRed),
                Text = " 操作提示：",
                DrawFont = m_DefaultTextFont,
                TextColor = Color.White,
                BkColor = Color.Red,
                TextFormat = new StringFormat() {LineAlignment = StringAlignment.Near},
            });
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle =
                    new Rectangle(new Point(location.X + DefaultTextSizeRed.Width + hinterval, location.Y),
                        DefaultTextSizeYellow),
                Text = " 请下车切除故障车停放制动，并确认停放制动已缓解。",
                DrawFont = m_DefaultTextFont,
                TextColor = Color.Yellow,
                BkColor = Color.Black,
                TextFormat = new StringFormat() {LineAlignment = StringAlignment.Near},
            });

            location = new Point(location.X, location.Y + DefaultTextSizeRed.Height + vinterval);
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSizeRed),
                Text = " 注意：",
                DrawFont = m_DefaultTextFont,
                TextColor = Color.White,
                BkColor = Color.Red,
                TextFormat = new StringFormat() {LineAlignment = StringAlignment.Near},
            });
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle =
                    new Rectangle(new Point(location.X + DefaultTextSizeRed.Width + hinterval, location.Y),
                        DefaultTextSizeYellow),
                Text = " 停放制动车辆4个轴均设停放制动缸及缓解拉绳，需要",
                DrawFont = m_DefaultTextFont,
                TextColor = Color.Yellow,
                BkColor = Color.Black,
                TextFormat = new StringFormat() {LineAlignment = StringAlignment.Near},
            });
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle =
                    new Rectangle(
                        new Point(location.X + DefaultTextSizeRed.Width + hinterval,
                            location.Y + DefaultTextSizeYellow.Height), DefaultTextSizeYellow),
                Text = " 将故障车单侧缓解拉绳全部拉出。",
                DrawFont = m_DefaultTextFont,
                TextColor = Color.Yellow,
                BkColor = Color.Black,
                TextFormat = new StringFormat() {LineAlignment = StringAlignment.Near},
            });
        }

        private void InitalizeUpTrainView()
        {
            m_UpTrainView = new TrainView();
            m_UpTrainView.InitalizeComponet(false);
            m_UpTrainView.ResetCarState();
            foreach (var car in m_UpTrainView.Cars)
            {
                car.CarStateProxy = new CarStateProxy() {UserSetState = CarState.Normal};
            }
            m_UpTrainView.Location = new Point(220, 155);
            m_UpTrainView.IsUnitNameVisible = false;
            m_UpTrainView.IsCarStateAutoChangable = false;
            m_UpTrainView.IsUnitStateVisible = false;
            m_UpTrainView.NeedDrawCarName = false;
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"/>
        public override void OnDraw(Graphics g)
        {
            if (!Visible)
            {
                return;
            }
            m_UpTrainView.paint(g);

            m_ConstInfos.ForEach(e => e.OnDraw(g));

            m_ConfirmButton.OnDraw(g);
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"/>
        public override bool OnMouseDown(Point point)
        {
            return m_ConfirmButton.OnMouseDown(point);
        }

        /// <summary>
        /// 鼠标按下后弹起
        /// </summary>
        /// <param name="point"/>
        public override bool OnMouseUp(Point point)
        {
            return m_ConfirmButton.OnMouseUp(point);
        }

        protected virtual void OnConfirmEvent()
        {
            var handler = ConfirmEvent;
            if (handler != null)
            {
                handler();
            }
        }
    }
}