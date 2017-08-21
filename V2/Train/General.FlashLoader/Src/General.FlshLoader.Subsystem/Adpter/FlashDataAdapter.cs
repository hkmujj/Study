using System;
using System.Linq;
using System.Text;
using System.Timers;
using CommonUtil.Util;
using General.FlashLoader.Subsystem.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace General.FlashLoader.Subsystem.Adpter
{
    public class FlashDataAdapter : IDisposable
    {
        private readonly Timer m_SenderTimer;

        private readonly SubsystemInitParam m_SubsystemInitParam;

        private readonly IFlashInteractive m_FlashInteractive;

        private ICommunicationDataService DataService
        {
            get { return m_SubsystemInitParam.CommunicationDataService; }
        }

        private readonly StringBuilder m_InBoolStringBuilder;

        private readonly StringBuilder m_InFloatStringBuilder;

        private readonly int m_InBoolStart;
        private readonly int m_InBoolCount;
        private readonly int m_InFloatStart;
        private readonly int m_InFloatCount;

        private readonly int m_OutBoolStart;
        private readonly int m_OutBoolCount;
        private readonly int m_OutFloatStart;
        private readonly int m_OutFloatCount;


        public FlashDataAdapter(SubsystemInitParam subsystemInitParam, IFlashInteractive flashInteractive)
        {

            m_SubsystemInitParam = subsystemInitParam;

            m_FlashInteractive = flashInteractive;

            m_InBoolStart = DataService.ReadService.ReadOnlyBoolDictionary.Min(m => m.Key);
            m_InBoolCount = DataService.ReadService.ReadOnlyBoolDictionary.Max(m => m.Key) - m_InBoolStart;

            m_InFloatStart = DataService.ReadService.ReadOnlyFloatDictionary.Min(m => m.Key);
            m_InFloatCount = DataService.ReadService.ReadOnlyFloatDictionary.Max(m => m.Key) - m_InFloatStart + 1;

            m_OutBoolStart = DataService.WriteService.ReadOnlyBoolDictionary.Min(m => m.Key);
            m_OutBoolCount = DataService.WriteService.ReadOnlyBoolDictionary.Max(m => m.Key) - m_OutBoolStart;
            m_OutFloatStart = DataService.WriteService.ReadOnlyFloatDictionary.Min(m => m.Key);
            m_OutFloatCount = DataService.WriteService.ReadOnlyFloatDictionary.Max(m => m.Key) - m_OutFloatStart + 1;

            m_InBoolStringBuilder = new StringBuilder(m_InBoolCount);
            m_InFloatStringBuilder = new StringBuilder(m_InFloatCount);

            m_SenderTimer = new Timer(2000);
            m_SenderTimer.Elapsed += (sender, args) =>
            {
                SendBoolsToFlash();
                SendFloatsToFlash();
            };
        }

        public void Run()
        {
            DataService.ReadService.DataChanged += ReadServiceOnDataChanged;
            m_FlashInteractive.FlashDataRevceived += FlashInteractiveOnFlashDataRevceived;
        }

        public void FlashInteractiveOnFlashDataRevceived(FlashCommandType cmdType, string value)
        {
            AppLog.Debug("Receive data from flash , flash data type={0}", cmdType);
            switch (cmdType)
            {
                case FlashCommandType.OutBool:
                    RecvBoolsFromFlash(value);
                    break;
                case FlashCommandType.OutFloat:
                    RecvFloatsFromFlash(value);
                    break;
                default:
                    AppLog.Debug("Can not parser flash data where type={0}", cmdType);
                    break;
            }
        }

        private void RecvFloatsFromFlash(string value)
        {
            var fs = value.Split(';');

            for (int i = 0; i < Math.Min(m_OutFloatCount, fs.Length); i++)
            {
                var wi = m_OutFloatStart + i;
                if (SubSysParam.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary.Values.Contains(wi))
                {
                    DataService.WriteService.ChangeFloat(wi, Convert.ToSingle(fs[i]));
                }
            }
        }

        private void RecvBoolsFromFlash(string value)
        {
            for (int i = 0; i < Math.Min(m_OutBoolCount, value.Length); i++)
            {
                var wi = m_OutBoolStart + i;
                if (SubSysParam.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary.Values.Contains(wi))
                {
                    DataService.WriteService.ChangeBool(wi, value[i] == '1');
                }
            }
        }


        private void ReadServiceOnDataChanged(object sender, CommunicationDataChangedArgs communicationDataChangedArgs)
        {
            if (communicationDataChangedArgs.ChangedBools.NewValue.Any())
            {
                SendBoolsToFlash();
            }
            if (communicationDataChangedArgs.ChangedFloats.NewValue.Any())
            {
                SendFloatsToFlash();
            }
        }

        private void SendFloatsToFlash()
        {
            m_InFloatStringBuilder.Clear();
            for (int i = m_InFloatStart; i < m_InFloatCount; i++)
            {
                m_InFloatStringBuilder.Append(DataService.ReadService.GetFloatAt(i));
                m_InFloatStringBuilder.Append(";");
            }
            m_FlashInteractive.SetValue(FlashCommandType.InFloat, m_InFloatStringBuilder.ToString());
        }

        private void SendBoolsToFlash()
        {
            m_InBoolStringBuilder.Clear();
            for (int i = m_InBoolStart; i < m_InBoolCount; i++)
            {
                m_InBoolStringBuilder.Append(DataService.ReadService.GetBoolAt(i) ? 1 : 0);
            }
            m_FlashInteractive.SetValue(FlashCommandType.InBool, m_InBoolStringBuilder.ToString());
        }

        public void Dispose()
        {
            m_SenderTimer.Close();
        }
    }
}