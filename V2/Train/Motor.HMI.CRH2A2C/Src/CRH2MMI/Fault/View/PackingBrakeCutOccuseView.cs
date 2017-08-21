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
    /// ͣ���ƶ����뷢��
    /// </summary>
    internal class PackingBrakeCutOccuseView : CommonInnerControlBase
    {
        public event Action ConfirmEvent;

        private TrainView m_UpTrainView;

        private List<GDIRectText> m_ConstInfos;

        private CRH2Button m_ConfirmButton;

        /// <summary>
        /// ��ҳ��Ĭ��btn��С 
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
        /// ��ʼ��
        /// </summary>
        public override void Init()
        {

            InitalizeUpTrainView();

            InitConstTextCollection();

            m_ConfirmButton = new CRH2Button()
            {
                OutLineRectangle = new Rectangle(new Point(800 - 130, 300 - 45), DefaultBtnSize),
                Text = "ȷ��",
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
                Text = " ͣ���ƶ�������Ϣ",
                DrawFont = new Font(CRH2Resource.Font12.FontFamily, 12, FontStyle.Bold),
                TextColor = Color.White,
                BkColor = Color.Red,
                TextFormat =
                    new StringFormat() {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center},
            });
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSizeRed),
                Text = " ������ʾ��",
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
                Text = " ���³��г����ϳ�ͣ���ƶ�����ȷ��ͣ���ƶ��ѻ��⡣",
                DrawFont = m_DefaultTextFont,
                TextColor = Color.Yellow,
                BkColor = Color.Black,
                TextFormat = new StringFormat() {LineAlignment = StringAlignment.Near},
            });

            location = new Point(location.X, location.Y + DefaultTextSizeRed.Height + vinterval);
            m_ConstInfos.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(location, DefaultTextSizeRed),
                Text = " ע�⣺",
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
                Text = " ͣ���ƶ�����4�������ͣ���ƶ��׼�������������Ҫ",
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
                Text = " �����ϳ����໺������ȫ��������",
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
        /// ��ͼ
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
        /// ��갴��
        /// </summary>
        /// <param name="point"/>
        public override bool OnMouseDown(Point point)
        {
            return m_ConfirmButton.OnMouseDown(point);
        }

        /// <summary>
        /// ��갴�º���
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