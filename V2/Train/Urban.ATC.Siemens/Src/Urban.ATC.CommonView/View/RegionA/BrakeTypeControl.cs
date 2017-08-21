using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Model;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionA
{
    public partial class BrakeTypeControl : PictureBox
    {
        private BrakeType m_BrakeType;
        private IReadOnlyDictionary<BrakeType, Image> m_BrakeTypeImageDictionary;

        public BrakeType BrakeType
        {
            set
            {
                m_BrakeType = value;
                UpdateImage();
                Invalidate();
            }
            get { return m_BrakeType; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IReadOnlyDictionary<BrakeType, Image> BrakeTypeImageDictionary
        {
            set
            {
                m_BrakeTypeImageDictionary = value;
                UpdateImage();
                Invalidate();
            }
            get { return m_BrakeTypeImageDictionary; }
        }

        public BrakeTypeControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            BrakeTypeImageDictionary = new ReadOnlyDictionary<BrakeType, Image>(new Dictionary<BrakeType, Image>()
            {
                {BrakeType.None, null},
                //{BrakeType.Lvel1, CommonResource.ATPCommonResource.C3_一级制动},
                //{BrakeType.Level2, CommonResource.ATPCommonResource.C2_一级制动},
                //{BrakeType.Level4, CommonResource.ATPCommonResource.C2_四级制动},
                //{BrakeType.Normal, CommonResource.ATPCommonResource.常用制动},
                {BrakeType.WeakNormal, CommonResource.ATPCommonResource.弱常用制动},
                {BrakeType.MiddlingNormal, CommonResource.ATPCommonResource.中等常用制动},
                {BrakeType.MaxNormal, CommonResource.ATPCommonResource.C2_最大常用制动},
                {BrakeType.AllowRelease, CommonResource.ATPCommonResource.C3_允许缓解},
                {BrakeType.Emergent, CommonResource.ATPCommonResource.C2_紧急制动},
            });

        }

        private void UpdateImage()
        {
            this.InvokeUpdateImageIfNeed(null);
            if (BrakeTypeImageDictionary != null)
            {
                if (BrakeTypeImageDictionary.ContainsKey(BrakeType))
                {
                    this.InvokeUpdateImageIfNeed(BrakeTypeImageDictionary[BrakeType]);
                }
            }
        }
    }
}
