using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Constant;
using Subway.TCMS.LanZhou.Event;
using Subway.TCMS.LanZhou.Model;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Model.Domain.Fault;
using Subway.TCMS.LanZhou.Model.Domain.PIS;
using Subway.TCMS.LanZhou.Utils;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Controller.Domain
{
    [Export]
    public class FaultController : ControllerBase<FalutViewModel>
    {
        public ICommand Levele1Command { get; private set; }

        public ICommand Levele2Command { get; private set; }

        public ICommand Levele3Command { get; private set; }

        public ICommand NextPageCommand { get; private set; }

        public ICommand PrePageCommand { get; private set; }

        private const int FaultItemPerPage = 10;

        public FaultController()
        {
            Levele2Command = new DelegateCommand<string>(OnLevele2);

            Levele3Command = new DelegateCommand<string>(OnLevele3);

            NextPageCommand = new DelegateCommand<string>(OnNext);

            PrePageCommand = new DelegateCommand<string>(OnPre);
        }

        private void OnNext(string obj)
        {
            ViewModel.Model.CurrentShowingFaultModel.ShowingPage.GotoNextPage();
        }

        private void OnPre(string obj)
        {
            ViewModel.Model.CurrentShowingFaultModel.ShowingPage.GotoPrePage();

        }

        private void OnLevele2(string historyOrCurrent)
        {

            var fm = ViewModel.Model;

            ViewModel.Model.CurrentShowingFaultModel.ShowingPage =
                ViewModel.Model.CurrentShowingFaultModel.Level2ShowingPage;

            fm.CurrentShowingFaultModel.Level2ShowingPage.Reset(fm.AllFaults);
            // TODO 
            //ViewModel.Model.HistoryShowingFaultModel.ShowingPage =
            //    ViewModel.Model.HistoryShowingFaultModel.Level1ShowingPage;
        }

        private void OnLevele3(string historyOrCurrent)
        {
            var fm = ViewModel.Model;

            ViewModel.Model.CurrentShowingFaultModel.ShowingPage =
                ViewModel.Model.CurrentShowingFaultModel.Level3ShowingPage;
            fm.CurrentShowingFaultModel.Level2ShowingPage.Reset(fm.AllFaults);

            // TODO 
            //ViewModel.Model.HistoryShowingFaultModel.ShowingPage =
            //    ViewModel.Model.HistoryShowingFaultModel.Level1ShowingPage;
        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {  
            var fm = ViewModel.Model;

            fm.PropertyChanged += FaultModelOnPropertyChanged;
            fm.AllFaults.CollectionChanged += AllFaultsOnCollectionChanged;

            fm.CurrentShowingFaultModel.AllShowingPage = new PageWrapper<FaultItem>(FaultItemPerPage);
            fm.CurrentShowingFaultModel.AllShowingPage.Reset(fm.AllFaults);

            fm.CurrentShowingFaultModel.Level2ShowingPage = new PageWrapper<FaultItem>(FaultItemPerPage,
                item => item.FaultInfo.Level == TrainFaultLevel.Level2);
            fm.CurrentShowingFaultModel.Level2ShowingPage.Reset(fm.AllFaults);

            fm.CurrentShowingFaultModel.Level3ShowingPage = new PageWrapper<FaultItem>(FaultItemPerPage,
                item => item.FaultInfo.Level == TrainFaultLevel.Level3);
            fm.CurrentShowingFaultModel.Level3ShowingPage.Reset(fm.AllFaults);

            fm.CurrentShowingFaultModel.ShowingPage = fm.CurrentShowingFaultModel.AllShowingPage; 
        }

        private void AllFaultsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            ViewModel.Model.CurrentShowingFaultModel.ShowingPage.Reset(ViewModel.Model.AllFaults);
        }

        private void FaultModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(FaultModel.CreateFaultIndex))
            {
                CreateFault(ViewModel.Model.CreateFaultIndex);
            }
        }

        public void CreateFault(int createFaultIndex)
        {
            var fi = GlobalParam.Instance.CarFaultInfosConfigCollection.Value.FirstOrDefault(f => f.LogicIndex == createFaultIndex);
            if (fi != null)
            {
                ViewModel.Model.AllFaults.Add(new FaultItem(fi, DateTime.Now));
            }
        }
    }
}