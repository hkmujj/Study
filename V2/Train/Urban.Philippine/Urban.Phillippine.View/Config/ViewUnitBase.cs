using System;
using System.Collections.Generic;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Extend;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config
{
    public class ViewUnitBase : NotificationObject, IUnit
    {
        protected FaultSignalStatus CurrentFaultSignalStatus = Interface.Enum.FaultSignalStatus.Normal;
        protected double CurrentFloat;
        protected SignalStatus CurrentSignalStatus = Interface.Enum.SignalStatus.Lowlevel;
        private double? m_F;
        private FaultSignalStatus? m_FaultSignalStatus;
        private SignalStatus? m_SignalStatus;
        private SignalType m_SignalType;

        public SignalStatus? SignalStatus
        {
            get { return m_SignalStatus; }
            set
            {
                if (value == m_SignalStatus)
                {
                    return;
                }
                m_SignalStatus = value;
                RaisePropertyChanged(() => SignalStatus);
            }
        }

        public FaultSignalStatus? FaultSignalStatus
        {
            get { return m_FaultSignalStatus; }
            set
            {
                if (value == m_FaultSignalStatus)
                {
                    return;
                }
                m_FaultSignalStatus = value;
                RaisePropertyChanged(() => FaultSignalStatus);
            }
        }

        public double? Float
        {
            get { return m_F; }
            set
            {
                if (value.Equals(m_F))
                {
                    return;
                }
                m_F = value;
                RaisePropertyChanged(() => Float);
            }
        }

        [ExcelField("SignalType")]
        public SignalType SignalType
        {
            get { return m_SignalType; }
            set { m_SignalType = value; }
        }

        [ExcelField("Name")]
        public string LogicName { get; set; }

        [ExcelField("Train")]
        public int Train { get; set; }

        public virtual void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "SignalType":
                    Enum.TryParse(value, true, out m_SignalType);
                    break;

                case "LogicName":
                    LogicName = value;
                    break;

                case "Train":
                    Train = value.ConvertToInt();
                    break;
            }
        }

        public virtual void Clear()
        {
            switch (SignalType)
            {
                case SignalType.UnKnow:
                    break;

                case SignalType.Signal:
                    SignalStatus = null;
                    break;

                case SignalType.FaultSignal:
                    FaultSignalStatus = null;
                    break;

                case SignalType.Float:
                    Float = null;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual void Clear(object obj, Type type)
        {
        }

        public virtual void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            if (SignalType == SignalType.Float)
            {
                return;
            }
            //switch (SignalType)
            //{
            //    case SignalType.UnKnow:
            //        break;

            //    case SignalType.Signal:
            //        SignalStatus = CurrentSignalStatus;
            //        break;

            //    case SignalType.FaultSignal:
            //        FaultSignalStatus = CurrentFaultSignalStatus;
            //        break;

            //    case SignalType.Float:
            //        break;
            //}
            GetValues(args);
            if (iPara != null && Train <= iPara)
            {
                SetValue();
            }
            else
            {
                switch (SignalType)
                {
                    case SignalType.UnKnow:
                        break;

                    case SignalType.Signal:
                        SignalStatus = null;
                        break;

                    case SignalType.FaultSignal:
                        FaultSignalStatus = null;
                        break;

                    case SignalType.Float:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void SetValue()
        {
            switch (SignalType)
            {
                case SignalType.UnKnow:
                    break;

                case SignalType.Signal:
                    SignalStatus = CurrentSignalStatus;
                    break;

                case SignalType.FaultSignal:
                    FaultSignalStatus = CurrentFaultSignalStatus;
                    break;

                case SignalType.Float:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GetValues(IDictionary<int, bool> args)
        {
            var index = IndexConfigure.InBoolIndex[LogicName];
            if (args.ContainsKey(index))
            {
                switch (SignalType)
                {
                    case SignalType.UnKnow:
                        break;
                    case SignalType.Signal:
                        CurrentSignalStatus = args[index]
                            ? Interface.Enum.SignalStatus.HighLevel
                            : Interface.Enum.SignalStatus.Lowlevel;
                        break;

                    case SignalType.FaultSignal:
                        CurrentFaultSignalStatus = args[index]
                            ? Interface.Enum.FaultSignalStatus.Fault
                            : Interface.Enum.FaultSignalStatus.Normal;
                        break;

                    case SignalType.Float:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public virtual void Changed(IDictionary<int, float> args, int? ipara = null)
        {
            if (SignalType != SignalType.Float)
            {
                return;
            }
            switch (SignalType)
            {
                case SignalType.UnKnow:
                    break;

                case SignalType.Signal:
                    break;

                case SignalType.FaultSignal:
                    break;

                case SignalType.Float:
                    Float = CurrentFloat;
                    break;
            }
            var index = IndexConfigure.IntFloatIndex[LogicName];
            if (args.ContainsKey(index))
            {
                CurrentFloat = args[index];
            }
            if (ipara != null && Train <= ipara && SignalType == SignalType.Float)
            {
                Float = CurrentFloat;
            }
            else
            {
                Float = null;
            }
        }
    }
}