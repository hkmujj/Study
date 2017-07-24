using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Yunda.COM.MMICommunication.InnerCommunication.Memery;

namespace Yunda.COM.MMICommunication
{
    [Guid("2E61C3BC-5A34-435E-835A-7B6047A7CA05")]
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IRecvData
    {
        [DispId(1)]
        void Recv(bool[] bools, float[] floats);
    }

    [Guid("4C47BE54-1813-4845-81D6-F0944DE52010")]
    [ComVisible(true)]
    public interface ICommunication
    {
        /// <summary>
        /// 初始化
        /// </summary>
        [DispId(1)]
        void Initalize();

        [DispId(2)]
        void Uninitalize();

        /// <summary>
        /// 设置接收数据的接口 
        /// </summary>
        /// <param name="address">函数void Recv(byte* byts, int blength, float* floats, int flength);的地址</param>
        [DispId(4)]
        void SetRecvHandler(int address);

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="floats"></param>
        [DispId(6)]
        void SendAsyn(byte[] bytes, float[] floats);
    }

    [Guid("9975317F-6B7F-4C56-8435-DF9A45AD2A0B")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ICommunication))]
    [ComSourceInterfaces(typeof(IRecvData))]
    [ComVisible(true)]
    public class Communication : ICommunication
    {
        [ComVisible(false)]
        public delegate void Recv(byte[] bools, int blength, float[] floats, int flength);

        public event Recv RecvEvent;

        private IInnerCommunication m_InnerCommunication;

        /// <summary>
        /// 
        /// </summary>
        public Communication()
        {
            Console.WriteLine("Communication .ctor");

            Console.WriteLine("Loaded assembly = {0}",GetType().Assembly);

            Console.WriteLine("Loaded assembly location = {0}", GetType().Assembly.Location);

            Console.WriteLine("exe path = {0}",AppDomain.CurrentDomain.BaseDirectory);
        }

        public void Initalize()
        {
            GlobalParam.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"));

            m_InnerCommunication = new MemeryShareCommunication();
            m_InnerCommunication.Received += InnerCommunicationOnReceived;
            m_InnerCommunication.Initalize(GlobalParam.Instance.MainConfig);

            Console.WriteLine("Communication Initalize");
        }

        private void InnerCommunicationOnReceived(byte[] bytes, float[] floats)
        {
            //Console.WriteLine("InnerCommunicationOnReceived bytes={0}\r\n floats={1}", string.Join(",", bytes),
            //    string.Join(",", floats));
            OnRecvEvent(bytes, bytes.Length, floats, floats.Length);
        }

        public void Uninitalize()
        {
            m_InnerCommunication.Uninitalize();

            Console.WriteLine("Communication Uninitalize");
        }

        public void SetRecvHandler(int address)
        {
            Console.WriteLine("Communication address, addr={0}", address);
            Recv cb = (Recv)Marshal.GetDelegateForFunctionPointer(new IntPtr(address), typeof(Recv));
            Console.WriteLine("Communication address, handler={0}", cb);
            RecvEvent += cb;
        }

        public void SendAsyn(byte[] bytes, float[] floats)
        {
            m_InnerCommunication.Send(bytes, floats);
        }

        protected virtual void OnRecvEvent(byte[] bools, int blength, float[] floats, int flength)
        {
            if (RecvEvent != null)
            {
                RecvEvent(bools, blength, floats, flength);
            }
        }
    }
}
