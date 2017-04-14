using System;

namespace ES.Facility.PublicModule.TypeTransition
{
    /// <summary>
    /// enum类型转换用， 只用于内部数据的转换
    /// </summary>
    public class EnumTransition
    {
        public EnumTransition()
        {
            IsRight = false;
        }

        public bool IsRight { get; set; }

        public T strToEnum<T>(string tmpStr) where T : struct
        {
            try
            {
                IsRight = true;
                return (T)Enum.Parse(typeof(T), tmpStr);
            }
            catch(ArgumentException)
            {
                IsRight = false;
                return (T)Enum.Parse(typeof(T), "0");
            }
        }

        public string enumToNameStr<T>(T tmpT) where T : struct
        {
            try
            {
                IsRight = true;
                return Enum.Format(typeof(T), tmpT, "G");
            }
            catch
            {
                IsRight = false;
                return string.Empty;
            }
        }

        public string enumToValueStr<T>(T tmpT) where T : struct
        {
            try
            {
                IsRight = true;
                return Enum.Format(typeof(T), tmpT, "D");
            }
            catch
            {
                IsRight = false;
                return string.Empty;
            }
        }

        public int enumToInt<T>(T tmpT) where T : struct
        {
            try
            {
                IsRight = true;
                return int.Parse(Enum.Format(typeof(T), tmpT, "D"));
            }
            catch
            {
                IsRight = false;
                return 0;
            }
        }
    }
}
