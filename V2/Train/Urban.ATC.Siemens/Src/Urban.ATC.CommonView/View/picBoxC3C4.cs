using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.CommonView.View;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Common.Control
{
    public partial class picBoxC3C4 : PicBoxControlBase
    {
        private TrainInteGrity m_Type;

        public TrainInteGrity Type
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
            }
        }

        public picBoxC3C4()
        {
            InitializeComponent();
        }

        public void ChangeImg()
        {
            switch (Type)
            {
                case TrainInteGrity.Initial:
                    ChangeImage(null);
                    break;

                case TrainInteGrity.TrainIntegrity:
                    ChangeImage(Resource1.Train_Integrity_not_ok);
                    break;

                case TrainInteGrity.BrakingPressure:
                    ChangeImage(Resource1.RST_Braking_pressure_not_ok);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}