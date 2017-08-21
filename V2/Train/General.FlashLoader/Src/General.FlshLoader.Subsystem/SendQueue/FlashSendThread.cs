using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using CommonUtil.Util;
using flash;
using General.FlashLoader.Subsystem.Model;

namespace General.FlashLoader.Subsystem.SendQueue
{
    public class FlashSendThread : BackgroundWorker
    {
        private readonly ConcurrentQueue<FlashData> m_CmdQueue;

        private readonly FlashWindow m_FlashInteractive;

        private volatile bool m_Running;

        private static readonly TimeSpan CheckInterval = new TimeSpan(0, 0, 0, 0, 20);

        public void Push(FlashData data)
        {
            m_CmdQueue.Enqueue(data);
        }

        public FlashSendThread(FlashWindow flashInteractive)
        {
            m_FlashInteractive = flashInteractive;
            m_Running = true;
            if (m_FlashInteractive == null)
            {
                AppLog.Error("IFlashInteractive is null");
            }
            m_CmdQueue = new ConcurrentQueue<FlashData>();
        }

        protected override void OnDoWork(DoWorkEventArgs args)
        {
            AppLog.Info("Flash sender thread start to work sucess.");

            while (m_Running)
            {
                if (m_CmdQueue.Any())
                {
                    FlashData data;
                    if (m_CmdQueue.TryDequeue(out data))
                    {
                        try
                        {
                            AppLog.Debug("Flash sender thread send values, {0}={1}", data.CommandType, data.Value);
                            m_FlashInteractive.SetValue(data.CommandType.ToString(), data.Value);
                        }
                        catch (Exception e)
                        {
                            AppLog.Info("Set flash vaule error,m_FlashWindow.SetValue({0}, {1}), {2}", data.CommandType, data.Value, e.ToString());
                        }
                    }
                    else
                    {
                        AppLog.Warn("Can not dequeue flash data from ConcurrentQueue, but the queue has {0} item(s).",
                            m_CmdQueue.Count);
                    }
                }
                else
                {
                    Thread.Sleep(CheckInterval);
                }
            }
        }

        public void Stop()
        {
            m_Running = false;
        }
    }
}