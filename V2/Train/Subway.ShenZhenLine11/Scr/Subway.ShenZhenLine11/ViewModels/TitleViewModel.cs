using System;
using System.ComponentModel.Composition;
using System.Threading;
using MMI.Facility.Interface.Data;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(TitleViewModel))]
    public class TitleViewModel : SubViewModelBase
    {
        private Timer m_Timer;
        private DateTime m_Time;
        private string m_TitleName;

        public string TitleName
        {
            get { return m_TitleName; }
            set
            {
                if (value == m_TitleName)
                {
                    return;
                }
                m_TitleName = value;
                RaisePropertyChanged(() => TitleName);
            }
        }
        public string DayOfWeak { get; set; }

        private void SetDay()
        {
            switch (Time.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    DayOfWeak = "星期天";
                    break;
                case DayOfWeek.Monday:
                    DayOfWeak = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    DayOfWeak = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    DayOfWeak = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    DayOfWeak = "星期四";
                    break;
                case DayOfWeek.Friday:
                    DayOfWeak = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    DayOfWeak = "星期六";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public DateTime Time
        {
            get { return m_Time; }
            set
            {
                if (value.Equals(m_Time))
                {
                    return;
                }
                m_Time = value;
                SetDay();
                RaisePropertyChanged(() => Time);
            }
        }

        public TitleViewModel()
        {
            m_Timer = new Timer((state) =>
            {
                Time = DateTime.Now;
            }, null, 2000, 1000);
        }
        public override void Clear()
        {
            base.Clear();
            Clear(typeof(TitleViewModel), this);
        }
        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(sender, args);
        }

        public override void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {
            base.Changed(sender, args);
        }
    }
}