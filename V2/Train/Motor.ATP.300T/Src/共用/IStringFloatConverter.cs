using System;
using System.Collections.Generic;
using System.Linq;

namespace Motor.ATP._300T.共用
{
    public interface IStringFloatConverter
    {
        char[] ConvertFloatArray(float[] data);

        float[] ConvertCharArray(char[] data);
    }

    class StringFloatConverter : IStringFloatConverter
    {
        public char[] ConvertFloatArray(float[] data)
        {
            return data.SelectMany(BitConverter.GetBytes).Select(s => BitConverter.ToChar(new byte[] { s, 0 }, 0)).ToArray();
        }

        public float[] ConvertCharArray(char[] data)
        {
            var ba = data.Select(s => BitConverter.GetBytes(s)[0]).ToArray();
            var rlt = new List<float>(ba.Length / 4);
            for (var i = 0; i < ba.Length; i += 4)
            {
                rlt.Add(BitConverter.ToSingle(ba, i));
            }
            return rlt.ToArray();
        }
    }
}