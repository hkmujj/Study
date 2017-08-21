using System;
using System.Text;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP._200H.Subsys.Model
{
    public class ATP200HAssistDisplayInfo : AssistDisplayInfo
    {
        private readonly StringBuilder m_CurrentSpeedBuffer;
        private readonly StringBuilder m_LimitedSpeedBuffer;
        private readonly StringBuilder m_CabSignalCodeTargetSpeedPairBuffer;
        private readonly StringBuilder m_TargetDistanceBuffer;

        public ATP200HAssistDisplayInfo(ATPDomain parent) : base(parent)
        {
            m_CurrentSpeedBuffer = new StringBuilder();
            m_LimitedSpeedBuffer = new StringBuilder();
            m_CabSignalCodeTargetSpeedPairBuffer = new StringBuilder();
            m_TargetDistanceBuffer = new StringBuilder();
        }

        public override string CurrentSpeed
        {
            get { return m_CurrentSpeed; }
            set
            {
                if (value == m_CurrentSpeed)
                {
                    return;
                }
                m_CurrentSpeed = InsertSpace(value, m_CurrentSpeedBuffer);
                RaisePropertyChanged(() => CurrentSpeed);
            }
        }

        public override string LimitedSpeed
        {
            get { return m_LimitedSpeed; }
            set
            {
                if (value == m_LimitedSpeed)
                {
                    return;
                }
                m_LimitedSpeed = InsertSpace(value, m_LimitedSpeedBuffer);
                RaisePropertyChanged(() => LimitedSpeed);
            }
        }

        public override string CabSignalCodeTargetSpeedPair
        {
            get { return m_CabSignalCodeTargetSpeedPair; }
            set
            {
                if (value == m_CabSignalCodeTargetSpeedPair)
                {
                    return;
                }
                m_CabSignalCodeTargetSpeedPair = InsertSpace(value, m_CabSignalCodeTargetSpeedPairBuffer, "   ");
                RaisePropertyChanged(() => CabSignalCodeTargetSpeedPair);
            }
        }

        public override string TargetDistance
        {
            get { return m_TargetDistance; }
            set
            {
                if (value == m_TargetDistance)
                {
                    return;
                }
                m_TargetDistance = InsertSpace(value, m_TargetDistanceBuffer);
                RaisePropertyChanged(() => TargetDistance);
            }
        }

        private string InsertSpace(string src, StringBuilder buffer, string end = "")
        {
            var s = src.Replace(" ", "");
            if (s == Full3Bit || s == Full4Bit)
            {
                return s;
            }

            buffer.Clear();
            foreach (var c in s)
            {
                buffer.Append(" ");
                buffer.Append(c);
            }
            buffer.Append(Enum.IsDefined(typeof(CabSignalCode), s) ? end : "");
            if (buffer.Length!=0)
            {
                buffer.Remove(0, 1);
            }
            return buffer.ToString();
        }
    }
}