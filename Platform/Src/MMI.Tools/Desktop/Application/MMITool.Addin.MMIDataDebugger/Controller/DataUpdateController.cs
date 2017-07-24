using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using DevExpress.Mvvm.POCO;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIDataDebugger.Model.Base;
using MMITool.Addin.MMIDataDebugger.ViewModel;
using Yunda.COM.MMICommunication;

namespace MMITool.Addin.MMIDataDebugger.Controller
{
    [Export]
    public class DataUpdateController : ControllerBase<DataViewModel>, IDisposable
    {
        private byte[] m_InBoolByteBuffer;
        private byte[] m_InFloatByteBuffer;

        private byte[] m_OutBoolByteBuffer;
        private byte[] m_OutFloatByteBuffer;

        private Timer m_SendDataTimer;

        private int m_IsSending;

        private ICommunication m_Communication;

        public void BoolsToBytes(IEnumerable<BoolItem> bools, byte[] bytes)
        {
            var bita = new BitArray(bools.Select(s => s.Value).ToArray());
            bita.CopyTo(bytes, 0);
        }

        public void FloatsToBytes(IEnumerable<FloatItem> floatItems, byte[] bytes)
        {
            using (var bw = new BinaryWriter(new MemoryStream(bytes)))
            {
                foreach (var it in floatItems)
                {
                    bw.Write(it.Value);

                }


                bw.Flush();
            }
        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            var dc = ViewModel.Model.DataCollection;
            m_InBoolByteBuffer =
                new byte[dc.InData.BoolItems.Value.Count/8 + (dc.InData.BoolItems.Value.Count%8 > 0 ? 1 : 0)];
            m_InFloatByteBuffer = new byte[dc.InData.FloatItems.Value.Count*4];

            m_OutBoolByteBuffer =
                new byte[dc.OutData.BoolItems.Value.Count/8 + (dc.OutData.BoolItems.Value.Count%8 > 0 ? 1 : 0)];
            m_OutFloatByteBuffer = new byte[dc.OutData.FloatItems.Value.Count*4];

            ThreadPool.QueueUserWorkItem(state =>
            {
                var communication = new Communication();
                communication.Initalize();
                Interlocked.Exchange(ref m_Communication, communication);

                m_SendDataTimer = new Timer(OnSendData, null, 3000, 20);
            });
        }

        private void OnSendData(object state)
        {
            if (m_IsSending > 0)
            {
                return;
            }

            Interlocked.Increment(ref m_IsSending);

            InDataToBytes();
            m_Communication.SendAsyn(m_InBoolByteBuffer,
                ViewModel.Model.DataCollection.InData.FloatItems.Value.Select(s => s.Value).ToArray());

            Interlocked.Decrement(ref m_IsSending);
        }

        private void InDataToBytes()
        {
            BoolsToBytes(ViewModel.Model.DataCollection.InData.BoolItems.Value, m_InBoolByteBuffer);
            //FloatsToBytes(ViewModel.Model.DataCollection.InData.FloatItems.Value, m_InFloatByteBuffer);
        }

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
            if (m_SendDataTimer != null)
            {
                m_SendDataTimer.Dispose();
                m_SendDataTimer = null;
            }
        }
    }
}