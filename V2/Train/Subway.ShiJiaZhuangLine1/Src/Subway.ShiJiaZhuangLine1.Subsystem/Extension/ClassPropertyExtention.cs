using System.Linq;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Extension
{
    public static class ClassPropertyExtention
    {
        public static void SetPropertyValue<T>(this object tagetClass, T value)
        {
            var taget = tagetClass.GetType().GetProperties().Where(w => w.PropertyType == typeof(T));
            foreach (var info in taget)
            {
                info.SetValue(tagetClass, value, null);
            }
        }

        public static bool GetValueAny<T>(this object tagetClass, T value)
        {
            var taget = tagetClass.GetType().GetProperties().Where(w => w.PropertyType == typeof(T));
            return taget.Any(info => info.GetValue(tagetClass, null).Equals(value));
        }
    }
}