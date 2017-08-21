using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class SpecialDetails : PicBoxControlBase
    {
        private SpecialModel m_Model;

        public SpecialModel Model
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
                case SpecialModel.Initial:
                    ChangeImage(null);
                    break;

                case SpecialModel.DepotEntry:
                    ChangeImage(Resource1.Depot_Entry);
                    break;

                case SpecialModel.OnDepot:
                    ChangeImage(Resource1.Depot_Driver);
                    break;

                case SpecialModel.ReleaseSpeed:
                    ChangeImage(Resource1.Relesase_Speed);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public SpecialDetails()
        {
            InitializeComponent();
        }
    }
}