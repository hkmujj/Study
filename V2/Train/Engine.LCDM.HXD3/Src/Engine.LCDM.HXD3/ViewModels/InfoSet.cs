using Engine.LCDM.HXD3.Config;
using Engine.LCDM.HXD3.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using Engine.LCDM.HXD3.Common;
using Engine.LCDM.HXD3.Controller;
using Engine.LCDM.HXD3.Events;
using Engine.LCDM.HXD3.Interfaces;
using Excel.Interface;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HXD3.ViewModels
{
    [Export(typeof(InfoSet))]
    public class InfoSet : ViewModelBase, IViewModelExtention<LCDMViewModel>
    {
        private Timer m_Timer;
        [ImportingConstructor]
        public InfoSet(InfoSetController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            m_Timer = new Timer((state => DisplayTime = DateTime.Now + DiffSpan), null, 1000, 1000);
            controller.ChangedTrainBumber();
            var tmp = ExcelParser.Parser<InitialSet>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            InitialCollection = new ObservableCollection<InitialSet>(tmp);
            InitDicToDictionary = InitialCollection.ToDictionary(t => t.Name, t => t.Content);
            if (InitDicToDictionary.ContainsKey("机车号"))
            {
                TrainNum = InitDicToDictionary["机车号"];
                NumbOne = int.Parse((TrainNum[0].ToString()));
                NumbTwo = int.Parse((TrainNum[1].ToString()));
                NumbThree = int.Parse((TrainNum[2].ToString()));
                NumbFour = int.Parse((TrainNum[3].ToString()));
                NumbFive = TrainNum[4];
                NumbSix = TrainNum[5];
            }
        }

        public InfoSetController Controller { get; private set; }

        public int NumbOne
        {
            get { return m_NumbOne; }
            set
            {
                if (value == m_NumbOne)
                    return;
                m_NumbOne = value;
                RaisePropertyChanged(() => NumbOne);
            }
        }

        private int m_NumbOne;

        public int NumbTwo
        {
            get { return m_NumbTwo; }
            set
            {
                if (value == m_NumbTwo) return;
                m_NumbTwo = value;
                RaisePropertyChanged(() => NumbTwo);
            }
        }

        private int m_NumbTwo;

        public int NumbThree
        {
            get { return m_NumbThree; }
            set
            {
                if (value == m_NumbThree) return;
                m_NumbThree = value;
                RaisePropertyChanged(() => NumbThree);
            }
        }

        private int m_NumbThree;

        public int NumbFour
        {
            get { return m_NumbFour; }
            set
            {
                if (value == m_NumbFour) return;
                m_NumbFour = value;
                RaisePropertyChanged(() => NumbFour);
            }
        }

        private int m_NumbFour;

        public Char NumbFive
        {
            get { return m_NumbFive; }
            set
            {
                if (value == m_NumbFive) return;
                m_NumbFive = value;
                RaisePropertyChanged(() => NumbFive);
            }
        }

        private Char m_NumbFive;

        public Char NumbSix
        {
            get { return m_NumbSix; }
            set
            {
                if (value == m_NumbSix) return;
                m_NumbSix = value;
                RaisePropertyChanged(() => NumbSix);
            }
        }

        private Char m_NumbSix;

        public int Index
        {
            get { return m_Index; }
            set
            {
                if (value == m_Index)
                {
                    return;
                }
                m_Index = value;
                ChangedIndex();
                RaisePropertyChanged(() => Index);
            }
        }

        private int m_Index;

        public int Years
        {
            get { return m_Years; }
            set
            {
                if (value == m_Years)
                {
                    return;
                }
                m_Years = value;
                YearsString = value < 10 ? ("0" + value.ToString()) : (value.ToString());
            }
        }

        private int m_Years = 2016;

        public int Months
        {
            get { return m_Months; }
            set
            {
                if (value == m_Months)
                {
                    return;
                }
                m_Months = value;
                MonthsString = value < 10 ? ("0" + value.ToString()) : (value.ToString());
            }
        }

        private int m_Months = 6;

        public int Days
        {
            get { return m_Days; }
            set
            {
                if (value == m_Days)
                {
                    return;
                }
                m_Days = value;
                DaysString = value < 10 ? ("0" + value.ToString()) : (value.ToString());
            }
        }

        private int m_Days = 15;

        public int Hours
        {
            get { return m_Hours; }
            set
            {
                if (value == m_Hours)
                {
                    return;
                }
                m_Hours = value;
                HoursString = value < 10 ? ("0" + value.ToString()) : (value.ToString());
            }
        }

        private int m_Hours = 15;

        public int Minutes
        {
            get { return m_Minutes; }
            set
            {
                if (value == m_Minutes)
                {
                    return;
                }
                m_Minutes = value;
                MinutesString = value < 10 ? ("0" + value.ToString()) : (value.ToString());
            }
        }

        private int m_Minutes = 13;

        public int Seconds
        {
            get { return m_Seconds; }
            set
            {
                if (value == m_Seconds)
                {
                    return;
                }
                m_Seconds = value;
                SecondsString = value < 10 ? ("0" + value.ToString()) : (value.ToString());
            }
        }
        private int m_Seconds = 39;

        public int IndexDate
        {
            get { return m_IndexDate; }
            set
            {
                if (value == m_IndexDate)
                {
                    return;
                }
                m_IndexDate = value;
                ChangedIndexDate();
                RaisePropertyChanged(() => IndexDate);
            }
        }
        private int m_IndexDate;

        public string YearsString
        {
            get { return m_YearsString; }
            set
            {
                if (value == m_YearsString)
                {
                    return;
                }
                m_YearsString = value;
                RaisePropertyChanged(() => YearsString);
            }
        }
        private string m_YearsString = "2016";

        public string MonthsString
        {
            get { return m_MonthsString; }
            set
            {
                if (value == m_MonthsString)
                {
                    return;
                }
                m_MonthsString = value;
                RaisePropertyChanged(() => MonthsString);
            }
        }
        private string m_MonthsString = "06";

        public string DaysString
        {
            get { return m_DaysString; }
            set
            {
                if (value == m_DaysString)
                {
                    return;
                }
                m_DaysString = value;
                RaisePropertyChanged(() => DaysString);
            }
        }
        private string m_DaysString = "15";

        public string HoursString
        {
            get { return m_HoursString; }
            set
            {
                if (value == m_HoursString)
                {
                    return;
                }
                m_HoursString = value;
                RaisePropertyChanged(() => HoursString);
            }
        }

        private string m_HoursString = "15";

        public string MinutesString
        {
            get { return m_MinutesString; }
            set
            {
                if (value == m_MinutesString)
                {
                    return;
                }
                m_MinutesString = value;
                RaisePropertyChanged(() => MinutesString);
            }
        }
        private string m_MinutesString = "13";

        public string SecondsString
        {
            get { return m_SecondsString; }
            set
            {
                if (value == m_SecondsString)
                {
                    return;
                }
                m_SecondsString = value;
                RaisePropertyChanged(() => SecondsString);
            }
        }
        private string m_SecondsString = "39";
        private void ChangedIndex()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<PictureIndexChanged>().Publish(new PictrueIndexChangedArgs() { Index = Index });
        }

        private void ChangedIndexDate()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<PictureIndexChanged>().Publish(new PictrueIndexChangedArgs() { DateIndex = IndexDate });
        }

        public bool CurIsCh
        {
            get { return m_CurIsCh; }
            set
            {
                if (value == m_CurIsCh)
                {
                    return;
                }
                m_CurIsCh = value;
                RaisePropertyChanged(() => CurIsCh);
            }
        }
        private bool m_CurIsCh;
        public bool NewIsCh
        {
            get { return m_NewIsCh; }
            set
            {
                if (value == m_NewIsCh)
                {
                    return;
                }
                m_NewIsCh = value;
                RaisePropertyChanged(() => NewIsCh);
            }
        }
        private bool m_NewIsCh;
        private DateTime m_DisplayTime;
        private string m_TrainNum;

        public LCDMViewModel ViewModel { get; set; }

        public DateTime DisplayTime
        {
            get { return m_DisplayTime; }
            set
            {
                if (value.Equals(m_DisplayTime))
                {
                    return;
                }
                m_DisplayTime = value;
                RaisePropertyChanged(() => DisplayTime);
            }
        }

        public TimeSpan DiffSpan { get; set; }
        public DateTime DiffTime { get; set; }

        public string TrainNum
        {
            get { return m_TrainNum; }
            set
            {
                if (value == m_TrainNum)
                {
                    return;
                }
                m_TrainNum = value;
                RaisePropertyChanged(() => TrainNum);
            }
        }
        public ObservableCollection<InitialSet> InitialCollection
        {
            get { return m_InitialCollection; }
            set
            {
                if (Equals(value, m_InitialCollection))
                {
                    return;
                }
                m_InitialCollection = value;
                RaisePropertyChanged(() => InitialCollection);
            }
        }
        private ObservableCollection<InitialSet> m_InitialCollection;
        public Dictionary<string, string> InitDicToDictionary; 
    }
}
