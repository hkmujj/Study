using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Controller;
using Subway.ShiJiaZhuangLine1.Subsystem.Model;

namespace Subway.ShiJiaZhuangLine1.Subsystem.ViewModels
{
    [Export]
    public class ShellViewModel : NotificationObject, INavigationAware, IUpdateStatusProvider, IDisposable, IDataListener
    {
        [ImportingConstructor]
        public ShellViewModel(MMIModel model, ShellController controller, TractionLockViewModel tractionLockViewModel)
        {
            Model = model;
            
            model.Dataserver = SubsysParams.Instance.SubsystemInitParam.CommunicationDataService;
            model.Init();
            Controller = controller;
            TractionLockViewModel = tractionLockViewModel;
            controller.ViewModel = this;
            controller.UpdateTitleName(ViewNames.MainRunningDoorPage);
        }

        public MMIModel Model { private set; get; }

        public ShellController Controller { private set; get; }

        public TractionLockViewModel TractionLockViewModel { private set; get; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Controller.UpdateTitleName(navigationContext);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }


        public void UpdateState(object sender, CommunicationDataChangedArgs e)
        {
            Model.ChangeStatus(sender, e);
        }

        public void Dispose()
        {
            Controller.Dispose();
            Model.Dispose();
            TractionLockViewModel.Dispose();
        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {

        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {

        }

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            UpdateState(sender, dataChangedArgs);
        }
    }
}