using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class StoppingAccuracy : PicBoxControlBase
    {
        private StopModel m_Model;

        public StopModel Model
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

        private void ChangeImg()
        {
            switch (Model)
            {
                case StopModel.Initial:
                    ChangeImage(null);
                    break;

                case StopModel.Outside:
                    ChangeImage(Resource1.Outside_stopping_window);
                    break;

                case StopModel.Inside:
                    ChangeImage(Resource1.Inside_Stopping_window);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public StoppingAccuracy()
        {
            InitializeComponent();
        }
    }
}