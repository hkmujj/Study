using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Train.Carriage;


namespace Tram.CBTC.NRIET.Converters
{
    public class CabStatusToTextConverter : IMultiValueConverter
    {

        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 3 || !(values[0] is int) || !(values[1] is OBCUStatus) || !(values[2] is OBCUStatus))
            {
                return DependencyProperty.UnsetValue;
            }
           
            string strText = String.Empty;
            string strCurCab = String.Empty;
            string strCabType = String.Empty;

            if ((int)values[0] == 0)
            {
                strCurCab = "本端";

                if ((OBCUStatus)values[1] == OBCUStatus.None)
                {
                    strCabType = "";
                }
                else if ((OBCUStatus)values[1] == OBCUStatus.Active)
                {
                    strCabType = "已激活";
                }
                else if ((OBCUStatus)values[1] == OBCUStatus.Standby)
                {
                    strCabType = "尚未激活";
                }
                else if ((OBCUStatus)values[1] == OBCUStatus.Bypass)
                {
                    strCabType = "旁路";
                }
                else if ((OBCUStatus)values[1] == OBCUStatus.Fault)
                {
                    strCabType = "故障";
                }
            }
            else if ((int)values[0] == 1)
            {
                strCurCab = "远端";

                if ((OBCUStatus)values[2] == OBCUStatus.None)
                {
                    strCabType = "";
                }
                else if ((OBCUStatus)values[2] == OBCUStatus.Active)
                {
                    strCabType = "已激活";
                }
                else if ((OBCUStatus)values[2] == OBCUStatus.Standby)
                {
                    strCabType = "尚未激活";
                }
                else if ((OBCUStatus)values[2] == OBCUStatus.Bypass)
                {
                    strCabType = "旁路";
                }
                else if ((OBCUStatus)values[2] == OBCUStatus.Fault)
                {
                    strCabType = "故障";
                }
            }
            else if ((int)values[0] == 2)
            {
                strCurCab = "两端";

                if ((OBCUStatus)values[1] == OBCUStatus.None && (OBCUStatus)values[2] == OBCUStatus.None)
                {
                    strCabType = "";
                }
                else if ((OBCUStatus)values[1] == OBCUStatus.Active && (OBCUStatus)values[2] == OBCUStatus.Active)
                {
                    strCabType = "已激活";
                }
                else if ((OBCUStatus)values[1] == OBCUStatus.Standby && (OBCUStatus)values[2] == OBCUStatus.Standby)
                {
                    strCabType = "尚未激活";
                }
                else if ((OBCUStatus)values[1] == OBCUStatus.Bypass && (OBCUStatus)values[2] == OBCUStatus.Bypass)
                {
                    strCabType = "旁路";
                }
                else if ((OBCUStatus)values[1] == OBCUStatus.Fault && (OBCUStatus)values[2] == OBCUStatus.Fault)
                {
                    strCabType = "故障";
                }

            }

            strText = strCurCab + strCabType;

            return strText;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}