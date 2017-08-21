using System;
using Urban.ATC.CommonView.Properties;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class Door : PicBoxControlBase
    {
        private DoorRelease m_DoorType;

        public DoorRelease DoorType
        {
            get { return m_DoorType; }
            set
            {
                if (m_DoorType == value)
                {
                    return;
                }
                m_DoorType = value;
                Changeimg();
            }
        }

        private void Changeimg()
        {
            switch (DoorType)
            {
                case DoorRelease.Initial:
                    ChangeImage(null);
                    break;

                case DoorRelease.OpenLeft:
                    ChangeImage(Resource1.Open_Left);
                    break;

                case DoorRelease.Openright:
                    ChangeImage(Resource1.Open_Right);
                    break;

                case DoorRelease.OpenBoth:
                    ChangeImage(Resource1.Open_Both);
                    break;

                case DoorRelease.OpenLeftfirst:
                    ChangeImage(Resource1.Open_Left_first);
                    break;

                case DoorRelease.OpenRightfirst:
                    ChangeImage(Resource1.Open_Right_first);
                    break;

                case DoorRelease.NoDoorSupervision:
                    ChangeImage(Resource1.No_door_supervision);
                    break;

                case DoorRelease.PermissiveReleaseDoor:
                    ChangeImage(Resource1.Permissiver_door_release);
                    break;

                case DoorRelease.ClosedAndLockedLevel1:
                    ChangeImage(Resource1.closed_and_locked_1);
                    break;

                case DoorRelease.ClosedAndLockedLevel2:
                    ChangeImage(Resource1.closed_and_locked_2);
                    break;

                case DoorRelease.ClosedAndLockedLevel3:
                    ChangeImage(Resource1.closed_and_locked_3);
                    break;

                case DoorRelease.ClosedAndLockedLevel4:
                    ChangeImage(Resource1.closed_and_locked_4);
                    break;

                case DoorRelease.ClosedAndLockedLevel5:
                    ChangeImage(Resource1.closed_and_locked_5);
                    break;

                case DoorRelease.ClosedAndLockedLevel6:
                    ChangeImage(Resource1.closed_and_locked_6);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Door()
        {
            InitializeComponent();
        }
    }
}