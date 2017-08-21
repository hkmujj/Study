using System;
using System.Globalization;
using System.Windows.Data;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public class DoorReleaseConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var model = (DoorRelease)value;
            switch (model)
            {
                default:
                case DoorRelease.Initial:
                    return (null);

                case DoorRelease.OpenLeft:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Open_Left);

                case DoorRelease.Openright:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Open_Right);

                case DoorRelease.OpenBoth:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Open_Both);

                case DoorRelease.OpenLeftfirst:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Open_Left_first);

                case DoorRelease.OpenRightfirst:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Open_Right_first);

                case DoorRelease.NoDoorSupervision:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.No_door_supervision);

                case DoorRelease.PermissiveReleaseDoor:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.Permissiver_door_release);

                case DoorRelease.ClosedAndLockedLevel1:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.closed_and_locked_1);

                case DoorRelease.ClosedAndLockedLevel2:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.closed_and_locked_2);

                case DoorRelease.ClosedAndLockedLevel3:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.closed_and_locked_3);

                case DoorRelease.ClosedAndLockedLevel4:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.closed_and_locked_4);

                case DoorRelease.ClosedAndLockedLevel5:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.closed_and_locked_5);

                case DoorRelease.ClosedAndLockedLevel6:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.closed_and_locked_6);

                case DoorRelease.PermitLeft:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.PermitLeft);

                case DoorRelease.PermitRight:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.PermitRight);

                case DoorRelease.PermitDouble:
                    return ConvertToImageSouce.ChangeImageToImageSouce(ImageResouce.PermitDouble);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}