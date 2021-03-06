﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CBTC.Infrasturcture.Model.Constant;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongDoorStatusMulConverter : IMultiValueConverter
    {
        private enum DoorStatusFlag
            {
                Unknown,
                OpenDoor,
                ColsehDoor,
                Abnormal
            };
        private enum DoorStatus
        {
            /// <summary>
            ///允许手动方式开左门，且左右门均关好
            ManualAllowOpenLeftAndClosed = 0,
            ///允许手动方式开左门，且车门未关闭
            ManualAllowOpenLeftAndOpen,
            ///允许手动方式开右门，且左右门均关好
            ManualAllowOpenRightAndClosed,
            ///允许手动方式开右门，且车门未关闭
            ManualAllowOpenRightAndOpen,
            ///允许手动方式开两侧门，且左右门均关好
            ManualAllowOpenDoubleAndClosed,
            ///允许手动方式开两侧门，且车门未关闭
            ManualAllowOpenDoubleAndOpen,
            ///允许手动方式先开左门，后开右门，且左右门均关好
            ManualAllowOpenLeftFirstAndClosed,
            ///允许手动方式先开左门，后开右门，且车门未关闭
            ManualAllowOpenLeftFirstAndOpen,
            ///允许手动方式先开右门，后开左门，且左右门均关好
            ManualAllowOpenRightFirstAndClosed,
            ///允许手动方式先开右门，后开左门，且车门未关闭
            ManualAllowOpenRightFirstAndOpen,
            ///允许自动方式开左门，且左右门均关好
            AutoAllowOpenLeftAndClosed,
            ///允许自动方式开左门，且车门未关闭
            AutoAllowOpenLeftAndOpen,
            ///允许自动方式开右门，且左右门均关好
            AutoAllowOpenRightAndClosed,
            ///允许自动方式开右门，且车门未关闭
            AutoAllowOpenRightAndOpen,
            ///允许自动方式开两侧门，且左右门均关好
            AutoAllowOpenDoubleAndClosed,
            ///允许自动方式开两侧门，且车门未关闭
            AutoAllowOpenDoubleAndOpen,
            ///允许自动方式先开左门，后开右门，且左右门均关好
            AutoAllowOpenLeftFirstAndClosed,
            ///允许自动方式先开左门，后开右门，且车门未关闭
            AutoAllowOpenLeftFirstAndOpen,
            ///允许自动方式先开右门，后开左门，且左右门均关好
            AutoAllowOpenRightFirstAndClosed,
            ///允许自动方式先开右门，后开左门，且车门未关闭
            AutoAllowOpenRightFirstAndOpen,
            /// 无门命令，且左右门都关好
            NoOpenDoorAndClosed,
            /// 无门命令，且车门未关闭
            NoOpenDoorAndOpen,
            /// 门监督丢失
            NODoorSupervision,
            ///不需显示
            Normal,
            /// </summary>
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 4 || (!(values[0] is DoorControlMode) && !(values[1] is DoorAllowState) && !(values[2] is DoorState) && !(values[3] is DoorState)) )
            {
                return DependencyProperty.UnsetValue;
            }

            Array xArray = parameter as Array;

            DoorStatusFlag bFlagDoorState = DoorStatusFlag.Unknown;
            if ((DoorState)values[2] == DoorState.Abnormal || (DoorState)values[3] == DoorState.Abnormal)
            {
                bFlagDoorState = DoorStatusFlag.Abnormal;
            }
            else if ((DoorState)values[2] == DoorState.Closed && (DoorState)values[3] == DoorState.Closed)
            {
                bFlagDoorState = DoorStatusFlag.ColsehDoor;
            }
            else if ((DoorState)values[2] == DoorState.Opend || (DoorState)values[3] == DoorState.Opend)
            {
                bFlagDoorState = DoorStatusFlag.OpenDoor;
            }
            else
            {
                bFlagDoorState = DoorStatusFlag.Unknown;
            }


            switch ((DoorControlMode)values[0])
            {
                case DoorControlMode.MOMC:
                    switch (bFlagDoorState)
                    {
                        case DoorStatusFlag.ColsehDoor:
                            switch ((DoorAllowState)values[1])
                            {
                                case DoorAllowState.LeftAllow:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenLeftAndClosed);
                                case DoorAllowState.RightAllow:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenRightAndClosed);
                                case DoorAllowState.AllowBoth:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenDoubleAndClosed);
                                case DoorAllowState.FirstLeft:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenLeftFirstAndClosed);
                                case DoorAllowState.FirstRight:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenRightFirstAndClosed);
                                case DoorAllowState.NoAllow:
                                    return xArray.GetValue((int)DoorStatus.NoOpenDoorAndClosed);
                                case DoorAllowState.Unkown:
                                default:
                                    return DependencyProperty.UnsetValue;
                            }
                        case DoorStatusFlag.OpenDoor:
                            switch ((DoorAllowState)values[1])
                            {
                                case DoorAllowState.LeftAllow:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenLeftAndOpen);
                                case DoorAllowState.RightAllow:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenRightAndOpen);
                                case DoorAllowState.AllowBoth:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenDoubleAndOpen);
                                case DoorAllowState.FirstLeft:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenLeftFirstAndOpen);
                                case DoorAllowState.FirstRight:
                                    return xArray.GetValue((int)DoorStatus.ManualAllowOpenRightFirstAndOpen);
                                case DoorAllowState.NoAllow:
                                    return xArray.GetValue((int)DoorStatus.NoOpenDoorAndOpen);
                                case DoorAllowState.Unkown:
                                default:
                                    return DependencyProperty.UnsetValue;
                            }
                        case DoorStatusFlag.Abnormal:
                            return xArray.GetValue((int) DoorStatus.NODoorSupervision);
                        case DoorStatusFlag.Unknown:
                        default:
                            return DependencyProperty.UnsetValue;
                    }
                case DoorControlMode.AOAC:
                case DoorControlMode.AOMC:
                    switch (bFlagDoorState)
                    {
                        case DoorStatusFlag.ColsehDoor:
                            switch ((DoorAllowState)values[1])
                            {
                                case DoorAllowState.LeftAllow:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenLeftAndClosed);
                                case DoorAllowState.RightAllow:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenRightAndClosed);
                                case DoorAllowState.AllowBoth:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenDoubleAndClosed);
                                case DoorAllowState.FirstLeft:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenLeftFirstAndClosed);
                                case DoorAllowState.FirstRight:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenRightFirstAndClosed);
                                case DoorAllowState.NoAllow:
                                    return xArray.GetValue((int)DoorStatus.NoOpenDoorAndClosed);
                                case DoorAllowState.Unkown:
                                default:
                                    return DependencyProperty.UnsetValue;
                            }
                        case DoorStatusFlag.OpenDoor:
                            switch ((DoorAllowState)values[1])
                            {
                                case DoorAllowState.LeftAllow:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenLeftAndOpen);
                                case DoorAllowState.RightAllow:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenRightAndOpen);
                                case DoorAllowState.AllowBoth:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenDoubleAndOpen);
                                case DoorAllowState.FirstLeft:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenLeftFirstAndOpen);
                                case DoorAllowState.FirstRight:
                                    return xArray.GetValue((int)DoorStatus.AutoAllowOpenRightFirstAndOpen);
                                case DoorAllowState.NoAllow:
                                    return xArray.GetValue((int)DoorStatus.NoOpenDoorAndOpen);
                                case DoorAllowState.Unkown:
                                default:
                                    return DependencyProperty.UnsetValue;
                            }
                        case DoorStatusFlag.Abnormal:
                            return xArray.GetValue((int) DoorStatus.NODoorSupervision);
                        case DoorStatusFlag.Unknown:
                        default:
                            return DependencyProperty.UnsetValue;
                    }
                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}