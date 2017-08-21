using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonResource;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    /// <summary>
    /// 预告信息
    /// </summary>
    public partial class ForecastInformationControl : ForecationInfoControlBase
    {
        private IATP m_ATP;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IATP ATP
        {
            set
            {
                if (m_ATP == value)
                {
                    return;
                }
                m_ATP = value;
                if (m_ATP != null && m_ATP.ForecastInformation != null)
                {
                    m_ATP.SpeedMonitoringSection.PropertyChanged += SpeedMonitoringSectionOnPropertyChanged;
                    foreach (var item in m_ATP.ForecastInformation.ForecastInformationItems)
                    {
                        AddForcast(item);
                    }

                    foreach (var box in m_PictureBoxBuffer)
                    {
                        box.AddOpuaqueLayer(ATP.Other);
                    }
                }
                Invalidate();
            }
            get { return m_ATP; }
        }

        private void SpeedMonitoringSectionOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<ISpeedMonitoringSection, IPlanSectionCoordinate>(a => a.PlanSectionCoordinate))
            {
                UpdateItemsLocation();
            }
        }

        /// <summary>
        /// 每行最大预告信息个数
        /// </summary>
        private const int MaxForecastInformationCount = 7;

        /// <summary>
        /// 最大行数
        /// </summary>
        private const int MaxForecastInformationLine = 2;

        private Queue<PictureBox> m_PictureBoxBuffer;

        private List<PictureBox> m_DownPictureBoxs;

        private Queue<PictureBox> m_UsingPictureBoxs;

        private readonly Action<PictureBox, IForecastInformationItem> m_UpdateAction;

        public ForecastInformationControl()
        {
            InitializeComponent();
            m_UpdateAction = (p, i) =>
            {
                if (UpdateType(p, i))
                {
                    UpdateLocaton(p, i);
                }
            };
            SetStyle(ControlStyles.ResizeRedraw, true);

            InitializePictureBuff();

            this.SizeChanged += OnSizeChanged;
           
        }

        private void InitializePictureBuff()
        {
            m_PictureBoxBuffer = new Queue<PictureBox>();
            m_UsingPictureBoxs = new Queue<PictureBox>();
            m_DownPictureBoxs = new List<PictureBox>();
            for (int i = 0; i < MaxForecastInformationCount; i++)
            {
                for (int j = 0; j < MaxForecastInformationLine; j++)
                {
                    var pBox = new PictureBox()
                    {
                        Visible = false,
                        Image = ATPCommonResource.CabSignal_H,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.FromArgb(22, 22, 22),
                    };
                    Controls.Add(pBox);
                    m_PictureBoxBuffer.Enqueue(pBox);
                    if (j == 1)
                    {
                        m_DownPictureBoxs.Add(pBox);
                    }
                }
            }
            ResizePictureBoxBuff();
        }

        private void AddForcast(IForecastInformationItem item)
        {
            var pbox = m_PictureBoxBuffer.Dequeue();
            m_UsingPictureBoxs.Enqueue(pbox);
            if (item != null)
            {
                item.PropertyChanged += ItemOnPropertyChanged;
                pbox.Tag = item;
                pbox.Visible = true;
            }
            UpdateItemControl(pbox, item);
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var item = (IForecastInformationItem)sender;
            var pbox = m_UsingPictureBoxs.First(f => f.Tag == item);

            UpdateItemControl(pbox, item);
        }

        private void UpdateItemsLocation()
        {
            foreach (var box in m_UsingPictureBoxs)
            {
                UpdateItemControl(box, (IForecastInformationItem)box.Tag);
            }
        }

        private void UpdateItemControl(PictureBox pbox, IForecastInformationItem item)
        {
            this.InvokeIfNeed(m_UpdateAction, pbox, item);
        }

        private bool UpdateType(PictureBox pbox, IForecastInformationItem item)
        {
            if (item.Type == ForecastInformationType.None)
            {
                pbox.Image = null;
                pbox.Visible = false;
                return false;
            }
            pbox.Visible = true;
            switch (item.Type)
            {
                case ForecastInformationType.None:
                    break;
                case ForecastInformationType.Bridge:
                    pbox.Image = ATPCommonResource.ForecastInfoBridge;
                    break;
                case ForecastInformationType.Station:
                    pbox.Image = ATPCommonResource.ForecastInfoStation;
                    break;
                case ForecastInformationType.Tunnel:
                    pbox.Image = ATPCommonResource.ForecastInfoTunnel;
                    break;
                case ForecastInformationType.TemporarySpeedLimit:
                    pbox.Image = ATPCommonResource.ForecastInfoTemporarySpeedLimit;
                    break;
                case ForecastInformationType.PhaseSeparatingSectionYellow:
                case ForecastInformationType.PhaseSeparatingSection:
                    pbox.Image = ATPCommonResource.ForecastInfoPhaseSeparatingSectionGray;
                    break;
                default:
                    LogMgr.Warn(string.Format("Can not get image of forcast information type(={0})", item.Type));
                    pbox.Image = null;
                    pbox.Visible = false;
                    return false;
            }

            return true;
        }

        private void UpdateLocaton(PictureBox pbox, IForecastInformationItem item)
        {
            if (Math.Abs(item.Distance) < float.Epsilon)
            {
                pbox.Image = null;
                pbox.Visible = false;
                return;
            }
            pbox.Left = GetLeftOf(item) - pbox.Width/2;
        }


        private int GetLeftOf(IForecastInformationItem item)
        {
            return (int)(m_ATP.SpeedMonitoringSection.PlanSectionCoordinate.GetDistanceScal(item.Distance) * Width);
        }


        private void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            ResizePictureBoxBuff();
            Invalidate();
        }

        private void ResizePictureBoxBuff()
        {
            foreach (var pictureBox in m_PictureBoxBuffer)
            {
                pictureBox.Size = new Size(Size.Height / 2, Size.Height / 2);
            }

            foreach (var pictureBox in m_DownPictureBoxs)
            {
                pictureBox.Top = Size.Height / 2;
            }

            foreach (var pictureBox in m_UsingPictureBoxs)
            {
                UpdateItemControl(pictureBox, pictureBox.Tag as IForecastInformationItem);
            }
        }
    }
}
