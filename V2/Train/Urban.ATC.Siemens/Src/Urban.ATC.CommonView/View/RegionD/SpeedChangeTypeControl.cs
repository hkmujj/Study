using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    internal partial class SpeedChangeTypeControl : PictureBox
    {
        private SpeedChangeType m_SpeedChangeType;
        private Dictionary<SpeedChangeType, Image> m_SpeedChangeTypeImageDictionary;
        private ISpeedChangeInfo m_SpeedChangeInfo;

        public SpeedChangeType SpeedChangeType
        {
            set
            {
                m_SpeedChangeType = value;
                UpdateSpeedChangeImage();
                Invalidate();
            }
            get { return m_SpeedChangeType; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ISpeedChangeInfo SpeedChangeInfo
        {
            set
            {
                if (m_SpeedChangeInfo != value)
                {
                    if (m_SpeedChangeInfo != null)
                    {
                        m_SpeedChangeInfo.PropertyChanged -= SpeedChangeInfoOnPropertyChanged;
                    }
                    m_SpeedChangeInfo = value;
                    if (m_SpeedChangeInfo != null)
                    {
                        SpeedChangeType = m_SpeedChangeInfo.SpeedChangeType;
                        m_SpeedChangeInfo.PropertyChanged += SpeedChangeInfoOnPropertyChanged;
                    }
                    else
                    {
                        SpeedChangeType = SpeedChangeType.None;
                    }
                    Invalidate();
                }
            }
            get { return m_SpeedChangeInfo; }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Dictionary<SpeedChangeType, Image> SpeedChangeTypeImageDictionary
        {
            set
            {
                m_SpeedChangeTypeImageDictionary = value;
                UpdateSpeedChangeImage();
                Invalidate();
            }
            get { return m_SpeedChangeTypeImageDictionary; }
        }

        public SpeedChangeTypeControl()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            SpeedChangeTypeImageDictionary = new Dictionary<SpeedChangeType, Image>
            {
                { SpeedChangeType.None, null },
                { SpeedChangeType.Acceleration, CommonResource.ATPCommonResource.预告加速 },
                { SpeedChangeType.Decelerate, CommonResource.ATPCommonResource.预告减速 },
                { SpeedChangeType.DecelerateToZero, CommonResource.ATPCommonResource.预告减速到0 },
            };
        }


        private void UpdateSpeedChangeImage()
        {
            Image img = null;
            if (SpeedChangeTypeImageDictionary != null && SpeedChangeTypeImageDictionary.ContainsKey(SpeedChangeType))
            {
                img = SpeedChangeTypeImageDictionary[SpeedChangeType];
            }
            this.InvokeUpdateImageIfNeed(img);
        }


        private void SpeedChangeInfoOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == PropertySupport.ExtractPropertyName<ISpeedChangeInfo, SpeedChangeType>(s => s.SpeedChangeType))
            {
                SpeedChangeType = SpeedChangeInfo.SpeedChangeType;
            }
        }
    }
}
