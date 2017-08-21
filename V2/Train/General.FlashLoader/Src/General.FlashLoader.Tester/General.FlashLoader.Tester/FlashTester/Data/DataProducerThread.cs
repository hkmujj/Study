using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using Yunda.FlashTester.Model;
using Yunda.FlashTester.SendQueue;

namespace Yunda.FlashTester.Data
{
    public class DataProducerThread
    {
        private readonly Timer m_DataTimer;

        private readonly bool[] m_InBools;

        public float[] InFloats { get; private set; }

        private readonly FlashSendThread m_SendThread;

        private const int ScreenLight = 416;

        private const int ActSpeed = 97;
        private const int SetSpeed = 98;

        private readonly StringBuilder m_InBoolStringBuilder;

        private readonly StringBuilder m_InFloatStringBuilder;

        private Random m_Random = new Random();

        public event Action<DataProducerThread> DataProducted;

        public DataProducerThread(FlashSendThread sendThread)
        {
            m_SendThread = sendThread;
            m_InBools = Enumerable.Range(1, 1600).Select(s => s % 3 == 0).ToArray();

            InFloats = Enumerable.Range(1, 600).Select(s => (float)s).ToArray();

            m_InBoolStringBuilder = new StringBuilder(m_InBools.Count());
            m_InFloatStringBuilder = new StringBuilder(InFloats.Count());

            m_DataTimer = new Timer(20) { AutoReset = true };
            m_DataTimer.Elapsed += DataTimerOnElapsed;
            m_DataTimer.Start();
        }

        private void DataTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            OnDataChanged();
        }

        private void OnDataChanged()
        {
            ProductData();

            OnDataProducted(this);

            SendData();
        }

        private void SendData()
        {
            SendBoolsToFlash();

            SendFloatsToFlash();
        }


        private void SendFloatsToFlash()
        {
            m_InFloatStringBuilder.Clear();
            for (int i = 0; i < InFloats.Length; i++)
            {
                m_InFloatStringBuilder.Append(InFloats[i]);
                m_InFloatStringBuilder.Append(";");
            }
            //m_FlashInteractive.SetValue(FlashCommandType.InFloat, m_InFloatStringBuilder.ToString());
            m_SendThread.Push(new FlashData(FlashCommandType.InFloat, m_InFloatStringBuilder.ToString()));
        }

        private void SendBoolsToFlash()
        {

            //点亮屏
//            for (int i = 400; i < 450; i++)
//            {
//                m_InBools[i] = false;
//            }


            m_InBoolStringBuilder.Clear();
            for (int i = 0; i < m_InBools.Length; i++)
            {
                m_InBoolStringBuilder.Append(m_InBools[i] ? 1 : 0);
            }

            m_SendThread.Push(new FlashData(FlashCommandType.InBool, m_InBoolStringBuilder.ToString()));

            Debug.Write(string.Format("push data {0}. {1} \r\n", DateTime.Now, DateTime.Now.Millisecond));
        }

        private void ProductData()
        {
            for (int i = 0; i < m_InBools.Length; i++)
            {
                m_InBools[i] = !m_InBools[i];
            }
            m_InBools[ScreenLight] = true;

            for (int i = 89; i < InFloats.Length; i++)
            {
                if (i != ActSpeed && i != SetSpeed)
                {
                    InFloats[i] = (InFloats[i] + 10)%300 + (float) m_Random.Next(1000)/1000;
                }
            }

            InFloats[ActSpeed] = (InFloats[ActSpeed] + 1) % 15530;
            InFloats[SetSpeed] = (InFloats[SetSpeed] + 1) % 15530;

        }

        protected virtual void OnDataProducted(DataProducerThread obj)
        {
            var handler = DataProducted;
            if (handler != null)
            {
                handler(obj);
            }
        }
    }
}