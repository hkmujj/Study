using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class ReverseOperation : PicBoxControlBase
    {
        private ReverseModel m_Model;

        public ReverseModel Model
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
                Invalidate();
            }
        }

        private void ChangeImg()
        {
            switch (Model)
            {
                case ReverseModel.Initial:
                    ChangeImage(null);
                    break;

                case ReverseModel.AROffered:
                    ChangeImage(Resource1.AR_offered);
                    break;

                case ReverseModel.ARActive:
                    ChangeImage(Resource1.AR_active);
                    break;

                case ReverseModel.DTRO:
                    ChangeImage(Resource1.DTRO_is_offered);
                    break;

                case ReverseModel.DTROactive:
                    ChangeImage(Resource1.DTRO_active);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ReverseOperation()
        {
            InitializeComponent();
        }
    }
}