using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Road.Station;

namespace Tram.CBTC.NRIET.Converters
{
    public class ReturnInfoToTextConvert : IMultiValueConverter
    {
        /// <summary>
        /// 转换值。
        /// </summary>
        /// <returns>
        /// 转换后的值。如果该方法返回 null，则使用有效的 null 值。
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue || values.Length < 2 || !(values[0] is ReturnInfo) || !(values[1] is StationInfo))
            {
                return DependencyProperty.UnsetValue;
            }

            string strReturnInfo = String.Empty;
            string strStation = String.Empty;
            string strReturnInfoType = String.Empty;

            if ((ReturnInfo)values[0] == ReturnInfo.None)
            {
                
            }
            else if ((ReturnInfo)values[0] == ReturnInfo.CurrentStationReturn)
            {
                strStation = ((StationInfo) values[1]).CurrentStation;
                strReturnInfoType = "折返轨";
            }
            else if ((ReturnInfo)values[0] == ReturnInfo.NextStationReurn)
            {
                strStation = ((StationInfo)values[1]).NextStation;
                strReturnInfoType = "折返轨";
            }

            strReturnInfo = strStation + strReturnInfoType;

            return strReturnInfo;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}