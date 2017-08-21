using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class ErrorDetail : PicBoxControlBase
    {
        private DoorDetailModel m_Model;

        public DoorDetailModel Model
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
                case DoorDetailModel.None:
                    ChangeImage(null);
                    break;

                case DoorDetailModel.RAD:
                    ChangeImage(Resource1.No_WCURadio_link);
                    break;

                case DoorDetailModel.ATP:
                    ChangeImage(Resource1.ATO_fault);
                    break;

                case DoorDetailModel.ATO:
                    ChangeImage(Resource1.ATO_fault);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ErrorDetail()
        {
            InitializeComponent();
        }
    }
}