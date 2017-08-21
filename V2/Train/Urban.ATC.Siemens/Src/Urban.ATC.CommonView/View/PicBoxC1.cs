using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.CommonView.View;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Common.Control
{
    public partial class PicBoxC1 : PicBoxControlBase
    {
        private DriveingBrakeType m_Type;

        public DriveingBrakeType Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type == value)
                {
                    return;
                }
                m_Type = value;
                ChangeImg();
                Invalidate();
            }
        }

        public PicBoxC1()
        {
            InitializeComponent();
        }

        public void ChangeImg()
        {
            switch (Type)
            {
                case DriveingBrakeType.Initial:
                    ChangeImage(null);
                    break;

                case DriveingBrakeType.Motoring:
                    ChangeImage(Resource1.motoring);
                    break;

                case DriveingBrakeType.Coasting:
                    ChangeImage(Resource1.coasting);
                    break;

                case DriveingBrakeType.Braking:
                    ChangeImage(Resource1.braking);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}