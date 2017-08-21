using System;
using System.Collections.Generic;
using System.Linq;

namespace Motor.HMI.CRH3C380B.Common
{
    public class ATP300DFloatStringInterpreter : IFloatStringInterpreter
    {
        public char[] InterpreterFloatArray(float[] data)
        {
            return data.SelectMany(BitConverter.GetBytes).Select(s => BitConverter.ToChar(new byte[] { s, 0 }, 0)).ToArray();
        }

        public float[] InterpreterCharArray(char[] data)
        {
            var ba = data.Select(s => BitConverter.GetBytes(s)[0]).ToArray();
            var rlt = new List<float>(ba.Length / 4);
            for (int i = 0; i < ba.Length; i += 4)
            {
                rlt.Add(BitConverter.ToSingle(ba, i));
            }
            return rlt.ToArray();
        }
    }
}