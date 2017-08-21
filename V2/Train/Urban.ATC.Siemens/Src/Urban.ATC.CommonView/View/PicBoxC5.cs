using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.CommonView.View;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Common.Control
{
    public partial class PicBoxC5 : PicBoxControlBase
    {
        private OBCUModel m_Model;

        public OBCUModel Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model == value)
                {
                    return;
                }
                m_Model = value;
                ChangeImg();
            }
        }

        public PicBoxC5()
        {
            InitializeComponent();
        }

        public void ChangeImg()
        {
            switch (Model)
            {
                case OBCUModel.None:
                    ChangeImage(null);
                    break;

                case OBCUModel.Level1:
                    ChangeImage(Resource1.OBCD_Green_White);
                    break;

                case OBCUModel.Level2:
                    ChangeImage(Resource1.OBCD_Green_Red);
                    break;

                case OBCUModel.Level3:
                    ChangeImage(Resource1.OBCD_Red_Green);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}