using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class Emergency : PicBoxControlBase
    {
        private EmergencyModel m_Model;

        public EmergencyModel Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model == value)
                {
                    return;
                }
                m_Model = value;
                Changeimg();
            }
        }

        private void Changeimg()
        {
            switch (Model)
            {
                case EmergencyModel.None:
                    ChangeImage(null);
                    break;

                case EmergencyModel.Slip:
                    ChangeImage(Resource1.Slip_or_Slide);
                    break;

                case EmergencyModel.EmergencyBrake:
                    ChangeImage(Resource1.Emergency_Brake);
                    break;

                case EmergencyModel.PSDNotCLose:
                    ChangeImage(Resource1.PSD_not_closed);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Emergency()
        {
            InitializeComponent();
        }
    }
}