using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionB
{
    public partial class ForecastInfoControl : PictureBox
    {
        private ForecastInformationType m_ForecastInformationType;
        private IReadOnlyDictionary<ForecastInformationType, Image> m_ForecastInformationTypeImageDictionary;

        public ForecastInformationType ForecastInformationType
        {
            set
            {
                if (m_ForecastInformationType != value)
                {
                    m_ForecastInformationType = value;
                    UpdateImage();
                    Invalidate();
                }
            }
            get { return m_ForecastInformationType; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IReadOnlyDictionary<ForecastInformationType, Image> ForecastInformationTypeImageDictionary
        {
            set
            {
                m_ForecastInformationTypeImageDictionary = value;
                UpdateImage();
                Invalidate();
            }
            get { return m_ForecastInformationTypeImageDictionary; }
        }

        public ForecastInfoControl()
        {
            InitializeComponent();
            ForecastInformationTypeImageDictionary = new Dictionary<ForecastInformationType, Image>
            {
                {ForecastInformationType.None, CommonResource.ATPCommonResource.无色},
                {ForecastInformationType.Bridge, CommonResource.ATPCommonResource.ForecastInfoBridge},
                {ForecastInformationType.Station, CommonResource.ATPCommonResource.ForecastInfoStation},
                {ForecastInformationType.Tunnel, CommonResource.ATPCommonResource.ForecastInfoTunnel},
                {ForecastInformationType.TemporarySpeedLimit, CommonResource.ATPCommonResource.ForecastInfoTemporarySpeedLimit},
                {ForecastInformationType.PhaseSeparatingSectionYellow, CommonResource.ATPCommonResource.ForecastInfoPhaseSeparatingSectionYellow},
                {ForecastInformationType.PhaseSeparatingSection, CommonResource.ATPCommonResource.ForecastInfoPhaseSeparatingSectionGray}
            }.AsReadOnlyDictionary();

            SetStyle(ControlStyles.ResizeRedraw, true);

        }

        private void UpdateImage()
        {
            this.InvokeUpdateImageIfNeed(ForecastInformationTypeImageDictionary.ContainsKey(ForecastInformationType)
                ? ForecastInformationTypeImageDictionary[ForecastInformationType]
                : null);
        }
    }
}
