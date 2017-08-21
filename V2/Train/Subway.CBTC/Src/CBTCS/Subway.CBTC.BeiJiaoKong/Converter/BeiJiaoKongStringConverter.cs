using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Subway.CBTC.BeiJiaoKong.Models.Domain;

namespace Subway.CBTC.BeiJiaoKong.Converter
{
    public class BeiJiaoKongStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != DependencyProperty.UnsetValue)
            {
                string strTemp = "";

                if (value != null)
                {
                    if (string.IsNullOrEmpty(value.ToString()))
                    {
                        for (int i = 0; i < (int)DriverNumber.TCTDriverNumber; i++)
                        {
                            strTemp += "－";
                        }
                        return strTemp;
                    }

                    strTemp = "";
                    for (int i = 0; i < ((int)DriverNumber.TCTDriverNumber - value.ToString().Length); i++)
                    {
                        strTemp += "－";
                    }

                    return toSBC(value.ToString()) + strTemp;
                }
                else
                {
                    for (int i = 0; i < (int)DriverNumber.TCTDriverNumber; i++)
                    {
                        strTemp += "－";
                    }
                    return strTemp;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string toSBC(string inputSBC)
        {
            char[] cTemp = inputSBC.ToCharArray();

            for (int i = 0; i < cTemp.Length; i++)
            {
                if (cTemp[i] == 32)
                {
                    cTemp[i] = (char) 12288;
                    continue;
                }
                if (cTemp[i] < 127)
                {
                    cTemp[i] = (char) (cTemp[i] + 65248);
                }
            }
            return new string(cTemp);
        }
    }
}
