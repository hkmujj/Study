using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MMI.Communacation.Interface.ProtocolLayer;

namespace MMI.Communacation.Control.ProtocolLayer.SendPackage
{
    public class SendPackage : ISend
    {
        public SendPackageHeadWrapper Head { set; get; }

        public byte[] Data { private set; get; }

        /// <summary>
        /// 发送的bool的缓冲区
        /// </summary>
        private readonly byte[] m_BoolBytesBuffer;

        private int BoolByteCount { get { return m_BoolBytesBuffer.Length; }}

        private int FloatByteCount { get { return Data.Length - BoolByteCount; }}

        public SendPackage(int floatByteCount, int boolByteCount)
        {
            Head = new SendPackageHeadWrapper {PackageHead = {TypeC = SendDataTypeC.First}};

            if (floatByteCount > 0)
            {
                Data = new byte[floatByteCount + boolByteCount];
            }
            if (boolByteCount > 0)
            {
                m_BoolBytesBuffer = new byte[boolByteCount];
            }
        }

        public void ConvertToBytes(SortedDictionary<int, bool> boolDictionary, int boolStartIndex, SortedDictionary<int, float> floatDictionary, int floatStartIndex)
        {
            var bitarr = new BitArray(boolDictionary.Values.Skip(boolStartIndex).Take(BoolByteCount * 8).ToArray());
            bitarr.CopyTo(m_BoolBytesBuffer, 0);

            Array.Clear(Data, 0, Data.Length);

            using (var bw = new BinaryWriter(new MemoryStream(Data)))
            {
                foreach (var f in floatDictionary.Skip(floatStartIndex).Take(FloatByteCount / 4))
                {
                    bw.Write(f.Value);
                }

                bw.Write(m_BoolBytesBuffer);

                bw.Flush();
            }
        }

        public byte[] ToSendBytes()
        {
            if (Data == null)
            {
                return Head.ToSendBytes();
            }
            var headBytes = Head.ToSendBytes();

            var sends = new byte[headBytes.Length + Data.Length];

            Buffer.BlockCopy(headBytes,0, sends,0, headBytes.Length);

            Buffer.BlockCopy(Data, 0, sends, headBytes.Length, Data.Length);

            return sends;
        }
    }
}
