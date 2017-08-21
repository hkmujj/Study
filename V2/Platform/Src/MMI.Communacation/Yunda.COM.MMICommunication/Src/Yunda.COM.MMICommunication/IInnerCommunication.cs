using System;
using Yunda.COM.MMICommunication.Model;

namespace Yunda.COM.MMICommunication
{
    internal interface IInnerCommunication
    {
        event Action<byte[], float[]> Received;

        /// <summary>
        /// 初始化
        /// </summary>
        void Initalize(IInitalizeParam initalizeParam);

        void Uninitalize();

        void Send(byte[] boolBytes, float[] floats);

    }
}