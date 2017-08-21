using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonResource;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;
using Motor.ATP.Domain.Model.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionB
{
    public partial class SpeedPointerPanel : Panel
    {
        private float m_Speed;
        private ISpeedDialPlate m_SpeedDialPlate;
        private Image m_PointerImage;
        private Brush m_SpeedBrush;
        private StringFormat m_SpeedStringFormat;
        private ISpeedModel m_SpeedModel;

        private RectangleF m_ImageRegion;
        private readonly Action<ATPColor> m_UpdatePointImageAction;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ISpeedModel SpeedModel
        {
            set
            {
                m_SpeedModel = value;
                Speed = value.Value;
                this.InvokeIfNeed(m_UpdatePointImageAction, value.SpeedColor);
                Invalidate();
            }
            get { return m_SpeedModel; }
        }


        public float Speed
        {
            set { m_Speed = value; Invalidate(); }
            get { return m_Speed; }
        }

        /// <summary>
        /// 速度值的刷
        /// </summary>
        public Brush SpeedBrush
        {
            set { m_SpeedBrush = value; Invalidate(); }
            get { return m_SpeedBrush; }
        }


        /// <summary>
        /// 速度值的格式 
        /// </summary>
                 [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public StringFormat SpeedStringFormat
        {
            set { m_SpeedStringFormat = value; Invalidate(); }
            get { return m_SpeedStringFormat; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ISpeedDialPlate SpeedDialPlate
        {
            set { m_SpeedDialPlate = value; Invalidate(); }
            get { return m_SpeedDialPlate; }
        }

        public Image PointerImage
        {
            set { m_PointerImage = value; Invalidate(); }
            get { return m_PointerImage; }
        }

        public SpeedPointerPanel()
        {
            InitializeComponent();
            m_SpeedDialPlate = new DefaultSpeedDialPlate();
            SpeedBrush = Brushes.White;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateImageRegion();
            m_UpdatePointImageAction = (ac) => PointerImage = GetImage(ac);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateImageRegion();
            base.OnSizeChanged(e);
        }

        private void UpdateImageRegion()
        {
            m_ImageRegion = RectangleF.Inflate(this.GetActureRectangleF(), -15, -15);
        }


        private Image GetImage(ATPColor speedColor)
        {
            switch (speedColor)
            {
                case ATPColor.None:
                case ATPColor.LightGrey:
                case ATPColor.Grey:
                case ATPColor.White:
                    return ATPCommonResource.指针灰;
                case ATPColor.Yellow:
                    return ATPCommonResource.指针黄;
                case ATPColor.Orange:
                    return ATPCommonResource.指针橙;
                case ATPColor.Red:
                    return ATPCommonResource.指针红;

                default:
                    LogMgr.Warn(string.Format("Can not get speed point image of {0}", speedColor));
                    return ATPCommonResource.指针灰;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (SpeedDialPlate == null || PointerImage == null)
            {
                return;
            }
            var g = e.Graphics;
            var old = g.Transform;

            var mat = new Matrix();
            mat.RotateAt(SpeedDialPlate.ConvertSpeedToAngle(Speed), new PointF((float)Width / 2, (float)Height / 2));

            g.Transform = mat;
            g.DrawImage(PointerImage, m_ImageRegion);
            g.Transform = old;
            if (SpeedBrush != null && SpeedStringFormat != null)
            {
                var txtSize = g.MeasureString("0000", Font);
                var txtRegion = RectangleF.Inflate(new RectangleF((float)Width / 2, (float)Height / 2, 0, 0), txtSize.Width, txtSize.Height);

                g.DrawString(Speed.ToString("0"), Font, SpeedBrush, txtRegion, SpeedStringFormat);
            }
        }
    }
}
