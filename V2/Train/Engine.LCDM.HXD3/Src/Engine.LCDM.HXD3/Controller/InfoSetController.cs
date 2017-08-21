using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.LCDM.HXD3.Common;
using Engine.LCDM.HXD3.Config;
using Engine.LCDM.HXD3.Constance;
using Engine.LCDM.HXD3.Enums;
using Engine.LCDM.HXD3.Events;
using Engine.LCDM.HXD3.Extentions;
using Engine.LCDM.HXD3.Resource;
using Engine.LCDM.HXD3.ViewModels;
using Engine.LCDM.HXD3.Views.BottomButton;
using Excel.Interface;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.LCDM.HXD3.Controller
{
    [Export(typeof(InfoSetController))]
    public class InfoSetController : ControllerBase<InfoSet>
    {
        [ImportingConstructor]
        public InfoSetController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            Increase = new DelegateCommand(IncreaseAction);
            Decrease = new DelegateCommand(DecreaseAction);
            MoveLeft = new DelegateCommand(MoveLeftAction);
            MoveRight = new DelegateCommand(MoveRightAction);

            LanguageChangeCommand = new DelegateCommand(ChangeLangAction);
            ExitCommand = new DelegateCommand(ExitAction);

            DateAdd = new DelegateCommand(DateAddAction);
            DateDecreace = new DelegateCommand(DateDecreaseAction);
            DateMoveLeft = new DelegateCommand(DateMoveLeftAction);
            DateMoveRight = new DelegateCommand(DateMoveRightAction);
            eventAggregator.GetEvent<VieChangedNotifiEvent>().Subscribe((args) =>
            {
                if (args.FullName.Equals(ViewNames.DateSetingButton))
                {
                    ViewModel.Years = ViewModel.DisplayTime.Year;
                    ViewModel.Months = ViewModel.DisplayTime.Month;
                    ViewModel.Days = ViewModel.DisplayTime.Day;
                    ViewModel.Hours = ViewModel.DisplayTime.Hour;
                    ViewModel.Minutes = ViewModel.DisplayTime.Minute;
                    ViewModel.Seconds = ViewModel.DisplayTime.Second;
                    ViewModel.DiffTime = ViewModel.DisplayTime;
                }
            });
            var tmp = ExcelParser.Parser<InitialSet>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            InitialCollection = new ObservableCollection<InitialSet>(tmp);
            InitDicToDictionary = InitialCollection.ToDictionary(t => t.Name, t => t.Content);
            if (InitDicToDictionary.ContainsKey("机车号"))
            {
                string trainInfo=InitDicToDictionary["机车号"];
                char[] chx=new char[8];
                trainInfo.CopyTo(0,chx,0,trainInfo.Length);
                var flt = ConvertCharArray(chx);
                flt[0].SendData(OutFloatKeys.机车号1);
                flt[1].SendData(OutFloatKeys.机车号2);

            }
        }

        private void IncreaseAction()
        {
            switch (ViewModel.Index)
            {
                case 0:
                    if (ViewModel.NumbOne < 9)
                        ViewModel.NumbOne++;
                    break;
                case 1:
                    if (ViewModel.NumbTwo < 9)
                        ViewModel.NumbTwo++;
                    break;
                case 2:
                    if (ViewModel.NumbThree < 9)
                        ViewModel.NumbThree++;
                    break;
                case 3:
                    if (ViewModel.NumbFour < 9) 
                        ViewModel.NumbFour++;
                    break;
                case 4:
                    if(ViewModel.NumbFive<90)
                    ViewModel.NumbFive ++;
                    else
                    {
                        ViewModel.NumbFive = 'A';
                    }
                    break;
                case 5:
                    if(ViewModel.NumbSix<90)
                    ViewModel.NumbSix++;
                    else
                    {
                        ViewModel.NumbSix = 'A';
                    }
                    break;
                default:
                    break;
            }
            ChangedTrainBumber();
        }
        public char[] ConvertFloatArray(float[] data)
        {
            return data.SelectMany(BitConverter.GetBytes).Select(s => BitConverter.ToChar(new byte[] { s, 0 }, 0)).ToArray();
        }
        public void ChangedTrainBumber()
        {

            var str = ViewModel.NumbOne.ToString() + ViewModel.NumbTwo.ToString() + ViewModel.NumbThree.ToString() + ViewModel.NumbFour.ToString() + ViewModel.NumbFive.ToString() + ViewModel.NumbSix.ToString();
            ViewModel.TrainNum = str;
            char[] ch = new char[8];

            str.CopyTo(0, ch, 0, str.Length);

            var flt = ConvertCharArray(ch);

            flt[0].SendData(OutFloatKeys.机车号1);
            flt[1].SendData(OutFloatKeys.机车号2);
        }
        public float[] ConvertCharArray(char[] data)
        {
            var ba = data.Select(s => BitConverter.GetBytes(s)[0]).ToArray();
            var rlt = new List<float>(ba.Length / 4);
            for (int i = 0; i < ba.Length; i += 4)
            {
                rlt.Add(BitConverter.ToSingle(ba, i));
            }
            return rlt.ToArray();
        }
        private void DecreaseAction()
        {
            switch (ViewModel.Index)
            {
                case 0:
                    if (ViewModel.NumbOne > 0)
                        ViewModel.NumbOne--;
                    break;
                case 1:
                    if (ViewModel.NumbTwo > 0)
                        ViewModel.NumbTwo--;
                    break;
                case 2:
                    if (ViewModel.NumbThree > 0)
                        ViewModel.NumbThree--;
                    break;
                case 3:
                    if (ViewModel.NumbFour > 0)
                        ViewModel.NumbFour--;
                    break;
                case 4:
                    if (ViewModel.NumbFive > 65)
                        ViewModel.NumbFive--;
                    else
                    {
                        ViewModel.NumbFive = 'Z';
                    }
                    break;
                case 5:
                    if (ViewModel.NumbSix > 65)
                        ViewModel.NumbSix--;
                    else
                    {
                        ViewModel.NumbSix = 'Z';
                    }
                    break;
                default:
                    break;
            }
            ChangedTrainBumber();
        }

        private void MoveLeftAction()
        {
            if (ViewModel.Index == 0)
            {
                ViewModel.Index = 5;
                return;
            }
            ViewModel.Index--;
        }

        private void MoveRightAction()
        {
            if (ViewModel.Index == 5)
            {
                ViewModel.Index = 0;
                return;
            }
            ViewModel.Index++;
        }
        private void ChangeLangAction()
        {
            ViewModel.NewIsCh = !ViewModel.NewIsCh;
            if (ViewModel.NewIsCh)
            {
                LCDMResourceManager.ChangedLanguage(Language.En);
                return;
            }
            LCDMResourceManager.ChangedLanguage(Language.Ch);
        }
        private void ExitAction()
        {
            ViewModel.ViewModel.Controller.Navigator.Execute(typeof(MainBottomButton).FullName);
            ViewModel.CurIsCh = ViewModel.NewIsCh;
        }

        private void ChangedDateTime()
        {
            ViewModel.DiffTime = new DateTime(ViewModel.Years, ViewModel.Months, ViewModel.Days, ViewModel.Hours, ViewModel.Minutes, ViewModel.Seconds);
            ViewModel.DiffSpan = ViewModel.DiffTime - DateTime.Now;
        }
        private void DateAddAction()
        {
            switch (ViewModel.IndexDate)
            {
                case 0:
                    ViewModel.Years += 1;


                    break;
                case 2:
                    if (ViewModel.Months < 12)
                        ViewModel.Months += 1;
                    else
                    {
                        ViewModel.Months = 1;
                    }
                    break;
                case 4:
                    if (ViewModel.Days < 31)
                        ViewModel.Days += 1;
                    else
                    {
                        ViewModel.Days = 1;
                    }
                    break;
                case 6:
                    if (ViewModel.Hours < 23)
                        ViewModel.Hours += 1;
                    else
                    {
                        ViewModel.Hours = 0;
                    }
                    break;
                case 8:
                    if (ViewModel.Minutes < 59)
                    {
                        ViewModel.Minutes += 1;
                    }
                    else
                    {
                        ViewModel.Minutes = 0;
                    }
                    break;
                case 10:
                    if (ViewModel.Seconds < 59)
                        ViewModel.Seconds += 1;
                    else
                    {
                        ViewModel.Seconds = 0;
                    }
                    break;
                default:
                    break;
            }
            ChangedDateTime();
        }

        private void DateDecreaseAction()
        {
            switch (ViewModel.IndexDate)
            {
                case 0:
                    ViewModel.Years -= 1;
                    break;
                case 2:
                    if (ViewModel.Months > 1)
                        ViewModel.Months -= 1;
                    else
                    {
                        ViewModel.Months = 12;
                    }
                    break;
                case 4:
                    if (ViewModel.Days > 1)
                        ViewModel.Days -= 1;
                    else
                    {
                        ViewModel.Days = 31;
                    }
                    break;
                case 6:
                    if (ViewModel.Hours > 0)
                        ViewModel.Hours -= 1;
                    else
                    {
                        ViewModel.Hours = 23;
                    }
                    break;
                case 8:
                    if (ViewModel.Minutes > 0)
                    {
                        ViewModel.Minutes -= 1;
                    }
                    else
                    {
                        ViewModel.Minutes = 59;
                    }
                    break;
                case 10:
                    if (ViewModel.Seconds > 0)
                        ViewModel.Seconds -= 1;
                    else
                    {
                        ViewModel.Seconds = 59;
                    }
                    break;
                default:
                    break;
            }
            ChangedDateTime();

        }

        private void DateMoveLeftAction()
        {
            if (ViewModel.IndexDate > 0)
            {
                ViewModel.IndexDate -= 2;
                return;
            }
            ViewModel.IndexDate = 10;
        }

        private void DateMoveRightAction()
        {
            ViewModel.IndexDate += 2;
            if (ViewModel.IndexDate > 10)
                ViewModel.IndexDate = 0;
        }

        public ICommand Decrease { get; private set; }
        public ICommand Increase { get; private set; }
        public ICommand MoveLeft { get; private set; }
        public ICommand MoveRight { get; private set; }
        public ICommand LanguageChangeCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand DateAdd { get; private set; }
        public ICommand DateDecreace { get; private set; }
        public ICommand DateMoveLeft { get; private set; }
        public ICommand DateMoveRight { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
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