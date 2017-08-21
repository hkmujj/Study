using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class Departure : PicBoxControlBase
    {
        private DepartureType m_Type;

        public DepartureType Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type == value)
                {
                    return;
                }
                m_Type = value;
                Changeimg();
            }
        }

        private void Changeimg()
        {
            switch (Type)
            {
                case DepartureType.None:
                    ChangeImage(null);
                    break;

                case DepartureType.DoorCloseOrder:
                    ChangeImage(Resource1.Door_Close_Order);
                    break;

                case DepartureType.DepartureRequest:
                    ChangeImage(Resource1.Departure_Request);
                    break;

                case DepartureType.Hold:
                    ChangeImage(Resource1.HOLD);
                    break;

                case DepartureType.Skip:
                    ChangeImage(Resource1.SKIP);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Departure()
        {
            InitializeComponent();
        }
    }
}