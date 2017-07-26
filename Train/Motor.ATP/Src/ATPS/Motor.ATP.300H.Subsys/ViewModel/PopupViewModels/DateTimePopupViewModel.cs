using System;
using System.ComponentModel.Composition;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Control;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class DateTimePopupViewModel : DriverPopupViewModelBase
    {
        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private readonly IDriverInputInterpreter m_ControlWordInterpreter;
        private int m_Year;
        private int m_Second;
        private int m_Minute;
        private int m_Hour;
        private int m_Day;
        private int m_Month;

        public DateTimePopupViewModel()
        {
            m_DriverInputInterpreter = new DriverInputDataInterpreter(false);
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();
            PopupViewName = PopupContentViewNames.InputDateTimeView;
            TitleContent = PopupViewStringKeys.StringTitleInputingDateTime;
        }

        public int Year
        {
            set
            {
                if (value == m_Year)
                {
                    return;
                }
                m_Year = value;
                RaisePropertyChanged(() => Year);
            }
            get { return m_Year; }
        }

        public int Month
        {
            set
            {
                if (value == m_Month)
                {
                    return;
                }
                m_Month = value;
                RaisePropertyChanged(() => Month);
            }
            get { return m_Month; }
        }

        public int Day
        {
            set
            {
                if (value == m_Day)
                {
                    return;
                }
                m_Day = value;
                RaisePropertyChanged(() => Day);
            }
            get { return m_Day; }
        }

        public int Hour
        {
            set
            {
                if (value == m_Hour)
                {
                    return;
                }
                m_Hour = value;
                RaisePropertyChanged(() => Hour);
            }
            get { return m_Hour; }
        }

        public int Minute
        {
            set
            {
                if (value == m_Minute)
                {
                    return;
                }
                m_Minute = value;
                RaisePropertyChanged(() => Minute);
            }
            get { return m_Minute; }
        }

        public int Second
        {
            set
            {
                if (value == m_Second)
                {
                    return;
                }
                m_Second = value;
                RaisePropertyChanged(() => Second);
            }
            get { return m_Second; }
        }

        public DateTime DateTime
        {
            set
            {
                var dt = value;
                Year = dt.Year % 100;
                Month = dt.Month;
                Day = dt.Day;
                Hour = dt.Hour;
                Minute = dt.Minute;
                Second = dt.Second;
            }
            get { return new DateTime(Year, Month, Day, Hour, Minute, Second); }
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {

            base.ResponseAction(driverInterface);

            DateTime = driverInterface.ATP.Other.ShowingDateTime;
            var dt = driverInterface.ATP.Other.ShowingDateTime;
            Year = dt.Year%100;
            Month = dt.Month;
            Day = dt.Day;
            Hour = dt.Hour;
            Minute = dt.Minute;
            Second = dt.Second;


        }
        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            if (args.MouseState == MouseState.MouseUp)
            {
                var rlt = m_DriverInputInterpreter.Interpreter(args.ActionType);

                if (rlt.DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
                {
                    DateTime = DateTime.Add(TimeSpan.FromHours(1));
                    return;
                }

                rlt = m_ControlWordInterpreter.Interpreter(args.ActionType);
                if (rlt.DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
                {
                    UpdateTimeSpan(rlt);
                }
            }
        }

        private void UpdateTimeSpan(DriverInputInterpreterResult rlt)
        {
            if (rlt.DriverInputType == DriverInputInterpreterResult.InputType.Control )
            {
                var word = rlt.GetControlWord();
                switch (word)
                {
                    case DriverInputControlWord.Ok:
                        ATP.Other.ShowingTimeDifference = DateTime - ATP.Other.NowInATP;
                        break;
                    case DriverInputControlWord.Cancel:
                        
                        break;
                    case DriverInputControlWord.Delete:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }   
        }
    }
}