using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.GIS.Config;
using Subway.WuHanLine6.GIS.Event;
using Subway.WuHanLine6.GIS.ViewModels;

namespace Subway.WuHanLine6.GIS.Controller
{
    [Export]
    public class WuHanLine6GisController : ControllerBase<Lazy<WuHanLine6GisViewModel>>
    {
        protected IEventAggregator EventAggregator { get; private set; }
        [ImportingConstructor]
        public WuHanLine6GisController(Lazy<WuHanLine6GisViewModel> viewModel, IEventAggregator eventAggregator) : base(viewModel)
        {
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<ArrowChangedEvent>().Subscribe(ArrowChanged);
            Task.Factory.StartNew(NextStationChaang);
        }

        private void NextStationChaang()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            var station = ViewModel.Value.NextStation;
            var index = 0;
            var isEngilsh = false;
            var isEnd = false;
            var enStr = GetStationName(station, true);
            var chStr = GetStationName(station);
            while (true)
            {
                int milis = 0;

                if (isEnd)
                {
                    milis = 2000;
                }
                else
                {
                    milis = isEngilsh ? 100 : 150;
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(milis));
                if (enStr.Length > 0 && chStr.Length >= 0)
                {
                    if (isEnd)
                    {
                        isEnd = false;
                        isEngilsh = !isEngilsh;
                        index = 0;
                    }
                    if (ViewModel.Value.NextStation != station)
                    {
                        station = ViewModel.Value.NextStation;
                        enStr = GetStationName(station, true);
                        chStr = GetStationName(station);
                        index = 0;
                        isEnd = false;
                        isEngilsh = false;
                        index = 0;

                    }
                    if (isEngilsh)
                    {
                        isEnd = enStr.Length < index;
                        if (isEnd)
                        {
                            continue;
                        }
                        ViewModel.Value.NextStationString = enStr.Substring(0, index);
                    }
                    else
                    {
                        isEnd = chStr.Length < index;
                        if (isEnd)
                        {
                            continue;
                        }
                        ViewModel.Value.NextStationString = chStr.Substring(0, index);
                    }
                    index++;
                }
              
            }
        }

        private string GetStationName(StationName name, bool isEnglsh = false)
        {
            if (name == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            if (isEnglsh)
            {
                result += "Next Station:";
                result += name.EnglishName;
                result += " ";
                result += name.IsTransfer ? $"Transfer Line {name.TransferLineIndex}" : "";
            }
            else
            {
                result += "下一站：";
                result += name.ChineseName;
                result += " ";
                result += name.IsTransfer ? $"可换乘{name.TransferLineIndex}号线" : "";
            }
            return result;
        }
        private bool falg;

        private void ArrowChanged()
        {
            int index = 0;
            while (falg)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (index == 0)
                {
                    ViewModel.Value.IsArrow1 = true;
                    ViewModel.Value.IsArrow2 = false;
                    ViewModel.Value.IsArrow3 = false;
                    ViewModel.Value.IsArrow4 = false;
                }
                if (index == 1)
                {
                    ViewModel.Value.IsArrow1 = true;
                    ViewModel.Value.IsArrow2 = true;
                    ViewModel.Value.IsArrow3 = false;
                    ViewModel.Value.IsArrow4 = false;
                }
                if (index == 2)
                {
                    ViewModel.Value.IsArrow1 = true;
                    ViewModel.Value.IsArrow2 = true;
                    ViewModel.Value.IsArrow3 = true;
                    ViewModel.Value.IsArrow4 = false;
                }
                if (index == 3)
                {
                    ViewModel.Value.IsArrow1 = true;
                    ViewModel.Value.IsArrow2 = true;
                    ViewModel.Value.IsArrow3 = true;
                    ViewModel.Value.IsArrow4 = true;
                }
                index++;
                if (index == 5)
                {
                    index = 0;
                    ViewModel.Value.IsArrow1 = false;
                    ViewModel.Value.IsArrow2 = false;
                    ViewModel.Value.IsArrow3 = false;
                    ViewModel.Value.IsArrow4 = false;
                }
            }
        }
        private void ArrowChanged(ArrowChangedEvent.Args obj)
        {
            falg = obj.IsStart;
            if (falg)
            {
                ArrowChanged();
            }
        }

        public void ChangToMap()
        {
            EventAggregator.GetEvent<ChangedViewEvents>().Publish(new ChangedViewEvents.Args(true));
        }

        public void ChangToStation()
        {
            EventAggregator.GetEvent<ChangedViewEvents>().Publish(new ChangedViewEvents.Args(false));
        }

        public void ChangedNext(bool falg)
        {
            if (ViewModel.Value.NextStation != null)
            {
                ViewModel.Value.NextStation.IsNext = falg;
                if (!falg)
                {
                    ViewModel.Value.NextStation.IsPast = true;
                }
            }
        }

        public void ArrowChanged(bool flg)
        {

        }
    }
}